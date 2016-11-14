using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AOP_Logger;

namespace AssemblyPart
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Неверные данные!");
                return;
            }

            if (File.Exists(args[0]))
            {
                Modifications modifications = new Modifications(args[0]);
                Console.WriteLine("Выполнение изменений");
            }
            else
                Console.WriteLine("Файл не найден!");
        }
    }
}