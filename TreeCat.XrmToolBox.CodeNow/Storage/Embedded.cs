using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCat.XrmToolBox.CodeNow.Storage
{
    public class Embedded : ICodeNowStorage
    {
        private List<CodeNowScript> scriptList = null;

        public string Title {
            get
            {
                return "Samples";
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
