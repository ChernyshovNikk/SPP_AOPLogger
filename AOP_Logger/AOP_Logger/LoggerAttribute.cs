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
        private FileStream fileStream;
        private StreamWriter streamWriter;

        public LoggerAttribute(string fileName)
        {
            this.loggerFileName = fileName;
            fileStream = new FileStream(loggerFileName, FileMode.Append);
            streamWriter = new StreamWriter(fileStream);
        }

        public void WriteInfoToFile(string loggerStringInformation)
        {
            streamWriter.Write(loggerStringInformation);
            streamWriter.Flush();
        }

        private string GetParameters(Dictionary<string, object> allParameters)
        {
            string stringOfParameters = "";

            foreach (string parameter in allParameters.Keys)
                stringOfParameters = parameter + " = " + allParameters[parameter].ToString();

            if (stringOfParameters == "")
                stringOfParameters = "none";

            return stringOfParameters;
        }

        public virtual void GetLoggerInformation(MethodBase method, Dictionary<string, object> parametersValue, object returnValue)
        {
            WriteInfoToFile(String.Format("CLASS: {{{0}}}. METHOD: {{{1}}}. PARAMETERS: {{{2}}}", method.DeclaringType.Name, method.Name, GetParameters(parametersValue)));

            if (returnValue != null)
                WriteInfoToFile(String.Format(" and RETURNS {{{0}}}{1}", returnValue.ToString(), "\r\n"));
            else
                WriteInfoToFile(String.Format("{0}", "\r\n"));
        }
    }
}