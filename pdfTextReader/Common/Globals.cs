﻿using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace pdfTextReader.Common
{
    class Globals
    {
        // KNB : 6/12/2021 : All private variables to the project used throughout the application
        private static string baseDirectory;
        private static string logFolder;
        private static string logFile;
        private static string dailyFolder;
        private static string yesterdayFolder;
        private static string dailyDownloadFile = DateTime.Now.ToString("yyyy-MM-dd") + " omma_dispensaries_list.pdf";
        private static string yesterdayDownloadFile = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " omma_dispensaries_list.pdf";

        private static string dispensaryURL = "https://oklahoma.gov/content/dam/ok/en/omma/docs/business-lists/omma_dispensaries_list.pdf";
       
        // KNB : Check that all folder paths needed for the application exists and if not create them.
        public static void SyncApplication()
        {
            // KNB : 06/12/21 : Get index of source code and go one directory up to set base directory
            // KNB : 06/12/21 : WILL THIS WORK ONCE IN PRODUCTION????
            int index = AppDomain.CurrentDomain.BaseDirectory.ToLower().IndexOf("source");          
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory.ToLower().Remove(index) + "NewDispoInfo\\";

            // Check if base Directory exists if not create it
            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }

            logFolder = baseDirectory + "Log\\";

            // Check if Log Folder exists, if not create it
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            // No need to check if it exists as long as it is always opened via Append Method which creates it if needed
            logFile = logFolder + "Log.txt";

            dailyFolder = baseDirectory + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            yesterdayFolder = baseDirectory + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "\\";
            // Check if the daily folder exists if not create it, dont worry about yesterdays since it may not exist on the first run
            if (!Directory.Exists(dailyFolder))
            {
                Directory.CreateDirectory(dailyFolder);
            }

            Thread.Sleep(2000);

        }
        public static string GetBaseDirectory()
        {
            return baseDirectory;
        }
        public static string GetLogFolder()
        {
            return logFolder;
        }
        public static string GetLogFile()
        {
            return logFile;
        }
        public static string GetDailyFolder()
        {
            return dailyFolder;
        }
        public static string GetYesterdayFolder()
        {
            return yesterdayFolder;
        }
        public static string GetDispensaryURL()
        {
            return dispensaryURL;
        }
        public static string GetDailyDownloadFile()
        {
            return dailyDownloadFile;
        }
        public static string GetYesterdayDownloadFile()
        {
            return yesterdayDownloadFile;
        }
        // KNB : 6/6/21 : Accpet string and post to console window.
        public static void LogIt(string logMessage) { Console.WriteLine("["+DateTime.Now.ToString()+ "] --- " + logMessage); }

        // KNB : 6/6/Prefix the date to the newest file in the path.
        public static void PrefixDateToLatestFile(WebDriver driver)
        {
            Globals.LogIt("Appending Date to newest file in the download directory.");
            DirectoryInfo downloadDirectory = new DirectoryInfo(GetDailyFolder());
            FileInfo newestFile = downloadDirectory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();

            string newName = DateTime.Now.ToString("yyyy-MM-dd") + " " + newestFile.Name;

            newestFile.MoveTo(GetDailyFolder() + newName);

            return;
        }


    }
}
