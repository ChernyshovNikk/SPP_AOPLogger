using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using AssemblyPart;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_AOPLogger
{
    [TestClass]
    public class Test_Logger
    {
        [TestMethod]
        public void WorkAOPLogger_Test()
        {
            List<string> expectList = new List<string>() {"{Test_AOPTarget}. METHOD: {.ctor}. PARAMETERS: {parameter1 = 10, parameter2 = someParam}",
                                                          "{Test_AOPTarget}. METHOD: {GetText}. PARAMETERS: {none} and RETURNS {Hello}",
                                                          "{Test_AOPTarget}. METHOD: {ChangeParameter_1}. PARAMETERS: {parameter1 = Text_Is, parameter2 = 22} and RETURNS {Text_Is_Change}",
                                                          "{Test_AOPTarget}. METHOD: {ChangeParameter_2}. PARAMETERS: {parameter1 = 37, parameter2 = newText} and RETURNS {50}",
                                                          "{Test_AOPTarget}. METHOD: {GetNothing}. PARAMETERS: {none}",
                                                          "{Test_AOPTarget}. METHOD: {GetText}. PARAMETERS: {none} and RETURNS {Hello}",
                                                          "{Test_AOPTarget}. METHOD: {ChangeParameter_2}. PARAMETERS: {parameter1 = 0, parameter2 = newText} and RETURNS {13}"};
            List<string> analyzeList = new List<string>();
            string fileName = "E:\\СПП. Лабораторные\\FourthLab\\AOP_Logger\\TargetForTest\\bin\\Debug\\TargetForTest.exe";
            Modifications assemblyModifier = new Modifications(fileName);
            GetFileAOPLoggers fileLoggers = new GetFileAOPLoggers(fileName);
            string loggerFile = "E:\\СПП. Лабораторные\\FourthLab\\AOP_Logger\\TargetForTest\\bin\\Debug\\AOP_LoggerFile.txt";
            analyzeList = fileLoggers.GetLoggersList(loggerFile);
            CollectionAssert.AreEqual(expectList, analyzeList);
        }
    }
}
