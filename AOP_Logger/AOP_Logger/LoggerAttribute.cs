using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace AOP_Logger
{
    public class LoggerAttribute : Attribute
    {
        private string loggerFileName;

        public LoggerAttribute(string fileName)
        {
            this.loggerFileName = fileName;
        }
    }
}