using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Helper
{
    public class WordsIOHelper
    {
        /// <summary>
        /// 保存单词序列
        /// </summary>
        /// <param name="strDestFilePath">目标文件路径</param>
        /// <param name="lstWords">单词序列</param>
        public static void Save(string strDestFilePath, List<Word> lstWords)
        {
            // 打开文件
            Stream stream = File.Open(strDestFilePath, FileMode.Create, FileAccess.Write);

            // 先保存单词个数
            BinaryIOHelper.WriteAInt(stream, lstWords.Count);

            // 再逐个保存单词
            foreach (Word word in lstWords)
                word.Save(stream);

            // 关闭文件
            stream.Close();
        }

        /// <summary>
        /// 读取单词序列
        /// </summary>
        /// <param name="strFilePath">单词所在文件的路径</param>
        /// <returns>读取出的单词序列</returns>
        public static List<Word> Read(string strFilePath)
        {
            // 打开文件
            Stream stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read);

            // 读取单词个数
            int nWordsCount = BinaryIOHelper.ReadAInt(stream);

            // 声明一个单词对象，并创建一个单词链表
            Word word;
            List<Word> lstWords = new List<Word>();

            // 读取单词
            for (int i = 0; i < nWordsCount; i++)
            {
                // 使用无参构造函数构造一个单词对象
                word = new Word();
                // 读取单词的数据
                word.Read(stream);
                // 将该单词加入单词链表
                lstWords.Add(word);

                // 是否是句子的结束符，如果是，则追加一个#
                if (word.Value == ";")
                    lstWords.Add(new Word(WordType.Boundary, "#"));
            }

            // 关闭文件，并返回单词链表
            stream.Close();
            return lstWords;
        }
    }
}
