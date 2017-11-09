using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YbotFieldControl
{
    public partial class GameControl
    {
        FieldControl fc;
        GameDisplay GD = new GameDisplay();
        Time time = new Time();
        LogWriter lw = new LogWriter();
        fileStructure fs = new fileStructure();
        StringBuilder logBuilder = new StringBuilder();

        private string filePath                     //Construct filePath to Node data
        {
            get
            {
                string path = fs.xmlFilePath;
                return path;
            }
        }

        private string xmlHeader                    //Construct xml Header
        {
            get
            {
                string header = fs.xmlHeader;
                return header;
            }
        }

        private int matchNumber = 0;

        private string timeOfDay = DateTime.Now.ToString("HH_mm_ss"); //Time of day stamp

        public Team red = new Team("red");            //Red team 
        public Team green = new Team("green");        //Green team

        public int autoModeTime = 30;   //Autonomous Mode time in secs
        public int manAutoTime = 0;    //Mantonomous Mode start time
        public int midModeTime = 90;   //Mid Mode time + automode time

        //Game flags
        public GameModes gameMode = GameModes.off;

        //------------------------------------------------------------------------------------------------\\
        //Current year's game variables
        //------------------------------------------------------------------------------------------------\\
        private Random randomNumber = new Random();   //Random Number
        private int minimumRandomTime = 1;
        private int maximumRandomTime = 7;
        private double startingTimeElapsed = 0;
        private bool speedRunActive = false;
        private int[,] buttonTowers = {{1, 6}, {3, 8}, {5, 10}};
        private int selectedTowerCombo = -1;
        private int blockingTime = 10;

        private bool redSwitchThrown = false;
        private bool greenSwitchThrown = false;
        private bool tower1Pressed = false;
        private bool tower5Pressed = false;
        private bool tower6Pressed = false;
        private bool tower10Pressed = false;

        // <TODO> determine scores
        public int autoSwitchThrowScore = 25;
        public int autoButtonPressScore = 50;

    }
}
