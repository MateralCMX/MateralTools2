using Materal.UI.WinForm.UIControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Materal.UI.WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加一个用户自定义控件到主界面
        /// </summary>
        /// <param name="uctr">要添加的控件对象</param>
        private void AddControl(UserControl uctr)
        {
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(uctr);
        }
        /// <summary>
        /// 项目文件导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportProjectFileControl uc = new ExportProjectFileControl();
            AddControl(uc);
        }
        /// <summary>
        /// Nuget包导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NugetPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportNugetPackageFileControl uc = new ExportNugetPackageFileControl();
            AddControl(uc);
        }
        /// <summary>
        /// XML文件合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XMLMergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesXMLFileMergeControl uc = new NotesXMLFileMergeControl();
            AddControl(uc);
        }
    }
}
