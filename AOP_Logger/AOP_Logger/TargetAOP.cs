using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AOP_Logger
{
    [LoggerAttribute("E:\\AOP_LoggerFile.txt")]
    public class TargetAOP
    {
        public TargetAOP(int parameter) { }

        public void First()
        {
            //code
        }

        public int Second(int parameter1, object parameter2)
        {
            return 10;
        }
    }
}