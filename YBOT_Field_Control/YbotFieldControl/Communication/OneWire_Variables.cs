using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using DalSemi.OneWire.Adapter;
using DalSemi.OneWire;

namespace YBOT_Field_Control_2016
{
    public partial class OneWire
    {
        #region Program Variables

        public int red = 1;                    //Red Relay#
        public int green = 2;                  //Green Relay#
        public int yellow = 3;                 //Yello Relay#
        public int blue = 4;                   //Blue Relay#

        public int input1 = 7;                 //Input1 Pin#
        public int input2 = 8;                 //Input2 Pin#

        private int maxNodes = 15;
        private int maxTowerNum = 10;       //Max number of game towers including control board
        private int maxNodeNum = 10;

        private LogWriter lw = new LogWriter();
        private fileStructure fs = new fileStructure();
        CommonVariables cv = new CommonVariables();
        private string path
        {
            get
            {
                string filePath = fs.filePath;
                return filePath;
            }
            set
            {

            }
        }
        public PortAdapter adapter = null;                                  //1 wire adapter

        /// <summary>
        /// state = Last Known State of Node (written or read)
        /// latchState = Current Latch State of Node
        /// writtenState = Last Written State of Node
        /// readState = Last Read State of Node
        /// </summary>
        public struct addList
        {
            public byte[] address;
            public byte state;
            public byte latchState;
            public bool selected;
            public byte writtenState;
            public byte readState;
        };

        public addList[] device;                          // device array
        public addList[] owNode;                           // Node array
        public addList[] coupler = new addList[4];
        public bool debug = false;                                           //Used to log debug reports

        #endregion
    }
}
