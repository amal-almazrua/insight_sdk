/****************************************************************************
 **
 ** Copyright 2015 by Emotiv. All rights reserved
 **
 ** EmotivBluetooth.java
 **
 ** This file shows how to connect to the Insight headset via the
 ** Bluetooth service in Android SDK.
 **
 ** The code is part of the Insight SDK for Android, and usage of this code
 ** is under the same restriction of the EULA and SDK License of Insight SDK.
 **
 ****************************************************************************/

package com.emotiv.bluetooth;

import java.math.BigInteger;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import android.os.Handler;
import android.os.Message;
import android.bluetooth.le.BluetoothLeScanner;
import android.bluetooth.le.ScanCallback;
import android.bluetooth.le.ScanResult;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothGatt;
import android.bluetooth.BluetoothGattCallback;
import android.bluetooth.BluetoothGattCharacteristic;
import android.bluetooth.BluetoothGattDescriptor;
import android.bluetooth.BluetoothManager;
import android.content.Context;
import android.content.pm.PackageManager;
import android.util.Log;
import android.widget.Toast;

public class EmotivBluetooth {
    
    /* UUIDs for Insight */
    private final UUID DEVICE_SERVICE_UUID = UUID.fromString("0000180a-0000-1000-8000-00805f9b34fb");
    private final UUID DEVICE_BATERY_UUID = UUID.fromString("0000180f-0000-1000-8000-00805f9b34fb");
    private final UUID Serial_Characteristic_UUID = UUID.fromString("00002a25-0000-1000-8000-00805f9b34fb");
    private final UUID Firmware_Characteristic_UUID = UUID.fromString("00002a26-0000-1000-8000-00805f9b34fb");
    private final UUID Setting_Characteristic_UUID = UUID.fromString("81072F44-9F3D-11E3-A9DC-0002A5D5C51B");
    private final UUID Manufac_Characteristic_UUID = UUID.fromString("00002a29-0000-1000-8000-00805f9b34fb");

    private final UUID DATA_SERVICE_UUID = UUID.fromString("81072F40-9F3D-11E3-A9DC-0002A5D5C51B");
    private final UUID EEG_Characteristic_UUID = UUID.fromString("81072F41-9F3D-11E3-A9DC-0002A5D5C51B");
    private final UUID MEMS_Characteristic_UUID = UUID.fromString("81072F42-9F3D-11E3-A9DC-0002A5D5C51B");
    private final UUID Config_Characteristic_UUID = UUID.fromString("81072F43-9F3D-11E3-A9DC-0002A5D5C51B");
    private final UUID CLIENT_CHARACTERISTIC_CONFIG = UUID.fromString("00002902-0000-1000-8000-00805f9b34fb");
    private static final long SCAN_PERIOD = 1000;

    private Context mContext;
    public static EmotivBluetooth _emobluetooth = null;
    private BluetoothAdapter mBluetoothAdapter;
    private ScanCallback scan;
    private Handler mHandler;
    private Handler tHandler;
    private Toast toast = null;
    private int _TypeHeadset; // 0 for Insight ; 1 for EPOC

    BluetoothGatt bluetoothGatt = null;
    private List < BluetoothDevice > list_device_epoc = new ArrayList < BluetoothDevice > ();
    private List < BluetoothDevice > list_device_insight = new ArrayList < BluetoothDevice > ();

    private UUID[] device_uuid = {
        DEVICE_SERVICE_UUID
    };
    private boolean lock = false;
    public boolean haveData = false;
    private int start_counter;

    private int test_epoc;
    private int test_insight;
    private int CounterScanEpoc;
    private int CounterScanInsight;
    private int valueMode;
    private double testnortifi = -1;
    private double CheckNortifiBLE = 0;
    private boolean checkUser = false;
    public boolean isSettingMode;
    private boolean isConnected = false;
    public boolean isEnableScan = true;

    byte[] EEGBuffer = new byte[32];
    int[] SignalStrengthBLE = new int[2];

