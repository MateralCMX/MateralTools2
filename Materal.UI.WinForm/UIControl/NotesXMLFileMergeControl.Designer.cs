namespace Materal.UI.WinForm.UIControl
{
    partial class NotesXMLFileMergeControl
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
            this.TextMainXML = new System.Windows.Forms.TextBox();
            this.BtnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ListSecondaryXML = new System.Windows.Forms.ListView();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnMerge = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "主文件：";
            // 
            // TextMainXML
            // 
            this.TextMainXML.Location = new System.Drawing.Point(70, 26);
            this.TextMainXML.Name = "TextMainXML";
            this.TextMainXML.Size = new System.Drawing.Size(314, 21);
            this.TextMainXML.TabIndex = 1;
            // 
            // BtnBrowse
            // 
            this.BtnBrowse.Location = new System.Drawing.Point(390, 24);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new System.Drawing.Size(75, 23);
            this.BtnBrowse.TabIndex = 2;
            this.BtnBrowse.Text = "浏览...";
            this.BtnBrowse.UseVisualStyleBackColor = true;
            this.BtnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "附文件：";
            // 
            // ListSecondaryXML
            // 
            this.ListSecondaryXML.Location = new System.Drawing.Point(70, 53);
            this.ListSecondaryXML.Name = "ListSecondaryXML";
            this.ListSecondaryXML.Size = new System.Drawing.Size(314, 271);
            this.ListSecondaryXML.TabIndex = 4;
            this.ListSecondaryXML.UseCompatibleStateImageBehavior = false;
            this.ListSecondaryXML.View = System.Windows.Forms.View.List;
            this.ListSecondaryXML.SelectedIndexChanged += new System.EventHandler(this.ListSecondaryXML_SelectedIndexChanged);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(390, 55);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 5;
            this.BtnAdd.Text = "添加";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(390, 84);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 6;
            this.BtnDelete.Text = "删除";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnMerge
            // 
            this.BtnMerge.Location = new System.Drawing.Point(390, 301);
            this.BtnMerge.Name = "BtnMerge";
            this.BtnMerge.Size = new System.Drawing.Size(75, 23);
            this.BtnMerge.TabIndex = 7;
            this.BtnMerge.Text = "合并";
            this.BtnMerge.UseVisualStyleBackColor = true;
            this.BtnMerge.Click += new System.EventHandler(this.BtnMerge_Click);
            // 
            // NotesXMLFileMergeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnMerge);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.ListSecondaryXML);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnBrowse);
            this.Controls.Add(this.TextMainXML);
            this.Controls.Add(this.label1);
            this.Name = "NotesXMLFileMergeControl";
            this.Size = new System.Drawing.Size(489, 348);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextMainXML;
        private System.Windows.Forms.Button BtnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView ListSecondaryXML;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnMerge;
    }
}
