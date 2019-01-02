using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class Analysis
    {
        /// <summary>
        /// 词法分析
        /// </summary>
        /// <param name="strBuffer">字符缓冲区</param>
        /// <returns>单词列表</returns>
        public static List<Word> AnalysisWords(string strBuffer)
        {
            // 声明变量，当前字符位置为0
            int nCurrentPosition = 0;
            Word word;

            // 创建单词列表
            List<Word> words = new List<Word>();

            // 识别单词
            while (nCurrentPosition < strBuffer.Length)
            {
                // 如果当前字符为空格，则跳过
                if (strBuffer[nCurrentPosition] == ' ')
                    nCurrentPosition++;

                // 识别一个单词
                word = IdentifyOneWord(strBuffer, ref nCurrentPosition);
                if (word == null)
                {
                    words.Add(new Word(WordType.Error, "错误"));
                    return words;
                }

                // 将识别出的单词加入列表
                words.Add(word);
            }

            // 返回单词列表
            return words;
        }

        /// <summary>
        /// 识别一个单词
        /// </summary>
        /// <param name="strBuffer">字符缓冲区</param>
        /// <param name="nCurrentPosition">输入和输出时都指向将要处理但尚未处理的字符</param>
        /// <returns>返回识别出的单词，如果识别失败返回null</returns>
        private static Word IdentifyOneWord(string strBuffer, ref int nCurrentPosition)
        {
            int CurrentStatus = 0;
            int nstatus;
            int begin = nCurrentPosition;
            Word CurrentWord = null;
            while (nCurrentPosition < strBuffer.Length)
            {
                nstatus = GetNextStatus(CurrentStatus, strBuffer[nCurrentPosition]);
                if (nstatus == 5)
                {
                    CurrentWord = SplitAWord(strBuffer, begin, nCurrentPosition - 1, CurrentStatus);
                    break;
                }
                else if (nstatus == 6)
                {
                    CurrentWord = null;
                    break;
                }
                else
                {
                    CurrentStatus = nstatus;
                    nCurrentPosition++;
                    if (nCurrentPosition == strBuffer.Length)
                    {
                        CurrentWord = SplitAWord(strBuffer, begin, nCurrentPosition - 1, nstatus);
                    }
                }
            }
            return CurrentWord;
        }

        /// <summary>
        /// 根据当前状态和当前字符查询下一状态
        /// </summary>
        /// <param name="nCurrentStatus">当前状态</param>
        /// <param name="cCurrentCharactor">当前字符</param>
        /// <returns>下一状态</returns>
        private static int GetNextStatus(int nCurrentStatus, char cCurrentCharactor)
        {
            int NextStatus = nCurrentStatus;
            switch (nCurrentStatus)
            {
                case 0:
                    if (CharactorHelper.IsBoundary(cCurrentCharactor))
                        NextStatus = 1;
                    else if (CharactorHelper.IsLetterOr_(cCurrentCharactor))
                        NextStatus = 4;
                    else if (CharactorHelper.IsNumber(cCurrentCharactor))
                        NextStatus = 3;
                    else if (CharactorHelper.IsOperator(cCurrentCharactor))
                        NextStatus = 2;
                    else
                        NextStatus = 6;
                    break;
                case 1:
                    NextStatus = 5;
                    break;
                case 2:
                    NextStatus = 5;
                    break;
                case 3:
                    if (!CharactorHelper.IsNumber(cCurrentCharactor))
                    {
                        NextStatus = 5;
                    }
                    else
                    {
                        NextStatus = 3;
                    }
                    break;
                case 4:
                    if (!CharactorHelper.IsLetterOr_(cCurrentCharactor) && !CharactorHelper.IsNumber(cCurrentCharactor))
                    {
                        NextStatus = 5;
                    }
                    else
                    {
                        NextStatus = 4;
                    }
                    break;
            }
            return NextStatus;
        }

        /// <summary>
        /// 拆分出一个单词
        /// </summary>
        /// <param name="strBuffer">字符缓冲区</param>
        /// <param name="nCharactorFrom">单词的第一个字符位置</param>
        /// <param name="nCharactorTo">单词的最后一个字符位置</param>
        /// <param name="nLastStatus">终态的前一个状态</param>
        /// <returns>识别出的单词</returns>
        private static Word SplitAWord(string strBuffer, int nCharactorFrom, int nCharactorTo, int nLastStatus)
        {
            string strWordValue = strBuffer.Substring(nCharactorFrom, nCharactorTo - nCharactorFrom + 1);
            WordType wt = GetWordType(nLastStatus);
            return new Word(wt, strWordValue);
        }

        /// <summary>
        /// 根据终态的前一个状态确定单词类别
        /// </summary>
        /// <param name="nLastStatus">终态的前一个状态</param>
        /// <returns>单词类别</returns>
        private static WordType GetWordType(int nLastStatus)
        {
            switch (nLastStatus)
            {
                case 1:
                    return WordType.Boundary;
                case 2:
                    return WordType.Operator;
                case 3:
                    return WordType.IntConst;
                case 4:
                    return WordType.Variable;
            }
            return WordType.Error;
        }
    }
}
