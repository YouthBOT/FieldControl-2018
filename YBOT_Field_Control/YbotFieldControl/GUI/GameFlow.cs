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
            red.Reset ();                   //Reset Red variables
            green.Reset ();                 //Reset Green variables

            // Reset game varibles
            autoRedSwitchTurnedOff = false;
            autoGreenSwitchTurnedOff = false;
            autoTower1Pressed = false;
            autoTower5Pressed = false;
            autoTower6Pressed = false;
            autoTower10Pressed = false;
            speedRedSwitchTurnedOn = false;
            speedGreenSwitchTurnedOn = false;
            redSwitchTurnedOn = false;
            greenSwitchTurnedOn = false;

            //Switch Game Mode to Reset Field
            gameMode = fc.ChangeGameMode (GameModes.reset);
            Thread.Sleep (200);

            //Clear all the nodes' information
            fc.ClearNodeState ();

            // Set game field to ready
            gameMode = fc.ChangeGameMode(GameModes.ready);
            Thread.Sleep(200);

            // Set to mode and start game
            gameMode = fc.ChangeGameMode (mode);

            GameLog ("Game Started");
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
            // Turn off speed mode
            for (int i = 0; i < TOWER_COMBO_LENGTH; ++i) {
                fc.SendMessage (BUTTON_TOWERS[i, 0], "7,0,0");
                fc.SendMessage (BUTTON_TOWERS[i, 1], "7,0,0");
            }

            //Record to logs
            GameLog ("Game End");
        }

        /// <summary>
        /// Main Game control : enters and exits game modes
        /// </summary>
        public void MainGame() {
            if (gameMode == GameModes.autonomous) {
                // Switching to auto mode
                if (fc.switchMode) {
                    fc.switchMode = false;

                    // Ring the bell
                    fc.RingBell ();
                    Thread.Sleep (200);

                    // Turn robots on in auto
                    fc.RobotTransmitters ("both", State.on, State.on);

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

                    // Turn robots on in manaul
                    fc.RobotTransmitters ("both", State.off, State.on);

                    // Turn off speed mode
                    for (int i = 0; i < TOWER_COMBO_LENGTH; ++i) {
                        fc.SendMessage (BUTTON_TOWERS[i, 0], "7,0,0");
                        fc.SendMessage (BUTTON_TOWERS[i, 1], "7,0,0");
                    }

                    GameLog ("Auto Mode Over, Start Manual Mode");      //Update Log
                }

                ManualMode ();
            } else if (gameMode == GameModes.speed) {
                // Switching to speed mode
                if (fc.switchMode) {
                    fc.switchMode = false;

                    //Ring bell
                    fc.RingBell ();                 
                    Thread.Sleep (200);

                    // Turn robots on in manaul
                    fc.RobotTransmitters ("both", State.off, State.on);

                    // green switch is in the up position
                    if (!fc.node[3].scored) {
                        greenSwitchTurnedOn = true;
                    }

                    // red switch is in the up position
                    if (!fc.node[8].scored) {
                        redSwitchTurnedOn = true;
                    }

                    // Send speed mode to button towers
                    for (int i = 0; i < TOWER_COMBO_LENGTH; ++i) {
						fc.SendMessage(BUTTON_TOWERS[i, 0], "7,0,1");
						fc.SendMessage(BUTTON_TOWERS[i, 1], "7,0,1");
					}
                   
                    // Pick a random tower combo
                    selectedTowerCombo = randomNumber.Next (0, TOWER_COMBO_LENGTH);
                    GameLog (string.Format ("Selecting speed towers {0} and {1}",
                        BUTTON_TOWERS[selectedTowerCombo, 0],
                        BUTTON_TOWERS[selectedTowerCombo, 1]));
                    // Select those towers
                    fc.SendMessage(BUTTON_TOWERS[selectedTowerCombo, 0], "7,1,1");
					fc.SendMessage(BUTTON_TOWERS[selectedTowerCombo, 1], "7,1,1");

                    // Get the starting elapsed time for the speed runs
                    startingTimeElapsed = time.elapsedTime.Elapsed.TotalSeconds;

                    GameLog ("Manual Mode Over, Start Speed Mode");
                }

                SpeedMode ();
            }
        }

        public void AutoMode() {
            // green turned the switch off
            if (fc.node[3].scored && !autoGreenSwitchTurnedOff) {
                autoGreenSwitchTurnedOff = true;
                green.score += AUTO_SWITCH_TURNED_OFF_SCORE;
            }

            // red turned the switch off
            if (fc.node[8].scored && !autoRedSwitchTurnedOff) {
				autoRedSwitchTurnedOff = true;
                red.score += AUTO_SWITCH_TURNED_OFF_SCORE;
            }

            if (fc.node[1].scored && !autoTower1Pressed) {
                autoTower1Pressed = true;
                green.score += AUTO_LEFT_BUTTON_PRESSED_SCORE;
            }

            if (fc.node[5].scored && !autoTower5Pressed) {
                autoTower5Pressed = true;
                green.score += AUTO_RIGHT_BUTTON_PRESSED_SCORE;
            }

            if (fc.node[6].scored && !autoTower6Pressed) {
                autoTower6Pressed = true;
                green.score += AUTO_LEFT_BUTTON_PRESSED_SCORE;
            }

            if (fc.node[10].scored && !autoTower10Pressed) {
                autoTower10Pressed = true;
                green.score += AUTO_RIGHT_BUTTON_PRESSED_SCORE;
            }
        }

        public void ManualMode() {
            
        }

        public void SpeedMode() {
            // node 3 is not scored when the switch is in the up direction
            if (!fc.node[3].scored && !greenSwitchTurnedOn) {
                speedGreenSwitchTurnedOn = true;
                greenSwitchTurnedOn = true;
                green.score += SPEED_SWITCH_TURNED_ON_SCORE;
            // node 3 is scored when the switch is in the down direction
            } else if (fc.node[3].scored && greenSwitchTurnedOn) {
                // switch was turned on previously
                if (speedGreenSwitchTurnedOn) {
                    // subtract that score
                    green.score -= SPEED_SWITCH_TURNED_ON_SCORE;
                }
                speedGreenSwitchTurnedOn = false;
                greenSwitchTurnedOn = false;
            }

            // node 8 is not scored when the switch is in the up direction
            if (!fc.node[8].scored && !redSwitchTurnedOn) {
                speedRedSwitchTurnedOn = true;
                redSwitchTurnedOn = true;
                red.score += SPEED_SWITCH_TURNED_ON_SCORE;
            // node 8 is scored when the switch is in the down direction
            } else if (fc.node[8].scored && redSwitchTurnedOn) {
                // switch was turned on previously
                if (speedRedSwitchTurnedOn) {
                    // subtract that score
                    red.score -= SPEED_SWITCH_TURNED_ON_SCORE;
                }
                speedRedSwitchTurnedOn = false;
                redSwitchTurnedOn = false;
            }

            // if time is still under the blocking time 
            if (time.elapsedTime.Elapsed.TotalSeconds - startingTimeElapsed < BLOCKING_TIME) {
                bool teamScored = false;
                // green pressed the button
				if (fc.node[BUTTON_TOWERS[selectedTowerCombo, 0]].scored && !greenSwitchTurnedOn) {
                    green.score += SPEED_BUTTON_PRESSED_SCORE;
                    teamScored = true;
                // red pressed the button
                } else if (fc.node[BUTTON_TOWERS[selectedTowerCombo, 1]].scored && !redSwitchTurnedOn) {
                    red.score += SPEED_BUTTON_PRESSED_SCORE;
                    teamScored = true;
                }

                // atleast one of the teams pressed a button, select a new tower combo
                if (teamScored) {
                    SelectSpeedTower ();
                }
            } else {
                SelectSpeedTower ();
            }
        }

        private void SelectSpeedTower () {
            // Deselect towers
            fc.SendMessage (BUTTON_TOWERS[selectedTowerCombo, 0], "7,0,1");
            fc.SendMessage (BUTTON_TOWERS[selectedTowerCombo, 1], "7,0,1");

            // Pick new tower combo
            selectedTowerCombo = randomNumber.Next (0, TOWER_COMBO_LENGTH);
            GameLog (string.Format ("Selecting speed towers {0} and {1}",
                BUTTON_TOWERS[selectedTowerCombo, 0],
                BUTTON_TOWERS[selectedTowerCombo, 1]));
            // Select towers
            if (!greenSwitchTurnedOn) {
                fc.SendMessage (BUTTON_TOWERS[selectedTowerCombo, 0], "7,1,1");
            }
            if (!redSwitchTurnedOn) {
                fc.SendMessage (BUTTON_TOWERS[selectedTowerCombo, 1], "7,1,1");
            }

            // Restart time
            startingTimeElapsed = time.elapsedTime.Elapsed.TotalSeconds;
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
            Console.WriteLine (s);
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
