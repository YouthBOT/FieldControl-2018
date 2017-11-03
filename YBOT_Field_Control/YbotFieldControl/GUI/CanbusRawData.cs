using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YbotFieldControl
{
    public partial class CANRawData : Form
    {
        fileStructure fs = new fileStructure();

        string filePath                         //Construct filePath to Node data
        {
            get
            {
                string path = fs.xmlFilePath;
                return path;
            }
        }
        string xmlHeader                        //Construct xml Header
        {
            get
            {
                string header = fs.xmlHeader;
                return header;
            }
        }
        public bool finishedStartup = false;    //Finished Flag
        LogWriter lw = new LogWriter();

        public CANRawData()
        {

            InitializeComponent();
            
        }

        private void btnClearText_Click(object sender, EventArgs e)
        {
            tbCANRawData.Clear();
        }

        private void btnSaveText_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.DefaultExt = "*.txt";
                saveFile.Filter = "Text Files | *.txt";

                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                    saveFile.FileName.Length > 0)
                {
                    string name = saveFile.FileName;
                    File.WriteAllText(name, tbCANRawData.Text);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void displayText(string s)
        {           
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(displayText), new object[] { s });
                return;
            }
            tbCANRawData.AppendText(s);
        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
           
        }
    }
}
