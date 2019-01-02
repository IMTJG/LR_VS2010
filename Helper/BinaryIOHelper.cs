using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Helper
{
    public class BinaryIOHelper
    {
        /// <summary>
        /// 向目标数据流写入一个整数
        /// </summary>
        /// <param name="stream">目标数据流</param>
        /// <param name="n">要写入的整数</param>
        static public void WriteAInt(Stream stream, int n)
        {
            // 将整数编码为字节数组
            byte[] by = BitConverter.GetBytes(n);
            // 将字节数组从数据流读出
            stream.Write(by, 0, by.Length);
        }

        /// <summary>
        /// 从数据流读出一个整数
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns>读出的整数</returns>
        static public int ReadAInt(Stream stream)
        {
            // 整数长度为4，从数据流中读出4个字节
            byte[] by = new byte[4];
            stream.Read(by, 0, 4);
            // 将字节数组转换为整数，并返回
            return BitConverter.ToInt32(by, 0);
        }

        /// <summary>
        /// 向目标数据流写入一个字符串
        /// </summary>
        /// <param name="stream">目标数据流</param>
        /// <param name="encoding">采用的字符集编码</param>
        /// <param name="str">要写入的字符串</param>
        static public void WriteAString(Stream stream, Encoding encoding, string str)
        {
            // 如果字符串为空，则只写入长度0
            if (string.IsNullOrEmpty(str))
            {
                WriteAInt(stream, 0);
                return;
            }

            // 将字符串编码为字节数组
            byte[] byStr = encoding.GetBytes(str);
            // 先写入字节数组长度
            WriteAInt(stream, byStr.Length);
            // 再写入字符串编码
            stream.Write(byStr, 0, byStr.Length);
        }

        /// <summary>
        /// 从数据流读出一个字符串
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="encoding">字符集编码</param>
        /// <returns>读出的字符串</returns>
        static public string ReadAString(Stream stream, Encoding encoding)
        {
            // 先读出字符串长度，如果为0，则是空字符串
            int nLen = ReadAInt(stream);
            if (nLen == 0)
                return "";

            // 根据字符串长度，读出相应字节数
            byte[] byData = new byte[nLen];
            stream.Read(byData, 0, nLen);

            // 解码为字符串并返回
            return encoding.GetString(byData);
        }

        static public void WriteAASCIICharactor(Stream stream, char c)
        {
            byte by = Convert.ToByte(c);
            stream.WriteByte(by);
        }

        static public char ReadAASCIICharactor(Stream stream)
        {
            int nRead = stream.ReadByte();
            return Convert.ToChar(Convert.ToByte(nRead));
        }
    }
}
