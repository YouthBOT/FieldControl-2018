using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.Windows.Forms;
using System.Data;

namespace YbotFieldControl
{
    public partial class Canbus
    {

        #region Set Up
        /// <summary>
        /// Searches for the CANBUS Port
        /// </summary>
        public void FindPort()
        {
            foreach (string portname in SerialPort.GetPortNames())
            {
                this.Startup(portname);
                if (this.canPort.IsOpen)
                {
                    this.newData = false;    //Reset flag
                    string s = string.Format("{0},5", commandNode);
                    this.Send(s);      //Test for command node
                    Thread.Sleep(100); //Wait
                    if (this.newData)
                    {
                        return;//If there is new data
                    }
                    else
                    {
                        this.Shutdown(portname);    //Else shutdown the port
                        this.logWrite(portname + " - Not CANBUS");
                    }
                }
            }
        }

        /// <summary>
        /// Starts XBee Port to recieve data
        /// </summary>
        public void Startup()
        {
            //If port is null find the port
            if (this.canPort.PortName == null) FindPort();
            //If port is not open and port is not null
            else if (!this.canPort.IsOpen)
            {
                try
                {
                    this.canPort.BaudRate = baud;
                    this.canPort.ReadTimeout = 500;
                    this.canPort.WriteTimeout = 500;
                    this.canPort.RtsEnable = rts;
                    this.canPort.DtrEnable = dtr;

                    this.canPort.Open();  //Open Port
                    this.getCanData = true;
                    this.readPort = new Thread(Port_DataReceived);
                    this.readPort.Start();
                    this.logWrite("CANBUS Started on " + this.canPort.PortName);
                }
                catch (Exception ex) { this.errorLog("CANBUS-StartUp() - " + ex); }
            }

        }

        /// <summary>   
        /// Starts XBee communication on a given port 
        /// </summary>
        /// <param name="port"> XBee Com Port </param>
        public void Startup(string port)
        {

            if (port != null)
            {
                try
                {
                    this.canPort.PortName = port;

                    if (!this.canPort.IsOpen)
                    {
                        this.canPort.BaudRate = baud;
                        this.canPort.ReadTimeout = 500;
                        this.canPort.WriteTimeout = 500;
                        this.canPort.RtsEnable = rts;
                        this.canPort.DtrEnable = dtr;

                        this.canPort.Open();  //Open Port
                        this.getCanData = true;
                        this.readPort = new Thread(Port_DataReceived);
                        this.readPort.Start();
                        this.logWrite("CANBUS Started on " + this.canPort.PortName);
                    }
                }
                catch (Exception ex) { this.errorLog("CANBUS-StartUp() - " + ex); }
            }
        }

        /// <summary>
        /// Closes XBee Port
        /// </summary>
        public void Shutdown()
        {
            try
            {
                foreach (string portname in SerialPort.GetPortNames())
                {

                    if (this.canPort.PortName == portname)
                    {
                        if (this.canPort.IsOpen)
                        {
                            this.getCanData = false;
                            this.readPort.Join();
                            this.canPort.Close();
                        }
                        this.logWrite("CANBUS Stopped on " + this.canPort.PortName);
                    }
                }
            }
            catch (Exception ex) { this.errorLog("CANBUS-ShutDown() - " + ex); }
        }

        /// <summary>
        /// Closes the given comport
        /// </summary>
        /// <param name="port">Comport</param>
        public void Shutdown(string port)
        {
            try
            {
                foreach (string portname in SerialPort.GetPortNames())
                {
                    if (this.canPort.PortName == port)
                    {
                        if (this.canPort.IsOpen)
                        {
                            getCanData = false;
                            readPort.Join();
                            this.canPort.Close();
                        }
                        this.logWrite("CANBUS Stopped on " + this.canPort.PortName);
                    }
                }

            }
            catch (Exception ex) { this.errorLog("CANBUS-Shutdown() - " + ex); }
        }
        #endregion

        #region Data Processing
        /// <summary>
        /// Recieves Data from XBee Port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Port_DataReceived()
        {
            SerialPort sp = this.canPort;             //Get Serial Port that has the data
            CanMessageEventArgs newMessage = new CanMessageEventArgs();


            while (getCanData)
            {
                string dataIn = null;
                try
                {
                    dataIn = sp.ReadLine();

                    if (dataIn != null)
                    {
                        this.messagesReceived++;
                        newMessage.canMessage = dataIn;
                        if (CanMessageReceived != null) CanMessageReceived(this, newMessage);

                        this.newData = true;

                        DateTime now = DateTime.Now;
                        string time = now.TimeOfDay.ToString();
                        string s = string.Format("Rcvd {0}: {1} : {2}",messagesReceived, time, dataIn);
                        Console.WriteLine(s);
                        this.logBuilder.AppendLine(s);
                    }
                }
                catch (Exception ex)
                {
                    //this.errorLog("CANBUS-Port_DataReceived() - " + ex);
                }
            }


        }

