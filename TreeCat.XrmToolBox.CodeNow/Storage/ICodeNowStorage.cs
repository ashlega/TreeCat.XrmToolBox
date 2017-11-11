using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCat.XrmToolBox.CodeNow.Storage
{
    public interface ICodeNowStorage
    {
        string Title { get; }

        bool Save(CodeNowScript script, bool askForLocation);
        CodeNowScript Open();
        void Configure();

        bool CustomCodeSelector { get; }
        List<CodeNowScript> GetScriptList();

        bool IsConfigured { get; }
        bool IsSaveSupported { get; }

    }
}
