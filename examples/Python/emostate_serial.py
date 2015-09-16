import sys
import os
import time
import ctypes
from arduinoCom import *

from ctypes import *

try:
    if sys.platform.startswith('win32'):
        libEDK = cdll.LoadLibrary("InsightEDK.dll")
    if sys.platform.startswith('linux'):
        srcDir = os.getcwd()
        libPath = srcDir + "/libedk.so.1.0.0"
        libEDK = CDLL(libPath)
except:
    print 'Error : cannot load dll lib'

write = sys.stdout.write
IEE_EmoEngineEventCreate = libEDK.IEE_EmoEngineEventCreate
IEE_EmoEngineEventCreate.restype = c_void_p
eEvent = IEE_EmoEngineEventCreate()

IEE_EmoEngineEventGetEmoState = libEDK.IEE_EmoEngineEventGetEmoState
IEE_EmoEngineEventGetEmoState.argtypes = [c_void_p, c_void_p]
IEE_EmoEngineEventGetEmoState.restype = c_int

IS_GetTimeFromStart = libEDK.IS_GetTimeFromStart
IS_GetTimeFromStart.argtypes = [ctypes.c_void_p]
IS_GetTimeFromStart.restype = c_float

IEE_EmoStateCreate = libEDK.IEE_EmoStateCreate
IEE_EmoStateCreate.restype = c_void_p
eState = IEE_EmoStateCreate()

IS_GetWirelessSignalStatus = libEDK.IS_GetWirelessSignalStatus
IS_GetWirelessSignalStatus.restype = c_int
IS_GetWirelessSignalStatus.argtypes = [c_void_p]

IS_FacialExpressionIsBlink = libEDK.IS_FacialExpressionIsBlink
IS_FacialExpressionIsBlink.restype = c_int
IS_FacialExpressionIsBlink.argtypes = [c_void_p]

IS_FacialExpressionIsLeftWink = libEDK.IS_FacialExpressionIsLeftWink
IS_FacialExpressionIsLeftWink.restype = c_int
IS_FacialExpressionIsLeftWink.argtypes = [c_void_p]

IS_FacialExpressionIsRightWink = libEDK.IS_FacialExpressionIsRightWink
IS_FacialExpressionIsRightWink.restype = c_int
IS_FacialExpressionIsRightWink.argtypes = [c_void_p]

IS_FacialExpressionGetUpperFaceAction =  \
    libEDK.IS_FacialExpressionGetUpperFaceAction
IS_FacialExpressionGetUpperFaceAction.restype = c_int
IS_FacialExpressionGetUpperFaceAction.argtypes = [c_void_p]

IS_FacialExpressionGetUpperFaceActionPower = \
    libEDK.IS_FacialExpressionGetUpperFaceActionPower
IS_FacialExpressionGetUpperFaceActionPower.restype = c_float
IS_FacialExpressionGetUpperFaceActionPower.argtypes = [c_void_p]

IS_FacialExpressionGetLowerFaceAction = \
    libEDK.IS_FacialExpressionGetLowerFaceAction
IS_FacialExpressionGetLowerFaceAction.restype = c_int
IS_FacialExpressionGetLowerFaceAction.argtypes = [c_void_p]

IS_FacialExpressionGetLowerFaceActionPower = \
    libEDK.IS_FacialExpressionGetLowerFaceActionPower
IS_FacialExpressionGetLowerFaceActionPower.restype = c_float
IS_FacialExpressionGetLowerFaceActionPower.argtypes = [c_void_p]

IS_PerformanceMetricGetInstantaneousExcitementTermScore = \
    libEDK.IS_PerformanceMetricGetInstantaneousExcitementScore
IS_PerformanceMetricGetInstantaneousExcitementTermScore.restype = c_float
IS_PerformanceMetricGetInstantaneousExcitementTermScore.argtypes = [c_void_p]

IS_PerformanceMetricGetExcitementLongTermScore = \
    libEDK.IS_PerformanceMetricGetExcitementLongTermScore
IS_PerformanceMetricGetExcitementLongTermScore.restype = c_float
IS_PerformanceMetricGetExcitementLongTermScore.argtypes = [c_void_p]


IS_PerformanceMetricGetEngagementBoredomScore = \
    libEDK.IS_PerformanceMetricGetEngagementBoredomScore
IS_PerformanceMetricGetEngagementBoredomScore.restype = c_float
IS_PerformanceMetricGetEngagementBoredomScore.argtypes = [c_void_p]

IS_MentalCommandGetCurrentAction = libEDK.IS_MentalCommandGetCurrentAction
IS_MentalCommandGetCurrentAction.restype = c_int
IS_MentalCommandGetCurrentAction.argtypes = [c_void_p]

IS_MentalCommandGetCurrentActionPower = \
    libEDK.IS_MentalCommandGetCurrentActionPower
IS_MentalCommandGetCurrentActionPower.restype = c_float
IS_MentalCommandGetCurrentActionPower.argtypes = [c_void_p]


# -------------------------------------------------------------------------

