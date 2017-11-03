using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;


//Currently a WIP
namespace YbotFieldControl
{
    public class XBee

    {
        public struct XBeeNode { public string highAddress; public string lowAddress; public string nodeID; public string data; };   //XBeeNode Structure
        public XBeeNode[] xbNode = new XBeeNode[30];                     //Create Node
        public StringBuilder xbReceivedData = new StringBuilder();       //Recieved Data on Xbee port
        public SerialPort xbPort = new SerialPort("COM4", 115200);       //XBee Port Default Com port
        public string DataIn;   //Data Strings
        LogWriter xbLog = new LogWriter();

        /// <summary>
        /// Recieves Data from XBee Port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;     //New Serial Port
            DataIn = null;                          //Null Data String
            try
            {
                xbReceivedData.Clear();                 //Clear previous data
                xbReceivedData.Append(sp.ReadLine());   //Add new recieved data
                DataIn = xbReceivedData.ToString();     //Store in DataIn
            }
            catch { }

            xbLog.WriteLog(DataIn, "xbLogs");

        }

        /// <summary>
        /// Starts XBee Port to recieve data
        /// </summary>
        public void Startup()
        {
            //If port is null use default port
            if (xbPort.PortName == null) xbPort.PortName = "COM4";
            //If port is not open and port is not null
            if (!xbPort.IsOpen)
            {
                try
                {
                    xbPort.Open();  //Open Port
                    xbPort.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);   //Start receiving data
                }
                catch
                {
                }
            }

        }

        /// <summary>   
        /// Starts XBee communication on a given port 
        /// </summary>
        /// <param name="port"> XBee Com Port </param>
        public void Startup(string port)
        {
            //If port is null use default port
            if (port != null) xbPort.PortName = port;

            //If port is not open and port is not null
            if (!xbPort.IsOpen && port != null)
            {
                try
                {
                    xbPort.Open();  //Open Port
                    xbPort.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);   //Start receiving data
                }
                catch
                {
                }
            }

        }

        /// <summary>
        /// Closes XBee Port
        /// </summary>
        public void Shutdown()
        {
            //If port is onen close the port
            if (xbPort.IsOpen) xbPort.Close();
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
                if (xbPort.IsOpen)
                {
                    //Send data
                    xbPort.Write(_data + "\r\n");
                }
            }
            catch { };
        }

    }
}
