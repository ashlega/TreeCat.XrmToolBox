using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;

namespace TreeCat.XrmToolBox.CodeNow.Storage
{
    public class Online : ICodeNowStorage
    {
        private static string rootUrl = "http://itaintboring.com/downloads/OnlineScripts/";
        private static string scriptListFile = "1_CodeNowScripts.txt";
        private static int downloadStage = 0;
        private static WebClient wc = null;

        private static List<CodeNowScript> scriptList = null;
        
        static Online()
        {
            wc = new WebClient();
            wc.DownloadStringCompleted += DownloadStringCompleted;
            DownloadScriptList();
        }

        private static string[] files = null;
        private static int fileIndex = 0;

        public static void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null) return;
            string result = e.Result;
            if (downloadStage == 0)
            {
                files = result.Split('\n');
                fileIndex = -1;
            }
            if (files != null)
            {
                if (downloadStage == 1)
                {
                    scriptList.Add(new CodeNowScript(result.Replace("\n", "\r\n")));
                }
                downloadStage = 1;
                fileIndex++;
                while (fileIndex < files.Length && files[fileIndex].Trim().StartsWith("//")) fileIndex++;
                if (fileIndex < files.Length)
                {
                    wc.DownloadStringAsync(new Uri(files[fileIndex].Trim()));
                }
                
            }
        }

        public static void DownloadScriptList()
        {
            scriptList = new List<CodeNowScript>();
            downloadStage = 0;
            fileIndex = 0;
            wc.DownloadStringAsync(new Uri(rootUrl + scriptListFile));
        }



        public string Title {
            get
            {
                return "Online";
            }
        }

        public void Configure()
        {
            
        }

        public bool IsConfigured
        {
            get
            {
                return true;
            }
        }

        public bool CustomCodeSelector { get { return false; } }

        public bool IsSaveSupported
        {
            get
            {
                return false;
            }
        }


        
        public List<CodeNowScript> GetScriptList()
        {
            return scriptList;
        }

        
        public CodeNowScript Open()
        {
            return null;
        }

        public bool Save(CodeNowScript script, bool askForLocation)
        {
            throw new Exception("Save not supported! Choose a different storage.");
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
