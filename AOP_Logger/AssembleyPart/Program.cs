using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AssemblyPart
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;

            if (File.Exists(args[0]))
            {
                Modifications assemblyModifier = new Modifications(args[0]);
                Console.WriteLine("Выполнение изменений", args[0]);
            }
            else
                Console.WriteLine("Файл не найден", args[0]);
        }
    }
}