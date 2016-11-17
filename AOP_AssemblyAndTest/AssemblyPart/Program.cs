using System;
using System.IO;

namespace AOP_AssemblyAndTest
{
    class Program
    {
        static string fileTarget = "D:\\FourthLab\\AOP_Target\\AOP_Target\\bin\\Debug\\AOP_Target.exe";

        static void Main(string[] args)
        {
            if (File.Exists(fileTarget))
            {
                Modifications modifications = new Modifications(fileTarget);
                Console.WriteLine("Работа успешно проведена!");
            }
            else
                Console.WriteLine("Файл не найден!");
        }
    }
}