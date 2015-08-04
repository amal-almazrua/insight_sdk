/**
 * Emotiv Insight SDK
 * Copyright (c) 2015 Emotiv Inc.
 *
 * This file is part of the Emotiv Insight SDK.
 * 
 * Header file to define constants and interfaces to access the EmoState structure.
 *
 * EmoStates are generated by the Emotiv detection engine (EmoEngine) and represent
 * the emotional status of the user at a given time.
 *
 * EmoStateHandle is an opaque reference to an internal EmoState structure
 *
 * None of the EmoState interface functions are thread-safe.
 *
 * This header file is designed to be included under C and C++ environment.
 *
 */

#ifndef IEMOSTATE_DLL_H
#define IEMOSTATE_DLL_H
#include <sys/types.h>

#if !defined(EDK_STATIC_LIB) && !defined(EDK_UTILS_ONLY)
    #ifdef _WIN32
        #ifdef EMOSTATE_DLL_EXPORTS
            #define EMOSTATE_DLL_API __declspec(dllexport)
        #else
            #define EMOSTATE_DLL_API __declspec(dllimport)
        #endif
    #else
        #define EMOSTATE_DLL_API
    #endif
#else
    #define EMOSTATE_DLL_API extern
#endif

/**
 * Defining EmoStateHandle as a void pointer
 */
typedef void* EmoStateHandle;

