using Shields.GraphViz.Components;
using Shields.GraphViz.Models;
using Shields.GraphViz.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GramaticasFormales
{
    class AnalisadorGram
    {
        private char[] StringSplited;
        public bool ReadingFormalG;
        public bool EqualReaded;
        public List<FormalValues> FValues;
        public List<FormalValues> ERFValues;
        public Image CurrentImage;

        public AnalisadorGram()
        {
            FValues = new List<FormalValues>();
            ERFValues = new List<FormalValues>();
        }

        public bool AnaliseString(string[] inputLine)
        {
            bool EpsilonError = false;

            foreach (string it_inputLine in inputLine)
            {
                ReadingFormalG = false;
                EqualReaded = false;
                StringSplited = it_inputLine.ToArray();
                StringBuilder sb = new StringBuilder();
                FValues.Add(new FormalValues());
                for (int it = 0; it < StringSplited.Length; it++)
                {
                    if (!EqualReaded && StringSplited[it] == '-')
                    {
                        if (StringSplited[it + 1] == '>')
                        {
                            FValues.Last().OptionValues.Add(new Options());
                            EqualReaded = true;
                            StringSplited[it + 1] = ' ';
                            it++;
                        }
                    }
                    if (StringSplited[it] == '|')
                        FValues.Last().OptionValues.Add(new Options());
                    if (StringSplited[it] == '<')
                        ReadingFormalG = true;
                    if (StringSplited[it] == '>')
                    {
                        ReadingFormalG = false;
                        sb.Remove(0, 1);
                        if (!EqualReaded)
                            FValues.Last().FomalTokens.Add(new Tokens(sb.ToString(), true));
                        else
                            FValues.Last().OptionValues.Last().OptionTokens.Add(new Tokens(sb.ToString(), true));
                        sb.Clear();
                    }
                    if (ReadingFormalG)
                        sb.Append(StringSplited[it]);
                    else
                    {
                        if (Char.IsLetter(StringSplited[it]) && StringSplited[it] != 'Ɛ')
                        {
                            if (Char.IsUpper(StringSplited[it]))
                            {
                                if (!EqualReaded)
                                    FValues.Last().FomalTokens.Add(new Tokens(StringSplited[it].ToString(), true));
                                else
                                    FValues.Last().OptionValues.Last().OptionTokens.Add(new Tokens(StringSplited[it].ToString(), true));
                            }
                            else
                            {
                                if (!EqualReaded)
                                    FValues.Last().FomalTokens.Add(new Tokens(StringSplited[it].ToString(), false));
                                else
                                    FValues.Last().OptionValues.Last().OptionTokens.Add(new Tokens(StringSplited[it].ToString(), false));
                            }
                        }
                        if (Char.IsDigit(StringSplited[it]))
                        {
                            if (!EqualReaded)
                                FValues.Last().FomalTokens.Add(new Tokens(StringSplited[it].ToString(), false));
                            else
                                FValues.Last().OptionValues.Last().OptionTokens.Add(new Tokens(StringSplited[it].ToString(), false));
                        }
                        if (StringSplited[it] == 'Ɛ')
                        {
                            if (!EqualReaded)
                            {
                                EpsilonError = true;
                                it = StringSplited.Length;
                            }
                            else
                            {
                                FValues.Last().OptionValues.Last().OptionTokens.Add(new Tokens(StringSplited[it].ToString(), false));
                            }
                        }
                    }
                }
            }
            return EpsilonError;
        }

        public string GenerateG()
        {
            StringBuilder sb = new StringBuilder();
            List<char> usedchars = new List<char>();
            sb.Append("G"); sb.AppendLine();
            sb.Append("{"); sb.AppendLine();
            sb.Append("Vn : ");
            foreach (FormalValues it_FV in FValues)
                foreach (Tokens it_token in it_FV.FomalTokens)
                    if (it_token.TokenType && !usedchars.Contains(it_token.TokenName[0]))
                    {
                        usedchars.Add(it_token.TokenName[0]);
                        sb.Append(it_token.TokenName + " ");
                    }
            sb.AppendLine();
            sb.Append("Vt : ");
            usedchars = new List<char>();
            foreach (FormalValues it_FV in FValues)
                foreach (Options it_opts in it_FV.OptionValues)
                    foreach (Tokens it_token in it_opts.OptionTokens)
                        if (!it_token.TokenType && !usedchars.Contains(it_token.TokenName[0]))
                        {
                            usedchars.Add(it_token.TokenName[0]);
                            sb.Append(it_token.TokenName + " ");
                        }
            sb.AppendLine(); sb.Append("S : ");
            foreach (Tokens it_token in FValues[0].FomalTokens)
                if (it_token.TokenType)
                {
                    sb.Append(it_token.TokenName);
                    break;
                }
            sb.AppendLine(); sb.Append("Ø : "); sb.AppendLine();
            foreach (FormalValues it_FV in FValues)
            {
                foreach (Tokens it_token in it_FV.FomalTokens)
                {
                    if (it_FV.FomalTokens.First() == it_token)
                    {
                        sb.Append("     " + it_token.TokenName);
                        if (it_FV.FomalTokens.Count == 1)
                            sb.Append(" -> ");
                    }
                    else
                        if (it_FV.FomalTokens.Last() == it_token)
                        sb.Append(" " + it_token.TokenName + " -> ");
                    else
                        sb.Append(" " + it_token.TokenName);
                }
                foreach (Options it_opts in it_FV.OptionValues)
                {
                    foreach (Tokens it_token in it_opts.OptionTokens)
                        sb.Append(it_token.TokenName + " ");
                    if (it_opts != it_FV.OptionValues.Last())
                        sb.Append("| ");
                }
                sb.AppendLine();
            }
            sb.Append("}");
            return sb.ToString();
        }

        public string GenerateClassif()
        {
            bool isAminorB = true, isAalone = true, isBminor2 = true;
            string Answer = "Unrestricted";
            foreach (FormalValues it_FV in FValues)
            {
                if (it_FV.FomalTokens.Count > 1)
                    isAalone = false;
                foreach (Options it_opts in it_FV.OptionValues)
                {
                    if (it_opts.OptionTokens.Count > 2)
                        isBminor2 = false;
                    if (it_opts.OptionTokens.Count < it_FV.FomalTokens.Count)
                        isAminorB = false;
                }
            }
            if (isAminorB && isAalone && isBminor2)
                Answer = "Regular";
            else
                if (isAminorB && isAalone)
                Answer = "Context-free";
            else
                    if (isAminorB)
                Answer = "Context-sensitive";
            return Answer;
        }

        public List<Options> GenerateER()
        {
            ERFValues = FValues;
            StringBuilder sb = new StringBuilder();
            //Paso_1_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_
            sb.AppendLine("Step 1: ");
            int updater = ERFValues.Count;
            for (int a = 0; a < ERFValues.Count; a++)
            {
                for (int b = a + 1; b < ERFValues.Count; b++)
                {
                    if (ERFValues[a].FomalTokens.First().TokenName == ERFValues[b].FomalTokens.First().TokenName) //detecta si son iguales
                    {
                        for (int c = 0; c < ERFValues[b].OptionValues.Count; c++)
                        {
                            ERFValues[a].OptionValues.Add(ERFValues[b].OptionValues[c]); //une b en a
                        }
                        ERFValues.RemoveAt(b);
                        b--;
                    }
                }
            }
            foreach (FormalValues it_FV in ERFValues) //print step 1
            {
                foreach (Tokens it_token in it_FV.FomalTokens)
                {
                    if (it_FV.FomalTokens.First() == it_token)
                    {
                        sb.Append("     " + it_token.TokenName);
                        if (it_FV.FomalTokens.Count == 1)
                            sb.Append(" -> ");
                    }
                    else
                        if (it_FV.FomalTokens.Last() == it_token)
                        sb.Append(" " + it_token.TokenName + " -> ");
                    else
                        sb.Append(" " + it_token.TokenName);
                }
                foreach (Options it_opts in it_FV.OptionValues)
                {
                    foreach (Tokens it_token in it_opts.OptionTokens)
                        sb.Append(it_token.TokenName + " ");
                    if (it_opts != it_FV.OptionValues.Last())
                        sb.Append("| ");
                }
                sb.AppendLine();
            }
            //Paso_2_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_
            for (int a = 0; a < ERFValues.Count; a++) //Recorre los i que son los valores formales 
            {
                for (int b = 0; b < ERFValues[a].OptionValues.Count; b++) //Recorre las opciones de i
                {
                    for (int c = 0; c < ERFValues[a].OptionValues[b].OptionTokens.Count; c++) // Recorre los tokens de las opciones de i
                    {
                        if (ERFValues[a].FomalTokens[0].TokenName == ERFValues[a].OptionValues[b].OptionTokens[c].TokenName)
                        {
                            //Recursividad
                            ERFValues[a].OptionValues[b].OptionTokens.RemoveAt(c);
                            //if (ERFValues[i].OptionValues[x].OptionTokens.Count == 1)
                            //{
                            ERFValues[a].OptionValues[b].OptionTokens[0].hasOperator = 1;
                            for (int d = 0; d < ERFValues[a].OptionValues.Count; d++)
                            {
                                if (b != d)
                                {
                                    ERFValues[a].OptionValues[d].OptionTokens.Add(ERFValues[a].OptionValues[b].OptionTokens[0]);
                                }
                            }
                            //}
                            //else {  //Hacer los tokens en parentesis y multiplicarlo con las demas opciones TODO  }
                            ERFValues[a].OptionValues.RemoveAt(b);
                        }
                    }
                    //____________________________________________
                    //for (int j = i + 1; j < ERFValues.Count; j++)
                    //{
                    //    TODO
                    //}
                }
            }
            //Paso_3_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_
            for (int i = ERFValues.Count - 1; i >= 0; i--) //Recorre los i desde el ultimo
            {
                for (int j = ERFValues.Count - 1; j >= 0; j--) //Recorre los j desde el ultimo
                {
                    for (int x = 0; x < ERFValues[j].OptionValues.Count; x++) //Recorre las opciones de j
                    {
                        for (int y = 0; y < ERFValues[j].OptionValues[x].OptionTokens.Count; y++) // Recorre los tokens de las opciones de j
                        {
                            if (ERFValues[j].OptionValues[x].OptionTokens[y].TokenName == ERFValues[i].FomalTokens[0].TokenName) //encuentra sustitucion
                            {
                                ERFValues[j].OptionValues[x].OptionTokens.RemoveAt(y);
                                List<string> iWtLS = new List<string>();
                                List<bool> iWtLB = new List<bool>();
                                List<int> iWtLI = new List<int>();
                                foreach (Tokens ittks in ERFValues[j].OptionValues[x].OptionTokens)
                                {
                                    iWtLS.Add(ittks.TokenName);
                                    iWtLB.Add(ittks.TokenType);
                                    iWtLI.Add(ittks.hasOperator);
                                }
                                Options iWantToLive = new Options(iWtLS, iWtLB, iWtLI);
                                ERFValues[j].OptionValues.RemoveAt(x);
                                for (int z = 0; z < ERFValues[i].OptionValues.Count; z++) //Recorre las opciones de i
                                {
                                    List<string> FSPWS = new List<string>();
                                    List<bool> FSPWB = new List<bool>();
                                    List<int> FSPWI = new List<int>();
                                    foreach (Tokens ittks in iWantToLive.OptionTokens)
                                    {
                                        FSPWS.Add(ittks.TokenName);
                                        FSPWB.Add(ittks.TokenType);
                                        FSPWI.Add(ittks.hasOperator);
                                    }
                                    Options FusionPowa = new Options(FSPWS, FSPWB, FSPWI);
                                    FusionPowa.OptionTokens.AddRange(ERFValues[i].OptionValues[z].OptionTokens);
                                    if (ERFValues[j].OptionValues.Count == 0)
                                        ERFValues[j].OptionValues.Add(new Options(FusionPowa.OptionTokens));
                                    else
                                    {
                                        ERFValues[j].OptionValues.Insert(x, FusionPowa);
                                        x++; // Agrega la combinacion de la op de i y iWantToLive a j, aumenta el counter x
                                    }
                                }
                            }
                            //Recursividad Again!!_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_
                            for (int a = 0; a < ERFValues.Count; a++) //Recorre los i 
                                for (int b = 0; b < ERFValues[a].OptionValues.Count; b++) //Recorre las opciones de i 
                                    for (int c = 0; c < ERFValues[a].OptionValues[b].OptionTokens.Count; c++)
                                    { // Recorre los tokens de las opciones de i
                                        if (ERFValues[a].FomalTokens[0].TokenName == ERFValues[a].OptionValues[b].OptionTokens[c].TokenName)
                                        {
                                            //Recursividad
                                            ERFValues[a].OptionValues[b].OptionTokens.RemoveAt(c);
                                            ERFValues[a].OptionValues[b].OptionTokens[0].hasOperator = 1;
                                            for (int d = 0; d < ERFValues[a].OptionValues.Count; d++)
                                                if (b != d)
                                                    ERFValues[a].OptionValues[d].OptionTokens.Add(ERFValues[a].OptionValues[b].OptionTokens[0]);                                             //}
                                            ERFValues[a].OptionValues.RemoveAt(b);
                                        }
                                        if (b >= ERFValues[a].OptionValues.Count)
                                            b--;
                                    }
                            if (x >= ERFValues[j].OptionValues.Count)
                                x--;
                        }
                    }
                }
            }
            return ERFValues.First().OptionValues;
        }

        public string Aumentada(List<Options> ops)
        {
            //genera String de Ampliada
            StringBuilder conjunto = new StringBuilder();
            StringBuilder temporal = new StringBuilder();
            conjunto.Append("Aumentada -> (");
            foreach (Options it_op in ops)
            {
                foreach (Tokens it_tokens in it_op.OptionTokens)
                {
                    if (temporal.ToString() == "")
                    {
                        temporal.Append(it_tokens.TokenName);
                        if (it_tokens.hasOperator == 1)
                            temporal.Append("ᵡ");
                        if (it_tokens.hasOperator == 2)
                            temporal.Append("⁺");
                    }
                    else
                    {
                        temporal.Append(" . ");
                        temporal.Append(it_tokens.TokenName);
                        if (it_tokens.hasOperator == 1)
                            temporal.Append(" ᵡ ");
                        if (it_tokens.hasOperator == 2)
                            temporal.Append(" ⁺ ");
                    }
                }
                if (it_op != ops.Last())
                    temporal.Append(" | ");
                conjunto.Append(temporal.ToString());
                temporal.Clear();
            }
            conjunto.Append(") . # ");
            return conjunto.ToString();
        }

        public string Posfija(List<Options> ops)
        {
            //genera String de Ampliada
            List<char> temporal = new List<char>();
            List<char> conjunto = new List<char>();
            foreach (Options it_op in ops)
            {
                foreach (Tokens it_tokens in it_op.OptionTokens)
                {
                    if (temporal.Count == 0 || (temporal.Count == 1 && temporal[0] == '('))
                    {
                        temporal.Add(it_tokens.TokenName[0]);
                        if (it_tokens.hasOperator == 1)
                            temporal.Add('ᵡ');
                        if (it_tokens.hasOperator == 2)
                            temporal.Add('⁺');
                    }
                    else
                    {
                        temporal.Add('.');
                        temporal.Add(it_tokens.TokenName[0]);
                        if (it_tokens.hasOperator == 1)
                            temporal.Add('ᵡ');
                        if (it_tokens.hasOperator == 2)
                            temporal.Add('⁺');
                    }
                }
                if (it_op != ops.Last())
                    temporal.Add('|');
                conjunto.AddRange(temporal);
                temporal.Clear();
            }
            //____METODO MRECURSIVO___
            string Answer = PosfijaRecurs(conjunto);
            return Answer;
        }

        public string PosfijaRecurs(List<char> currentList)
        {
            Stack<string> ValueStack = new Stack<string>();
            Stack<string> OperatorStack = new Stack<string>();
            string temp1, temp2;
            for (int i = 0; i < currentList.Count; i++)
            {
                switch (currentList[i])
                {
                    case ('ᵡ'):

                        break;
                    case ('⁺'):

                        break;
                    case ('.'):
                        if (ValueStack.Count < 2)
                            OperatorStack.Push(currentList[i].ToString());
                        else
                        {
                            temp2 = ValueStack.Pop();
                            temp1 = ValueStack.Pop();
                            temp1 += currentList[i].ToString();
                            temp1 += temp2;
                            ValueStack.Push(temp1);
                            temp1 = "";
                            temp2 = "";
                        }
                        break;
                    case ('|'):
                        List<char> nextToOr = new List<char>();
                        int isInside = 0;
                        int saveindx2 = i;
                        for (int j = i + 1; j < currentList.Count; j++)
                        {
                            if (currentList[j] == '|' && isInside == 0)
                            {
                                saveindx2 = j;
                                j = currentList.Count;
                                i = saveindx2 - 1;
                            }
                            else
                            {
                                nextToOr.Add(currentList[j]);
                                if (currentList[j] == '(')
                                    isInside++;
                                else
                                    if (currentList[j] == ')')
                                        isInside--;
                                if(j+1 == currentList.Count)
                                    i = j;
                            }                              // se recupera el resto de la lista no analizada 
                        }
                        temp2 = PosfijaRecurs(nextToOr);  // se manda a recursividad del OR a la derecha
                        temp1 = ValueStack.Pop();         // se recupera izquierda del OR
                        temp1 += temp2;                   // se une la izquierda con la derecha         
                        temp1 += '|';                     // se agrega OR al final para generar un solo valor
                        ValueStack.Push(temp1);           // Todo unido se vuelve guardar     
                        temp1 = "";
                        temp2 = "";
                        break;
                    case ('('):
                        int saveindx = i;
                        List<char> nextToPar = new List<char>();
                        for (int j = i + 1; j < currentList.Count; j++)
                        {
                            if (currentList[j] == ')')
                            {
                                saveindx = j;
                                j = currentList.Count;
                            }
                            else
                                nextToPar.Add(currentList[j]);
                        }                                       // se recupera el resto de la lista no analizada entre ()
                        i = saveindx;                           // se mueve el indice i hasta el ")"
                        temp2 = "("+PosfijaRecurs(nextToPar) + ")";       // se manda a recursividad
                        if (currentList[i] != currentList.Last())
                        {
                            if (currentList[i + 1] == '⁺')
                                temp2 += '⁺';
                            else
                            {
                                if (currentList[i + 1] == 'ᵡ')
                                    temp2 += 'ᵡ';
                            }
                        }                                       // agrega ⁺ o ᵡ despues de los ()
                        if (OperatorStack.Count == 0)
                            ValueStack.Push(temp2);
                        else
                        {
                            if (ValueStack.Count == 0)
                                ValueStack.Push(temp2);
                            else
                            {
                                temp1 = ValueStack.Pop();
                                temp1 += temp2;
                                temp1 += OperatorStack.Pop();
                                ValueStack.Push(temp1);
                            }
                        }
                        temp1 = "";
                        temp2 = "";
                        break;
                    default:
                        string value = currentList[i].ToString();
                        if (currentList[i].ToString() != currentList.Last().ToString())
                        {
                            if (currentList[i + 1] == '⁺')
                                value += '⁺';
                            else
                            {
                                if (currentList[i + 1] == 'ᵡ')
                                    value += 'ᵡ';
                            }
                        }
                        if (OperatorStack.Count == 0)
                            ValueStack.Push(value);
                        else
                        {
                            if (ValueStack.Count == 0)
                                ValueStack.Push(value);
                            else
                            {
                                temp1 = ValueStack.Pop();
                                temp1 += value;
                                temp1 += OperatorStack.Pop();
                                ValueStack.Push(temp1);
                                temp1 = "";
                            }
                        }
                        break;
                }
            }

            temp1 = ValueStack.Pop(); // se recupera cadena final 
            return temp1;
        }

        public async Task<Image> GenerateTommyAsync(string tommyInput)
        {
            List<char> arrL = tommyInput.ToList();  //Elimina Espacios
            for (int it = arrL.Count - 1; it >= 0; it--)
                if (arrL[it] == ' ')
                    arrL.RemoveAt(it);
            for (int it = 0; it < arrL.Count; it++) //Agrega puntos
            {
                if (it + 1 < arrL.Count)
                    if (arrL[it] != '|' && arrL[it] != '(')
                        if (arrL[it + 1] != '|' && arrL[it + 1] != ')' && arrL[it + 1] != '⁺' && arrL[it + 1] != 'ᵡ')
                        {
                            arrL.Insert(it + 1, '.');
                            it++;
                        }
            }
            string Answer = PosfijaRecurs(arrL);
            /****************************************************/
            List<char> LChars = Answer.ToList();
            GraphGen grage = new GraphGen(LChars);
            grage.myGraph= grage.TommyGraphGen(grage.postfija);
            Image img= await grage.DrawMe();
            return img;

        }
    }
}

