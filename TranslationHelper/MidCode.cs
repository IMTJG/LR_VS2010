using System;
using System.Collections.Generic;
using System.Text;
using Helper;

namespace TranslationHelper
{
    public class MidCode
    {
        public char Operator { get; set; }  // 运算符
        public Word Arg1 { get; set; }      // 运算数1
        public Word Arg2 { get; set; }      // 运算数2
        public Word Result { get; set; }    // 运算结果

        // 四元式的字符串形式
        public string MidCodeString
        {
            get
            {
                return string.Format("({0}, {1}, {2}, {3})", Operator, Arg1.Value,
                    Arg2 == null ? "" : Arg2.Value, Result.Value);
            }
        }

        public MidCode(char Operator, Word Arg1, Word Arg2, Word Result)
        {
            this.Operator = Operator;
            this.Arg1 = Arg1;
            this.Arg2 = Arg2;
            this.Result = Result;
        }
    }
}
