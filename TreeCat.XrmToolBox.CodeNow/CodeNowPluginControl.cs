using System;
using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;

public delegate void LogMessageDelegate(string msg);

namespace TreeCat.XrmToolBox.CodeNow
{
    

    public partial class CodeNowPluginControl: PluginControlBase, IHelpPlugin
    {
        private string fileName = null;
        
        #region Base tool implementation

        
        public static string BaseUsing = @"using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;";

        private System.ComponentModel.IContainer components;
        private FastColoredTextBoxNS.FastColoredTextBox tbCode;
        private SplitContainer splitContainer4;
        private Panel panel2;
        private SplitContainer splitContainerCode;
        
        private Label label5;
        private Label label6;
        private TextBox tbUsing;
        private TextBox tbLog;
        private ToolStrip toolStrip2;
        private ToolStripButton tbRunCode;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem tbLoadItem;
        private ToolStripMenuItem tbSaveItem;
        private ToolStripMenuItem tbMakeExeItem;
        private CheckBox cbPlugin;
        private ToolStripSeparator toolStripMenuItem1;
        Delegate delegateInstance = null;





        public CodeNowPluginControl()
        {
            
            InitializeComponent();
            delegateInstance = Delegate.CreateDelegate(typeof(LogMessageDelegate), this, "LogMessage");
            HideShowControls();
            ShowSampleCode();

        }

        public void ShowSampleCode()
        {
            if (cbPlugin.Checked)
            {
                tbCode.Text = Common.PluginSourceSample;
            }
            else
            {
                tbCode.Text = Common.CodeNowSample;
                tbUsing.Text = BaseUsing;
            }
        }


        private void BtnCloseClick(object sender, EventArgs e)
        {
            CloseTool(); // PluginBaseControl method that notifies the XrmToolBox that the user wants to close the plugin
            // Override the ClosingPlugin method to allow for any plugin specific closing logic to be performed (saving configs, canceling close, etc...)
        }

        #endregion Base tool implementation

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelWorker(); // PluginBaseControl method that calls the Background Workers CancelAsync method.

