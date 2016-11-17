using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace AOP_LoggerAttribute
{
    public class LoggerAttribute : Attribute
    {
        private string loggerFileName;
        public FileStream fileStream;
        public StreamWriter streamWriter;

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

            foreach (string currentParameter in allParameters.Keys)
                stringOfParameters += currentParameter + " = " + allParameters[currentParameter].ToString() + ", ";

            if (stringOfParameters != "")
                stringOfParameters = stringOfParameters.Remove(stringOfParameters.Length - 2, 2);
            else
                stringOfParameters = "none";

            return stringOfParameters;
        }


        public void GetEnterParametersValue(MethodBase method, Dictionary<string, object> enterParameters)
        {
            WriteInfoToFile(String.Format("CLASS: {{{0}}}. METHOD: {{{1}}}. PARAMETERS: {{{2}}}", method.DeclaringType.Name, method.Name, GetParameters(enterParameters)));
        }


        public void GetReturnParameterValue(object returnValue)
        {
            if (returnValue != null)
                WriteInfoToFile(String.Format(" and RETURNS {{{0}}}{1}", returnValue.ToString(), "\r\n"));
            else
                WriteInfoToFile(String.Format("{0}", Environment.NewLine));
        }
    }
}