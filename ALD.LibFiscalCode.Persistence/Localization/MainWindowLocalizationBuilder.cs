using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using ALD.LibFiscalCode.Persistence.Models;
using System.Reflection.Emit;
using System.Reflection;

namespace ALD.LibFiscalCode.Persistence.Localization
{
    public class MainWindowLocalizationBuilder
    {
        public MainWindowLocalizationBuilder(IEnumerable<LocalizedString> localizedStrings, CultureInfo cultureInfo)
        {
        }

        public static object GetMainWindowLocalizationImpl(IEnumerable<LocalizedString> localizedStrings,
            CultureInfo cultureInfo)
        {
            AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
            AssemblyBuilder ab =
                AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndCollect);
            ModuleBuilder mb = ab.DefineDynamicModule("MainWindowLocalization");
            var type = mb.DefineType("MainWindowLocalizationImpl");

            foreach (var element in localizedStrings)
            {
                FieldBuilder field = type.DefineField($"_{element.Name}", typeof(string),
                    FieldAttributes.Private);
                var prop = type.DefineProperty(element.Name, PropertyAttributes.None, typeof(string), null);
                MethodBuilder getter = type.DefineMethod(
                    $"get_{element.Name}", // Method name
                    MethodAttributes.Public | MethodAttributes.SpecialName,
                    typeof(string), // Return type
                    new Type[0]);
                MethodBuilder setter = type.DefineMethod(
                    $"set_{element.Name}", // Method name
                    MethodAttributes.Public | MethodAttributes.SpecialName,
                    typeof(string), // Return type
                    new Type[1] {typeof(string)});
                ILGenerator setGen = setter.GetILGenerator();
                setGen.Emit(OpCodes.Ldarg_0); // Load "this" onto eval stack
                setGen.Emit(OpCodes.Ldarg_1); // Load 2nd arg, i.e., value
                setGen.Emit(OpCodes.Stfld, field); // Store value into field
                setGen.Emit(OpCodes.Ret); // return
                ILGenerator getGen = getter.GetILGenerator();
                getGen.Emit(OpCodes.Ldarg_0); // Load "this" onto eval stack
                getGen.Emit(OpCodes.Ldfld, field); // Load field value onto eval stack
                getGen.Emit(OpCodes.Ret); // Return
                prop.SetGetMethod(getter); // Link the get method and property
                prop.SetSetMethod(setter); // Link the set method and property

                //t.SetValue(t.Name, element.Value);
                //t.SetValue(t.Name, null);
            }


            //   var assemblyName = new AssemblyName("ALD.Localization");
            // var assembly = AppDomain.CurrentDomain.GetAssemblies()[0];
            var c = type.CreateType();
            var obj = Activator.CreateInstance(c);
            foreach (var element in c.GetProperties())
            {
                var currentProp = c.GetProperty(element.Name);
                currentProp?.SetValue(obj, element.Name,
                    new object?[0] );
            }

            return obj;
        }
    }
}