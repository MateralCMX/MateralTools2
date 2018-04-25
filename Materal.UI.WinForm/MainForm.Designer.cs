namespace Materal.UI.WinForm
{
    partial class MainForm
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
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.ExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NugetPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XMLMergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.TopMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopMenuStrip
            // 
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportToolStripMenuItem,
            this.ToolsToolStripMenuItem});
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.Size = new System.Drawing.Size(800, 25);
            this.TopMenuStrip.TabIndex = 0;
            this.TopMenuStrip.Text = "menuStrip1";
            // 
            // ExportToolStripMenuItem
            // 
            this.ExportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectFileToolStripMenuItem,
            this.NugetPackageToolStripMenuItem});
            this.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem";
            this.ExportToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.ExportToolStripMenuItem.Text = "导出";
            // 
            // ProjectFileToolStripMenuItem
            // 
            this.ProjectFileToolStripMenuItem.Name = "ProjectFileToolStripMenuItem";
            this.ProjectFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ProjectFileToolStripMenuItem.Text = "项目文件";
            this.ProjectFileToolStripMenuItem.Click += new System.EventHandler(this.ProjectFileToolStripMenuItem_Click);
            // 
            // NugetPackageToolStripMenuItem
            // 
            this.NugetPackageToolStripMenuItem.Name = "NugetPackageToolStripMenuItem";
            this.NugetPackageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.NugetPackageToolStripMenuItem.Text = "Nuget包";
            this.NugetPackageToolStripMenuItem.Click += new System.EventHandler(this.NugetPackageToolStripMenuItem_Click);
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.XMLMergeToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.ToolsToolStripMenuItem.Text = "工具";
            // 
            // XMLMergeToolStripMenuItem
            // 
            this.XMLMergeToolStripMenuItem.Name = "XMLMergeToolStripMenuItem";
            this.XMLMergeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.XMLMergeToolStripMenuItem.Text = "XML合并";
            this.XMLMergeToolStripMenuItem.Click += new System.EventHandler(this.XMLMergeToolStripMenuItem_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 25);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(800, 519);
            this.MainPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 544);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.TopMenuStrip);
            this.MainMenuStrip = this.TopMenuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Materal工具库-WinForm";
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip TopMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProjectFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NugetPackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem XMLMergeToolStripMenuItem;
        private System.Windows.Forms.Panel MainPanel;
    }
}

