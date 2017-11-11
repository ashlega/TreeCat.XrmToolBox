namespace CodeNow
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.codeNowPluginControl1 = new TreeCat.XrmToolBox.CodeNow.CodeNowPluginControl();
            this.SuspendLayout();
            // 
            // codeNowPluginControl1
            // 
            this.codeNowPluginControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeNowPluginControl1.ConnectionDetail = null;
            this.codeNowPluginControl1.Location = new System.Drawing.Point(1, 2);
            this.codeNowPluginControl1.Name = "codeNowPluginControl1";
            this.codeNowPluginControl1.Size = new System.Drawing.Size(900, 570);
            this.codeNowPluginControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 573);
            this.Controls.Add(this.codeNowPluginControl1);
            this.Name = "MainForm";
            this.Text = "Code Now";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TreeCat.XrmToolBox.CodeNow.CodeNowPluginControl codeNowPluginControl1;
    }
}

