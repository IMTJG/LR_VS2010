using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LRHelper;
using Helper;
using TranslationHelper;

namespace LR_VS2010
{
    public partial class LR : Form
    {

        // 单词链表
        public List<Word> Words = null;
        public LR()
        {
            InitializeComponent();
        }

        private void LRForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowseWordFile_Click(object sender, EventArgs e) 
        {
            
            OpenFileDialog ofd = new OpenFileDialog();//打开文件对话框
            ofd.Filter = "C#文件(*.cs)|*.cs";     //选择.cs文件
            // 显示对话框选择文件
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            tbWordFile.Text = ofd.FileName; // 将文件路径显示到TextBox

            /*string strFile = SelectAFile("单词序列(*.words)|*.words");

            if (strFile != "")
                tbWordFile.Text = strFile;*/
        }

        private string SelectAFile(string strFilter)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = strFilter;
            if (ofd.ShowDialog() == DialogResult.OK)
                return ofd.FileName;
            return "";
        }

        private void btnLRAnalysis_Click(object sender, EventArgs e)
        {

            //词法分析
            if (tbWordFile.Text == "")
            {
                MessageBox.Show("请先选择要分析的C#文件");
                return;
            }

            // 读取文件内容并预处理，同时显示到tbContent
            string strBuffer = FilePretreatment.ReadAndPretreat(tbWordFile.Text);
            richTextBox1.Text = strBuffer;
            //MessageBox.Show(strBuffer);

            // 识别单词
            Words = Analysis.AnalysisWords(strBuffer);

            // 显示到列表
            listView2.Items.Clear();
            foreach (Word word in Words)
                listView2.Items.Add(new ListViewItem(word.GetListRowStrings()));

            //SaveFileDialog sfd = new SaveFileDialog();
            //// 设置文件扩展名为.words
            //sfd.Filter = "单词(.words)|*.words";
            //// 如果用户没有写扩展名，则自动追加扩展名
            //sfd.AddExtension = true;
            //// 显示对话框
            //if (sfd.ShowDialog() != DialogResult.OK)
            //    return;
            string FileName = tbWordFile.Text+".words";
            WordsIOHelper.Save(FileName, Words);

            //// 将文件路径显示到TextBox
            //tbWordFile.Text = ofd.FileName;

            //string strFile = SelectAFile("单词序列(*.words)|*.words");
            string strFile = FileName;
            // string strFile = FilePretreatment.ReadAndPretreat(sfd.FileName);
            if (strFile != "")
                tbWordFile.Text = strFile;


            // 读取LR分析表
            LRTable lrTable = new LRTable();
            lrTable.Read("LR1.LR");

            // 读取文法产生式
            Grammar grammar = new Grammar();
            grammar.Read("LR1.grammar");

            // 读取单词序列，每句追加一个#作为单词序列的结束
            List<Helper.Word> words = Helper.WordsIOHelper.Read(tbWordFile.Text);
           
            // 创建在列表中显示的节点集合
            List<ViewNode> lstViewNodes = new List<ViewNode>();
            // 四元式集合
            MidCodes mcCodes = new MidCodes();

            // 语法分析
            bool bResult = true;
            int nStartWordIndex = 0;
            while (bResult && nStartWordIndex < words.Count)
            {
                bResult = LRAnalysis.Analysis(words, ref nStartWordIndex, grammar, lrTable, lstViewNodes, mcCodes);
                nStartWordIndex++;
            }

            // 将结果显示到列表
            lvAnalysis.Items.Clear();
            int i = 0;
            for (; i < lstViewNodes.Count; i++)
                lvAnalysis.Items.Add(new ListViewItem(lstViewNodes[i].GetListViewBindData(i + 1)));

            ListViewItem item = new ListViewItem(string.Format("{0}", i + 1));
            item.SubItems[0].Text = bResult ? "成功" : "失败";
            lvAnalysis.Items.Add(item);

            // 显示四元式
            lvMidCodes.Items.Clear();
            foreach (MidCode mc in mcCodes.lstMidCodes)
                lvMidCodes.Items.Add(new ListViewItem(mc.MidCodeString));

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
