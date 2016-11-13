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
            InjectAssembly();
        }


        private void Insert_CurrentMethod(ILProcessor ilProc, MethodReference getCurrentMethodRef, VariableDefinition currentMethodVar)
        {
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Call, getCurrentMethodRef));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Stloc, currentMethodVar));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Ldloc, currentMethodVar));
        }


        private void Insert_Attribute(ILProcessor ilProc, TypeReference loggerAttributeRef, MethodReference getTypeFromHandleRef, MethodReference getCustomAttributeRef, VariableDefinition attributeVariable)
        {
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Ldtoken, loggerAttributeRef));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Call, getTypeFromHandleRef));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Call, getCustomAttributeRef));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Castclass, loggerAttributeRef));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Stloc, attributeVariable));
        }


        private void Insert_Dictionary(ILProcessor ilProc, MethodDefinition method, MethodReference dictConstructorRef, VariableDefinition parametersVariable, MethodReference dictMethodAddRef)
        {
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Newobj, dictConstructorRef));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Stloc, parametersVariable));
            foreach (var argument in method.Parameters)
            {
                ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Ldloc, parametersVariable));
                ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Ldstr, argument.Name));
                ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Ldarg, argument));
                ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Call, dictMethodAddRef));
            }
        }


        private void Insert_EnterParameters(ILProcessor ilProc, VariableDefinition attributeVariable, VariableDefinition currentMethodVar, VariableDefinition parametersVariable, MethodReference loggerAttributeEnterInfoRef) 
        {
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Ldloc, attributeVariable));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Ldloc, currentMethodVar));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Ldloc, parametersVariable));
            ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Callvirt, loggerAttributeEnterInfoRef));
        }


        private void Insert_ReturnValue(ILProcessor ilProc, MethodDefinition method, TypeReference objectRef, MethodReference loggerAttributeReturnInfoRef, VariableDefinition attributeVariable)
        {
            var returnVariable = new VariableDefinition(objectRef);
            ilProc.Body.Variables.Add(returnVariable);
            Instruction lastInstruction = ilProc.Body.Instructions.Last();

            if (!method.ReturnType.Name.Equals(typeof(void).Name))
            {
                var returnVar = method.Body.Variables.First(var => var.VariableType.Name.Equals(method.ReturnType.Name));
                ilProc.InsertBefore(lastInstruction, Instruction.Create(OpCodes.Ldloc, returnVar));
                if (returnVar.VariableType.IsPrimitive || returnVar.VariableType.IsValueType)
                    ilProc.InsertBefore(lastInstruction, Instruction.Create(OpCodes.Box, returnVar.VariableType));
                ilProc.InsertBefore(lastInstruction, Instruction.Create(OpCodes.Stloc, returnVariable));
            }

            ilProc.InsertBefore(lastInstruction, Instruction.Create(OpCodes.Ldloc, attributeVariable));
            ilProc.InsertBefore(lastInstruction, Instruction.Create(OpCodes.Ldloc, returnVariable));
            ilProc.InsertBefore(lastInstruction, Instruction.Create(OpCodes.Callvirt, loggerAttributeReturnInfoRef));
        }



        public void InjectAssembly()
        {
            var assembly = AssemblyDefinition.ReadAssembly(fileName);
            var getCurrentMethodRef = assembly.MainModule.Import(typeof(MethodBase).GetMethod("GetCurrentMethod"));
            var getCustomAttributeRef = assembly.MainModule.Import(typeof(Attribute).GetMethod("GetCustomAttribute", new Type[] { typeof(MethodInfo), typeof(Type) }));
            var getTypeFromHandleRef = assembly.MainModule.Import(typeof(Type).GetMethod("GetTypeFromHandle"));
            var methodBaseRef = assembly.MainModule.Import(typeof(MethodBase));
            var objectRef = assembly.MainModule.Import(typeof(object));
            var loggerAttributeRef = assembly.MainModule.Import(typeof(LoggerAttribute));
            var loggerAttributeEnterInfoRef = assembly.MainModule.Import(typeof(LoggerAttribute).GetMethod("GetEnterParameters"));
            var loggerAttributeReturnInfoRef = assembly.MainModule.Import(typeof(LoggerAttribute).GetMethod("GetReturnValue"));
            var dictionaryType = Type.GetType("System.Collections.Generic.Dictionary`2[System.String,System.Object]");
            var dictStringObjectRef = assembly.MainModule.Import(dictionaryType);
            var dictConstructorRef = assembly.MainModule.Import(dictionaryType.GetConstructor(Type.EmptyTypes));
            var dictMethodAddRef = assembly.MainModule.Import(dictionaryType.GetMethod("Add"));

            foreach (var typeDef in assembly.MainModule.Types)
            {
                foreach (var method in typeDef.Methods.Where(m => m.CustomAttributes.Where(
                             attr => attr.AttributeType.Resolve().BaseType.Name == "LoggerAttribute").FirstOrDefault() != null))
                {
                    method.Body.InitLocals = true; 
                    var ilProc = method.Body.GetILProcessor();
                    var attributeVariable = new VariableDefinition(loggerAttributeRef);
                    var currentMethodVar = new VariableDefinition(methodBaseRef);
                    var parametersVariable = new VariableDefinition(dictStringObjectRef);

                    ilProc.Body.Variables.Add(attributeVariable);
                    ilProc.Body.Variables.Add(currentMethodVar);
                    ilProc.Body.Variables.Add(parametersVariable);

                    instruction = ilProc.Body.Instructions[0];
                    ilProc.InsertBefore(instruction, Instruction.Create(OpCodes.Nop));

                    Insert_CurrentMethod(ilProc, getCurrentMethodRef, currentMethodVar);
                    Insert_Attribute(ilProc, loggerAttributeRef, getTypeFromHandleRef, getCustomAttributeRef, attributeVariable);
                    Insert_Dictionary(ilProc, method, dictConstructorRef, parametersVariable, dictMethodAddRef);
                    Insert_EnterParameters(ilProc, attributeVariable, currentMethodVar, parametersVariable, loggerAttributeEnterInfoRef);
                    Insert_ReturnValue(ilProc, method, objectRef, loggerAttributeReturnInfoRef, attributeVariable);
                }
            }
            assembly.Write(fileName);
        }
    }
}