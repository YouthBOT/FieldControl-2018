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
        private Random randomTowerNumber = new Random();   //Random Number
        private Random randomTeamNumber = new Random(); 
        private double startingTimeElapsed = 0;
		private readonly int[,] BUTTON_TOWERS = {{1, 6}, {3, 8}, {5, 10}};
		private const int TOWER_COMBO_LENGTH = 3;
        private int redTower = -1;
        private int greenTower = -1;
        private int selectedTowerCombo = -1;
        private const int BLOCKING_TIME = 10;
		private const int SWITCH_UP_INPUT = 2;
		private const int SWITCH_DOWN_INPUT = 3;

        private bool speedMode = false;
        private bool redSwitchTurnedOn = false;
        private bool greenSwitchTurnedOn = false;
        private bool speedTeamCheckToggle = false;
        private bool teamScored = false;

        public const int AUTO_SWITCH_TURNED_OFF_SCORE = 75;
        public const int AUTO_RIGHT_TOWER_PRESSED_SCORE = 50;
        public const int AUTO_LEFT_TOWER_PRESSED_SCORE = 25;
        public const int SPEED_BUTTON_PRESSED_SCORE = 10;
        public const int SPEED_SWITCH_TURNED_ON_SCORE = 50;

        private class SchoolStandings
        {
            public int id;
            public string name;
            public int wins;
            public int loses;
            public int ties;
            public double average;
            public double highest;
            public int matchesPlayed;

            public SchoolStandings () {
                id = 0;
                name = null;
                wins = 0;
                loses = 0;
                ties = 0;
                average = 0d;
                highest = 0d;
                matchesPlayed = 0;
            }
        }

        private bool generatedSeedBracketMatches = false;
    }
}
