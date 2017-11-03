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
        public void begin()
        {
            this.red.reset();                   //Reset Red variables
            this.green.reset();                 //Reset Green variables
            this.joint.reset();                 //Reset Joint variables

            //Switch Game Mode to Reset Field
            this.gameMode = this.fc.ChangeGameMode(GameModes.reset);

            //Clear all the nodes' information
            this.fc.ClearNodeState();

            //Reset Solar Variables
            sunTower = 0;
            value = 0;
            solarAligned = false;
            solarChanged = false;
            secondManSun = false;
            solarOverride = false;

            //Reset Emergency Variables
            for (int i = 0; i < emergencyTowers.Length; i++)
            {
                eTowers[i] = emergencyTowers[i];
            }
            alarmCouter = 3;

        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void GameStartUp()
        {
            begin();        //reset variables and flags       

            //Not practice Mode
            practiceMode = false;

            //Set Field Ready
            gameMode = this.fc.ChangeGameMode(GameModes.ready);

            //Pick new sun tower
            ChangeSunTower();

            //Ring Bell
            this.fc.RingBell();
            Thread.Sleep(200);

            //Set to Automode
            gameMode = this.fc.ChangeGameMode(GameModes.autonomous);
            GameLog("Game Started");
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void GameStartUp(GameModes mode)
        {
            begin();        //reset variables and flags       

            //Not pratice mode
            practiceMode = false;

            //Set game field to ready
            gameMode = this.fc.ChangeGameMode(GameModes.ready);
            Thread.Sleep(100);

            //Pick new sun tower
            ChangeSunTower();

            //Ring Bell
            this.fc.RingBell();
            Thread.Sleep(200);

            //Set to Automode
            gameMode = this.fc.ChangeGameMode(mode);
            GameLog("Game Started");
 
            //If practice mode
            if (mode == GameModes.debug)
            {
                gameMode = this.fc.ChangeGameMode(GameModes.debug);
            }
        }

        /// <summary>
        /// End a Game
        /// </summary>
        public void GameShutDown()
        {
            //Sound buzzer
            this.fc.SoundBuzzer();
            Thread.Sleep(500);

            //Stop all timers
            solarTimer.Stop();
            sunChangeTimer.Stop();

            //Record to logs
            GameLog("Towers Off");
            GameLog("Game End");
        }

        /// <summary>
        /// Main Game control :  enters and exits game modes
        /// </summary>
        public void MainGame()
        {
            //If Automode enter AutoMode
            if (gameMode == GameModes.autonomous | gameMode == GameModes.mantonomous)
            {
                if (this.fc.switchMode)
                {
                    this.fc.switchMode = false;
                    GameLog("Start AutoMode");
                }
                else AutoMode();

            }
            //If not autoMode and not Endmode enter MidMode
            else if (gameMode == GameModes.manual)
            {
                //Do this between rounds
                if (this.fc.switchMode)
                {
                    value = 0;
                    solarTimer.Stop();
                    this.fc.switchMode = false;
                    //Reset Emergency Variables
                    for (int i = 0; i < emergencyTowers.Length; i++)
                    {
                        int id = emergencyTowers[i];
                        this.fc.node[id].deviceCycled = false;
                        this.fc.node[id].alarmState = false;
                    }

                    //Pick new sun tower
                    ChangeSunTower();
                    solarChanged = false;
                    solarOverride = false;

                    this.fc.RingBell();                        //Ring bell
                    Thread.Sleep(200);

                    GameLog("AutoMode Over");                   //Update Log
                    GameLog("Transmitters ON");                 //Update Log

                    //Clear Selective Nodes information
                    this.fc.ClearNodeState(true);

                    //Pick Emergency Tower
                    GetEmerTower();
                    sunChangeTimer.Start();
                }
                else ManualMode();
            }
        }

        public void AutoMode()
        {
            //if solar panel is aligned
            if(this.fc.node[11].byte6 > 0 || solarOverride)
            {
                if (solarOverride) this.fc.node[11].byte6 = 1;
                //if aligned start timer
                if(!solarAligned)
                {
                    //Reset and Start timer
                    solarTimer.Start();
                    solarAligned = true;
                }

                //Calculate score
                if (this.fc.node[11].byte6 == 1)
                {
                    value = 4;        
                }
                else if (this.fc.node[11].byte6 == 2)
                {
                    value = 7;
                }
                else if (this.fc.node[11].byte6 == 3)
                {
                    value = 10;
                }
                
             }
            else value = 0;

            int testedTowers = 0;
            int cycledTowers = 0;

            //see if the towers have been tested or cycled
            for (int i = 0; i < emergencyTowers.Length; i++)
            {
                int tower = emergencyTowers[i];
                if (this.fc.node[tower].tested == true) testedTowers++;
                if (this.fc.node[tower].deviceCycled == true) cycledTowers++;
            }

            this.joint.autoEmergencyTowerCycled = cycledTowers;
            this.joint.autoTowerTested = testedTowers;

            //Calculate scores
            if (!this.joint.autoMan)
            {
                int towerScore = (testedTowers * 50) + (cycledTowers * 100);
                this.joint.score = towerScore + this.joint.autoSolarPanelScore;
                this.joint.autoScore = this.joint.score;

                this.red.score = this.joint.score;
                this.red.autoScore = this.red.score;

                this.green.score = this.joint.score;
                this.green.autoScore = this.green.score;
            }

        }

        public void ManualMode()
        {
            //if solar panel is aligned
            if (this.fc.node[11].byte6 > 0 || solarOverride)
            {
                if (solarOverride) this.fc.node[11].byte6 = 1;

                //if aligned start timer
                if (!solarAligned)
                {
                    solarTimer.Start();
                    solarAligned = true;
                }

                //Calculate score
                if (this.fc.node[11].byte6 == 1) value = 1;
                else if (this.fc.node[11].byte6 == 2) value = 2;
                else if (this.fc.node[11].byte6 == 3) value = 4;
                
            }
            else value = 0;

            //If Alarm has been cleared score the tower
            if (emergencyTower != 15)
            {
                if (!this.fc.node[emergencyTower].alarmState)
                {
                    this.joint.emergencyCleared++;
                    emergencyTower = 15;
                    if (this.joint.emergencyCleared < 4) GetEmerTower();
                }
            }

            //Calculate Scores
            this.joint.manScore = (this.joint.emergencyCleared * 100);
            this.joint.manScore += (this.joint.manSolarPanelScore1 + this.joint.manSolarPanelScore2);
            if (this.joint.emergencyCleared < 4) this.joint.manScore -= 250;

            this.joint.score = (this.joint.autoScore + this.joint.manScore);

            this.red.score = this.joint.score;
            this.red.autoScore = this.red.score;

            this.green.score = this.joint.score;
            this.green.autoScore = this.green.score;
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
            this.lw.Log(logBuilder.ToString(), file, folder);
            this.fc.writeLogs(folder);
            logBuilder.Clear();
        }

        //------------------------------------------------------------------------------------------------\\
        //Current year's game methods
        //------------------------------------------------------------------------------------------------\\

        private void GetEmerTower()
        {
            //Get Next Emergency Tower
            int num = rndNum.Next(0, alarmCouter);
            emergencyTower = eTowers[num];

            //Set Alarm State to true
            this.fc.node[emergencyTower].alarmState = true;

            //Send Alarm to Tower
            string str = null;
            str = ("7,1,2,");
            this.fc.SendMessage(emergencyTower, str);

            eTowers[num] = 15;
            Array.Sort(eTowers);
            if (alarmCouter > 0) alarmCouter--;

        }

        private void ChangeSunTower()
        {
            
            //Turn off Current Sun Tower
            string str = ("7,0,0,");
            this.fc.SendMessage(sunTower, str);

            //Tell Solar Panel what the new sun tower is
            sunTower = rndNum.Next(1, 11);
            str = ("7,1,4," + sunTower.ToString());
            this.fc.SendMessage(11, str);

            //Tell the new sun tower to turn on
            str = ("7,1,1,");
            this.fc.SendMessage(sunTower, str);

            //Reset Solar Panel Timers and Flags
            solarTimer.Stop();
            fc.node[solarPanel].byte6 = 0;
            value = 0;
            solarAligned = false;
            solarChanged = true;
            Thread.Sleep(50);
        }

        //Change Sun Tower after 1 min
        private void sunChangeTimer_Tick(object sender, EventArgs e)
        {
            sunChangeTimer.Stop();
            secondManSun = true;
            ChangeSunTower();
            solarOverride = false;
        }

        //Score Solar Panel every min
        private void solarTimer_Tick(object sender, EventArgs e)
        {
            if (gameMode == GameModes.autonomous)
            {
                this.joint.autoSolarPanelScore += value;
            }
            else if (gameMode == GameModes.manual)
            {
                //If Second sun tower
                if (!secondManSun)
                {
                    this.joint.manSolarPanelScore1 += value;
                }
                else
                {
                    this.joint.manSolarPanelScore2 += value;
                }
            }
        }
    }
}
