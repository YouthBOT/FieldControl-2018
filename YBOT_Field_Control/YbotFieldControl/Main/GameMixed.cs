using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBOT_Field_Control_2016
{
    public class GameMixed
    {
        #region Game and Scoring Variables

        Field_Control fc;
        LogWriter lw = new LogWriter();    //Log Writer Constructor
        fileStructure fs = new fileStructure();

        private string time =                //Time of day stamp
              DateTime.Now.Hour.ToString() + "_"
            + DateTime.Now.Minute.ToString() + "_"
            + DateTime.Now.Second.ToString();


        public Team red = new Team("red");            //New team 
        public Team green = new Team("green");        //New team

        public int autoModeTime = 30;   //Autonomous Mode time in secs
        public int manAutoTime = 20;    //Mantonomous Mode start time
        public int midModeTime = 120;    //Mid Mode time + automode time
        public int matchNumber = 0;     //Current match number


        //Game flags

        //public bool STOP = false;           //True when game has been stopped
        public GameModes gameMode = GameModes.off;
        //public bool autoMode = false;       //True when game is in autonomous mode
        //public bool midMode = false;        //True when game is in middle game mode
        //public bool endMode = false;        //True when game is in end of game mode
        //public bool gameDone = false;       //True when game has finished
        public bool practiceMode = false;   //True when game is in practice mode

        //------------------------------------------------------------------------------------------------\\
        //Current year's game variables
        //------------------------------------------------------------------------------------------------\\

        private int autoballValue = 5;
        private int autoMax = 4;
        public int[] wamTowers = { 1, 3, 5, 6, 8, 10, 0 };
        private Random rndNum = new Random();
        private bool wamSelect = true;
        private int wamRndNum = 0;
        private int wamTowersLeft = 6;
        private Time wamTime = new Time();

        public int redButton = 0;
        public int greenButton = 1;
        public int blueButton = 0;


        #endregion

        #region Constructors

        public GameMixed()
        {
            this.fc = new Field_Control();
        }

        public GameMixed(Field_Control _fc)
        {
            this.fc = _fc;
        }
        #endregion

        //Initiate game class
        public void begin()
        {
            this.red.reset();                   //Reset Red variables
            this.green.reset();                 //Reset Green variables
            gameMode = this.fc.ChangeGameMode(GameModes.reset);
            //Read states on all towers
            foreach (Nodes nd in this.fc.node)
            {
                this.fc.UpdateNodeState(nd.id);
            }

            //Reset Wam towers
            wamTowers[0] = 1;
            wamTowers[1] = 3;
            wamTowers[2] = 5;
            wamTowers[3] = 6;
            wamTowers[4] = 8;
            wamTowers[5] = 10;
            wamTowers[6] = 15;

            wamSelect = true;
            wamTowersLeft = 6;

            this.fc.ClearNodeState();
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void GameStartUp()
        {
            begin();        //reset variables and flags       
            this.fc.StartUp();  //Start up field communications

            practiceMode = false;
            gameMode = this.fc.ChangeGameMode(GameModes.ready);

            GameLog("Game Started");

            //Ring Bell
            this.fc.RingBell();

            //Turn on All Field Lights
            this.fc.AllFieldLights(LightColor.blue, State.on);

            //Turn off All Field Lights
            this.fc.AllFieldLights(LightColor.off, State.off);

            //Reset latch status of each tower//
            //this.fc.ResetAllNodeLatches();
            GameLog("All Tower Latch Reset");

            //Set to Automode
            gameMode = this.fc.ChangeGameMode(GameModes.autonomous);

            //Turn on Robot Transmiters
            this.fc.RobotTransmitters("both", State.on, State.on);
            GameLog("Transmitters On");

            GameLog("Game Start");

            //Set towers to starting states
            //this.fc.SetRelayState(4, 5, State.on);
            //this.fc.SetRelayState(9, 5, State.on);

            //Turn all tower lights blue
            this.fc.AllFieldLights(LightColor.blue, State.on);

        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void GameStartUp(GameModes mode)
        {
            begin();        //reset variables and flags       
            this.fc.StartUp();  //Start up field communications

            practiceMode = false;
            gameMode = this.fc.ChangeGameMode(GameModes.ready);

            GameLog("Game Started");

            //Ring Bell
            this.fc.RingBell();

            //Turn on All Field Lights
            this.fc.AllFieldLights(LightColor.blue, State.on);

            //Turn off All Field Lights
            this.fc.AllFieldLights(LightColor.off, State.off);

            //Reset latch status of each tower//
            //this.fc.ResetAllNodeLatches();
            //GameLog("All Tower Latch Reset");

            //Set to Automode
            gameMode = this.fc.ChangeGameMode(mode);

            //Turn on Robot Transmiters
            if (mode == GameModes.autonomous) this.fc.RobotTransmitters("both", State.on, State.on);
            else this.fc.RobotTransmitters("both", State.off, State.on);
            GameLog("Transmitters On");

            GameLog("Game Start");

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
            //Turn off Transmitters
            this.fc.RobotTransmitters("both", State.off, State.off);
            GameLog("Transmitters Off");

            //Sound buzzer
            this.fc.SoundBuzzer();

            GameLog("Towers Off");
            GameLog("Game End");
        }

        /// <summary>
        /// Main Game control :  enters and exits game modes
        /// </summary>
        public void MainGame()
        {
            if (gameMode != GameModes.end)
            {
                //If Automode enter AutoMode
                if (gameMode == GameModes.autonomous | gameMode == GameModes.mantonomous)
                {
                    if (this.fc.switchMode)
                    {
                        this.fc.switchMode = false;
                        GameLog("Start AutoMode");
                        //this.fc.clearNodeState();
                    }
                    else AutoMode();
                }
                //If not autoMode and not Endmode enter MidMode
                else if (gameMode == GameModes.manual)
                {
                    //Do this between rounds
                    if (this.fc.switchMode)
                    {
                        this.fc.AllFieldLights(LightColor.blue, State.on);
                        this.fc.switchMode = false;
                        this.fc.RobotTransmitters("both", State.off, State.on); //Turn on transmitter to Manual Mode
                        this.fc.RingBell();                        //Ring bell
                        GameLog("AutoMode Over");                   //Update Log
                        GameLog("Transmitters ON");                 //Update Log

                        this.fc.AllFieldLights(LightColor.off, State.off);                  //Turn off all Lights
                        this.fc.ClearNodeState();

                        //Tell FoS tower it is time for main game
                        //this.fc.SetRelayState(4, 6, State.on);
                        //this.fc.SetRelayState(9, 6, State.on);

                        //Start Timer
                        wamTime.elapsedTime.Restart();
                        //Get random tower
                        getWamTower();

                    }
                    else MidMode();
                }
                else
                {
                    if (this.fc.switchMode)
                    {
                        this.fc.switchMode = false;
                    }
                    //Place Practice Mode code here
                }

                Application.DoEvents();         //Do window's stuff
            }
        }

        public void AutoMode()
        {
            //Read states on all towers
            foreach (Nodes nd in this.fc.node)
            {
                this.fc.GetNodeState(nd.id);
            }

            //Red
            //If Red is still in auto and hasn't finished the task
            if (!this.red.autoMan && !this.red.autoFinished)
            {
                //Do for all red towers
                for (int t = 1; t < 6; t++)
                {
                    if (t != 4)
                    {
                        //If not already lit
                        if (this.fc.node[t].lightStatus != LightColor.red.ToString())
                        {
                            //Check to see if any buttons are pressed on each red tower
                            if (this.fc.InputState(t, redButton) || this.fc.InputState(t, greenButton))
                            {
                                this.fc.Light(t, LightColor.off, State.off);
                                this.fc.Light(t, LightColor.red, State.on);
                                this.red.autoTowerCount++;               //Increment tower count
                                this.red.autoLightScore++;
                                GameLog("Red Scored : Tower" + t);
                            }

                        }
                        //If red has lit four towers
                        if (this.red.autoTowerCount == 4)
                        {
                            for (t = 1; t < 6; t++)
                            {
                                //If not already lit
                                if (this.fc.node[t].lightMode != LightColor.red.ToString())
                                {
                                    this.fc.Light(t, LightColor.off, State.off);
                                    this.fc.Light(t, LightColor.red, State.on);   //Light them all red
                                }
                            }
                            this.red.autoFinished = true;            //Finish AutoMode
                            this.red.autoLightScore = 5;
                            GameLog("Red Bonus Light");
                            GameLog("Red Finished AutoMode");
                        }
                    }
                }
            }
            //If this.red is in mantonomous mode 
            else if (!this.red.autoFinished)
            {
                //Do for all red towers
                for (int t = 1; t < 6; t++)
                {
                    if (t != 4)
                    {
                        //If not already lit
                        if (this.fc.node[t].lightStatus != LightColor.red.ToString() && this.fc.node[t].lightStatus == LightColor.blue.ToString())
                        {
                            //Check to see if the red button is pressed 
                            if (this.fc.InputState(t, redButton) && !this.fc.InputState(t, greenButton))
                            {
                                this.fc.Light(t, LightColor.off, State.off);
                                this.fc.Light(t, LightColor.red, State.on);   //Light tower red
                                this.red.autoTowerCount++;
                                this.red.autoLightScore++;
                                GameLog("Red Scored : Tower" + t);
                            }
                            //Else if the green button is pressed
                            else if (this.fc.InputState(t, greenButton))
                            {
                                this.fc.Light(t, LightColor.off, State.off); ;   //Light tower off
                                this.red.autoTowerCount++;                       //Increament tower count
                                GameLog("Red Missed : Tower" + t);
                            }
                            //If all the towers have been pressed
                            if (this.red.autoTowerCount == 5)
                            {
                                this.red.autoFinished = true;            //Finish AutoMode
                                GameLog("Red Finished AutoMode");
                            }
                        }
                    }
                }
            }


            ////green
            //If green is still in auto and hasn't finished the task
            if (!this.green.autoMan && !this.green.autoFinished)
            {
                //Do for all green towers
                for (int t = 6; t < 11; t++)
                {
                    //If not already lit
                    if (t != 9)
                    {
                        if (this.fc.node[t].lightStatus != LightColor.green.ToString())
                        {
                            //Check to see if any buttons are pressed on each green tower
                            if (this.fc.InputState(t, greenButton) || this.fc.InputState(t, redButton))
                            {
                                this.fc.Light(t, LightColor.off, State.off);
                                this.fc.Light(t, LightColor.green, State.on);
                                this.green.autoTowerCount++;               //Increment tower count
                                this.green.autoLightScore++;
                                GameLog("Green Scored : Tower" + t);
                            }

                        }
                        //If green has lit four towers
                        if (this.green.autoTowerCount == 4)
                        {
                            for (t = 6; t < 11; t++)
                            {
                                //If not already lit
                                if (this.fc.node[t].lightMode != LightColor.green.ToString())
                                {
                                    this.fc.Light(t, LightColor.off, State.off);
                                    this.fc.Light(t, LightColor.green, State.on);   //Light them all green
                                }
                            }
                            this.green.autoFinished = true;            //Finish AutoMode
                            this.green.autoLightScore++;
                            GameLog("Green Bonus Light");
                            GameLog("Green Finished AutoMode");
                        }
                    }
                }
            }
            //If green is in mantonomous mode 
            else if (!this.green.autoFinished)
            {
                //Do for all green towers
                for (int t = 6; t < 11; t++)
                {
                    if (t != 9)
                    {
                        //If not already lit
                        if (this.fc.node[t].lightStatus != LightColor.green.ToString() && this.fc.node[t].lightStatus == LightColor.blue.ToString())
                        {
                            //Check to see if the green button is pressed 
                            if (!this.fc.InputState(t, redButton) && this.fc.InputState(t, greenButton))
                            {
                                this.fc.Light(t, LightColor.off, State.off);
                                this.fc.Light(t, LightColor.green, State.on);   //Light tower green
                                this.green.autoTowerCount++;
                                this.green.autoLightScore++;
                                GameLog("Green Score : Tower" + t);
                            }
                            //Else if the red button is pressed
                            else if (this.fc.InputState(t, redButton))
                            {
                                this.fc.Light(t, LightColor.off, State.off); ;   //Light tower off
                                this.green.autoTowerCount++;                       //Increament tower count
                                GameLog("Green Missed : Tower" + t);
                            }
                            //If all the towers have been pressed
                            if (this.green.autoTowerCount == 5)
                            {
                                this.green.autoFinished = true;            //Finish AutoMode
                                GameLog("Green Finished AutoMode");
                            }
                        }
                    }
                }
            }

            if (!this.red.fosScored)
            {
                this.red.autoFoSScore = Convert.ToInt32(this.fc.node[4].fosValue);
                if (this.red.autoTowerCount < 5 && this.red.autoFoSScore == 4)
                {
                    this.red.autoLightScore++;
                    this.red.autoTowerCount++;
                    this.red.fosScored = true;
                }
            }
            if (!this.green.fosScored)
            {
                this.green.autoFoSScore = Convert.ToInt32(this.fc.node[9].fosValue);
                if (this.green.autoTowerCount < 5 && this.green.autoFoSScore == 4)
                {
                    this.green.autoLightScore++;
                    this.green.autoTowerCount++;
                    this.green.fosScored = true;
                }
            }

            this.red.autoScore = (this.red.autoLightScore * 2) + (this.red.autoFoSScore * 2);
            this.green.autoScore = (this.green.autoLightScore * 2) + (this.green.autoFoSScore * 2);

            this.red.score = this.red.autoScore;
            this.green.score = this.green.autoScore;
        }

        public void MidMode()
        {
            //If it has been 5 seconds
            if (wamTime.Timer(5))
            {
                //Pick a new tower or turn it off
                getWamTower();
                GameLog("Random Tower = " + wamTowers[wamRndNum]);
            }

            //Read states on all towers
            foreach (Nodes nd in this.fc.node)
            {
                this.fc.GetNodeState(nd.id);
            }

            //If switch is red
            if (this.fc.InputState(2, redButton) && this.fc.node[2].lightStatus != LightColor.red.ToString())
            {
                //light tower red
                this.fc.Light(2, LightColor.red, State.on);
                GameLog("Red Scored Teeter Tower 2");
            }
            //If switch is green
            else if (this.fc.InputState(2, greenButton) && this.fc.node[2].lightStatus != LightColor.green.ToString())
            {
                //light tower green
                this.fc.Light(2, LightColor.green, State.on);
                GameLog("Green Scored Teeter Tower 2");
            }
            //Else neither is pressed
            else if (!this.fc.InputState(7, redButton) && !this.fc.InputState(7, greenButton))
            {
                if (this.fc.node[2].lightStatus != LightColor.off.ToString())
                {
                    //turn off tower
                    this.fc.Light(7, LightColor.off, State.off);
                    GameLog("Teeter Tower 7 - Off");
                }
            }

            //If switch is red
            if (this.fc.InputState(7, redButton) && this.fc.node[7].lightStatus != LightColor.red.ToString())
            {
                //light tower red
                this.fc.Light(7, LightColor.red, State.on);
                GameLog("Red Scored Teeter Tower 7");
            }
            //If switch is green
            else if (this.fc.InputState(7, greenButton) && this.fc.node[7].lightStatus != LightColor.green.ToString())
            {
                //light tower green
                this.fc.Light(7, LightColor.green, State.on);
                GameLog("Green Scored Teeter Tower 7");
            }
            //Else neither is pressed
            else if (!this.fc.InputState(7, redButton) && !this.fc.InputState(7, greenButton))
            {
                if (this.fc.node[7].lightStatus != LightColor.off.ToString())
                {
                    //turn off tower
                    this.fc.Light(7, LightColor.off, State.off);
                    GameLog("Teeter Tower 7 - Off");
                }
            }

            int randTower = wamTowers[wamRndNum];

            //If tower is not 15
            if (randTower != 15)
            {
                //Check Tower State
                //this.fc.GetNodeState(randTower);
                //If switch is red
                if (this.fc.InputState(randTower, redButton) && !this.fc.InputState(randTower, greenButton))
                {
                    //light tower red
                    this.fc.Light(randTower, LightColor.off, State.off);
                    this.fc.Light(randTower, LightColor.red, State.on);
                    wamTowers[wamRndNum] = 15;
                    wamTowersLeft--;
                    GameLog("Red Scored : Tower" + randTower);

                }
                //If switch is green
                else if (!this.fc.InputState(randTower, redButton) && this.fc.InputState(randTower, greenButton))
                {
                    //light tower green
                    this.fc.Light(randTower, LightColor.off, State.off);
                    this.fc.Light(randTower, LightColor.green, State.on);
                    wamTowers[wamRndNum] = 15;
                    wamTowersLeft--;
                    GameLog("Green Scored : Tower" + randTower);
                }
                //Both buttons are pressed
                else if (this.fc.InputState(randTower, redButton) && this.fc.InputState(randTower, greenButton))
                {
                    //turn off tower
                    this.fc.Light(randTower, LightColor.off, State.off);
                    wamRndNum = 6;
                    GameLog("Random Tower" + randTower + " - Off");
                }
            }

            this.red.manualLightScore = 0;
            this.green.manualLightScore = 0;
            for (int i = 1; i < 11; i++)
            {
                if (this.fc.node[i].lightStatus == LightColor.red.ToString()) this.red.manualLightScore++;
                else if (this.fc.node[i].lightStatus == LightColor.green.ToString()) this.green.manualLightScore++;
            }

            int gFoS1 = 0;
            int gFoS2 = 0;
            int rFoS1 = 0;
            int rFoS2 = 0;

            if (this.fc.node[4].fosColor == LightColor.red.ToString()) rFoS1 = this.fc.node[4].fosValue;
            else gFoS1 = this.fc.node[4].fosValue;

            if (this.fc.node[9].fosColor == LightColor.red.ToString()) rFoS2 = this.fc.node[9].fosValue;
            else gFoS2 = this.fc.node[9].fosValue;

            this.red.manualFoSscore = rFoS1 + rFoS2;
            GameLog("Red FoS = " + this.red.manualFoSscore);
            this.green.manualFoSscore = gFoS1 + gFoS2;
            GameLog("Green FoS = " + this.green.manualFoSscore);


            this.red.manScore = (this.red.manualLightScore * 2) + (this.red.manualFoSscore * 2);
            this.red.score = this.red.autoScore + this.red.manScore;
            this.green.manScore = (this.green.manualLightScore * 2) + (this.green.manualFoSscore * 2);
            this.green.score = this.green.autoScore + this.green.manScore;
        }

        public void EndMode()
        {
            //End Mode code here
        }

        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="text">Text as string</param>
        public void GameLog(string text)
        {
            string file = "\\Match " + matchNumber.ToString() + " - Log";
            string folder = "Matches\\" + "Match " + matchNumber.ToString();

            this.lw.WriteLog(text, file, folder);
        }

        //------------------------------------------------------------------------------------------------\\
        //Current year's game methods
        //------------------------------------------------------------------------------------------------\\

        private void getWamTower()
        {
            //Restart timer
            wamTime.elapsedTime.Restart();

            //If it is time to selecct a new tower
            if (wamSelect)
            {
                Array.Sort(wamTowers);
                wamRndNum = rndNum.Next(0, wamTowersLeft);
                wamSelect = false;
                this.fc.Light(wamTowers[wamRndNum], LightColor.blue, State.on);
                Console.WriteLine(wamRndNum + ":" + wamTowers[wamRndNum]);
            }
            //Else turn off the tower and wait
            else
            {
                if (wamTowers[wamRndNum] != 15)
                {
                    this.fc.Light(wamTowers[wamRndNum], LightColor.off, State.off);
                }
                wamSelect = true;
                wamRndNum = 6;
                Console.WriteLine(wamRndNum + ":" + wamTowers[wamRndNum]);
            }
        }

    }
}
