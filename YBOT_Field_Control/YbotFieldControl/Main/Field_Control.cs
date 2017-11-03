using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data;

namespace YBOT_Field_Control_2016
{
    public class Field_Control
    {
        #region Variables
        private OneWire ow;
        private CANBUS cb;
        private fileStructure fs = new fileStructure();
        private LogWriter lw = new LogWriter();
        public Nodes[] node;
        private DataSet nodeDS = new DataSet();
        private DataGrid nodeGrid = new DataGrid();
        private CommonVariables cv = new CommonVariables();
        public struct owStates
        {
            public string adapter;
            public string mainState;
        }
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
        public owStates owStat = new owStates();
        public bool switchMode = false;
        bool oneWirePresent = false;    //Set flag false
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
            ow = null;                                  //Despose of one wire object
            cb = new CANBUS(nodeTable.Rows.Count);      //New Canbus object
            ow = new OneWire(nodeTable.Rows.Count);     //New one wire object

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

                        if (nodeType == "1-Wire")
                        {
                            this.node[nodeID].type = ComModes.oneWire;
                            oneWirePresent = true;
                        }
                        else if (nodeType == "CANBUS")
                        {
                            this.node[nodeID].type = ComModes.canBus;
                            canbusPresent = true;
                        }
                        else if (nodeType == "XBee") this.node[nodeID].type = ComModes.xBee;
                        else if (nodeType == "WiFi") this.node[nodeID].type = ComModes.wiFi;
                        else this.node[nodeID].type = ComModes.none;

