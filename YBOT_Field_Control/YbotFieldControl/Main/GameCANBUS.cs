using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace YBOT_Field_Control_2016
{
    public class Game
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
        public GameModes gameMode = GameModes.off;
        public bool practiceMode = false;   //True when game is in practice mode

        //------------------------------------------------------------------------------------------------\\
        //Current year's game variables
        //------------------------------------------------------------------------------------------------\\

        private int autoballValue = 5;
        private int autoMax = 4;
        public int[] wamTowers = { 1, 3, 5, 6, 8, 10, 15 };
        private Random rndNum = new Random();
        private bool wamSelect = true;
        private int wamRndNum = 0;
        private int wamTowersLeft = 6;
        private Time wamTime = new Time();

        #endregion

        #region Constructors

        public Game()
        {
            this.fc = new Field_Control();
        }

        public Game(Field_Control _fc)
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

            //Reset Wam towers
            wamTowers[0] = 1;
            wamTowers[1] = 3;
            wamTowers[2] = 5;
            wamTowers[3] = 6;
            wamTowers[4] = 8;
            wamTowers[5] = 10;
            wamTowers[6] = 15;

            //wamTowers[0] = 1;
            //wamTowers[1] = 10;
            //wamTowers[2] = 15;
            //wamTowers[3] = 15;
            //wamTowers[4] = 15;
            //wamTowers[5] = 15;
            //wamTowers[6] = 15;

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

            //Set to Automode
            gameMode = this.fc.ChangeGameMode(GameModes.autonomous);

            Thread.Sleep(200);

            //Turn on Robot Transmiters
            this.fc.RobotTransmitters("both", State.on, State.on);
            GameLog("Transmitters On");

            GameLog("Game Start");

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

            //Set to Automode
            gameMode = this.fc.ChangeGameMode(mode);

            Thread.Sleep(200);

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
            //Do loop until the game is over or stopped
            if (gameMode != GameModes.end)
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
                        this.fc.switchMode = false;
                        this.fc.AllFieldLights(LightColor.off, State.off);
                        this.fc.RobotTransmitters("both", State.off, State.on); //Turn on transmitter to Manual Mode
                        this.fc.RingBell();                        //Ring bell
                        GameLog("AutoMode Over");                   //Update Log
                        GameLog("Transmitters ON");                 //Update Log

                        this.fc.ClearNodeState();

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
                    //Place 3rd mode here
                }

                Application.DoEvents();         //Do window's stuff
            }
        }

        public void AutoMode()
        {
            //Read states on all towers
            this.fc.UpdateNodeState();

            //Red
            //If Red is still in auto and hasn't finished the task
            if (!this.red.autoFinished)
            {
                int maxAutoTowers = 4;
                if (this.red.autoMan) maxAutoTowers = 5;

                //Do for all red towers
                for (int t = 1; t < 6; t++)
                {
                    //If tower is not score and the tower light is red
                    if (!this.fc.node[t].scored && (this.fc.node[t].lightStatus == LightColor.red.ToString()))
                    {
                        this.red.autoTowerCount++;          //Score tower red
                        this.red.autoLightScore++;          //One more light is scored
                        this.fc.node[t].scored = true;      //Tower is scored
                        GameLog("Red Scored : Tower " + t);  //Record
                        Console.WriteLine("Red = " + this.red.autoTowerCount);
                    }
                }
                //If tower count is at the max
                if (this.red.autoTowerCount >= maxAutoTowers)
                {
                    this.red.autoFinished = true;       //Finish AutoMode
                    GameLog("Red Finished AutoMode ");   //Record it

                    //If not in mantonomous mode
                    if (!this.red.autoMan)
                    {
                        //Do for all red towers
                        for (int t = 1; t < 6; t++)
                        {
                            //if tower is not scored and light isn't red
                            if (!this.fc.node[t].scored && (this.fc.node[t].lightStatus != LightColor.red.ToString()))
                            {
                                this.fc.Light(t, LightColor.red, State.on); //Turn on the light
                                this.red.autoTowerCount++;                  //Increase the tower count
                                this.red.autoLightScore++;                  //Score for red
                                this.fc.node[t].scored = true;              //Set tower scored
                                GameLog("Red Scored : Tower " + t);          //Record it
                                GameLog("Red Bonus Light : Tower " + t);     //Recored it
                            }
                        }
                    }
                }
            }


            //Repeat for green
            //If green is still in auto and hasn't finished the task
            if (!this.green.autoFinished)
            {
                int maxAutoTowers = 4;
                if (this.green.autoMan) maxAutoTowers = 5;

                //Do for all this.green towers
                for (int t = 6; t < 11; t++)
                {
                    if (!this.fc.node[t].scored && (this.fc.node[t].lightStatus == LightColor.green.ToString()))
                    {
                        this.green.autoTowerCount++;
                        this.green.autoLightScore++;
                        this.fc.node[t].scored = true;
                        GameLog("Green Scored : Tower " + t);
                        Console.WriteLine("Green = " + this.green.autoTowerCount);
                    }
                }
                if (this.green.autoTowerCount >= maxAutoTowers)
                {
                    this.green.autoFinished = true;
                    GameLog("Green Finished AutoMode");

                    if (!this.green.autoMan)
                    {
                        for (int t = 6; t < 11; t++)
                        {
                            if (!this.fc.node[t].scored && (this.fc.node[t].lightStatus != LightColor.green.ToString()))
                            {
                                this.fc.Light(t, LightColor.green, State.on); //Turn on the light
                                this.green.autoTowerCount++;
                                this.green.autoLightScore++;
                                this.fc.node[t].scored = true;
                                GameLog("Green Scored : Tower " + t);
                                GameLog("Green Bonus Light : Tower " + t);
                            }
                        }
                    }
                }
            }

            //Score FoS Towers
            int redFos = Convert.ToInt32(this.fc.node[4].fosValue);
            int greenFoS = Convert.ToInt32(this.fc.node[9].fosValue);

            this.red.autoFoSScore = redFos * (redFos+1);
            this.green.autoFoSScore = greenFoS * (greenFoS+1);

            //Update Scores
            this.red.autoScore = (this.red.autoLightScore * 2) + (this.red.autoFoSScore);
            this.green.autoScore = (this.green.autoLightScore * 2) + (this.green.autoFoSScore);

            this.red.score = this.red.autoScore;
            this.green.score = this.green.autoScore;


            GameLog
                ("Red" +
                "\n\t" + "Auto Lights = " + this.red.autoLightScore +
                "\n\t" + "Auto FoS Meter = " + this.red.autoFoSScore+
                "\n\t" + "Auto Man Mode = " + this.red.autoMan+
                "\n\t" + "Auto Finished = " + this.red.autoFinished+
                "\n\t" + "Auto Score = " + this.red.autoScore
                );

            GameLog
                ("Green" +
                "\n\t" + "Auto Lights = " + this.green.autoLightScore +
                "\n\t" + "Auto FoS Meter = " + this.green.autoFoSScore +
                "\n\t" + "Auto Man Mode = " + this.green.autoMan +
                "\n\t" + "Auto Finished = " + this.green.autoFinished +
                "\n\t" + "Auto Score = " + this.green.autoScore
                );

        }

        public void MidMode()
        {
            //If it has been 5 seconds
            if (wamTime.Timer(5))
            {
                //Pick a new tower or turn it off
                getWamTower();
               
            }

            //Reset light scores
            red.manualLightScore = 0;
            green.manualLightScore = 0;

            //Get Tower Status
            this.fc.UpdateNodeState();

            //Repeat for all the towers
            for (int i = 1; i < 11; i++)
            {
                if (this.fc.node[i].lightStatus == LightColor.red.ToString())           //If tower is red
                {
                    red.manualLightScore++;                                             //Score tower red

                    if (i == wamTowers[wamRndNum])                                      //If tower is the selected tower 
                    {
                        GameLog("WaM Tower: " + wamTowers[wamRndNum] + "Scored Red");
                        wamTowers[wamRndNum] = 15;                                      //Remove it form the list
                        wamTowersLeft--;                                                //one less tower to select
                        
                    }
                }
                else if (this.fc.node[i].lightStatus == LightColor.green.ToString())    //If tower is green
                {
                    green.manualLightScore++;                                           //Score tower green

                    if (i == wamTowers[wamRndNum])                                      //If tower is the selected tower
                    {
                        GameLog("WaM Tower: " + wamTowers[wamRndNum] + "Scored Green");
                        wamTowers[wamRndNum] = 15;                                      //Remove it from the list
                        wamTowersLeft--;                                                //One less tower to select
                        
                    }
                }
            }


            //Reset FoS variables
            int gFoS1 = 0;
            int gFoS2 = 0;
            int rFoS1 = 0;
            int rFoS2 = 0;

            //Get tower 4 FoS Score
            if (this.fc.node[4].fosColor == LightColor.red.ToString()) rFoS1 = this.fc.node[4].fosValue;
            else if(this.fc.node[4].fosColor == LightColor.green.ToString()) gFoS1 = this.fc.node[4].fosValue;

            //Get tower 9 FoS Score
            if (this.fc.node[9].fosColor == LightColor.red.ToString()) rFoS2 = this.fc.node[9].fosValue;
            else if (this.fc.node[9].fosColor == LightColor.green.ToString()) gFoS2 = this.fc.node[9].fosValue;

            this.red.manualFoSscore = (rFoS1 * (rFoS1+1)) + (rFoS2 * (rFoS2 + 1));                                                        //Red FoS score
            this.green.manualFoSscore = (gFoS1 * (gFoS1 + 1)) + (gFoS2 * (gFoS2 + 1));                                                      //Green FoS score                                                   

            this.red.manScore = (this.red.manualLightScore * 2) + (this.red.manualFoSscore);            //Red Man Score
            this.red.score = this.red.autoScore + this.red.manScore;                                        //Red Score
            this.green.manScore = (this.green.manualLightScore * 2) + (this.green.manualFoSscore);      //Green man score
            this.green.score = this.green.autoScore + this.green.manScore;                                  //Green Score


            GameLog
                ("Red" +
                "\n\t" + "Manual Lights = " + this.red.manualLightScore +
                "\n\t" + "Manual FoS Meter = " + this.red.manualFoSscore +
                "\n\t" + "Manual Score = " + this.red.manScore
                );

            GameLog
                ("Green" +
                "\n\t" + "Manual Lights = " + this.green.manualLightScore +
                "\n\t" + "Manual FoS Meter = " + this.green.manualFoSscore +
                "\n\t" + "Manual Score = " + this.green.manScore
                );
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
            string file = "\\Match " + matchNumber.ToString() + " - Log";       //File name
            string folder = "Matches\\" + "Match " + matchNumber.ToString();    //Folder path

            this.lw.WriteLog(text, file, folder);                               //Write log
        }

        //------------------------------------------------------------------------------------------------\\
        //Current year's game methods
        //------------------------------------------------------------------------------------------------\\

        private void getWamTower()
        {
            //If it is time to selecct a new tower
            if (wamSelect)                                                              //If it is time to select a new tower
            {
                if (wamTowersLeft < 0) wamTowersLeft = 0;                               //Towers Left is negative set it to zero
                Array.Sort(wamTowers);                                                  //Sort Array so the smaller numbers are first
                wamRndNum = rndNum.Next(0, wamTowersLeft);                              //Pick a random number between 0 and what towers are left
                wamSelect = false;                                                      //Switch flag off
                GameLog("Random Tower = " + wamTowers[wamRndNum]);

                if (wamTowers[wamRndNum] != 15)                                         //If the selected tower is a real tower number
                {
                    //this.fc.Light(wamTowers[wamRndNum], LightColor.blue, State.on);     //Turn the light on blue
                    this.fc.SendMessage(wamTowers[wamRndNum], "7,1");
                    Console.WriteLine(wamRndNum + " : " + wamTowers[wamRndNum]);        //Let us know what happened
                    wamTime.elapsedTime.Restart();                                      //Reset selection timer
                }
            }
            //Else turn off the tower and wait
            else
            {
                int t = wamTowers[wamRndNum];
                if (t != 15)                                         //If the selected tower is a real tower number
                {
                    this.fc.SendMessage(wamTowers[wamRndNum], "7,0");
                    //this.fc.GetNodeState(t);
                    //Thread.Sleep(100);
                    //if (this.fc.node[t].lightStatus == LightColor.blue.ToString())
                    //    this.fc.Light(wamTowers[wamRndNum], LightColor.off, State.off);     //turn off the light
                }
                wamSelect = true;                                                       //Set flag for next time
                wamRndNum = 6;                                                          //Set index to a non tower
                Console.WriteLine(wamRndNum + " : " + wamTowers[wamRndNum]);            //let us know what happened
                wamTime.elapsedTime.Restart();                                          //Reset selection timer
            }
        }

        public void UpDateScores()
        {
            this.fc.GetNodeState();
            Thread.Sleep(150);
            this.fc.UpdateNodeState();
            GameLog("UPDATE SCORE");
            int redLights = 0;
            int greenLights = 0;
            int redFoS = 0;
            int greenFoS = 0;

            switch (gameMode)
            {
                case GameModes.autonomous:
                    redLights = 0;
                    greenLights = 0;
                    for(int i = 1; i<11; i++)
                    {
                        if (this.fc.node[i].lightStatus == LightColor.red.ToString()) redLights++;
                        else if (this.fc.node[i].lightStatus == LightColor.green.ToString()) greenLights++;
                    }
                    //Score FoS Towers
                    redFoS = Convert.ToInt32(this.fc.node[4].fosValue);
                    greenFoS = Convert.ToInt32(this.fc.node[9].fosValue);

                    this.red.autoFoSScore = redFoS * (redFoS + 1);
                    this.green.autoFoSScore = greenFoS * (greenFoS + 1);

                    //Update Scores
                    this.red.autoScore = (this.red.autoLightScore * 2) + (this.red.autoFoSScore);
                    this.green.autoScore = (this.green.autoLightScore * 2) + (this.green.autoFoSScore);

                    this.red.score = this.red.autoScore;
                    this.green.score = this.green.autoScore;

                    GameLog("---------------------------------");
                    break;

                case GameModes.mantonomous:
                    redLights = 0;
                    greenLights = 0;
                    for (int i = 1; i < 11; i++)
                    {
                        if (this.fc.node[i].lightStatus == LightColor.red.ToString()) redLights++;
                        else if (this.fc.node[i].lightStatus == LightColor.green.ToString()) greenLights++;
                    }
                    //Score FoS Towers
                    redFoS = Convert.ToInt32(this.fc.node[4].fosValue);
                    greenFoS = Convert.ToInt32(this.fc.node[9].fosValue);

                    this.red.autoFoSScore = redFoS * (redFoS + 1);
                    this.green.autoFoSScore = greenFoS * (greenFoS + 1);

                    //Update Scores
                    this.red.autoScore = (this.red.autoLightScore * 2) + (this.red.autoFoSScore);
                    this.green.autoScore = (this.green.autoLightScore * 2) + (this.green.autoFoSScore);

                    this.red.score = this.red.autoScore;
                    this.green.score = this.green.autoScore;

                    GameLog("---------------------------------");
                    break;

                case GameModes.manual:
                    MidMode();
                    GameLog("---------------------------------");
                    break;

                default:
                    break;
            }
        }

    }
}
