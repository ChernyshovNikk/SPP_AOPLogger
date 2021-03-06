﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AOP_LoggerAttribute;

namespace TargetForTest
{
    [LoggerAttribute("D:\\FourthLab\\AOP_AssemblyAndTest\\TargetForTest\\bin\\Debug\\AOP_LoggerFile.txt")]
    public class Test_AOPTarget
    {
        private string testParam1;
        private int testParam2;

        public Test_AOPTarget(string parameter1, int parameter2)
        {
            testParam1 = parameter1 + "_Test";
            testParam2 = parameter2;
        }

        public string GetText()
        {
            return "Hello";
        }

        public string ChangeParameter_1(string parameter1, int parameter2)
        {
            testParam1 = parameter1 + "_Change";
            testParam2 = parameter2;
            return testParam1;
        }

        public int ChangeParameter_2(int parameter1, string parameter2)
        {
            testParam1 = parameter2;
            testParam2 = parameter1 + 13;
            return testParam2;
        }

        public void GetNothing()
        {
            //some code
        }
    }
}