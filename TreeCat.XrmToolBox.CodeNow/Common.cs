using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;


namespace TreeCat.XrmToolBox.CodeNow
{
    public enum COMPILE_ACTION
    {
        RUN_NOW,
        MAKE_EXE,
        COMPILE_DLL
    }

    public class Common
    {
        public static string GetTextResource(string resourceName)
        {
            string result = null;
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //var resourceName = "TreeCat.XrmToolBox.CodeNow.ExeConfig.xml";

            using (System.IO.Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;

            
        }

        public static byte[] GetByteResource(string resourceName)
        {
            byte[] result = null;
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //var resourceName = "TreeCat.XrmToolBox.CodeNow.ExeConfig.xml";

            using (System.IO.Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
                {
                    result = reader.ReadBytes((int)reader.BaseStream.Length);
                }
            }

            return result;


        }

        public string GenerateCode(IOrganizationService service, Delegate delegateInstance, COMPILE_ACTION action, string exeFileName, string code, string usingList)
        {
            string result = null;
            var provider = new Microsoft.CSharp.CSharpCodeProvider();
            var parameters = new System.CodeDom.Compiler.CompilerParameters();

            parameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            parameters.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Xml.dll");
            parameters.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            parameters.ReferencedAssemblies.Add("System.Activities.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.IdentityModel.dll");
            parameters.ReferencedAssemblies.Add("System.ServiceModel.dll");
            parameters.ReferencedAssemblies.Add("System.Runtime.Serialization.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.Crm.Sdk.Proxy.dll");
            parameters.ReferencedAssemblies.Add("System.Configuration.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.Xrm.Sdk.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.Xrm.Tooling.Connector.dll");

            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = action == COMPILE_ACTION.RUN_NOW;
            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = action == COMPILE_ACTION.MAKE_EXE;
            if (action != COMPILE_ACTION.RUN_NOW) parameters.OutputAssembly = exeFileName;

            string snkFileName = null;
            string exeSource = code;
            if (action != COMPILE_ACTION.COMPILE_DLL)
            {
                exeSource = BaseCode.Replace("#CODE#", code).Replace("#USING#", usingList);
            }
            else {
                //need a key file
                snkFileName = System.IO.Path.GetDirectoryName(exeFileName) + "\\codenow.snk";
                parameters.CompilerOptions = "/keyfile:\"" + snkFileName + "\"";
                System.IO.File.WriteAllBytes(snkFileName, CodenowKey);
            }
            System.CodeDom.Compiler.CompilerResults results = null;
            try
            {
                results = provider.CompileAssemblyFromSource(parameters, exeSource);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (snkFileName != null) System.IO.File.Delete(snkFileName);
            }


            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (System.CodeDom.Compiler.CompilerError error in results.Errors)
                {
                    result += (String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText)) + System.Environment.NewLine;
                }

            }
            else if (action == COMPILE_ACTION.RUN_NOW)
            {
                var assembly = results.CompiledAssembly;
                Type program = assembly.GetType("ToolBoxSnippet.Program");
                var main = program.GetMethod("RunCode");
                object[] callParameters = new object[1];

                object[] args = new object[] { delegateInstance, service };
                main.Invoke(null, args);
            }
            else
            {
                result += "Done!";
            }
            return result;
        }

        private static string baseCode = null;
        public static string BaseCode 
        {
            get
            {
                if (baseCode == null) baseCode = GetTextResource("TreeCat.XrmToolBox.CodeNow.SourceFiles.ExeSource.txt");
                return baseCode;
            }
        }

        private static string baseConfig = null;
        public static string BaseConfig
        {
            get
            {
                if (baseConfig == null) baseConfig = GetTextResource("TreeCat.XrmToolBox.CodeNow.SourceFiles.ExeConfig.xml");
                return baseConfig;
            }
        }

        private static string codeNowSample = null;
        public static string CodeNowSample
        {
            get
            {
                if (codeNowSample == null) codeNowSample = GetTextResource("TreeCat.XrmToolBox.CodeNow.SourceFiles.CodeNowSample.txt");
                return codeNowSample;
            }
        }

        private static string pluginSourceSample = null;
        public static string PluginSourceSample
        {
            get
            {
                if (pluginSourceSample == null) pluginSourceSample = GetTextResource("TreeCat.XrmToolBox.CodeNow.SourceFiles.PluginSourceSample.txt");
                return pluginSourceSample;
            }
        }

        private static byte[] codenowKey = null;
        public static byte[] CodenowKey
        {
            get
            {
                if (codenowKey == null) codenowKey = GetByteResource("TreeCat.XrmToolBox.CodeNow.SourceFiles.codenow.snk");
                return codenowKey;
            }
        }
    }
}
