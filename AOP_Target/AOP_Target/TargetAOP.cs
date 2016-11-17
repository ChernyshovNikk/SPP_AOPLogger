using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AOP_LoggerAttribute;

namespace AOP_Target
{
    [LoggerAttribute("AOP_LoggerFile.txt")]
    public class TargetAOP
    {
        public TargetAOP(int parameter) { }

        public void First(string parameter1)
        {
            //code
        }

        public int Second(int parameter1, bool parameter2)
        {
            return parameter1 + 10;
        }
    }
}