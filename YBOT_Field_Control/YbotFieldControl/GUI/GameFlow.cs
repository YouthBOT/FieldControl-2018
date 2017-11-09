using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace YbotFieldControl
{
    public partial class GameControl
    {
        //Initiate game class
        public void Begin () {
            red.Reset ();                   //Reset Red variables
            green.Reset ();                 //Reset Green variables

            //Switch Game Mode to Reset Field
            gameMode = fc.ChangeGameMode(GameModes.reset);

            //Clear all the nodes' information
            fc.ClearNodeState();
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void GameStartUp() {
            GameStartUp (GameModes.autonomous);
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void GameStartUp(GameModes mode) {
            // Reset variables and flags    
            Begin ();  
                    
            // Set game field to ready
            gameMode = fc.ChangeGameMode(GameModes.ready);
            Thread.Sleep(200);

            // Set to mode and start game
            gameMode = fc.ChangeGameMode (mode);
            fc.RobotTransmitters ("both", State.on, State.on);
            GameLog("Game Started");
        }

        /// <summary>
        /// End a Game
        /// </summary>
        public void GameShutDown() {
            //Turn on all Hub Channel
            fc.FieldAllOff ();
            gameMode = fc.ChangeGameMode (GameModes.off);

            //Sound buzzer
            fc.SoundBuzzer();
            Thread.Sleep(500);

            // Turn all the lights off
            fc.AllFieldLights (LightColor.off, State.off);
            // Turn the robots off
            fc.RobotTransmitters ("both", State.off, State.off);

            // <TODO> Turn off button lights
            // <TODO> Turn off Manual and Auto relays

            //Record to logs
            GameLog ("Game End");
        }

        /// <summary>
        /// Main Game control : enters and exits game modes
        /// </summary>
        public void MainGame() {
            if (gameMode == GameModes.autonomous || gameMode == GameModes.mantonomous) {
                // Switching to auto mode
                if (fc.switchMode) {
                    fc.switchMode = false;

                    // Ring the bell
                    fc.RingBell ();
                    Thread.Sleep (200);

                    for (int i = 0; i < 3; ++i) {
                        fc.Light (buttonTowers[i, 0], LightColor.blue);
                        fc.Light (buttonTowers[i, 1], LightColor.blue);
                    }

                    // <TODO> light up buttons

                    GameLog ("Start Auto Mode");
                }

                AutoMode ();
            } else if (gameMode == GameModes.manual) {
                // Switching to manual mode
                if (fc.switchMode) {
                    fc.switchMode = false;

                    // Ring the bell
                    fc.RingBell ();
                    Thread.Sleep (200);

                    // <TODO> determine what happens with the lights during manual
                    // Turn all the lights off
                    fc.AllFieldLights (LightColor.off, State.off);

                    GameLog ("Auto Mode Over, Start Manual Mode");      //Update Log
                }

                ManualMode ();
            } else if (gameMode == GameModes.speed) {
                // Switching to speed mode
                if (fc.switchMode) {
                    fc.switchMode = false;

                    fc.RingBell ();                 //Ring bell
                    Thread.Sleep (200);

                    // get the starting elapsed time for the speed runs
                    startingTimeElapsed = time.elapsedTime.Elapsed.TotalSeconds;
                    selectedTowerCombo = randomNumber.Next (0, 2);
                    fc.Light (buttonTowers[selectedTowerCombo, 0], LightColor.green);
                    fc.Light (buttonTowers[selectedTowerCombo, 1], LightColor.red);

                    GameLog ("Manual Mode Over, Start Speed Mode");
                }

                SpeedMode ();
            }
        }

        public void AutoMode() {
            // green throw the switch
            if (fc.node[3].scored && !greenSwitchThrown) {
                greenSwitchThrown = true;
                fc.Light (3, LightColor.green);
                green.score += autoSwitchThrowScore;
            }

            // red throw the switch
            if (fc.node[8].scored && !redSwitchThrown) {
                redSwitchThrown = true;
                fc.Light (3, LightColor.red);
                red.score += autoSwitchThrowScore;
            }

            // <TODO> possible don't need the conditional for the switch
            if (greenSwitchThrown) {
                if (fc.node[1].scored && !tower1Pressed) {
                    tower1Pressed = true;
                    fc.Light (1, LightColor.green);
                    green.score += autoButtonPressScore;
                }

                if (fc.node[5].scored && !tower5Pressed) {
                    tower5Pressed = true;
                    fc.Light (5, LightColor.green);
                    green.score += autoButtonPressScore;
                }
            }

            if (redSwitchThrown) {
                if (fc.node[6].scored && !tower6Pressed) {
                    tower6Pressed = true;
                    fc.Light (6, LightColor.red);
                    red.score += autoButtonPressScore;
                }

                if (fc.node[10].scored && !tower10Pressed) {
                    tower10Pressed = true;
                    fc.Light (10, LightColor.red);
                    red.score += autoButtonPressScore;
                }
            }
        }

        public void ManualMode() {
            
        }

        public void SpeedMode() {
  
            if (time.elapsedTime.Elapsed.TotalSeconds - startingTimeElapsed < blockingTime) {

            } else {
                fc.Light (buttonTowers[selectedTowerCombo, 0], LightColor.off);
                fc.Light (buttonTowers[selectedTowerCombo, 1], LightColor.off);
                startingTimeElapsed = time.elapsedTime.Elapsed.TotalSeconds;
                selectedTowerCombo = randomNumber.Next (0, 2);
                fc.Light (buttonTowers[selectedTowerCombo, 0], LightColor.green);
                fc.Light (buttonTowers[selectedTowerCombo, 1], LightColor.red);
            }



        }

        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="text">Text as string</param>
        private void GameLog(string text)
        {
            DateTime now = DateTime.Now;
            string time = now.TimeOfDay.ToString();
            string s = string.Format("{0} : {1}", time, text);
            logBuilder.AppendLine(s);
        }

        public void LogGame()
        {
            string file = string.Format("\\Match {0} - Log", matchNumber.ToString());       //File name
            string folder = string.Format("Matches\\Match {0}", matchNumber.ToString());    //Folder path
            lw.Log(logBuilder.ToString(), file, folder);
            fc.writeLogs(folder);
            logBuilder.Clear();
        }
    }
}
