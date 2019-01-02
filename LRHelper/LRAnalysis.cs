using System;
using System.Collections.Generic;
using System.Text;
using TranslationHelper;
using Helper;

namespace LRHelper
{
    public class LRAnalysis
    {
        /// <summary>
        /// 语法分析
        /// </summary>
        /// <param name="words">单词序列</param>
        /// <param name="nStartWordIndex">指向当前单词的指针，传入时应保证指向该句第一个单词，分析成功结束时应保证指向句末的#</param>
        /// <param name="grammar">文法</param>
        /// <param name="lrTable">LR分析表</param>
        /// <param name="lstViewNodes">显示用的行节点</param>
        /// <param name="mcMidCodes">生成的四元式</param>
        /// <returns>成功返回true，否则返回false</returns>
        public static bool Analysis(List<Helper.Word> words, ref int nStartWordIndex, Grammar grammar, LRTable lrTable, List<ViewNode> lstViewNodes, MidCodes mcMidCodes)
        {
            // 初始化状态栈，压入初态0
            Stack<int> StatusStack = new Stack<int>();
            StatusStack.Push(0);

            // 初始化文法符号栈（已归约串栈），压入开始符号#
            Stack<CharactorNode> SumupStack = new Stack<CharactorNode>();
            SumupStack.Push(new CharactorNode('#', null));

            // 显示初态信息
            AddAViewNode(lstViewNodes, StatusStack, SumupStack, words, nStartWordIndex, null);

            // 将要生成但尚未生成的临时变量序号
            int nTemporaryVariable = 1;

            // 开始分析
            while (true)
            {
                int CurrentStatus = StatusStack.Peek();
                Helper.Word CurrentWord = words[nStartWordIndex];
                char CurrentCharOfWord = GetWordCharactor(CurrentWord);
                LRAction lrAction = lrTable.GetAction(CurrentStatus, CurrentCharOfWord);
                switch (lrAction.actionType)
                {
                    case ActionType.Push:
                        StatusStack.Push(lrAction.StatusOrProductionIndex);
                        SumupStack.Push(new CharactorNode(CurrentCharOfWord, CurrentWord));
                        nStartWordIndex++;
                        AddAViewNode(lstViewNodes, StatusStack, SumupStack, words, nStartWordIndex, null);
                        break;
                    case ActionType.Sumup:
                        string right = grammar.Productions[lrAction.StatusOrProductionIndex].Right;
                        int len = right.Length;
                        Production pro = grammar.Productions[lrAction.StatusOrProductionIndex];
                        char left = grammar.Productions[lrAction.StatusOrProductionIndex].Left;

                        if (len == 1)
                        {
                            LRHelper.CharactorNode charactorNode = SumupStack.Pop();
                            StatusStack.Pop();
                            SumupStack.Push(new CharactorNode(left, charactorNode.word));

                        }
                        else if (len == 3)
                        {
                            List<LRHelper.CharactorNode> charactorNodes = new List<CharactorNode>();
                            for (int i = 0; i < len; i++)
                            {
                                StatusStack.Pop();
                                charactorNodes.Add(SumupStack.Pop());
                            }

                            if (GetWordCharactor(charactorNodes[1].word) == charactorNodes[1].word.Value[0])
                            {
                                Helper.Word WordTemp = NewTemp(ref nTemporaryVariable);
                                mcMidCodes.Gen(GetWordCharactor(charactorNodes[1].word), charactorNodes[2].word, charactorNodes[0].word, WordTemp);
                                SumupStack.Push(new CharactorNode(left, WordTemp));
                            }
                            else
                            {
                                SumupStack.Push(new CharactorNode(left, charactorNodes[1].word));
                            }
                        }
                        else if (len == 4)
                        {
                            List<LRHelper.CharactorNode> ccharactorNodes = new List<CharactorNode>();
                            for (int i = 0; i < len; i++)
                            {
                                StatusStack.Pop();
                                ccharactorNodes.Add(SumupStack.Pop());
                            }
                            if (left == 'X')
                            {
                                mcMidCodes.Gen(GetWordCharactor(ccharactorNodes[2].word), ccharactorNodes[1].word, null, ccharactorNodes[3].word);
                                SumupStack.Push(new CharactorNode(left, null));
                            }
                        }


                        lrAction = lrTable.GetAction(StatusStack.Peek(), grammar.Productions[lrAction.StatusOrProductionIndex].Left);
                        if (lrAction.actionType != ActionType.Error)
                        {
                            StatusStack.Push(lrAction.StatusOrProductionIndex);

                            AddAViewNode(lstViewNodes, StatusStack, SumupStack, words, nStartWordIndex, pro);
                        }
                        else
                            return false;
                        break;
                    case ActionType.Accept:
                        return true;
                    case ActionType.Error:
                        return false;
                }
            }
        }

        /// <summary>
        /// 生成临时变量
        /// </summary>
        /// <param name="nTemporaryVariable">将要生成但尚未生成的临时变量序号</param>
        /// <returns></returns>
        private static Word NewTemp(ref int nTemporaryVariable)
        {
            return new Word(Helper.WordType.Variable, string.Format("T{0}", nTemporaryVariable++));
        }

        /// <summary>
        /// 获得单词对应的文法符号
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns>对应的文法符号</returns>
        private static char GetWordCharactor(Helper.Word word)
        {
            switch (word.Type)
            {
                case Helper.WordType.IntConst:
                    return 'i';
                case Helper.WordType.Variable:
                    return 'i';
                case Helper.WordType.Boundary:
                    return word.Value[0];
                case Helper.WordType.Operator:
                    return word.Value[0];
                case Helper.WordType.Error:
                    return 'A';
            }
            return 'A';
        }

        /// <summary>
        /// 显示节点的构造方法
        /// </summary>
        /// <param name="lstViewNodes">显示节点的列表</param>
        /// <param name="StatusStack">状态栈</param>
        /// <param name="SumupStack">文法符号栈</param>
        /// <param name="lstWords">单词列表</param>
        /// <param name="nWordIndex">从第几个单词开始显示</param>
        /// <param name="production">归约用的产生式</param>
        private static void AddAViewNode(List<ViewNode> lstViewNodes, Stack<Int32> StatusStack, Stack<CharactorNode> SumupStack, List<Helper.Word> lstWords, int nWordIndex, Production production)
        {
            ViewNode vn = new ViewNode(StatusStack, SumupStack, lstWords, nWordIndex, production);
            lstViewNodes.Add(vn);
        }
    }
}
