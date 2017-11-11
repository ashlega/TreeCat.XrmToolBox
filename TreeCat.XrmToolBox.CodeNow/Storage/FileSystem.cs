using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeCat.XrmToolBox.CodeNow.Storage
{
    public class FileSystem : ICodeNowStorage
    {
        private List<CodeNowScript> scriptList = null;

        public string Title {
            get
            {
                return "File System";
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

        public bool CustomCodeSelector { get { return true; } }

        public bool IsSaveSupported
        {
            get
            {
                return true;
            }
        }

        public List<CodeNowScript> GetScriptList()
        {
            if (scriptList == null)
            {
                scriptList = new List<CodeNowScript>();
                scriptList.Add(new CodeNowScript(Common.CodeNowSample));
                scriptList.Add(new CodeNowScript(Common.PluginSourceSample));
                scriptList.Add(new CodeNowScript(Common.SolutionStatsCode));
            }
            return scriptList;
        }

        

        public CodeNowScript Open()
        {
            CodeNowScript result = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(ofd.FileName);
                string s = sr.ReadToEnd();
                sr.Close();
                result = new CodeNowScript(s);
                result.Location = ofd.FileName;
            }

            return result;
        }

        public bool Save(CodeNowScript script, bool askForLocation)
        {
            string destination = script.Location;
            if (string.IsNullOrEmpty(destination) || askForLocation)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "All Files|*.*";
                sfd.Title = "Save File";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    destination = sfd.FileName;
                }
                else
                {
                    destination = null;
                }
            }
            
            if(destination != null)
            {
                System.IO.File.WriteAllText(destination, script.GetString());
                script.Location = destination;
                script.SourceStorage = this;
            }
            return destination != null;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
