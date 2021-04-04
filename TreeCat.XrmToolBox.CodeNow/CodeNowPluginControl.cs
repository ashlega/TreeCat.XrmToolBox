using System;
using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;

public delegate void LogMessageDelegate(object msg);
public delegate void ProgressIndicatorDelegate();

namespace TreeCat.XrmToolBox.CodeNow
{
    public enum SAMPLE_CODE_ID
    {
        NONE = 0,
        SOLUTION_STATS = 1
    }

    public partial class CodeNowPluginControl: PluginControlBase, IHelpPlugin
    {
        private List<Storage.ICodeNowStorage> storageList = new List<Storage.ICodeNowStorage>();

        private CodeNowScript Script { get; set; }
        
        #region Base tool implementation

        
        

        private System.ComponentModel.IContainer components;
        private FastColoredTextBoxNS.FastColoredTextBox tbCode;
        private SplitContainer splitContainer4;
        private Panel panelCode;
        private SplitContainer splitContainerCode;
        
        private Label label5;
        private Label label6;
        private TextBox tbUsing;
        private TextBox tbLog;
        private ToolStrip toolStrip2;
        private ToolStripButton tbRunCode;
        private ToolStripButton tbCompileCode;
        private Timer progressTimer;
        private ProgressBar executionProgressBar;
        private TextBox tbDescription;
        private Label Description;
        private TextBox tbCategory;
        private Label label3;
        private TextBox tbTitle;
        private Label label2;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem tbLoadItem;
        private ToolStripMenuItem tbSaveItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tbNewItem;
        private ToolStripMenuItem tbNewScript;
        private ToolStripMenuItem tbSaveAs;
        private TextBox tbLocation;
        private Label label1;
        private Panel panelStart;
        private Label label4;
        private PictureBox pictureBox1;
        Delegate delegateInstance = null;





