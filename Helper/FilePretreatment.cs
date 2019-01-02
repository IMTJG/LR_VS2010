using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Helper
{
    public class FilePretreatment
    {
        /// <summary>
        /// 读取C#文件，并进行预处理
        /// </summary>
        /// <param name="strSourceFilePath">源文件路径</param>
        /// <returns>预处理后的源程序</returns>
        public static string ReadAndPretreat(string strSourceFilePath)
        {
            // 读取文件内容
            string strBuffer = Read(strSourceFilePath);

            // 预处理
            return Pretreat(strBuffer);
        }

        public static string Read(string strSourceFilePath)
        {
            /**************************************************************
            * 读取文件内容，放入到缓冲区byBuffer中 
            **************************************************************/
            // 打开文件，返回数据流数据类型
            Stream stream = File.Open(strSourceFilePath, FileMode.Open, FileAccess.Read);
            // 根据数据流的长度确定缓冲区的长度，声明缓冲区数组
            byte[] byBuffer = new byte[stream.Length];
            // 从数据流中读取所有字节，放入缓冲区
            stream.Read(byBuffer, 0, byBuffer.Length);
            // 关闭数据流
            stream.Close();

            // 根据前两个字节获得文件的字符集编码
            Encoding encoding = GetFileEncodeType(byBuffer[0], byBuffer[1]);

            // 通过字符集编码，把缓冲区的字节数组转换为字符串类型的缓冲区
            // 字节数组中，除前两个字节表示字符集编码外，还有一个字节用作标识位。
            // 要把这3个字节都去掉，从byBuffer的第三个字节开始转换
            return encoding.GetString(byBuffer, 3, byBuffer.Length - 3);
        }

        /// <summary>
        /// 根据文本文件的前两个字节获得文件的字符集编码
        /// 文件的字符集在Windows下有两种，一种是ANSI，一种Unicode。
        /// 对于Unicode，Windows支持了它的三种编码方式：小尾编码（Unicode)、大尾编码(BigEndianUnicode)、UTF-8编码。
        /// 我们可以从文件的头部的前两个字节来区分一个文件是属于哪种编码：
        /// FF FE-Unicode的小尾编码
        /// FE FF-Unicode的大尾编码
        /// EF BB-Unicode的UTF-8编码
        /// 其它，则是ANSI编码。
        /// </summary>
        /// <param name="byFlag1">文件中第一个字节</param>
        /// <param name="byFlag2">文件中第二个字节</param>
        /// <returns>字符集编码</returns>
        private static Encoding GetFileEncodeType(byte byFlag1, byte byFlag2)
        {
            if (byFlag1 == 0xFF && byFlag2 == 0xFE)
                return Encoding.Unicode;
            if (byFlag1 == 0xFE && byFlag2 == 0xFF)
                return Encoding.BigEndianUnicode;
            if (byFlag1 == 0xEF && byFlag2 == 0xBB)
                return Encoding.UTF8;
            return Encoding.Default;
        }

        /// <summary>
        /// 预处理
        /// （1）删除注释，包括行注释//和多行注释/*   */
        /// （2）删除空白，包括\t、\r、\n
        /// （3）多个空格合并为一个
        /// </summary>
        /// <param name="strBuffer">字符缓冲区</param>
        /// <returns>预处理后的字符缓冲区</returns>
        private static string Pretreat(string strBuffer)
        {
            // 获得缓冲区的长度
            int nBufferLength = strBuffer.Length;
            // 初始化当前状态为0
            int nCurrentStatus = 0;
            // 当前字符
            char cCurrentCharactor;

            // 对每个字符进行预处理
            for (int nCharactorIndex = 0; nCharactorIndex < nBufferLength; nCharactorIndex++)
            {
                // 当前字符
                cCurrentCharactor = strBuffer[nCharactorIndex];

                // 根据当前状态
                switch (nCurrentStatus)
                {
                    case 0:
                        if (cCurrentCharactor == '/')   // 可能是注释
                            nCurrentStatus = 1;
                        else if (cCurrentCharactor == ' ')
                            nCurrentStatus = 5;
                        else if (cCurrentCharactor == '\r' || cCurrentCharactor == '\n' || cCurrentCharactor == '\t')
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                        break;
                    case 1:
                        if (cCurrentCharactor == '/')   // 行注释符
                            nCurrentStatus = 2;
                        else if (cCurrentCharactor == '*')  // 多行注释
                            nCurrentStatus = 3;
                        else
                        {
                            nCurrentStatus = 0;     // 不是注释
                            nCharactorIndex--;
                        }
                        break;
                    case 2:
                        if (cCurrentCharactor == '\r' || cCurrentCharactor == '\n') // 行注释的结束，要删除前面的//
                        {
                            nCharactorIndex--;
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                            nCurrentStatus = 0;
                        }
                        else
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                        break;
                    case 3:
                        if (cCurrentCharactor == '*')   // 可能是注释的结束
                        {
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                            nCurrentStatus = 4;
                        }
                        else
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                        break;
                    case 4:
                        if (cCurrentCharactor == '/')   // 注释结束
                        {
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                            // 删除前面的/*
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                            nCurrentStatus = 0;
                        }
                        else if (cCurrentCharactor == '*')  // 还可能是注释的结束
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                        else  // 不是注释的结束
                        {
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                            nCurrentStatus = 3;
                        }
                        break;
                    case 5:
                        if (cCurrentCharactor == ' ')   // 多余的空格
                            RemoveACharactor(ref strBuffer, ref nCharactorIndex, ref nBufferLength);
                        else
                        {
                            nCurrentStatus = 0;
                            nCharactorIndex--;
                        }
                        break;
                }
            }

            return strBuffer;
        }

        /// <summary>
        /// 从字符缓冲区的指定位置删除一个字符
        /// </summary>
        /// <param name="strBuffer">字符缓冲区，使用引用调用同时做输入和输出参数</param>
        /// <param name="nRemovingPosition">要删除字符的位置，使用引用调用同时做输入和输出参数</param>
        /// <param name="nBufferLength">字符缓冲区的长度，使用引用调用同时做输入和输出参数</param>
        private static void RemoveACharactor(ref string strBuffer, ref int nRemovingPosition, ref int nBufferLength)
        {
            strBuffer = strBuffer.Remove(nRemovingPosition, 1);
            nRemovingPosition--;
            nBufferLength--;
        }
    }
}
