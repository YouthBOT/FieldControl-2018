using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data;

namespace YbotFieldControl
{
    public class FieldControl
    {
        #region Variables
        private Canbus cb;
        private fileStructure fs = new fileStructure();
        private LogWriter lw = new LogWriter();
        public Nodes[] node;
        private DataSet nodeDS = new DataSet();
        private DataGrid nodeGrid = new DataGrid();
        private CommonVariables cv = new CommonVariables();

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
        public bool switchMode = false;
        bool canbusPresent = false;     //Set flag false
        #endregion

        #region Set Up Methods
        /// <summary>
        /// Reads XML File and sets up communcations
        /// </summary>
        public void SetUp()
        {
            DataTable nodeTable = null;     //Clear table

            try
            {
                this.nodeDS.Clear();                                            //Clear data base
                nodeDS.ReadXml(filePath, XmlReadMode.ReadSchema);               //Read Node XML File into the Data Set
                nodeTable = nodeDS.Tables[xmlHeader];                           //Fill table with new data
                this.node = new Nodes[nodeTable.Rows.Count];                    //Create node array
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            cb = null;                                  //Despose of canbus object
            cb = new Canbus();      //New Canbus object

            if (nodeTable != null)
            {
                //Fill node array with data from xml file
                foreach (DataRow row in nodeTable.Rows)
                {
                    if (row[0] != DBNull.Value)
                    {
                        try
                        {
                            int nodeID = Convert.ToInt16(row["Node_ID"]);
                            string nodeType = row["Node_Type"].ToString();
                            this.node[nodeID].id = nodeID;

                            switch (nodeType)
                            {
                                case "CANBUS":
                                    this.node[nodeID].type = ComModes.canBus;
                                    canbusPresent = true;
                                    break;

                                case "XBee":
                                    this.node[nodeID].type = ComModes.xBee;
                                    break;

                                case "WiFi":
                                    this.node[nodeID].type = ComModes.wiFi;
                                    break;

                                default:
                                    MessageBox.Show("Not a valid type");
                                    this.node[nodeID].type = ComModes.none;
                                    break;
                            }

                            this.node[nodeID].address = GetNodeAddress(nodeID);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                }
            }

            //Start canbus if we have canbus nodes
            if (canbusPresent)
            {
                this.cb.CanMessageReceived += new Canbus.CanReceivedEvent(UpdateNodeState);
                this.cb.FindPort();
            }
        }

        /// <summary>
        /// Starts Field Communications
        /// </summary>
        public void StartUp()
        {
            this.cb.Startup();
        }

        /// <summary>
        /// Starts Field Communications in given mode
        /// </summary>
        /// <param name="mode">Communcation Mode Value</param>
        public void StartUp(ComModes mode)
        {
            switch (mode)
            {
                case ComModes.canBus:
                    this.cb.Startup();
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// Starts Field Communication in given mode on given port
        /// </summary>
        /// <param name="mode">Communication Mode Value</param>
        /// <param name="port">ComPort</param>
        public void StartUp(ComModes mode, string port)
        {
            switch (mode)
            {
                case ComModes.canBus:
                    this.cb.Startup(port);
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Starts Field Communication in given mode on given port
        /// </summary>
        /// <param name="mode">Communication Mode Value</param>
        /// <param name="port">ComPort</param>
        public void StartUp(ComModes mode, string port, bool dtr, bool rts)
        {
            switch (mode)
            {
                case ComModes.canBus:
                    this.cb.dtr = dtr;
                    this.cb.rts = rts;
                    this.cb.Startup(port);
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Shuts down field communcations
        /// </summary>
        public void ShutDown()
        {
            cb.Shutdown();
        }

        /// <summary>
        /// Shuts down field communication on given Mode
        /// </summary>
        /// <param name="mode">Communication Mode Value</param>
        public void ShutDown(ComModes mode)
        {
            switch (mode)
            {
                case ComModes.canBus:
                    this.cb.Shutdown();
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Shuts down field communication on give mode on given port
        /// </summary>
        /// <param name="mode">Communication Mode Value</param>
        /// <param name="port">ComPort</param>
        public void ShutDown(ComModes mode, string port)
        {
            switch (mode)
            {
                case ComModes.canBus:
                    this.cb.Shutdown(port);
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }


        }

        /// <summary>
        /// Tests if Node is connected to network
        /// </summary>
        /// <param name="_nodeID">Node Address</param>
        /// <returns></returns>
        public bool TestConnections(int _nodeID)
        {
            ComModes mode = new ComModes();
            mode = this.node[_nodeID].type;

            switch (mode)
            {
                case ComModes.canBus:
                    int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                    this.cb.TestConnection(nodeAddress);
                    Thread.Sleep(30);
                    if (this.node[_nodeID].reportRec > 0) return true;
                    else return false;

                case ComModes.xBee:

                    return false;

                case ComModes.wiFi:

                    return false;

                default:
                    return false;
            }

        }

        /// <summary>
        /// Gets node's address value
        /// </summary>
        /// <param name="_nodeID">Node's ID number</param>
        /// <returns>Node's ID Value</returns>
        public string GetNodeAddress(int _nodeID)
        {
            ComModes mode = new ComModes();
            mode = this.node[_nodeID].type;
            string address = "No Address";

            switch (mode)
            {
                case ComModes.canBus:
                    if (this.node[_nodeID].id == 0) address = this.cb.commandNode.ToString();
                    else if (this.node[_nodeID].id > 10)
                    {
                        int temp = this.node[_nodeID].id + 10;
                        address = temp.ToString();
                    }
                    else address = this.node[_nodeID].id.ToString();
                    return address;

                case ComModes.xBee:

                    return address;

                case ComModes.wiFi:

                    return address;

                default:
                    return address;
            }
        }

        /// <summary>
        /// Set's node's mode to off
        /// </summary>
        public void FieldAllOff() {
            if (canbusPresent) {
                cb.Send("0,7,0,0,");
                Thread.Sleep(20);
                cb.ChangeGameMode(0, GameModes.off);
                Thread.Sleep(20);

                AllFieldLights (LightColor.off, State.off);
            }

            ClearNodeState();
        }
        #endregion

        #region Light Controls

        /// <summary>
        /// Sets Node's Ring to given color
        /// </summary>
        /// <param name="_nodeID">Node's ID Vaule</param>
        /// <param name="_color1">Color Value</param>
        /// <param name="_color2">Color Value</param>
        /// <param name="_color3">Color Value</param>
        /// <param name="_color4">Color Value</param>
        public void Light(int _nodeID, LightColor _color1, LightColor _color2, LightColor _color3, LightColor _color4)
        {
            string colorState = string.Format("{0}|{1}|{2}|{3}", _color1.ToString(), _color2.ToString(), _color3.ToString(), _color4.ToString());
            node[_nodeID].lightStatus = colorState;
            ComModes mode = new ComModes();
            mode = node[_nodeID].type;

            switch (mode)
            {
                case ComModes.canBus:
                    int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                    this.cb.Light(nodeAddress, _color1, _color2, _color3, _color4);
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Sets Node to Given Color Value and State
        /// </summary>
        /// <param name="_nodeID">Node's ID Value</param>
        /// <param name="_color">Color Value</param>
        /// <param name="_state">on, off</param>
        public void Light(int _nodeID, LightColor _color) {
            ComModes mode = new ComModes();
            mode = node[_nodeID].type;

            switch (mode) {
                case ComModes.canBus:
                    int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                    cb.Light(nodeAddress, _color);
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Sets All Nodes to Given Color and State
        /// </summary>
        /// <param name="_color">Color Value</param>
        /// <param name="_state">on, off</param>
        public void AllFieldLights(LightColor _color, State _state) {
            if (canbusPresent) {
                cb.Light (0, _color);
            }
        }

        /// <summary>
        /// Sets all node's rings on
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        public void LightTest(int _nodeID)
        {
            this.node[_nodeID].lightStatus = "Test";

            ComModes mode = new ComModes();
            mode = this.node[_nodeID].type;

            switch (mode)
            {
                case ComModes.canBus:
                    int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                    this.cb.LightTest(nodeAddress);
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Sets all node's rings on
        /// </summary>
        public void LightTest()
        {
            if (canbusPresent) this.cb.LightTest(0);
        }

        #endregion

        #region Output Controls
        /// <summary>
        /// Sets Node's Outputs to given state
        /// </summary>
        /// <param name="_nodeID">Node's ID</param>
        /// <param name="_state">State of all Outputs(0 all off, 255 all on)</param>
        public void SetOutputState(int _nodeID, byte _state)
        {
            this.node[_nodeID].outputStatus = _state;                                      //Update this.node status

            ComModes mode = new ComModes();
            mode = this.node[_nodeID].type;

            switch (mode)
            {
                case ComModes.canBus:
                    int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                    this.cb.SetOutputState(nodeAddress, _state);                     //Send Node Output State
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Sets one of the Node's Outputs to a given state
        /// </summary>
        /// <param name="_nodeID">Node's ID</param>
        /// <param name="_OutputNum">Output to switch</param>
        /// <param name="_state">on or off</param>
        public void SetOutputState(int _nodeID, int _OutputNum, State _state)
        {
            byte newState;
            int Output = _OutputNum - 1;                                              //Get Output Location
            byte currentSate = this.node[_nodeID].outputStatus;                      //Get Current State
            if (_state == State.on) newState = (byte)((1 << Output) | currentSate);  //Update State
            else newState = (byte)(~(1 << Output) & currentSate);
            this.node[_nodeID].outputStatus = newState;                              //Update this.node status

            ComModes mode = new ComModes();
            mode = this.node[_nodeID].type;

            switch (mode)
            {
                case ComModes.canBus:
                    int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                    this.cb.SetOutput(nodeAddress, _OutputNum, _state);                   //Send Output request
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Updaters
        /// <summary>
        /// Update All Nodes state
        /// </summary>
        public void UpdateNodeState(object sender, CanMessageEventArgs e) { 
            try {
                //Parse incoming data
                string[] parsedString = e.canMessage.Split(',');

                int nodeID = Convert.ToInt32(parsedString[0]);
                if (nodeID == cv.canControlID) nodeID = 0;
                else if (nodeID > 20) nodeID -= 10;

                node[nodeID].reportRec = Convert.ToByte(parsedString[1]);

                //Console.WriteLine("Incomming Message : Node {1}: {0}", e.canMessage, nodeID);

                if (node[nodeID].reportRec == 9) {
                    node[nodeID].fromPC = Convert.ToInt32(parsedString[2]);
                    node[nodeID].toPC = Convert.ToInt32(parsedString[3]);
                    node[nodeID].commandNodeMessagesReceived = Convert.ToInt32(parsedString[4]);
                    node[nodeID].commandNodeMessagesSent = Convert.ToInt32(parsedString[5]);
                    node[nodeID].nodeMessagesReceived = Convert.ToInt32(parsedString[6]);
                    node[nodeID].nodeMessagesSent = Convert.ToInt32(parsedString[7]);

                    node[cv.controlBoard].nodeMessagesSent = Convert.ToInt32(parsedString[4]);
                    node[cv.controlBoard].nodeMessagesReceived = Convert.ToInt32(parsedString[5]);

                } else {
                    node[nodeID].lightStatus = this.cb.colorCode(parsedString[2]);
                    node[nodeID].lightMode = parsedString[3];
                    node[nodeID].gameMode = this.cb.gameModeCode(parsedString[4]);
                    node[nodeID].inputStatus = Convert.ToByte(parsedString[5]);
                    node[nodeID].outputStatus = Convert.ToByte(parsedString[6]);
                    node[nodeID].byte6 = Convert.ToByte(parsedString[7]);
                    node[nodeID].byte7 = Convert.ToByte(parsedString[8]);

                    // <TODO> determine switch inputs
                }
            } catch (Exception ex) {
                logWrite ("Update Node Failed - " + ex);
            }
        }

        /// <summary>
        /// Get's State of all Nodes
        /// </summary>
        public void GetNodeState()
        {
            this.cb.Report();
        }

        /// <summary>
        /// Get's State of given Node
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        public void GetNodeState(int _nodeID)
        {
            ComModes mode = new ComModes();
            mode = this.node[_nodeID].type;

            switch (mode)
            {
                case ComModes.canBus:
                    int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                    this.cb.Report(nodeAddress);
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Checks last read state of given input
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        /// <param name="input">input number</param>
        /// <returns></returns>
        public bool InputState(int _nodeID, int input)
        {
            if (node[_nodeID].type == ComModes.canBus)
            {
                int check = 1 << input;
                int i = node[_nodeID].inputStatus & check;
                if (i == check) return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// Clear all node variables
        /// </summary>
        public void ClearNodeState() {
            ClearNodeState(false);
        }

        /// <summary>
        /// Clear some node variables
        /// </summary>
        /// <param name="selective">True = some; False = all</param>
        public void ClearNodeState(bool selective) {
            foreach (Nodes nd in node) {
                node[nd.id].reportRec = 0;
                node[nd.id].lightStatus = "off";
                node[nd.id].lightMode = "0";            
                node[nd.id].inputStatus = 0;
                node[nd.id].outputStatus = 0;
                node[nd.id].byte6 = 0;
                node[nd.id].byte7 = 0;
                node[nd.id].scored = false;

                if (!selective) {             
                    node[nd.id].gameMode = "off";
                }
            }
        }

        public void ResetTestVariables()
        {
            cb.messagesSent = 0;
            cb.messagesReceived = 0;
        }
        #endregion

        #region Field Controls
        public void SendMessage(int _nodeID, string _message)
        {
            ComModes mode = new ComModes();
            mode = this.node[_nodeID].type;

            switch (mode)
            {
                case ComModes.canBus:
                    int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                    string s = string.Format("{0},{1}", nodeAddress, _message);
                    this.cb.Send(s);
                    break;

                case ComModes.xBee:

                    break;

                case ComModes.wiFi:

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Set's Robot Transmitter to given state
        /// </summary>
        /// <param name="_teamColor">Team's color (red, green)</param>
        /// <param name="_autoState">Autonomous on, off</param>
        /// <param name="_transmitterState">Transmitters on, off</param>
        public void RobotTransmitters(string _teamColor, State _autoState, State _transmitterState) {
            if (_teamColor == "green") {
                if (node[cv.greenTeam_Node].type == ComModes.canBus) {
                    int nodeAddress = Convert.ToInt32(node[cv.greenTeam_Node].address);
                    cb.RobotTransmitters(nodeAddress, _autoState, _transmitterState);
                }
            } else if (_teamColor == "red") {
                if (node[cv.redTeam_Node].type == ComModes.canBus) {
                    int nodeAddress = Convert.ToInt32(node[cv.redTeam_Node].address);
                    cb.RobotTransmitters(nodeAddress, _autoState, _transmitterState);
                }
            } else {
                if (node[cv.greenTeam_Node].type == ComModes.canBus) {
                    int nodeAddress = Convert.ToInt32(node[cv.greenTeam_Node].address);
                    cb.RobotTransmitters(nodeAddress, _autoState, _transmitterState);
                }

                if (node[cv.redTeam_Node].type == ComModes.canBus) {
                    int nodeAddress = Convert.ToInt32(node[cv.redTeam_Node].address);
                    cb.RobotTransmitters(nodeAddress, _autoState, _transmitterState);
                }
            }
        }

        /// <summary>
        /// Ring Field Bell
        /// </summary>
        public void RingBell() {
            cb.Send("31, 100");
        }

        /// <summary>
        /// Sounds Field Buzzer
        /// </summary>
        public void SoundBuzzer() {
            cb.Send("31, 101");
        }

        /// <summary>
        /// Changes Game Mode on all Nodes
        /// </summary>
        /// <param name="mode">Game Mode Value</param>
        /// <returns></returns>
        public GameModes ChangeGameMode(GameModes mode) {
            switchMode = true;

            foreach (Nodes nd in node) {
                node[nd.id].gameMode = mode.ToString();
            }

            if (canbusPresent) {
                cb.ChangeGameMode(0, mode);
                Thread.Sleep(10);
            }

            return mode;
        }

        /// <summary>
        /// Changes given Node's Game Mode
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        /// <param name="mode">Game Mode Value</param>
        /// <returns></returns>
        public GameModes ChangeGameMode(int _nodeID, GameModes mode) {
            switchMode = true;

            if (node[_nodeID].type == ComModes.canBus) {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);   //Get Address
                cb.ChangeGameMode(nodeAddress, mode);
                Thread.Sleep(10);
            }

            node[_nodeID].gameMode = mode.ToString();
            return mode;
        }

        public void ChangeGameFunction(int function, int functionMode, string option) {
            if (canbusPresent) {
                string s = string.Format("0,6,{0},{1},{2}", function, functionMode, option);
                cb.Send(s);
            }
        }

        public void ChangeGameFunction(int _nodeID, int function, int functionMode, string option)
        {
            if (node[_nodeID].type == ComModes.canBus)
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);   //Get Address
                string s = string.Format("{0},6,{1},{2},{2}", nodeAddress, function, functionMode, option);
                this.cb.Send(nodeAddress + ",6," + function + "," + functionMode + "," + option);
            }

        }

        /// <summary>
        /// Sets Debug Mode
        /// </summary>
        /// <param name="_state">on, off</param>
        public void DebugMode(State _state)
        {
            if (canbusPresent)
            {
                if (_state == State.on) this.ChangeGameMode(0, GameModes.debug);
                else this.ChangeGameMode(0, GameModes.reset);
            }
        }

        /// <summary>
        /// Monitor Raw Serial Data
        /// </summary>
        /// <param name="type"></param>
        public void SeeRawData(ComModes type)
        {
            if (type == ComModes.canBus) this.cb.monitorData();
        }

        public void StopRawData(ComModes type)
        {
            if (type == ComModes.canBus) this.cb.stopMonitoringData();
        }

        #endregion

        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="text">Text as string</param>
        private void logWrite(string text)
        {
            this.lw.WriteLog(text, "Field_Control_Log");
        }

        public void writeLogs(string path)
        {
            if (canbusPresent) cb.writeLogs(path);
        }

        public void writeLogs()
        {
            if (canbusPresent) cb.writeLogs();
        }

        public void StartFosCalibration(int _nodeID)
        {
            if (node[_nodeID].type == ComModes.canBus)
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);   //Get Address
                this.cb.StartFosCalibration(nodeAddress);
                Thread.Sleep(10);
            }
        }
    }
}
