using System;
using System.IO;

namespace YbotFieldControl
{
    //Field Communication modes
    public enum ComModes
    {
        none,
        canBus,
        xBee,
        wiFi,
    }

    public enum State
    {
        on,
        off,
    }

    public enum LightColor
    {
        off,
        red,
        green,
        yellow,
        blue,
        white,
        test,
        all = red|green|yellow|blue,
    }

    public enum GameModes
    {
        reset,
        ready,
        start,
        autonomous,
        mantonomous,
        manual,
        end,
        off,
        debug,
    }

    public struct Nodes
    {
        public int id;              //Node's ID number
        public ComModes type;       //Node's Communication Type
        public string address;      //Node's Address
        public byte reportRec;      //If a report is received it will be > than 0
        public byte outputStatus;    //Node's Output state each bit is a the state of the Output 1 on, 0 off
        public byte inputStatus;    //Node's Input state each bit is a the state of the input 1 on, 0 off
        public string lightStatus;  //Node's light color
        public string gameMode;     //Node's game mode status
        public string lightMode;    //Node's light mode 
        public byte byte6;          //Misc Information sent from Node
        public byte byte7;          //Misc Information sent from Node
        public bool scored;         //Flag true if scored, false if not
        //Network Testing Variables
        public int fromPC;          //Number of messages received from pc to command node
        public int toPC;            //Number of messages sent to the pc from the command node
        public int commandNodeMessagesSent;
        public int commandNodeMessagesReceived;
        public int nodeMessagesSent;
        public int nodeMessagesReceived;


    }

    public class CommonVariables
    {
        public int bell = 2;                   //Bell Output
        public int buzzer = 3;                 //Buzzer Output
        public int controlBoard = 0;           //Control Board tower number
        public int canControlID = 31;

        public int redTeam_Node = 3;           	//Red team's controller Node    
        public int greenTeam_Node = 8;         	//Green teams' controller Node
        public int transmitterOutput = 5;       //Controller Output number - Enable / Disable
        public int auto_driverOutput = 6;		//Autonomous Mode Output = Auto / Manual

    }

	public class fileStructure
	{
		public string filePath {
			get {
				if (Environment.OSVersion.Platform == PlatformID.Unix) {
					return Path.Combine (Environment.GetEnvironmentVariable ("HOME"), "Desktop/YBOT Field Files/");
				} 

				return Environment.ExpandEnvironmentVariables (@"%USERPROFILE%\Desktop\YBOT Field Files\");
			}
		}

		public string setupFilePath {
			get {
				if (Environment.OSVersion.Platform == PlatformID.Unix) {
					return Path.Combine (Environment.GetEnvironmentVariable ("HOME"), "Desktop/YBOT Field Files/Setup/");
				}

				return Environment.ExpandEnvironmentVariables (@"%USERPROFILE%\Desktop\YBOT Field Files\Setup");
			}
		}

		public string xmlFilePath {
			get {
				if (Environment.OSVersion.Platform == PlatformID.Unix) {
					return Path.Combine (Environment.GetEnvironmentVariable ("HOME"), "Desktop/YBOT Field Files/Setup/YBOT_Nodes.xml");
				}

				return Environment.ExpandEnvironmentVariables (@"%USERPROFILE%\Desktop\YBOT Field Files\Setup\YBOT_Nodes.xml");
			}
		}

		public string xmlHeader = "YBOT_Nodes";
    }
}
