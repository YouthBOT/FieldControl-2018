using System;

namespace YBotSqlWrapper
{
    public class Match
    {
        public int tournamentId;
        public int matchNumber;

        public int greenTeam;
        public int greenScore;
        public int greenPenalty;
        public int greenDq;
        public string greenResult;

        public int redTeam;
        public int redScore;
        public int redPenalty;
        public int redDq;
        public string redResult;

        public int autoCornersTested;
        public int autoEmergencyCycled;
        public int autoSolarPanel;

        public int manSolarPanel1;
        public int manSolarPanel2;
        public int manualEmergencyCleared;
        public int rocketPosition;
        public int rockScore;
        public int rockWeight;
        public int rocketBonus;
    }
}
