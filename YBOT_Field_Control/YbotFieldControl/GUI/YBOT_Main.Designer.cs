namespace YbotFieldControl
{
    partial class YBOT_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.nodeDG = new System.Windows.Forms.DataGridView();
            this.btnSetAllCAN = new System.Windows.Forms.Button();
            this.btnSetAllXBee = new System.Windows.Forms.Button();
            this.btnSetAllWifi = new System.Windows.Forms.Button();
            this.btnAddNewNode = new System.Windows.Forms.Button();
            this.nodeDS = new System.Data.DataSet();
            this.btnReloadFile = new System.Windows.Forms.Button();
            this.btnUpdateFile = new System.Windows.Forms.Button();
            this.btnTestNodeConnections = new System.Windows.Forms.Button();
            this.cmGridRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.setTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cANBUSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xBeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wiFiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbLightControls = new System.Windows.Forms.GroupBox();
            this.btnTestAllNodesLights = new System.Windows.Forms.Button();
            this.btnLightAll = new System.Windows.Forms.Button();
            this.btnLightRed = new System.Windows.Forms.Button();
            this.btnLightWhite = new System.Windows.Forms.Button();
            this.btnLightBlue = new System.Windows.Forms.Button();
            this.btnLightsOff = new System.Windows.Forms.Button();
            this.btnLightYellow = new System.Windows.Forms.Button();
            this.btnLightGreen = new System.Windows.Forms.Button();
            this.btnLightTest = new System.Windows.Forms.Button();
            this.gbOutputControls = new System.Windows.Forms.GroupBox();
            this.btnOutput8 = new System.Windows.Forms.Button();
            this.btnOutput7 = new System.Windows.Forms.Button();
            this.btnOutput6 = new System.Windows.Forms.Button();
            this.btnOutpu3 = new System.Windows.Forms.Button();
            this.btnOutput4 = new System.Windows.Forms.Button();
            this.btnOutput5 = new System.Windows.Forms.Button();
            this.btnOutput2 = new System.Windows.Forms.Button();
            this.btnOutPut1 = new System.Windows.Forms.Button();
            this.btnToggleAllOutpus = new System.Windows.Forms.Button();
            this.gbCanbus = new System.Windows.Forms.GroupBox();
            this.cbRTSenable = new System.Windows.Forms.CheckBox();
            this.cbDTRenable = new System.Windows.Forms.CheckBox();
            this.btnChangeAllBaudRate = new System.Windows.Forms.Button();
            this.btnChangeBaudRate = new System.Windows.Forms.Button();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.btnChangeModeAll = new System.Windows.Forms.Button();
            this.btnCANChangeMode = new System.Windows.Forms.Button();
            this.cbGameModes = new System.Windows.Forms.ComboBox();
            this.btnCanRefreshList = new System.Windows.Forms.Button();
            this.btnCanClosePort = new System.Windows.Forms.Button();
            this.cbCanPort = new System.Windows.Forms.ComboBox();
            this.btnCanOpenPort = new System.Windows.Forms.Button();
            this.btnRawData = new System.Windows.Forms.Button();
            this.btnLaunchGame = new System.Windows.Forms.Button();
            this.btnMonitorNodes = new System.Windows.Forms.Button();
            this.gbFieldFunctions = new System.Windows.Forms.GroupBox();
            this.btnSetSunLocation = new System.Windows.Forms.Button();
            this.btnTransmitterOff = new System.Windows.Forms.Button();
            this.btnTransmitterAuto = new System.Windows.Forms.Button();
            this.btnTransmitterManual = new System.Windows.Forms.Button();
            this.btnSoundBuzzer = new System.Windows.Forms.Button();
            this.btnRingBell = new System.Windows.Forms.Button();
            this.btnFunction_1 = new System.Windows.Forms.Button();
            this.btnSwitchFunction = new System.Windows.Forms.Button();
            this.cbSwitchFunction = new System.Windows.Forms.ComboBox();
            this.gbTesting = new System.Windows.Forms.GroupBox();
            this.btnLogData = new System.Windows.Forms.Button();
            this.btnResetAllNodes = new System.Windows.Forms.Button();
            this.btnResetSelNode = new System.Windows.Forms.Button();
            this.btnStopNetworkTest = new System.Windows.Forms.Button();
            this.btnStartNetTestAllNodes = new System.Windows.Forms.Button();
            this.btnStartNetTestSelNode = new System.Windows.Forms.Button();
            this.lblDelayMultiplier = new System.Windows.Forms.Label();
            this.tbDelayMultiplier = new System.Windows.Forms.TextBox();
            this.displayUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.sqlConnectButton = new System.Windows.Forms.Button();
            this.btnDeselect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nodeDG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeDS)).BeginInit();
            this.cmGridRightClick.SuspendLayout();
            this.gbLightControls.SuspendLayout();
            this.gbOutputControls.SuspendLayout();
            this.gbCanbus.SuspendLayout();
            this.gbFieldFunctions.SuspendLayout();
            this.gbTesting.SuspendLayout();
            this.SuspendLayout();
            // 
            // nodeDG
            // 
            this.nodeDG.AllowUserToAddRows = false;
            this.nodeDG.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nodeDG.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.nodeDG.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.nodeDG.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nodeDG.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.nodeDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nodeDG.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.nodeDG.Location = new System.Drawing.Point(332, 12);
            this.nodeDG.Name = "nodeDG";
            this.nodeDG.Size = new System.Drawing.Size(842, 336);
            this.nodeDG.TabIndex = 0;
            this.nodeDG.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.nodeDG_CellMouseDown);
            this.nodeDG.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.nodeDG_RowHeaderMouseClick);
            // 
            // btnSetAllCAN
            // 
            this.btnSetAllCAN.Location = new System.Drawing.Point(1076, 473);
            this.btnSetAllCAN.Name = "btnSetAllCAN";
            this.btnSetAllCAN.Size = new System.Drawing.Size(100, 50);
            this.btnSetAllCAN.TabIndex = 2;
            this.btnSetAllCAN.Text = "Set All CANBUS";
            this.btnSetAllCAN.UseVisualStyleBackColor = true;
            this.btnSetAllCAN.Click += new System.EventHandler(this.btnSetAllCAN_Click);
            // 
            // btnSetAllXBee
            // 
            this.btnSetAllXBee.Location = new System.Drawing.Point(1076, 417);
            this.btnSetAllXBee.Name = "btnSetAllXBee";
            this.btnSetAllXBee.Size = new System.Drawing.Size(100, 50);
            this.btnSetAllXBee.TabIndex = 3;
            this.btnSetAllXBee.Text = "Set All XBee";
            this.btnSetAllXBee.UseVisualStyleBackColor = true;
            // 
            // btnSetAllWifi
            // 
            this.btnSetAllWifi.Location = new System.Drawing.Point(1076, 361);
            this.btnSetAllWifi.Name = "btnSetAllWifi";
            this.btnSetAllWifi.Size = new System.Drawing.Size(100, 50);
            this.btnSetAllWifi.TabIndex = 4;
            this.btnSetAllWifi.Text = "Set All WiFi";
            this.btnSetAllWifi.UseVisualStyleBackColor = true;
            // 
            // btnAddNewNode
            // 
            this.btnAddNewNode.Location = new System.Drawing.Point(970, 473);
            this.btnAddNewNode.Name = "btnAddNewNode";
            this.btnAddNewNode.Size = new System.Drawing.Size(100, 50);
            this.btnAddNewNode.TabIndex = 5;
            this.btnAddNewNode.Text = "Add New Node";
            this.btnAddNewNode.UseVisualStyleBackColor = true;
            this.btnAddNewNode.Click += new System.EventHandler(this.btnAddNewNode_Click);
            // 
            // nodeDS
            // 
            this.nodeDS.DataSetName = "nodeDataSet";
            // 
            // btnReloadFile
            // 
            this.btnReloadFile.Location = new System.Drawing.Point(970, 361);
            this.btnReloadFile.Name = "btnReloadFile";
            this.btnReloadFile.Size = new System.Drawing.Size(100, 50);
            this.btnReloadFile.TabIndex = 7;
            this.btnReloadFile.Text = "Reload File";
            this.btnReloadFile.UseVisualStyleBackColor = true;
            this.btnReloadFile.Click += new System.EventHandler(this.btnReloadFile_Click);
            // 
            // btnUpdateFile
            // 
            this.btnUpdateFile.Location = new System.Drawing.Point(970, 417);
            this.btnUpdateFile.Name = "btnUpdateFile";
            this.btnUpdateFile.Size = new System.Drawing.Size(100, 50);
            this.btnUpdateFile.TabIndex = 8;
            this.btnUpdateFile.Text = "Update File";
            this.btnUpdateFile.UseVisualStyleBackColor = true;
            this.btnUpdateFile.Click += new System.EventHandler(this.btnUpdateFile_Click);
            // 
            // btnTestNodeConnections
            // 
            this.btnTestNodeConnections.Location = new System.Drawing.Point(6, 19);
            this.btnTestNodeConnections.Name = "btnTestNodeConnections";
            this.btnTestNodeConnections.Size = new System.Drawing.Size(100, 50);
            this.btnTestNodeConnections.TabIndex = 9;
            this.btnTestNodeConnections.Text = "Test Node Connections";
            this.btnTestNodeConnections.UseVisualStyleBackColor = true;
            this.btnTestNodeConnections.Click += new System.EventHandler(this.btnTestNodeConnections_Click);
            // 
            // cmGridRightClick
            // 
            this.cmGridRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDeleteRow,
            this.setTypeToolStripMenuItem});
            this.cmGridRightClick.Name = "cmGridRightClick";
            this.cmGridRightClick.Size = new System.Drawing.Size(120, 48);
            // 
            // tsmDeleteRow
            // 
            this.tsmDeleteRow.Name = "tsmDeleteRow";
            this.tsmDeleteRow.Size = new System.Drawing.Size(119, 22);
            this.tsmDeleteRow.Text = "Delete Row";
            this.tsmDeleteRow.Click += new System.EventHandler(this.tsmDeleteRow_Click);
            // 
            // setTypeToolStripMenuItem
            // 
            this.setTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cANBUSToolStripMenuItem,
            this.xBeeToolStripMenuItem,
            this.wiFiToolStripMenuItem});
            this.setTypeToolStripMenuItem.Name = "setTypeToolStripMenuItem";
            this.setTypeToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.setTypeToolStripMenuItem.Text = "Set Type";
            // 
            // cANBUSToolStripMenuItem
            // 
            this.cANBUSToolStripMenuItem.Name = "cANBUSToolStripMenuItem";
            this.cANBUSToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.cANBUSToolStripMenuItem.Text = "CANBUS";
            this.cANBUSToolStripMenuItem.Click += new System.EventHandler(this.cANBUSToolStripMenuItem_Click);
            // 
            // xBeeToolStripMenuItem
            // 
            this.xBeeToolStripMenuItem.Name = "xBeeToolStripMenuItem";
            this.xBeeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.xBeeToolStripMenuItem.Text = "XBee";
            // 
            // wiFiToolStripMenuItem
            // 
            this.wiFiToolStripMenuItem.Name = "wiFiToolStripMenuItem";
            this.wiFiToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.wiFiToolStripMenuItem.Text = "WiFi";
            // 
            // gbLightControls
            // 
            this.gbLightControls.Controls.Add(this.btnTestAllNodesLights);
            this.gbLightControls.Controls.Add(this.btnLightAll);
            this.gbLightControls.Controls.Add(this.btnLightRed);
            this.gbLightControls.Controls.Add(this.btnLightWhite);
            this.gbLightControls.Controls.Add(this.btnLightBlue);
            this.gbLightControls.Controls.Add(this.btnLightsOff);
            this.gbLightControls.Controls.Add(this.btnLightYellow);
            this.gbLightControls.Controls.Add(this.btnLightGreen);
            this.gbLightControls.Controls.Add(this.btnLightTest);
            this.gbLightControls.Location = new System.Drawing.Point(13, 13);
            this.gbLightControls.Name = "gbLightControls";
            this.gbLightControls.Size = new System.Drawing.Size(313, 166);
            this.gbLightControls.TabIndex = 10;
            this.gbLightControls.TabStop = false;
            this.gbLightControls.Text = "Light Controls";
            // 
            // btnTestAllNodesLights
            // 
            this.btnTestAllNodesLights.Location = new System.Drawing.Point(7, 112);
            this.btnTestAllNodesLights.Name = "btnTestAllNodesLights";
            this.btnTestAllNodesLights.Size = new System.Drawing.Size(70, 40);
            this.btnTestAllNodesLights.TabIndex = 8;
            this.btnTestAllNodesLights.Text = "Test - All Nodes";
            this.btnTestAllNodesLights.UseVisualStyleBackColor = true;
            this.btnTestAllNodesLights.Click += new System.EventHandler(this.btnTestAllNodesLights_Click);
            // 
            // btnLightAll
            // 
            this.btnLightAll.Location = new System.Drawing.Point(159, 112);
            this.btnLightAll.Name = "btnLightAll";
            this.btnLightAll.Size = new System.Drawing.Size(70, 40);
            this.btnLightAll.TabIndex = 7;
            this.btnLightAll.Text = "All";
            this.btnLightAll.UseVisualStyleBackColor = true;
            this.btnLightAll.Click += new System.EventHandler(this.btnLightAll_Click);
            // 
            // btnLightRed
            // 
            this.btnLightRed.Location = new System.Drawing.Point(159, 20);
            this.btnLightRed.Name = "btnLightRed";
            this.btnLightRed.Size = new System.Drawing.Size(70, 40);
            this.btnLightRed.TabIndex = 6;
            this.btnLightRed.Text = "Red";
            this.btnLightRed.UseVisualStyleBackColor = true;
            this.btnLightRed.Click += new System.EventHandler(this.btnLightRed_Click);
            // 
            // btnLightWhite
            // 
            this.btnLightWhite.Location = new System.Drawing.Point(235, 112);
            this.btnLightWhite.Name = "btnLightWhite";
            this.btnLightWhite.Size = new System.Drawing.Size(70, 40);
            this.btnLightWhite.TabIndex = 5;
            this.btnLightWhite.Text = "White";
            this.btnLightWhite.UseVisualStyleBackColor = true;
            this.btnLightWhite.Click += new System.EventHandler(this.btnLightWhite_Click);
            // 
            // btnLightBlue
            // 
            this.btnLightBlue.Location = new System.Drawing.Point(159, 66);
            this.btnLightBlue.Name = "btnLightBlue";
            this.btnLightBlue.Size = new System.Drawing.Size(70, 40);
            this.btnLightBlue.TabIndex = 4;
            this.btnLightBlue.Text = "Blue";
            this.btnLightBlue.UseVisualStyleBackColor = true;
            this.btnLightBlue.Click += new System.EventHandler(this.btnLightBlue_Click);
            // 
            // btnLightsOff
            // 
            this.btnLightsOff.Location = new System.Drawing.Point(83, 20);
            this.btnLightsOff.Name = "btnLightsOff";
            this.btnLightsOff.Size = new System.Drawing.Size(70, 40);
            this.btnLightsOff.TabIndex = 3;
            this.btnLightsOff.Text = "Off";
            this.btnLightsOff.UseVisualStyleBackColor = true;
            this.btnLightsOff.Click += new System.EventHandler(this.btnLightsOff_Click);
            // 
            // btnLightYellow
            // 
            this.btnLightYellow.Location = new System.Drawing.Point(235, 66);
            this.btnLightYellow.Name = "btnLightYellow";
            this.btnLightYellow.Size = new System.Drawing.Size(70, 40);
            this.btnLightYellow.TabIndex = 2;
            this.btnLightYellow.Text = "Yellow";
            this.btnLightYellow.UseVisualStyleBackColor = true;
            this.btnLightYellow.Click += new System.EventHandler(this.btnLightYellow_Click);
            // 
            // btnLightGreen
            // 
            this.btnLightGreen.Location = new System.Drawing.Point(237, 20);
            this.btnLightGreen.Name = "btnLightGreen";
            this.btnLightGreen.Size = new System.Drawing.Size(70, 40);
            this.btnLightGreen.TabIndex = 1;
            this.btnLightGreen.Text = "Green";
            this.btnLightGreen.UseVisualStyleBackColor = true;
            this.btnLightGreen.Click += new System.EventHandler(this.btnLightGreen_Click);
            // 
            // btnLightTest
            // 
            this.btnLightTest.Location = new System.Drawing.Point(7, 20);
            this.btnLightTest.Name = "btnLightTest";
            this.btnLightTest.Size = new System.Drawing.Size(70, 40);
            this.btnLightTest.TabIndex = 0;
            this.btnLightTest.Text = "Test";
            this.btnLightTest.UseVisualStyleBackColor = true;
            this.btnLightTest.Click += new System.EventHandler(this.btnLightTest_Click);
            // 
            // gbOutputControls
            // 
            this.gbOutputControls.Controls.Add(this.btnOutput8);
            this.gbOutputControls.Controls.Add(this.btnOutput7);
            this.gbOutputControls.Controls.Add(this.btnOutput6);
            this.gbOutputControls.Controls.Add(this.btnOutpu3);
            this.gbOutputControls.Controls.Add(this.btnOutput4);
            this.gbOutputControls.Controls.Add(this.btnOutput5);
            this.gbOutputControls.Controls.Add(this.btnOutput2);
            this.gbOutputControls.Controls.Add(this.btnOutPut1);
            this.gbOutputControls.Controls.Add(this.btnToggleAllOutpus);
            this.gbOutputControls.Location = new System.Drawing.Point(13, 185);
            this.gbOutputControls.Name = "gbOutputControls";
            this.gbOutputControls.Size = new System.Drawing.Size(313, 163);
            this.gbOutputControls.TabIndex = 11;
            this.gbOutputControls.TabStop = false;
            this.gbOutputControls.Text = "Output Controls";
            // 
            // btnOutput8
            // 
            this.btnOutput8.Location = new System.Drawing.Point(235, 112);
            this.btnOutput8.Name = "btnOutput8";
            this.btnOutput8.Size = new System.Drawing.Size(70, 40);
            this.btnOutput8.TabIndex = 12;
            this.btnOutput8.Text = "Output 8";
            this.btnOutput8.UseVisualStyleBackColor = true;
            this.btnOutput8.Click += new System.EventHandler(this.btnOutput8_Click);
            // 
            // btnOutput7
            // 
            this.btnOutput7.Location = new System.Drawing.Point(159, 112);
            this.btnOutput7.Name = "btnOutput7";
            this.btnOutput7.Size = new System.Drawing.Size(70, 40);
            this.btnOutput7.TabIndex = 13;
            this.btnOutput7.Text = "Output 7";
            this.btnOutput7.UseVisualStyleBackColor = true;
            this.btnOutput7.Click += new System.EventHandler(this.btnOutput7_Click);
            // 
            // btnOutput6
            // 
            this.btnOutput6.Location = new System.Drawing.Point(83, 112);
            this.btnOutput6.Name = "btnOutput6";
            this.btnOutput6.Size = new System.Drawing.Size(70, 40);
            this.btnOutput6.TabIndex = 14;
            this.btnOutput6.Text = "Output 6";
            this.btnOutput6.UseVisualStyleBackColor = true;
            this.btnOutput6.Click += new System.EventHandler(this.btnOutput6_Click);
            // 
            // btnOutpu3
            // 
            this.btnOutpu3.Location = new System.Drawing.Point(159, 66);
            this.btnOutpu3.Name = "btnOutpu3";
            this.btnOutpu3.Size = new System.Drawing.Size(70, 40);
            this.btnOutpu3.TabIndex = 11;
            this.btnOutpu3.Text = "Output 3";
            this.btnOutpu3.UseVisualStyleBackColor = true;
            this.btnOutpu3.Click += new System.EventHandler(this.btnOutput3_Click);
            // 
            // btnOutput4
            // 
            this.btnOutput4.Location = new System.Drawing.Point(235, 66);
            this.btnOutput4.Name = "btnOutput4";
            this.btnOutput4.Size = new System.Drawing.Size(70, 40);
            this.btnOutput4.TabIndex = 12;
            this.btnOutput4.Text = "Output 4";
            this.btnOutput4.UseVisualStyleBackColor = true;
            this.btnOutput4.Click += new System.EventHandler(this.btnOutput4_Click);
            // 
            // btnOutput5
            // 
            this.btnOutput5.Location = new System.Drawing.Point(7, 112);
            this.btnOutput5.Name = "btnOutput5";
            this.btnOutput5.Size = new System.Drawing.Size(70, 40);
            this.btnOutput5.TabIndex = 13;
            this.btnOutput5.Text = "Output 5";
            this.btnOutput5.UseVisualStyleBackColor = true;
            this.btnOutput5.Click += new System.EventHandler(this.btnOutput5_Click);
            // 
            // btnOutput2
            // 
            this.btnOutput2.Location = new System.Drawing.Point(83, 66);
            this.btnOutput2.Name = "btnOutput2";
            this.btnOutput2.Size = new System.Drawing.Size(70, 40);
            this.btnOutput2.TabIndex = 10;
            this.btnOutput2.Text = "Output 2";
            this.btnOutput2.UseVisualStyleBackColor = true;
            this.btnOutput2.Click += new System.EventHandler(this.btnOutput2_Click);
            // 
            // btnOutPut1
            // 
            this.btnOutPut1.Location = new System.Drawing.Point(7, 66);
            this.btnOutPut1.Name = "btnOutPut1";
            this.btnOutPut1.Size = new System.Drawing.Size(70, 40);
            this.btnOutPut1.TabIndex = 9;
            this.btnOutPut1.Text = "Output 1";
            this.btnOutPut1.UseVisualStyleBackColor = true;
            this.btnOutPut1.Click += new System.EventHandler(this.btnOutput1_Click);
            // 
            // btnToggleAllOutpus
            // 
            this.btnToggleAllOutpus.Location = new System.Drawing.Point(7, 20);
            this.btnToggleAllOutpus.Name = "btnToggleAllOutpus";
            this.btnToggleAllOutpus.Size = new System.Drawing.Size(70, 40);
            this.btnToggleAllOutpus.TabIndex = 0;
            this.btnToggleAllOutpus.Text = "Toggle All Outputs";
            this.btnToggleAllOutpus.UseVisualStyleBackColor = true;
            this.btnToggleAllOutpus.Click += new System.EventHandler(this.btnAllOutputsOn_Click);
            // 
            // gbCanbus
            // 
            this.gbCanbus.Controls.Add(this.cbRTSenable);
            this.gbCanbus.Controls.Add(this.cbDTRenable);
            this.gbCanbus.Controls.Add(this.btnChangeAllBaudRate);
            this.gbCanbus.Controls.Add(this.btnChangeBaudRate);
            this.gbCanbus.Controls.Add(this.cbBaudRate);
            this.gbCanbus.Controls.Add(this.btnChangeModeAll);
            this.gbCanbus.Controls.Add(this.btnCANChangeMode);
            this.gbCanbus.Controls.Add(this.cbGameModes);
            this.gbCanbus.Controls.Add(this.btnCanRefreshList);
            this.gbCanbus.Controls.Add(this.btnCanClosePort);
            this.gbCanbus.Controls.Add(this.cbCanPort);
            this.gbCanbus.Controls.Add(this.btnCanOpenPort);
            this.gbCanbus.Location = new System.Drawing.Point(15, 361);
            this.gbCanbus.Name = "gbCanbus";
            this.gbCanbus.Size = new System.Drawing.Size(313, 268);
            this.gbCanbus.TabIndex = 13;
            this.gbCanbus.TabStop = false;
            this.gbCanbus.Text = "CANBUS";
            // 
            // cbRTSenable
            // 
            this.cbRTSenable.AutoSize = true;
            this.cbRTSenable.Location = new System.Drawing.Point(7, 87);
            this.cbRTSenable.Name = "cbRTSenable";
            this.cbRTSenable.Size = new System.Drawing.Size(146, 17);
            this.cbRTSenable.TabIndex = 15;
            this.cbRTSenable.Text = "RTS Enable (Off for LEO)";
            this.cbRTSenable.UseVisualStyleBackColor = true;
            this.cbRTSenable.CheckedChanged += new System.EventHandler(this.cbRTSenable_CheckedChanged);
            // 
            // cbDTRenable
            // 
            this.cbDTRenable.AutoSize = true;
            this.cbDTRenable.Location = new System.Drawing.Point(7, 66);
            this.cbDTRenable.Name = "cbDTRenable";
            this.cbDTRenable.Size = new System.Drawing.Size(147, 17);
            this.cbDTRenable.TabIndex = 14;
            this.cbDTRenable.Text = "DTR Enable (On for LEO)";
            this.cbDTRenable.UseVisualStyleBackColor = true;
            this.cbDTRenable.CheckedChanged += new System.EventHandler(this.cbDTRenable_CheckedChanged);
            // 
            // btnChangeAllBaudRate
            // 
            this.btnChangeAllBaudRate.Location = new System.Drawing.Point(202, 193);
            this.btnChangeAllBaudRate.Name = "btnChangeAllBaudRate";
            this.btnChangeAllBaudRate.Size = new System.Drawing.Size(101, 40);
            this.btnChangeAllBaudRate.TabIndex = 13;
            this.btnChangeAllBaudRate.Text = "Change Rate All Nodes";
            this.btnChangeAllBaudRate.UseVisualStyleBackColor = true;
            this.btnChangeAllBaudRate.Click += new System.EventHandler(this.btnChangeAllBaudRate_Click);
            // 
            // btnChangeBaudRate
            // 
            this.btnChangeBaudRate.Location = new System.Drawing.Point(128, 193);
            this.btnChangeBaudRate.Name = "btnChangeBaudRate";
            this.btnChangeBaudRate.Size = new System.Drawing.Size(60, 40);
            this.btnChangeBaudRate.TabIndex = 12;
            this.btnChangeBaudRate.Text = "Change Rate";
            this.btnChangeBaudRate.UseVisualStyleBackColor = true;
            this.btnChangeBaudRate.Click += new System.EventHandler(this.btnChangeBaudRate_Click);
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "Select Baud Rate",
            "5 KBPS",
            "10 KBPS",
            "20 KBPS",
            "25 KBPS",
            "31K 25 BPS",
            "33 KBPS",
            "40 KBPS",
            "50 KBPS",
            "80 KBPS",
            "83K 3 BPS",
            "95 KBPS",
            "100 KBPS",
            "125 KBPS",
            "200 KBPS",
            "250 KBPS",
            "500 KBPS",
            "1000 KBPS"});
            this.cbBaudRate.Location = new System.Drawing.Point(7, 204);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(115, 21);
            this.cbBaudRate.TabIndex = 11;
            this.cbBaudRate.Text = "Select Baud Rate";
            // 
            // btnChangeModeAll
            // 
            this.btnChangeModeAll.Location = new System.Drawing.Point(202, 141);
            this.btnChangeModeAll.Name = "btnChangeModeAll";
            this.btnChangeModeAll.Size = new System.Drawing.Size(101, 40);
            this.btnChangeModeAll.TabIndex = 10;
            this.btnChangeModeAll.Text = "Change All Nodes";
            this.btnChangeModeAll.UseVisualStyleBackColor = true;
            this.btnChangeModeAll.Click += new System.EventHandler(this.btnChangeModeAll_Click);
            // 
            // btnCANChangeMode
            // 
            this.btnCANChangeMode.Location = new System.Drawing.Point(128, 141);
            this.btnCANChangeMode.Name = "btnCANChangeMode";
            this.btnCANChangeMode.Size = new System.Drawing.Size(60, 40);
            this.btnCANChangeMode.TabIndex = 8;
            this.btnCANChangeMode.Text = "Change Mode";
            this.btnCANChangeMode.UseVisualStyleBackColor = true;
            this.btnCANChangeMode.Click += new System.EventHandler(this.btnCANChangeMode_Click);
            // 
            // cbGameModes
            // 
            this.cbGameModes.FormattingEnabled = true;
            this.cbGameModes.Items.AddRange(new object[] {
            "Reset",
            "Ready/Standby",
            "Start",
            "Autonomous",
            "Mantonomous",
            "Manual",
            "End",
            "Field Off",
            "Debug"});
            this.cbGameModes.Location = new System.Drawing.Point(7, 152);
            this.cbGameModes.Name = "cbGameModes";
            this.cbGameModes.Size = new System.Drawing.Size(115, 21);
            this.cbGameModes.TabIndex = 7;
            this.cbGameModes.Text = "Select Game Mode";
            // 
            // btnCanRefreshList
            // 
            this.btnCanRefreshList.Location = new System.Drawing.Point(128, 20);
            this.btnCanRefreshList.Name = "btnCanRefreshList";
            this.btnCanRefreshList.Size = new System.Drawing.Size(60, 40);
            this.btnCanRefreshList.TabIndex = 5;
            this.btnCanRefreshList.Text = "Refresh List";
            this.btnCanRefreshList.UseVisualStyleBackColor = true;
            this.btnCanRefreshList.Click += new System.EventHandler(this.btnCanRefreshList_Click);
            // 
            // btnCanClosePort
            // 
            this.btnCanClosePort.Location = new System.Drawing.Point(258, 19);
            this.btnCanClosePort.Name = "btnCanClosePort";
            this.btnCanClosePort.Size = new System.Drawing.Size(45, 40);
            this.btnCanClosePort.TabIndex = 4;
            this.btnCanClosePort.Text = "Close Port";
            this.btnCanClosePort.UseVisualStyleBackColor = true;
            this.btnCanClosePort.Click += new System.EventHandler(this.btnCanClosePort_Click);
            // 
            // cbCanPort
            // 
            this.cbCanPort.FormattingEnabled = true;
            this.cbCanPort.Location = new System.Drawing.Point(7, 30);
            this.cbCanPort.Name = "cbCanPort";
            this.cbCanPort.Size = new System.Drawing.Size(115, 21);
            this.cbCanPort.TabIndex = 3;
            this.cbCanPort.Text = "CANBUS Port - Not Found";
            // 
            // btnCanOpenPort
            // 
            this.btnCanOpenPort.Location = new System.Drawing.Point(202, 20);
            this.btnCanOpenPort.Name = "btnCanOpenPort";
            this.btnCanOpenPort.Size = new System.Drawing.Size(45, 40);
            this.btnCanOpenPort.TabIndex = 2;
            this.btnCanOpenPort.Text = "Open Port";
            this.btnCanOpenPort.UseVisualStyleBackColor = true;
            this.btnCanOpenPort.Click += new System.EventHandler(this.btnCanOpenPort_Click);
            // 
            // btnRawData
            // 
            this.btnRawData.Location = new System.Drawing.Point(112, 19);
            this.btnRawData.Name = "btnRawData";
            this.btnRawData.Size = new System.Drawing.Size(100, 50);
            this.btnRawData.TabIndex = 9;
            this.btnRawData.Text = "Raw Data";
            this.btnRawData.UseVisualStyleBackColor = true;
            this.btnRawData.Click += new System.EventHandler(this.btnRawData_Click);
            // 
            // btnLaunchGame
            // 
            this.btnLaunchGame.Location = new System.Drawing.Point(1076, 585);
            this.btnLaunchGame.Name = "btnLaunchGame";
            this.btnLaunchGame.Size = new System.Drawing.Size(100, 50);
            this.btnLaunchGame.TabIndex = 14;
            this.btnLaunchGame.Text = "Launch Game";
            this.btnLaunchGame.UseVisualStyleBackColor = true;
            this.btnLaunchGame.Click += new System.EventHandler(this.btnLaunchGame_Click);
            // 
            // btnMonitorNodes
            // 
            this.btnMonitorNodes.Location = new System.Drawing.Point(218, 19);
            this.btnMonitorNodes.Name = "btnMonitorNodes";
            this.btnMonitorNodes.Size = new System.Drawing.Size(100, 50);
            this.btnMonitorNodes.TabIndex = 15;
            this.btnMonitorNodes.Text = "Monitor Nodes";
            this.btnMonitorNodes.UseVisualStyleBackColor = true;
            this.btnMonitorNodes.Click += new System.EventHandler(this.btnMonitorNodes_Click);
            // 
            // gbFieldFunctions
            // 
            this.gbFieldFunctions.Controls.Add(this.btnDeselect);
            this.gbFieldFunctions.Controls.Add(this.btnSetSunLocation);
            this.gbFieldFunctions.Controls.Add(this.btnTransmitterOff);
            this.gbFieldFunctions.Controls.Add(this.btnTransmitterAuto);
            this.gbFieldFunctions.Controls.Add(this.btnTransmitterManual);
            this.gbFieldFunctions.Controls.Add(this.btnSoundBuzzer);
            this.gbFieldFunctions.Controls.Add(this.btnRingBell);
            this.gbFieldFunctions.Controls.Add(this.btnFunction_1);
            this.gbFieldFunctions.Location = new System.Drawing.Point(672, 361);
            this.gbFieldFunctions.Name = "gbFieldFunctions";
            this.gbFieldFunctions.Size = new System.Drawing.Size(223, 268);
            this.gbFieldFunctions.TabIndex = 14;
            this.gbFieldFunctions.TabStop = false;
            this.gbFieldFunctions.Text = "Field Functions";
            // 
            // btnSetSunLocation
            // 
            this.btnSetSunLocation.Location = new System.Drawing.Point(112, 131);
            this.btnSetSunLocation.Name = "btnSetSunLocation";
            this.btnSetSunLocation.Size = new System.Drawing.Size(90, 50);
            this.btnSetSunLocation.TabIndex = 8;
            this.btnSetSunLocation.Text = "Set Sun Location";
            this.btnSetSunLocation.UseVisualStyleBackColor = true;
            this.btnSetSunLocation.Click += new System.EventHandler(this.btnSetSunLocation_Click);
            // 
            // btnTransmitterOff
            // 
            this.btnTransmitterOff.Location = new System.Drawing.Point(9, 188);
            this.btnTransmitterOff.Name = "btnTransmitterOff";
            this.btnTransmitterOff.Size = new System.Drawing.Size(90, 50);
            this.btnTransmitterOff.TabIndex = 7;
            this.btnTransmitterOff.Text = "TransmitterOff";
            this.btnTransmitterOff.UseVisualStyleBackColor = true;
            this.btnTransmitterOff.Click += new System.EventHandler(this.btnTransmitterOff_Click);
            // 
            // btnTransmitterAuto
            // 
            this.btnTransmitterAuto.Location = new System.Drawing.Point(9, 75);
            this.btnTransmitterAuto.Name = "btnTransmitterAuto";
            this.btnTransmitterAuto.Size = new System.Drawing.Size(90, 50);
            this.btnTransmitterAuto.TabIndex = 6;
            this.btnTransmitterAuto.Text = "Tansmitter Auto";
            this.btnTransmitterAuto.UseVisualStyleBackColor = true;
            this.btnTransmitterAuto.Click += new System.EventHandler(this.btnTransmitterAuto_Click);
            // 
            // btnTransmitterManual
            // 
            this.btnTransmitterManual.Location = new System.Drawing.Point(9, 132);
            this.btnTransmitterManual.Name = "btnTransmitterManual";
            this.btnTransmitterManual.Size = new System.Drawing.Size(90, 50);
            this.btnTransmitterManual.TabIndex = 5;
            this.btnTransmitterManual.Text = "Transmitter Manual";
            this.btnTransmitterManual.UseVisualStyleBackColor = true;
            this.btnTransmitterManual.Click += new System.EventHandler(this.btnTransmitterManual_Click);
            // 
            // btnSoundBuzzer
            // 
            this.btnSoundBuzzer.Location = new System.Drawing.Point(112, 76);
            this.btnSoundBuzzer.Name = "btnSoundBuzzer";
            this.btnSoundBuzzer.Size = new System.Drawing.Size(90, 50);
            this.btnSoundBuzzer.TabIndex = 4;
            this.btnSoundBuzzer.Text = "Sound Buzzer";
            this.btnSoundBuzzer.UseVisualStyleBackColor = true;
            this.btnSoundBuzzer.Click += new System.EventHandler(this.btnSoundBuzzer_Click);
            // 
            // btnRingBell
            // 
            this.btnRingBell.Location = new System.Drawing.Point(112, 19);
            this.btnRingBell.Name = "btnRingBell";
            this.btnRingBell.Size = new System.Drawing.Size(90, 50);
            this.btnRingBell.TabIndex = 3;
            this.btnRingBell.Text = "Ring Bell";
            this.btnRingBell.UseVisualStyleBackColor = true;
            this.btnRingBell.Click += new System.EventHandler(this.btnRingBell_Click);
            // 
            // btnFunction_1
            // 
            this.btnFunction_1.Location = new System.Drawing.Point(9, 19);
            this.btnFunction_1.Name = "btnFunction_1";
            this.btnFunction_1.Size = new System.Drawing.Size(90, 50);
            this.btnFunction_1.TabIndex = 2;
            this.btnFunction_1.Text = "Calibrate";
            this.btnFunction_1.UseVisualStyleBackColor = true;
            this.btnFunction_1.Click += new System.EventHandler(this.btnCalibrateFos_Click);
            // 
            // btnSwitchFunction
            // 
            this.btnSwitchFunction.Location = new System.Drawing.Point(6, 76);
            this.btnSwitchFunction.Name = "btnSwitchFunction";
            this.btnSwitchFunction.Size = new System.Drawing.Size(100, 50);
            this.btnSwitchFunction.TabIndex = 9;
            this.btnSwitchFunction.Text = "Switch Function";
            this.btnSwitchFunction.UseVisualStyleBackColor = true;
            this.btnSwitchFunction.Click += new System.EventHandler(this.btnSwitchFunction_Click);
            // 
            // cbSwitchFunction
            // 
            this.cbSwitchFunction.FormattingEnabled = true;
            this.cbSwitchFunction.Items.AddRange(new object[] {
            "Off",
            "Test - Random",
            "Test - Speed",
            "Test - Network Response",
            "Test - Network Speed"});
            this.cbSwitchFunction.Location = new System.Drawing.Point(114, 76);
            this.cbSwitchFunction.Name = "cbSwitchFunction";
            this.cbSwitchFunction.Size = new System.Drawing.Size(206, 21);
            this.cbSwitchFunction.TabIndex = 8;
            this.cbSwitchFunction.Text = "Select Function";
            // 
            // gbTesting
            // 
            this.gbTesting.Controls.Add(this.btnLogData);
            this.gbTesting.Controls.Add(this.btnResetAllNodes);
            this.gbTesting.Controls.Add(this.btnResetSelNode);
            this.gbTesting.Controls.Add(this.btnStopNetworkTest);
            this.gbTesting.Controls.Add(this.btnStartNetTestAllNodes);
            this.gbTesting.Controls.Add(this.btnStartNetTestSelNode);
            this.gbTesting.Controls.Add(this.lblDelayMultiplier);
            this.gbTesting.Controls.Add(this.tbDelayMultiplier);
            this.gbTesting.Controls.Add(this.btnSwitchFunction);
            this.gbTesting.Controls.Add(this.cbSwitchFunction);
            this.gbTesting.Controls.Add(this.btnTestNodeConnections);
            this.gbTesting.Controls.Add(this.btnMonitorNodes);
            this.gbTesting.Controls.Add(this.btnRawData);
            this.gbTesting.Location = new System.Drawing.Point(334, 361);
            this.gbTesting.Name = "gbTesting";
            this.gbTesting.Size = new System.Drawing.Size(332, 268);
            this.gbTesting.TabIndex = 16;
            this.gbTesting.TabStop = false;
            this.gbTesting.Text = "Testing";
            // 
            // btnLogData
            // 
            this.btnLogData.Location = new System.Drawing.Point(218, 188);
            this.btnLogData.Name = "btnLogData";
            this.btnLogData.Size = new System.Drawing.Size(100, 50);
            this.btnLogData.TabIndex = 24;
            this.btnLogData.Text = "Log Data";
            this.btnLogData.UseVisualStyleBackColor = true;
            this.btnLogData.Click += new System.EventHandler(this.btnLogData_Click);
            // 
            // btnResetAllNodes
            // 
            this.btnResetAllNodes.Location = new System.Drawing.Point(112, 188);
            this.btnResetAllNodes.Name = "btnResetAllNodes";
            this.btnResetAllNodes.Size = new System.Drawing.Size(100, 50);
            this.btnResetAllNodes.TabIndex = 23;
            this.btnResetAllNodes.Text = "Reset All Nodes";
            this.btnResetAllNodes.UseVisualStyleBackColor = true;
            this.btnResetAllNodes.Click += new System.EventHandler(this.btnResetAllNodes_Click);
            // 
            // btnResetSelNode
            // 
            this.btnResetSelNode.Location = new System.Drawing.Point(6, 188);
            this.btnResetSelNode.Name = "btnResetSelNode";
            this.btnResetSelNode.Size = new System.Drawing.Size(100, 50);
            this.btnResetSelNode.TabIndex = 22;
            this.btnResetSelNode.Text = "Reset Selected Node";
            this.btnResetSelNode.UseVisualStyleBackColor = true;
            this.btnResetSelNode.Click += new System.EventHandler(this.btnResetSelNode_Click);
            // 
            // btnStopNetworkTest
            // 
            this.btnStopNetworkTest.Location = new System.Drawing.Point(218, 132);
            this.btnStopNetworkTest.Name = "btnStopNetworkTest";
            this.btnStopNetworkTest.Size = new System.Drawing.Size(100, 50);
            this.btnStopNetworkTest.TabIndex = 21;
            this.btnStopNetworkTest.Text = "Stop Network Test";
            this.btnStopNetworkTest.UseVisualStyleBackColor = true;
            this.btnStopNetworkTest.Click += new System.EventHandler(this.btnStopNetworkTest_Click);
            // 
            // btnStartNetTestAllNodes
            // 
            this.btnStartNetTestAllNodes.Location = new System.Drawing.Point(112, 132);
            this.btnStartNetTestAllNodes.Name = "btnStartNetTestAllNodes";
            this.btnStartNetTestAllNodes.Size = new System.Drawing.Size(100, 50);
            this.btnStartNetTestAllNodes.TabIndex = 20;
            this.btnStartNetTestAllNodes.Text = "Start Network Test - All Nodes";
            this.btnStartNetTestAllNodes.UseVisualStyleBackColor = true;
            this.btnStartNetTestAllNodes.Click += new System.EventHandler(this.btnStartNetTestAllNodes_Click);
            // 
            // btnStartNetTestSelNode
            // 
            this.btnStartNetTestSelNode.Location = new System.Drawing.Point(6, 132);
            this.btnStartNetTestSelNode.Name = "btnStartNetTestSelNode";
            this.btnStartNetTestSelNode.Size = new System.Drawing.Size(100, 50);
            this.btnStartNetTestSelNode.TabIndex = 19;
            this.btnStartNetTestSelNode.Text = "Start Network Test - Selected Node";
            this.btnStartNetTestSelNode.UseVisualStyleBackColor = true;
            this.btnStartNetTestSelNode.Click += new System.EventHandler(this.btnStartNetTestSelNode_Click);
            // 
            // lblDelayMultiplier
            // 
            this.lblDelayMultiplier.AutoSize = true;
            this.lblDelayMultiplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelayMultiplier.Location = new System.Drawing.Point(217, 103);
            this.lblDelayMultiplier.Name = "lblDelayMultiplier";
            this.lblDelayMultiplier.Size = new System.Drawing.Size(103, 16);
            this.lblDelayMultiplier.TabIndex = 17;
            this.lblDelayMultiplier.Text = " Delay Multiplier";
            // 
            // tbDelayMultiplier
            // 
            this.tbDelayMultiplier.Location = new System.Drawing.Point(115, 102);
            this.tbDelayMultiplier.Name = "tbDelayMultiplier";
            this.tbDelayMultiplier.Size = new System.Drawing.Size(96, 20);
            this.tbDelayMultiplier.TabIndex = 16;
            this.tbDelayMultiplier.Text = "0";
            // 
            // displayUpdateTimer
            // 
            this.displayUpdateTimer.Interval = 11;
            this.displayUpdateTimer.Tick += new System.EventHandler(this.displayUpdateTimer_Tick);
            // 
            // sqlConnectButton
            // 
            this.sqlConnectButton.Location = new System.Drawing.Point(1076, 529);
            this.sqlConnectButton.Name = "sqlConnectButton";
            this.sqlConnectButton.Size = new System.Drawing.Size(100, 50);
            this.sqlConnectButton.TabIndex = 17;
            this.sqlConnectButton.Text = "Connect to SQL Server";
            this.sqlConnectButton.UseVisualStyleBackColor = true;
            this.sqlConnectButton.Click += new System.EventHandler(this.sqlConnectButton_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.Location = new System.Drawing.Point(112, 188);
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(90, 50);
            this.btnDeselect.TabIndex = 9;
            this.btnDeselect.Text = "Deselect Tower";
            this.btnDeselect.UseVisualStyleBackColor = true;
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // YBOT_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 641);
            this.Controls.Add(this.sqlConnectButton);
            this.Controls.Add(this.gbTesting);
            this.Controls.Add(this.gbFieldFunctions);
            this.Controls.Add(this.btnLaunchGame);
            this.Controls.Add(this.gbCanbus);
            this.Controls.Add(this.gbOutputControls);
            this.Controls.Add(this.gbLightControls);
            this.Controls.Add(this.btnUpdateFile);
            this.Controls.Add(this.btnReloadFile);
            this.Controls.Add(this.btnAddNewNode);
            this.Controls.Add(this.btnSetAllWifi);
            this.Controls.Add(this.btnSetAllXBee);
            this.Controls.Add(this.btnSetAllCAN);
            this.Controls.Add(this.nodeDG);
            this.Name = "YBOT_Main";
            this.Text = "YBOT Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.YBOT_Main_FormClosed);
            this.Shown += new System.EventHandler(this.YBOT_Main_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nodeDG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeDS)).EndInit();
            this.cmGridRightClick.ResumeLayout(false);
            this.gbLightControls.ResumeLayout(false);
            this.gbOutputControls.ResumeLayout(false);
            this.gbCanbus.ResumeLayout(false);
            this.gbCanbus.PerformLayout();
            this.gbFieldFunctions.ResumeLayout(false);
            this.gbTesting.ResumeLayout(false);
            this.gbTesting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView nodeDG;
        private System.Windows.Forms.Button btnSetAllCAN;
        private System.Windows.Forms.Button btnSetAllXBee;
        private System.Windows.Forms.Button btnSetAllWifi;
        private System.Windows.Forms.Button btnAddNewNode;
        private System.Data.DataSet nodeDS;
        private System.Windows.Forms.Button btnReloadFile;
        private System.Windows.Forms.Button btnUpdateFile;
        private System.Windows.Forms.Button btnTestNodeConnections;
        private System.Windows.Forms.ContextMenuStrip cmGridRightClick;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteRow;
        private System.Windows.Forms.ToolStripMenuItem setTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cANBUSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xBeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wiFiToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbLightControls;
        private System.Windows.Forms.Button btnLightTest;
        private System.Windows.Forms.GroupBox gbOutputControls;
        private System.Windows.Forms.Button btnToggleAllOutpus;
        private System.Windows.Forms.Button btnOutPut1;
        private System.Windows.Forms.Button btnOutput8;
        private System.Windows.Forms.Button btnOutput7;
        private System.Windows.Forms.Button btnOutput6;
        private System.Windows.Forms.Button btnOutpu3;
        private System.Windows.Forms.Button btnOutput4;
        private System.Windows.Forms.Button btnOutput5;
        private System.Windows.Forms.Button btnOutput2;
        private System.Windows.Forms.Button btnLightRed;
        private System.Windows.Forms.Button btnLightWhite;
        private System.Windows.Forms.Button btnLightBlue;
        private System.Windows.Forms.Button btnLightsOff;
        private System.Windows.Forms.Button btnLightYellow;
        private System.Windows.Forms.Button btnLightGreen;
        private System.Windows.Forms.Button btnLightAll;
        private System.Windows.Forms.GroupBox gbCanbus;
        private System.Windows.Forms.ComboBox cbCanPort;
        private System.Windows.Forms.Button btnCanOpenPort;
        private System.Windows.Forms.Button btnCanClosePort;
        private System.Windows.Forms.Button btnCanRefreshList;
        private System.Windows.Forms.Button btnLaunchGame;
        private System.Windows.Forms.Button btnMonitorNodes;
        private System.Windows.Forms.Button btnCANChangeMode;
        private System.Windows.Forms.ComboBox cbGameModes;
        private System.Windows.Forms.Button btnRawData;
        private System.Windows.Forms.Button btnTestAllNodesLights;
        private System.Windows.Forms.Button btnChangeModeAll;
        private System.Windows.Forms.GroupBox gbFieldFunctions;
        private System.Windows.Forms.Button btnFunction_1;
        private System.Windows.Forms.Button btnRingBell;
        private System.Windows.Forms.Button btnTransmitterOff;
        private System.Windows.Forms.Button btnTransmitterAuto;
        private System.Windows.Forms.Button btnTransmitterManual;
        private System.Windows.Forms.Button btnSoundBuzzer;
        private System.Windows.Forms.Button btnSwitchFunction;
        private System.Windows.Forms.ComboBox cbSwitchFunction;
        private System.Windows.Forms.GroupBox gbTesting;
        private System.Windows.Forms.Label lblDelayMultiplier;
        private System.Windows.Forms.TextBox tbDelayMultiplier;
        private System.Windows.Forms.Button btnStopNetworkTest;
        private System.Windows.Forms.Button btnStartNetTestAllNodes;
        private System.Windows.Forms.Button btnStartNetTestSelNode;
        private System.Windows.Forms.Button btnResetAllNodes;
        private System.Windows.Forms.Button btnResetSelNode;
        private System.Windows.Forms.Timer displayUpdateTimer;
        private System.Windows.Forms.Button btnChangeAllBaudRate;
        private System.Windows.Forms.Button btnChangeBaudRate;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Button btnLogData;
        private System.Windows.Forms.CheckBox cbRTSenable;
        private System.Windows.Forms.CheckBox cbDTRenable;
        private System.Windows.Forms.Button btnSetSunLocation;
        private System.Windows.Forms.Button sqlConnectButton;
        private System.Windows.Forms.Button btnDeselect;
    }
}