            MessageBox.Show("Cancelled");
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeNowPluginControl));
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbPlugin = new System.Windows.Forms.CheckBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbLoadItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbSaveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMakeExeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRunCode = new System.Windows.Forms.ToolStripButton();
            this.splitContainerCode = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbUsing = new System.Windows.Forms.TextBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbCode = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCode)).BeginInit();
            this.splitContainerCode.Panel1.SuspendLayout();
            this.splitContainerCode.Panel2.SuspendLayout();
            this.splitContainerCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCode)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer4
            // 
            this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tbLog);
            this.splitContainer4.Size = new System.Drawing.Size(877, 561);
            this.splitContainer4.SplitterDistance = 400;
            this.splitContainer4.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.cbPlugin);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Controls.Add(this.splitContainerCode);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(871, 394);
            this.panel2.TabIndex = 0;
            // 
            // cbPlugin
            // 
            this.cbPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPlugin.AutoSize = true;
            this.cbPlugin.Location = new System.Drawing.Point(147, 369);
            this.cbPlugin.Name = "cbPlugin";
            this.cbPlugin.Size = new System.Drawing.Size(55, 17);
            this.cbPlugin.TabIndex = 2;
            this.cbPlugin.Text = "Plugin";
            this.cbPlugin.UseVisualStyleBackColor = true;
            this.cbPlugin.Click += new System.EventHandler(this.cbPlugin_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton2,
            this.tbRunCode});
            this.toolStrip2.Location = new System.Drawing.Point(0, 363);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(134, 27);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbLoadItem,
            this.tbSaveItem,
            this.toolStripMenuItem1,
            this.tbMakeExeItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(38, 24);
            this.toolStripDropDownButton2.Text = "File";
            // 
            // tbLoadItem
            // 
            this.tbLoadItem.Name = "tbLoadItem";
            this.tbLoadItem.Size = new System.Drawing.Size(152, 22);
            this.tbLoadItem.Text = "Load";
            this.tbLoadItem.Click += new System.EventHandler(this.tbLoadItem_Click);
            // 
            // tbSaveItem
            // 
            this.tbSaveItem.Name = "tbSaveItem";
            this.tbSaveItem.Size = new System.Drawing.Size(152, 22);
            this.tbSaveItem.Text = "Save";
            this.tbSaveItem.Click += new System.EventHandler(this.tbSaveItem_Click);
            // 
            // tbMakeExeItem
            // 
            this.tbMakeExeItem.Name = "tbMakeExeItem";
            this.tbMakeExeItem.Size = new System.Drawing.Size(152, 22);
            this.tbMakeExeItem.Text = "Compile";
            this.tbMakeExeItem.Click += new System.EventHandler(this.tbMakeExeItem_Click);
            // 
            // tbRunCode
            // 
            this.tbRunCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbRunCode.Image = ((System.Drawing.Image)(resources.GetObject("tbRunCode.Image")));
            this.tbRunCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRunCode.Name = "tbRunCode";
            this.tbRunCode.Size = new System.Drawing.Size(63, 24);
            this.tbRunCode.Text = "Run Code";
            this.tbRunCode.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // splitContainerCode
            // 
            this.splitContainerCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerCode.Location = new System.Drawing.Point(0, 0);
            this.splitContainerCode.Name = "splitContainerCode";
            // 
            // splitContainerCode.Panel1
            // 
            this.splitContainerCode.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerCode.Panel1.Controls.Add(this.tbCode);
            this.splitContainerCode.Panel1.Controls.Add(this.label5);
            // 
            // splitContainerCode.Panel2
            // 
            this.splitContainerCode.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerCode.Panel2.Controls.Add(this.label6);
            this.splitContainerCode.Panel2.Controls.Add(this.tbUsing);
            this.splitContainerCode.Size = new System.Drawing.Size(871, 363);
            this.splitContainerCode.SplitterDistance = 550;
            this.splitContainerCode.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "C# Source Code";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Using";
            // 
            // tbUsing
            // 
            this.tbUsing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUsing.Location = new System.Drawing.Point(4, 25);
            this.tbUsing.Multiline = true;
            this.tbUsing.Name = "tbUsing";
            this.tbUsing.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbUsing.Size = new System.Drawing.Size(310, 335);
            this.tbUsing.TabIndex = 0;
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(3, 3);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(871, 151);
            this.tbLog.TabIndex = 1;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // tbCode
            // 
            this.tbCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCode.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.tbCode.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.tbCode.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.tbCode.BackBrush = null;
            this.tbCode.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.tbCode.CharHeight = 14;
            this.tbCode.CharWidth = 8;
            this.tbCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbCode.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.tbCode.HighlightingRangeType = FastColoredTextBoxNS.HighlightingRangeType.AllTextRange;
            this.tbCode.IsReplaceMode = false;
            this.tbCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.tbCode.LeftBracket = '(';
            this.tbCode.LeftBracket2 = '{';
            this.tbCode.Location = new System.Drawing.Point(7, 25);
            this.tbCode.Name = "tbCode";
            this.tbCode.Paddings = new System.Windows.Forms.Padding(0);
            this.tbCode.RightBracket = ')';
            this.tbCode.RightBracket2 = '}';
            this.tbCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbCode.Size = new System.Drawing.Size(540, 335);
            this.tbCode.TabIndex = 1;
            this.tbCode.Text = "fastColoredTextBox1";
            this.tbCode.Zoom = 100;
            // 
            // CodeNowPluginControl
            // 
            this.Controls.Add(this.splitContainer4);
            this.Name = "CodeNowPluginControl";
            this.Size = new System.Drawing.Size(877, 561);
            this.Load += new System.EventHandler(this.CodeNowPluginControl_Load);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainerCode.Panel1.ResumeLayout(false);
            this.splitContainerCode.Panel1.PerformLayout();
            this.splitContainerCode.Panel2.ResumeLayout(false);
            this.splitContainerCode.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCode)).EndInit();
            this.splitContainerCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbCode)).EndInit();
            this.ResumeLayout(false);

        }


        private void LogMessage(string msg)
        {
            tbLog.Text += msg + System.Environment.NewLine;
        }


        

        private void buttonRun_Click(object sender, EventArgs e)
        {
            var cmn = new Common();
            string result = cmn.GenerateCode(Service, delegateInstance, COMPILE_ACTION.RUN_NOW, null, tbCode.Text, tbUsing.Text);
            if (result == null) LogMessage(result);
        }

       

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

       
        

        private void tbMakeExeItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (cbPlugin.Checked)
            {
                sfd.Filter = "*.dll|*.dll";
                sfd.Title = "Compile a dll";
            }
            else
            {
                sfd.Filter = "*.exe|*.exe";
                sfd.Title = "Make an Executable";
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var cmn = new Common();
                string result = cmn.GenerateCode(Service, delegateInstance, cbPlugin.Checked ? COMPILE_ACTION.COMPILE_DLL : COMPILE_ACTION.MAKE_EXE, sfd.FileName, tbCode.Text, tbUsing.Text);
                LogMessage(result);

                if (cbPlugin.Checked == false)
                {

                    string compiledFileName = System.IO.Path.GetFileName(sfd.FileName);
                    string targetDir = System.IO.Path.GetDirectoryName(sfd.FileName);

                    string codeBase = System.Reflection.Assembly.GetEntryAssembly().Location;
                    codeBase = System.IO.Path.GetDirectoryName(codeBase);
                    var files = System.IO.Directory.GetFiles(codeBase, "*.dll");
                    foreach (var f in files)
                    {
                        string fileName = System.IO.Path.GetFileName(f);
                        if (fileName.ToLower().Contains("mctools")) continue;
                        System.IO.File.Copy(f, System.IO.Path.Combine(targetDir, fileName), true);
                    }


                    string exeConfigFile = System.IO.Path.Combine(targetDir, compiledFileName + ".config");
                    if (!System.IO.File.Exists(exeConfigFile))
                    {
                        System.IO.File.WriteAllText(exeConfigFile, Common.BaseConfig);
                    }
                }

                //ADD CODE TO THE MAIN METHOD TO CREATE CrmSvc...
                //TO ASSIGN IT TO THE SERVICE


            }

        }

        private void tbLoadItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(ofd.FileName);
                tbCode.Text = sr.ReadToEnd();
                fileName = ofd.FileName;
                sr.Close();
            }
        }

        private void tbSaveItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*";
            sfd.Title = "Save File";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                fileName = sfd.FileName;
                System.IO.File.WriteAllText(sfd.FileName, tbCode.Text);
            }


        }

        public void HideShowControls()
        {
            if (cbPlugin.Checked)
            {
                splitContainerCode.Panel2Collapsed = true;
                splitContainerCode.Panel2.Hide();
            }
            else
            {
                splitContainerCode.Panel2Collapsed = false;
                splitContainerCode.Panel2.Show();
            }
        }

        private void cbPlugin_Click(object sender, EventArgs e)
        {
            HideShowControls();
            ShowSampleCode();


        }

        private void CodeNowPluginControl_Load(object sender, EventArgs e)
        {
            
        }

        public string HelpUrl
        {
            get
            {
                return "http://www.itaintboring.com/tcs-tools/code-now-plugin-for-xrmtoolbox/#suggestions";
            }
        }
    }
}
