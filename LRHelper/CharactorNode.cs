using System;
using System.Collections.Generic;
using System.Text;

namespace LRHelper
{
    public class CharactorNode
    {
        public char Charactor { get; set; }     // 文法符号
        public Helper.Word word { get; set; }   // 对应的单词

        public CharactorNode(char cCharactor, Helper.Word word)
        {
            this.Charactor = cCharactor;
            this.word = word;
        }
    }
}