    byte[] MEMSBuffer = new byte[20];
    boolean isEnableBLE = false;
    boolean isScanDevice = false;
    public EmotivBluetooth(Context context) {
        isConnected = false;
        mContext = context;
        mHandler = new Handler();
        tHandler = new Handler();
        toast = Toast.makeText(mContext, "Bluetooth not enabled", Toast.LENGTH_SHORT);
        if (!mContext.getPackageManager().hasSystemFeature(PackageManager.FEATURE_BLUETOOTH_LE)) {
            Toast.makeText(mContext, "Bluetooth LE not supported on this device",
                           Toast.LENGTH_SHORT).show();
            return;
        }

        final BluetoothManager bluetoothManager = 
            (BluetoothManager) mContext.getSystemService(Context.BLUETOOTH_SERVICE);
        mBluetoothAdapter = bluetoothManager.getAdapter();

        // Checks if Bluetooth is supported on the device.
        if (mBluetoothAdapter == null) {
            return;
        }
        if (!mBluetoothAdapter.isEnabled()) {
            toast.show();
        }
        Thread checkBLEThread = new Thread() {@Override
            public void run() {
                // TODO Auto-generated method stub
                super.run();
                while (!isEnableBLE) {
                    try {
                        if (mBluetoothAdapter.isEnabled()) {
                            handler.sendEmptyMessage(1);
                            isEnableBLE = true;
                            break;
                        }
                        Thread.sleep(1000);
                    } catch (Exception ex) {
                        ex.printStackTrace();
                    }
                }
            }
        };
        checkBLEThread.start();

        Thread checkScanDevice = new Thread() {@Override
            public void run() {
                super.run();
                while (true) {
                    if (isScanDevice) {
                        try {
                            handler.sendEmptyMessage(2);
                            Thread.sleep(1000);
                        } catch (InterruptedException e) {
                            // TODO: handle exception
                            e.printStackTrace();
                        }
                    }
                }
            }
        };
        checkScanDevice.start();
    }

