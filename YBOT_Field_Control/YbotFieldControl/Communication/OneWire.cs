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
        #region Constructors

        public OneWire()
        {

            device = new addList[16];                          // device array
            owNode = new addList[16];                           // Node array

        }

        public OneWire(int _maxNodes)
        {
            maxNodes = _maxNodes;
            device = new addList[maxNodes + 1];                          // device array
            owNode = new addList[maxNodes + 1];                           // Node array
        }

        #endregion

        #region 1-Wire Methods

        /// <summary>
        /// Used to start the 1 wire devices and load or search for nodes
        /// </summary>
        public void Setup()
        {
            Start_1Wire();       //Start 1 wire
            if (adapter != null)
            {
                LocateCoupler();     //Find coupler
                HubAllMainsOn();     //Turn on 1 wire mains
                Locate_Nodes();    //Locate towers
            }
        }

        /// <summary>
        /// Used to set default 1 wire adapter
        /// </summary>
        public void Start_1Wire()
        {
            string adapterName = null, adapterPort = null;      //Set variables for adatername and port
            int count = 0;                                      //Used to count port number
            //If there is an adapter set then stop it and clear its info
            if (adapter != null)
            {
                try
                {
                    adapter.EndExclusive();
                    adapter.Dispose();
                    adapter = null;
                }
                catch
                {
                    adapter = null;
                }
            }
            else
            {

                //Try all COM and USB ports for 1 wire adapters
                do
                {
                    //Search all COM ports for Serial 1 wire adapter
                    if (adapter == null)
                    {
                        adapterName = "{DS9097U}";
                        adapterPort = "COM" + count.ToString();
                        setAdapter(adapterName, adapterPort);
                    }
                    //Search all USB ports for USB 1 wire adapter
                    if (adapter == null)
                    {
                        adapterName = "{DS9490}";
                        adapterPort = "USB" + count.ToString();
                        setAdapter(adapterName, adapterPort);
                    }

                    count++;
                } while (count < 15 && adapter == null); //Repeat until adapter is found or all ports have been searched

                //If adapter wasn't set
                if (adapter == null)
                {
                    //Try search for the default adapter
                    try
                    {
                        onewireSearch();
                        adapter = AccessProvider.DefaultAdapter;
                    }
                    //If failed show/record error and return
                    catch
                    {
                        adapter = null;
                        string error = ("Error in Start_1Wire: No Adapter Found");
                        logWrite(error);
                        MessageBox.Show("No Adapter! Plugin in 1wire adapter and restart program");
                        return;
                    }
                }
            }

            //Record success in log
            logWrite("Start_1Wire Complete");
            //Record adapter, port, and speed in log
            logWrite("Adapter = " + adapter.ToString() + " @ " + adapter.Speed.ToString());
            //Search for 1 wire devices
            //onewireSearch(); 
        }

        ///// <summary>
        ///// Used to set the Conditional Search Status on a given Node
        ///// </summary>
        ///// <param name="pinsToWatch">pins to Watch as a byte "0" don't watch "1" watch</param>
        ///// <param name="pinPolarities">State of the pin to start "0" off "1" onn</param>
        ///// <param name="controlBuffer">Sets the the conditions that will trigger alarm "See data sheet for 1 wire device 2408"</param>
        ///// <param name="node">Number of Node to set the conditions as a int</param>
        //public void SetConditionalSearch(byte pinsToWatch, byte pinPolarities, byte controlBuffer, int node)
        //{

        //    byte[] byteBuffer = {0xCC,                   // 5Ah - Channel access write command
        //                         0x8B,                   // Target Address
        //                         0x00,                   // Target Address
        //                         pinsToWatch,          // pins to trigger a response
        //                         pinPolarities,        // State pins will trigger a response
        //                         controlBuffer};         // Control register

        //    //Send Data to Node and update Node.state
        //    Update_Node(node, byteBuffer);
        //    adapter.Reset();

        //    if (debug) logWrite("DEBUG: Set_NodeState : Node# = " + node.ToString() +
        //                         " : Status = " + owNode[node].state.ToString());
        //}

        ///// <summary>
        ///// Performs a conditional search on the bus line
        ///// </summary>
        ///// <returns>Return the result as a byte array</returns>
        //public byte[] ConditionalSearch()
        //{
        //    byte[] block = { 0xEC,              
        //                    0xFF,
        //                    0xFF};

        //    byte[] status = null;

        //    adapter.Reset();

        //    if (adapter != null)
        //    {
        //        try
        //        {
        //            adapter.DataBlock(block, 0, block.Length);
        //            status = block;
        //            return status;
        //        }
        //        catch
        //        {
        //            logWrite("Error: ConditionalSearch: block = " + ToHex(block, 0, block.Length));
        //            status = null;
        //        }
        //    }
        //    return status;
        //}

        ///// <summary>
        ///// Used to verify if a Node is alarming
        ///// </summary>
        ///// <param name="status">Search result</param>
        ///// <param name="bit">bit to check</param>
        ///// <returns>Returns True if a Node is alarming; False if nothing has changed</returns>
        //public bool ConditionalSearchStatus(byte status, int bit)
        //{
        //    byte bitValue = 0;

        //    if (bit != 0)
        //    {
        //        bit -= 1;                                     // pin was selected with "people" number. Processing needs bit position (1 less).
        //        bitValue = (byte)Math.Pow(2.0, bit);          // This returns the value of the bit positon
        //    }
        //    if ((status & bitValue) != bitValue)              // the pin is off
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;                                  // the pin must be on
        //    }
        //}

        /// <summary>
        /// Used to clear activity latches on a given Node
        /// </summary>
        /// <param name="node">Node Number as an int</param>
        public void ResetActivityLatch(int node)
        {
            byte[] block = {0xC3,               // C3h - Reset activity latches
                            0xFF};              // OxAA is returned if successful

            //Send Data to Node and update pinLatchStatus
            Update_Node(node, block);
            owNode[node].latchState = (byte)Update_Node(node, block)[1]; //was 3

            if (debug)
                logWrite("DEBUG: ClearLatch : Node# = " + node.ToString() +
                          " : Status = " + owNode[node].latchState.ToString());
        }

        /// <summary>
        /// Used to change the state of all the pins on a given Node
        /// </summary>
        /// <param name="node">Node Number as an int</param>
        public void PinWrite(int node, State state)
        {
            byte write = 0;
            byte inverted = 1;
            if (state == State.off)
            {
                write = 0xFF;
                inverted = 0x00;
            }
            else
            {
                write = 0x00;
                inverted = 0xFF;
            }

            byte[] byteBuffer = {   0x5A,       //5Ah - Channel access write command
                                    write,      // Write byte to PIO (initialized to 0x00 - all on)
                                    inverted,   // Write inverted byte to PIO
                                    0xFF,       // Read for verification (AAh = success)
                                    0xFF };     // Read PIO pin status

            //Send Data to Node and update Node.state
            owNode[node].state = (byte)~Update_Node(node, byteBuffer)[1];
            owNode[node].writtenState = write;
            if (debug)
                logWrite("DEBUG: PinWrite - All : Node# = " + node.ToString() +
                          " : Status = " + owNode[node].state.ToString());
        }

        /// <summary>
        /// Use this to turn a Pin off on a giving Node
        /// </summary>
        /// <param name="pin">Pin Number</param>
        /// <param name="node">Node Number</param>
        /// <param name="state">true = on; false = off</param>
        public void PinWrite(int pin, int node, State state)
        {
            byte pinStatus;
            byte bitValue;

            pin -= 1;                                 // Pin was selected with "people" number. Processing needs bit number (1 less).
            bitValue = (byte)Math.Pow(2.0, pin);      // This returns the value of the bit positon
            pinStatus = owNode[node].state;         // Get non inverted Pin status

            if (state == State.on) bitValue = (byte)(pinStatus | bitValue);  // Change selected Pin but maintain other Pins status
            else bitValue = (byte)(pinStatus ^ bitValue);              // Change selected Pin but maintain other Pins status

            byte pinComplement = (byte)~bitValue;
            byte[] byteBuffer = {   0x5A,                   // 5Ah - Channel access write command
                                    pinComplement,          // Write byte to PIO (complemented because 1 is off)
                                    bitValue,               // Write inverted byte to PIO
                                    0xFF,                   // Read for verification (Reading AAh = success)
                                    0xFF };                 // Read PIO pin status

            //Send Data to Node and update owowNode[node].state
            owNode[node].state = (byte)~Update_Node(node, byteBuffer)[1];
            owNode[node].writtenState = bitValue;

            if (debug)
                logWrite("DEBUG: PinWrite : Node# = " + node.ToString() +
                          " : Status = " + owNode[node].state.ToString());
        }

        /// <summary>
        /// Used to set the Node's Pins to a given state
        /// </summary>
        /// <param name="PinStatus">Pin status as a byte:
        /// each position in the byte is a Pin:
        /// 0 = off, 1 = on </param>
        /// <param name="node">Node number as an int</param>
        public void WriteNodeState(int node, byte state)
        {
            byte pinComplement = (byte)~(state);
            byte[] byteBuffer = {   0x5A,                   // 5Ah - Channel access write command
                                    pinComplement,        // Write byte to PIO (complemented because 1 is off)
                                    state,            // Write inverted byte to PIO
                                    0xFF,                   // Read for verification (Reading AAh = success)
                                    0xFF };                 // Read PIO pin status

            //Send Data to Node and update owowNode[node].state
            owNode[node].state = (byte)~Update_Node(node, byteBuffer)[1];
            owNode[node].writtenState = state;

            if (debug)
                logWrite("DEBUG: Set_NodeState : Node# = " + node.ToString() +
                          " : Status = " + owNode[node].state.ToString());
        }

        /// <summary>
        /// Read current state of given node
        /// </summary>
        /// <param name="node">node number</param>
        /// <returns>returns current state as byte 1=on, 2=off</returns>
        public byte ReadState(int node)
        {
            byte[] block = {0xF5,               // F5h - Channel access read command
                            0xFF};              // Read PIO pins

            //Send Data to Node and update owNode[node].state
            owNode[node].state = 0x0;
            owNode[node].state = (byte)~Update_Node(node, block)[1];
            owNode[node].readState = owNode[node].state;

            if (debug)
                logWrite("DEBUG: pinStatus : Node# = " + node.ToString() +
                          " : Status = " + owNode[node].state.ToString());

            //Return State
            return owNode[node].state;
        }

        /// <summary>
        /// Read current State or Latch State of node
        /// </summary>
        /// <param name="node">node number</param>
        /// <param name="latch">true = latch status; false = current state</param>
        /// <returns>returns state/latch state of node as byte 1=on, 0=off</returns>
        public byte ReadState(int node, bool latch)
        {
            if (latch)
            {
                byte[] block = {0xF0,           // FOh - Read PIO registers command
                            0x8A,           // PIO Activity Latch State Register address - lo byte
                            0x00,           // PIO Activity Latch State Register address - hi byte
                            0xFF};          // PIO Activity Latch State is returned in this byte

                //Send Data to Node and update pinLatchStatus
                owNode[node].latchState = (byte)Update_Node(node, block)[3];

                if (debug)
                    logWrite("DEBUG: LatchStatus : Node# = " + node.ToString() +
                              " : Status = " + owNode[node].latchState.ToString());

                //Return State
                return owNode[node].latchState;
            }
            else
            {
                byte[] block = {0xF5,               // F5h - Channel access read command
                                0xFF};              // Read PIO pins

                //Send Data to Node and update owNode[node].state
                owNode[node].state = (byte)~Update_Node(node, block)[1];
                owNode[node].readState = owNode[node].state;

                if (debug)
                    logWrite("DEBUG: pinStatus : Node# = " + node.ToString() +
                              " : Status = " + owNode[node].state.ToString());

                //Return State
                return owNode[node].state;
            }
        }

        public bool TestConnection(int node)
        {
            if (nodeAvailable(owNode[node].address)) return true;
            else return false;
        }

        /// <summary>
        /// Used to get the config file path and name
        /// </summary>
        /// <param name="_node">True returns Node False returns coupler</param>
        /// <returns>Path and File Names</returns>
        public string GetConfigFiles(bool _node)
        {
            if (_node) return path + @"\1_Wire_NodeConfig.txt";
            else return path + @"\1_Wire_CouplerConfig.txt";
        }

        /// <summary>
        /// Used to set the Node position in the file NodeConfig.txt
        /// or to recall the Node position from the file NodeConfig.txt
        /// </summary>
        public void Locate_Nodes()
        {
            logWrite("Started Locate Node");
            //string path = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Desktop\YBOT Field Control Program Files\Setup Files");
            //string file = path + @"\NodeConfig.txt";
            string file = GetConfigFiles(true);
            OneWire_Locate_Nodes.allDone = false;
            OneWire_Locate_Nodes.clicked = false;

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                path = null;
                MessageBox.Show(ex.Message);
            }

            string LineIn = null;
            if (File.Exists(file))                                                  //If file is there
            {
                File.OpenRead(file);                                                //Open the file
                StreamReader NodeConfigFile = new StreamReader(file);               //Read the file
                for (int i = 0; i < device.Length; i++)                             //For a given number of devices
                {
                    LineIn = NodeConfigFile.ReadLine();                             //Read each line
                    owNode[i].address = FromHex(LineIn);                            //Store the Node's address
                }
                NodeConfigFile.Close();                                             //Close the file
            }

            else                                                                    //If the file is not there
            {
                onewireSearch();
                OneWire_Locate_Nodes lt = new OneWire_Locate_Nodes();
                lt.Show();
                while (!OneWire_Locate_Nodes.allDone)
                {
                    Application.DoEvents();                                                 //Do other stuff in windows (respond to clicks)
                    byte[] newDevice;

                    for (int i = 0; i < device.Length; i++)                                 //For all the devices
                    {
                        if (device[i].address != null && !OneWire_Locate_Nodes.allDone)     //If there is a device and Done is not clicked
                        {
                            newDevice = device[i].address;                                  //Set newDevice to the device's address
                            owNode[maxNodes].address = newDevice;                           //Set the Node to the device's address
                            owNode[maxNodes].selected = false; ;
                            PinWrite(maxNodes, State.on);                                 //Turn on the Node's Pins
                            OneWire_Locate_Nodes.NodeID = maxNodes;                         //Set the Node number to last space

                            if (newDevice[0] == 0x29)                                       // If new device is a hobby board
                            {
                                do
                                {
                                    Application.DoEvents();                                 //Respond to clicks
                                    owNode[OneWire_Locate_Nodes.NodeID].address = newDevice; //Set the Node number to the button that was clicked
                                } while (!OneWire_Locate_Nodes.clicked && !OneWire_Locate_Nodes.allDone); //Stop when the button is clicked or done

                                OneWire_Locate_Nodes.clicked = false;                    //Reset the clicked flag to false
                                logWrite("Node# " + OneWire_Locate_Nodes.NodeID + " Found");
                            }
                            PinWrite(maxNodes, State.off);                                        //Turn off the Pins on the Node
                            owNode[maxNodes].address = null;                                //Clear the address
                        }
                    }
                }

                lt.Close();
                FileStream NodeConfig = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write); //Create file NodeConfig.txt
                StreamWriter NodeConfigFile = new StreamWriter(NodeConfig);
                for (int i = 0; i < owNode.Length; i++)                              //For all the Nodes
                {
                    if (owNode[i].address != null)                                   //If there is a Node address
                    {
                        NodeConfigFile.WriteLine(ToHex(owNode[i].address, 0, owNode[i].address.Length)); //Store Node address each line is a new Node
                    }
                    else NodeConfigFile.WriteLine("");                             //If no address then return an empty line
                }
                NodeConfigFile.WriteLine();                                        //Write to file
                NodeConfigFile.Close();                                            //Close the file
            }
            logWrite("Located Nodes Completed");
        }

        /// <summary>
        /// Used to set the coupler position in the file CouplerConfig.txt
        /// or to recall the coupler position from the file CouplerrConfig.txt
        /// </summary>
        public void LocateCoupler()
        {
            //string path = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Desktop\YBOT Field Control Program Files\Setup Files");
            //string file = path + @"\CouplerConfig.txt";
            string file = GetConfigFiles(false);

            OneWire_Locate_Nodes.allDone = false;
            OneWire_Locate_Nodes.clicked = false;

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                path = null;
            }

            string LineIn = null;
            if (File.Exists(file))                                     //If file is there
            {
                File.OpenRead(file);                                   //Open the file
                StreamReader CouplerConfigFile = new StreamReader(file); //Read the file
                for (int i = 0; i < coupler.Length; i++)                             //For a given number of devices
                {
                    LineIn = CouplerConfigFile.ReadLine();                            //Read each line
                    coupler[i].address = FromHex(LineIn);                             //Store the coupler's address
                }
                CouplerConfigFile.Close();                                            //Close the file
            }

            else                                                                    //If the file is not there
            {
                onewireSearch();
                OneWire_Locate_Nodes lt = new OneWire_Locate_Nodes();
                lt.Show();
                while (!OneWire_Locate_Nodes.allDone)
                {
                    Application.DoEvents();                                         //Do other stuff in windows (respond to clicks)
                    byte[] newDevice;

                    for (int i = 0; i < device.Length; i++)                         //For all the devices
                    {
                        if (device[i].address != null && !OneWire_Locate_Nodes.allDone)   //If there is a device and Done is not clicked
                        {
                            newDevice = device[i].address;                          //Set newDevice to the device's address

                            OneWire_Locate_Nodes.couplerID = 0;                            //Set the coupler number to 0

                            if (newDevice[0] == 0x1F)                               // Then it is a switch
                            {
                                byte[] byteBuffer = {   0xCC,     // Connect to Main line
                                                0xFF,       // Read)
                                                0xFF,       // Read
                                                0xFF };     // Read
                                resetMatchROM(newDevice);               // Start the communication
                                touchByte(byteBuffer);              // Send the data
                                do
                                {
                                    Application.DoEvents();                         //Respond to clicks
                                    coupler[OneWire_Locate_Nodes.couplerID].address = newDevice; //Set the coupler number to the button that was clicked
                                } while (!OneWire_Locate_Nodes.clicked && !OneWire_Locate_Nodes.allDone); //Stop when the button is clicked or done

                                OneWire_Locate_Nodes.clicked = false;                    //Reset the clicked flag to false
                                HubAllOff();
                            }

                        }
                    }
                }

                coupler[0].address = null;
                lt.Close();

                FileStream CouplerConfig = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write); //Create file NodeConfig.txt
                StreamWriter CouplerConfigFile = new StreamWriter(CouplerConfig);
                for (int i = 0; i < coupler.Length; i++)                              //For all the couplers
                {
                    if (coupler[i].address != null)                                   //If there is a coupler address
                    {
                        CouplerConfigFile.WriteLine(ToHex(coupler[i].address, 0, coupler[i].address.Length)); //Store coupler address each line is a new coupler
                    }
                    else CouplerConfigFile.WriteLine("");                             //If no address then return an empty line
                }
                CouplerConfigFile.WriteLine();                                        //Write to file
                CouplerConfigFile.Close();                                            //Close the file
            }
            logWrite("Located Couplers Completed");
        }

        /// <summary>
        /// Used to turn on the auxiliary channel on the hub
        /// </summary>
        public void HubAllAuxOn()
        {
            try
            {
                byte[] board;

                for (int i = 1; i < coupler.Length; i++)     //For all the devices
                {
                    board = coupler[i].address;

                    //If coupler address is invalid then reset addresses
                    if (!addressCheck(board))
                    {
                        logWrite("Error - HubAllAuxOn(): coupler address mismatch: Reset coupler Addresses" + " : coupler#: " + i.ToString() + " : Address = "
                                + ToHex(board, 0, board.Length));
                        LocateCoupler();
                        logWrite("Recovered - HubAllAuxOn(): Reset coupler addresses" + " : coupler#: " + i.ToString() + " : Address = "
                                  + ToHex(board, 0, board.Length));
                    }

                    if (board != null)                   // Then it is a coupler
                    {
                        byte[] byteBuffer = { 0x33,        // Connect to Main line
                                        0xFF,           // Read
                                        0xFF,           // Read
                                        0xFF };         // Read
                        resetMatchROM(board);               // Start communication   
                        touchByte(byteBuffer);              // Send data
                    }
                }
            }
            catch
            {
                string error = ("Error in HubAuxOn");
                logWrite(error);
                return;
            }
            if (debug) logWrite("HubAuxOn Completed");
        }

        /// <summary>
        /// Used to turn on the mains on the hub
        /// </summary>
        public void HubAllMainsOn()
        {
            try
            {
                byte[] board;

                if (adapter != null)
                {
                    for (int i = 1; i < coupler.Length; i++)     //For all the devices
                    {
                        board = coupler[i].address;

                        //If coupler address is invalid then reset addresses
                        if (!addressCheck(board))
                        {
                            logWrite("Error - HubAllMainsOn(): coupler address mismatch: Reset coupler Addresses" + " : coupler#: " + i.ToString() + " : Address = "
                                    + ToHex(board, 0, board.Length));
                            LocateCoupler();
                            logWrite("Recovered - HubAllMainsOn(): Reset coupler addresses" + " : coupler#: " + i.ToString() + " : Address = "
                                      + ToHex(board, 0, board.Length));
                        }

                        if (board != null)                   // Then it is a coupler
                        {
                            byte[] byteBuffer = { 0xCC,        // Connect to Main line
                                        0xFF,           // Read
                                        0xFF,           // Read
                                        0xFF };         // Read
                            resetMatchROM(board);               // Start communication   
                            touchByte(byteBuffer);              // Send data
                        }
                    }
                }
            }
            catch
            {
                string error = ("Error in HubMainOn");
                logWrite(error);
                return;
            }
            if (debug) logWrite("HubMainOn Completed");
        }

        /// <summary>
        /// Used to turn on the mains on the hub
        /// </summary>
        public void SelectHubMain(int channelNum)
        {
            try
            {
                byte[] board;

                if (adapter != null && channelNum > 0)
                {
                    board = coupler[channelNum].address;

                    //If coupler address is invalid then reset addresses
                    if (!addressCheck(board))
                    {
                        logWrite("Error - SelectHubMain : coupler address mismatch: Reset coupler Addresses" + " : coupler#: " + channelNum.ToString() + " : Address = "
                                + ToHex(board, 0, board.Length));
                        LocateCoupler();
                        logWrite("Recovered - SelectHubMain: Reset coupler addresses" + " : coupler#: " + channelNum.ToString() + " : Address = "
                                  + ToHex(board, 0, board.Length));
                    }

                    if (board != null)                   // Then it is a coupler
                    {
                        byte[] byteBuffer = { 0xCC,        // Connect to Main line
                                        0xFF,           // Read
                                        0xFF,           // Read
                                        0xFF };         // Read
                        resetMatchROM(board);               // Start communication   
                        touchByte(byteBuffer);              // Send data
                    }
                }
                //onewireSearch();                            //search for 1 wire devices

            }
            catch
            {
                string error = ("Error in SelectHubMain");
                logWrite(error);
                return;
            }
        }

        /// <summary>
        /// Used to turn on the auxiliary channel on the hub
        /// </summary>
        public void SelectHubAux(int channelNum)
        {
            try
            {
                byte[] board;

                if (adapter != null && channelNum > 0)
                {
                    board = coupler[channelNum].address;

                    //If coupler address is invalid then reset addresses
                    if (!addressCheck(board))
                    {
                        logWrite("Error - SelectHubAux : coupler address mismatch: Reset coupler Addresses" + " : coupler#: " + channelNum.ToString() + " : Address = "
                                + ToHex(board, 0, board.Length));
                        LocateCoupler();
                        logWrite("Recovered - SelectHubAux : Reset coupler addresses" + " : coupler#: " + channelNum.ToString() + " : Address = "
                                  + ToHex(board, 0, board.Length));
                    }

                    if (board != null)                   // Then it is a coupler
                    {
                        byte[] byteBuffer = { 0x33,        // Connect to Main line
                                        0xFF,           // Read
                                        0xFF,           // Read
                                        0xFF };         // Read
                        resetMatchROM(board);               // Start communication   
                        touchByte(byteBuffer);              // Send data
                    }
                }
            }
            catch
            {
                string error = ("Error in SelectHubAux");
                logWrite(error);
                return;
            }
        }

        /// <summary>
        /// Turns all the channels off on the hub
        /// </summary>
        public void SelectHubOff(int channelNum)
        {
            try
            {
                byte[] board;

                if (adapter != null && channelNum > 0)
                {
                    board = coupler[channelNum].address;

                    //If coupler address is invalid then reset addresses
                    if (!addressCheck(board))
                    {
                        logWrite("Error - SelectHubOff : coupler address mismatch: Reset coupler Addresses" + " : coupler#: " + channelNum.ToString() + " : Address = "
                                + ToHex(board, 0, board.Length));
                        LocateCoupler();
                        logWrite("Recovered - SelectHubOff : Reset coupler addresses" + " : coupler#: " + channelNum.ToString() + " : Address = "
                                  + ToHex(board, 0, board.Length));
                    }

                    if (board != null)                   // Then it is a coupler
                    {
                        byte[] byteBuffer = { 0x66,        // Connect to Main line
                                        0xFF,           // Read
                                        0xFF,           // Read
                                        0xFF };         // Read
                        resetMatchROM(board);               // Start communication   
                        touchByte(byteBuffer);              // Send data
                    }
                }

            }
            catch
            {
                string error = ("Error in SelectHubOff");
                logWrite(error);
                return;
            }
        }

        /// <summary>
        /// Turns all the channels off on the hub
        /// </summary>
        public void HubAllOff()
        {
            try
            {
                byte[] board;

                for (int i = 1; i < coupler.Length; i++)     //For all the devices
                {
                    board = coupler[i].address;

                    //If coupler address is invalid then reset addresses
                    if (!addressCheck(board))
                    {
                        logWrite("Error - HubAllOff() : coupler address mismatch: Reset coupler Addresses" + " : coupler#: " + i.ToString() + " : Address = "
                                + ToHex(board, 0, board.Length));
                        LocateCoupler();
                        logWrite("Recovered - HubAllOff(): Reset coupler addresses" + " : coupler#: " + i.ToString() + " : Address = "
                                  + ToHex(board, 0, board.Length));
                    }

                    if (board != null)                   // Then it is a device
                    {
                        byte[] byteBuffer = { 0x66,        // Connect to Main line
                                        0xFF,           // Read
                                        0xFF,           // Read
                                        0xFF };         // Read
                        resetMatchROM(board);               // Start communication   
                        touchByte(byteBuffer);              // Send data
                    }
                }
            }
            catch
            {
                string error = ("Error in HubAllOff");
                logWrite(error);
                return;
            }
            if (debug) logWrite("HubAllOff Complete");
        }

        /// <summary>
        /// Starts or resumes communication with a selected Node
        /// </summary>
        /// <param name="node">Node number as int</param>
        public void Select_Node(int node)
        {
            byte[] address = owNode[node].address;   //Stores Node address as an 8 bit byte array

            //If the Node isn't selected clear the NodeSelected array
            if (!owNode[node].selected)
            {
                for (int i = 0; i < owNode.Length; i++)
                {
                    owNode[i].selected = false;
                }
            }

            //If the Node's address is valid and a 1 wire adater is present
            if (adapter != null && owNode[node].address != null)
            {
                if (!addressCheck(address))
                {
                    logWrite("Error - Select_Node: Node address mismatch: Reset Node Addresses" + " : Node#: " + node.ToString() + " : Address = "
                              + ToHex(address, 0, address.Length));
                    Locate_Nodes();
                    logWrite("Recovered - Select_Node: Reset Node addresses" + " : Node#: " + node.ToString() + " : Address = "
                              + ToHex(address, 0, address.Length));

                }

                //Try to start communication with Node at Regular Speed
                try
                {
                    adapter.Speed = OWSpeed.SPEED_REGULAR;
                    adapter.Reset();

                    //If the Node isn't already selected MatchROM
                    if (!owNode[node].selected)
                    {
                        adapter.PutByte(0x55); // Match ROM cmd
                        adapter.DataBlock(address, 0, 8);
                        owNode[node].selected = true;
                        if (debug)
                            logWrite("DEBUG: Select_Node: Start: Node Number = " + node.ToString() +
                                     " : Address = " + ToHex(address, 0, address.Length));
                    }

                    //Else resume communication
                    else
                    {
                        adapter.PutByte(0xA5); // Match ROM cmd
                        if (debug)
                            logWrite("DEBUG: Select_Node: Resume:  Node Number = " + node.ToString() +
                                     " : Address = " + ToHex(address, 0, address.Length));
                    }
                    
                }

                //If it didn't work then record error and return
                catch
                {
                    logWrite("Error: Select_Node: Node Number = " + node.ToString() +
                             " : Address = " + ToHex(address, 0, address.Length));
                    return;
                }
            }
        }

        /// <summary>
        /// Used to Get and Set Data for a given Node
        /// </summary>
        /// <param name="node">Node Number as int</param>
        /// <param name="block">Data to be sent as 8 bit byte array</param>
        /// <returns>Node Status as un-inverted 8 bit byte array</returns>
        public byte[] Update_Node(int node, byte[] block)
        {
            Select_Node(node);  //Select Node to communicate with
            byte[] status = block;   //Byte array to store Node status


            if (adapter != null && owNode[node].address != null)    //If 1 wire adapter is present; else return a null status
            {
                //Try to send data to the Node and return Node status
                try
                {
                    adapter.DataBlock(block, 0, block.Length);
                    status = block;
                    if (debug)
                        logWrite("DEBUG: Update_Node: Node Number = " + node.ToString() +
                                 " : Status = " + ToHex(status, 0, status.Length));
                    return status;
                }
                //If it doesn't work update log files and return a null status
                catch
                {
                    logWrite("Error: Update_Node: Node Number = " + node.ToString() +
                             " : Block = " + ToHex(block, 0, block.Length));
                    Array.Clear(status, 0, status.Length);
                    return status;
                }
            }
            else
            {
                Array.Clear(status, 0, status.Length);
                return status;
            }
        }

        /// <summary>
        /// Used to verify 1 wire address: 
        /// </summary>
        /// <param name="node">Node number as int</param>
        /// <returns>returns true if valid, false if not valid</returns>
        public bool addressCheck(byte[] address)
        {
            uint result = 0;

            if (address != null && address.Length == 8)
            {
                //Try to calculate the CRC8 and check it against the checksum
                //Retrun true if a match: return false if no match
                try
                {

                    result = DalSemi.Utils.CRC8.Compute(address, 0, 7);

                    if (debug)
                        logWrite("DEBUG: Address = " + ToHex(address, 0, address.Length) +
                                 " : CRC = " + result.ToString() + " : Checksum = " + address[7].ToString());

                    if (result == address[7]) return true;
                    else
                    {
                        try
                        {
                            logWrite("Error: addressCheck (checksum did not match): Address = " + ToHex(address, 0, address.Length) +
                                     " : CRC = " + result.ToString() + " : Checksum = " + address[7].ToString());
                            return false;
                        }
                        catch
                        {
                            if (debug) logWrite("DEBUG: addressCheck: No Node Address");
                            return false;
                        }
                    }

                }
                //If it didn't work log the error and return false
                catch
                {
                    try
                    {
                        logWrite("Error: addressCheck (could not calculate checksum): Address = " + ToHex(address, 0, address.Length) +
                                 " : CRC = " + result.ToString() + " : Checksum = " + address[7].ToString());
                        return false;
                    }
                    catch
                    {
                        if (debug) logWrite("DEBUG: addressCheck: No Node Address");
                        return false;
                    }
                }
            }
            else return false;
        }

        /// <summary>
        /// Used to start regular speed communications with the device
        /// </summary>
        /// <param name="address">Device's address as a byte[]</param>
        public void resetMatchROM(byte[] address)
        //Code from 1wire
        {
            if ((address != null) && (address.Length == 8))
            {
                if (adapter != null)
                {
                    try
                    {
                        adapter.Speed = OWSpeed.SPEED_REGULAR;
                        adapter.Reset();
                        adapter.PutByte(0x55); // Match ROM cmd
                        adapter.DataBlock(address, 0, 8);
                        if (debug) logWrite("Error: resetMatchROM: Address = " + ToHex(address, 0, address.Length));
                    }
                    catch
                    {
                        logWrite("Error: resetMatchROM: Address = " + ToHex(address, 0, address.Length));
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Starts communication with device at Over Drive Speed
        /// </summary>
        /// <param name="address">Device's address as a byte[]</param>
        public void resetODMatchROM(byte[] address)
        //Code from 1wire
        {
            if (adapter != null)
            {
                try
                {
                    adapter.Reset();
                    adapter.PutByte(0x69); // OD Match ROM cmd
                    adapter.Speed = OWSpeed.SPEED_OVERDRIVE;
                    adapter.DataBlock(address, 0, 8);
                    if (debug)
                        logWrite("DEBUG: resetODMatchROM: Address = " +
                                 ToHex(address, 0, address.Length));
                }
                catch
                {
                    logWrite("Error: resetODMatchROM: Address = " + ToHex(address, 0, address.Length));
                    return;
                }
            }
        }

        /// <summary>
        /// Sends data to device
        /// </summary>
        /// <param name="block">data to be sent as a byte[]</param>
        public void touchByte(byte[] block)
        {
            if (adapter != null)
            {
                try
                {
                    adapter.DataBlock(block, 0, block.Length);
                    if (debug) logWrite("DEBUG: touchByte: block = " + ToHex(block, 0, block.Length));
                }
                catch
                {
                    logWrite("Error: touchByte: block = " + ToHex(block, 0, block.Length));
                    return;
                }
            }
        }

        /// <summary>
        /// Used to grab devices and set Default Adapter
        /// </summary>
        /// <param name="adapterName">Adapter as a string</param>
        /// <param name="portName">Communication port as a string</param>
        public void setAdapter(string adapterName, string portName)
        //Code from 1 wire
        {
            try
            {
                adapter = AccessProvider.GetAdapter(adapterName, portName);

                if (adapter != null)
                {
                    adapter.BeginExclusive(true);

                    adapter.Speed = OWSpeed.SPEED_REGULAR;
                    adapter.Reset();
                    adapter.PutByte(0x3C);
                    adapter.Speed = OWSpeed.SPEED_OVERDRIVE;
                    adapter.PutByte(0x3C);

                    adapter.Speed = OWSpeed.SPEED_REGULAR;
                    adapter.Reset();
                    adapter.PutByte(0x3C);
                    adapter.Speed = OWSpeed.SPEED_OVERDRIVE;
                    adapter.PutByte(0x3C);

                    adapter.Speed = OWSpeed.SPEED_REGULAR;
                    adapter.Reset();
                    adapter.PutByte(0x3C);
                    if (debug)
                        logWrite("DEBUG: setAdapter: Begin Exclusive; adapter != null : adapterName = " + adapterName +
                                 " : portName = " + portName);
                }

            }
            catch
            {
                if (adapter != null) adapter.Dispose();
                adapter = null;
                if (debug) logWrite("Error: setAdapter : adapterName = " + adapterName + " : portName = " + portName);
            }
        }

        /// <summary>
        /// Used to Change a byte to Hex
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="off"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public string ToHex(byte[] buff, int off, int len)
        //Code from 1 wire
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder(buff.Length * 3);
                sb.Append(buff[off].ToString("X2"));
                for (int i = 1; i < len; i++)
                {
                    sb.Append(" ");
                    sb.Append(buff[off + i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Changes Hex to a byte
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public byte[] FromHex(string s)
        {
            s = System.Text.RegularExpressions.Regex.Replace(s.ToUpper(), "[^0-9A-F]", "");
            byte[] b = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                b[i / 2] = byte.Parse(s.Substring(i, 2),
                   System.Globalization.NumberStyles.AllowHexSpecifier);
            return b;
        }

        /// <summary>
        /// Used to search for 1wire devices
        /// </summary>
        public void onewireSearch()
        //Code from 1wire
        {
            int number = 0;
            for (int i = 0; i < device.Length; i++)
            {
                device[i].address = new byte[8];
            }

            if (adapter != null)
            {
                try
                {
                    adapter.Speed = OWSpeed.SPEED_REGULAR;
                    adapter.TargetAllFamilies();
                    byte[] add = new byte[8];

                    if (adapter.GetFirstDevice(add, 0))
                    {
                        do
                        {
                            for (int i = 0; i < add.Length; i++)
                            {
                                device[number].address[i] = add[i];
                            }
                            number++;
                        }
                        while (adapter.GetNextDevice(add, 0));
                    }
                }
                catch
                {
                    logWrite("Error in onewireSearch");
                    return;
                }
            }
            logWrite("onewireSearch Complete");
        }

        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="text">Text as string</param>
        private void logWrite(string text)
        {
            lw.WriteLog(text, "1Wire_Log", debug);
        }

        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="text">Text as string</param>
        private void errorLog(string text)
        {
            lw.WriteLog(text, "1Wire_Error_Log", debug);
        }

        /// <summary>
        /// Used to verify regular speed communications with the device
        /// </summary>
        /// <param name="address">Device's address as a byte[]</param>
        public bool nodeAvailable(byte[] address)
        //Code from 1wire
        {
            if (adapter != null)
            {
                try
                {
                    adapter.Speed = OWSpeed.SPEED_REGULAR;
                    adapter.Reset();
                    adapter.PutByte(0x55); // Match ROM cmd
                    adapter.DataBlock(address, 0, 8);
                    if (debug) logWrite("resetMatchROM: Address = " + ToHex(address, 0, address.Length));
                    return true;
                }
                catch
                {
                    logWrite("Error: resetMatchROM: Address = " + ToHex(address, 0, address.Length));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Field Controls

        /// <summary>
        /// Starts all communications with field
        /// </summary>
        public void StartUp()
        {
            this.HubAllMainsOn();  //Turn on 1-wire hub's mains
        }

        /// <summary>
        /// Turns off field and field communications
        /// </summary>
        public void ShutDown()
        {
            this.FieldAllOff();     //Turn off all pins on the field
            this.HubAllOff();       //Turn off all hubs
        }

        /// <summary>
        /// Check the state of given pin on given node
        /// From the last read or written state
        /// </summary>
        /// <param name="node">node number</param>
        /// <param name="pin">pin number</param>
        /// <returns>returns pin's state true=on, false=off</returns>
        public bool PinState(int node, int pin)
        {
            byte _currentState = this.owNode[node].state;       //Set _currentState to current nodes state

            byte _bitValue = 0;

            if (pin != 0)                                       //If pin is not zero
            {
                pin -= 1;                                       // Pin was selected with "people" number. Processing needs bit position (1 less).
                _bitValue = (byte)Math.Pow(2.0, pin);           // This returns the value of the bit positon
            }
            if ((_currentState & _bitValue) != _bitValue)       // the Pin is off
            {
                return false;
            }
            else
            {
                return true;                                    // the Pin must be on
            }
        }

        /// <summary>
        /// Check the given pin state or latch state on given node
        /// From the last read or written state
        /// </summary>
        /// <param name="node">node number</param>
        /// <param name="pin">pin number</param>
        /// <param name="latch">true = latch status; false = pin's status</param>
        /// <returns>returns pin's state or latch status</returns>
        public bool PinState(int node, int pin, bool latch)
        {
            byte pinStatus;

            if (!latch)
            {
                pinStatus = owNode[node].state;         //Set to last known state of the node
            }
            else
            {
                pinStatus = owNode[node].latchState;     //Set to the last know latch state of the node
            }

            byte bitValue = 0;
            if (pin != 0)
            {
                pin -= 1;                                     // pin was selected with "people" number. Processing needs bit position (1 less).
                bitValue = (byte)Math.Pow(2.0, pin);          // This returns the value of the bit positon
            }
            if ((pinStatus & bitValue) != bitValue)             // the pin is off
            {
                return false;
            }
            else
            {
                return true;                           // the pin must be on
            }
        }

        /// <summary>
        /// Turn on given light on given node and not change state of other pins
        /// </summary>
        /// <param name="node">Node number</param>
        /// <param name="lightPin">Light's pin number</param>
        /// <param name="state">true = on; false = off</param>
        public void Light(int node, LightColor _color, State _state)
        {
            int lightPin = 0;
            if (_color == LightColor.all) Light(node, State.on, State.on, State.on, State.on);
            else if (_color == LightColor.red) lightPin = red;
            else if (_color == LightColor.green) lightPin = green;
            else if (_color == LightColor.yellow) lightPin = yellow;
            else if (_color == LightColor.blue) lightPin = blue;
            else Light(node, State.off, State.off, State.off, State.off);

            if (lightPin != 0)
            {
                byte _currentState = this.owNode[node].state;            //Set _currentState to node's state

                lightPin -= 1;                                              // Pin was selected with "people" number. Processing needs bit number (1 less).
                if (lightPin > -1)
                {
                    byte _bitValue = (byte)Math.Pow(2.0, lightPin);             // This returns the value of the bit positon

                    if (_state == State.on) _currentState = (byte)(_currentState | _bitValue);   // Change selected Pin but maintain other Pins status
                    else _currentState = (byte)(_currentState & ~_bitValue);        // Change selected Pin but maintain other Pins status

                    this.WriteNodeState(node, _currentState);                        //Write new state to node keeping buttons from latching on
                }
            }
        }

        /// <summary>
        /// Turns on multiple lights at the same time and not change state of other pins
        /// </summary>
        /// <param name="node">Node number</param>
        /// <param name="red">true = on, false = off</param>
        /// <param name="green">true = on, false = off</param>
        /// <param name="blue">true = on, false = off</param>
        /// <param name="yellow">true = on, false = off</param>
        public void Light(int node, State _red, State _green, State _blue, State _yellow)
        {
            byte _currentState = this.owNode[node].state;                        //Set current tower state
            byte _state = (byte)(_currentState & ~0XF);                             //Set all light pins off

            int r = red;
            int g = green;
            int b = blue;
            int y = yellow;

            if (_red == State.on) _state = (byte)(_state | (byte)(Math.Pow(2.0, (r -= 1))));     //If red set red bit high
            if (_green == State.on) _state = (byte)(_state | (byte)(Math.Pow(2.0, (g -= 1))));   //If green set green bit high
            if (_blue == State.on) _state = (byte)(_state | (byte)(Math.Pow(2.0, (b -= 1))));    //If blue set blue bit high
            if (_yellow == State.on) _state = (byte)(_state | (byte)(Math.Pow(2.0, (y -= 1))));  //If yellow set yellow bit high

            this.WriteNodeState(node, _state);                                      //Write to node
        }

        /// <summary>
        /// Write all light pins to given state and not change state of other pins
        /// </summary>
        /// <param name="state">true = on ; false = off</param>
        public void AllLights(State _state)
        {
            for (int n = 0; n <= this.maxNodeNum; n++)                              //For all towers
            {
                if (n != cv.controlBoard)                                     //If the node isn't the control board
                {
                    byte _currentState = this.owNode[n].state;                   //Set _current state = node's state

                    if (_state == State.on)                                                      //If on
                    {
                        _currentState = (byte)(_currentState | 0XF);                //Set all light pins on
                        this.WriteNodeState(n, _currentState);                      //Write state to node
                    }
                    else
                    {
                        _currentState = (byte)(_currentState & ~0XF);               //Set all light pins off
                        this.WriteNodeState(n, _currentState);                      //Write state to node
                    }
                }
            }
        }

        /// <summary>
        /// Turn off all field components
        /// </summary>
        public void FieldAllOff()
        {
            for (int n = 0; n <= maxTowerNum; n++)                      //For all nodes
            {
                this.WriteNodeState(n, 0x0);                                    //Write all pins off
            }
        }

        /// <summary>
        /// Clear all node tower latches
        /// </summary>
        public void AllTowersLatchReset()
        {
            for (int i = 0; i <= this.maxTowerNum; i++)
            {
                if (i != cv.controlBoard) this.ResetActivityLatch(i);
            }
        }

        /// <summary>
        /// Rind Field Bell
        /// </summary>
        public void RingBell()
        {
            PinWrite(cv.controlBoard, cv.bell, State.on);
            this.PinWrite(cv.controlBoard, cv.bell, State.off);
            Thread.Sleep(300);
            this.PinWrite(cv.controlBoard, cv.bell, State.on);
            this.PinWrite(cv.controlBoard, cv.bell, State.off);
        }

        /// <summary>
        /// Sound Buzzer
        /// </summary>
        public void SoundBuzzer()
        {
            this.PinWrite(cv.controlBoard, cv.buzzer, State.on);
            Thread.Sleep(750);
            this.PinWrite(cv.controlBoard, cv.buzzer, State.off);
        }


        /// <summary>
        /// Turn on team's transmitters to automode or manualmode
        /// </summary>
        /// <param name="team">red, green, both</param>
        /// <param name="autonomousState">true = automode, false = manual mode</param>
        /// <param name="transmitterState">true = controllers on, false = controllers off</param>
        public void RobotTransmitters(int _nodeNum, State autonomousState, State transmitterState)
        {
                this.PinWrite(cv.auto_driverRelay, _nodeNum, autonomousState);
                this.PinWrite(cv.transmitterRelay, _nodeNum, transmitterState);
        }
        #endregion
    }
}
