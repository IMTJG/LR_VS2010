using System;
using System.Collections.Generic;
using System.Text;

namespace LRHelper
{
    /// <summary>
    /// 显示的节点
    /// </summary>
    public class ViewNode
    {
        public string StatusStack { get; set; }
        public string SumupStack { get; set; }
        public string InputWords { get; set; }
        public string Production { get; set; }

        /// <summary>
        /// 显示节点的构造方法
        /// </summary>
        /// <param name="StatusStack">状态栈</param>
        /// <param name="SumupStack">文法符号栈</param>
        /// <param name="lstWords">单词列表</param>
        /// <param name="nWordIndex">从第几个单词开始显示</param>
        /// <param name="production">归约用的产生式</param>
        public ViewNode(Stack<Int32> StatusStack, Stack<CharactorNode> SumupStack, List<Helper.Word> lstWords, int nWordIndex, Production production)
        {
            // 构造状态栈字符串
            int[] nStatus = StatusStack.ToArray();
            this.StatusStack = "";
            for (int i = nStatus.Length - 1; i >= 0; i-- )
                this.StatusStack += string.Format("{0} ", nStatus[i]);

            // 构造文法符号栈字符串
            CharactorNode[] cns = SumupStack.ToArray();
            this.SumupStack = "";
            for (int i = cns.Length - 1; i >= 0; i-- )
                this.SumupStack += string.Format("{0} ", cns[i].Charactor);

            // 构造输入的单词字符串
            this.InputWords = "";
            for (int i = nWordIndex; i < lstWords.Count; i++)
                this.InputWords += string.Format("{0} ", lstWords[i].Value);

            // 构造产生式字符串
            this.Production = "";
            if (production != null)
                this.Production = string.Format("{0}→{1}", production.Left, production.Right);
        }

        public string[] GetListViewBindData(int nIndex)
        {
            string[] strData = new string[5];
            strData[0] = nIndex.ToString();
            strData[1] = StatusStack;
            strData[2] = SumupStack;
            strData[3] = InputWords;
            strData[4] = Production;
            return strData;
        }
    }
}
