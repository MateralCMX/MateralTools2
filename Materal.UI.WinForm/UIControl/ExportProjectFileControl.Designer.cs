namespace Materal.UI.WinForm.UIControl
{
    partial class ExportProjectFileControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textTarget = new System.Windows.Forms.TextBox();
            this.btnBrowseTarget = new System.Windows.Forms.Button();
            this.btnBrowseCode = new System.Windows.Forms.Button();
            this.textCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.checkOpenExplorer = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "目标文件夹：";
            // 
            // textTarget
            // 
            this.textTarget.Location = new System.Drawing.Point(93, 27);
            this.textTarget.Name = "textTarget";
            this.textTarget.Size = new System.Drawing.Size(408, 21);
            this.textTarget.TabIndex = 1;
            this.textTarget.TextChanged += new System.EventHandler(this.TextTargetOrCode_TextChanged);
            // 
            // btnBrowseTarget
            // 
            this.btnBrowseTarget.Location = new System.Drawing.Point(507, 25);
            this.btnBrowseTarget.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnBrowseTarget.Name = "btnBrowseTarget";
            this.btnBrowseTarget.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTarget.TabIndex = 2;
            this.btnBrowseTarget.Text = "浏览...";
            this.btnBrowseTarget.UseVisualStyleBackColor = true;
            this.btnBrowseTarget.Click += new System.EventHandler(this.BtnBrowseTarget_Click);
            // 
            // btnBrowseCode
            // 
            this.btnBrowseCode.Location = new System.Drawing.Point(507, 54);
            this.btnBrowseCode.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnBrowseCode.Name = "btnBrowseCode";
            this.btnBrowseCode.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCode.TabIndex = 5;
            this.btnBrowseCode.Text = "浏览...";
            this.btnBrowseCode.UseVisualStyleBackColor = true;
            this.btnBrowseCode.Click += new System.EventHandler(this.BtnBrowseCode_Click);
            // 
            // textCode
            // 
            this.textCode.Location = new System.Drawing.Point(93, 56);
            this.textCode.Name = "textCode";
            this.textCode.Size = new System.Drawing.Size(408, 21);
            this.textCode.TabIndex = 4;
            this.textCode.TextChanged += new System.EventHandler(this.TextTargetOrCode_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "代码文件夹：";
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(426, 83);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(345, 83);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // checkOpenExplorer
            // 
            this.checkOpenExplorer.AutoSize = true;
            this.checkOpenExplorer.Location = new System.Drawing.Point(93, 87);
            this.checkOpenExplorer.Name = "checkOpenExplorer";
            this.checkOpenExplorer.Size = new System.Drawing.Size(120, 16);
            this.checkOpenExplorer.TabIndex = 8;
            this.checkOpenExplorer.Text = "导出后打开文件夹";
            this.checkOpenExplorer.UseVisualStyleBackColor = true;
            // 
            // ExportProjectFileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkOpenExplorer);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnBrowseCode);
            this.Controls.Add(this.textCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseTarget);
            this.Controls.Add(this.textTarget);
            this.Controls.Add(this.label1);
            this.Name = "ExportProjectFileControl";
            this.Size = new System.Drawing.Size(595, 117);
            this.Load += new System.EventHandler(this.ExportProjectFileControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textTarget;
        private System.Windows.Forms.Button btnBrowseTarget;
        private System.Windows.Forms.Button btnBrowseCode;
        private System.Windows.Forms.TextBox textCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox checkOpenExplorer;
    }
}
