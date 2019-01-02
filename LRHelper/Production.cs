using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LRHelper
{
    public class Production
    {
        public char Left { get; set; }      // 产生式左部
        public string Right { get; set; }   // 产生式右部

        public Production()
        {
        }

        public Production(char cLeft, string strRight)
        {
            Left = cLeft;
            Right = strRight;
        }

        public string[] GetBindData(int nIndex)
        {
            string[] strData = new string[3];
            strData[0] = nIndex.ToString();
            strData[1] = string.Format("{0}", Left);
            strData[2] = Right;
            return strData;
        }

        public void Save(Stream stream)
        {
            string strTemp = string.Format("{0}{1}", Left, Right);
            byte[] byTemp = ASCIIEncoding.ASCII.GetBytes(strTemp);
            byte[] byLength = BitConverter.GetBytes(byTemp.Length);
            stream.Write(byLength, 0, byLength.Length);
            stream.Write(byTemp, 0, byTemp.Length);
        }

        public void Read(Stream stream)
        {
            byte[] byLength = new byte[4];
            stream.Read(byLength, 0, 4);
            int nLen = BitConverter.ToInt32(byLength, 0);

            byte[] byTemp = new byte[nLen];
            stream.Read(byTemp, 0, nLen);
            string strTemp = ASCIIEncoding.ASCII.GetString(byTemp);

            Left = strTemp[0];
            Right = strTemp.Substring(1);
        }
    }
}
