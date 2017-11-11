using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCat.XrmToolBox.CodeNow
{
    public enum CODE_NOW_SCRIPT_TYPE
    {
        PLUGIN = 1,
        CODE   = 2
    }

    public class CodeNowScript
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Using { get; set; }
        public string Code  { get; set; }
        public string Location { get; set; }

        public CODE_NOW_SCRIPT_TYPE ScriptType { get; set; }

        public Storage.ICodeNowStorage SourceStorage { get; set; }

        public CodeNowScript()
        {

        }

        public CodeNowScript(string s)
        {
            FromString(s);
        }

        public override string ToString()
        {
            return (Category != null ? Category + ": " : "") + Title;
        }

        public string GetData(string s, string tag)
        {
            string tagStart = "/*" + tag.ToUpper();
            string tagEnd = tag.ToUpper() + "*/";
            string result = null;
            string upperS = s.ToUpper();
            int i = upperS.ToUpper().IndexOf(tagStart);
            if(i > -1)
            {
                i = i + tagStart.Length;
                int j = upperS.Length;
                if (tagEnd != null) j = upperS.IndexOf(tagEnd, i);
                if(j > -1)
                {
                    result = s.Substring(i, j - i).Trim();
                }
            }
            return result;
        }

        public string AddData(string data, string tag)
        {
            return "/*" + tag + System.Environment.NewLine + data + System.Environment.NewLine + tag + "*/" + System.Environment.NewLine;
        }

        public void FromString(string s)
        {
            if (s.ToUpper().IndexOf("/*TYPE") < 0)
            {
                var lines = s.Split('\n');
                foreach(var l in lines)
                {
                    if (!l.Trim().ToUpper().StartsWith("USING")) Code += l.TrimEnd()+System.Environment.NewLine;
                }
                if (s.ToUpper().IndexOf("CODENOW()") < 0)
                {
                    ScriptType = CODE_NOW_SCRIPT_TYPE.PLUGIN;
                    Title = "Plugin";
                }
                else
                {
                    ScriptType = CODE_NOW_SCRIPT_TYPE.CODE;
                    Title = "CodeNow";
                }
                Code = Code.Trim();
                Category = "Unspecified";
                Using = Common.BaseUsing;
            }
            else
            {
                Description = GetData(s, "Description");
                Title = GetData(s, "Title");
                Using = GetData(s, "Using");
                Code = GetData(s, "Code");
                Category = GetData(s, "Category");
                string type = GetData(s, "Type");
                ScriptType = type == "Plugin" ? CODE_NOW_SCRIPT_TYPE.PLUGIN : CODE_NOW_SCRIPT_TYPE.CODE;
            }
        }

        public string GetString()
        {

            string result = "";
            result += AddData(Category, "Category");
            result += AddData(Description, "Description");
            result += AddData(Title, "Title");
            result += AddData(ScriptType == CODE_NOW_SCRIPT_TYPE.PLUGIN ? "Plugin" : "Code", "Type");
            result += AddData(Using, "Using");
            result += AddData(Code, "Code");
            return result;
        }


    }
}
