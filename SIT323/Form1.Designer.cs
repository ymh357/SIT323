namespace SIT323
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.CrozzlePage = new System.Windows.Forms.TabPage();
            this.crozzleWebBrowser = new System.Windows.Forms.WebBrowser();
            this.ErrorPage = new System.Windows.Forms.TabPage();
            this.errorWebBrowser = new System.Windows.Forms.WebBrowser();
            this.scoreTextBox = new System.Windows.Forms.TextBox();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.CrozzlePage.SuspendLayout();
            this.ErrorPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(559, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.validateFileToolStripMenuItem});
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.新建ToolStripMenuItem.Text = "File";
            // 
            // validateFileToolStripMenuItem
            // 
            this.validateFileToolStripMenuItem.Name = "validateFileToolStripMenuItem";
            this.validateFileToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.validateFileToolStripMenuItem.Text = "Validate File";
            this.validateFileToolStripMenuItem.Click += new System.EventHandler(this.validateFileToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.CrozzlePage);
            this.tabControl.Controls.Add(this.ErrorPage);
            this.tabControl.Location = new System.Drawing.Point(3, 71);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(555, 315);
            this.tabControl.TabIndex = 1;
            // 
            // CrozzlePage
            // 
            this.CrozzlePage.Controls.Add(this.crozzleWebBrowser);
            this.CrozzlePage.Location = new System.Drawing.Point(4, 25);
            this.CrozzlePage.Name = "CrozzlePage";
            this.CrozzlePage.Padding = new System.Windows.Forms.Padding(3);
            this.CrozzlePage.Size = new System.Drawing.Size(547, 286);
            this.CrozzlePage.TabIndex = 0;
            this.CrozzlePage.Text = "Crozzle";
            this.CrozzlePage.UseVisualStyleBackColor = true;
            // 
            // crozzleWebBrowser
            // 
            this.crozzleWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crozzleWebBrowser.Location = new System.Drawing.Point(3, 3);
            this.crozzleWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.crozzleWebBrowser.Name = "crozzleWebBrowser";
            this.crozzleWebBrowser.Size = new System.Drawing.Size(541, 280);
            this.crozzleWebBrowser.TabIndex = 0;
            // 
            // ErrorPage
            // 
            this.ErrorPage.Controls.Add(this.errorWebBrowser);
            this.ErrorPage.Location = new System.Drawing.Point(4, 25);
            this.ErrorPage.Name = "ErrorPage";
            this.ErrorPage.Padding = new System.Windows.Forms.Padding(3);
            this.ErrorPage.Size = new System.Drawing.Size(547, 322);
            this.ErrorPage.TabIndex = 1;
            this.ErrorPage.Text = "Error";
            this.ErrorPage.UseVisualStyleBackColor = true;
            // 
            // errorWebBrowser
            // 
            this.errorWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorWebBrowser.Location = new System.Drawing.Point(3, 3);
            this.errorWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.errorWebBrowser.Name = "errorWebBrowser";
            this.errorWebBrowser.Size = new System.Drawing.Size(541, 316);
            this.errorWebBrowser.TabIndex = 0;
            // 
            // scoreTextBox
            // 
            this.scoreTextBox.Location = new System.Drawing.Point(185, 36);
            this.scoreTextBox.Name = "scoreTextBox";
            this.scoreTextBox.Size = new System.Drawing.Size(140, 25);
            this.scoreTextBox.TabIndex = 2;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scoreLabel.ForeColor = System.Drawing.Color.Crimson;
            this.scoreLabel.Location = new System.Drawing.Point(5, 31);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(133, 30);
            this.scoreLabel.TabIndex = 3;
            this.scoreLabel.Text = "Score:  ";
            this.scoreLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 377);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.scoreTextBox);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "CrozzleGameForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.CrozzlePage.ResumeLayout(false);
            this.ErrorPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage ErrorPage;
        private System.Windows.Forms.ToolStripMenuItem validateFileToolStripMenuItem;
        private System.Windows.Forms.TabPage CrozzlePage;
        private System.Windows.Forms.WebBrowser crozzleWebBrowser;
        private System.Windows.Forms.WebBrowser errorWebBrowser;
        private System.Windows.Forms.TextBox scoreTextBox;
        private System.Windows.Forms.Label scoreLabel;
    }
}