                        this.node[nodeID].address = GetNodeAddress(nodeID);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }

            }

            //Start 1-wire if we have one wire nodes
            if (oneWirePresent)
            {
                this.ow.Setup();
                if (this.owStat.adapter != null)
                {
                    this.owStat.adapter = this.ow.adapter.ToString();
                }
            }
            //Start canbus if we have canbus nodes
            if (canbusPresent) this.cb.FindPort();
        }

        /// <summary>
        /// Starts Field Communications
        /// </summary>
        public void StartUp()
        {
            this.cb.Startup();
            this.ow.StartUp();
            this.owStat.mainState = "ON";
        }

        /// <summary>
        /// Starts Field Communications in given mode
        /// </summary>
        /// <param name="mode">Communcation Mode Value</param>
        public void StartUp(ComModes mode)
        {
            if (mode == ComModes.oneWire)
            {
                this.ow.StartUp();
                this.owStat.mainState = "ON";
            }
            else if (mode == ComModes.canBus) this.cb.Startup();
        }

        /// <summary>
        /// Starts Field Communication in given mode on given port
        /// </summary>
        /// <param name="mode">Communication Mode Value</param>
        /// <param name="port">ComPort</param>
        public void StartUp(ComModes mode, string port)
        {
            if (mode == ComModes.oneWire)
            {
                this.ow.StartUp();
                this.owStat.mainState = "ON";
            }
            else if (mode == ComModes.canBus) this.cb.Startup(port);
        }

        /// <summary>
        /// Shuts down field communcations
        /// </summary>
        public void ShutDown()
        {
            this.cb.Shutdown();
            this.ow.ShutDown();
            this.owStat.mainState = "OFF";
        }

        /// <summary>
        /// Shuts down field communication on given Mode
        /// </summary>
        /// <param name="mode">Communication Mode Value</param>
        public void ShutDown(ComModes mode)
        {
            if (mode == ComModes.oneWire)
            {
                this.ow.ShutDown();
                this.owStat.mainState = "OFF";
            }
            else if (mode == ComModes.canBus) this.cb.Shutdown();

        }

        /// <summary>
        /// Shuts down field communication on give mode on given port
        /// </summary>
        /// <param name="mode">Communication Mode Value</param>
        /// <param name="port">ComPort</param>
        public void ShutDown(ComModes mode, string port)
        {
            if (mode == ComModes.oneWire)
            {
                this.ow.ShutDown();
                this.owStat.mainState = "OFF";
            }
            else if (mode == ComModes.canBus) this.cb.Shutdown(port);

        }

        /// <summary>
        /// Tests if Node is connected to network
        /// </summary>
        /// <param name="_nodeID">Node Address</param>
        /// <returns></returns>
        public bool TestConnections(int _nodeID)
        {
            if (this.node[_nodeID].type == ComModes.oneWire)
            {
                return this.ow.TestConnection(_nodeID);
            }
            else if (this.node[_nodeID].type == ComModes.canBus)
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                return this.cb.TestConnection(nodeAddress);
            }
            else return false;
        }

        /// <summary>
        /// Gets node's address value
        /// </summary>
        /// <param name="_nodeID">Node's ID number</param>
        /// <returns>Node's ID Value</returns>
        public string GetNodeAddress(int _nodeID)
        {
            if (this.node[_nodeID].type == ComModes.oneWire)
            {
                if (this.ow.adapter != null)
                {
                    string newAddress = this.ow.ToHex(this.ow.owNode[_nodeID].address, 0, this.ow.owNode[_nodeID].address.Length);
                    if (newAddress == null) newAddress = "No Address";
                    return newAddress;
                }
                else return "No Adapter";

            }
            else if (this.node[_nodeID].type == ComModes.canBus)
            {
                string address;
                if (this.node[_nodeID].id == 0) address = this.cb.commandNode.ToString();
                else address = this.node[_nodeID].id.ToString();
                return address;
            }
            else return "No Address";
        }

        /// <summary>
        /// Set's node's mode to off
        /// </summary>
        public void FieldAllOff()
        {
            if (oneWirePresent)
            {
                foreach (Nodes nd in node)
                {
                    if (nd.type == ComModes.oneWire)
                    {
                        this.ow.PinWrite(nd.id, State.off);                            //Write all pins off
                    }
                }
            }
            if (canbusPresent)
            {
                this.cb.ChangeGameMode(0, GameModes.off);
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
            string colorState = (_color1.ToString() + "|" + _color2.ToString() + "|" + _color3.ToString() + "|" + _color1.ToString());
            this.node[_nodeID].lightStatus = colorState;

            if (this.node[_nodeID].type == ComModes.oneWire)
            {
                State red = State.off;
                State green = State.off;
                State yellow = State.off;
                State blue = State.off;

                if (_color1 == LightColor.red) red = State.on;
                else if (_color1 == LightColor.green) green = State.on;
                else if (_color1 == LightColor.yellow) yellow = State.on;
                else if (_color1 == LightColor.blue) blue = State.on;

                if (_color2 == LightColor.red) red = State.on;
                else if (_color2 == LightColor.green) green = State.on;
                else if (_color2 == LightColor.yellow) yellow = State.on;
                else if (_color2 == LightColor.blue) blue = State.on;

                if (_color3 == LightColor.red) red = State.on;
                else if (_color3 == LightColor.green) green = State.on;
                else if (_color3 == LightColor.yellow) yellow = State.on;
                else if (_color3 == LightColor.blue) blue = State.on;

                if (_color4 == LightColor.red) red = State.on;
                else if (_color4 == LightColor.green) green = State.on;
                else if (_color4 == LightColor.yellow) yellow = State.on;
                else if (_color4 == LightColor.blue) blue = State.on;

                this.ow.Light(_nodeID, red, green, blue, yellow);
            }
            else if (this.node[_nodeID].type == ComModes.canBus)
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                this.cb.Light(nodeAddress, _color1, _color2, _color3, _color4);
            }
            else return;
        }

        /// <summary>
        /// Sets Node to Given Color Value and State
        /// </summary>
        /// <param name="_nodeID">Node's ID Value</param>
        /// <param name="_color">Color Value</param>
        /// <param name="_state">on, off</param>
        public void Light(int _nodeID, LightColor _color, State _state)
        {
            this.node[_nodeID].lightStatus = _color.ToString();
            if (this.node[_nodeID].type == ComModes.oneWire)
            {
                this.ow.Light(_nodeID, _color, _state);
            }
            else if (this.node[_nodeID].type == ComModes.canBus)
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                if (_state == State.off) _color = LightColor.off;
                this.cb.Light(nodeAddress, _color);
            }
            else return;
        }

        /// <summary>
        /// Sets All Nodes to Given Color and State
        /// </summary>
        /// <param name="_color">Color Value</param>
        /// <param name="_state">on, off</param>
        public void AllFieldLights(LightColor _color, State _state)
        {
            foreach (Nodes nd in node)
            {
                if (nd.type == ComModes.oneWire)
                {
                    if (nd.lightStatus != _color.ToString()) Light(nd.id, _color, _state);
                }
                node[nd.id].lightStatus = _color.ToString();
            }
            if (canbusPresent) cb.Light(0, _color);
        }

        /// <summary>
        /// Sets all node's rings on
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        public void LightTest(int _nodeID)
        {
            this.node[_nodeID].lightStatus = "Test";
            if (this.node[_nodeID].type == ComModes.oneWire)
            {
                this.ow.Light(_nodeID, State.on, State.on, State.on, State.on);
            }
            else if (this.node[_nodeID].type == ComModes.canBus)
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);
                this.cb.LightTest(nodeAddress);
            }
            else return;
        }

        /// <summary>
        /// Sets all node's rings on
        /// </summary>
        public void LightTest()
        {
            foreach (Nodes nd in node)
            {
                this.node[nd.id].lightStatus = "Test";
                if (this.node[nd.id].type == ComModes.oneWire)
                {
                    this.ow.Light(nd.id, State.on, State.on, State.on, State.on);
                    Thread.Sleep(500);
                    this.ow.Light(nd.id, State.off, State.off, State.off, State.off);
                }
            }
            if (canbusPresent)
            {
                this.cb.LightTest(0);
            }
            else return;
        }

        #endregion

        #region Relay Controls
        /// <summary>
        /// Sets Node's Relays to given state
        /// </summary>
        /// <param name="_nodeID">Node's ID</param>
        /// <param name="_state">State of all relays(0 all off, 255 all on)</param>
        public void SetRelayState(int _nodeID, byte _state)
        {
            this.node[_nodeID].relayStatus = _state;                                      //Update this.node status

            if (this.node[_nodeID].type == ComModes.oneWire)                         //If onewire
            {
                this.ow.WriteNodeState(_nodeID, _state);                        //Swith Relays to given state
            }
            else if (this.node[_nodeID].type == ComModes.canBus)                     //If CANBUS
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);   //Get Address
                this.cb.SetRelayState(nodeAddress, _state);                     //Send Node Relay State
            }
            else return;
        }

        /// <summary>
        /// Sets one of the Node's relays to a given state
        /// </summary>
        /// <param name="_nodeID">Node's ID</param>
        /// <param name="_relayNum">Relay to switch</param>
        /// <param name="_state">on or off</param>
        public void SetRelayState(int _nodeID, int _relayNum, State _state)
        {
            byte newState;
            int relay = _relayNum - 1;                                              //Get Relay Location
            byte currentSate = this.node[_nodeID].relayStatus;                                //Get Current State
            if (_state == State.on) newState = (byte)((1 << relay) | currentSate);  //Update State
            else newState = (byte)(~(1 << relay) & currentSate);
            this.node[_nodeID].relayStatus = newState;                                        //Update this.node status

            if (this.node[_nodeID].type == ComModes.oneWire)                             //If OneWire
            {
                this.ow.PinWrite(_relayNum, _nodeID, _state);                       //Set relay on or off
            }
            else if (this.node[_nodeID].type == ComModes.canBus)                         //If Canbus
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);   //Get Address
                this.cb.SetRelay(nodeAddress, _relayNum, _state);                   //Send relay request
            }
            else return;
        }

        /// <summary>
        /// Sets Relay to given state on all field nodes
        /// </summary>
        /// <param name="_state">State of all relays(0 all off, 255 all on)</param>
        public void AllFieldRelays(byte _state)
        {
            foreach (Nodes nd in node)
            {
                this.node[nd.id].relayStatus = _state;                                      //Update this.node status

                if (this.node[nd.id].type == ComModes.oneWire)                         //If onewire
                {
                    this.ow.WriteNodeState(nd.id, _state);                        //Swith Relays to given state
                }
            }

            if (canbusPresent)                     //If CANBUS
            {
                this.cb.SetRelayState(0, _state);                     //Send Node Relay State
            }
            else return;
        }

        public void AllFieldRelays(int _relayNum, State _state)
        {
            foreach (Nodes nd in node)
            {
                byte newState;
                int relay = _relayNum - 1;                                              //Get Relay Location
                byte currentSate = this.node[nd.id].relayStatus;                                //Get Current State
                if (_state == State.on) newState = (byte)((1 << relay) | currentSate);  //Update State
                else newState = (byte)(~(1 << relay) & currentSate);
                this.node[nd.id].relayStatus = newState;                                        //Update this.node status

                if (this.node[nd.id].type == ComModes.oneWire)                         //If onewire
                {
                    this.ow.PinWrite(_relayNum, nd.id, _state);                       //Set relay on or off
                }
            }

            if (canbusPresent)                     //If CANBUS
            {
                this.cb.SetRelay(0, _relayNum, _state);                   //Send relay request
            }
            else return;
        }

        #endregion

        #region Latch Controls

        /// <summary>
        /// Resets Latches on all nodes
        /// </summary>
        public void ResetAllNodeLatches()
        {
            foreach (Nodes nd in node)
            {
                if (nd.type == ComModes.oneWire)
                {
                    this.ow.ResetActivityLatch(nd.id);                            //Write all pins off
                }
            }
        }

        /// <summary>
        /// Resets latches on given 1-wire node
        /// </summary>
        /// <param name="_nodeID">Node's ID Value</param>
        public void ResetOneWireLatch(int _nodeID)
        {
            this.ow.ResetActivityLatch(_nodeID);
        }

        /// <summary>
        /// Reads Latch Value on given 1-wire node
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        public void ReadOneWireLatch(int _nodeID)
        {
            this.ow.ReadState(_nodeID, true);
        }
        #endregion

        #region Updaters
        /// <summary>
        /// Update All Nodes state
        /// </summary>
        public void UpdateNodeState()
        {
            foreach (Nodes nd in node)
            {
                if (this.node[nd.id].type == ComModes.oneWire)                             //If OneWire
                {
                    node[nd.id].inputStatus = this.ow.owNode[nd.id].state;
                    node[nd.id].latchStatus = this.ow.owNode[nd.id].latchState;

                    bool r = this.ow.PinState(nd.id, this.ow.red);
                    bool g = this.ow.PinState(nd.id, this.ow.green);
                    bool y = this.ow.PinState(nd.id, this.ow.yellow);
                    bool b = this.ow.PinState(nd.id, this.ow.blue);

                    if (r && g && y && b) node[nd.id].lightStatus = LightColor.all.ToString();
                    else if (r && !g && !y && !b) node[nd.id].lightStatus = LightColor.red.ToString();
                    else if (!r && g && !y && !b) node[nd.id].lightStatus = LightColor.green.ToString();
                    else if (!r && !g && y && !b) node[nd.id].lightStatus = LightColor.yellow.ToString();
                    else if (!r && !g && !y && b) node[nd.id].lightStatus = LightColor.blue.ToString();
                    else if (!r && !g && !y && !b) node[nd.id].lightStatus = LightColor.off.ToString();
                    else node[nd.id].lightStatus = "other";
                }
                else if (this.node[nd.id].type == ComModes.canBus)                         //If Canbus
                { 
                    if (this.cb.canRec[nd.id] != null)
                    {
                        this.cb.ParseCanData(nd.id);

                        this.node[nd.id].lightStatus = this.cb.canIN[nd.id].lightStatus;
                        this.node[nd.id].lightMode = this.cb.canIN[nd.id].lightMode;
                        this.node[nd.id].gameMode = this.cb.canIN[nd.id].gameMode;
                        this.node[nd.id].inputStatus = this.cb.canIN[nd.id].inputStatus;
                        //this.node[nd.id].latchStatus = this.cb.canIN[nd.id].inputStatus;
                        this.node[nd.id].relayStatus = this.cb.canIN[nd.id].relayStatus;
                        this.node[nd.id].fosValue = this.cb.canIN[nd.id].fosValue;
                        this.node[nd.id].fosColor = this.cb.canIN[nd.id].fosColor;

                        this.logWrite
                            ("Updated Node Data = " + nd.id + "," +
                            this.node[nd.id].lightStatus + "," +
                            this.node[nd.id].lightMode + "," +
                            this.node[nd.id].gameMode + "," +
                            this.node[nd.id].inputStatus + "," +
                            //this.node[nd.id].latchStatus + "," +
                            this.node[nd.id].relayStatus + "," +
                            this.node[nd.id].fosValue + "," +
                            this.node[nd.id].fosColor
                            );

                        this.cb.canRec[nd.id] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Update given node's state
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        public void UpdateNodeState(int _nodeID)
        {
            if (this.node[_nodeID].type == ComModes.oneWire)                             //If OneWire
            {
                node[_nodeID].inputStatus = this.ow.owNode[_nodeID].state;
                node[_nodeID].latchStatus = this.ow.owNode[_nodeID].latchState;

                bool r = this.ow.PinState(_nodeID, this.ow.red);
                bool g = this.ow.PinState(_nodeID, this.ow.green);
                bool y = this.ow.PinState(_nodeID, this.ow.yellow);
                bool b = this.ow.PinState(_nodeID, this.ow.blue);

                if (r && g && y && b) node[_nodeID].lightStatus = LightColor.all.ToString();
                else if (r && !g && !y && !b) node[_nodeID].lightStatus = LightColor.red.ToString();
                else if (!r && g && !y && !b) node[_nodeID].lightStatus = LightColor.green.ToString();
                else if (!r && !g && y && !b) node[_nodeID].lightStatus = LightColor.yellow.ToString();
                else if (!r && !g && !y && b) node[_nodeID].lightStatus = LightColor.blue.ToString();
                else if (!r && !g && !y && !b) node[_nodeID].lightStatus = LightColor.off.ToString();
                else node[_nodeID].lightStatus = "other";
            }
            else if (this.node[_nodeID].type == ComModes.canBus)                         //If Canbus
            {
                
                if (this.cb.canRec[_nodeID] != null)
                {
                    this.cb.ParseCanData(_nodeID);

                    this.node[_nodeID].lightStatus = this.cb.canIN[_nodeID].lightStatus;
                    this.node[_nodeID].lightMode = this.cb.canIN[_nodeID].lightMode;
                    this.node[_nodeID].gameMode = this.cb.canIN[_nodeID].gameMode;
                    this.node[_nodeID].inputStatus = this.cb.canIN[_nodeID].inputStatus;
                    //this.node[_nodeID].latchStatus = this.cb.canIN[_nodeID].inputStatus;
                    this.node[_nodeID].relayStatus = this.cb.canIN[_nodeID].relayStatus;
                    this.node[_nodeID].fosValue = this.cb.canIN[_nodeID].fosValue;
                    this.node[_nodeID].fosColor = this.cb.canIN[_nodeID].fosColor;

                    this.logWrite
                    ("Updated Node Data = " + _nodeID + "," +
                    this.node[_nodeID].lightStatus + "," +
                    this.node[_nodeID].lightMode + "," +
                    this.node[_nodeID].gameMode + "," +
                    this.node[_nodeID].inputStatus + "," +
                    //this.node[_nodeID].latchStatus + "," +
                    this.node[_nodeID].relayStatus + "," +
                    this.node[_nodeID].fosValue + "," +
                    this.node[_nodeID].fosColor
                    );

                    this.cb.canRec[_nodeID] = null;
                }
            }
        }

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
            if (this.node[_nodeID].type == ComModes.oneWire)
            {
                this.ow.ReadState(_nodeID);
                //UpdateNodeState(_nodeID);
            }
            else if (this.node[_nodeID].type == ComModes.canBus)
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);   //Get Address
                this.cb.Report(nodeAddress);
                //UpdateNodeState(_nodeID);
            }
            else return;
        }

        /// <summary>
        /// Gets State of given node
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        /// <param name="_latch">Get Latch State (true-yes, false-no)</param>
        public void GetNodeState(int _nodeID, bool _latch)
        {
            if (this.node[_nodeID].type == ComModes.oneWire)
            {
                this.ow.ReadState(_nodeID, _latch);
                UpdateNodeState(_nodeID);
            }
            else if (this.node[_nodeID].type == ComModes.canBus)
            {
                UpdateNodeState(_nodeID);
            }
            else return;
        }

        /// <summary>
        /// Checks last read state of given input
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        /// <param name="input">input number</param>
        /// <returns></returns>
        public bool InputState(int _nodeID, int input)
        {
            if (this.node[_nodeID].type == ComModes.oneWire)
            {
                int i = 0;
                if (input == 0) i = this.ow.input1;
                else if (input == 1) i = this.ow.input2;
                else i = input;
                return this.ow.PinState(_nodeID, i);
            }
            else if (this.node[_nodeID].type == ComModes.canBus)
            {
                int check = 1 << input;
                int i = node[_nodeID].inputStatus & check;
                if (i == check) return true;
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Check last read latch state of given input
        /// </summary>
        /// <param name="_nodeID">Node ID Value</param>
        /// <param name="input">Input number</param>
        /// <returns></returns>
        public bool LatchState(int _nodeID, int input)
        {
            if (this.node[_nodeID].type == ComModes.oneWire)
            {
                int i = 0;
                if (input == 0) i = this.ow.input1;
                else if (input == 1) i = this.ow.input2;
                else i = input;
                return this.ow.PinState(_nodeID, i, true);
            }
            else if (this.node[_nodeID].type == ComModes.canBus)
            {
                int check = 1 << input;
                int i = node[_nodeID].latchStatus & check;
                if (i == check) return true;
                else return false;
            }
            else return false;
        }

        public void ClearNodeState()
        {
            foreach (Nodes nd in node)
            {
                if (this.node[nd.id].type == ComModes.oneWire)                             //If OneWire
                {

                    node[nd.id].inputStatus = this.ow.owNode[nd.id].state;
                    node[nd.id].latchStatus = this.ow.owNode[nd.id].latchState;

                    bool r = this.ow.PinState(nd.id, this.ow.red);
                    bool g = this.ow.PinState(nd.id, this.ow.green);
                    bool y = this.ow.PinState(nd.id, this.ow.yellow);
                    bool b = this.ow.PinState(nd.id, this.ow.blue);

                    if (r && g && y && b) node[nd.id].lightStatus = LightColor.all.ToString();
                    else if (r && !g && !y && !b) node[nd.id].lightStatus = LightColor.red.ToString();
                    else if (!r && g && !y && !b) node[nd.id].lightStatus = LightColor.green.ToString();
                    else if (!r && !g && y && !b) node[nd.id].lightStatus = LightColor.yellow.ToString();
                    else if (!r && !g && !y && b) node[nd.id].lightStatus = LightColor.blue.ToString();
                    else if (!r && !g && !y && !b) node[nd.id].lightStatus = LightColor.off.ToString();
                    else node[nd.id].lightStatus = "other";
                }
                else if (this.node[nd.id].type == ComModes.canBus)                         //If Canbus
                {
                    this.node[nd.id].lightStatus = "off";
                    this.node[nd.id].lightMode = "0";
                    this.node[nd.id].gameMode = "off";
                    this.node[nd.id].inputStatus = 0;
                    this.node[nd.id].latchStatus = 0;
                    this.node[nd.id].relayStatus = 0;
                    this.node[nd.id].fosValue = 0;
                    this.node[nd.id].fosColor = "off";
                    this.node[nd.id].scored = false;
                }
            }
        }
        #endregion

        #region Field Controls
        public void SendMessage(int _nodeID, string _message)
        {
            if (this.node[_nodeID].type == ComModes.canBus)                         //If Canbus
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);   //Get Address
                this.cb.Send(nodeAddress + "," + _message);
            }
        }

        /// <summary>
        /// Set's Robot Transmitter to given state
        /// </summary>
        /// <param name="_teamColor">Team's color (red, green)</param>
        /// <param name="_autoState">Autonomous on, off</param>
        /// <param name="_transmitterState">Transmitters on, off</param>
        public void RobotTransmitters(string _teamColor, State _autoState, State _transmitterState)
        {
            if (_teamColor == "green")
            {
                if (node[cv.greenTeam_Node].type == ComModes.oneWire)
                {
                    this.ow.RobotTransmitters(cv.greenTeam_Node, _autoState, _transmitterState);
                }
                else if (node[cv.greenTeam_Node].type == ComModes.canBus)
                {
                    int nodeAddress = Convert.ToInt32(this.node[cv.greenTeam_Node].address);
                    this.cb.RobotTransmitters(nodeAddress, _autoState, _transmitterState);
                }
            }
            else if (_teamColor == "red")
            {
                if (node[cv.redTeam_Node].type == ComModes.oneWire)
                {
                    this.ow.RobotTransmitters(cv.redTeam_Node, _autoState, _transmitterState);
                }
                else if (node[cv.redTeam_Node].type == ComModes.canBus)
                {
                    int nodeAddress = Convert.ToInt32(this.node[cv.redTeam_Node].address);
                    this.cb.RobotTransmitters(nodeAddress, _autoState, _transmitterState);
                }
            }
            else
            {
                if (node[cv.greenTeam_Node].type == ComModes.oneWire)
                {
                    this.ow.RobotTransmitters(cv.greenTeam_Node, _autoState, _transmitterState);
                }
                else if (node[cv.greenTeam_Node].type == ComModes.canBus)
                {
                    int nodeAddress = Convert.ToInt32(this.node[cv.greenTeam_Node].address);
                    this.cb.RobotTransmitters(nodeAddress, _autoState, _transmitterState);
                }

                if (node[cv.redTeam_Node].type == ComModes.oneWire)
                {
                    this.ow.RobotTransmitters(cv.redTeam_Node, _autoState, _transmitterState);
                }
                else if (node[cv.redTeam_Node].type == ComModes.canBus)
                {
                    int nodeAddress = Convert.ToInt32(this.node[cv.redTeam_Node].address);
                    this.cb.RobotTransmitters(nodeAddress, _autoState, _transmitterState);
                }
            }
        }

        /// <summary>
        /// Ring Field Bell
        /// </summary>
        public void RingBell()
        {
            SetRelayState(cv.controlBoard, cv.bell, State.on);
            Thread.Sleep(350);
            SetRelayState(cv.controlBoard, cv.bell, State.off);
            Thread.Sleep(350);
            SetRelayState(cv.controlBoard, cv.bell, State.on);
            Thread.Sleep(350);
            SetRelayState(cv.controlBoard, cv.bell, State.off);
        }

        /// <summary>
        /// Sounds Field Buzzer
        /// </summary>
        public void SoundBuzzer()
        {
            SetRelayState(cv.controlBoard, cv.buzzer, State.on);
            Thread.Sleep(200);
            SetRelayState(cv.controlBoard, cv.buzzer, State.off);
        }

        /// <summary>
        /// Changes Game Mode on all Nodes
        /// </summary>
        /// <param name="mode">Game Mode Value</param>
        /// <returns></returns>
        public GameModes ChangeGameMode(GameModes mode)
        {
            switchMode = true;
            foreach (Nodes nd in node)
            {
                node[nd.id].gameMode = mode.ToString();
            }
            if (canbusPresent)
            {
                this.cb.ChangeGameMode(0, mode);
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
        public GameModes ChangeGameMode(int _nodeID, GameModes mode)
        {
            switchMode = true;
            if (node[_nodeID].type == ComModes.oneWire)
            {
            }
            else if (node[_nodeID].type == ComModes.canBus)
            {
                int nodeAddress = Convert.ToInt32(this.node[_nodeID].address);   //Get Address
                this.cb.ChangeGameMode(nodeAddress, mode);
                Thread.Sleep(10);
            }
            node[_nodeID].gameMode = mode.ToString();
            return mode;
        }

        /// <summary>
        /// Sets Debug Mode
        /// </summary>
        /// <param name="_state">on, off</param>
        public void DebugMode(State _state)
        {
            foreach (Nodes nd in node)
            {
                if (nd.type == ComModes.oneWire)
                {
                    if (_state == State.on) this.ow.debug = true;
                    else this.ow.debug = false;
                }
                node[nd.id].gameMode = GameModes.debug.ToString();
            }

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


    }
}
