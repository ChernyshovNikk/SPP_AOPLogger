using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using AOP_Logger;

namespace AssemblyPart
{
    public class Modifications
    {
        private string fileName;
        private Instruction instruction;

        public Modifications(string fileName)
        {
            this.fileName = fileName;
            ChangeAssembly();
        }

        public void ChangeAssembly()
        {
            // считываем сборку в формате Mono.Cecil
            var assembly = AssemblyDefinition.ReadAssembly(fileName);
            var getCurrentMethodRef = assembly.MainModule.Import(typeof(MethodBase).GetMethod("GetCurrentMethod"));
            var getCustomAttributeRef = assembly.MainModule.Import(typeof(Attribute).GetMethod("GetCustomAttribute", new Type[] { typeof(MethodInfo), typeof(Type) }));
            var getTypeFromHandleRef = assembly.MainModule.Import(typeof(Type).GetMethod("GetTypeFromHandle"));
            var methodBaseRef = assembly.MainModule.Import(typeof(MethodBase));
            var loggerAttributeRef = assembly.MainModule.Import(typeof(LoggerAttribute));
            var loggerAttributeOnEnterRef = assembly.MainModule.Import(typeof(LoggerAttribute).GetMethod("GetLoggerInformation"));
            var dictionaryType = Type.GetType("System.Collections.Generic.Dictionary`2[System.String,System.Object]");
            var dictStringObjectRef = assembly.MainModule.Import(dictionaryType);
            var dictConstructorRef = assembly.MainModule.Import(dictionaryType.GetConstructor(Type.EmptyTypes));
            var dictMethodAddRef = assembly.MainModule.Import(dictionaryType.GetMethod("Add"));


        }
    }
}