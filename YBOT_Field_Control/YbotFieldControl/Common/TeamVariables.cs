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
        public int reactor;
        public int speed;

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
            this.score = 0;
            this.autoCount = 0;
            this.finalScore = 0;
            this.penalty = 0;
            this.dq = false;
            this.matchResult = null;
            this.autoFinished = false;
            this.autoScore = 0;
            this.manScore = 0;
            this.endGameScore = 0;
            this.autoMan = false;
        }

        public void StoreTeamVariablesToSqlMatch (ref Match match) {
            if (teamColor.Equals ("red", StringComparison.OrdinalIgnoreCase)) {
                match.redScore = finalScore;
                match.redPenalty = penalty;
                match.redDq = dq ? 1 : 0;
                match.redResult = matchResult;
            } else {
                match.greenScore = finalScore;
                match.greenPenalty = penalty;
                match.greenDq = dq ? 1 : 0;
                match.greenResult = matchResult;
            }
        }

        public void StoreJointVariablesToSqlMatch (ref Match match) {
            
        }
    }
}
