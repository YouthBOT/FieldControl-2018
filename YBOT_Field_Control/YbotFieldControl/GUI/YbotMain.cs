using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using YBotSqlWrapper;

namespace YbotFieldControl
{
    public partial class YBOT_Main : Form
    {
        #region Variables
        fileStructure fs = new fileStructure();
        public FieldControl fc = new FieldControl();

        string filePath                         //Construct filePath to Node data
        {
            get
            {
                return fs.xmlFilePath;
            }
        }

        string xmlHeader                        //Construct xml Header
        {
            get
            {
                return fs.xmlHeader;
            }
        }

        public bool finishedStartup = false;    //Finished Flag

        private int selectedTower = 0;
        #endregion

        #region Start Up/Shutdown
        public YBOT_Main()
        {
            InitializeComponent();

            try
            {
				this.nodeDS.ReadXml (filePath, XmlReadMode.ReadSchema); 			//Read Node XML File into the Data Set
                this.nodeDG.DataSource = this.nodeDS;                               //Set DataSet as Data Source for Grid
                this.nodeDG.DataMember = xmlHeader;                                 //Populate Grid with data

                this.nodeDG.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void YBOT_Main_Shown(object sender, EventArgs e)
        {
            this.fc.SetUp();
            updateAddress();

            //For Each Node in Data Set
            foreach (DataGridViewRow row in this.nodeDG.Rows)
            {
                try
                {
                    if (row.Cells["Node_Type"].Value != null)                                           //If Node Type is something
                    {

                        string nodeType = row.Cells["Node_Type"].Value.ToString();                      //Get Node Type
                        int nodeID = Convert.ToInt32(row.Cells["Node_ID"].Value);                       //Get node ID as int
                        row.Cells["Node_Status"].Value = "Checking Connection";                         //Display Checking Connections
                        Application.DoEvents();

                        if (this.fc.TestConnections(nodeID))                                            //If the node is available
                        {
                            row.Cells["Node_Status"].Value = "Connected";                               //Report Connected to Data Grid
                        }
                        else
                        {
                            row.Cells["Node_Status"].Value = "Not Connected";                           //Else it isn't connected
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

            updateDisplay(this, null);
            refreshPortList();

            var passwordFile = Path.Combine (fs.setupFilePath, "sql-password.txt");
            var password = string.Empty;
            if (File.Exists (passwordFile)) {
                var lines = File.ReadAllLines (passwordFile);
                password = lines[0];
            }

            YbotSql.Instance.SqlConnectedEvent += OnSqlConnect;
            YbotSql.Instance.SqlMessageEvent += OnSqlMessage;
            YbotSql.Instance.Connect ("149.56.109.90", password, false);
			//YbotSql.Instance.Connect("127.0.0.1", string.Empty, false);
        }

        private void YBOT_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.fc.writeLogs();
            this.fc.StopRawData(ComModes.canBus);
            this.fc.FieldAllOff();
            this.fc.ShutDown();
            YbotSql.Instance.Disconnect ();
            Application.Exit();
        }

        private void OnSqlConnect (object sender) {
            sqlConnectButton.Invoke ((MethodInvoker)delegate () {
                sqlConnectButton.Text = "SQL Database Connected";
                sqlConnectButton.FlatStyle = FlatStyle.Flat;
                sqlConnectButton.Enabled = false;
            });

            YbotSql.Instance.GetGlobalData ();
            YbotSql.Instance.SqlConnectedEvent -= OnSqlConnect;
            YbotSql.Instance.SqlMessageEvent -= OnSqlMessage;
        }

        private void OnSqlMessage (object sender, SqlMessageArgs args) {
            if ((args.type == SqlMessageType.Exception) && (!YbotSql.Instance.IsConnected)) {
                sqlConnectButton.Invoke ((MethodInvoker)delegate () {
                    sqlConnectButton.Text = "Connect to SQL Database";
                    sqlConnectButton.FlatStyle = FlatStyle.Standard;
                    sqlConnectButton.Enabled = true;
                });
                MessageBox.Show ("Failed to connect to Sql Server");
                YbotSql.Instance.SqlMessageEvent -= OnSqlMessage;
            }
        }

        private void sqlConnectButton_Click (object sender, EventArgs e) {
            SqlConnectWindow sqlConnect = new SqlConnectWindow ();
            sqlConnect.Show ();
        }

        #endregion

        #region Data Source/Grid Buttons
        private void btnAddNewNode_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newNode = this.nodeDS.Tables[xmlHeader].NewRow();    //Set up new row
                this.nodeDS.Tables[xmlHeader].Rows.Add(newNode);             //Add new row to data set
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void btnTestNodeConnections_Click(object sender, EventArgs e)
        {
            //For Each Node in Data Set
            foreach (DataGridViewRow row in this.nodeDG.Rows)
            {
                try
                {
                    if (row.Cells["Node_Type"].Value != null)                                           //If Node Type is something
                    {

                        string nodeType = row.Cells["Node_Type"].Value.ToString();                      //Get Node Type
                        int nodeID = Convert.ToInt32(row.Cells["Node_ID"].Value);                       //Get node ID as int
                        row.Cells["Node_Status"].Value = "Checking Connection";                         //Display Checking Connections
                        Application.DoEvents();

                        if (this.fc.TestConnections(nodeID))                                             //If the node is available
                        {
                            row.Cells["Node_Status"].Value = "Connected";                               //Report Connected to Data Grid
                        }
                        else
                        {
                            row.Cells["Node_Status"].Value = "Not Connected";                           //Else it isn't connected
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
        }

        private void btnSetAllCAN_Click(object sender, EventArgs e)
        {
            //For Each Node in Data Set
            foreach (DataGridViewRow row in this.nodeDG.Rows)
            {
                try
                {
                    if (row.Cells["Node_Type"].Value != null)                       //If node type is something
                    {
                        row.Cells["Node_Type"].Value = "CANBUS";                    //Set it to CANBUS
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
            updateData();
        }

        private void btnUpdateFile_Click(object sender, EventArgs e)
        {
            updateData();
        }

        private void btnReloadFile_Click(object sender, EventArgs e)
        {
            try
            {
                this.nodeDS.Clear();                     //Clear data base
                this.nodeDS.ReadXml(filePath);           //Read Node XML File into the Data Set
                this.nodeDG.DataMember = xmlHeader;      //Populate Grid with data
                this.fc.SetUp();
                updateAddress();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void nodeDG_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //If the right mouse button was pressed
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex != -1)
                {

                    int selectedRow = e.RowIndex;                   //Set the selected row
                    this.nodeDG.ClearSelection();                   //Clear all other selected rows
                    this.nodeDG.Rows[selectedRow].Selected = true;  //Set this one as selected
                    this.cmGridRightClick.Show(Cursor.Position);    //Display at the current cursor posistion
                }
            }
        }

        private void tsmDeleteRow_Click(object sender, EventArgs e)
        {
            int rowToDel = this.nodeDG.Rows.GetFirstRow(DataGridViewElementStates.Selected); //Get Selected Row
            this.nodeDG.Rows.RemoveAt(rowToDel);                                             //Remove that row
            this.nodeDG.ClearSelection();                                                    //Clear selection
        }

        private void cANBUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowSelected = this.nodeDG.Rows.GetFirstRow(DataGridViewElementStates.Selected);  //Get Selected row
            try
            {
                this.nodeDG.Rows[rowSelected].Cells["Node_Type"].Value = "CANBUS";               //Set Node type to CANBUS
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            this.nodeDG.ClearSelection();
        }

        private void nodeDG_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //If the left mouse button was pressed
            if (e.Button == MouseButtons.Left)
            {
                if (e.RowIndex != -1)
                {

                    int rowSelected = e.RowIndex;                   //Set the selected row
                    this.nodeDG.ClearSelection();                   //Clear all other selected rows
                    this.nodeDG.Rows[rowSelected].Selected = true;  //Set this one as selected
                    string nodeID = this.nodeDG.Rows[rowSelected].Cells["Node_ID"].Value.ToString();
                    selectedTower = Convert.ToInt16(nodeID);
                }
            }
        }

        private void updateAddress()
        {
            //For Each Node in Data Set
            foreach (DataGridViewRow row in this.nodeDG.Rows)
            {
                if (row.Cells[0].Value != DBNull.Value)
                {
                    try
                    {
                        if (row.Cells["Node_Type"].Value != null)                                           //If Node Type is something
                        {
                            int nodeID = Convert.ToInt32(row.Cells["Node_ID"].Value);                       //Get node ID as int
                            string newAddress = this.fc.GetNodeAddress(nodeID);
                            row.Cells["Node_Address"].Value = newAddress;
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void updateData()
        {
            try
            {
                nodeDS.WriteXml(filePath, XmlWriteMode.WriteSchema);                  //Save Data set to XML File
                fc.SetUp();
                updateAddress();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        #region Light Buttons
        private void btnLightTest_Click(object sender, EventArgs e)
        {
            this.fc.LightTest(selectedTower);
            this.nodeDG.Rows[selectedTower].Cells["Light_State"].Value =
                this.fc.node[selectedTower].lightStatus;
        }

        private void btnLightRed_Click(object sender, EventArgs e)
        {
            this.fc.Light(selectedTower, LightColor.red);
            this.nodeDG.Rows[selectedTower].Cells["Light_State"].Value =
                this.fc.node[selectedTower].lightStatus;

        }

        private void btnLightGreen_Click(object sender, EventArgs e)
        {
            this.fc.Light(selectedTower, LightColor.green);
            this.nodeDG.Rows[selectedTower].Cells["Light_State"].Value =
                this.fc.node[selectedTower].lightStatus;
        }

        private void btnLightBlue_Click(object sender, EventArgs e)
        {
            this.fc.Light(selectedTower, LightColor.blue);
            this.nodeDG.Rows[selectedTower].Cells["Light_State"].Value =
                this.fc.node[selectedTower].lightStatus;
        }

        private void btnLightYellow_Click(object sender, EventArgs e)
        {
            this.fc.Light(selectedTower, LightColor.yellow);
            this.nodeDG.Rows[selectedTower].Cells["Light_State"].Value =
                this.fc.node[selectedTower].lightStatus;
        }

        private void btnLightsOff_Click(object sender, EventArgs e)
        {
            this.fc.Light(selectedTower, LightColor.off);
            this.nodeDG.Rows[selectedTower].Cells["Light_State"].Value =
                this.fc.node[selectedTower].lightStatus;
        }

        private void btnLightWhite_Click(object sender, EventArgs e)
        {
            this.fc.Light(selectedTower, LightColor.white);
            this.nodeDG.Rows[selectedTower].Cells["Light_State"].Value =
                this.fc.node[selectedTower].lightStatus;
        }

        private void btnLightAll_Click(object sender, EventArgs e)
        {
            this.fc.Light(selectedTower, LightColor.all);
            this.nodeDG.Rows[selectedTower].Cells["Light_State"].Value =
                this.fc.node[selectedTower].lightStatus;
        }

        private void btnTestAllNodesLights_Click(object sender, EventArgs e)
        {
            if (this.btnTestAllNodesLights.BackColor == DefaultBackColor)
            {
                this.fc.AllFieldLights(LightColor.all, State.on);
                this.btnTestAllNodesLights.BackColor = Color.Green;
            }
            else
            {
                this.fc.AllFieldLights(LightColor.off, State.off);
                this.btnTestAllNodesLights.BackColor = DefaultBackColor;
            }
        }
        #endregion

        #region Output Buttons
        private void btnAllOutputsOn_Click(object sender, EventArgs e)
        {
            btnOutPut1.PerformClick();
            btnOutput2.PerformClick();
            btnOutpu3.PerformClick();
            btnOutput4.PerformClick();
            btnOutput5.PerformClick();
            btnOutput6.PerformClick();
            btnOutput7.PerformClick();
            btnOutput8.PerformClick();
        }

        private void btnOutput1_Click(object sender, EventArgs e)
        {
            if (btnOutPut1.BackColor == DefaultBackColor)
            {
                btnOutPut1.BackColor = Color.Green;
                btnOutPut1.ForeColor = Color.White;
                this.fc.SetOutputState(selectedTower, 1, State.on);
            }
            else
            {
                btnOutPut1.BackColor = DefaultBackColor;
                btnOutPut1.ForeColor = DefaultForeColor;
                this.fc.SetOutputState(selectedTower, 1, State.off);
            }

            this.nodeDG.Rows[selectedTower].Cells["Output_State"].Value =
              Convert.ToString(this.fc.node[selectedTower].outputStatus, 2).PadLeft(8, '0');
        }

        private void btnOutput2_Click(object sender, EventArgs e)
        {
            if (btnOutput2.BackColor == DefaultBackColor)
            {
                btnOutput2.BackColor = Color.Green;
                btnOutput2.ForeColor = Color.White;
                this.fc.SetOutputState(selectedTower, 2, State.on);
            }
            else
            {
                btnOutput2.BackColor = DefaultBackColor;
                btnOutput2.ForeColor = DefaultForeColor;
                this.fc.SetOutputState(selectedTower, 2, State.off);
            }
            this.nodeDG.Rows[selectedTower].Cells["Output_State"].Value =
                Convert.ToString(this.fc.node[selectedTower].outputStatus, 2).PadLeft(8, '0');
        }

        private void btnOutput3_Click(object sender, EventArgs e)
        {
            if (btnOutpu3.BackColor == DefaultBackColor)
            {
                btnOutpu3.BackColor = Color.Green;
                btnOutpu3.ForeColor = Color.White;
                this.fc.SetOutputState(selectedTower, 3, State.on);
            }
            else
            {
                btnOutpu3.BackColor = DefaultBackColor;
                btnOutpu3.ForeColor = DefaultForeColor;
                this.fc.SetOutputState(selectedTower, 3, State.off);
            }
            this.nodeDG.Rows[selectedTower].Cells["Output_State"].Value =
                Convert.ToString(this.fc.node[selectedTower].outputStatus, 2).PadLeft(8, '0');
        }

        private void btnOutput4_Click(object sender, EventArgs e)
        {
            if (btnOutput4.BackColor == DefaultBackColor)
            {
                btnOutput4.BackColor = Color.Green;
                btnOutput4.ForeColor = Color.White;
                this.fc.SetOutputState(selectedTower, 4, State.on);
            }
            else
            {
                btnOutput4.BackColor = DefaultBackColor;
                btnOutput4.ForeColor = DefaultForeColor;
                this.fc.SetOutputState(selectedTower, 4, State.off);
            }
            this.nodeDG.Rows[selectedTower].Cells["Output_State"].Value =
                Convert.ToString(this.fc.node[selectedTower].outputStatus, 2).PadLeft(8, '0');
        }

        private void btnOutput5_Click(object sender, EventArgs e)
        {
            if (btnOutput5.BackColor == DefaultBackColor)
            {
                btnOutput5.BackColor = Color.Green;
                btnOutput5.ForeColor = Color.White;
                this.fc.SetOutputState(selectedTower, 5, State.on);
            }
            else
            {
                btnOutput5.BackColor = DefaultBackColor;
                btnOutput5.ForeColor = DefaultForeColor;
                this.fc.SetOutputState(selectedTower, 5, State.off);
            }
            this.nodeDG.Rows[selectedTower].Cells["Output_State"].Value =
                Convert.ToString(this.fc.node[selectedTower].outputStatus, 2).PadLeft(8, '0');
        }

        private void btnOutput6_Click(object sender, EventArgs e)
        {
            if (btnOutput6.BackColor == DefaultBackColor)
            {
                btnOutput6.BackColor = Color.Green;
                btnOutput6.ForeColor = Color.White;
                this.fc.SetOutputState(selectedTower, 6, State.on);
            }
            else
            {
                btnOutput6.BackColor = DefaultBackColor;
                btnOutput6.ForeColor = DefaultForeColor;
                this.fc.SetOutputState(selectedTower, 6, State.off);
            }
            this.nodeDG.Rows[selectedTower].Cells["Output_State"].Value =
             Convert.ToString(this.fc.node[selectedTower].outputStatus, 2).PadLeft(8, '0');
        }

        private void btnOutput7_Click(object sender, EventArgs e)
        {
            if (btnOutput7.BackColor == DefaultBackColor)
            {
                btnOutput7.BackColor = Color.Green;
                btnOutput7.ForeColor = Color.White;
                this.fc.SetOutputState(selectedTower, 7, State.on);
            }
            else
            {
                btnOutput7.BackColor = DefaultBackColor;
                btnOutput7.ForeColor = DefaultForeColor;
                this.fc.SetOutputState(selectedTower, 7, State.off);
            }
            this.nodeDG.Rows[selectedTower].Cells["Output_State"].Value =
                Convert.ToString(this.fc.node[selectedTower].outputStatus, 2).PadLeft(8, '0');
        }

        private void btnOutput8_Click(object sender, EventArgs e)
        {
            if (btnOutput8.BackColor == DefaultBackColor)
            {
                btnOutput8.BackColor = Color.Green;
                btnOutput8.ForeColor = Color.White;
                this.fc.SetOutputState(selectedTower, 8, State.on);
            }
            else
            {
                btnOutput8.BackColor = DefaultBackColor;
                btnOutput8.ForeColor = DefaultForeColor;
                this.fc.SetOutputState(selectedTower, 8, State.off);
            }
            this.nodeDG.Rows[selectedTower].Cells["Output_State"].Value =
                Convert.ToString(this.fc.node[selectedTower].outputStatus, 2).PadLeft(8, '0');
        }

        #endregion

        #region Updates
        private void btnRawData_Click(object sender, EventArgs e)
        {
            this.fc.SeeRawData(ComModes.canBus);
            this.btnMonitorNodes.PerformClick();
        }

        private void btnMonitorNodes_Click(object sender, EventArgs e)
        {
            if (btnMonitorNodes.BackColor == DefaultBackColor)
            {
                this.displayUpdateTimer.Start();
                btnMonitorNodes.BackColor = Color.Green;
            }
            else
            {
                this.displayUpdateTimer.Stop();
                btnMonitorNodes.BackColor = DefaultBackColor;
            }
        }

        private void updateDisplay(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.nodeDG.Rows)
            {
                int n = Convert.ToInt32(row.Cells["Node_ID"].Value);
                this.nodeDG.Rows[n].Cells["Output_State"].Value = Convert.ToString(this.fc.node[n].outputStatus, 2).PadLeft(8, '0');
                this.nodeDG.Rows[n].Cells["Input_State"].Value = Convert.ToString(this.fc.node[n].inputStatus, 2).PadLeft(8, '0');
                if (this.fc.node[n].lightStatus != null)
                    this.nodeDG.Rows[n].Cells["Light_State"].Value = Convert.ToString(this.fc.node[n].lightStatus);
                this.nodeDG.Rows[n].Cells["Sent|Rec"].Value = Convert.ToString(this.fc.node[n].nodeMessagesSent) + "|" 
                    + Convert.ToString(this.fc.node[n].nodeMessagesReceived);
            }
        }
        #endregion

        #region CANBUS
        private void refreshPortList()
        {
            cbCanPort.Items.Clear();
            foreach (string portname in SerialPort.GetPortNames())
            {
                cbCanPort.Items.Add(portname);
                cbCanPort.Text = portname.ToString();
            }
        }

        private void btnCanRefreshList_Click(object sender, EventArgs e)
        {
            refreshPortList();
        }

        private void btnCanOpenPort_Click(object sender, EventArgs e)
        {
            bool rts = cbRTSenable.Checked;
            bool dtr = cbDTRenable.Checked;

            if (rts || dtr)
            {
                string port = cbCanPort.Text;
                this.fc.StartUp(ComModes.canBus, port, dtr, rts);
            }
            else
            {
                string port = cbCanPort.Text;
                this.fc.StartUp(ComModes.canBus, port);
            }
        }

        private void btnCanClosePort_Click(object sender, EventArgs e)
        {
            string port = cbCanPort.Text;
            this.fc.ShutDown(ComModes.canBus, port);
        }

        private void btnChangeModeAll_Click(object sender, EventArgs e)
        {
            if (cbGameModes.Text != "Select Game Mode")
            {
                if (cbGameModes.SelectedItem == cbGameModes.Items[0]) this.fc.ChangeGameMode(GameModes.reset);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[1]) this.fc.ChangeGameMode(GameModes.ready);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[2]) this.fc.ChangeGameMode(GameModes.start);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[3]) this.fc.ChangeGameMode(GameModes.autonomous);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[4]) this.fc.ChangeGameMode(GameModes.mantonomous);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[5]) this.fc.ChangeGameMode(GameModes.manual);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[6]) this.fc.ChangeGameMode(GameModes.end);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[7]) this.fc.ChangeGameMode(GameModes.off);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[8]) this.fc.ChangeGameMode(GameModes.debug);

            }
        }

        private void btnCANChangeMode_Click(object sender, EventArgs e)
        {
            if (cbGameModes.Text != "Select Game Mode")
            {
                if (cbGameModes.SelectedItem == cbGameModes.Items[0]) this.fc.ChangeGameMode(selectedTower, GameModes.reset);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[1]) this.fc.ChangeGameMode(selectedTower, GameModes.ready);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[2]) this.fc.ChangeGameMode(selectedTower, GameModes.start);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[3]) this.fc.ChangeGameMode(selectedTower, GameModes.autonomous);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[4]) this.fc.ChangeGameMode(selectedTower, GameModes.mantonomous);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[5]) this.fc.ChangeGameMode(selectedTower, GameModes.manual);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[6]) this.fc.ChangeGameMode(selectedTower, GameModes.end);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[7]) this.fc.ChangeGameMode(selectedTower, GameModes.off);
                else if (cbGameModes.SelectedItem == cbGameModes.Items[8]) this.fc.ChangeGameMode(selectedTower, GameModes.debug);

            }
        }

        private void btnChangeBaudRate_Click(object sender, EventArgs e)
        {
            int rate = cbBaudRate.SelectedIndex;
            this.fc.ChangeGameFunction(selectedTower, 6, rate, null);
        }

        private void btnChangeAllBaudRate_Click(object sender, EventArgs e)
        {
            int rate = cbBaudRate.SelectedIndex;
            this.fc.ChangeGameFunction(6, rate, null);
            this.fc.ChangeGameFunction(0, 6, rate, null);
        }

        private void cbDTRenable_CheckedChanged(object sender, EventArgs e)
        {
            btnCanClosePort.PerformClick();
            btnCanOpenPort.PerformClick();
        }

        private void cbRTSenable_CheckedChanged(object sender, EventArgs e)
        {
            btnCanClosePort.PerformClick();
            btnCanOpenPort.PerformClick();
        }

        #endregion

        #region Field Functions
        private void btnCalibrateFos_Click(object sender, EventArgs e)
        {
            this.fc.StartFosCalibration(selectedTower);
        }

        private void btnRingBell_Click(object sender, EventArgs e)
        {
            this.fc.RingBell();
        }

        private void btnSoundBuzzer_Click(object sender, EventArgs e)
        {
            this.fc.SoundBuzzer();
        }

        private void btnTransmitterAuto_Click(object sender, EventArgs e)
        {
            this.fc.RobotTransmitters("both", State.on, State.on);
        }

        private void btnTransmitterManual_Click(object sender, EventArgs e)
        {
            this.fc.RobotTransmitters("both", State.off, State.on);
        }

        private void btnTransmitterOff_Click(object sender, EventArgs e)
        {
            this.fc.RobotTransmitters("both", State.off, State.off);
        }

        private void btnSwitchFunction_Click(object sender, EventArgs e)
        {
            string delay = this.tbDelayMultiplier.Text;
            if (cbSwitchFunction.SelectedItem == cbSwitchFunction.Items[0]) this.fc.ChangeGameFunction(0, 0, delay);
            else if (cbSwitchFunction.SelectedItem == cbSwitchFunction.Items[1]) this.fc.ChangeGameFunction(8, 1, delay);
            else if (cbSwitchFunction.SelectedItem == cbSwitchFunction.Items[2]) this.fc.ChangeGameFunction(8, 2, delay);
            else if (cbSwitchFunction.SelectedItem == cbSwitchFunction.Items[3]) this.fc.ChangeGameFunction(9, 1, delay);
            else if (cbSwitchFunction.SelectedItem == cbSwitchFunction.Items[4]) this.fc.ChangeGameFunction(9, 2, delay);
        }

        private void btnStartNetTestSelNode_Click(object sender, EventArgs e)
        {
            displayUpdateTimer.Start();
            this.fc.ChangeGameMode(selectedTower, GameModes.start);
        }

        private void btnStartNetTestAllNodes_Click(object sender, EventArgs e)
        {
            displayUpdateTimer.Start();
            this.fc.ChangeGameMode(GameModes.start);
        }

        private void btnStopNetworkTest_Click(object sender, EventArgs e)
        {
            displayUpdateTimer.Stop();
            this.fc.ChangeGameMode(GameModes.end);
        }

        private void btnResetSelNode_Click(object sender, EventArgs e)
        {
            displayUpdateTimer.Stop();
            this.fc.ChangeGameMode(selectedTower, GameModes.reset);
            this.fc.ResetTestVariables();
        }

        private void btnResetAllNodes_Click(object sender, EventArgs e)
        {
            displayUpdateTimer.Stop();
            this.fc.ChangeGameMode(GameModes.reset);
            this.fc.ChangeGameMode(0, GameModes.reset);
            this.fc.ResetTestVariables();
        }
        #endregion

        private void btnLaunchGame_Click(object sender, EventArgs e)
        {
            GameControl gc = new GameControl(this.fc);
            gc.Show();
        }

        private void displayUpdateTimer_Tick(object sender, EventArgs e)
        {
            this.updateDisplay(sender, e);
            this.fc.writeLogs();
        }

        private void btnLogData_Click(object sender, EventArgs e)
        {
            this.fc.writeLogs();
        }

        private void btnSetSunLocation_Click(object sender, EventArgs e)
        {
            string str = ("7,1,4," + this.selectedTower);
            this.fc.SendMessage(11, str);
            Thread.Sleep(20);
            str = ("7,1,1,");
            this.fc.SendMessage(this.selectedTower, str);
        }
      
        private void btnDeselect_Click(object sender, EventArgs e)
        {
            string str = ("7,0,");
            this.fc.SendMessage(this.selectedTower, str);
        }
    }
}
