using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class CharactorHelper
    {
        /// <summary>
        /// 判断一个字符是否为数字
        /// </summary>
        /// <param name="cCharactor">要判断的字符</param>
        /// <returns>是数字返回true，否则返回false</returns>
        public static bool IsNumber(char cCharactor)
        {
            return char.IsDigit(cCharactor);
        }

        /// <summary>
        /// 判断一个字符是否为字母或下划线
        /// </summary>
        /// <param name="cCharactor">要判断的字符</param>
        /// <returns>是字母或下划线返回true，否则返回false</returns>
        public static bool IsLetterOr_(char cCharactor)
        {
            return cCharactor == '_' || char.IsLetter(cCharactor);
        }

        /// <summary>
        /// 判断一个字符是否为运算符
        /// </summary>
        /// <param name="cCharactor">要判断的字符</param>
        /// <returns>是运算符返回true，否则返回false</returns>
        public static bool IsOperator(char cCharactor)
        {
            switch (cCharactor)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '=':
                case '(':
                case ')':
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断一个字符是否为界符
        /// </summary>
        /// <param name="cCharactor">要判断的字符</param>
        /// <returns>是界符返回true，否则返回false</returns>
        public static bool IsBoundary(char cCharactor)
        {
            return cCharactor == ';';
        }
    }
}
