using System;
using YBotSqlWrapper;

namespace YbotFieldControl
{
    public class Team
    {
        //public Variables
        private string _teamColor;        //Team color "red" or "green"
        public string teamColor
        {
            get
            {
                return _teamColor;
            }
        }

        private int _score;               //Team's score
        public int score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        private int _autoCount;           //Autonomous Mode Counter
        public int autoCount
        {
            get
            {
                return _autoCount;
            }
            set
            {
                _autoCount = value;
            }
        }

        private int _finalScore;          //Team's final score
        public int finalScore
        {
            get
            {
                return _finalScore;
            }
            set
            {
                _finalScore = value;
            }
        }

        private int _penalty;             //Penalty amount
        public int penalty
        {
            get
            {
                return _penalty;
            }
            set
            {
                _penalty = value;
            }
        }

        private bool _dq;                 //Team DQ flag
        public bool dq
        {
            get
            {
                return _dq;
            }
            set
            {
                _dq = value;
            }
        }

        private string _matchResult;      //Match Result string
        public string matchResult
        {
            get
            {
                return _matchResult;
            }
            set
            {
                _matchResult = value;
            }
        }

        private bool _autoFinished;       //Autonomous mode finished flag
        public bool autoFinished
        {
            get
            {
                return _autoFinished;
            }
            set
            {
                _autoFinished = value;
            }
        }

        private int _autoScore;           //Autonomous mode score
        public int autoScore
        {
            get
            {
                return _autoScore;
            }
            set
            {
                _autoScore = value;
            }
        }

        private int _manScore;            //Middle round score
        public int manScore
        {
            get
            {
                return _manScore;
            }
            set
            {
                _manScore = value;
            }
        }

        private int _endGameScore;        //End of game score
        public int endGameScore
        {
            get
            {
                return _endGameScore;
            }
            set
            {
                _endGameScore = value;
            }
        }

        private bool _autoMan;            //Mantonomous Flag
        public bool autoMan
        {
            get
            {
                return _autoMan;
            }
            set
            {
                _autoMan = value;
            }
        }

        //------------------------------------------------------------------------------------------------\\
        //Current year's game variables
        //------------------------------------------------------------------------------------------------\\
        public bool autoSwitchTurnedOff;
        public bool autoRightTowerPressed;
        public bool autoLeftTowerPressed;
        public int rodsRemoved;
        public int rodsReplaced;
        public int reactorMultiplier;
        public int reactorScore;
        public int speedTowers;
        public bool manualSwitchTurnedOn;
        public int speedScore;

        //Team color 
        public Team(string teamColor)
        {
            _teamColor = teamColor;
        }

        /// <summary>
        /// Resets variable
        /// </summary>
        public void Reset()
        {
            score = 0;
            autoCount = 0;
            finalScore = 0;
            penalty = 0;
            dq = false;
            matchResult = null;
            autoFinished = false;
            autoScore = 0;
            manScore = 0;
            endGameScore = 0;
            autoMan = false;

            autoSwitchTurnedOff = false;
            autoRightTowerPressed = false;
            autoLeftTowerPressed = false;
            rodsRemoved = 0;
            rodsReplaced = 0;
            reactorMultiplier = 1;
            reactorScore = 0;
            speedTowers = 0;
            manualSwitchTurnedOn = false;
            speedScore = 0;
    }

        public void StoreTeamVariablesToSqlMatch (
            ref Match match, 
            string teamVariableString, 
            string teamVariables=null)
        {
            if (teamColor.Equals ("red", StringComparison.OrdinalIgnoreCase)) {
                match.redScore = finalScore;
                match.redPenalty = penalty;
                match.redDq = dq ? 1 : 0;
                match.redResult = matchResult;

                match.redTeamVariables = teamVariableString;
                if (teamVariables != null) {
                    match.teamVariables = teamVariables;
                }
            } else {
                match.greenScore = finalScore;
                match.greenPenalty = penalty;
                match.greenDq = dq ? 1 : 0;
                match.greenResult = matchResult;

                match.greenTeamVariables = teamVariableString;
                if (teamVariables != null) {
                    match.teamVariables = teamVariables;
                }
            }
        }

        public void StoreJointVariablesToSqlMatch (ref Match match) {
            
        }
    }
}
