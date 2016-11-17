using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using AOP_AssemblyAndTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_AOPLogger
{
    [TestClass]
    public class Test_Logger
    {
        [TestMethod]
        public void WorkTestAOPLogger_Test()
        {
            List<string> expectList = new List<string>() {"CLASS: {Test_AOPTarget}. METHOD: {.ctor}. PARAMETERS: {parameter1 = someParam, parameter2 = 13}",
                                                          "CLASS: {Test_AOPTarget}. METHOD: {GetText}. PARAMETERS: {none} and RETURNS {Hello}",
                                                          "CLASS: {Test_AOPTarget}. METHOD: {ChangeParameter_1}. PARAMETERS: {parameter1 = Text_Is, parameter2 = 22} and RETURNS {Text_Is_Change}",
                                                          "CLASS: {Test_AOPTarget}. METHOD: {ChangeParameter_2}. PARAMETERS: {parameter1 = 37, parameter2 = newText} and RETURNS {50}",
                                                          "CLASS: {Test_AOPTarget}. METHOD: {GetNothing}. PARAMETERS: {none}"};
            List<string> analyzeList = new List<string>();
            string fileName = "D:\\FourthLab\\AOP_AssemblyAndTest\\TargetForTest\\bin\\Debug\\TargetForTest.exe";
            Modifications assemblyModifier = new Modifications(fileName);
            GetFileAOPLoggers fileLoggers = new GetFileAOPLoggers(fileName);
            string loggerFile = "D:\\FourthLab\\AOP_AssemblyAndTest\\TargetForTest\\bin\\Debug\\AOP_LoggerFile.txt";
            analyzeList = fileLoggers.GetLoggersList(loggerFile);
            CollectionAssert.AreEqual(expectList, analyzeList);
        }


        [TestMethod]
        public void WorkMainAOPLogger_Test()
        {
            List<string> expectList = new List<string>() {"CLASS: {TargetAOP}. METHOD: {.ctor}. PARAMETERS: {parameter = 9}",
                                                          "CLASS: {TargetAOP}. METHOD: {First}. PARAMETERS: {parameter1 = Hello}",
                                                          "CLASS: {TargetAOP}. METHOD: {First}. PARAMETERS: {parameter1 = world}",
                                                          "CLASS: {TargetAOP}. METHOD: {Second}. PARAMETERS: {parameter1 = 1, parameter2 = false} and RETURNS {11}",
                                                          "CLASS: {TargetAOP}. METHOD: {First}. PARAMETERS: {parameter1 = Welcome}",
                                                          "CLASS: {TargetAOP}. METHOD: {Second}. PARAMETERS: {parameter1 = 5, parameter2 = true} and RETURNS {15}",
                                                          "CLASS: {TargetAOP}. METHOD: {Second}. PARAMETERS: {parameter1 = 22, parameter2 = true} and RETURNS {32}",
                                                          "CLASS: {TargetAOP}. METHOD: {First}. PARAMETERS: {parameter1 = Today}",
                                                          "CLASS: {TargetAOP}. METHOD: {Second}. PARAMETERS: {parameter1 = 13, parameter2 = false} and RETURNS {23}"};
            List<string> analyzeList = new List<string>();
            string fileName = "D:\\FourthLab\\AOP_Target\\AOP_Target\\bin\\Debug\\AOP_Target.exe";
            Modifications assemblyModifier = new Modifications(fileName);
            GetFileAOPLoggers fileLoggers = new GetFileAOPLoggers(fileName);
            string loggerFile = "D:\\FourthLab\\AOP_Target\\AOP_Target\\bin\\Debug\\AOP_LoggerFile.txt";
            analyzeList = fileLoggers.GetLoggersList(loggerFile);
            CollectionAssert.AreEqual(expectList, analyzeList);
        }
    }
}