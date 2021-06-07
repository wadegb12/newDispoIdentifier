﻿using System;
using System.IO;

namespace pdfTextReader.Services
{
    class Logger
    {
        public string LogFile { get; set; }

        public Logger(string logFileLocation)
        {
            LogFile = logFileLocation;
        }

        public void WriteLog(string log)
        {
            if (!File.Exists(LogFile))
            {
                //create log file
            }

            using (StreamWriter sw = File.AppendText(LogFile))
            {
                sw.WriteLine("[" + DateTime.Now.ToString() + "] --- " + log);
            }
        }
    }
}