    Handler handler = new Handler() {
        public void handleMessage(Message msg) {
            switch (msg.what) {
                case 1:
                    scanLeDevice(true);
                    break;
                case 2:
                    {
                        if (test_epoc != CounterScanEpoc) {
                            test_epoc = CounterScanEpoc;
                        } else {
                            list_device_epoc.clear();
                            SignalStrengthBLE[1] = 0;
                            CounterScanEpoc = 0;
                        }
                        if (test_insight != CounterScanInsight) {
                            test_insight = CounterScanInsight;
                        } else {
                            list_device_insight.clear();
                            SignalStrengthBLE[0] = 0;
                            CounterScanInsight = 0;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    };

    Runnable thread_scan = new Runnable() {

        @Override
        public void run() {
            // TODO Auto-generated method stub
            test_epoc = 0;
            test_insight = 0;
            CounterScanEpoc = 0;
            CounterScanInsight = 0;
            try {

                if (android.os.Build.VERSION.SDK_INT < android.os.Build.VERSION_CODES.LOLLIPOP) {
                    mBluetoothAdapter.stopLeScan(mLeScanCallback);
                } else {
                    mBluetoothAdapter.getBluetoothLeScanner().stopScan(scan);
                }
                if (mBluetoothAdapter.isEnabled()) {
                    if (android.os.Build.VERSION.SDK_INT < android.os.Build.VERSION_CODES.LOLLIPOP) {
                        mBluetoothAdapter.startLeScan(device_uuid, mLeScanCallback);
                    } else {
                        mBluetoothAdapter.getBluetoothLeScanner().startScan(scan);
                    }
                }
            } catch (NullPointerException e) {
                // TODO: handle exception
            }
        }
    };

    /* Bug on Android 4.4.3:
       Get disconnected event 10s after headset is turned off.
       So we must check nortifi manually and call function disconnect().
     */
    Runnable thread_checknortifi = new Runnable() {

        @Override
        public void run() {
            // TODO Auto-generated method stub
            if (haveData) {
                if (testnortifi != CheckNortifiBLE) {
                    testnortifi = CheckNortifiBLE;
                } else {
                    if (bluetoothGatt != null && haveData) {
                        //  Log.e("_____BLE____","Call disconnect");
                        Thread thread = new Thread() {
                            public void run() {
                                try {
                                    bluetoothGatt.disconnect();
                                    haveData = false;
                                } catch (NullPointerException e) {
                                    // TODO: handle exception
                                }
                            };

                        };
                        thread.start();
                    }
                }
                tHandler.postDelayed(this, 1000);
            }
        }
    };

    /* Get number of devices with EPOC+ type */
    public int GetNumberDeviceEpocPlus() {
        return list_device_epoc.size();
    }

    /* Get number of devices with Insight type */
    public int GetNumberDeviceInsight() {
        return list_device_insight.size();
    }

    /* Get device name of EPOC+ headset */
    public String GetNameDeviceEpocPlus(int index) {
        String name = "";
        try {
            if (index < list_device_epoc.size()) {
                name = list_device_epoc.get(index).getName();
            }
        } catch (Exception e) {
            // TODO: handle exception
        }

        return name;
    }

    /* Get device name of Insight headset */
    public String GetNameDeviceInsight(int index) {
        String name = "";
        try {
            if (index < list_device_insight.size()) {
                name = list_device_insight.get(index).getName();
            }
        } catch (Exception e) {
            // TODO: handle exception
        }

        return name;
    }

    /* Feature Setting Mode when use EPOC+
     *   value = 0 -> epoc 14bit
     *   value = 1 -> epoc 16bit No motion
     *   value = 2 -> epoc 16bit 32hz motion
     *   value = 3 -> epoc 16bit 64hz motion
     */
    public int EmoSettingMode(int value) {
        if (value != 0 && value != 1 && value != 2) {
            return 0;
        }
        switch (value) {
            case 0:
                valueMode = 256;
                break;
            case 1:
                valueMode = 384;
                break;
            case 2:
                valueMode = 388;
                break;
            case 3:
                valueMode = 392;
                break;
            default:
                break;
        }
        return 1;
    }

    /* Connecting device */
    public boolean EmoConnectDevice(int idDevice, int indexDevice) {
        BluetoothDevice device = null;
        bluetoothGatt = null;
        switch (idDevice) {
            case 0:
                if (!list_device_insight.isEmpty() && indexDevice < list_device_insight.size()) {
                    device = list_device_insight.get(indexDevice);
                }
                break;
            case 1:
                if (!list_device_epoc.isEmpty() && indexDevice < list_device_epoc.size()) {
                    device = list_device_epoc.get(indexDevice);
                }
            default:
                break;
        }
        if (device != null && !isConnected) {
            mBluetoothAdapter.cancelDiscovery();
            bluetoothGatt = device.connectGatt(mContext, false, mBluetoothGattCallback);
        }
        if (bluetoothGatt != null) {
            scanLeDevice(false);
            return true;
        } else {
            return false;
        }
    }

    /* Get signal strength from Insight */
    public int GetSignalStrengthBLEInsight() {
        int[] value = new int[2];
        System.arraycopy(SignalStrengthBLE, 0, value, 0, 2);
        return value[0];
    }

    /* Get signal strength from EPOC+ */
    public int GetSignalStrengthBLEEPOCPLUS() {
        int[] value = new int[2];
        System.arraycopy(SignalStrengthBLE, 0, value, 0, 2);
        return value[1];
    }

    public int SettingModeForHeadset(int value) {
        if (list_device_epoc.isEmpty()) {
            return 0;
        } else {
            valueMode = value;
        }
        return 1;
    }

    /* Scan BTLE devices */
    private void scanLeDevice(final boolean enable) {
        if (enable) {
            mHandler.postDelayed(thread_scan, SCAN_PERIOD);
            isScanDevice = true;
            if (android.os.Build.VERSION.SDK_INT < android.os.Build.VERSION_CODES.LOLLIPOP) {
                scan = null;
                mBluetoothAdapter.startLeScan(device_uuid, mLeScanCallback);
            } else {
                scan = new ScanCallback() {@Override
                    public void onBatchScanResults(List < ScanResult > results) {

                        Log.i("bluetooth", "The batch result is " + results.size());
                    }

                    @Override
                    public void onScanResult(int callbackType, final ScanResult result) {
                        Thread thread = new Thread() {
                            public void run() {
                                try {
                                    BluetoothDevice device = result.getDevice();
                                    if (device.getName().contains("Insight")) {
                                        SignalStrengthBLE[0] = result.getRssi();
                                        CounterScanInsight++;
                                        if (!list_device_insight.contains(device)) {
                                            list_device_insight.add(device);
                                        }
                                    }
                                    if (device.getName().contains("EPOC+")) {
                                        SignalStrengthBLE[1] = result.getRssi();
                                        CounterScanEpoc++;
                                        if (!list_device_epoc.contains(device)) {
                                            list_device_epoc.add(device);
                                        }
                                    }
                                } catch (NullPointerException e) {
                                    // TODO: handle exception
                                }
                            };

                        };

                        thread.start();
                    }

                    @Override
                    public void onScanFailed(int errorCode) {
                        super.onScanFailed(errorCode);
                    }
                };

                try {
                    mBluetoothAdapter.getBluetoothLeScanner().startScan(scan);
                } catch (NullPointerException e) {
                    e.printStackTrace();
                }
            }

        } else {
            isScanDevice = false;
            mHandler.removeCallbacks(thread_scan);
            if (android.os.Build.VERSION.SDK_INT < android.os.Build.VERSION_CODES.LOLLIPOP) {
                mBluetoothAdapter.stopLeScan(mLeScanCallback);
            } else try {
                mBluetoothAdapter.getBluetoothLeScanner().stopScan(scan);
            } catch (NullPointerException e) {
                e.printStackTrace();
            }
        }

    }

    /* Callback for the BTLE device scan */
    private BluetoothAdapter.LeScanCallback mLeScanCallback = new BluetoothAdapter.LeScanCallback() {

        @Override
        public void onLeScan(final BluetoothDevice device, final int rssi,
                             final byte[] scanRecord) {
            Thread thread = new Thread() {
                public void run() {
                    try {
                        if (device.getName().contains("Insight")) {
                            SignalStrengthBLE[0] = rssi;
                            CounterScanInsight++;
                            if (!list_device_insight.contains(device)) {
                                list_device_insight.add(device);
                            }
                        }
                        if (device.getName().contains("EPOC+")) {
                            SignalStrengthBLE[1] = rssi;
                            CounterScanEpoc++;
                            if (!list_device_epoc.contains(device)) {
                                list_device_epoc.add(device);
                            }
                        }
                    } catch (NullPointerException e) {
                        // TODO: handle exception
                    }
                };

            };

            thread.start();

        }
    };

    /* Callback for services discovery */
    private BluetoothGattCallback mBluetoothGattCallback = new BluetoothGattCallback() {

        public void onServicesDiscovered(BluetoothGatt gatt, int status) {
            Thread thread = new Thread() {
                public void run() {
                    try {
                        Thread.sleep(100);
                    } catch (InterruptedException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                    if (_TypeHeadset == 0) {
                        ReadValueForCharacteristec(bluetoothGatt, DEVICE_SERVICE_UUID, Firmware_Characteristic_UUID);
                    }
                    if (_TypeHeadset == 1) {
                        if (!isSettingMode) {
                            ReadValueForCharacteristec(bluetoothGatt, DATA_SERVICE_UUID, Setting_Characteristic_UUID);
                        } else {
                            SetNortifyValue(bluetoothGatt, 2);
                        }
                    }

                };
            };

            thread.start();
        };

        public void onCharacteristicRead(BluetoothGatt gatt,
                                         BluetoothGattCharacteristic characteristic, int status) {
            if (characteristic.getUuid().equals(Serial_Characteristic_UUID)) {
                byte[] serial = characteristic.getValue();
                if (serial != null && serial.length > 0) {
                    SendSerialNumber(serial);
                }
                try {
                    Thread.sleep(100);
                } catch (InterruptedException e) {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }
                if (!checkUser) {
                    tHandler.postDelayed(thread_checknortifi, 1000);
                    checkUser = true;
                    SetNortifyValue(gatt, 0);
                }
            } else if (characteristic.getUuid().equals(Setting_Characteristic_UUID)) {
                byte[] setting = characteristic.getValue();
                if (setting != null && setting.length > 0) {
                    ReadUserConfig(characteristic.getValue());
                }
                ReadValueForCharacteristec(gatt, DEVICE_SERVICE_UUID, Firmware_Characteristic_UUID);
            } else if (characteristic.getUuid().equals(Firmware_Characteristic_UUID)) {
                byte[] firmware = characteristic.getValue();
                if (firmware != null && firmware.length > 0) {
                    SendFirmWareVersion(characteristic.getValue());
                }
                ReadValueForCharacteristec(gatt, DEVICE_SERVICE_UUID, Serial_Characteristic_UUID);
            }

        };

        /* Callback for getting data from BTLE */
        public void onCharacteristicChanged(BluetoothGatt gatt,
                                            BluetoothGattCharacteristic characteristic) {
            byte[] oldData = characteristic.getValue();
            if (characteristic.getUuid().equals(EEG_Characteristic_UUID)) {
                haveData = true;
                CheckNortifiBLE++;
                if (!lock) {
                    start_counter = (int) oldData[0];
                    lock = true;
                    int chunk_start = (int) oldData[1];
                    if (chunk_start == 1) System.arraycopy(oldData, 2, EEGBuffer, 0, 16);
                }
                if (start_counter == (int) oldData[0]) {
                    // receive 1 packet
                    System.arraycopy(oldData, 2, EEGBuffer, 16 * ((int) oldData[1] - 1), 16);
                    if ((int) oldData[1] == 2) {
                        WriteEEG(EEGBuffer);
                        if (start_counter >= 127) {
                            start_counter = 0;
                        } else {
                            start_counter++;
                        }
                    }
                } else {
                    if ((int) oldData[0] < 128 && (int) oldData[0] >= 0) {
                        start_counter = (int) oldData[0];
                        System.arraycopy(oldData, 2, EEGBuffer, 16 * ((int) oldData[1] - 1), 16);
                    }
                }
            } else if (characteristic.getUuid().equals(MEMS_Characteristic_UUID)) {
                System.arraycopy(oldData, 0, MEMSBuffer, 20, oldData.length);
                WriteMEMS(MEMSBuffer);
            }
        };

        public void onConnectionStateChange(final BluetoothGatt gatt,
                                            final int status, final int newState) {

            if (newState == BluetoothGatt.STATE_CONNECTED) {
                if (status != BluetoothGatt.GATT_SUCCESS) {
                    bluetoothGatt.disconnect();
                    bluetoothGatt.close();
                    list_device_insight.clear();
                    list_device_epoc.clear();
                } else {
                    isConnected = true;
                    try {
                        if (gatt.getDevice().getName().contains("Insight")) {
                            SetTypeHeadset(0);
                            _TypeHeadset = 0;
                        }
                        if (gatt.getDevice().getName().contains("EPOC+")) {
                            SetTypeHeadset(1);
                            _TypeHeadset = 1;
                        }

                    } catch (NullPointerException e) {
                        // TODO: handle exception
                    }
                    lock = false;
                    list_device_insight.clear();
                    list_device_epoc.clear();
                    Thread thread = new Thread() {
                        public void run() {
                            try {
                                Thread.sleep(200);
                            } catch (InterruptedException e) {
                                // TODO Auto-generated catch block
                                e.printStackTrace();
                            }
                            gatt.discoverServices();
                        };

                    };
                    thread.start();
                }
            } else if (newState == BluetoothGatt.STATE_DISCONNECTED) {
                isConnected = false;
                Thread thread = new Thread() {
                    public void run() {
                        try {
                            Thread.sleep(50);
                        } catch (InterruptedException e) {
                            // TODO Auto-generated catch block
                            e.printStackTrace();
                        }
                        gatt.close();
                        bluetoothGatt = null;
                        if (checkUser) {
                            checkUser = false;
                            DisconnectDevice();
                            tHandler.removeCallbacks(thread_checknortifi);
                        }
                        scanLeDevice(false);
                        scanLeDevice(true);
                    };
                };
                thread.start();
            }
        };

        public void onDescriptorWrite(BluetoothGatt gatt, BluetoothGattDescriptor descriptor, int status) {
            if (!isSettingMode) {
                if (_TypeHeadset != 0) {
                    SetNortifyValue(gatt, 1);
                }
            } else {
                WriteValueForCharacteristic(gatt, valueMode);
            }
        }

        public void onCharacteristicWrite(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, int status) {
            if (status == BluetoothGatt.GATT_SUCCESS) {
                byte result[] = characteristic.getValue();
                if (result[0] == 0x01) {
                    isSettingMode = false;
                    ReadValueForCharacteristec(gatt, DATA_SERVICE_UUID, Setting_Characteristic_UUID);
                }
            }
        }

        /* Setting read charesteristic */
        private void ReadValueForCharacteristec(BluetoothGatt gatt, UUID serviceUUID, UUID characteristicUUID) {
            try {
                BluetoothGattCharacteristic characteristic = 
                    gatt.getService(serviceUUID).getCharacteristic(characteristicUUID);
                gatt.readCharacteristic(characteristic);
            } catch (NullPointerException e) {
                // TODO: handle exception
            }
        }

        /* Setting write charesteristic */
        private void WriteValueForCharacteristic(BluetoothGatt gatt, int value) {
            try {
                BluetoothGattCharacteristic characteristic = gatt.getService(DATA_SERVICE_UUID)
                    .getCharacteristic(Config_Characteristic_UUID);
                BigInteger data = BigInteger.valueOf(value);
                if (characteristic != null) {
                    characteristic.setValue(data.toByteArray());
                    gatt.writeCharacteristic(characteristic);
                }
            } catch (NullPointerException e) {
                // TODO: handle exception
            }
        }

        /* Setting notifiy charesteristic */
        private void SetNortifyValue(BluetoothGatt gatt, int state) {
            BluetoothGattCharacteristic characteristic;
            switch (state) {
                case 0:
                    {
                        characteristic = gatt.getService(DATA_SERVICE_UUID)
                            .getCharacteristic(EEG_Characteristic_UUID);
                    }
                    break;
                case 1:
                    {
                        characteristic = gatt.getService(DATA_SERVICE_UUID)
                            .getCharacteristic(MEMS_Characteristic_UUID);
                    }
                case 2:
                    {
                        characteristic = gatt.getService(DATA_SERVICE_UUID)
                            .getCharacteristic(Config_Characteristic_UUID);
                    }
                    break;
                default:
                    return;
            }

            // Enable local notifications
            try {
                Thread.sleep(200);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            gatt.setCharacteristicNotification(characteristic, true);

            // Enabled remote notifications
            BluetoothGattDescriptor desc = characteristic.getDescriptor(CLIENT_CHARACTERISTIC_CONFIG);
            desc.setValue(BluetoothGattDescriptor.ENABLE_NOTIFICATION_VALUE);
            gatt.writeDescriptor(desc);
        };
    };

    /* Native function call in C++ */
    private native void SendSerialNumber(byte[] serial);
    private native void SendFirmWareVersion(byte[] value);
    private native void WriteEEG(byte[] serial);
    private native void WriteMEMS(byte[] serial);
    private native void SetTypeHeadset(int type);
    private native void ReadUserConfig(byte[] data);
    private native void DisconnectDevice();
    
    {
        System.loadLibrary("bedk");
    }
}
