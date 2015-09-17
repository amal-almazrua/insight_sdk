import sys
import os
import json
import time
import ctypes

insight = Insight(composerConnect=True)


class Insight(object):

    def __init__(self, composerConnect=False, composerPort=1726, userID=0):
        self.composerConnect = composerConnect
        self.composerPort = composerPort
        self.userID - ctypes.c_uint(0)
        self.user = ctypes.pointer(userID)
        try:
            if sys.platform.startswith('win32'):
                self.libEDK = ctypes.cdll.LoadLibrary("InsightEDK.dll")
            if sys.platform.startswith('linux'):
                srcDir = os.getcwd()
                libPath = srcDir + "/self.libEDK.so.1.0.0"
                self.libEDK = ctypes.CDLL(libPath)
        except:
            print 'Error : cannot load dll lib'

        IEE_EmoEngineEventCreate = self.libEDK.IEE_EmoEngineEventCreate
        IEE_EmoEngineEventCreate.restype = ctypes.c_void_p
        self.eEvent = IEE_EmoEngineEventCreate()

        IEE_EmoStateCreate = self.libEDK.IEE_EmoStateCreate
        IEE_EmoStateCreate.restype = ctypes.c_void_p
        self.eState = IEE_EmoStateCreate()

    def connect(self):
        if self.composerConnect:
            self.libEDK.IEE_EngineRemoteConnect("127.0.0.1", self.composerPort)
        else:
            self.libEDK.IEE_EngineConnect("Emotiv Systems-5")

    def get_state(self, eEvent):
        return self.libEDK.IEE_EngineGetNextEvent(eEvent)

    def get_event_type(self, eEvent):
        return self.libEDK.IEE_EmoEngineEventGetType(eEvent)

    def get_userID(self, eEvent, user):
        return self.libEDK.IEE_EmoEngineEventGetUserId(eEvent, user)

    def get_insight_time_from_start(self, eState):
        IS_GetTimeFromStart = self.libEDK.IS_GetTimeFromStart
        IS_GetTimeFromStart.argtypes = [ctypes.ctypes.c_void_p]
        IS_GetTimeFromStart.restype = ctypes.c_float
        return IS_GetTimeFromStart(eState)

    def get_engine_event_emo_state(self, eEvent, eState):
        IEE_EmoEngineEventGetEmoState = \
            self.libEDK.IEE_EmoEngineEventGetEmoState
        IEE_EmoEngineEventGetEmoState.argtypes = [
            ctypes.c_void_p, ctypes.c_void_p]
        IEE_EmoEngineEventGetEmoState.restype = ctypes.c_int
        return IEE_EmoEngineEventGetEmoState(eEvent, eState)

    def get_wireless_signal_status(self, eState):
        IS_GetWirelessSignalStatus = self.libEDK.IS_GetWirelessSignalStatus
        IS_GetWirelessSignalStatus.restype = ctypes.c_int
        IS_GetWirelessSignalStatus.argtypes = [ctypes.c_void_p]
        return IS_GetWirelessSignalStatus(eState)

    def get_facial_expression_is_blink(self, eState):
        IS_FacialExpressionIsBlink = self.libEDK.IS_FacialExpressionIsBlink
        IS_FacialExpressionIsBlink.restype = ctypes.c_int
        IS_FacialExpressionIsBlink.argtypes = [ctypes.c_void_p]
        return IS_FacialExpressionIsBlink(eState)

    def get_left_wink(self, eState):
        IS_FacialExpressionIsLeftWink = \
            self.libEDK.IS_FacialExpressionIsLeftWink
        IS_FacialExpressionIsLeftWink.restype = ctypes.c_int
        IS_FacialExpressionIsLeftWink.argtypes = [ctypes.c_void_p]
        return IS_FacialExpressionIsLeftWink(eState)

    def get_right_wink(self, eState):
        IS_FacialExpressionIsRightWink = \
            self.libEDK.IS_FacialExpressionIsRightWink
        IS_FacialExpressionIsRightWink.restype = ctypes.c_int
        IS_FacialExpressionIsRightWink.argtypes = [ctypes.c_void_p]
        return IS_FacialExpressionIsRightWink(eState)

    def get_upper_face_action(self, eState):
        IS_FacialExpressionGetUpperFaceAction =  \
            self.libEDK.IS_FacialExpressionGetUpperFaceAction
        IS_FacialExpressionGetUpperFaceAction.restype = ctypes.c_int
        IS_FacialExpressionGetUpperFaceAction.argtypes = [ctypes.c_void_p]
        return IS_FacialExpressionGetUpperFaceAction(eState)

    def get_upper_face_action_power(self, eState):
        IS_FacialExpressionGetUpperFaceActionPower = \
            self.libEDK.IS_FacialExpressionGetUpperFaceActionPower
        IS_FacialExpressionGetUpperFaceActionPower.restype = ctypes.c_float
        IS_FacialExpressionGetUpperFaceActionPower.argtypes = [ctypes.c_void_p]
        return IS_FacialExpressionGetUpperFaceActionPower(eState)

    def get_lower_face_action(self, eState):
        IS_FacialExpressionGetLowerFaceAction = \
            self.libEDK.IS_FacialExpressionGetLowerFaceAction
        IS_FacialExpressionGetLowerFaceAction.restype = ctypes.c_int
        IS_FacialExpressionGetLowerFaceAction.argtypes = [ctypes.c_void_p]
        return IS_FacialExpressionGetLowerFaceAction(eState)

    def get_lower_face_action_power(self, eState):
        IS_FacialExpressionGetLowerFaceActionPower = \
            self.libEDK.IS_FacialExpressionGetLowerFaceActionPower
        IS_FacialExpressionGetLowerFaceActionPower.restype = ctypes.c_float
        IS_FacialExpressionGetLowerFaceActionPower.argtypes = [ctypes.c_void_p]
        return IS_FacialExpressionGetLowerFaceActionPower(eState)

    def get_short_term_excitement_score(self, eState):
        IS_PerformanceMetricGetInstantaneousExcitementTermScore = \
            self.libEDK.IS_PerformanceMetricGetInstantaneousExcitementScore
        IS_PerformanceMetricGetInstantaneousExcitementTermScore.restype = \
            ctypes.c_float
        IS_PerformanceMetricGetInstantaneousExcitementTermScore.argtypes = [
            ctypes.c_void_p]
        return IS_PerformanceMetricGetInstantaneousExcitementTermScore(eState)

    def get_long_term_excitement_score(self, eState):
        IS_PerformanceMetricGetExcitementLongTermScore = \
            self.libEDK.IS_PerformanceMetricGetExcitementLongTermScore
        IS_PerformanceMetricGetExcitementLongTermScore.restype = ctypes.c_float
        IS_PerformanceMetricGetExcitementLongTermScore.argtypes = [
            ctypes.c_void_p]
        return IS_PerformanceMetricGetExcitementLongTermScore(eState)

    def get_engagement_boredom_score(self, eState):
        IS_PerformanceMetricGetEngagementBoredomScore = \
            self.libEDK.IS_PerformanceMetricGetEngagementBoredomScore
        IS_PerformanceMetricGetEngagementBoredomScore.restype = ctypes.c_float
        IS_PerformanceMetricGetEngagementBoredomScore.argtypes = [
            ctypes.c_void_p]
        return IS_PerformanceMetricGetEngagementBoredomScore(eState)

    def get_mental_command_current_action(self, eState):
        IS_MentalCommandGetCurrentAction = \
            self.libEDK.IS_MentalCommandGetCurrentAction
        IS_MentalCommandGetCurrentAction.restype = ctypes.c_int
        IS_MentalCommandGetCurrentAction.argtypes = [ctypes.c_void_p]
        return IS_MentalCommandGetCurrentAction(eState)

    def get_mental_command_current_action_power(self, eState):
        IS_MentalCommandGetCurrentActionPower = \
            self.libEDK.IS_MentalCommandGetCurrentActionPower
        IS_MentalCommandGetCurrentActionPower.restype = ctypes.c_float
        IS_MentalCommandGetCurrentActionPower.argtypes = [ctypes.c_void_p]
        return IS_MentalCommandGetCurrentActionPower(eState)