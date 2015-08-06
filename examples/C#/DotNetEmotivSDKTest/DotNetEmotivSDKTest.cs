using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Emotiv;

namespace DotNetEmotivSDKTest
{
    class Program
    {
        static System.IO.StreamWriter engineLog = new System.IO.StreamWriter("engineLog.log");
        static System.IO.StreamWriter expLog = new System.IO.StreamWriter("expLog.log");
        static System.IO.StreamWriter cogLog = new System.IO.StreamWriter("cogLog.log");
        static System.IO.StreamWriter affLog = new System.IO.StreamWriter("affLog.log");
        static Profile profile;

        static void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("connected");
        }

        static void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("disconnected");
        }
        static void engine_UserAdded(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("user added ({0})", e.userId);
            Profile profile = EmoEngine.Instance.GetUserProfile(0);
            profile.GetBytes();
        }
        static void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("user removed");
        }

        static void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();
            // Console.WriteLine("new emostate {0}", timeFromStart);
        }

        static void engine_EmoEngineEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();
            
            Int32 headsetOn = es.GetHeadsetOn();
            Int32 numCqChan = es.GetNumContactQualityChannels();            
            EdkDll.IEE_EEG_ContactQuality_t[] cq = es.GetContactQualityFromAllChannels();
            for (Int32 i = 0; i < numCqChan; ++i)
            {
                if (cq[i] != es.GetContactQuality(i))
                {
                    throw new Exception();
                }
            }
            EdkDll.IEE_SignalStrength_t signalStrength = es.GetWirelessSignalStatus();
            Int32 chargeLevel = 0;
            Int32 maxChargeLevel = 0;
            es.GetBatteryChargeLevel(out chargeLevel, out maxChargeLevel);

            engineLog.Write(
                "{0},{1},{2},{3},{4},",
                timeFromStart,
                headsetOn, signalStrength, chargeLevel, maxChargeLevel);

            for (int i = 0; i < cq.Length; ++i)
            {
                engineLog.Write("{0},", cq[i]);
            }
            engineLog.WriteLine("");
            engineLog.Flush();
        }      

        static void engine_PerformanceMetricEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();

            EdkDll.IEE_PerformanceMetricAlgo_t[] pmAlgoList = { 
                                                      EdkDll.IEE_PerformanceMetricAlgo_t.PM_ENGAGEMENT,
                                                      EdkDll.IEE_PerformanceMetricAlgo_t.PM_EXCITEMENT,
                                                      EdkDll.IEE_PerformanceMetricAlgo_t.PM_STRESS,
                                                      EdkDll.IEE_PerformanceMetricAlgo_t.PM_RELAXATION,
                                                      EdkDll.IEE_PerformanceMetricAlgo_t.PM_INTEREST,
                                                      };

            Boolean[] isAffActiveList = new Boolean[pmAlgoList.Length];

            Single longTermExcitementScore = es.PerformanceMetricGetExcitementLongTermScore();
            Single instantaneousExcitement = es.PerformanceMetricInstantaneousExcitementTermScore();
            for (int i = 0; i < pmAlgoList.Length; ++i)
            {
                isAffActiveList[i] = es.PerformanceMetricIsActive(pmAlgoList[i]);
            }

            Single meditationScore = es.PerformanceMetricGetRelaxationScore();
            Single frustrationScore = es.PerformanceMetricGetFrustrationScore();
            Single boredomScore = es.PerformanceMetricGetEngagementBoredomScore();
            Single interestScore = es.PerformanceMetricGetInterestScore();

            double rawScoreEc = 0, rawScoreMd = 0, rawScoreFt = 0, rawScoreEg = 0, rawScoreIn = 0;
            double minScaleEc = 0, minScaleMd = 0, minScaleFt = 0, minScaleEg = 0, minScaleIn = 0;
            double maxScaleEc = 0, maxScaleMd = 0, maxScaleFt = 0, maxScaleEg = 0, maxScaleIn = 0;
            double scaledScoreEc = 0, scaledScoreMd = 0, scaledScoreFt = 0, scaledScoreEg = 0, scaledScoreIn = 0;

            es.PerformanceMetricGetInstantaneousExcitementModelParams(out rawScoreEc, out minScaleEc, out maxScaleEc);
            if (minScaleEc != maxScaleEc)
            {
                if (rawScoreEc < minScaleEc)
                {
                    scaledScoreEc = 0;
                }
                else if (rawScoreEc > maxScaleEc)
                {
                    scaledScoreEc = 1;
                }
                else
                {
                    scaledScoreEc = (rawScoreEc - minScaleEc) / (maxScaleEc - minScaleEc);
                }
                Console.WriteLine("PerformanceMetric Short Excitement: Raw Score {0:f5} Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreEc, minScaleEc, maxScaleEc, scaledScoreEc);
            }

            es.PerformanceMetricGetEngagementBoredomModelParams(out rawScoreEg, out minScaleEg, out maxScaleEg);
            if (minScaleEg != maxScaleEg)
            {
                if (rawScoreEg < minScaleEg)
                {
                    scaledScoreEg = 0;
                }
                else if (rawScoreEg > maxScaleEg)
                {
                    scaledScoreEg = 1;
                }
                else
                {
                    scaledScoreEg = (rawScoreEg - minScaleEg) / (maxScaleEg - minScaleEg);
                }
                Console.WriteLine("PerformanceMetric Engagement : Raw Score {0:f5}  Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreEg, minScaleEg, maxScaleEg, scaledScoreEg);
            }
            es.PerformanceMetricGetRelaxationModelParams(out rawScoreMd, out minScaleMd, out maxScaleMd);
            if (minScaleMd != maxScaleMd)
            {
                if (rawScoreMd < minScaleMd)
                {
                    scaledScoreMd = 0;
                }
                else if (rawScoreMd > maxScaleMd)
                {
                    scaledScoreMd = 1;
                }
                else
                {
                    scaledScoreMd = (rawScoreMd - minScaleMd) / (maxScaleMd - minScaleMd);
                }
                Console.WriteLine("PerformanceMetric Meditation : Raw Score {0:f5} Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreMd, minScaleMd, maxScaleMd, scaledScoreMd);
            }
            es.PerformanceMetricGetStressModelParams(out rawScoreFt, out minScaleFt, out maxScaleFt);
            if (maxScaleFt != minScaleFt)
            {
                if (rawScoreFt < minScaleFt)
                {
                    scaledScoreFt = 0;
                }
                else if (rawScoreFt > maxScaleFt)
                {
                    scaledScoreFt = 1;
                }
                else
                {
                    scaledScoreFt = (rawScoreFt - minScaleFt) / (maxScaleFt - minScaleFt);
                }
                Console.WriteLine("PerformanceMetric Frustration : Raw Score {0:f5} Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreFt, minScaleFt, maxScaleFt, scaledScoreFt);
            }

            if (maxScaleIn != minScaleIn)
            {
                if (rawScoreIn < minScaleIn)
                {
                    scaledScoreIn = 0;
                }
                else if (rawScoreIn > maxScaleIn)
                {
                    scaledScoreIn = 1;
                }
                else
                {
                    scaledScoreIn = (rawScoreIn - minScaleIn) / (maxScaleIn - minScaleIn);
                }
                Console.WriteLine("PerformanceMetric Interest : Raw Score {0:f5} Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreIn, minScaleIn, maxScaleIn, scaledScoreIn);
            }

            affLog.Write(
                "{0},{1},{2},{3},{4},{5},{6}",
                timeFromStart,
                longTermExcitementScore, instantaneousExcitement, meditationScore, frustrationScore, boredomScore, interestScore);
          
            for (int i = 0; i < pmAlgoList.Length; ++i)
            {
                affLog.Write("{0},", isAffActiveList[i]);
            }
            affLog.WriteLine("");
            affLog.Flush();
        }

        static void engine_MentalCommandEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();

            EdkDll.IEE_MentalCommandAction_t cogAction = es.MentalCommandGetCurrentAction();
            Single power = es.MentalCommandGetCurrentActionPower();
            Boolean isActive = es.MentalCommandIsActive();            

            cogLog.WriteLine(
                "{0},{1},{2},{3}",
                timeFromStart,
                cogAction, power, isActive);
            cogLog.Flush();
        }

        static void engine_FacialExpressionEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();

            EdkDll.IEE_FacialExpressionAlgo_t[] expAlgoList = { 
                                                      EdkDll.IEE_FacialExpressionAlgo_t.FE_BLINK, 
                                                      EdkDll.IEE_FacialExpressionAlgo_t.FE_CLENCH, 
                                                      EdkDll.IEE_FacialExpressionAlgo_t.FE_SUPRISE, 
                                                      EdkDll.IEE_FacialExpressionAlgo_t.FE_FROWN, 
                                                      EdkDll.IEE_FacialExpressionAlgo_t.FE_HORIEYE, 
                                                      EdkDll.IEE_FacialExpressionAlgo_t.FE_NEUTRAL, 
                                                      EdkDll.IEE_FacialExpressionAlgo_t.FE_SMILE, 
                                                      EdkDll.IEE_FacialExpressionAlgo_t.FE_WINK_LEFT, 
                                                      EdkDll.IEE_FacialExpressionAlgo_t.FE_WINK_RIGHT
                                                      };
            Boolean[] isExpActiveList = new Boolean[expAlgoList.Length];

            Boolean isBlink = es.FacialExpressionIsBlink();
            Boolean isLeftWink = es.FacialExpressionIsLeftWink();
            Boolean isRightWink = es.FacialExpressionIsRightWink();
            Boolean isEyesOpen = es.FacialExpressionIsEyesOpen();
            Boolean isLookingUp = es.FacialExpressionIsLookingUp();
            Boolean isLookingDown = es.FacialExpressionIsLookingDown();
            Single leftEye = 0.0F;
            Single rightEye = 0.0F;
            Single x = 0.0F;
            Single y = 0.0F;
            es.FacialExpressionGetEyelidState(out leftEye, out rightEye);
            es.FacialExpressionGetEyeLocation(out x, out y);
            Single eyebrowExtent = es.FacialExpressionGetEyebrowExtent();
            Single smileExtent = es.FacialExpressionGetSmileExtent();
            Single clenchExtent = es.FacialExpressionGetClenchExtent();
            EdkDll.IEE_FacialExpressionAlgo_t upperFaceAction = es.FacialExpressionGetUpperFaceAction();
            Single upperFacePower = es.FacialExpressionGetUpperFaceActionPower();
            EdkDll.IEE_FacialExpressionAlgo_t lowerFaceAction = es.FacialExpressionGetLowerFaceAction();
            Single lowerFacePower = es.FacialExpressionGetLowerFaceActionPower();
            for (int i = 0; i < expAlgoList.Length; ++i)
            {
                isExpActiveList[i] = es.FacialExpressionIsActive(expAlgoList[i]);
            }

            expLog.Write(
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},",
                timeFromStart,
                isBlink, isLeftWink, isRightWink, isEyesOpen, isLookingUp,
                isLookingDown, leftEye, rightEye,
                x, y, eyebrowExtent, smileExtent, upperFaceAction,
                upperFacePower, lowerFaceAction, lowerFacePower);
            for (int i = 0; i < expAlgoList.Length; ++i)
            {
                expLog.Write("{0},", isExpActiveList[i]);
            }
            expLog.WriteLine("");
            expLog.Flush();   
        }

        static void engine_MentalCommandTrainingStarted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Start MentalCommand Training");
        }

        static void engine_MentalCommandTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("MentalCommand Training Success. (A)ccept/Reject?");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.A)
            {
                Console.WriteLine("Accept!!!");
                EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ACCEPT);
            }
            else
            {
                EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_REJECT);
            }
        }

        static void engine_MentalCommandTrainingCompleted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("MentalCommand Training Completed.");
        }

        static void engine_MentalCommandTrainingRejected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("MentalCommand Training Rejected.");
        }

        static void engine_FacialExpressionTrainingStarted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Start FacialExpression Training");
        }

        static void engine_FacialExpressionTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("FacialExpression Training Success. (A)ccept/Reject?");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.A)
            {
                Console.WriteLine("Accept!!!");
                EmoEngine.Instance.FacialExpressionSetTrainingControl(0, EdkDll.IEE_FacialExpressionTrainingControl_t.FE_ACCEPT);
            }
            else
            {               
                EmoEngine.Instance.FacialExpressionSetTrainingControl(0, EdkDll.IEE_FacialExpressionTrainingControl_t.FE_REJECT);
            }
        }

        static void engine_FacialExpressionTrainingCompleted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("FacialExpressione Training Completed.");
        }

        static void engine_FacialExpressionTrainingRejected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("FacialExpression Training Rejected.");
        }

        static void keyHandler(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.F1:                    
                    EmoEngine.Instance.MentalCommandSetTrainingAction(0, EdkDll.IEE_MentalCommandAction_t.MC_PUSH);                    
                    EmoEngine.Instance.MentalCommandSetTrainingControl(0, EdkDll.IEE_MentalCommandTrainingControl_t.MC_START);
                    break;
                case ConsoleKey.F2:
                    EmoEngine.Instance.FacialExpressionSetTrainingAction(0, EdkDll.IEE_FacialExpressionAlgo_t.FE_CLENCH);
                    EmoEngine.Instance.FacialExpressionSetTrainingControl(0, EdkDll.IEE_FacialExpressionTrainingControl_t.FE_START);
                    break;
                case ConsoleKey.F3:
                    profile = EmoEngine.Instance.GetUserProfile(0);
                    Console.WriteLine("Get profile");
                    break;
                case ConsoleKey.F4:
                    EmoEngine.Instance.SetUserProfile(0, profile);
                    Console.WriteLine("Set profile");
                    break;
                case ConsoleKey.F5:
                    EmoEngine.Instance.MentalCommandSetActivationLevel(0, 2);
                    Console.WriteLine("Cog Activateion level set to {0}", EmoEngine.Instance.MentalCommandGetActivationLevel(0));
                    break;
                case ConsoleKey.F6:
                    Console.WriteLine("Cog Activateion level is {0}", EmoEngine.Instance.MentalCommandGetActivationLevel(0));
                    break;
                case ConsoleKey.F7:
                    OptimizationParam oParam = new OptimizationParam();
                    oParam.SetVitalAlgorithm(EdkDll.IEE_EmotivSuite_t.IEE_PERFORMANCEMETRIC, 0);
                    oParam.SetVitalAlgorithm(EdkDll.IEE_EmotivSuite_t.IEE_MENTALCOMMAND, 0);
                    oParam.SetVitalAlgorithm(EdkDll.IEE_EmotivSuite_t.IEE_FACIALEXPRESSION, 0);
                    EmoEngine.Instance.OptimizationEnable(oParam);
                    Console.WriteLine("Optimization is On");
                    break;
                case ConsoleKey.F8:
                    EmoEngine.Instance.OptimizationDisable();
                    Console.WriteLine("Optimization is Off");
                    break;
                case ConsoleKey.F9:
                    String version;
                    UInt32 buildNum;
                    EmoEngine.Instance.SoftwareGetVersion(out version, out buildNum);
                    Console.WriteLine("Software Version: {0}, {1}", version, buildNum);
                    break;
            }
        }

        static void Main(string[] args)
        {

            EmoEngine engine = EmoEngine.Instance;
            
            engine.EmoEngineConnected += 
                new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected += 
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            engine.UserAdded += 
                new EmoEngine.UserAddedEventHandler(engine_UserAdded);
            engine.UserRemoved += 
                new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);
            engine.EmoStateUpdated += 
                new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);
            engine.FacialExpressionEmoStateUpdated += 
                new EmoEngine.FacialExpressionEmoStateUpdatedEventHandler(engine_FacialExpressionEmoStateUpdated);
            engine.MentalCommandEmoStateUpdated += 
                new EmoEngine.MentalCommandEmoStateUpdatedEventHandler(engine_MentalCommandEmoStateUpdated);
            engine.PerformanceMetricEmoStateUpdated += 
                new EmoEngine.PerformanceMetricEmoStateUpdatedEventHandler(engine_PerformanceMetricEmoStateUpdated);
            engine.EmoEngineEmoStateUpdated += 
                new EmoEngine.EmoEngineEmoStateUpdatedEventHandler(engine_EmoEngineEmoStateUpdated);
            engine.MentalCommandTrainingStarted +=
                new EmoEngine.MentalCommandTrainingStartedEventEventHandler(engine_MentalCommandTrainingStarted);
            engine.MentalCommandTrainingSucceeded +=
                new EmoEngine.MentalCommandTrainingSucceededEventHandler(engine_MentalCommandTrainingSucceeded);
            engine.MentalCommandTrainingCompleted += 
                new EmoEngine.MentalCommandTrainingCompletedEventHandler(engine_MentalCommandTrainingCompleted);
            engine.MentalCommandTrainingRejected += 
                new EmoEngine.MentalCommandTrainingRejectedEventHandler(engine_MentalCommandTrainingRejected);
            engine.FacialExpressionTrainingStarted +=
                new EmoEngine.FacialExpressionTrainingStartedEventEventHandler(engine_FacialExpressionTrainingStarted);
            engine.FacialExpressionTrainingSucceeded +=
                new EmoEngine.FacialExpressionTrainingSucceededEventHandler(engine_FacialExpressionTrainingSucceeded);
            engine.FacialExpressionTrainingCompleted += 
                new EmoEngine.FacialExpressionTrainingCompletedEventHandler(engine_FacialExpressionTrainingCompleted);
            engine.FacialExpressionTrainingRejected += 
                new EmoEngine.FacialExpressionTrainingRejectedEventHandler(engine_FacialExpressionTrainingRejected);

            engine.Connect();

            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            // int x, y;

            while (true)
            {
                try
                {
                    if (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey(true);
                        keyHandler(cki.Key);

                        if (cki.Key == ConsoleKey.X)
                        {
                            break;
                        }
                    }
                    engine.ProcessEvents(1000);


                    //engine.HeadsetGetGyroDelta(0, out x, out y);
                    //if (x!=0 || y!=0)
                    //    Console.WriteLine("{0}, {1}", x, y);
                }
                catch (EmoEngineException e)
                {
                    Console.WriteLine("{0}", e.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e.ToString());
                }
            }
            engine.Disconnect();
        }
    }
}
