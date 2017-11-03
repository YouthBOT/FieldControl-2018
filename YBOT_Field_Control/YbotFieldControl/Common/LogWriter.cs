using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YBotSqlWrapper;

namespace YbotFieldControl
{
    class LogWriter
    {
        fileStructure fs = new fileStructure();     //Create a fileStructure

        //Directory path for YBOT 1 on the user's desktop
        private string YBOTpath
        {
            get
            {
                string path = fs.filePath;
                return path;
            }
        }

        /// <summary>
        /// Path used for YBOT feild control files
        /// </summary>
        /// <returns>Path as a string (File located on user's desktop</returns>
        public string YBOTFilePath()
        {
            //Store date as a string
            string date = DateTime.Today.ToLongDateString();

            //Path for todays date
            string path = string.Format(@"{0}\{1}", YBOTpath, date);


            //Check to see if folder exists if not then make it; fail no path is created
            try
            {
                if (!Directory.Exists(YBOTpath))
                {
                    Directory.CreateDirectory(YBOTpath);
                    Directory.CreateDirectory(path);
                }
                else if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                path = null;
            }

            //Return path name
            return path;
        }

        public void WriteLog(string text)
        {
            WriteLog(text, "YBOT_Field_Control_Log", "Logs");
        }

        public void WriteLog(string text, string fileName)
        {
            WriteLog(text, fileName, "Logs");
        }

        public void WriteLog(string text, string fileName, string filePath)
        {
            string file = string.Format(@"\{0}.txt", fileName);
            string path = string.Format(@"{0}\{1}", YBOTFilePath(), filePath);
            DateTime now = DateTime.Now;

            //Try and write data to file, return if fail
            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string date = DateTime.Today.ToShortDateString();
                string time = now.TimeOfDay.ToString();
                text = string.Format("{0}_{1} : {2}", date, time, text);
                FileStream fs = new FileStream((path + file), FileMode.Append, FileAccess.Write, FileShare.Write);
                fs.Close();
                StreamWriter sw = new StreamWriter((path + file), true, Encoding.ASCII);
                sw.WriteLine(text);
                sw.Close();
            }
            catch
            {
                return;
            }
        }

        public void Log(string text)
        {
            Log(text, "YBOT_Field_Control_Log", "Logs");
        }

        public void Log(string text, string fileName)
        {
            Log(text, fileName, "Logs");
        }

        public void Log(string text, string fileName, string filePath)
        {
            string file = string.Format(@"\{0}.txt", fileName);
            string path = string.Format(@"{0}\{1}", YBOTFilePath(), filePath);

            //Try and write data to file, return if fail
            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                text = string.Format(text);
                FileStream fs = new FileStream((path + file), FileMode.Append, FileAccess.Write, FileShare.Write);
                fs.Close();
                StreamWriter sw = new StreamWriter((path + file), true, Encoding.ASCII);
                sw.WriteLine(text);
                sw.Close();

                YbotSql.Instance.AddLog(text, fileName);
            }
            catch
            {
                return;
            }
        }
    }
}