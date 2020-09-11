using DynamicWrapperCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GacWithFusionConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var x = GlobalAssemblyCacheHelper.GetAssemblyNames("Siemens.Sinumerik.Operate.Services");
            var a = GlobalAssemblyCacheHelper.GetAssemblies("Siemens.Sinumerik.Operate.Services");
            var list = new List<string>();
            foreach (var y in a)
            {
                if (GlobalAssemblyCacheHelper.TryGetAssemblyLocation(y, out var i))
                {
                    list.Add(i);
                }
            }

            foreach (var assemblyName in GlobalAssemblyCacheHelper.GetAssemblies())
            {
                Console.WriteLine(assemblyName);
            }
        }

        //public static IEnumerable<AssemblyName> GetGacAssemblyFullNames()
        //{
        //    GlobalAssemblyCacheHelper.WinApi.IApplicationContext applicationContext;

        //    GlobalAssemblyCacheHelper.WinApi.CreateAssemblyEnum(out var assemblyEnum, null, null, GlobalAssemblyCacheHelper.WinApi.CacheFlags.AsmCacheGac, IntPtr.Zero);
        //    while (assemblyEnum.GetNextAssembly(out applicationContext, out var assemblyName, 0) == 0)
        //    {
        //        uint nChars = 0;
        //        assemblyName.GetDisplayName(null, ref nChars, 0);

        //        StringBuilder name = new StringBuilder((int)nChars);
        //        assemblyName.GetDisplayName(name, ref nChars, 0);

        //        AssemblyName a = null;
        //        try
        //        {
        //            a = new AssemblyName(name.ToString());
        //        }
        //        catch (Exception)
        //        {
        //        }

        //        if (a != null)
        //        {
        //            yield return a;
        //        }
        //    }
        //}
    }
}