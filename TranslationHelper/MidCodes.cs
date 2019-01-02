using System;
using System.Collections.Generic;
using System.Text;
using Helper;

namespace TranslationHelper
{
    public class MidCodes
    {
        public List<MidCode> lstMidCodes = new List<MidCode>();

        public MidCodes()
        {
        }

        public void Gen(char Operator, Word Arg1, Word Arg2, Word Result)
        {
            MidCode mc = new MidCode(Operator, Arg1, Arg2, Result);
            lstMidCodes.Add(mc);
        }
    }
}
