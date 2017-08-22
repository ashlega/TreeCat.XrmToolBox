using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastColoredTextBoxNS;



namespace TreeCat.XrmToolBox.CodeNow
{
    [Export(typeof(IXrmToolBoxPlugin)),
    ExportMetadata("BackgroundColor", "MediumBlue"),
    ExportMetadata("PrimaryFontColor", "White"),
    ExportMetadata("SecondaryFontColor", "LightGray"),
    ExportMetadata("SmallImageBase64", null),//"a base64 encoded image"),
    ExportMetadata("BigImageBase64", null),//"a base64 encoded image"),
    ExportMetadata("Name", "Code Now"),
    ExportMetadata("Description", "Use C# Code with Dynamics NOW!")]
    public class CodeNowPlugin : PluginBase, IHelpPlugin
    {
        public string HelpUrl
        {
            get
            {
                return "http://www.itaintboring.com/tcs-tools/code-now-plugin-for-xrmtoolbox/#suggestions";
            }
        }

        public override IXrmToolBoxPluginControl GetControl()
        {
            return new CodeNowPluginControl();
        }
    }
    
}
