using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LRHelper
{
    public class Grammar
    {
        public List<Production> Productions { get; set; }   // 产生式集
        public List<char> VNs { get; set; }     // 非终极符号集
        public List<char> VTs { get; set; }     // 终极符号集
        public char StartCharactor { get; set; }    // 开始符号

        public Grammar()
        {
            Productions = new List<Production>();
        }

        public Production AddProduction(char cLeft, string strRight)
        {
            Production p = new Production(cLeft, strRight);
            Productions.Add(p);
            CalculateCharactors();
            return p;
        }

        public void DeleteProduction(int nIndex)
        {
            Productions.RemoveAt(nIndex);
            CalculateCharactors();
        }

        public Production ModifyProduction(int nIndex, char cLeft, string strRight)
        {
            Productions[nIndex].Left = cLeft;
            Productions[nIndex].Right = strRight;
            CalculateCharactors();
            return Productions[nIndex];
        }

        public int GetProductionIndex(char cLeft, string strRight)
        {
            for (int i = 0; i < Productions.Count; i++)
            {
                if (Productions[i].Left == cLeft && Productions[i].Right == strRight)
                    return i;
            }
            return -1;
        }

        private void CalculateCharactors()
        {
            if (Productions.Count == 0)
                return;

            StartCharactor = Productions[0].Left;

            VNs = new List<char>();
            foreach (Production p in Productions)
            {
                if (!IsVN(p.Left))
                    VNs.Add(p.Left);
            }

            VTs = new List<char>();
            foreach (Production p in Productions)
            {
                for (int i = 0; i < p.Right.Length; i++)
                {
                    if (!IsVN(p.Right[i]) && !IsVT(p.Right[i]))
                        VTs.Add(p.Right[i]);
                }
            }
            VTs.Add('#');
        }

        public bool IsVT(char c)
        {
            return IsCharIn(c, VTs);
        }

        public bool IsVN(char c)
        {
            return IsCharIn(c, VNs);
        }

        private bool IsCharIn(char c, List<char> charSet)
        {
            if (charSet == null)
                return false;

            foreach (char cInSet in charSet)
            {
                if (c == cInSet)
                    return true;
            }
            return false;
        }

        public void Save(string strFilePath)
        {
            Stream stream = File.Open(strFilePath, FileMode.Create, FileAccess.Write);

            byte[] byLen = BitConverter.GetBytes(Productions.Count);
            stream.Write(byLen, 0, byLen.Length);

            foreach (Production p in Productions)
                p.Save(stream);

            stream.Close();
        }

        public void Read(string strFilePath)
        {
            Stream stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read);

            byte[] byLen = new byte[4];
            stream.Read(byLen, 0, byLen.Length);
            int nLen = BitConverter.ToInt32(byLen, 0);

            Production p;
            Productions = new List<Production>();
            for (int i = 0; i < nLen; i++)
            {
                p = new Production();
                p.Read(stream);
                Productions.Add(p);
            }

            stream.Close();
            CalculateCharactors();
        }

        public int GetColumnIndex(char c)
        {
            for (int i = 0; i < VTs.Count; i++)
            {
                if (VTs[i] == c)
                    return i + 1;
            }

            for (int i = 0; i < VNs.Count; i++)
            {
                if (VNs[i] == c)
                    return VTs.Count + i + 1;
            }

            return -1;
        }
    }
}
