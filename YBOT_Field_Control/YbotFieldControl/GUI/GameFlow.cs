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
            redSwitchTurnedOn = false;
            greenSwitchTurnedOn = false;
            speedMode = false;

            //Switch Game Mode to Reset Field
            gameMode = fc.ChangeGameMode (GameModes.reset);
            Thread.Sleep (200);

            //Clear all the nodes' information
            fc.ClearNodeState ();
            fc.GetNodeState();

            // Set game field to ready
            gameMode = fc.ChangeGameMode(GameModes.ready);
            Thread.Sleep(1000);

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
            } else if (gameMode == GameModes.manual && !speedMode) {
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
            } else if (gameMode == GameModes.manual && speedMode) {
                // Switching to speed mode
                if (fc.switchMode) {
                    fc.switchMode = false;

                    //Ring bell
                    fc.RingBell ();                 
                    Thread.Sleep (200);

                    // Turn robots on in manaul
                    fc.RobotTransmitters ("both", State.off, State.on);
                    // Get switch inputs
                    fc.GetNodeState(3);
                    fc.GetNodeState(8);
                    Thread.Sleep(200);

                    redSwitchTurnedOn = fc.InputState(3, SWITCH_UP_INPUT);
                    greenSwitchTurnedOn = fc.InputState(8, SWITCH_UP_INPUT);

                    // Turn off speed mode
                    for (int i = 0; i < TOWER_COMBO_LENGTH; ++i)
                    {
                        fc.SendMessage(BUTTON_TOWERS[i, 0], "7,0,1");
                        fc.SendMessage(BUTTON_TOWERS[i, 1], "7,0,1");
                    }

                    // Select a tower pair
                    SelectSpeedTower();

                    // Get the starting elapsed time for the speed runs
                    startingTimeElapsed = time.elapsedTime.Elapsed.TotalSeconds;

                    GameLog ("Manual Mode Over, Start Speed Mode");
                }

                SpeedMode ();
            }
        }

        public void AutoMode() {
            // red turned the switch off
            if (fc.InputState(3, SWITCH_DOWN_INPUT) && !red.autoSwitchTurnedOff)
            {
                red.autoSwitchTurnedOff = true;
                red.score += AUTO_SWITCH_TURNED_OFF_SCORE;
                fc.SendMessage(5, "7,1,0");
            }

            // green turned the switch off
            if (fc.InputState(8, SWITCH_DOWN_INPUT) && !green.autoSwitchTurnedOff)
            {
                green.autoSwitchTurnedOff = true;
                green.score += AUTO_SWITCH_TURNED_OFF_SCORE;
                fc.SendMessage(10, "7,1,0");
            }

            if (red.autoSwitchTurnedOff) {
                if (fc.node[5].scored && !red.autoRightTowerPressed) {
                    red.autoRightTowerPressed = true;
                    red.score += AUTO_RIGHT_TOWER_PRESSED_SCORE;
                    fc.SendMessage(5, "7,0,0");
                    fc.SendMessage(1, "7,1,0");
                }

                if (red.autoRightTowerPressed && fc.node[1].scored && !red.autoLeftTowerPressed) {
                    red.autoLeftTowerPressed = true;
                    red.score += AUTO_LEFT_TOWER_PRESSED_SCORE;
                    fc.SendMessage(1, "7,0,0");
                }
            }

            if (green.autoSwitchTurnedOff) {
                if (fc.node[5].scored && !green.autoRightTowerPressed) {
                    green.autoRightTowerPressed = true;
                    green.score += AUTO_RIGHT_TOWER_PRESSED_SCORE;
                    fc.SendMessage (5, "7,0,0");
                    fc.SendMessage (1, "7,1,0");
                }

                if (green.autoRightTowerPressed && fc.node[1].scored && !green.autoLeftTowerPressed) {
                    green.autoLeftTowerPressed = true;
                    green.score += AUTO_LEFT_TOWER_PRESSED_SCORE;
                    fc.SendMessage (1, "7,0,0");
                }
            }
        }

        public void ManualMode() {
            
        }

        public void SpeedMode() {
            if (fc.InputState(3, SWITCH_UP_INPUT) && !redSwitchTurnedOn) {
                redSwitchTurnedOn = true;
                red.manualSwitchTurnedOn = true;
                red.score += SPEED_SWITCH_TURNED_ON_SCORE;

                // deselect teams tower
                if (selectedTowerCombo != -1) {
                    fc.SendMessage(BUTTON_TOWERS[selectedTowerCombo, 0], "7,0,1");
                }
            } else if (fc.InputState(3, SWITCH_DOWN_INPUT) && redSwitchTurnedOn) {
                // switch was turned on previously
                if (red.manualSwitchTurnedOn) {
                    // subtract that score
                    red.score -= SPEED_SWITCH_TURNED_ON_SCORE;
                }
                red.manualSwitchTurnedOn = false;
                redSwitchTurnedOn = false;

                // select teams tower
                if (selectedTowerCombo != -1) {
                    fc.SendMessage(BUTTON_TOWERS[selectedTowerCombo, 0], "7,1,1");
                }
            }

            if (fc.InputState(8, SWITCH_UP_INPUT) && !greenSwitchTurnedOn) {
                greenSwitchTurnedOn = true;
                green.manualSwitchTurnedOn = true;
                green.score += SPEED_SWITCH_TURNED_ON_SCORE;

                if (selectedTowerCombo != -1) {
                    fc.SendMessage(BUTTON_TOWERS[selectedTowerCombo, 1], "7,0,1");
                }
            } else if (fc.InputState(8, SWITCH_DOWN_INPUT) && greenSwitchTurnedOn) {
                // switch was turned on previously
                if (green.manualSwitchTurnedOn) {
                    // subtract that score
                    green.score -= SPEED_SWITCH_TURNED_ON_SCORE;
                }
                green.manualSwitchTurnedOn = false;
                greenSwitchTurnedOn = false;

                if (selectedTowerCombo != -1) {
                    fc.SendMessage(BUTTON_TOWERS[selectedTowerCombo, 1], "7,1,1");
                }
            }

            if (!teamScored)
            {
                // if time is still under the blocking time 
                if (time.elapsedTime.Elapsed.TotalSeconds - startingTimeElapsed < BLOCKING_TIME)
                {
                    if (speedTeamCheckToggle)
                    {
                        // red pressed the button
                        if (fc.node[BUTTON_TOWERS[selectedTowerCombo, 0]].scored && !redSwitchTurnedOn)
                        {
                            red.score += SPEED_BUTTON_PRESSED_SCORE;
                            red.speedTowers++;
                            teamScored = true;
                        // green pressed the button
                        }
                        else if (fc.node[BUTTON_TOWERS[selectedTowerCombo, 1]].scored && !greenSwitchTurnedOn)
                        {
                            green.score += SPEED_BUTTON_PRESSED_SCORE;
                            green.speedTowers++;
                            teamScored = true;
                        }
                    }
                    else
                    {
                        // red pressed the button
                        if (fc.node[BUTTON_TOWERS[selectedTowerCombo, 1]].scored && !greenSwitchTurnedOn)
                        {
                            green.score += SPEED_BUTTON_PRESSED_SCORE;
                            green.speedTowers++;
                            teamScored = true;
                        }
                        // green pressed the button
                        else if (fc.node[BUTTON_TOWERS[selectedTowerCombo, 0]].scored && !redSwitchTurnedOn)
                        {
                            red.score += SPEED_BUTTON_PRESSED_SCORE;
                            red.speedTowers++;
                            teamScored = true;
                        }
                    }

                    speedTeamCheckToggle = !speedTeamCheckToggle;

                    // atleast one of the teams pressed a button, select a new tower combo
                    if (teamScored)
                    {
                        SelectSpeedTower();
                    }
                }
                else
                {
                    SelectSpeedTower();
                }
            }
        }

        private void SelectSpeedTower() {
            // Deselect towers
            if (selectedTowerCombo != -1) {
                GameLog(string.Format("Deselecting speed towers {0} and {1}",
                    BUTTON_TOWERS[selectedTowerCombo, 0],
                    BUTTON_TOWERS[selectedTowerCombo, 1]));
                fc.SendMessage(BUTTON_TOWERS[selectedTowerCombo, 0], "7,0,1");
                fc.SendMessage(BUTTON_TOWERS[selectedTowerCombo, 1], "7,0,1");

                // Clear scored flag
                fc.node[BUTTON_TOWERS[selectedTowerCombo, 0]].scored = false;
                fc.node[BUTTON_TOWERS[selectedTowerCombo, 1]].scored = false;
                Thread.Sleep(200);
            }

            // Pick new tower combo
            var newTowerCombo = randomNumber.Next(0, TOWER_COMBO_LENGTH);
            while (selectedTowerCombo == newTowerCombo) {
                newTowerCombo = randomNumber.Next(0, TOWER_COMBO_LENGTH);
            }
            selectedTowerCombo = newTowerCombo;
            GameLog (string.Format ("Selecting speed towers {0} and {1}",
                BUTTON_TOWERS[selectedTowerCombo, 0],
                BUTTON_TOWERS[selectedTowerCombo, 1]));
            // Select towers
            if (!redSwitchTurnedOn) {
                fc.SendMessage (BUTTON_TOWERS[selectedTowerCombo, 0], "7,1,1");
            }
            if (!greenSwitchTurnedOn) {
                fc.SendMessage (BUTTON_TOWERS[selectedTowerCombo, 1], "7,1,1");
            }

            // Clear scored flag
            fc.node[BUTTON_TOWERS[selectedTowerCombo, 0]].scored = false;
            fc.node[BUTTON_TOWERS[selectedTowerCombo, 1]].scored = false;

            // Restart time
            startingTimeElapsed = time.elapsedTime.Elapsed.TotalSeconds;
            teamScored = false;
        }

        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="text">Text as string</param>
        private void GameLog(string text)
        {
            DateTime now = DateTime.Now;
            var time = now.TimeOfDay.ToString();
            var s = string.Format("{0} : {1}", time, text);
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
