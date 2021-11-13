﻿using System;
using System.IO;

namespace ACNHPoker
{

    class Log
    {
        public Log()
        {
        }

        public static void logEvent(string Location, string Message)
        {
            if (!File.Exists(Utilities.logPath))
            {
                string fullPath = Path.GetFullPath(Utilities.logPath);
                string logFolder = Path.GetDirectoryName(fullPath);
                if(!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }

                string logheader = "Timestamp" + "," + "Form" + "," + "Message";

                using (StreamWriter sw = File.CreateText(Utilities.logPath))
                {
                    sw.WriteLine(logheader);
                }
            }

            DateTime localDate = DateTime.Now;

            string newLog = localDate.ToString() + "," + Location + "," + Message;

            using (StreamWriter sw = File.AppendText(Utilities.logPath))
            {
                sw.WriteLine(newLog);
            }
        }
    }
}