        public CodeNowPluginControl()
        {
            
            InitializeComponent();
            delegateInstance = Delegate.CreateDelegate(typeof(LogMessageDelegate), this, "LogMessage");
            EnableControls();
            //HideShowControls();
            //ShowSampleCode();

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
            this.panelCode = new System.Windows.Forms.Panel();
            this.panelStart = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.executionProgressBar = new System.Windows.Forms.ProgressBar();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbLoadItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbSaveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbNewScript = new System.Windows.Forms.ToolStripMenuItem();
            this.tbCompileCode = new System.Windows.Forms.ToolStripButton();
            this.tbRunCode = new System.Windows.Forms.ToolStripButton();
            this.splitContainerCode = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.Description = new System.Windows.Forms.Label();
            this.tbCategory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbUsing = new System.Windows.Forms.TextBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.tbCode = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panelCode.SuspendLayout();
            this.panelStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.splitContainer4.Panel1.Controls.Add(this.panelCode);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tbLog);
            this.splitContainer4.Size = new System.Drawing.Size(877, 561);
            this.splitContainer4.SplitterDistance = 400;
            this.splitContainer4.TabIndex = 0;
            // 
            // panelCode
            // 
            this.panelCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCode.Controls.Add(this.panelStart);
            this.panelCode.Controls.Add(this.executionProgressBar);
            this.panelCode.Controls.Add(this.toolStrip2);
            this.panelCode.Controls.Add(this.splitContainerCode);
            this.panelCode.Location = new System.Drawing.Point(3, 3);
            this.panelCode.Name = "panelCode";
            this.panelCode.Size = new System.Drawing.Size(871, 394);
            this.panelCode.TabIndex = 0;
            // 
            // panelStart
            // 
            this.panelStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStart.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelStart.Controls.Add(this.pictureBox1);
            this.panelStart.Controls.Add(this.label4);
            this.panelStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelStart.Location = new System.Drawing.Point(82, 49);
            this.panelStart.Name = "panelStart";
            this.panelStart.Size = new System.Drawing.Size(682, 229);
            this.panelStart.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::TreeCat.XrmToolBox.CodeNow.Properties.Resources.Logo64;
            this.pictureBox1.Location = new System.Drawing.Point(144, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 56);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(212, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(267, 80);
            this.label4.TabIndex = 0;
            this.label4.Text = "CodeNow Plugin for XrmToolBox\r\n\r\nTo load a script, use \r\nFile->Open";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // executionProgressBar
            // 
            this.executionProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.executionProgressBar.Location = new System.Drawing.Point(419, 370);
            this.executionProgressBar.Name = "executionProgressBar";
            this.executionProgressBar.Size = new System.Drawing.Size(449, 18);
            this.executionProgressBar.TabIndex = 5;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton2,
            this.tbCompileCode,
            this.tbRunCode});
            this.toolStrip2.Location = new System.Drawing.Point(0, 363);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(216, 27);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbLoadItem,
            this.tbSaveItem,
            this.tbSaveAs,
            this.toolStripSeparator1,
            this.tbNewItem,
            this.tbNewScript});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(38, 24);
            this.toolStripDropDownButton2.Text = "File";
            // 
            // tbLoadItem
            // 
            this.tbLoadItem.Name = "tbLoadItem";
            this.tbLoadItem.Size = new System.Drawing.Size(135, 22);
            this.tbLoadItem.Text = "Open";
            this.tbLoadItem.Click += new System.EventHandler(this.tbLoadItem_Click);
            // 
            // tbSaveItem
            // 
            this.tbSaveItem.Name = "tbSaveItem";
            this.tbSaveItem.Size = new System.Drawing.Size(135, 22);
            this.tbSaveItem.Text = "Save";
            this.tbSaveItem.Click += new System.EventHandler(this.tbSaveItem_Click);
            // 
            // tbSaveAs
            // 
            this.tbSaveAs.Name = "tbSaveAs";
            this.tbSaveAs.Size = new System.Drawing.Size(135, 22);
            this.tbSaveAs.Text = "Save As";
            this.tbSaveAs.Click += new System.EventHandler(this.tbSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
            // 
            // tbNewItem
            // 
            this.tbNewItem.Name = "tbNewItem";
            this.tbNewItem.Size = new System.Drawing.Size(135, 22);
            this.tbNewItem.Text = "New Plugin";
            this.tbNewItem.Click += new System.EventHandler(this.tbNewItem_Click);
            // 
            // tbNewScript
            // 
            this.tbNewScript.Name = "tbNewScript";
            this.tbNewScript.Size = new System.Drawing.Size(135, 22);
            this.tbNewScript.Text = "New Script";
            this.tbNewScript.Click += new System.EventHandler(this.tbNewScript_Click);
            // 
            // tbCompileCode
            // 
            this.tbCompileCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbCompileCode.Image = ((System.Drawing.Image)(resources.GetObject("tbCompileCode.Image")));
            this.tbCompileCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCompileCode.Name = "tbCompileCode";
            this.tbCompileCode.Size = new System.Drawing.Size(56, 24);
            this.tbCompileCode.Text = "Compile";
            this.tbCompileCode.Click += new System.EventHandler(this.tbMakeExeItem_Click);
            // 
            // tbRunCode
            // 
            this.tbRunCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbRunCode.Image = ((System.Drawing.Image)(resources.GetObject("tbRunCode.Image")));
            this.tbRunCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRunCode.Name = "tbRunCode";
            this.tbRunCode.Size = new System.Drawing.Size(32, 24);
            this.tbRunCode.Text = "Run";
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
            this.splitContainerCode.Panel2.Controls.Add(this.tbLocation);
            this.splitContainerCode.Panel2.Controls.Add(this.label1);
            this.splitContainerCode.Panel2.Controls.Add(this.tbDescription);
            this.splitContainerCode.Panel2.Controls.Add(this.Description);
            this.splitContainerCode.Panel2.Controls.Add(this.tbCategory);
            this.splitContainerCode.Panel2.Controls.Add(this.label3);
            this.splitContainerCode.Panel2.Controls.Add(this.tbTitle);
            this.splitContainerCode.Panel2.Controls.Add(this.label2);
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
            // tbLocation
            // 
            this.tbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocation.Location = new System.Drawing.Point(4, 339);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.ReadOnly = true;
            this.tbLocation.Size = new System.Drawing.Size(310, 20);
            this.tbLocation.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Location";
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.Location = new System.Drawing.Point(4, 251);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDescription.Size = new System.Drawing.Size(310, 69);
            this.tbDescription.TabIndex = 7;
            // 
            // Description
            // 
            this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Description.AutoSize = true;
            this.Description.BackColor = System.Drawing.SystemColors.Control;
            this.Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Description.Location = new System.Drawing.Point(3, 235);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(71, 13);
            this.Description.TabIndex = 6;
            this.Description.Text = "Description";
            // 
            // tbCategory
            // 
            this.tbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCategory.Location = new System.Drawing.Point(4, 212);
            this.tbCategory.Name = "tbCategory";
            this.tbCategory.Size = new System.Drawing.Size(310, 20);
            this.tbCategory.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Category";
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(4, 173);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(310, 20);
            this.tbTitle.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Title";
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
            this.tbUsing.Size = new System.Drawing.Size(310, 128);
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
            // progressTimer
            // 
            this.progressTimer.Interval = 500;
            this.progressTimer.Tick += new System.EventHandler(this.progressTimer_Tick);
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
            this.tbCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.tbCode.BackBrush = null;
            this.tbCode.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.tbCode.CharHeight = 14;
            this.tbCode.CharWidth = 8;
            this.tbCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
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
            this.panelCode.ResumeLayout(false);
            this.panelStart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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


        private void LogMessage(object msg)
        {
            tbLog.AppendText(msg.ObjectToStringDebug() + Environment.NewLine);
        }


        private void StartProgress()
        {
            if (this.executionProgressBar.InvokeRequired)
            {
                ProgressIndicatorDelegate d = new ProgressIndicatorDelegate(StartProgress);
                this.Invoke(d);
            }
            else
            {
                executionProgressBar.Value = 1;
                tbCode.Enabled = false;
                progressTimer.Start();
            }
        }

        private void StopProgress()
        {
            if (this.executionProgressBar.InvokeRequired)
            {
                ProgressIndicatorDelegate d = new ProgressIndicatorDelegate(StopProgress);
                this.Invoke(d);
            }
            else
            {
                executionProgressBar.Value = 0;
                progressTimer.Stop();
                tbCode.Enabled = true;
            }
            
        }


        private void buttonRun_Click(object sender, EventArgs e)
        {
            var cmn = new Common();
            string result = cmn.GenerateCode(Service, delegateInstance, COMPILE_ACTION.RUN_NOW, null, tbCode.Text, tbUsing.Text, StartProgress, StopProgress);
            if (result != null) LogMessage(result);
        }

       

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void EnableControls()
        {
            splitContainerCode.Visible = CurrentScript != null;
            panelStart.Visible = CurrentScript == null;

            tbCompileCode.Enabled = CurrentScript != null;
            tbRunCode.Enabled = CurrentScript != null && CurrentScript.ScriptType == CODE_NOW_SCRIPT_TYPE.CODE;
            tbSaveItem.Enabled = CurrentScript != null;
            tbSaveAs.Enabled = CurrentScript != null;
            
        }
        

        private void tbMakeExeItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (CurrentScript.ScriptType == CODE_NOW_SCRIPT_TYPE.PLUGIN)
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
                string result = cmn.GenerateCode(Service, delegateInstance, CurrentScript.ScriptType == CODE_NOW_SCRIPT_TYPE.PLUGIN ? COMPILE_ACTION.COMPILE_DLL : COMPILE_ACTION.MAKE_EXE, sfd.FileName, tbCode.Text, tbUsing.Text, StartProgress, StopProgress);
                LogMessage(result);

                if (CurrentScript.ScriptType == CODE_NOW_SCRIPT_TYPE.CODE)
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

        private void LoadStorageList()
        {
            storageList.Clear();
            storageList.Add(new Storage.FileSystem());
            storageList.Add(new Storage.Embedded());
            storageList.Add(new Storage.Online());
        }

        private void tbLoadItem_Click(object sender, EventArgs e)
        {
            StorageSelector ss = new StorageSelector(storageList, false);
            if(ss.ShowDialog() == DialogResult.OK)
            {
                if (ss.SelectedStorage.CustomCodeSelector)
                {
                    var script =  ss.SelectedStorage.Open();
                    if (script != null)
                    {
                        CurrentScript = script;
                        CurrentScript.SourceStorage = ss.SelectedStorage;
                    }
                }
                else
                {
                    StorageForm sf = new StorageForm(ss.SelectedStorage);
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        if (sf.SelectedScript != null)
                        {
                            CurrentScript = sf.SelectedScript;
                            CurrentScript.SourceStorage = ss.SelectedStorage;
                        }
                    }
                }
            }
            /*
            StorageForm sf = new StorageForm(false);
            if (sf.ShowDialog() == DialogResult.OK)
            {
                if (sf.SelectedScript != null)
                {
                    CurrentScript = sf.SelectedScript;
                }
            }
            */

            /*
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
            */
        }

        private void SaveScript(bool askForLocation)
        {
            
            var saveStorageCount = storageList.Count(x => x.IsSaveSupported);
            Storage.ICodeNowStorage saveStorage = null;
            if (saveStorageCount == 1)
            {
                saveStorage = storageList.Find(x => x.IsSaveSupported);
            }
            else
            {
                StorageSelector ss = new StorageSelector(storageList, true);
                if (ss.ShowDialog() == DialogResult.OK && ss.SelectedStorage != null)
                {
                    saveStorage = ss.SelectedStorage;
                }
            }

            if (saveStorage != null)
            {
                if (saveStorage.Save(CurrentScript, askForLocation))
                {
                    CurrentScript.SourceStorage = saveStorage;
                    tbLocation.Text = CurrentScript.Location;
                }
            }
        }
        
        private void UpdateCurrentScript()
        {
            if(CurrentScript != null)
            {
                CurrentScript.Code = tbCode.Text;
                CurrentScript.Title = tbTitle.Text;
                CurrentScript.Using = tbUsing.Text;
                CurrentScript.Category = tbCategory.Text;
                CurrentScript.Description = tbDescription.Text;

            }
        }

        private void tbSaveAs_Click(object sender, EventArgs e)
        {
            UpdateCurrentScript();
            SaveScript(true);
            /*
            StorageSelector ss = new StorageSelector(storageList, true);
            if (ss.ShowDialog() == DialogResult.OK && ss.SelectedStorage != null)
            {
                ss.SelectedStorage.Save(CurrentScript, true);
            }
            */
        }

        private void tbSaveItem_Click(object sender, EventArgs e)
        {
            UpdateCurrentScript();
            if (CurrentScript.SourceStorage != null && CurrentScript.SourceStorage.IsSaveSupported)
            {
                CurrentScript.SourceStorage.Save(CurrentScript, false);
            }
            else
            {
                SaveScript(false);
            }
            /*
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*";
            sfd.Title = "Save File";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                fileName = sfd.FileName;
                System.IO.File.WriteAllText(sfd.FileName, tbCode.Text);
            }
            */

        }
        /*
        public void HideShowControls()
        {
            if (cbProjectStyle.SelectedIndex == 1)
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
        */

        private void cbPlugin_Click(object sender, EventArgs e)
        {
            


        }

       

        public string HelpUrl
        {
            get
            {
                return "http://www.itaintboring.com/tcs-tools/code-now-plugin-for-xrmtoolbox/#suggestions";
            }
        }

        private void cbProjectStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //HideShowControls();
            //ShowSampleCode();
        }

        private void CodeNowPluginControl_Load(object sender, EventArgs e)
        {
            LoadStorageList();
            //cbProjectStyle.SelectedIndex = 0;
        }

        private void tbSolutionStats_Click(object sender, EventArgs e)
        {
            //ShowSampleCode(SAMPLE_CODE_ID.SOLUTION_STATS);
        }

        private void progressTimer_Tick(object sender, EventArgs e)
        {

            if (executionProgressBar.Value + 5 >= executionProgressBar.Maximum) executionProgressBar.Value = 0;
            else executionProgressBar.Value += 5;
        }

        private CodeNowScript currentScript = null;      
        public CodeNowScript CurrentScript
        {
            get
            {
                return currentScript;
            }
            set
            {
                if (value != null)
                {
                    currentScript = value;
                    tbCode.Text = value.Code;
                    tbUsing.Text = value.Using;
                    tbCategory.Text = value.Category;
                    tbDescription.Text = value.Description;
                    tbTitle.Text = value.Title;
                    tbLocation.Text = value.Location;
                }
                EnableControls();
            }
        }
        private void tbStorage_Click(object sender, EventArgs e)
        {
            
        }

        private void tbNewItem_Click(object sender, EventArgs e)
        {
            var script = new CodeNowScript(Common.PluginSourceSample);
            script.Title = "New Plugin";
            script.Description = "";
            CurrentScript = script;
        }

        private void tbNewScript_Click(object sender, EventArgs e)
        {
            var script = new CodeNowScript(Common.CodeNowSample);
            script.Title = "New Script";
            script.Description = "";
            CurrentScript = script;
        }

        
    }
}
