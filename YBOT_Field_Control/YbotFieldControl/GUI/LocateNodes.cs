using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YbotFieldControl
{
    public partial class OneWire_Locate_Nodes : Form
    {
        public static int NodeID;
        public static int couplerID;
        public static bool clicked = false;
        public static bool allDone = false;

        public OneWire_Locate_Nodes()
        {
            InitializeComponent();
        }

        private void btn_Node1_Click(object sender, EventArgs e)
        {
            NodeID = 1;
            btn_Node1.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node2_Click(object sender, EventArgs e)
        {
            NodeID = 2;
            btn_Node2.BackColor = Color.Red;
            clicked = true;

        }

        private void btn_Node3_Click(object sender, EventArgs e)
        {
            NodeID = 3;
            btn_Node3.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node4_Click(object sender, EventArgs e)
        {
            NodeID = 4;
            btn_Node4.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node5_Click(object sender, EventArgs e)
        {
            NodeID = 5;
            btn_Node5.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node6_Click(object sender, EventArgs e)
        {
            NodeID = 6;
            btn_Node6.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node7_Click(object sender, EventArgs e)
        {
            NodeID = 7;
            btn_Node7.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node8_Click(object sender, EventArgs e)
        {
            NodeID = 8;
            btn_Node8.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node9_Click(object sender, EventArgs e)
        {
            NodeID = 9;
            btn_Node9.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node10_Click(object sender, EventArgs e)
        {
            NodeID = 10;
            btn_Node10.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node11_Click(object sender, EventArgs e)
        {
            NodeID = 11;
            btn_Node11.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node12_Click(object sender, EventArgs e)
        {
            NodeID = 12;
            btn_Node12.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node13_Click(object sender, EventArgs e)
        {
            NodeID = 13;
            btn_Node13.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node14_Click(object sender, EventArgs e)
        {
            NodeID = 14;
            btn_Node14.BackColor = Color.Red;
            clicked = true;
        }

        private void btn_Node0_Click(object sender, EventArgs e)
        {
            NodeID = 0;
            btn_Node0.BackColor = Color.Red;
            clicked = true;
        }

        private void btndone_Click(object sender, EventArgs e)
        {
            allDone = true;
        }

        private void btnCoupler1_Click(object sender, EventArgs e)
        {
            couplerID = 1;
            btnCoupler1.BackColor = Color.Red;
            clicked = true;
        }

        private void btnCoupler2_Click(object sender, EventArgs e)
        {
            couplerID = 2;
            btnCoupler2.BackColor = Color.Red;
            clicked = true;
        }

        private void btnCoupler3_Click(object sender, EventArgs e)
        {
            couplerID = 3;
            btnCoupler3.BackColor = Color.Red;
            clicked = true;
        }
    }
}
