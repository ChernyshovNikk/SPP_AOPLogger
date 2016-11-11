using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP_Logger
{
    public class Program
    {
        static void Main(string[] args)
        {
            TargetAOP target = new TargetAOP(9);
            target.First();
            target.First();
            target.Second(1, new object());
            target.First();
            target.Second(5, new char());
            target.Second(22, new object());
            target.First();
            target.Second(9, new int());
        }
    }
}