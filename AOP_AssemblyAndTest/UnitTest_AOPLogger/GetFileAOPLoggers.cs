using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace UnitTest_AOPLogger
{
    public class GetFileAOPLoggers
    {
        private FileStream fileStream;
        private StreamReader streamReader;
        private string loggersFromFile;

        public GetFileAOPLoggers(string fileName)
        {
            Process process = new Process();
            process.StartInfo.FileName = fileName;
            process.Start();
            process.WaitForExit();
        }

        public List<string> GetLoggersList(string loggerFile)
        {
            if (File.Exists(loggerFile))
            {
                fileStream = new FileStream(loggerFile, FileMode.Open, FileAccess.Read);
                streamReader = new StreamReader(fileStream);
                loggersFromFile = streamReader.ReadToEnd();
                return loggersFromFile.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
                return null;
        }
    }
}