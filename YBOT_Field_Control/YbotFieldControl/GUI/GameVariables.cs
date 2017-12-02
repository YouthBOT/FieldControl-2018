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
        private double startingTimeElapsed = 0;
		private readonly int[,] BUTTON_TOWERS = {{1, 6}, {3, 8}, {5, 10}};
		private const int TOWER_COMBO_LENGTH = 3;
        private int selectedTowerCombo = -1;
        private const int BLOCKING_TIME = 10;
		private const int SWITCH_UP_INPUT = 3;
		private const int SWITCH_DOWN_INPUT = 4;

        private bool autoRedSwitchTurnedOff = false;
        private bool autoGreenSwitchTurnedOff = false;
		private bool autoGreenPressedOne = false;
		private bool autoGreenPressedTwo = false;
		private bool autoRedPressedOne = false;
		private bool autoRedPressedTwo = false;
        private bool autoTower1Pressed = false;
		private bool autoTower3Pressed = false;
        private bool autoTower5Pressed = false;
        private bool autoTower6Pressed = false;
		private bool autoTower8Pressed = false;
        private bool autoTower10Pressed = false;
        private bool speedRedSwitchTurnedOn = false;
        private bool speedGreenSwitchTurnedOn = false;
        private bool redSwitchTurnedOn = false;
        private bool greenSwitchTurnedOn = false;

        // <TODO> determine scores
        public const int AUTO_SWITCH_TURNED_OFF_SCORE = 75;
        public const int AUTO_FIRST_BUTTON_PRESSED_SCORE = 50;
        public const int AUTO_SECOND_BUTTON_PRESSED_SCORE = 25;
        public const int SPEED_BUTTON_PRESSED_SCORE = 10;
        public const int SPEED_SWITCH_TURNED_ON_SCORE = 50;
    }
}
