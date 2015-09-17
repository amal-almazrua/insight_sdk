Notes:

###Download Insight SDK Lite

###Download Xavier Composer

####For Python dev:

  - You need to link to directory with `InsightEDK.DLL` and `edk_utils.dll`
  - You can reference the files, or insert them in directory running the python script.
  `C:\Program Files (x86)\Emotiv Insight SDK Lite v1.1.1.0\Dll`


1. Upload Arduino Code to Arduino Board

2. Run Xavier Composer if not connected to device

3. Run Python script `emostate_serial.py`

####Python
    - `logEmoState` is the function in the script that packs a tuple with various Emotiv States.
    - `valToArduino` sends the data in the tuple over serial to the Arduino.

####Arduino

    -Arduino receives serial and parses out values IN ORDER.

    `Time`, `userID`, `wirelessSigStatus`, `Blink`, `leftWink`, `rightWink`, `surprise`, `frown`, `clench`, `smile`, `longExcitement`, `shortExcitement`, `boredom`, `mentalCommandAction`, `mentalCommandPower`.
