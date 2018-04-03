using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Materal.UI.WinForm.UIControl
{
    public partial class NotesXMLFileMergeControl : UserControl
    {
        public NotesXMLFileMergeControl()
        {
            InitializeComponent();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml文件|*.xml";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                TextMainXML.Text = ofd.FileName;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml文件|*.xml";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string name in ofd.FileNames)
                {
                    ListSecondaryXML.Items.Add(new ListViewItem(name));
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ListSecondaryXML.Items.RemoveAt(ListSecondaryXML.SelectedIndices[0]);
            BtnDelete.Enabled = false;
        }

        private void BtnMerge_Click(object sender, EventArgs e)
        {
            string mainXMLPath = TextMainXML.Text;
            List<string> secondaryXMLPaths = new List<string>();
            foreach (ListViewItem item in ListSecondaryXML.Items)
            {
                secondaryXMLPaths.Add(item.Text);
            }
            NotesXMLFileMergeManager.Merge(mainXMLPath, secondaryXMLPaths.ToArray());
            MessageBox.Show("合并完成");
        }

        private void ListSecondaryXML_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDelete.Enabled = true;
        }
    }
}