#ifdef __cplusplus
extern "C"
{
#endif
    /**
     * Emotiv Detection Suite enumerator
     */
    typedef enum IEE_EmotivSuite_enum {

        IEE_FACIALEXPRESSION = 0, IEE_PERFORMANCEMETRIC, IEE_MENTALCOMMAND

    } IEE_EmotivSuite_t;

    /**
     * FacialExpression facial expression type enumerator
     */
    typedef enum IEE_FacialExpressionAlgo_enum {

        FE_NEUTRAL    = 0x0001,
        FE_BLINK      = 0x0002,
        FE_WINK_LEFT  = 0x0004,
        FE_WINK_RIGHT = 0x0008,
        FE_HORIEYE    = 0x0010,
        FE_SURPRISE   = 0x0020,
        FE_FROWN      = 0x0040,
        FE_SMILE      = 0x0080,
        FE_CLENCH     = 0x0100,

    } IEE_FacialExpressionAlgo_t;
    
    /**
     * PerformanceMetric emotional type enumerator
     */
    typedef enum IEE_PerformanceMetricAlgo_enum {

        PM_EXCITEMENT = 0x0001,
        PM_RELAXATION = 0x0002,
        PM_STRESS     = 0x0004,
        PM_ENGAGEMENT = 0x0008,
        PM_INTEREST   = 0x0010

    } IEE_PerformanceMetricAlgo_t;

    /**
     * MentalCommand action type enumerator
     */
    typedef enum IEE_MentalCommandAction_enum {

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

    } IEE_MentalCommandAction_t;
    
    /**
     * Wireless Signal Strength enumerator
     */
    typedef enum IEE_SignalStrength_enum {

        NO_SIG = 0,
        BAD_SIG,
        GOOD_SIG
    
    } IEE_SignalStrength_t;

    //! Logical input channel identifiers
    /*! Note: the number of channels may not necessarily match the number of
        electrodes on your headset. Signal quality and input data for some
        sensors will be identical: CMS = DRL
    */
    typedef enum IEE_InputChannels_enum {
        
        IEE_CHAN_CMS = 0,
        IEE_CHAN_DRL = 0,
        IEE_CHAN_AF3 = 3,
        IEE_CHAN_T7  = 7,
        IEE_CHAN_Pz  = 9,
        IEE_CHAN_T8  = 12,
        IEE_CHAN_AF4 = 16
        
    } IEE_InputChannels_t;

    //! EEG Electrode Contact Quality enumeration
    /*! Used to characterize the EEG signal reception or electrode contact
        for a sensor on the headset.  Note that this differs from the wireless
        signal strength, which refers to the radio communication between the 
        headset transmitter and USB dongle receiver.
     */
    typedef enum IEE_EEG_ContactQuality_enum {
        
        IEEG_CQ_NO_SIGNAL,
        IEEG_CQ_VERY_BAD,
        IEEG_CQ_POOR,
        IEEG_CQ_FAIR,
        IEEG_CQ_GOOD
        
    } IEE_EEG_ContactQuality_t;

    //! Create EmoState handle.
    /*!
        NOTE: THIS FUNCTION HAS BEEN DEPRECATED - please use IEE_EmoStateCreate instead.

        IS_Init is called automatically after the creation of the EmoState handle.
        IS_Free must be called to free up resources during the creation of the EmoState handle.

        \return the EmoStateHandle if succeeded

        \sa IEE_EmoStateCreate, IS_Free, IS_Init
    */
    EMOSTATE_DLL_API EmoStateHandle
        IS_Create();

    //! Free EmoState handle
    /*!
        NOTE: THIS FUNCTION HAS BEEN DEPRECATED - please use IEE_EmoStateFree instead.

        \param state - EmoStateHandle that was created by IS_Create function call

        \sa IEE_EmoStateFree, IS_Create
    */
    EMOSTATE_DLL_API void
        IS_Free(EmoStateHandle state);

    //! Initialize the EmoState into neutral state
    /*!
        \param state - EmoStateHandle
        
        \sa IS_Create, IS_Free
    */
    EMOSTATE_DLL_API void
        IS_Init(EmoStateHandle state);

    //! Return the time since EmoEngine has been successfully connected to the headset
    /*!
        If the headset is disconnected from EmoEngine due to low battery or weak
        wireless signal, the time will be reset to zero.

        \param state - EmoStateHandle

        \return float - time in second

    */
    EMOSTATE_DLL_API float
        IS_GetTimeFromStart(EmoStateHandle state);

    //! Return whether the headset has been put on correctly or not
    /*!
        If the headset cannot not be detected on the head, then signal quality will not report
        any results for all the channels

        \param state - EmoStatehandle

        \return int - (1: On, 0: Off)
    */
    EMOSTATE_DLL_API int
        IS_GetHeadsetOn(EmoStateHandle state);

    //! Query the number of channels of available sensor contact quality data
    /*!
        \param state - EmoStateHandle
        \return number of channels for which contact quality data is available (int)

        \sa IS_GetNumContactQualityChannels
    */
    EMOSTATE_DLL_API int
        IS_GetNumContactQualityChannels(EmoStateHandle state);

    //! Query the contact quality of a specific EEG electrode
    /*!
        \param state - EmoStateHandle
        \param electroIdx - The index of the electrode for query

        \return IEE_EEG_ContactQuality_t - Enumerated value that characterizes the EEG electrode contact for the specified input channel

        \sa IS_GetContactQuality
    */
    EMOSTATE_DLL_API IEE_EEG_ContactQuality_t
        IS_GetContactQuality(EmoStateHandle state,
                             int electroIdx);

    //! Query the contact quality of all the electrodes in one single call
    /*!
        The contact quality will be stored in the array, contactQuality, passed to the function.
        The value stored in contactQuality[0] is identical to the result returned by
        IS_GetContactQuality(state, 0)
        The value stored in contactQuality[1] is identical to the result returned by
        IS_GetContactQuality(state, 1). etc.
        The ordering of the array is consistent with the ordering of the logical input
        channels in IEE_InputChannels_enum.

        \param state - EmoStateHandle
        \param contactQuality - array of 32-bit float of size numChannels
        \param numChannels - size (number of floats) of the input array

        \return Number of signal quality values copied to the contactQuality array.

        \sa IS_GetContactQualityFromAllChannels
    */
    EMOSTATE_DLL_API int
        IS_GetContactQualityFromAllChannels(EmoStateHandle state,
                                            IEE_EEG_ContactQuality_t* contactQuality,
                                            size_t numChannels);

    //! Query whether the user is blinking at the time the EmoState is captured.
    /*!
        \param state - EmoStateHandle

        \return blink status (1: blink, 0: not blink)

    */
    EMOSTATE_DLL_API int
        IS_FacialExpressionIsBlink(EmoStateHandle state);

    //! Query whether the user is winking left at the time the EmoState is captured.
    /*!
        \param state - EmoStateHandle

        \return left wink status (1: wink, 0: not wink)

        \sa IS_FacialExpressionIsRightWink
    */
    EMOSTATE_DLL_API int
        IS_FacialExpressionIsLeftWink(EmoStateHandle state);

    //! Query whether the user is winking right at the time the EmoState is captured.
    /*!
        \param state - EmoStateHandle

        \return right wink status (1: wink, 0: not wink)

        \sa IS_FacialExpressionIsLeftWink
    */
    EMOSTATE_DLL_API int
        IS_FacialExpressionIsRightWink(EmoStateHandle state);

    //! Query whether the eyes of the user are opened at the time the EmoState is captured.
    /*!
        \param state - EmoStateHandle

        \return eye open status (1: eyes open, 0: eyes closed)

    */
    EMOSTATE_DLL_API int
        IS_FacialExpressionIsEyesOpen(EmoStateHandle state);

    //! Query whether the user is looking up at the time the EmoState is captured.
    /*!
        \param state - EmoStateHandle

        \return eyes position (1: looking up, 0: not looking up)

        \sa IS_FacialExpressionIsLookingDown
    */
    EMOSTATE_DLL_API int
        IS_FacialExpressionIsLookingUp(EmoStateHandle state);

    //! Query whether the user is looking down at the time the EmoState is captured.
    /*!
        \param state - EmoStateHandle

        \return eyes position (1: looking down, 0: not looking down)

        \sa IS_FacialExpressionIsLookingUp
    */
    EMOSTATE_DLL_API int
        IS_FacialExpressionIsLookingDown(EmoStateHandle state);

    //! Query the eyelids state of the user
    /*!
        The left and right eyelid state are stored in the parameter leftEye and rightEye
        respectively. They are floating point values ranging from 0.0 to 1.0.
        0.0 indicates that the eyelid is fully opened while 1.0 indicates that the
        eyelid is fully closed.

        \param state - EmoStatehandle
        \param leftEye - the left eyelid state (0.0 to 1.0)
        \param rightEye - the right eyelid state (0.0 to 1.0)

    */
    EMOSTATE_DLL_API void
        IS_FacialExpressionGetEyelidState(EmoStateHandle state,
                                          float* leftEye,
                                          float* rightEye);

    //! Query the eyes position of the user
    /*!
        The horizontal and vertical position of the eyes are stored in the parameter x and y
        respectively. They are floating point values ranging from -1.0 to 1.0.
        
        For horizontal position, -1.0 indicates that the user is looking left while
        1.0 indicates that the user is looking right.
        
        For vertical position, -1.0 indicates that the user is looking down while
        1.0 indicatest that the user is looking up.

        This function assumes that both eyes have the same horizontal or vertical positions.
        (i.e. no cross eyes)

        \param state - EmoStateHandle
        \param x - the horizontal position of the eyes
        \param y - the veritcal position of the eyes

    */
    EMOSTATE_DLL_API void
        IS_FacialExpressionGetEyeLocation(EmoStateHandle state,
                                          float* x,
                                          float* y);

    //! Returns the eyebrow extent of the user (Obsolete function)
    /*!
        \param state - EmoStateHandle
        
        \return eyebrow extent value (0.0 to 1.0)

        \sa IS_FacialExpressionGetUpperFaceAction, IS_FacialExpressionGetUpperFaceActionPower
    */
    EMOSTATE_DLL_API float
        IS_FacialExpressionGetEyebrowExtent(EmoStateHandle state);

    //! Returns the smile extent of the user (Obsolete function)
    /*!
        \param state - EmoStatehandle
        
        \return smile extent value (0.0 to 1.0)

        \sa IS_FacialExpressionGetLowerFaceAction, IS_FacialExpressionGetLowerFaceActionPower
    */
    EMOSTATE_DLL_API float
        IS_FacialExpressionGetSmileExtent(EmoStateHandle state);

    //! Returns the clench extent of the user (Obsolete function)
    /*!
        \param state - EmoStatehandle

        \return clench extent value (0.0 to 1.0)

        \sa IS_FacialExpressionGetLowerFaceAction, IS_FacialExpressionGetLowerFaceActionPower
    */
    EMOSTATE_DLL_API float
        IS_FacialExpressionGetClenchExtent(EmoStateHandle state);


    //! Returns the detected upper face FacialExpression action of the user
    /*!
        \param state - EmoStatehandle

        \return pre-defined FacialExpression action types

        \sa IS_FacialExpressionGetUpperFaceActionPower
    */
    EMOSTATE_DLL_API IEE_FacialExpressionAlgo_t
        IS_FacialExpressionGetUpperFaceAction(EmoStateHandle state);

    //! Returns the detected upper face FacialExpression action power of the user
    /*!
        \param state - EmoStatehandle

        \return power value (0.0 to 1.0)

        \sa IS_FacialExpressionGetUpperFaceAction
    */
    EMOSTATE_DLL_API float
        IS_FacialExpressionGetUpperFaceActionPower(EmoStateHandle state);

    //! Returns the detected lower face FacialExpression action of the user
    /*!
        \param state - EmoStatehandle

        \return pre-defined FacialExpression action types

        \sa IS_FacialExpressionGetLowerFaceActionPower
    */
    EMOSTATE_DLL_API IEE_FacialExpressionAlgo_t
        IS_FacialExpressionGetLowerFaceAction(EmoStateHandle state);

    //! Returns the detected lower face FacialExpression action power of the user
    /*!
        \param state - EmoStatehandle

        \return power value (0.0 to 1.0)

        \sa IS_FacialExpressionGetLowerFaceAction
    */
    EMOSTATE_DLL_API float
        IS_FacialExpressionGetLowerFaceActionPower(EmoStateHandle state);
    
    //! Query whether the signal is too noisy for FacialExpression detection to be active
    /*!
        \param state - EmoStateHandle
        \param type  - FacialExpression detection type

        \return detection state (0: Not Active, 1: Active)

        \sa IEE_FacialExpressionAlgo_t
    */
    EMOSTATE_DLL_API int
        IS_FacialExpressionIsActive(EmoStateHandle state,
                                    IEE_FacialExpressionAlgo_t type);

    //! Returns the long term excitement level of the user
    /*!
        \param state - EmoStateHandle

        \return excitement level (0.0 to 1.0)

        \sa IS_PerformanceMetricGetExcitementShortTermScore
    */
    EMOSTATE_DLL_API float
        IS_PerformanceMetricGetExcitementLongTermScore(EmoStateHandle state);

    //! Returns short term excitement level of the user
    /*!
        \param state - EmoStateHandle

        \return excitement level (0.0 to 1.0)

        \sa IS_PerformanceMetricGetExcitementLongTermScore
    */
    EMOSTATE_DLL_API float
        IS_PerformanceMetricGetInstantaneousExcitementScore(EmoStateHandle state);
    
    //! Query whether the signal is too noisy for PerformanceMetric detection to be active
    /*!
        \param state - EmoStateHandle
        \param type  - PerformanceMetric detection type

        \return detection state (0: Not Active, 1: Active)

        \sa IEE_PerformanceMetricAlgo_t
    */
    EMOSTATE_DLL_API int
        IS_PerformanceMetricIsActive(EmoStateHandle state,
                                     IEE_PerformanceMetricAlgo_t type);

    //! Returns meditation level of the user
    /*!
        \param state - EmoStateHandle

        \return meditation level (0.0 to 1.0)
    */
    EMOSTATE_DLL_API float
        IS_PerformanceMetricGetRelaxationScore(EmoStateHandle state);

    //! Returns frustration level of the user
    /*!
        \param state - EmoStateHandle

        \return frustration level (0.0 to 1.0)
    */
    EMOSTATE_DLL_API float
        IS_PerformanceMetricGetStressScore(EmoStateHandle state);

    //! Returns engagement/boredom level of the user
    /*!
        \param state - EmoStateHandle

        \return engagement/boredom level (0.0 to 1.0)
    */
    EMOSTATE_DLL_API float
        IS_PerformanceMetricGetEngagementBoredomScore(EmoStateHandle state);

    //! Returns valence level of the user
    /*!
        \param state - EmoStateHandle

        \return valence level (0.0 to 1.0)
    */
    EMOSTATE_DLL_API float
        IS_PerformanceMetricGetInterestScore(EmoStateHandle state);

    //! Returns focus level of the user
    /*!
        \param state - EmoStateHandle

        \return focus level (0.0 to 1.0)
    */
    EMOSTATE_DLL_API float
        IS_PerformanceMetricGetFocusScore(EmoStateHandle state);

    //! Returns the detected MentalCommand action of the user
    /*!
        \param state - EmoStateHandle

        \return MentalCommand action type

        \sa IEE_MentalCommandAction_t, IS_MentalCommandGetCurrentActionPower
    */
    EMOSTATE_DLL_API IEE_MentalCommandAction_t
        IS_MentalCommandGetCurrentAction(EmoStateHandle state);

    //! Returns the detected MentalCommand action power of the user
    /*!
        \param state - EmoStateHandle

        \return MentalCommand action power (0.0 to 1.0)

        \sa IS_MentalCommandGetCurrentAction
    */
    EMOSTATE_DLL_API float
        IS_MentalCommandGetCurrentActionPower(EmoStateHandle state);
    
    //! Query whether the signal is too noisy for MentalCommand detection to be active
    /*!
        \param state - EmoStateHandle

        \return detection state (0: Not Active, 1: Active)
    */
    EMOSTATE_DLL_API int
        IS_MentalCommandIsActive(EmoStateHandle state);

    //! Query of the current wireless signal strength
    /*!
        \param state - EmoStateHandle

        \return wireless signal strength [No Signal, Bad, Fair, Good, Excellent].

        \sa IEE_SignalStrength_t
    */
    EMOSTATE_DLL_API IEE_SignalStrength_t
        IS_GetWirelessSignalStatus(EmoStateHandle state);

    //! Clone EmoStateHandle
    /*!
        \param a - Destination of EmoStateHandle
        \param b - Source of EmoStateHandle

        \sa IS_Create
    */
    EMOSTATE_DLL_API void
        IS_Copy(EmoStateHandle a,
                EmoStateHandle b);

    //! Check whether two states are with identical 'emotiv' state
    /*!
        \param a - EmoStateHandle
        \param b - EmoStateHandle

        \return 1: Equal, 0: Different

        \sa IS_FacialExpressionEqual, IS_MentalCommandEqual, IS_EmoEngineEqual, IS_Equal
    */
    EMOSTATE_DLL_API int
        IS_PerformanceMetricEqual(EmoStateHandle a,
                                  EmoStateHandle b);

    //! Check whether two states are with identical FacialExpression state, i.e. are both state representing the same facial expression
    /*!
        \param a - EmoStateHandle
        \param b - EmoStateHandle

        \return 1: Equal, 0: Different

        \sa IS_PerformanceMetricEqual, IS_MentalCommandEqual, IS_EmoEngineEqual, IS_Equal
    */
    EMOSTATE_DLL_API int
        IS_FacialExpressionEqual(EmoStateHandle a,
                                 EmoStateHandle b);

    //! Check whether two states are with identical MentalCommand state
    /*!
        \param a - EmoStateHandle
        \param b - EmoStateHandle

        \return 1: Equal, 0: Different

        \sa IS_PerformanceMetricEqual, IS_FacialExpressionEqual, IS_EmoEngineEqual, IS_Equal
    */
    EMOSTATE_DLL_API int
        IS_MentalCommandEqual(EmoStateHandle a,
                              EmoStateHandle b);

    //! Check whether two states are with identical EmoEngine state.
    /*!
        This function is comparing the time since EmoEngine start,
        the wireless signal strength and the signal quality of different channels

        \param a - EmoStateHandle
        \param b - EmoStateHandle

        \return 1: Equal, 0: Different

        \sa IS_PerformanceMetricEqual, IS_FacialExpressionEqual, IS_MentalCommandEqual, IS_Equal
    */
    EMOSTATE_DLL_API int
        IS_EmoEngineEqual(EmoStateHandle a,
                          EmoStateHandle b);

    //! Check whether two EmoStateHandles are identical
    /*!
        \param a - EmoStateHandle
        \param b - EmoStateHandle

        \return 1: Equal, 0: Different

        \sa IS_PerformanceMetricEqual, IS_FacialExpressionEqual, IS_EmoEngineEqual
    */
    EMOSTATE_DLL_API int
        IS_Equal(EmoStateHandle a,
                 EmoStateHandle b);

    //! Get the level of charge remaining in the headset battery
    /*!
        \param state            - EmoStateHandle
        \param chargeLevel      - the current level of charge in the headset battery
        \param maxChargeLevel   - the maximum level of charge in the battery

    */
    EMOSTATE_DLL_API void
        IS_GetBatteryChargeLevel(EmoStateHandle state,
                                 int* chargeLevel,
                                 int* maxChargeLevel);

    //! Returns short term excitement model parameters
    /*!
        \param state                - EmoStateHandle
        \param rawScore             - return raw score
        \param minScale, maxScale   - return scale range
        \sa IS_PerformanceMetricGetExcitementShortTermModelParams
    */
    EMOSTATE_DLL_API void
        IS_PerformanceMetricGetInstantaneousExcitementModelParams(EmoStateHandle state,
                                                                  double* rawScore,
                                                                  double* minScale,
                                                                  double* maxScale);

    //! Returns Relaxation model parameters
    /*!
        \param state                - EmoStateHandle
        \param rawScore             - return raw score
        \param minScale, maxScale   - return scale range
        \sa IS_PerformanceMetricGetMeditationModelParams
    */
    EMOSTATE_DLL_API void
        IS_PerformanceMetricGetRelaxationModelParams(EmoStateHandle state,
                                                     double* rawScore,
                                                     double* minScale,
                                                     double* maxScale);

    //! Returns EngagementBoredom model parameters
    /*!
        \param state                - EmoStateHandle
        \param rawScore             - return raw score
        \param minScale, maxScale   - return scale range
        \sa IS_PerformanceMetricGetEngagementBoredomModelParams
    */
    EMOSTATE_DLL_API void
        IS_PerformanceMetricGetEngagementBoredomModelParams(EmoStateHandle state,
                                                            double* rawScore,
                                                            double* minScale,
                                                            double* maxScale);

    //! Returns Stress model parameters
    /*!
        \param state                - EmoStateHandle
        \param rawScore             - return raw score
        \param minScale, maxScale   - return scale range
        \sa IS_PerformanceMetricGetFrustrationModelParams
    */
    EMOSTATE_DLL_API void
        IS_PerformanceMetricGetStressModelParams(EmoStateHandle state,
                                                 double* rawScore,
                                                 double* minScale,
                                                 double* maxScale);
    
    //! Returns Interest model parameters
    /*!
        \param state                - EmoStateHandle
        \param rawScore             - return raw score
        \param minScale, maxScale   - return scale range
        \sa IS_PerformanceMetricGetValenceModelParams
    */
    EMOSTATE_DLL_API void
        IS_PerformanceMetricGetInterestModelParams(EmoStateHandle state,
                                                   double* rawScore,
                                                   double* minScale,
                                                   double* maxScale);
    //! Returns Focus model parameters
    /*!
        \param state                - EmoStateHandle
        \param rawScore             - return raw score
        \param minScale, maxScale   - return scale range
    */
    EMOSTATE_DLL_API void
        IS_PerformanceMetricGetFocusModelParams(EmoStateHandle state,
                                                double* rawScore,
                                                double* minScale,
                                                double* maxScale);

#ifdef __cplusplus
};
#endif
#endif // EMOSTATE_DLL_H

    

