using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Helper
{
    /// <summary>
    /// 单词类别
    /// </summary>
    public enum WordType
    {
        Operator,   // 算符
        IntConst,   // 常数（无符号整数）
        Variable,   // 变量（标识符）
        Boundary,   // 界符
        Error       // 错误
    }

    public class Word
    {
        /// <summary>
        /// 单词类别
        /// </summary>
        public WordType Type { get; set; }
        public string TypeName
        {
            get
            {
                switch (Type)
                {
                    case WordType.Operator:
                        return "算符";
                    case WordType.IntConst:
                        return "常数";
                    case WordType.Variable:
                        return "变量";
                    case WordType.Boundary:
                        return "界符";
                    case WordType.Error:
                        return "错误";
                }
                return "未定义";
            }
        }

        /// <summary>
        /// 单词值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Word()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="wt">单词类别</param>
        /// <param name="strValue">单词值</param>
        public Word(WordType wt, string strValue)
        {
            Type = wt;
            Value = strValue;
        }

        /// <summary>
        /// 获得在ListView控件中显示的字符串数组
        /// </summary>
        /// <returns></returns>
        public string[] GetListRowStrings()
        {
            string[] strData = new string[2];
            strData[0] = TypeName;
            strData[1] = Value;
            return strData;
        }

        /// <summary>
        /// 以二进制字节形式保存到数据流
        /// </summary>
        /// <param name="stream">要保存的目标数据流</param>
        public void Save(Stream stream)
        {
            // 将单词类别转换为整数保存
            BinaryIOHelper.WriteAInt(stream, (int)Type);
            // 以ASCII码保存单词的值
            BinaryIOHelper.WriteAString(stream, Encoding.ASCII, Value);
        }

        /// <summary>
        /// 从数据流读取二进制字节形式保存的单词
        /// </summary>
        /// <param name="stream">要读取的数据流</param>
        public void Read(Stream stream)
        {
            // 读出一个整数，并转换为单词类别的枚举
            Type = (WordType)BinaryIOHelper.ReadAInt(stream);
            // 以ASCII码读取单词的值
            Value = BinaryIOHelper.ReadAString(stream, Encoding.ASCII);
        }
    }
}
