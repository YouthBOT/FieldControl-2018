using System;

namespace YBotSqlWrapper
{
    public class Match
    {
        public int tournamentId;
        public int matchNumber;

        public int redTeam;
        public int redScore;
        public int redPenalty;
        public int redDq;
        public string redResult;

        /*
        public bool redAutoSwitchTurnedOff;
        public bool redAutoRightTowerPressed;
        public bool redAutoLeftTowerPressed;
        public int redRodsRemoved;
        public int redRodsReplaced;
        public int redReactorMultiplier;
        public int redReactorScore;
        public int redSpeedTowers;
        public bool redManualSwitchTurnedOn;
        public int redSpeedScore;
        */

        public int greenTeam;
        public int greenScore;
        public int greenPenalty;
        public int greenDq;
        public string greenResult;

        /*
        public bool greenAutoSwitchTurnedOff;
        public bool greenAutoRightTowerPressed;
        public bool greenAutoLeftTowerPressed;
        public int greenRodsRemoved;
        public int greenRodsReplaced;
        public int greenReactorMultiplier;
        public int greenReactorScore;
        public int greenSpeedTowers;
        public bool greenManualSwitchTurnedOn;
        public int greenSpeedScore;
        */

        public string teamVariables;
        public string redTeamVariables;
        public string greenTeamVariables;
    }
}