        /// <summary>
        /// Sends data to destination node
        /// </summary>
        /// <param name="_data">Data as a string</param>
        public void Send(string _data)
        {
            try
            {
                //If port is opne
                if (this.canPort.IsOpen)
                {
                    //Send data
                    this.messagesSent++;
                    this.canPort.WriteLine("$," + _data);

                    DateTime now = DateTime.Now;
                    string time = now.TimeOfDay.ToString();
                    string s = string.Format("Sent {0}: {1} : {2}",messagesSent, time, _data);
                    Console.WriteLine(s);
                    this.logBuilder.AppendLine(s);
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex) { this.errorLog("CANBUS-Send(_data) - " + ex); };

        }

        /// <summary>Tests connection.</summary>
        ///
        /// <param name="nodeNum">The node number.</param>
        ///
        /// <returns>true if the test passes, false if the test fails.</returns>
        public void TestConnection(int _nodeAddress)
        {
            string s = string.Format("{0},0", _nodeAddress);
            this.Send(s);
            Thread.Sleep(100);
        }
        #endregion

        #region Light Controls
        /// <summary>
        /// Sends The Light Test string to Node
        /// </summary>
        /// <param name="_nodeID">Node's Address</param>
        public void LightTest(int nodeAddress)
        {
            this.newData = false;
            string s = string.Format("{0},1,5,5,0", nodeAddress);
            this.Send(s);
        }

        /// <summary>
        /// Set's light to given color on given node
        /// </summary>
        /// <param name="_nodeID">Node's Address</param>
        /// <param name="_color">Color value</param>
        public void Light(int nodeAddress, LightColor _color)
        {
            this.newData = false;
            if (_color == LightColor.all)
            {
                this.Light(nodeAddress, LightColor.yellow, LightColor.blue, LightColor.green, LightColor.red);
            }
            else
            {
                int clr = (int)_color;
                string s = string.Format("{0},1,{1},1", nodeAddress, clr);
                this.Send(s);
            }
        }

        /// <summary>
        /// Set's each ring on the given node to the given color
        /// </summary>
        /// <param name="_nodeID">Node's Address</param>
        /// <param name="_ring1">Color value</param>
        /// <param name="_ring2">Color value</param>
        /// <param name="_ring3">Color value</param>
        /// <param name="_ring4">Color value</param>
        public void Light(int _nodeAddress, LightColor _ring1, LightColor _ring2, LightColor _ring3, LightColor _ring4)
        {
            this.newData = false;
            string s = string.Format("{0},1,{1},1,1", _nodeAddress, (int)_ring1);
            this.Send(s);
            s = string.Format("{0},1,{1},1,2", _nodeAddress, (int)_ring2);
            this.Send(s);
            s = string.Format("{0},1,{1},1,3", _nodeAddress, (int)_ring3);
            this.Send(s);
            s = string.Format("{0},1,{1},1,4", _nodeAddress, (int)_ring4);
            this.Send(s);
        }
        #endregion

        #region Output Controls
        /// <summary>
        /// Set's node's Output board to a given state
        /// </summary>
        /// <param name="_nodeID">Node's Address</param>
        /// <param name="_state">Byte that represents the value of each Output as 1(on) 0(off)</param>
        public void SetOutputState(int _nodeAddress, byte _state)
        {
            this.newData = false;
            this.Send(_nodeAddress + ",4,0," + _state);
        }

        /// <summary>
        /// Sets Output on given node to given state
        /// </summary>
        /// <param name="_nodeID">Node's Address</param>
        /// <param name="_outputNum">Output Number</param>
        /// <param name="_state">on or off</param>
        public void SetOutput(int _nodeAddress, int _outputNum, State _state)
        {
            this.newData = false;
            if (_state == State.on)
            {
                this.Send(_nodeAddress + ",4," + _outputNum + ",1");
            }
            else
            {
                this.Send(_nodeAddress + ",4," + _outputNum + ",0");
            }
        }

        /// <summary>
        /// Turn on team's transmitters to automode or manualmode
        /// </summary>
        /// <param name="team">red, green, both</param>
        /// <param name="autonomousState">true = automode, false = manual mode</param>
        /// <param name="transmitterState">true = controllers on, false = controllers off</param>
        public void RobotTransmitters(int _nodeAddress, State _autoState, State _transmitterState)
        {
            if (_transmitterState == State.on)
            {
                if (_autoState == State.on) Send(_nodeAddress + ",3,1");
                else this.Send(_nodeAddress + ",3,2");
            }
            else this.Send(_nodeAddress + ",3,0");
        }
        #endregion

        #region Misc Functions

        /// <summary>
        /// Changes Game Mode on given node
        /// </summary>
        /// <param name="_nodeID">Node's Address</param>
        /// <param name="mode">Game Mode Value</param>
        public void ChangeGameMode(int _nodeAddress, GameModes mode)
        {
            string s = string.Format("{0},2,{1}", _nodeAddress, (int)mode);
            this.Send(s);
        }

        public void Report()
        {
            this.Send("0,0");
        }

        public void Report(int _nodeAddress)
        {
            string s = string.Format("{0},0", _nodeAddress);
            this.Send(s);
        }

        /// <summary>
        /// Returns the color's name given the value
        /// </summary>
        /// <param name="_color">Color Value as a number string</param>
        /// <returns>Color Name</returns>
        public string colorCode(string _color)
        {
            if (_color != null)
            {
                int c = Convert.ToInt32(_color);

                if (c == (int)LightColor.red) return LightColor.red.ToString();
                else if (c == (int)LightColor.green) return LightColor.green.ToString();
                else if (c == (int)LightColor.yellow) return LightColor.yellow.ToString();
                else if (c == (int)LightColor.blue) return LightColor.blue.ToString();
                else if (c == (int)LightColor.white) return LightColor.white.ToString();
                else return LightColor.off.ToString();
            }
            else return LightColor.off.ToString();
        }

        public string gameModeCode(string _mode)
        {
            if (_mode != null)
            {
                int m = Convert.ToInt32(_mode);

                if (m == (int)GameModes.reset) return GameModes.reset.ToString();
                else if (m == (int)GameModes.ready) return GameModes.ready.ToString();
                else if (m == (int)GameModes.start) return GameModes.start.ToString();
                else if (m == (int)GameModes.autonomous) return GameModes.autonomous.ToString();
                else if (m == (int)GameModes.mantonomous) return GameModes.mantonomous.ToString();
                else if (m == (int)GameModes.manual) return GameModes.manual.ToString();
                else if (m == (int)GameModes.end) return GameModes.end.ToString();
                else if (m == (int)GameModes.off) return GameModes.off.ToString();
                else if (m == (int)GameModes.debug) return GameModes.debug.ToString();
                else return GameModes.off.ToString();
            }
            else return GameModes.off.ToString();
        }
        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="text">Text as string</param>
        public void logWrite(string text)
        {
            this.lw.WriteLog(text, "CANBUS_Log");

        }

        public void writeLogs()
        {
            this.lw.Log(logBuilder.ToString(), "CANBUS_Log");
            Form frm = Application.OpenForms["CANRawData"];
            if (frm != null)
            {
                this.canRaw.displayText(logBuilder.ToString());
            }
            logBuilder.Clear();
        }

        public void writeLogs(string path)
        {
            this.lw.Log(logBuilder.ToString(), "CANBUS_Log");
            this.lw.Log(logBuilder.ToString(), "CANBUS_Log", path);
            Form frm = Application.OpenForms["CANRawData"];
            if (frm != null)
            {
                this.canRaw.displayText(logBuilder.ToString());
            }
            logBuilder.Clear();
        }

        /// <summary>
        /// Writes text to log file
        /// </summary>
        /// <param name="text">Text as string</param>
        private void errorLog(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Launches Data Monitor Window
        /// </summary>
        public void monitorData()
        {
            Form frm = Application.OpenForms["CANRawData"];
            if (frm == null)
            {
                this.canRaw = new CANRawData();
                this.canRaw.Show();
            }
        }

        public void stopMonitoringData()
        {
            Form frm = Application.OpenForms["CANRawData"];
            if (frm != null)
            {
                this.canRaw.Close();
            }
        }

        public void StartFosCalibration(int _nodeAddress)
        {
            string s = string.Format("{0},7,1,9,", _nodeAddress);
            this.Send(s);
        }
        #endregion
    }
}
