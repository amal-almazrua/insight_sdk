/**
 * Emotiv Insight SDK
 * Copyright (c) 2015 Emotiv Inc.
 *
 * This file is part of the Emotiv Insight SDK.
 *
 * The main interface that allows interactions between external programs and the Emotiv detection engine.
 *
 * None of these API functions are thread-safe.
 *
 * This header file is designed to be included under C and C++ environment.
 *
 */

#ifndef IEDK_H
#define IEDK_H

#include "IedkErrorCode.h"
#include "IEmoStateDLL.h"

#ifndef EDK_STATIC_LIB
    #ifdef EDK_EXPORTS
        #ifdef _WIN32
            #define EDK_API __declspec(dllexport)
        #else
            #define EDK_API
        #endif
    #else
        #ifdef _WIN32
            #define EDK_API __declspec(dllimport)
        #else
            #define EDK_API
        #endif
    #endif
#else
    #define EDK_API extern
#endif

#ifdef __cplusplus
extern "C"
{
#endif
    //! FacialExpression Suite threshold type enumerator
    typedef enum IEE_FacialExpressionThreshold_enum {
        FE_SENSITIVITY  //!< Sensitivity of each facial expression
    } IEE_FacialExpressionThreshold_t;

    //! FacialExpression Suite training control enumerator
    typedef enum IEE_FacialExpressionTrainingControl_enum {
        FE_NONE = 0,    //!< No action
        FE_START,       //!< Start a new training
        FE_ACCEPT,      //!< Accept current training
        FE_REJECT,      //!< Reject current training
        FE_ERASE,       //!< Erase training data for a facial expression
        FE_RESET        //!< Reset current training
    } IEE_FacialExpressionTrainingControl_t;

    //! FacialExpression Suite signature type enumerator
    typedef enum IEE_FacialExpressionSignature_enum {
        FE_SIG_UNIVERSAL = 0,   //!< Use built-in universal signature
        FE_SIG_TRAINED          //!< Use custom trained signature
    } IEE_FacialExpressionSignature_t;

    //! MentalCommand Suite training control enumerator
    typedef enum IEE_MentalCommandTrainingControl_enum {
        MC_NONE = 0,    //!< No action
        MC_START,       //!< Start a new training
        MC_ACCEPT,      //!< Accept current training
        MC_REJECT,      //!< Reject current training
        MC_ERASE,       //!< Erase training data for an action
        MC_RESET        //!< Reset current training
    } IEE_MentalCommandTrainingControl_t;


    //! Handle to EmoState structure allocated by IEE_EmoStateCreate.
    /*!
        \sa IEE_EmoStateCreate()
     */
    typedef void* EmoStateHandle;

    //! Handle to EmoEngine event structure allocated by IEE_EmoEngineEventCreate.
    /*!
        \sa IEE_EmoEngineEventCreate()
     */
    typedef void* EmoEngineEventHandle;

    //! Handle to optimization param allocated by IEE_OptimizationParamCreate.
    /*!
        \sa IEE_OptimizationParamCreate()
     */
    typedef void* OptimizationParamHandle;

    //! Handle to data placeholder allocated by IEE_MotionDataCreate.
    /*!
        \sa IEE_MotionDataCreate()
     */
    typedef void* DataHandle;

    //! EmoEngine event types
    typedef enum IEE_Event_enum {
        IEE_UnknownEvent          = 0x0000,     //!< An unknown event.
        IEE_EmulatorError         = 0x0001,     //!< Error event from emulator. Connection to EmoComposer could be lost.
        IEE_ReservedEvent         = 0x0002,     //!< Reserved event.
        IEE_UserAdded             = 0x0010,     //!< A headset is connected.
        IEE_UserRemoved           = 0x0020,     //!< A headset has been disconnected.
        IEE_EmoStateUpdated       = 0x0040,     //!< Detection results have been updated from EmoEngine.
        IEE_ProfileEvent          = 0x0080,     //!< A profile has been returned from EmoEngine.
        IEE_MentalCommandEvent    = 0x0100,     //!< A IEE_MentalCommandEvent_t has been returned from EmoEngine.
        IEE_FacialExpressionEvent = 0x0200,     //!< A IEE_FacialExpressionEvent_t has been returned from EmoEngine.
        IEE_InternalStateChanged  = 0x0400,     //!< Reserved for internal use.
        IEE_AllEvent              = IEE_UserAdded | IEE_UserRemoved | IEE_EmoStateUpdated |
                                    IEE_ProfileEvent | IEE_MentalCommandEvent |
                                    IEE_FacialExpressionEvent | IEE_InternalStateChanged
                                    //!< Bit-mask for all events except error types
    } IEE_Event_t;

    //! FacialExpression event types
    typedef enum IEE_FacialExpressionEvent_enum {
        IEE_FacialExpressionNoEvent = 0,        //!< No new event
        IEE_FacialExpressionTrainingStarted,    //!< The training has begun after FE_START is sent.
        IEE_FacialExpressionTrainingSucceeded,  //!< The training is succeeded, waiting for FE_ACCEPT or FE_REJECT.
        IEE_FacialExpressionTrainingFailed,     //!< The training is failed due to signal issues. Please restart training.
        IEE_FacialExpressionTrainingCompleted,  //!< The training is completed after FE_ACCEPT is sent.
        IEE_FacialExpressionTrainingDataErased, //!< The training data for a particular facial expression has been erased by FE_ERASE.
        IEE_FacialExpressionTrainingRejected,   //!< The training is rejected after FE_REJECT is sent.
        IEE_FacialExpressionTrainingReset       //!< The training has been reset after FE_RESET is sent.
    } IEE_FacialExpressionEvent_t;
    
    //! MentalCommand event types
    typedef enum IEE_MentalCommandEvent_enum {
        IEE_MentalCommandNoEvent = 0,                   //!< No new event
        IEE_MentalCommandTrainingStarted,               //!< The training has begun after MC_START is sent.
        IEE_MentalCommandTrainingSucceeded,             //!< The training is succeeded, waiting for MC_ACCEPT or MC_REJECT.
        IEE_MentalCommandTrainingFailed,                //!< The training is failed due to signal issues. Please restart training.
        IEE_MentalCommandTrainingCompleted,             //!< The training is completed after MC_ACCEPT is sent.
        IEE_MentalCommandTrainingDataErased,            //!< The training data for a particular action has been erased by MC_ERASE.
        IEE_MentalCommandTrainingRejected,              //!< The training is rejected after MC_REJECT is sent.
        IEE_MentalCommandTrainingReset,                 //!< The training has been reset after MC_RESET is sent.
        IEE_MentalCommandAutoSamplingNeutralCompleted,  //!< The neutral training is completed.
        IEE_MentalCommandSignatureUpdated               //!< The mental command signature has been updated for new actions.
    } IEE_MentalCommandEvent_t;
    
    //! Input sensor description
    typedef struct IInputSensorDescriptor_struct {
        IEE_InputChannels_t channelId;  //!< Logical channel id
        int                 fExists;    //!< Non-zero if this sensor exists on this headset model
        const char*         pszLabel;   //!< Text label identifying this sensor
        double              xLoc;       //!< X coordinate from center of head towards nose
        double              yLoc;       //!< Y coordinate from center of head towards ears
        double              zLoc;       //!< Z coordinate from center of head toward top of skull
    } IInputSensorDescriptor_t;

    //! Motion data channel description
    typedef enum IEE_MotionDataChannel_enum {
        IED_COUNTER_MEMS = 0,   //!< Sample counter
        IED_GYROSCOPEX,         //!< Gyroscope X-axis
        IED_GYROSCOPEY,         //!< Gyroscope Y-axis
        IED_GYROSCOPEZ,         //!< Gyroscope Z-axis
        IED_ACCX,               //!< Accelerometer X-axis
        IED_ACCY,               //!< Accelerometer Y-axis
        IED_ACCZ,               //!< Accelerometer Z-axis
        IED_MAGX,               //!< Magnetometer X-axis
        IED_MAGY,               //!< Magnetometer Y-axis
        IED_MAGZ,               //!< Magnetometer Z-axis
        IED_MOTIONTIMESTAMP     //!< Timestamp of the motion data stream
    } IEE_MotionDataChannel_t;
    

    //! Initialize EmoEngine instance which reads data from the headset.
    /*!
        This function should be called at the beginning of programs that make use of EmoEngine, most probably in initialization routine or constructor.
     
        \return EDK_ERROR_CODE
                - EDK_OK if a connection is established

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_EngineConnect(const char* strDevID = "Emotiv Systems-5");

    
    //! Initialize the connection to a remote instance of EmoEngine.
    /*!
        Blocking call

        \param szHost - A null-terminated string identifying the hostname or IP address of the remote EmoEngine server
        \param port - The port number of the remote EmoEngine server
                    - If connecting to the Emotiv Control Panel, use port 3008
                    - If connecting to the EmoComposer, use port 1726
    
        \return EDK_ERROR_CODE
                - EDK_OK if a connection is established

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_EngineRemoteConnect(const char* szHost,
                                unsigned short port);
    

    //! Terminate the connection to EmoEngine.
    /*!
        This function should be called at the end of programs which make use of EmoEngine, most probably in clean up routine or destructor.
     
        \return EDK_ERROR_CODE
                - EDK_OK if disconnection is achieved

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_EngineDisconnect();


    //! Enable diagnostics mode.
    /*!
        Controls the output of logging information from EmoEngine (which is off by default).
        This should only be enabled if instructed to do so by Emotiv developer support for the purposes of collecting diagnostic information.
     
        \param szFilename - The path of the logfile
        \param fEnable - If non-zero, then diagnostic information will be written to logfile.
                       - If zero, then all diagnostic information is suppressed (default).
        \param nReserved - Reserved for future use.

        \return EDK_ERROR_CODE
                - EDK_OK if the command succeeded
    */
    EDK_API int
        IEE_EnableDiagnostics(const char* szFilename,
                              int fEnable,
                              int nReserved);

    
    //! Return a handle to memory that can hold an EmoEngine event.
    /*!
        This handle can be reused by the caller to retrieve subsequent events.
     
        \return EmoEngineEventHandle
    */
    EDK_API EmoEngineEventHandle
        IEE_EmoEngineEventCreate();


    //! Return a handle to memory that can hold a profile byte stream.
    /*!
        This handle can be reused by the caller to retrieve subsequent profile bytes.
     
        \return EmoEngineEventHandle
    */
    EDK_API EmoEngineEventHandle
        IEE_ProfileEventCreate();

    
    //! Free memory referenced by an event handle.
    /*!
        \param hEvent - a handle returned by IEE_EmoEngineEventCreate() or IEE_ProfileEventCreate()
    */
    EDK_API void
        IEE_EmoEngineEventFree(EmoEngineEventHandle hEvent);

    
    //! Return a handle to memory that can store an EmoState.
    /*!
        This handle can be reused by the caller to retrieve subsequent EmoStates.
     
        \return EmoStateHandle
    */
    EDK_API EmoStateHandle
        IEE_EmoStateCreate();

    
    //! Free memory referenced by an EmoState handle.
    /*!
        \param hState - a handle returned by IEE_EmoStateCreate()
    */
    EDK_API void
        IEE_EmoStateFree(EmoStateHandle hState);


    //! Return the event type for an event already retrieved using IEE_EngineGetNextEvent().
    /*!
        \param hEvent - a handle returned by IEE_EmoEngineEventCreate()
    
        \return IEE_Event_t
    */
    EDK_API IEE_Event_t
        IEE_EmoEngineEventGetType(EmoEngineEventHandle hEvent);

    
    //! Return the MentalCommand-specific event type.
    /*!
        Returns the MentalCommand-specific event type for an IEE_MentalCommandEvent event already retrieved using IEE_EngineGetNextEvent().
     
        \param hEvent - a handle returned by IEE_EmoEngineEventCreate()
    
        \return IEE_MentalCommandEvent_t
    */
    EDK_API IEE_MentalCommandEvent_t
        IEE_MentalCommandEventGetType(EmoEngineEventHandle hEvent);


    //! Return the FacialExpression-specific event type.
    /*!
        Returns the FacialExpression-specific event type for an IEE_FacialExpressionEvent event already retrieved using IEE_EngineGetNextEvent().
     
        \param hEvent - a handle returned by IEE_EmoEngineEventCreate()
    
        \return IEE_FacialExpressionEvent_t
    */
    EDK_API IEE_FacialExpressionEvent_t
        IEE_FacialExpressionEventGetType(EmoEngineEventHandle hEvent);
    

    //! Retrieve the user ID for IEE_UserAdded and IEE_UserRemoved events.
    /*!
        \param hEvent - a handle returned by IEE_EmoEngineEventCreate()
        \param pUserIdOut - receives the user ID associated with the current event
    
        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_EmoEngineEventGetUserId(EmoEngineEventHandle hEvent,
                                    unsigned int *pUserIdOut);

    
    //! Copy an EmoState returned with a IEE_EmoStateUpdate event to memory referenced by an EmoStateHandle.
    /*!
        \param hEvent - a handle returned by IEE_EmoEngineEventCreate() and populated with IEE_EmoEngineGetNextEvent()
        \param hEmoState - a handle returned by IEE_EmoStateCreate()
    
        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_EmoEngineEventGetEmoState(EmoEngineEventHandle hEvent,
                                      EmoStateHandle hEmoState);
    

    //! Retrieve the next EmoEngine event
    /*!
        Non-blocking call

        \param hEvent - a handle returned by IEE_EmoEngineEventCreate()

        \return EDK_ERROR_CODE
                - EDK_OK if an new event has been retrieved
                - EDK_NO_EVENT if no new events have been generated by EmoEngine
        
        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_EngineGetNextEvent(EmoEngineEventHandle hEvent);

    
    //! Clear a specific EmoEngine event type or all events currently inside the event queue.
    /*!
        Event flags can be combined together as one argument except for IEE_UnknownEvent and IEE_EmulatorError.
     
        \param eventTypes - EmoEngine event type (IEE_Event_t), multiple events can be combined such as (IEE_UserAdded | IEE_UserRemoved)

        \return EDK_ERROR_CODE
                - EDK_OK if the events have been cleared from the queue
                - EDK_INVALID_PARAMETER if input event types are invalid
        
        \sa IEE_Event_t, IedkErrorCode.h
    */
    EDK_API int
        IEE_EngineClearEventQueue(int eventTypes);

    
    //! Retrieve number of active users (headset) connected to the EmoEngine.
    /*!
        \param pNumUserOut - receives number of users

        \return EDK_ERROR_CODE
                - EDK_OK if successful.

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_EngineGetNumUser(unsigned int* pNumUserOut);


    //! Set the player number display.
    /*!
        Sets the player number displayed on the physical input device (currently the USB Dongle) that corresponds to the specified user.
     
        \param userId - EmoEngine user ID
        \param playerNum - application assigned player number displayed on input device hardware (must be in the range 1-4)
        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_SetHardwarePlayerDisplay(unsigned int userId,
                                     unsigned int playerNum);


    //! Load an EmoEngine profile for the specified user.
    /*!
        \param userId - user ID
        \param profileBuffer - pointer to buffer containing a serialized user profile previously returned from EmoEngine.
        \param length - buffer size (number of bytes)

        \return EDK_ERROR_CODE
                - EDK_OK if the function succeeds in loading this profile

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_SetUserProfile(unsigned int userId,
                           const unsigned char profileBuffer[],
                           unsigned int length);


    //! Return user profile data in a synchronous manner.
    /*!
        Fills in the event referred to by hEvent with an IEE_ProfileEvent event
        that contains the profile data for the specified user.

        \param userId - user ID
        \param hEvent - a handle returned by IEE_EmoEngineEventCreate()

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_GetUserProfile(unsigned int userId,
                           EmoEngineEventHandle hEvent);


    //! Return a serialized user profile for a default user in a synchronous manner.
    /*!
        Fills in the event referred to by hEvent with an IEE_ProfileEvent event
        that contains the profile data for the default user

        \param hEvent - a handle returned by IEE_EmoEngineEventCreate()

        \return EDK_ERROR_CODE
                 - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_GetBaseProfile(EmoEngineEventHandle hEvent);
    

    //! Return the number of bytes required to store a serialized version of the requested user profile.
    /*! 
        \param hEvt - an EmoEngineEventHandle of type IEE_ProfileEvent
        \param pProfileSizeOut - receives number of bytes required by the profile

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_GetUserProfileSize(EmoEngineEventHandle hEvt,
                               unsigned int* pProfileSizeOut);

    
    //! Copy a serialized version of the requested user profile into the caller's buffer.
    /*!     
        \param hEvt - an EmoEngineEventHandle returned in a IEE_ProfileEvent event
        \param destBuffer - pointer to a destination buffer
        \param length - the size of the destination buffer in bytes

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_GetUserProfileBytes(EmoEngineEventHandle hEvt,
                                unsigned char destBuffer[],
                                unsigned int length);
    

    //! Load a user profile from disk and assigns it to the specified user
    /*!     
        \param userID - a valid user ID
        \param szInputFilename - platform-dependent filesystem path of saved user profile

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_LoadUserProfile(unsigned int userID,
                            const char* szInputFilename);
    

    //!  Save a user profile for specified user to disk
    /*!     
        \param userID - a valid user ID
        \param szOutputFilename - platform-dependent filesystem path for output file

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_SaveUserProfile(unsigned int userID,
                            const char* szOutputFilename);
    

    //! Set threshold for FacialExpression algorithms
    /*!
        \param userId - user ID
        \param algoName - FacialExpression algorithm type
        \param thresholdName - FacialExpression threshold type
        \param value - threshold value (min: 0, max: 1000)

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_FacialExpressionAlgo_t, IEE_FacialExpressionThreshold_t
    */
    EDK_API int
        IEE_FacialExpressionSetThreshold(unsigned int userId,
                                         IEE_FacialExpressionAlgo_t algoName,
                                         IEE_FacialExpressionThreshold_t thresholdName,
                                         int value);


    //! Get threshold from FacialExpression algorithms
    /*!
        \param userId - user ID
        \param algoName - FacialExpression algorithm type
        \param thresholdName - FacialExpression threshold type
        \param pValueOut - receives threshold value

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_FacialExpressionAlgo_t, IEE_FacialExpressionThreshold_t
    */
    EDK_API int
        IEE_FacialExpressionGetThreshold(unsigned int userId,
                                         IEE_FacialExpressionAlgo_t algoName,
                                         IEE_FacialExpressionThreshold_t thresholdName,
                                         int *pValueOut);


    //! Set the current facial expression for FacialExpression training
    /*!
        Blocking call

        \param userId - user ID
        \param action - which facial expression would like to be trained

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_FacialExpressionAlgo_t
    */
    EDK_API int 
        IEE_FacialExpressionSetTrainingAction(unsigned int userId,
                                              IEE_FacialExpressionAlgo_t action);


    //! Set the control flag for FacialExpression training
    /*!
        Blocking call

        \param userId - user ID
        \param control - pre-defined control command

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_FacialExpressionTrainingControl_t
    */
    EDK_API int 
        IEE_FacialExpressionSetTrainingControl(unsigned int userId,
                                               IEE_FacialExpressionTrainingControl_t control);


    //! Get the facial expression currently selected for FacialExpression training
    /*!
        Blocking call

        \param userId - user ID
        \param pActionOut - receives facial expression currently selected for training

        \return EDK_ERROR_CODE
                - EDK_OK if successful
     
        \sa IedkErrorCode.h, IEE_FacialExpressionAlgo_t
    */
    EDK_API int 
        IEE_FacialExpressionGetTrainingAction(unsigned int userId,
                                              IEE_FacialExpressionAlgo_t* pActionOut);

    
    //! Return the duration of a FacialExpression training session
    /*!
        \param userId - user ID
        \param pTrainingTimeOut - receive the training time in ms

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int 
        IEE_FacialExpressionGetTrainingTime(unsigned int userId,
                                            unsigned int* pTrainingTimeOut);


    //! Get a list of expressions that have been trained by the user
    /*!
        Blocking call

        \param userId - user ID
        \param pTrainedActionsOut - receives a bit vector composed of IEE_FacialExpressionAlgo_t contants

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_FacialExpressionAlgo_t
    */
    EDK_API int
        IEE_FacialExpressionGetTrainedSignatureActions(unsigned int userId,
                                                       unsigned long* pTrainedActionsOut);


    //! Check if the user has trained sufficient actions to activate a trained signature
    /*!
        *pfAvailableOut will be set to 1 if the user has trained FE_NEUTRAL and at least
        one other FacialExpression action.  Otherwise, *pfAvailableOut == 0.

        Blocking call

        \param userId - user ID
        \param pfAvailableOut - receives an int that is non-zero if a trained signature can be activated

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_FacialExpressionGetTrainedSignatureAvailable(unsigned int userId,
                                                         int* pfAvailableOut);
    

    //! Configure the FacialExpression suite to use either the built-in, universal signature or a personal, trained signature
    /*!
        Note: FacialExpression defaults to use its universal signature.  This function will fail if IEE_FacialExpressionGetTrainedSignatureAvailable returns false.

        Blocking call

        \param userId - user ID
        \param sigType - signature type to use

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_FacialExpressionSignature_t
    */
    EDK_API int 
        IEE_FacialExpressionSetSignatureType(unsigned int userId,
                                             IEE_FacialExpressionSignature_t sigType);
    

    //! Check whether the FacialExpression suite is currently using either the built-in, universal signature or a trained signature
    /*!
        Blocking call

        \param userId - user ID
        \param pSigTypeOut - receives the signature type currently in use

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_FacialExpressionSignature_t
    */
    EDK_API int 
        IEE_FacialExpressionGetSignatureType(unsigned int userId,
                                             IEE_FacialExpressionSignature_t* pSigTypeOut);
    

    //! Set the current MentalCommand active action types
    /*!
        \param userId - user ID
        \param activeActions - a bit vector composed of IEE_MentalCommandAction_t contants

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_MentalCommandAction_t
    */
    EDK_API int
        IEE_MentalCommandSetActiveActions(unsigned int userId,
                                          unsigned long activeActions);

    
    //! Get the current MentalCommand active action types
    /*!
        \param userId - user ID
        \param pActiveActionsOut - receive a bit vector composed of IEE_MentalCommandAction_t contants

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_MentalCommandAction_t
    */
    EDK_API int
        IEE_MentalCommandGetActiveActions(unsigned int userId,
                                          unsigned long* pActiveActionsOut);

    
    //! Return the duration of a MentalCommand training session
    /*!
        \param userId - user ID
        \param pTrainingTimeOut - receive the training time in ms

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int 
        IEE_MentalCommandGetTrainingTime(unsigned int userId,
                                         unsigned int* pTrainingTimeOut);

    
    //! Set the training control flag for MentalCommand training
    /*!
        \param userId - user ID
        \param control - pre-defined MentalCommand training control

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_MentalCommandTrainingControl_t
    */
    EDK_API int 
        IEE_MentalCommandSetTrainingControl(unsigned int userId,
                                            IEE_MentalCommandTrainingControl_t control);

    
    //! Set the type of MentalCommand action to be trained
    /*!
        \param userId - user ID
        \param action - which action would like to be trained

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_MentalCommandAction_t
    */
    EDK_API int 
        IEE_MentalCommandSetTrainingAction(unsigned int userId,
                                           IEE_MentalCommandAction_t action);


    //! Get the type of MentalCommand action currently selected for training
    /*!
        \param userId - user ID
        \param pActionOut - action that is currently selected for training

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_MentalCommandAction_t
    */
    EDK_API int 
        IEE_MentalCommandGetTrainingAction(unsigned int userId,
                                           IEE_MentalCommandAction_t* pActionOut);


    //! Get a list of the actions that have been trained by the user
    /*!
        Blocking call

        \param userId - user ID
        \param pTrainedActionsOut - receives a bit vector composed of IEE_MentalCommandAction_t contants

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_MentalCommandAction_t
    */
    EDK_API int
        IEE_MentalCommandGetTrainedSignatureActions(unsigned int userId,
                                                    unsigned long* pTrainedActionsOut);
    
    
    //! Get the current overall skill rating of the user in MentalCommand
    /*!
        Blocking call

        \param userId - user ID
        \param pOverallSkillRatingOut - receives the overall skill rating [from 0.0 to 1.0]

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_MentalCommandGetOverallSkillRating(unsigned int userId,
                                               float* pOverallSkillRatingOut);


    //! Get the current skill rating for particular MentalCommand actions of the user
    /*!
        Blocking call

        \param userId - user ID
        \param action - a particular action of IEE_MentalCommandAction_t contant
        \param pActionSkillRatingOut - receives the action skill rating [from 0.0 to 1.0]

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h, IEE_MentalCommandAction_t
    */
    EDK_API int
        IEE_MentalCommandGetActionSkillRating(unsigned int userId,
                                              IEE_MentalCommandAction_t action,
                                              float* pActionSkillRatingOut);

    
    //! Set the overall sensitivity for all MentalCommand actions
    /*!
        \param userId - user ID
        \param level - sensitivity level of all actions (lowest: 1, highest: 7)

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int 
        IEE_MentalCommandSetActivationLevel(unsigned int userId,
                                            int level);

    
    //! Set the sensitivity of MentalCommand actions
    /*!
        \param userId - user ID
        \param action1Sensitivity - sensitivity of action 1 (min: 1, max: 10)
        \param action2Sensitivity - sensitivity of action 2 (min: 1, max: 10)
        \param action3Sensitivity - sensitivity of action 3 (min: 1, max: 10)
        \param action4Sensitivity - sensitivity of action 4 (min: 1, max: 10)

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int 
        IEE_MentalCommandSetActionSensitivity(unsigned int userId,
                                              int action1Sensitivity,
                                              int action2Sensitivity,
                                              int action3Sensitivity,
                                              int action4Sensitivity);

    
    //! Get the overall sensitivity for all MentalCommand actions
    /*!
        \param userId - user ID
        \param pLevelOut - sensitivity level of all actions (min: 1, max: 10)

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int 
        IEE_MentalCommandGetActivationLevel(unsigned int userId,
                                            int *pLevelOut);

    
    //! Query the sensitivity of MentalCommand actions
    /*!
        \param userId - user ID
        \param pAction1SensitivityOut - sensitivity of action 1
        \param pAction2SensitivityOut - sensitivity of action 2
        \param pAction3SensitivityOut - sensitivity of action 3
        \param pAction4SensitivityOut - sensitivity of action 4

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int 
        IEE_MentalCommandGetActionSensitivity(unsigned int userId,
                                              int* pAction1SensitivityOut,
                                              int* pAction2SensitivityOut,
                                              int* pAction3SensitivityOut,
                                              int* pAction4SensitivityOut);

    
    //! Start the sampling of Neutral state in MentalCommand
    /*!
        \param userId - user ID

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_MentalCommandStartSamplingNeutral(unsigned int userId);

    
    //! Stop the sampling of Neutral state in MentalCommand
    /*!
        \param userId - user ID

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_MentalCommandStopSamplingNeutral(unsigned int userId);

    
    //! Enable or disable signature caching in MentalCommand
    /*!
        Enable signature caching will shorten the time to build the signature after each training,
        with the penalty of more memory usage.
     
        \param userId  - user ID
        \param enabled - flag to set status of caching (1: enable, 0: disable)

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_MentalCommandSetSignatureCaching(unsigned int userId,
                                             unsigned int enabled);


    //! Query the status of signature caching in MentalCommand
    /*!
        \param userId  - user ID
        \param pEnabledOut - flag to get status of caching (1: enable, 0: disable)

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_MentalCommandGetSignatureCaching(unsigned int userId,
                                             unsigned int* pEnabledOut);


    //! Set the cache size for the signature caching in MentalCommand
    /*!
        \param userId - user ID
        \param size   - number of signatures to be kept in the cache (0: unlimited)

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_MentalCommandSetSignatureCacheSize(unsigned int userId,
                                               unsigned int size);


    //! Get the current cache size for the signature caching in MentalCommand
    /*!
        \param userId - user ID
        \param pSizeOut - number of signatures to be kept in the cache (0: unlimited)

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_MentalCommandGetSignatureCacheSize(unsigned int userId,
                                               unsigned int* pSizeOut);


    //! Return a struct containing details about a specific channel
    /*!
        \param channelId - channel identifier (see IEmoStateDll.h)
        \param pDescriptorOut - provides detailed sensor location and other info

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IEmoStateDll.h, IedkErrorCode.h
    */
    EDK_API int
        IEE_HeadsetGetSensorDetails(IEE_InputChannels_t channelId,
                                    IInputSensorDescriptor_t* pDescriptorOut);


    //! Return the current hardware version of the headset and dongle for a particular user
    /*!
        \param userId - user ID for query
        \param pHwVersionOut - hardware version for the user headset/dongle pair. hiword is headset version, loword is dongle version.

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IEmoStateDll.h, IedkErrorCode.h
    */
    EDK_API int
        IEE_HardwareGetVersion(unsigned int userId,
                               unsigned long* pHwVersionOut);
    

    //! Return the current version of the Emotiv SDK
    /*!
        \param pszVersionOut - SDK software version in X.X.X format.
        \param nVersionChars - Length of char buffer pointed to by pszVersion argument.
        \param pBuildNumOut  - Build number. Unique for each release.

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IedkErrorCode.h
    */
    EDK_API int
        IEE_SoftwareGetVersion(char* pszVersionOut,
                               unsigned int nVersionChars,
                               unsigned long* pBuildNumOut);
    

    //! Return the delta of the movement of the gyro since the previous call for a particular user
    /*!
        \param userId - user ID for query
        \param pXOut  - horizontal displacement
        \param pYOut  - vertical displacment

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IEmoStateDll.h, IedkErrorCode.h
    */
    EDK_API int
        IEE_HeadsetGetGyroDelta(unsigned int userId,
                                int* pXOut,
                                int* pYOut);
    

    //! Re-zero the gyro for a particular user
    /*!
        \param userId - user ID for query

        \return EDK_ERROR_CODE
                - EDK_OK if successful

        \sa IEmoStateDll.h, IedkErrorCode.h
    */
    EDK_API int
        IEE_HeadsetGyroRezero(unsigned int userId);
    
    
    //! Return a handle to memory that can hold motion data.
    //  This handle can be reused by the caller to retrieve subsequent data.
    /*!
        \return DataHandle
     */
    EDK_API DataHandle
        IEE_MotionDataCreate();
    
    
    //! Free memory referenced by a data handle.
    /*!
        \param hData - a handle returned by IEE_MotionDataCreate()
     */
    EDK_API void
        IEE_MotionDataFree(DataHandle hData);
    
    
    //! Update the content of the data handle to point to new data since the last call
    /*!
        \param userId - user ID
        \param hData - a handle returned by IEE_MotionDataCreate()
        \return EDK_ERROR_CODE
                - EDK_OK if successful
     */
    EDK_API int
        IEE_MotionDataUpdateHandle(unsigned int userId,
                                   DataHandle hData);
    
    
    //! Extract data of a particular channel from the data handle
    /*!
        \param hData - a handle returned by IEE_MotionDataCreate()
        \param channel - channel that you are interested in
        \param buffer - pre-allocated buffer
        \param bufferSizeInSample - size of the pre-allocated buffer
        \return EDK_ERROR_CODE
                - EDK_OK if successful
     */
    EDK_API int
        IEE_MotionDataGet(DataHandle hData,
                          IEE_MotionDataChannel_t channel,
                          double buffer[],
                          unsigned int bufferSizeInSample);
    
    
    //! Extract data of a list of channels from the data handle
    /*!
        \param hData - a handle returned by IEE_MotionDataCreate()
        \param channels - a list of channel that you are interested in
        \param nChannels - number of channels in the channel list
        \param buffer - pre-allocated 2 dimensional buffer, has to be nChannels x bufferSizeInSample
        \param bufferSizeInSample - size of the pre-allocated buffer for each channel
        \return EDK_ERROR_CODE
                - EDK_OK if successful
     */
    EDK_API int
        IEE_MotionDataGetMultiChannels(DataHandle hData,
                                       IEE_MotionDataChannel_t channels[],
                                       unsigned int nChannels,
                                       double* buffer[],
                                       unsigned int bufferSizeInSample);
    
    
    //! Return number of sample of motion data stored in the data handle
    /*!
        \param hData - a handle returned by IEE_MotionDataCreate()
        \param nSampleOut - receives the number of sample of data stored in the data handle
        \return EDK_ERROR_CODE
                - EDK_OK if successful
     */
    EDK_API int
        IEE_MotionDataGetNumberOfSample(DataHandle hData,
                                        unsigned int* nSampleOut);
    
    
    //! Set the size of the motion data buffer.
    /*!
        The size of the buffer affects how frequent IEE_MotionDataUpdateHandle() needs to be called to prevent data loss.
     
        \param bufferSizeInSec - buffer size in second
        \return EDK_ERROR_CODE
                - EDK_OK if successful
     */
    EDK_API int
        IEE_MotionDataSetBufferSizeInSec(float bufferSizeInSec);
    
    
    //! Return the size of the motion data buffer
    /*!
        \param pBufferSizeInSecOut - receives the size of the data buffer
        \return EDK_ERROR_CODE
                - EDK_OK if successful
     */
    EDK_API int
        IEE_MotionDataGetBufferSizeInSec(float* pBufferSizeInSecOut);
    
    
    //! Get sampling rate of the motion data stream
    /*!
        \param userId - user ID
        \param samplingRateOut - receives the sampling rate
        \return EDK_ERROR_CODE
                - EDK_OK if successful
     */
    EDK_API int
        IEE_MotionDataGetSamplingRate(unsigned int userId,
                                      unsigned int* samplingRateOut);
    
#if defined(__APPLE__) || defined(__ANDROID__)
    
    //!
    //! The following API calls are only applicable for Mac/iOS/Android to establish BTLE connection with the headset.
    //!
    
    //! Initialize access to BTLE devices
    /*!
        \remark Available on Mac/iOS/Android only.
     
        \return true if initialised successfully
     */
    EDK_API bool
        IEE_EmoInitDevice();
    
    
    //! Connect to a particular headset
    /*!
        \remark Available on Mac/iOS/Android only.
     
        \param indexDevice - the index of device in list (start with 0)
        \return true if connected successfully
     */
    EDK_API int
        IEE_EmoConnectDevice(int indexDevice);
    
    
    //! Check the signal strength of current connected device
    /*!
        \remark Available on Mac/iOS/Android only.
     
        If there are multiple headsets around, you should choose to connect to the one with strongest signal.
     
        \param value - -30 to 0 (weak to strong)
     */
    EDK_API void
        IEE_GetSignalStrengthBLEInsight(int& value);
    
    
    //! Get number of Insight headset in the list
    /*!
        \remark Available on Mac/iOS/Android only.
     
        \return number of Insight headsets
         */
    EDK_API int
        IEE_GetNumberDeviceInsight();
    
    
    //! Return name of headset in listed devices
    /*!
        \remark Available on Mac/iOS/Android only.
     
        \param index - index in list device
        \return const char* - name of the headset
    */
    EDK_API const char*
        IEE_GetNameDeviceInsightAtIndex(int index);
    
#endif
    
#ifdef __cplusplus
};
#endif
#endif
