using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Emotiv
{
    public class EdkDll
    {
        public const Int32 EDK_OK                           = 0x0000;
        public const Int32 EDK_UNKNOWN_ERROR                = 0x0001;

        public const Int32 EDK_INVALID_PROFILE_ARCHIVE      = 0x0101;
        public const Int32 EDK_NO_USER_FOR_BASEPROFILE      = 0x0102;

        public const Int32 EDK_CANNOT_ACQUIRE_DATA          = 0x0200;

        public const Int32 EDK_BUFFER_TOO_SMALL             = 0x0300;
        public const Int32 EDK_OUT_OF_RANGE                 = 0x0301;
        public const Int32 EDK_INVALID_PARAMETER            = 0x0302;
        public const Int32 EDK_PARAMETER_LOCKED             = 0x0303;
        public const Int32 EDK_MC_INVALID_TRAINING_ACTION   = 0x0304;
        public const Int32 EDK_MC_INVALID_TRAINING_CONTROL  = 0x0305;
        public const Int32 EDK_MC_INVALID_ACTIVE_ACTION     = 0x0306;
        public const Int32 EDK_MC_EXCESS_MAX_ACTIONS        = 0x0307;
        public const Int32 EDK_FE_NO_SIG_AVAILABLE          = 0x0308;
        public const Int32 EDK_FILESYSTEM_ERROR             = 0x0309;

        public const Int32 EDK_INVALID_USER_ID              = 0x0400;

        public const Int32 EDK_EMOENGINE_UNINITIALIZED      = 0x0500;
        public const Int32 EDK_EMOENGINE_DISCONNECTED       = 0x0501;
        public const Int32 EDK_EMOENGINE_PROXY_ERROR        = 0x0502;

        public const Int32 EDK_NO_EVENT                     = 0x0600;

        public const Int32 EDK_GYRO_NOT_CALIBRATED          = 0x0700;

        public const Int32 EDK_OPTIMIZATION_IS_ON           = 0x0800;

        public const Int32 EDK_RESERVED1                    = 0x0900;

        public enum IEE_FacialExpressionThreshold_t
        {
            FE_SENSITIVITY
        } ;

        public enum IEE_FacialExpressionTrainingControl_t
        {
            FE_NONE = 0, FE_START, FE_ACCEPT, FE_REJECT, FE_ERASE, FE_RESET
        } ;

        public enum IEE_FacialExpressionSignature_t
        {
            FE_SIG_UNIVERSAL = 0, FE_SIG_TRAINED
        } ;

        public enum IEE_MentalCommandTrainingControl_t 
        {
            MC_NONE = 0, MC_START, MC_ACCEPT, MC_REJECT, MC_ERASE, MC_RESET
        } ;

        public enum IEE_Event_t
        {
            IEE_UnknownEvent          = 0x0000,
            IEE_EmulatorError         = 0x0001,
            IEE_ReservedEvent         = 0x0002,
            IEE_UserAdded             = 0x0010,
            IEE_UserRemoved           = 0x0020,
            IEE_EmoStateUpdated       = 0x0040,
            IEE_ProfileEvent          = 0x0080,
            IEE_MentalCommandEvent    = 0x0100,
            IEE_FacialExpressionEvent = 0x0200,
            IEE_InternalStateChanged  = 0x0400,
            IEE_AllEvent              = IEE_UserAdded | IEE_UserRemoved | IEE_EmoStateUpdated | IEE_ProfileEvent |
                                       IEE_MentalCommandEvent | IEE_FacialExpressionEvent | IEE_InternalStateChanged
        } ;

        public enum IEE_FacialExpressionEvent_t 
        {
            IEE_FacialExpressionNoEvent = 0,
            IEE_FacialExpressionTrainingStarted,
            IEE_FacialExpressionTrainingSucceeded,
            IEE_FacialExpressionTrainingFailed,
            IEE_FacialExpressionTrainingCompleted,
            IEE_FacialExpressionTrainingDataErased,
            IEE_FacialExpressionTrainingRejected,
            IEE_FacialExpressionTrainingReset
        } ;

        public enum IEE_MentalCommandEvent_t 
        {
            IEE_MentalCommandNoEvent = 0,
            IEE_MentalCommandTrainingStarted,
            IEE_MentalCommandTrainingSucceeded,
            IEE_MentalCommandTrainingFailed,
            IEE_MentalCommandTrainingCompleted,
            IEE_MentalCommandTrainingDataErased,
            IEE_MentalCommandTrainingRejected,
            IEE_MentalCommandTrainingReset,
            IEE_MentalCommandAutoSamplingNeutralCompleted,
            IEE_MentalCommandSignatureUpdated
        } ;

        public enum IEE_DataChannel_t   
        {
            IED_COUNTER = 0,        //!< Sample counter
            IED_INTERPOLATED = 1,   //!< Indicate if data is interpolated
            IED_RAW_CQ = 2,         //!< Raw contact quality value
            IED_AF3 = 3,            //!< Channel AF3
            IED_T7 = 7,             //!< Channel T7
            IED_Pz = 9,             //!< Channel Pz
            IED_T8 = 12,            //!< Channel T8
            IED_AF4 = 16,           //!< Channel AF4
            IED_TIMESTAMP = 19,     //!< System timestamp
            IED_ES_TIMESTAMP = 20,  //!< EmoState timestamp
            IED_FUNC_ID = 21,       //!< Reserved function id
            IED_FUNC_VALUE = 22,    //!< Reserved function value
            IED_MARKER = 23,        //!< Marker value from hardware
            IED_SYNC_SIGNAL = 24    //!< Synchronisation signal
	    } ;

        public enum IEE_MotionDataChannel_t 
        {
            IMD_COUNTER = 0,        //!< Sample counter
            IMD_GYROX,              //!< Gyroscope X-axis
            IMD_GYROY,              //!< Gyroscope Y-axis
            IMD_GYROZ,              //!< Gyroscope Z-axis
            IMD_ACCX,               //!< Accelerometer X-axis
            IMD_ACCY,               //!< Accelerometer Y-axis
            IMD_ACCZ,               //!< Accelerometer Z-axis
            IMD_MAGX,               //!< Magnetometer X-axis
            IMD_MAGY,               //!< Magnetometer Y-axis
            IMD_MAGZ,               //!< Magnetometer Z-axis
            IMD_TIMESTAMP           //!< Timestamp of the motion data stream
        } ;

        [StructLayout(LayoutKind.Sequential)]
        public class IInputSensorDescriptor_t
        {
            public IEE_InputChannels_t channelId; // logical channel id
            public Int32 fExists;                 // does this sensor exist on this headset model
            public String pszLabel;               // text label identifying this sensor
            public Double xLoc;                   // x coordinate from center of head towards nose
            public Double yLoc;                   // y coordinate from center of head towards ears
            public Double zLoc;                   // z coordinate from center of head toward top of skull
        }

        public enum IEE_EmotivSuite_t
        {
            IEE_FACIALEXPRESSION = 0, IEE_PERFORMANCEMETRIC, IEE_MENTALCOMMAND
        } ;

        public enum IEE_FacialExpressionAlgo_t
        {
            FE_NEUTRAL     = 0x0001,
            FE_BLINK       = 0x0002,
            FE_WINK_LEFT   = 0x0004,
            FE_WINK_RIGHT  = 0x0008,
            FE_HORIEYE     = 0x0010,
            FE_SUPRISE     = 0x0020,
            FE_FROWN       = 0x0040,
            FE_SMILE       = 0x0080,
            FE_CLENCH      = 0x0100,
        } ;

        public enum IEE_PerformanceMetricAlgo_t
        {
            PM_EXCITEMENT      = 0x0001,
            PM_RELAXATION      = 0x0002,
            PM_STRESS          = 0x0004,
            PM_ENGAGEMENT      = 0x0008,
            PM_INTEREST        = 0x0010
        } ;

        public enum IEE_MentalCommandAction_t
        {
            MC_NEUTRAL                  = 0x0001,
            MC_PUSH                     = 0x0002,
            MC_PULL                     = 0x0004,
            MC_LIFT                     = 0x0008,
            MC_DROP                     = 0x0010,
            MC_LEFT                     = 0x0020,
            MC_RIGHT                    = 0x0040,
            MC_ROTATE_LEFT              = 0x0080,
            MC_ROTATE_RIGHT             = 0x0100,
            MC_ROTATE_CLOCKWISE         = 0x0200,
            MC_ROTATE_COUNTER_CLOCKWISE = 0x0400,
            MC_ROTATE_FORWARDS          = 0x0800,
            MC_ROTATE_REVERSE           = 0x1000,
            MC_DISAPPEAR                = 0x2000

        } ;
        public enum IEE_SignalStrength_t
        {
            NO_SIG = 0, BAD_SIG, GOOD_SIG
        } ;

        public enum IEE_InputChannels_t
        {
            IEE_CHAN_CMS = 0, 
            IEE_CHAN_DRL = 0, 
            IEE_CHAN_AF3 = 3, 
            IEE_CHAN_T7  = 7,
		    IEE_CHAN_Pz  = 9, 
            IEE_CHAN_T8  = 12, 
            IEE_CHAN_AF4 = 16
        } ;

        public enum IEE_EEG_ContactQuality_t
        {
            IEEG_CQ_NO_SIGNAL, 
            IEEG_CQ_VERY_BAD, 
            IEEG_CQ_POOR, 
            IEEG_CQ_FAIR, 
            IEEG_CQ_GOOD 
        } ;

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EngineConnect")]
        static extern Int32 Unmanged_IEE_EngineConnect(String security);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EngineLocalConnect")]
        static extern Int32 Unmanged_IEE_EngineLocalConnect(String pathEEGFile, String pathMotionFile);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EngineRemoteConnect")]
        static extern Int32 Unmanged_IEE_EngineRemoteConnect(String szHost, UInt16 port);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EngineDisconnect")]
        static extern Int32 Unmanged_IEE_EngineDisconnect();
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EnableDiagnostics")]
        static extern Int32 Unmanged_IEE_EnableDiagnostics(String szFilename, Int32 fEnable, Int32 nReserved);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EmoEngineEventCreate")]
        static extern IntPtr Unmanged_IEE_EmoEngineEventCreate();
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_ProfileEventCreate")]
        static extern IntPtr Unmanged_IEE_ProfileEventCreate();
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EmoEngineEventFree")]
        static extern void Unmanged_IEE_EmoEngineEventFree(IntPtr hEvent);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EmoStateCreate")]
        static extern IntPtr Unmanged_IEE_EmoStateCreate();
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EmoStateFree")]
        static extern void Unmanged_IEE_EmoStateFree(IntPtr hState);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EmoEngineEventGetType")]
        static extern IEE_Event_t Unmanged_IEE_EmoEngineEventGetType(IntPtr hEvent);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandEventGetType")]
        static extern IEE_MentalCommandEvent_t Unmanged_IEE_MentalCommandEventGetType(IntPtr hEvent);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionEventGetType")]
        static extern IEE_FacialExpressionEvent_t Unmanged_IEE_FacialExpressionEventGetType(IntPtr hEvent);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EmoEngineEventGetUserId")]
        static extern Int32 Unmanged_IEE_EmoEngineEventGetUserId(IntPtr hEvent, out UInt32 pUserIdOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EmoEngineEventGetEmoState")]
        static extern Int32 Unmanged_IEE_EmoEngineEventGetEmoState(IntPtr hEvent, IntPtr hEmoState);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EngineGetNextEvent")]
        static extern Int32 Unmanged_IEE_EngineGetNextEvent(IntPtr hEvent);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EngineClearEventQueue")]
        static extern Int32 Unmanged_IEE_EngineClearEventQueue(Int32 eventTypes);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EngineGetNumUser")]
        static extern Int32 Unmanged_IEE_EngineGetNumUser(out UInt32 pNumUserOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_SetHardwarePlayerDisplay")]
        static extern Int32 Unmanged_IEE_SetHardwarePlayerDisplay(UInt32 userId, UInt32 playerNum);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_SetUserProfile")]
        static extern Int32 Unmanged_IEE_SetUserProfile(UInt32 userId, Byte[] profileBuffer, UInt32 length);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_GetUserProfile")]
        static extern Int32 Unmanged_IEE_GetUserProfile(UInt32 userId, IntPtr hEvent);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_GetBaseProfile")]
        static extern Int32 Unmanged_IEE_GetBaseProfile(IntPtr hEvent);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_GetUserProfileSize")]
        static extern Int32 Unmanged_IEE_GetUserProfileSize(IntPtr hEvt, out UInt32 pProfileSizeOut);


        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_GetUserProfileBytes")]
        static extern Int32 Unmanged_IEE_GetUserProfileBytes(IntPtr hEvt, Byte[] destBuffer, UInt32 length);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_LoadUserProfile")]
        static extern Int32 Unmanged_IEE_LoadUserProfile(UInt32 userID, String szInputFilename);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_SaveUserProfile")]
        static extern Int32 Unmanged_IEE_SaveUserProfile(UInt32 userID, String szOutputFilename);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionSetThreshold")]
        static extern Int32 Unmanged_IEE_FacialExpressionSetThreshold(UInt32 userId, IEE_FacialExpressionAlgo_t algoName, IEE_FacialExpressionThreshold_t thresholdName, Int32 value);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionGetThreshold")]
        static extern Int32 Unmanged_IEE_FacialExpressionGetThreshold(UInt32 userId, IEE_FacialExpressionAlgo_t algoName, IEE_FacialExpressionThreshold_t thresholdName, out Int32 pValueOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionSetTrainingAction")]
        static extern Int32 Unmanged_IEE_FacialExpressionSetTrainingAction(UInt32 userId, IEE_FacialExpressionAlgo_t action);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionSetTrainingControl")]
        static extern Int32 Unmanged_IEE_FacialExpressionSetTrainingControl(UInt32 userId, IEE_FacialExpressionTrainingControl_t control);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionGetTrainingAction")]
        static extern Int32 Unmanged_IEE_FacialExpressionGetTrainingAction(UInt32 userId, out IEE_FacialExpressionAlgo_t pActionOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionGetTrainingTime")]
        static extern Int32 Unmanged_IEE_FacialExpressionGetTrainingTime(UInt32 userId, out UInt32 pTrainingTimeOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionGetTrainedSignatureActions")]
        static extern Int32 Unmanged_IEE_FacialExpressionGetTrainedSignatureActions(UInt32 userId, out UInt32 pTrainedActionsOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionGetTrainedSignatureAvailable")]
        static extern Int32 Unmanged_IEE_FacialExpressionGetTrainedSignatureAvailable(UInt32 userId, out Int32 pfAvailableOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionSetSignatureType")]
        static extern Int32 Unmanged_IEE_FacialExpressionSetSignatureType(UInt32 userId, IEE_FacialExpressionSignature_t sigType);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_FacialExpressionGetSignatureType")]
        static extern Int32 Unmanged_IEE_FacialExpressionGetSignatureType(UInt32 userId, out IEE_FacialExpressionSignature_t pSigTypeOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandSetActiveActions")]
        static extern Int32 Unmanged_IEE_MentalCommandSetActiveActions(UInt32 userId, UInt32 activeActions);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetActiveActions")]
        static extern Int32 Unmanged_IEE_MentalCommandGetActiveActions(UInt32 userId, out UInt32 pActiveActionsOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetTrainingTime")]
        static extern Int32 Unmanged_IEE_MentalCommandGetTrainingTime(UInt32 userId, out UInt32 pTrainingTimeOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandSetTrainingControl")]
        static extern Int32 Unmanged_IEE_MentalCommandSetTrainingControl(UInt32 userId, IEE_MentalCommandTrainingControl_t control);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandSetTrainingAction")]
        static extern Int32 Unmanged_IEE_MentalCommandSetTrainingAction(UInt32 userId, IEE_MentalCommandAction_t action);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetTrainingAction")]
        static extern Int32 Unmanged_IEE_MentalCommandGetTrainingAction(UInt32 userId, out IEE_MentalCommandAction_t pActionOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetTrainedSignatureActions")]
        static extern Int32 Unmanged_IEE_MentalCommandGetTrainedSignatureActions(UInt32 userId, out UInt32 pTrainedActionsOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetOverallSkillRating")]
        static extern Int32 Unmanged_IEE_MentalCommandGetOverallSkillRating(UInt32 userId, out Single pOverallSkillRatingOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetActionSkillRating")]
        static extern Int32 Unmanged_IEE_MentalCommandGetActionSkillRating(UInt32 userId, IEE_MentalCommandAction_t action, out Single pActionSkillRatingOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandSetActivationLevel")]
        static extern Int32 Unmanged_IEE_MentalCommandSetActivationLevel(UInt32 userId, Int32 level);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandSetActionSensitivity")]
        static extern Int32 Unmanged_IEE_MentalCommandSetActionSensitivity(UInt32 userId,
                                            Int32 action1Sensitivity, Int32 action2Sensitivity,
                                            Int32 action3Sensitivity, Int32 action4Sensitivity);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetActivationLevel")]
        static extern Int32 Unmanged_IEE_MentalCommandGetActivationLevel(UInt32 userId, out Int32 pLevelOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetActionSensitivity")]
        static extern Int32 Unmanged_IEE_MentalCommandGetActionSensitivity(UInt32 userId,
                                            out Int32 pAction1SensitivityOut, out Int32 pAction2SensitivityOut,
                                            out Int32 pAction3SensitivityOut, out Int32 pAction4SensitivityOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandStartSamplingNeutral")]
        static extern Int32 Unmanged_IEE_MentalCommandStartSamplingNeutral(UInt32 userId);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandStopSamplingNeutral")]
        static extern Int32 Unmanged_IEE_MentalCommandStopSamplingNeutral(UInt32 userId);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandSetSignatureCaching")]
        static extern Int32 Unmanged_IEE_MentalCommandSetSignatureCaching(UInt32 userId, UInt32 enabled);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetSignatureCaching")]
        static extern Int32 Unmanged_IEE_MentalCommandGetSignatureCaching(UInt32 userId, out UInt32 pEnabledOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandSetSignatureCacheSize")]
        static extern Int32 Unmanged_IEE_MentalCommandSetSignatureCacheSize(UInt32 userId, UInt32 size);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MentalCommandGetSignatureCacheSize")]
        static extern Int32 Unmanged_IEE_MentalCommandGetSignatureCacheSize(UInt32 userId, out UInt32 pSizeOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_HeadsetGetSensorDetails")]
        static extern Int32 Unmanged_IEE_HeadsetGetSensorDetails(IEE_InputChannels_t channelId, out IInputSensorDescriptor_t pDescriptorOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_HardwareGetVersion")]
        static extern Int32 Unmanged_IEE_HardwareGetVersion(UInt32 userId, out UInt32 pHwVersionOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_SoftwareGetVersion")]
        static extern Int32 Unmanged_IEE_SoftwareGetVersion(StringBuilder pszVersionOut, UInt32 nVersionChars, out UInt32 pBuildNumOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_HeadsetGetGyroDelta")]
        static extern Int32 Unmanged_IEE_HeadsetGetGyroDelta(UInt32 userId, out Int32 pXOut, out Int32 pYOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_HeadsetGyroRezero")]
        static extern Int32 Unmanged_IEE_HeadsetGyroRezero(UInt32 userId);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_OptimizationParamCreate")]
        static extern IntPtr Unmanged_IEE_OptimizationParamCreate();

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_OptimizationParamFree")]
        static extern void Unmanged_IEE_OptimizationParamFree(IntPtr hParam);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_OptimizationEnable")]
        static extern Int32 Unmanged_IEE_OptimizationEnable(IntPtr hParam);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_OptimizationIsEnabled")]
        static extern Int32 Unmanged_IEE_OptimizationIsEnabled(out Boolean pEnabledOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_OptimizationDisable")]
        static extern Int32 Unmanged_IEE_OptimizationDisable();

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_OptimizationGetParam")]
        static extern Int32 Unmanged_IEE_OptimizationGetParam(IntPtr hParam);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_OptimizationGetVitalAlgorithm")]
        static extern Int32 Unmanged_IEE_OptimizationGetVitalAlgorithm(IntPtr hParam, IEE_EmotivSuite_t suite, out UInt32 pVitalAlgorithmBitVectorOut);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_OptimizationSetVitalAlgorithm")]
        static extern Int32 Unmanged_IEE_OptimizationSetVitalAlgorithm(IntPtr hParam, IEE_EmotivSuite_t suite, UInt32 vitalAlgorithmBitVector);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_ResetDetection")]
        static extern Int32 Unmanged_IEE_ResetDetection(UInt32 userId, IEE_EmotivSuite_t suite, UInt32 detectionBitVector);

        //Motion Data
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataCreate")]
        static extern IntPtr Unmanaged_IEE_MotionDataCreate();
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataFree")]
        static extern void Unmanaged_IEE_MotionDataFree(IntPtr hData);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataUpdateHandle")]
        static extern Int32 Unmanaged_IEE_MotionDataUpdateHandle(UInt32 userId, IntPtr hData);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataGet")]
        static extern Int32 Unmanaged_IEE_MotionDataGet(IntPtr hData, IEE_MotionDataChannel_t channel, Double[] buffer, UInt32 bufferSizeInSample);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataGetMultiChannels")]
        static extern Int32 Unmanaged_IEE_MotionDataGetMultiChannels(IntPtr hData, IEE_MotionDataChannel_t[] channelList, UInt32 nChannel, Double[][] buffer, UInt32 bufferSizeInSample);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataGetNumberOfSample")]
        static extern Int32 Unmanaged_IEE_MotionDataGetNumberOfSample(IntPtr hData, out UInt32 nSampleOut);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataSetBufferSizeInSec")]
        static extern Int32 Unmanaged_IEE_MotionDataSetBufferSizeInSec(Single bufferSizeInSec);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataGetBufferSizeInSec")]
        static extern Int32 Unmanaged_IEE_MotionDataGetBufferSizeInSec(out Single pBufferSizeInSecOut);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataAcquisitionEnable")]
        static extern Int32 Unmanaged_IEE_MotionDataAcquisitionEnable(UInt32 userId, Boolean enable);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataAcquisitionIsEnabled")]
        static extern Int32 Unmanaged_IEE_MotionDataAcquisitionIsEnabled(UInt32 userId, out Boolean pEnableOut);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_MotionDataGetSamplingRate")]
        static extern Int32 Unmanaged_IEE_MotionDataGetSamplingRate(UInt32 userId, out UInt32 pSamplingRate);

        //EEG Data
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataCreate")]
        static extern IntPtr Unmanaged_IEE_DataCreate();
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataFree")]
        static extern void Unmanaged_IEE_DataFree(IntPtr hData);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataUpdateHandle")]
        static extern Int32 Unmanaged_IEE_DataUpdateHandle(UInt32 userId, IntPtr hData);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataGet")]
        static extern Int32 Unmanaged_IEE_DataGet(IntPtr hData, IEE_DataChannel_t channel, Double[] buffer, UInt32 bufferSizeInSample);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataGetMultiChannels")]
        static extern Int32 Unmanaged_IEE_DataGetMultiChannels(IntPtr hData, IEE_DataChannel_t[] channelList, UInt32 nChannel  , Double[][] buffer, UInt32 bufferSizeInSample);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataGetNumberOfSample")]
        static extern Int32 Unmanaged_IEE_DataGetNumberOfSample(IntPtr hData, out UInt32 nSampleOut);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataSetBufferSizeInSec")]
        static extern Int32 Unmanaged_IEE_DataSetBufferSizeInSec(Single bufferSizeInSec);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataGetBufferSizeInSec")]
        static extern Int32 Unmanaged_IEE_DataGetBufferSizeInSec(out Single pBufferSizeInSecOut);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataAcquisitionEnable")]
        static extern Int32 Unmanaged_IEE_DataAcquisitionEnable(UInt32 userId, Boolean enable);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataAcquisitionIsEnabled")]
        static extern Int32 Unmanaged_IEE_DataAcquisitionIsEnabled(UInt32 userId, out Boolean pEnableOut);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataSetSychronizationSignal")]
        static extern Int32 Unmanaged_IEE_DataSetSychronizationSignal(UInt32 userId, Int32 signal);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataSetMarker")]
        static extern Int32 Unmanaged_IEE_DataSetMarker(UInt32 userId, Int32 marker);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_DataGetSamplingRate")]
        static extern Int32 Unmanaged_IEE_DataGetSamplingRate(UInt32 userId, out UInt32 pSamplingRate);

        //EDF File
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EdfStart")]
        static extern Int32 Unmanaged_IEE_EdfStart();
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EdfStop")]
        static extern Int32 Unmanaged_IEE_EdfStop();
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EdfSeek")]
        static extern Int32 Unmanaged_IEE_EdfSeek(UInt32 sec);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EdfGetTotalTime")]
        static extern Int32 Unmanaged_IEE_EdfGetTotalTime(out UInt32 totalTime);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EdfStartSaving")]
        static extern Int32 Unmanaged_IEE_EdfStartSaving(UInt32 userID, String filename, String patientID, String recordID, String date, String time);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EdfStopSaving")]
        static extern Int32 Unmanaged_IEE_EdfStopSaving(UInt32 userID);
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IEE_EdfStopSavingAll")]
        static extern Int32 Unmanaged_IEE_EdfStopSavingAll();

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_Create")]
        static extern IntPtr Unmanaged_IS_Create();

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_Free")]
        static extern void Unmanaged_IS_Free(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_Init")]
        static extern void Unmanaged_IS_Init(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_GetTimeFromStart")]
        static extern Single Unmanaged_IS_GetTimeFromStart(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_GetHeadsetOn")]
        static extern Int32 Unmanaged_IS_GetHeadsetOn(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_GetNumContactQualityChannels")]
        static extern Int32 Unmanaged_IS_GetNumContactQualityChannels(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_GetContactQuality")]
        static extern IEE_EEG_ContactQuality_t Unmanaged_IS_GetContactQuality(IntPtr state, Int32 electroIdx);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_GetContactQualityFromAllChannels")]
        static extern Int32 Unmanaged_IS_GetContactQualityFromAllChannels(IntPtr state, IEE_EEG_ContactQuality_t[] contactQuality, UInt32 numChannels);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionIsBlink")]
        static extern Boolean Unmanaged_IS_FacialExpressionIsBlink(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionIsLeftWink")]
        static extern Boolean Unmanaged_IS_FacialExpressionIsLeftWink(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionIsRightWink")]
        static extern Boolean Unmanaged_IS_FacialExpressionIsRightWink(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionIsEyesOpen")]
        static extern Boolean Unmanaged_IS_FacialExpressionIsEyesOpen(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionIsLookingUp")]
        static extern Boolean Unmanaged_IS_FacialExpressionIsLookingUp(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionIsLookingDown")]
        static extern Boolean Unmanaged_IS_FacialExpressionIsLookingDown(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionGetEyelidState")]
        static extern void Unmanaged_IS_FacialExpressionGetEyelidState(IntPtr state, out Single leftEye, out Single rightEye);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionGetEyeLocation")]
        static extern void Unmanaged_IS_FacialExpressionGetEyeLocation(IntPtr state, out Single x, out Single y);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionGetEyebrowExtent")]
        static extern Single Unmanaged_IS_FacialExpressionGetEyebrowExtent(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionGetSmileExtent")]
        static extern Single Unmanaged_IS_FacialExpressionGetSmileExtent(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionGetClenchExtent")]
        static extern Single Unmanaged_IS_FacialExpressionGetClenchExtent(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionGetUpperFaceAction")]
        static extern IEE_FacialExpressionAlgo_t Unmanaged_IS_FacialExpressionGetUpperFaceAction(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionGetUpperFaceActionPower")]
        static extern Single Unmanaged_IS_FacialExpressionGetUpperFaceActionPower(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionGetLowerFaceAction")]
        static extern IEE_FacialExpressionAlgo_t Unmanaged_IS_FacialExpressionGetLowerFaceAction(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionGetLowerFaceActionPower")]
        static extern Single Unmanaged_IS_FacialExpressionGetLowerFaceActionPower(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionIsActive")]
        static extern Boolean Unmanaged_IS_FacialExpressionIsActive(IntPtr state, IEE_FacialExpressionAlgo_t type);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetExcitementLongTermScore")]
        static extern Single Unmanaged_IS_PerformanceMetricGetExcitementLongTermScore(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetInstantaneousExcitementScore")]
        static extern Single Unmanaged_IS_PerformanceMetricGetInstantaneousExcitementScore(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricIsActive")]
        static extern Boolean Unmanaged_IS_PerformanceMetricIsActive(IntPtr state, IEE_PerformanceMetricAlgo_t type);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetRelaxationScore")]
        static extern Single Unmanaged_IS_PerformanceMetricGetRelaxationScore(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetStressScore")]
        static extern Single Unmanaged_IS_PerformanceMetricGetStressScore(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetEngagementBoredomScore")]
        static extern Single Unmanaged_IS_PerformanceMetricGetEngagementBoredomScore(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetInterestScore")]
        static extern Single Unmanaged_IS_PerformanceMetricGetInterestScore(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetFocusScore")]
        static extern Single Unmanaged_IS_PerformanceMetricGetFocusScore(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_MentalCommandGetCurrentAction")]
        static extern IEE_MentalCommandAction_t Unmanaged_IS_MentalCommandGetCurrentAction(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_MentalCommandGetCurrentActionPower")]
        static extern Single Unmanaged_IS_MentalCommandGetCurrentActionPower(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_MentalCommandIsActive")]
        static extern Boolean Unmanaged_IS_MentalCommandIsActive(IntPtr state);
        
        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_GetWirelessSignalStatus")]
        static extern IEE_SignalStrength_t Unmanaged_IS_GetWirelessSignalStatus(IntPtr state);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_Copy")]
        static extern void Unmanaged_IS_Copy(IntPtr a, IntPtr b);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricEqual")]
        static extern Boolean Unmanaged_IS_PerformanceMetricEqual(IntPtr a, IntPtr b);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_FacialExpressionEqual")]
        static extern Boolean Unmanaged_IS_FacialExpressionEqual(IntPtr a, IntPtr b);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_MentalCommandEqual")]
        static extern Boolean Unmanaged_IS_MentalCommandEqual(IntPtr a, IntPtr b);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_EmoEngineEqual")]
        static extern Boolean Unmanaged_IS_EmoEngineEqual(IntPtr a, IntPtr b);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_Equal")]
        static extern Boolean Unmanaged_IS_Equal(IntPtr a, IntPtr b);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_GetBatteryChargeLevel")]
        static extern void Unmanaged_IS_GetBatteryChargeLevel(IntPtr state, out Int32 chargeLevel, out Int32 maxChargeLevel);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetInstantaneousExcitementModelParams")]
        static extern void Unmanaged_IS_PerformanceMetricGetInstantaneousExcitementModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetRelaxationModelParams")]
        static extern void Unmanaged_IS_PerformanceMetricGetRelaxationModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetEngagementBoredomModelParams")]
        static extern void Unmanaged_IS_PerformanceMetricGetEngagementBoredomModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetStressModelParams")]
        static extern void Unmanaged_IS_PerformanceMetricGetStressModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetInterestModelParams")]
        static extern void Unmanaged_IS_PerformanceMetricGetInterestModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale);

        [DllImport("edk.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IS_PerformanceMetricGetFocusModelParams")]
        static extern void Unmanaged_IS_PerformanceMetricGetFocusModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale);

	
    
        public static Int32 IEE_EngineConnect(String security)
        {            
            return Unmanged_IEE_EngineConnect(security);
        }
        public static Int32 IEE_EngineLocalConnect(String pathEEGFile, String pathMotionFile)
        {
            return Unmanged_IEE_EngineLocalConnect(pathEEGFile, pathMotionFile);
        }
        public static Int32 IEE_EngineRemoteConnect(String szHost, UInt16 port)
        {
            return Unmanged_IEE_EngineRemoteConnect(szHost, port);
        }
        public static Int32 IEE_EngineDisconnect()
        {
            return Unmanged_IEE_EngineDisconnect();
        }
        public static Int32 IEE_EnableDiagnostics(String szFilename, Int32 fEnable, Int32 nReserved)
        {
            return Unmanged_IEE_EnableDiagnostics(szFilename, fEnable, nReserved);
        }
        public static IntPtr IEE_EmoEngineEventCreate()
        {
            return Unmanged_IEE_EmoEngineEventCreate();
        }
        public static IntPtr IEE_ProfileEventCreate()
        {
            return Unmanged_IEE_ProfileEventCreate();
        }
        public static void IEE_EmoEngineEventFree(IntPtr hEvent)
        {
            Unmanged_IEE_EmoEngineEventFree(hEvent);
        }
        public static IntPtr IEE_EmoStateCreate()
        {
            return Unmanged_IEE_EmoStateCreate();
        }
        public static void IEE_EmoStateFree(IntPtr hState)
        {
            Unmanged_IEE_EmoStateFree(hState);
        }
        public static IEE_Event_t IEE_EmoEngineEventGetType(IntPtr hEvent)
        {
            return Unmanged_IEE_EmoEngineEventGetType(hEvent);
        }
        public static IEE_MentalCommandEvent_t IEE_MentalCommandEventGetType(IntPtr hEvent)
        {
            return Unmanged_IEE_MentalCommandEventGetType(hEvent);
        }
        public static IEE_FacialExpressionEvent_t IEE_FacialExpressionEventGetType(IntPtr hEvent)
        {
            return Unmanged_IEE_FacialExpressionEventGetType(hEvent);
        }

        public static Int32 IEE_EmoEngineEventGetUserId(IntPtr hEvent, out UInt32 pUserIdOut)
        {
            return Unmanged_IEE_EmoEngineEventGetUserId(hEvent, out pUserIdOut);
        }

        public static Int32 IEE_EmoEngineEventGetEmoState(IntPtr hEvent, IntPtr hEmoState)
        {
            return Unmanged_IEE_EmoEngineEventGetEmoState(hEvent, hEmoState);
        }

        public static Int32 IEE_EngineGetNextEvent(IntPtr hEvent)
        {
            return Unmanged_IEE_EngineGetNextEvent(hEvent);
        }

        public static Int32 IEE_EngineClearEventQueue(Int32 eventTypes)
        {
            return Unmanged_IEE_EngineClearEventQueue(eventTypes);
        }

        public static Int32 IEE_EngineGetNumUser(out UInt32 pNumUserOut)
        {
            return Unmanged_IEE_EngineGetNumUser(out pNumUserOut);
        }

        public static Int32 IEE_SetHardwarePlayerDisplay(UInt32 userId, UInt32 playerNum)
        {
            return Unmanged_IEE_SetHardwarePlayerDisplay(userId, playerNum);
        }

        public static Int32 IEE_SetUserProfile(UInt32 userId, byte[] profileBuffer, UInt32 length)
        {
            return Unmanged_IEE_SetUserProfile(userId, profileBuffer, length);
        }

        public static Int32 IEE_GetUserProfile(UInt32 userId, IntPtr hEvent)
        {
            return Unmanged_IEE_GetUserProfile(userId, hEvent);
        }

        public static Int32 IEE_GetBaseProfile(IntPtr hEvent)
        {
            return Unmanged_IEE_GetBaseProfile(hEvent);
        }

        public static Int32 IEE_GetUserProfileSize(IntPtr hEvt, out UInt32 pProfileSizeOut)
        {
            return Unmanged_IEE_GetUserProfileSize(hEvt, out pProfileSizeOut);
        }


        public static Int32 IEE_GetUserProfileBytes(IntPtr hEvt, Byte[] destBuffer, UInt32 length)
        {
            return Unmanged_IEE_GetUserProfileBytes(hEvt, destBuffer, length);
        }

        public static Int32 IEE_LoadUserProfile(UInt32 userID, String szInputFilename)
        {
            return Unmanged_IEE_LoadUserProfile(userID, szInputFilename);
        }

        public static Int32 IEE_SaveUserProfile(UInt32 userID, String szOutputFilename)
        {
            return Unmanged_IEE_SaveUserProfile(userID, szOutputFilename);
        }

        public static Int32 IEE_FacialExpressionSetThreshold(UInt32 userId, IEE_FacialExpressionAlgo_t algoName, IEE_FacialExpressionThreshold_t thresholdName, Int32 value)
        {
            return Unmanged_IEE_FacialExpressionSetThreshold(userId, algoName, thresholdName, value);
        }

        public static Int32 IEE_FacialExpressionGetThreshold(UInt32 userId, IEE_FacialExpressionAlgo_t algoName, IEE_FacialExpressionThreshold_t thresholdName, out Int32 pValueOut)
        {
            return Unmanged_IEE_FacialExpressionGetThreshold(userId, algoName, thresholdName, out pValueOut);
        }

        public static Int32 IEE_FacialExpressionSetTrainingAction(UInt32 userId, IEE_FacialExpressionAlgo_t action)
        {
            return Unmanged_IEE_FacialExpressionSetTrainingAction(userId, action);
        }

        public static Int32 IEE_FacialExpressionSetTrainingControl(UInt32 userId, IEE_FacialExpressionTrainingControl_t control)
        {
            return Unmanged_IEE_FacialExpressionSetTrainingControl(userId, control);
        }

        public static Int32 IEE_FacialExpressionGetTrainingAction(UInt32 userId, out IEE_FacialExpressionAlgo_t pActionOut)
        {
            return Unmanged_IEE_FacialExpressionGetTrainingAction(userId, out pActionOut);
        }

        public static Int32 IEE_FacialExpressionGetTrainingTime(UInt32 userId, out UInt32 pTrainingTimeOut)
        {
            return Unmanged_IEE_FacialExpressionGetTrainingTime(userId, out pTrainingTimeOut);
        }

        public static Int32 IEE_FacialExpressionGetTrainedSignatureActions(UInt32 userId, out UInt32 pTrainedActionsOut)
        {
            return Unmanged_IEE_FacialExpressionGetTrainedSignatureActions(userId, out pTrainedActionsOut);
        }


        public static Int32 IEE_FacialExpressionGetTrainedSignatureAvailable(UInt32 userId, out Int32 pfAvailableOut)
        {
            return Unmanged_IEE_FacialExpressionGetTrainedSignatureAvailable(userId, out pfAvailableOut);
        }

        public static Int32 IEE_FacialExpressionSetSignatureType(UInt32 userId, IEE_FacialExpressionSignature_t sigType)
        {
            return Unmanged_IEE_FacialExpressionSetSignatureType(userId, sigType);
        }

        public static Int32 IEE_FacialExpressionGetSignatureType(UInt32 userId, out IEE_FacialExpressionSignature_t pSigTypeOut)
        {
            return Unmanged_IEE_FacialExpressionGetSignatureType(userId, out pSigTypeOut);
        }

        public static Int32 IEE_MentalCommandSetActiveActions(UInt32 userId, UInt32 activeActions)
        {
            return Unmanged_IEE_MentalCommandSetActiveActions(userId, activeActions);
        }

        public static Int32 IEE_MentalCommandGetActiveActions(UInt32 userId, out UInt32 pActiveActionsOut)
        {
            return Unmanged_IEE_MentalCommandGetActiveActions(userId, out pActiveActionsOut);
        }

        public static Int32 IEE_MentalCommandGetTrainingTime(UInt32 userId, out UInt32 pTrainingTimeOut)
        {
            return Unmanged_IEE_MentalCommandGetTrainingTime(userId, out pTrainingTimeOut);
        }

        public static Int32 IEE_MentalCommandSetTrainingControl(UInt32 userId, IEE_MentalCommandTrainingControl_t control)
        {
            return Unmanged_IEE_MentalCommandSetTrainingControl(userId, control);
        }

        public static Int32 IEE_MentalCommandSetTrainingAction(UInt32 userId, IEE_MentalCommandAction_t action)
        {
            return Unmanged_IEE_MentalCommandSetTrainingAction(userId, action);
        }

        public static Int32 IEE_MentalCommandGetTrainingAction(UInt32 userId, out IEE_MentalCommandAction_t pActionOut)
        {
            return Unmanged_IEE_MentalCommandGetTrainingAction(userId, out pActionOut);
        }

        public static Int32 IEE_MentalCommandGetTrainedSignatureActions(UInt32 userId, out UInt32 pTrainedActionsOut)
        {
            return Unmanged_IEE_MentalCommandGetTrainedSignatureActions(userId, out pTrainedActionsOut);
        }

        public static Int32 IEE_MentalCommandGetOverallSkillRating(UInt32 userId, out Single pOverallSkillRatingOut)
        {
            return Unmanged_IEE_MentalCommandGetOverallSkillRating(userId, out pOverallSkillRatingOut);
        }

        public static Int32 IEE_MentalCommandGetActionSkillRating(UInt32 userId, IEE_MentalCommandAction_t action, out Single pActionSkillRatingOut)
        {
            return Unmanged_IEE_MentalCommandGetActionSkillRating(userId, action, out pActionSkillRatingOut);
        }

        public static Int32 IEE_MentalCommandSetActivationLevel(UInt32 userId, Int32 level)
        {
            return Unmanged_IEE_MentalCommandSetActivationLevel(userId, level);
        }

        public static Int32 IEE_MentalCommandSetActionSensitivity(UInt32 userId,
                                            Int32 action1Sensitivity, Int32 action2Sensitivity,
                                            Int32 action3Sensitivity, Int32 action4Sensitivity)
        {
            return Unmanged_IEE_MentalCommandSetActionSensitivity(userId, action1Sensitivity, action2Sensitivity, action3Sensitivity, action4Sensitivity);
        }

        public static Int32 IEE_MentalCommandGetActivationLevel(UInt32 userId, out Int32 pLevelOut)
        {
            return Unmanged_IEE_MentalCommandGetActivationLevel(userId, out pLevelOut);
        }

        public static Int32 IEE_MentalCommandGetActionSensitivity(UInt32 userId,
                                            out Int32 pAction1SensitivityOut, out Int32 pAction2SensitivityOut,
                                            out Int32 pAction3SensitivityOut, out Int32 pAction4SensitivityOut)
        {
            return Unmanged_IEE_MentalCommandGetActionSensitivity(userId, out pAction1SensitivityOut, out pAction2SensitivityOut, 
                out pAction3SensitivityOut, out pAction4SensitivityOut);
        }

        public static Int32 IEE_MentalCommandStartSamplingNeutral(UInt32 userId)
        {
            return Unmanged_IEE_MentalCommandStartSamplingNeutral(userId);
        }

        public static Int32 IEE_MentalCommandStopSamplingNeutral(UInt32 userId)
        {
            return Unmanged_IEE_MentalCommandStopSamplingNeutral(userId);
        }

        public static Int32 IEE_MentalCommandSetSignatureCaching(UInt32 userId, UInt32 enabled)
        {
            return Unmanged_IEE_MentalCommandSetSignatureCaching(userId, enabled);
        }

        public static Int32 IEE_MentalCommandGetSignatureCaching(UInt32 userId, out UInt32 pEnabledOut)
        {
            return Unmanged_IEE_MentalCommandGetSignatureCaching(userId, out pEnabledOut);
        }

        public static Int32 IEE_MentalCommandSetSignatureCacheSize(UInt32 userId, UInt32 size)
        {
            return Unmanged_IEE_MentalCommandSetSignatureCacheSize(userId, size);
        }

        public static Int32 IEE_MentalCommandGetSignatureCacheSize(UInt32 userId, out UInt32 pSizeOut)
        {
            return Unmanged_IEE_MentalCommandGetSignatureCacheSize(userId, out pSizeOut);
        }

        public static Int32 IEE_HeadsetGetSensorDetails(IEE_InputChannels_t channelId, out IInputSensorDescriptor_t pDescriptorOut)
        {
            return Unmanged_IEE_HeadsetGetSensorDetails(channelId, out pDescriptorOut);
        }

        public static Int32 IEE_HardwareGetVersion(UInt32 userId, out UInt32 pHwVersionOut)
        {
            return Unmanged_IEE_HardwareGetVersion(userId, out pHwVersionOut);
        }

        public static Int32 IEE_SoftwareGetVersion(StringBuilder pszVersionOut, UInt32 nVersionChars, out UInt32 pBuildNumOut)
        {
            return Unmanged_IEE_SoftwareGetVersion(pszVersionOut, nVersionChars, out pBuildNumOut);
        }

        public static Int32 IEE_HeadsetGetGyroDelta(UInt32 userId, out Int32 pXOut, out Int32 pYOut)
        {
            return Unmanged_IEE_HeadsetGetGyroDelta(userId, out pXOut, out pYOut);
        }

        public static Int32 IEE_HeadsetGyroRezero(UInt32 userId)
        {
            return Unmanged_IEE_HeadsetGyroRezero(userId);
        }

        public static IntPtr IEE_OptimizationParamCreate()
        {
            return Unmanged_IEE_OptimizationParamCreate();
        }

        public static void IEE_OptimizationParamFree(IntPtr hParam)
        {
            Unmanged_IEE_OptimizationParamFree(hParam);
        }

        public static Int32 IEE_OptimizationEnable(IntPtr hParam)
        {
            return Unmanged_IEE_OptimizationEnable(hParam);
        }

        public static Int32 IEE_OptimizationIsEnabled(out Boolean pEnabledOut)
        {
            return Unmanged_IEE_OptimizationIsEnabled(out pEnabledOut);
        }

        public static Int32 IEE_OptimizationDisable()
        {
            return Unmanged_IEE_OptimizationDisable();
        }

        public static Int32 IEE_OptimizationGetParam(IntPtr hParam)
        {
            return Unmanged_IEE_OptimizationGetParam(hParam);
        }

        public static Int32 IEE_OptimizationGetVitalAlgorithm(IntPtr hParam, IEE_EmotivSuite_t suite, out UInt32 pVitalAlgorithmBitVectorOut)
        {
            return Unmanged_IEE_OptimizationGetVitalAlgorithm(hParam, suite, out pVitalAlgorithmBitVectorOut);
        }

        
        public static Int32 IEE_OptimizationSetVitalAlgorithm(IntPtr hParam, IEE_EmotivSuite_t suite, UInt32 vitalAlgorithmBitVector)
        {
            return Unmanged_IEE_OptimizationSetVitalAlgorithm(hParam, suite, vitalAlgorithmBitVector);
        }

        
        public static Int32 IEE_ResetDetection(UInt32 userId, IEE_EmotivSuite_t suite, UInt32 detectionBitVector)
        {
            return Unmanged_IEE_ResetDetection(userId, suite, detectionBitVector);
        }

        public static IntPtr IEE_MotionDataCreate()
        {
            return Unmanaged_IEE_MotionDataCreate();
        }

        public static void IEE_MotionDataFree(IntPtr hData)
        {
            Unmanaged_IEE_MotionDataFree(hData);
        }

        public static Int32 IEE_MotionDataUpdateHandle(UInt32 userId, IntPtr hData)
        {
            return Unmanaged_IEE_MotionDataUpdateHandle(userId, hData);
        }

        public static Int32 IEE_MotionDataGet(IntPtr hData, IEE_MotionDataChannel_t channel, Double[] buffer, UInt32 bufferSizeInSample)
        {
            return Unmanaged_IEE_MotionDataGet(hData, channel, buffer, bufferSizeInSample);
        }

        public static Int32 IEE_MotionDataGetMultiChannels(IntPtr hData, IEE_MotionDataChannel_t[] channelList, UInt32 nChannel, Double[][] buffer, UInt32 bufferSizeInSample)
        {
            return Unmanaged_IEE_MotionDataGetMultiChannels(hData, channelList, nChannel, buffer, bufferSizeInSample);
        }

        public static Int32 IEE_MotionDataGetNumberOfSample(IntPtr hData, out UInt32 nSampleOut)
        {
            return Unmanaged_IEE_MotionDataGetNumberOfSample(hData, out nSampleOut);
        }

        public static Int32 IEE_MotionDataSetBufferSizeInSec(Single bufferSizeInSec)
        {
            return Unmanaged_IEE_MotionDataSetBufferSizeInSec(bufferSizeInSec);
        }

        public static Int32 IEE_MotionDataGetBufferSizeInSec(out Single pBufferSizeInSecOut)
        {
            return Unmanaged_IEE_MotionDataGetBufferSizeInSec(out pBufferSizeInSecOut);
        }

        public static Int32 IEE_MotionDataAcquisitionEnable(UInt32 userId, Boolean enable)
        {
            return Unmanaged_IEE_MotionDataAcquisitionEnable(userId, enable);
        }

        public static Int32 IEE_MotionDataAcquisitionIsEnabled(UInt32 userId, out Boolean pEnableOut)
        {
            return Unmanaged_IEE_MotionDataAcquisitionIsEnabled(userId, out pEnableOut);
        }

        public static Int32 IEE_MotionDataGetSamplingRate(UInt32 userId, out UInt32 pSamplingRateOut)
        {
            return Unmanaged_IEE_MotionDataGetSamplingRate(userId, out pSamplingRateOut);
        }

        public static IntPtr IEE_DataCreate()
        {
            return Unmanaged_IEE_DataCreate();
        }
        
        public static void IEE_DataFree(IntPtr hData)
        {
            Unmanaged_IEE_DataFree(hData);
        }
        
        public static Int32 IEE_DataUpdateHandle(UInt32 userId, IntPtr hData)
        {
            return Unmanaged_IEE_DataUpdateHandle(userId, hData);
        }
        
        public static Int32 IEE_DataGet(IntPtr hData, IEE_DataChannel_t channel, Double[] buffer, UInt32 bufferSizeInSample)
        {
            return Unmanaged_IEE_DataGet(hData, channel, buffer, bufferSizeInSample);
        }

        public static Int32 IEE_DataGetMultiChannel(IntPtr hData, IEE_DataChannel_t[] channelList, UInt32 nChannel, Double[][] buffer, UInt32 bufferSizeInSample)
        {
            return Unmanaged_IEE_DataGetMultiChannels(hData, channelList, nChannel, buffer, bufferSizeInSample);
        }
        
        public static Int32 IEE_DataGetNumberOfSample(IntPtr hData, out UInt32 nSampleOut)
        {
            return Unmanaged_IEE_DataGetNumberOfSample(hData, out nSampleOut);
        }

        public static Int32 IEE_DataSetBufferSizeInSec(Single bufferSizeInSec)
        {
            return Unmanaged_IEE_DataSetBufferSizeInSec(bufferSizeInSec);
        }
        
        public static Int32 IEE_DataGetBufferSizeInSec(out Single pBufferSizeInSecOut)
        {
            return Unmanaged_IEE_DataGetBufferSizeInSec(out pBufferSizeInSecOut);
        }
        
        public static Int32 IEE_DataAcquisitionEnable(UInt32 userId, Boolean enable)
        {
            return Unmanaged_IEE_DataAcquisitionEnable(userId, enable);
        }

        public static Int32 IEE_DataAcquisitionIsEnabled(UInt32 userId, out Boolean pEnableOut)
        {
            return Unmanaged_IEE_DataAcquisitionIsEnabled(userId, out pEnableOut);
        }

        public static Int32 IEE_DataSetSychronizationSignal(UInt32 userId, Int32 signal)
        {
            return Unmanaged_IEE_DataSetSychronizationSignal(userId, signal);
        }

        public static Int32 IEE_DataSetMarker(UInt32 userId, Int32 marker)
        {
            return Unmanaged_IEE_DataSetMarker(userId, marker);
        }

        public static Int32 IEE_DataGetSamplingRate(UInt32 userId, out UInt32 pSamplingRateOut)
        {
            return Unmanaged_IEE_DataGetSamplingRate(userId, out pSamplingRateOut);
        }

        public static Int32 IEE_EdfStart()
        {
            return Unmanaged_IEE_EdfStart();
        }

        public static Int32 IEE_EdfStop()
        {
            return Unmanaged_IEE_EdfStop();
        }

        public static Int32 IEE_EdfSeek(UInt32 sec)
        {
            return Unmanaged_IEE_EdfSeek(sec);
        }

        public static Int32 IEE_EdfGetTotalTime(out UInt32 totalTime)
        {
            return Unmanaged_IEE_EdfGetTotalTime(out totalTime);
        }

        public static void IEE_EdfStartSaving(UInt32 userID, String filename, String patientID, String recordID, String date, String time)
        {
            Unmanaged_IEE_EdfStartSaving(userID, filename, patientID, recordID, date, time);
        }

        public static void IEE_EdfStopSaving(UInt32 userID)
        {
            Unmanaged_IEE_EdfStopSaving(userID);
        }

        public static void IEE_EdfStopSavingAll()
        {
            Unmanaged_IEE_EdfStopSavingAll();
        }

        public static IntPtr IS_Create()
        {
            return Unmanaged_IS_Create();
        }
        public static void IS_Free(IntPtr state)
        {
            Unmanaged_IS_Free(state);
        }
        public static void IS_Init(IntPtr state)
        {
            Unmanaged_IS_Init(state);
        }
        public static Single IS_GetTimeFromStart(IntPtr state)
        {
            return Unmanaged_IS_GetTimeFromStart(state);
        }
        public static Int32 IS_GetHeadsetOn(IntPtr state)
        {
            return Unmanaged_IS_GetHeadsetOn(state);
        }
        public static Int32 IS_GetNumContactQualityChannels(IntPtr state)
        {
            return Unmanaged_IS_GetNumContactQualityChannels(state);
        }
        public static IEE_EEG_ContactQuality_t IS_GetContactQuality(IntPtr state, Int32 electroIdx)
        {
            return Unmanaged_IS_GetContactQuality(state, electroIdx);
        }
        public static Int32 IS_GetContactQualityFromAllChannels(IntPtr state, out IEE_EEG_ContactQuality_t[] contactQuality)
        {
            Int32 numChannels = EdkDll.IS_GetNumContactQualityChannels(state);
            contactQuality = new IEE_EEG_ContactQuality_t[numChannels];
            return Unmanaged_IS_GetContactQualityFromAllChannels(state, contactQuality, (UInt32)contactQuality.Length);
        }
        public static Boolean IS_FacialExpressionIsBlink(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionIsBlink(state);
        }

        public static Boolean IS_FacialExpressionIsLeftWink(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionIsLeftWink(state);
        }
        public static Boolean IS_FacialExpressionIsRightWink(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionIsRightWink(state);
        }
        public static Boolean IS_FacialExpressionIsEyesOpen(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionIsEyesOpen(state);
        }
        public static Boolean IS_FacialExpressionIsLookingUp(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionIsLookingUp(state);
        }
        public static Boolean IS_FacialExpressionIsLookingDown(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionIsLookingDown(state);
        }

        public static void IS_FacialExpressionGetEyelidState(IntPtr state, out Single leftEye, out Single rightEye)
        {
            Unmanaged_IS_FacialExpressionGetEyelidState(state, out leftEye, out rightEye);
        }

        public static void IS_FacialExpressionGetEyeLocation(IntPtr state, out Single x, out Single y)
        {
            Unmanaged_IS_FacialExpressionGetEyeLocation(state, out x, out y);
        }

        public static Single IS_FacialExpressionGetEyebrowExtent(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionGetEyebrowExtent(state);
        }

        public static Single IS_FacialExpressionGetSmileExtent(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionGetSmileExtent(state);
        }

        public static Single IS_FacialExpressionGetClenchExtent(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionGetClenchExtent(state);
        }

        public static IEE_FacialExpressionAlgo_t IS_FacialExpressionGetUpperFaceAction(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionGetUpperFaceAction(state);
        }

        public static Single IS_FacialExpressionGetUpperFaceActionPower(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionGetUpperFaceActionPower(state);
        }

        public static IEE_FacialExpressionAlgo_t IS_FacialExpressionGetLowerFaceAction(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionGetLowerFaceAction(state);
        }

        public static Single IS_FacialExpressionGetLowerFaceActionPower(IntPtr state)
        {
            return Unmanaged_IS_FacialExpressionGetLowerFaceActionPower(state);
        }
        public static Boolean IS_FacialExpressionIsActive(IntPtr state, IEE_FacialExpressionAlgo_t type)
        {
            return Unmanaged_IS_FacialExpressionIsActive(state, type);
        }

        public static Single IS_PerformanceMetricGetExcitementLongTermScore(IntPtr state)
        {
            return Unmanaged_IS_PerformanceMetricGetExcitementLongTermScore(state);
        }

        public static Single IS_PerformanceMetricGetInstantaneousExcitementScore(IntPtr state)
        {
            return Unmanaged_IS_PerformanceMetricGetInstantaneousExcitementScore(state);
        }

        public static Boolean IS_PerformanceMetricIsActive(IntPtr state, IEE_PerformanceMetricAlgo_t type)
        {
            return Unmanaged_IS_PerformanceMetricIsActive(state, type);
        }

        public static Single IS_PerformanceMetricGetRelaxationScore(IntPtr state)
        {
            return Unmanaged_IS_PerformanceMetricGetRelaxationScore(state);
        }

        public static Single IS_PerformanceMetricGetStressScore(IntPtr state)
        {
            return Unmanaged_IS_PerformanceMetricGetStressScore(state);
        }

        public static Single IS_PerformanceMetricGetEngagementBoredomScore(IntPtr state)
        {
            return Unmanaged_IS_PerformanceMetricGetEngagementBoredomScore(state);
        }

        public static Single IS_PerformanceMetricGetInterestScore(IntPtr state)
        {
            return Unmanaged_IS_PerformanceMetricGetInterestScore(state);
        }

        public static Single IS_PerformanceMetricGetFocusScore(IntPtr state)
        {
            return Unmanaged_IS_PerformanceMetricGetFocusScore(state);
        }

        public static IEE_MentalCommandAction_t IS_MentalCommandGetCurrentAction(IntPtr state)
        {
            return Unmanaged_IS_MentalCommandGetCurrentAction(state);
        }

        public static Single IS_MentalCommandGetCurrentActionPower(IntPtr state)
        {
            return Unmanaged_IS_MentalCommandGetCurrentActionPower(state);
        }

        public static Boolean IS_MentalCommandIsActive(IntPtr state)
        {
            return Unmanaged_IS_MentalCommandIsActive(state);
        }

        public static IEE_SignalStrength_t IS_GetWirelessSignalStatus(IntPtr state)
        {
            return Unmanaged_IS_GetWirelessSignalStatus(state);
        }

        public static void IS_Copy(IntPtr a, IntPtr b)
        {
            Unmanaged_IS_Copy(a, b);
        }

        public static Boolean IS_PerformanceMetricEqual(IntPtr a, IntPtr b)
        {
            return Unmanaged_IS_PerformanceMetricEqual(a, b);
        }

        public static Boolean IS_FacialExpressionEqual(IntPtr a, IntPtr b)
        {
            return Unmanaged_IS_FacialExpressionEqual(a, b);
        }

        public static Boolean IS_MentalCommandEqual(IntPtr a, IntPtr b)
        {
            return Unmanaged_IS_MentalCommandEqual(a, b);
        }

        public static Boolean IS_EmoEngineEqual(IntPtr a, IntPtr b)
        {
            return Unmanaged_IS_EmoEngineEqual(a, b);
        }

        public static Boolean IS_Equal(IntPtr a, IntPtr b)
        {
            return Unmanaged_IS_Equal(a, b);
        }

        public static void IS_GetBatteryChargeLevel(IntPtr state, out Int32 chargeLevel, out Int32 maxChargeLevel)
        {
            Unmanaged_IS_GetBatteryChargeLevel(state, out chargeLevel, out maxChargeLevel);
        }

        public static void IS_PerformanceMetricGetInstantaneousExcitementModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale)
        {
            Unmanaged_IS_PerformanceMetricGetInstantaneousExcitementModelParams(state,out rawScore,out minScale,out maxScale);
        }

        public static void IS_PerformanceMetricGetRelaxationModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale)
        {
             Unmanaged_IS_PerformanceMetricGetRelaxationModelParams(state,out rawScore,out minScale,out maxScale);
        }

        public static void IS_PerformanceMetricGetEngagementBoredomModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale)
        {
           Unmanaged_IS_PerformanceMetricGetEngagementBoredomModelParams(state, out rawScore, out minScale, out maxScale);
        }

        public static void IS_PerformanceMetricGetStressModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale)
        {
            Unmanaged_IS_PerformanceMetricGetStressModelParams(state,out rawScore,out minScale,out maxScale);
        }

        public static void IS_PerformanceMetricGetInterestModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale)
        {
            Unmanaged_IS_PerformanceMetricGetInterestModelParams(state, out rawScore, out minScale, out maxScale);
        }

        public static void IS_PerformanceMetricGetFocusModelParams(IntPtr state, out Double rawScore, out Double minScale, out Double maxScale)
        {
            Unmanaged_IS_PerformanceMetricGetFocusModelParams(state, out rawScore, out minScale, out maxScale);
        }
    }
}
