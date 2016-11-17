using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetForTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_AOPTarget target = new Test_AOPTarget("someParam", 13);
            target.GetText();
            string change1_String = target.ChangeParameter_1("Text_Is", 22);
            int change2_Int = target.ChangeParameter_2(37, "newText");
            target.GetNothing();
        }
    }
}