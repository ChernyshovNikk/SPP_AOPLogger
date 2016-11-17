using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP_Target
{
    public class Program
    {
        static void Main(string[] args)
        {
            TargetAOP target = new TargetAOP(9);
            target.First("Hello");
            target.First("world");
            target.Second(1, false);
            target.First("Welcome");
            target.Second(5, true);
            target.Second(22, true);
            target.First("Today");
            target.Second(13, false);
        }
    }
}