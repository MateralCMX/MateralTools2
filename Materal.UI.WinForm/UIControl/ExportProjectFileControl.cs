using MateralTools.MVerify;
using System;
using System.IO;
using System.Windows.Forms;

namespace Materal.UI.WinForm.UIControl
{
    public partial class ExportProjectFileControl : UserControl
    {
        public ExportProjectFileControl()
        {
            InitializeComponent();
        }
        #region 属性
        /// <summary>
        /// 目标文件夹路径
        /// </summary>
        private string _targetPath
        {
            get
            {
                return textTarget.Text.Trim();
            }
            set
            {
                textTarget.Text = value;
            }
        }
        /// <summary>
        /// 代码文件夹路径
        /// </summary>
        private string _codePath
        {
            get
            {
                return textCode.Text.Trim();
            }
            set
            {
                textCode.Text = value;
            }
        }
        private ExportProjectFileManager _epfMa;
        #endregion
        #region 私有方法
        /// <summary>
        /// 验证
        /// 通过验证则会启用导出按钮
        /// 不通过验证会禁用导出按钮
        /// </summary>
        private void Verification()
        {
            btnExport.Enabled = !_targetPath.MIsNullOrEmpty() && !_codePath.MIsNullOrEmpty() && Directory.Exists(_codePath);
        }
        /// <summary>
        /// 重置信息
        /// </summary>
        private void ResetInfo()
        {
            /*重置导出对象*/
            _epfMa = new ExportProjectFileManager
            {
                ProjectName = "MateralTools",
                ExcludeDirectoryName = new string[]
                {
                    "bin",
                    "obj",
                }
            };
            _epfMa.ExportSuccess += ExportSuccess;
            _epfMa.ExportFail += ExportFail;
            /*重置目标文件夹*/
            _targetPath = @"E:\Project\MateralTools\Application\Code";
            /*重置代码文件夹*/
            _codePath = @"E:\Project\MateralTools\Project\MateralTools";
            /*其他*/
            checkOpenExplorer.Checked = true;
            Verification();
        }
        /// <summary>
        /// 浏览文件夹路径
        /// </summary>
        /// <param name="targetControl">目标属性</param>
        private void BrowsePath(TextBox targetControl)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            if (targetControl.Text.MIsNullOrEmpty())
            {
                fbd.SelectedPath = Environment.CurrentDirectory;
            }
            else
            {
                fbd.SelectedPath = targetControl.Text;
            }
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                targetControl.Text = fbd.SelectedPath;
            }
        }
        /// <summary>
        /// 导出成功
        /// </summary>
        private void ExportSuccess()
        {
            MessageBox.Show("导出成功！");
        }
        /// <summary>
        /// 导出失败
        /// </summary>
        private void ExportFail()
        {
            MessageBox.Show("导出失败！");
        }
        #endregion
        #region 事件
        /// <summary>
        /// 控件加载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportProjectFileControl_Load(object sender, EventArgs e)
        {
            ResetInfo();
        }
        /// <summary>
        /// 目标文件夹浏览按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseTarget_Click(object sender, EventArgs e)
        {
            BrowsePath(textTarget);
        }
        /// <summary>
        /// 代码文件夹浏览按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseCode_Click(object sender, EventArgs e)
        {
            BrowsePath(textCode);
        }
        /// <summary>
        /// 导出按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExport_Click(object sender, EventArgs e)
        {
            _epfMa.IsOpenExplorer = checkOpenExplorer.Checked;
            _epfMa.FilePath = _codePath;
            _epfMa.ExportFile(_targetPath);
        }
        /// <summary>
        /// 重置按钮单机事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetInfo();
        }
        /// <summary>
        /// 文件夹路径更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextTargetOrCode_TextChanged(object sender, EventArgs e)
        {
            Verification();
        }
        #endregion
    }
}
