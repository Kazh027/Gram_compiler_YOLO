using System.Collections.Generic;
using System.Text;

namespace GramaticasFormales
{
    //**************
    class FormalValues
    {
        public List<Tokens> FomalTokens;
        public List<Options> OptionValues;


        public FormalValues()
        {
            FomalTokens = new List<Tokens>();
            OptionValues = new List<Options>();

        }

    }
    //**************
    class Options
    {
        public List<Tokens> OptionTokens;
        public int hasOperator;
        public Options()
        {
            OptionTokens = new List<Tokens>();
            hasOperator = 0; //none
        }

        public Options(List<Tokens> opstks)
        {
            OptionTokens = new List<Tokens>();
            OptionTokens = opstks;
            hasOperator = 0; //none
        }

        public Options(List<string> Tokname, List<bool> TokType, List<int> op)
        {
            OptionTokens = new List<Tokens>();
            for (int i = 0; i < Tokname.Count; i++)           
                OptionTokens.Add(new Tokens(Tokname[i], TokType[i], op[i]));
            hasOperator = 0; //none
        }
    }
    //**************
    class Tokens
    {
        public string TokenName;
        public bool TokenType; //true -> Formal false -> terminal
        public int hasOperator;
        public List<Tokens> MyTokens;

        public Tokens(string in_TokenName, bool in_TokenType)
        {
            TokenName = in_TokenName;
            TokenType = in_TokenType;
            hasOperator = 0; //none
            MyTokens = null; //normal data
        }

        public Tokens(string in_TokenName, bool in_TokenType ,int hoperator)
        {
            TokenName = in_TokenName;
            TokenType = in_TokenType;
            hasOperator = hoperator;
            MyTokens = null; //normal data
        }

        public Tokens(List<Tokens> inputtks,int hoperator)
        {
            TokenName = "null";
            TokenType = false;
            hasOperator = hoperator;
            MyTokens = inputtks;
        }
    }
}
