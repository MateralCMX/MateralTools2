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
            this.exportProjectFileControl1 = new Materal.UI.WinForm.UIControl.ExportProjectFileControl();
            this.notesXMLFileMergeControl1 = new Materal.UI.WinForm.UIControl.NotesXMLFileMergeControl();
            this.SuspendLayout();
            // 
            // exportProjectFileControl1
            // 
            this.exportProjectFileControl1.Location = new System.Drawing.Point(12, 12);
            this.exportProjectFileControl1.Name = "exportProjectFileControl1";
            this.exportProjectFileControl1.Size = new System.Drawing.Size(595, 117);
            this.exportProjectFileControl1.TabIndex = 0;
            // 
            // notesXMLFileMergeControl1
            // 
            this.notesXMLFileMergeControl1.Location = new System.Drawing.Point(12, 135);
            this.notesXMLFileMergeControl1.Name = "notesXMLFileMergeControl1";
            this.notesXMLFileMergeControl1.Size = new System.Drawing.Size(489, 348);
            this.notesXMLFileMergeControl1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 544);
            this.Controls.Add(this.notesXMLFileMergeControl1);
            this.Controls.Add(this.exportProjectFileControl1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private UIControl.ExportProjectFileControl exportProjectFileControl1;
        private UIControl.NotesXMLFileMergeControl notesXMLFileMergeControl1;
    }
}