def logEmoState(userID, eState):

    FacialExpressionStates = {}
    FacialExpressionStates[FE_FROWN] = 0
    FacialExpressionStates[FE_SURPRISE] = 0
    FacialExpressionStates[FE_SMILE] = 0
    FacialExpressionStates[FE_CLENCH] = 0

    upperFaceAction = IS_FacialExpressionGetUpperFaceAction(eState)
    upperFacePower = IS_FacialExpressionGetUpperFaceActionPower(eState)
    lowerFaceAction = IS_FacialExpressionGetLowerFaceAction(eState)
    lowerFacePower = IS_FacialExpressionGetLowerFaceActionPower(eState)
    FacialExpressionStates[upperFaceAction] = upperFacePower
    FacialExpressionStates[lowerFaceAction] = lowerFacePower

    emoStateDict['Time'] = IS_GetTimeFromStart(eState)
    emoStateDict['UserID'] = userID.value
    emoStateDict['wirelessSigStatus'] = IS_GetWirelessSignalStatus(eState)
    emoStateDict['Blink'] = IS_FacialExpressionIsBlink(eState)
    emoStateDict['leftWink'] = IS_FacialExpressionIsLeftWink(eState)
    emoStateDict['rightWink'] = IS_FacialExpressionIsRightWink(eState)
    emoStateDict['Surprise'] = FacialExpressionStates[FE_SURPRISE]
    emoStateDict['Frown'] = FacialExpressionStates[FE_FROWN]
    emoStateDict['Clench'] = FacialExpressionStates[FE_CLENCH]
    emoStateDict['Smile'] = FacialExpressionStates[FE_SMILE]
    emoStateDict['longExcitement'] = \
        IS_PerformanceMetricGetExcitementLongTermScore(eState)
    emoStateDict['shortExcitement'] = \
        IS_PerformanceMetricGetInstantaneousExcitementTermScore(eState)
    emoStateDict['Boredom'] = \
        IS_PerformanceMetricGetEngagementBoredomScore(eState)
    emoStateDict['MentalCommand Action'] = \
        IS_MentalCommandGetCurrentAction(eState)
    emoStateDict['MentalCommand Power'] = \
        IS_MentalCommandGetCurrentActionPower(eState)

    # print emoStateDict
    emoStateTuple = (emoStateDict['Time'], emoStateDict['UserID'],
                     emoStateDict['wirelessSigStatus'], emoStateDict['Blink'],
                     emoStateDict['leftWink'], emoStateDict['rightWink'],
                     emoStateDict['Surprise'], emoStateDict['Frown'],
                     emoStateDict['Clench'], emoStateDict['Smile'],
                     emoStateDict['longExcitement'], emoStateDict[
                         'shortExcitement'],
                     emoStateDict['Boredom'], emoStateDict[
                         'MentalCommand Action'],
                     emoStateDict['MentalCommand Power'])

    return emoStateTuple


# -------------------------------------------------------------------------

userID = c_uint(0)
user = pointer(userID)
composerPort = c_uint(1726)
timestamp = c_float(0.0)
option = c_int(0)
state = c_int(0)

FE_SURPRISE = 0x0020
FE_FROWN = 0x0040
FE_SMILE = 0x0080
FE_CLENCH = 0x0100


# -------------------------------------------------------------------------

def emoStateDict():
    header = ['Time', 'UserID', 'wirelessSigStatus', 'Blink', 'leftWink',
              'rightWink', 'Surprise', 'Frown',
              'Smile', 'Clench',
              'shortExcitement', 'longExcitement',
              'Boredom', 'MentalCommand Action', 'MentalCommand Power']
    emoStateDict = {}
    for emoState in header:
        emoStateDict.setdefault(emoState, None)
    return emoStateDict


# -----------------------------------------------------------------------

# connect to Arduino

print "Please enter port for Arduino"
print "Example:"
print "Mac -- \n /dev/tty.usbmodem1451 "
print "Windows -- \n COM4"
arduino_port = str(raw_input())

setupSerial(arduino_port)

# -----------------------------------------------------------------------
# start EmoEngine or EmoComposer

print "==================================================================="
print "Example to show how to log EmoState from EmoEngine/EmoComposer."
print "==================================================================="
print "Press '1' to start and connect to the EmoEngine                    "
print "Press '2' to connect to the EmoComposer                            "
print ">> "


# -------------------------------------------------------------------------


option = int(raw_input())
if option == 1:
    if libEDK.IEE_EngineConnect("Emotiv Systems-5") != 0:
        print "Emotiv Engine start up failed."
elif option == 2:
    if libEDK.IEE_EngineRemoteConnect("127.0.0.1", composerPort) != 0:
        print "Cannot connect to EmoComposer on"
else:
    print "option = ?"

print "Start receiving Emostate! Press any key to stop logging...\n"


while (1):
    state = libEDK.IEE_EngineGetNextEvent(eEvent)
    if state == 0:
        eventType = libEDK.IEE_EmoEngineEventGetType(eEvent)
        libEDK.IEE_EmoEngineEventGetUserId(eEvent, user)
        if eventType == 64:  # libEDK.IEE_Event_enum.IEE_EmoStateUpdated
            libEDK.IEE_EmoEngineEventGetEmoState(eEvent, eState)
            timestamp = IS_GetTimeFromStart(eState)
            print "%10.3f New EmoState from user %d ...\r" % (timestamp,
                                                              userID.value)
            valToArduino(logEmoState(userID, eState))
    elif state != 0x0600:
        print "Internal error in Emotiv Engine ! "
    time.sleep(1)
# -------------------------------------------------------------------------
libEDK.IEE_EngineDisconnect()
libEDK.IEE_EmoStateFree(eState)
libEDK.IEE_EmoEngineEventFree(eEvent)
