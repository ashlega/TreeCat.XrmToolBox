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
    ExportMetadata("SmallImageBase64", "R0lGODlhQABAAMZ1AAAAzAAAzQEBzAICzAMDzAQEzQUFzQYGzQcHzQgIzQkJzgoKzgsLzg0Nzg8PzxAQzxISzxQU0BcX0BgY0Rwc0R0d0R8f0iEh0iQk0ycn0ygo1Ckp1DAw1TEx1TQ01jY21jc31zw82D4+2D8/2EJC2UND2UZG2khI2k5O21JS3FRU3VZW3VdX3V9f32Bg32Ji32Rk4Gdn4Glp4Gpq4W1t4W5u4W9v4nNz4nR043Z243d343h444CA5YKC5oaG5oeH54qK55CQ6JiY6pyc652d65+f66Ki7KSk7Kio7amp7aur7q2t7q6u7rCw77Gx77a28Lq68Lu78by88cDA8sPD8sXF88rK9MzM9NDQ9dTU9dbW9tfX9tjY9tnZ9t7e99/f+ODg+OHh+OLi+OPj+OXl+ebm+efn+ejo+enp+evr+u7u+vDw+/Hx+/Ly+/Pz+/T0/PX1/Pf3/Pj4/Pr6/f39/f///////////////////////////////////////////yH+EUNyZWF0ZWQgd2l0aCBHSU1QACH5BAEKAH8ALAAAAABAAEAAAAf+gAGCg4SFhoeIiYqLjI2Oj5CRkpOUlZaXmJmam5ydnp+goaKjpKWmp6ipqqusra6vsLGylwwuSF1qcWNQNxCLFHV1bQOCwMLEnTJnwczMazaKK8FSg9J11JwHRs3czEMAiEHBPYPideScRN3rdTqIVsEng/B18psszWU4GwkKIEV0mMGZYCjBnDp0HAgyiFChJgBbmFHxVQhGMx6GRATLMkhjHY6bTDBLEwGRumBbBLFbqQkIMx+JOnwp0sKCypXrNFVhNgISzpyZwjArKUlDMDSDjNZByskNswKTXgRrMkhqHaqc5DBDMGlIsByDvNYBy8kMMwmTtAQLMUhtHba1nLAwI5EIw8yaN3820ySE2Y9EO5hxyas3mCYVzNBQLNSADDMghAtrMjCGWZQGhQosYUYnA6EpwVIMAl1HtKcYzb7MuGDgAYorzZgQIvAmGMEAtG2DUlKYTQVCHoJ5GRS8znBQC5z8jFOiUI1gRwY9rxM9lIAda9h1+WAISbAZg7zXAT8KAo0nYODA+ZKEBYFDYoJxGBS/zvxZ+PPr38+/v///AAYo4IAEFmjggQgmqOCCDDIYCAA7"),//"a base64 encoded image"),
    ExportMetadata("BigImageBase64", "R0lGODlhQABAAMZ1AAAAzAAAzQEBzAICzAMDzAQEzQUFzQYGzQcHzQgIzQkJzgoKzgsLzg0Nzg8PzxAQzxISzxQU0BcX0BgY0Rwc0R0d0R8f0iEh0iQk0ycn0ygo1Ckp1DAw1TEx1TQ01jY21jc31zw82D4+2D8/2EJC2UND2UZG2khI2k5O21JS3FRU3VZW3VdX3V9f32Bg32Ji32Rk4Gdn4Glp4Gpq4W1t4W5u4W9v4nNz4nR043Z243d343h444CA5YKC5oaG5oeH54qK55CQ6JiY6pyc652d65+f66Ki7KSk7Kio7amp7aur7q2t7q6u7rCw77Gx77a28Lq68Lu78by88cDA8sPD8sXF88rK9MzM9NDQ9dTU9dbW9tfX9tjY9tnZ9t7e99/f+ODg+OHh+OLi+OPj+OXl+ebm+efn+ejo+enp+evr+u7u+vDw+/Hx+/Ly+/Pz+/T0/PX1/Pf3/Pj4/Pr6/f39/f///////////////////////////////////////////yH+EUNyZWF0ZWQgd2l0aCBHSU1QACH5BAEKAH8ALAAAAABAAEAAAAf+gAGCg4SFhoeIiYqLjI2Oj5CRkpOUlZaXmJmam5ydnp+goaKjpKWmp6ipqqusra6vsLGylwwuSF1qcWNQNxCLFHV1bQOCwMLEnTJnwczMazaKK8FSg9J11JwHRs3czEMAiEHBPYPideScRN3rdTqIVsEng/B18psszWU4GwkKIEV0mMGZYCjBnDp0HAgyiFChJgBbmFHxVQhGMx6GRATLMkhjHY6bTDBLEwGRumBbBLFbqQkIMx+JOnwp0sKCypXrNFVhNgISzpyZwjArKUlDMDSDjNZByskNswKTXgRrMkhqHaqc5DBDMGlIsByDvNYBy8kMMwmTtAQLMUhtHba1nLAwI5EIw8yaN3820ySE2Y9EO5hxyas3mCYVzNBQLNSADDMghAtrMjCGWZQGhQosYUYnA6EpwVIMAl1HtKcYzb7MuGDgAYorzZgQIvAmGMEAtG2DUlKYTQVCHoJ5GRS8znBQC5z8jFOiUI1gRwY9rxM9lIAda9h1+WAISbAZg7zXAT8KAo0nYODA+ZKEBYFDYoJxGBS/zvxZ+PPr38+/v///AAYo4IAEFmjggQgmqOCCDDIYCAA7"),//"a base64 encoded image"),
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
