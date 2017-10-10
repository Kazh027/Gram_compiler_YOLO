using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GramaticasFormales
{
    public partial class Form1 : Form
    {
        AnalisadorGram AnalisadorG;
        public Form1()
        {
            AnalisadorG = new AnalisadorGram();
            InitializeComponent();
        }

        private void Epsilon_Click(object sender, EventArgs e)
        {
            int Saveindex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, "Ɛ");
            textBox1.SelectionStart = Saveindex + 1;
            textBox1.SelectionLength = 0;
            textBox1.Focus();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            AnalisadorG.FValues.Clear();
        }

        private void Done_Click(object sender, EventArgs e)
        {
            if (textBox1.Lines.Length > 0)
            {
                AnalisadorG.FValues.Clear();
                bool res = AnalisadorG.AnaliseString(textBox1.Lines);
                if (res)
                {
                    MessageBox.Show("Input Error: Wrong Epsilon Position!");

                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show(AnalisadorG.GenerateG());
                }
            }
            else
                MessageBox.Show("Input Error: Write something!!");
            textBox1.Focus();
        }

        private void GClasifButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Lines.Length > 0)
            {
                AnalisadorG.FValues.Clear();
                bool res = AnalisadorG.AnaliseString(textBox1.Lines);
                if (res)
                {
                    MessageBox.Show("Input Error: Wrong Epsilon Position!");
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("The grammatical classification is: " + AnalisadorG.GenerateClassif() + " grammar.");
                }
            }
            else
                MessageBox.Show("Input Error: Write something!!");
        }

        private void GenER_Click(object sender, EventArgs e)
        {
            if (textBox1.Lines.Length > 0)
            {
                AnalisadorG.FValues.Clear();
                bool res = AnalisadorG.AnaliseString(textBox1.Lines);
                if (res)
                {
                    MessageBox.Show("Input Error: Wrong Epsilon Position!");
                    textBox1.Text = "";
                }
                else
                {
                    string ans = AnalisadorG.GenerateClassif();
                    if (ans == "Regular")
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("ER -> ");
                        List<Options> ops = AnalisadorG.GenerateER();
                        foreach (Options it_op in ops)
                        {
                            foreach (Tokens it_tokens in it_op.OptionTokens)
                            {
                                if (it_tokens.hasOperator == 1)
                                    sb.Append("{" + it_tokens.TokenName + "}");
                                else
                                {
                                    sb.Append(it_tokens.TokenName);
                                }
                            }
                            if (it_op != ops.Last())
                                sb.Append("| ");
                        }
                        sb.AppendLine();
                        sb.Append("factoring ER...");
                        sb.AppendLine();
                        for (int i = 0; i < ops.Count; i++)
                        {
                            for (int j = 0; j < ops[i].OptionTokens.Count; j++)
                            {
                                if (ops[i].OptionTokens[j].hasOperator == 1)
                                {
                                    for (int k = 0; k < ops[i].OptionTokens.Count; k++)
                                    {
                                        if (ops[i].OptionTokens[k].TokenName == ops[i].OptionTokens[j].TokenName && ops[i].OptionTokens[k].hasOperator == 0)
                                        {
                                            ops[i].OptionTokens.RemoveAt(k);
                                            k = ops[i].OptionTokens.Count;
                                            ops[i].OptionTokens[j - 1].hasOperator = 2;
                                        }
                                    }
                                }
                            }
                        }
                        sb.Append("ER -> ");
                        foreach (Options it_op in ops)
                        {
                            foreach (Tokens it_tokens in it_op.OptionTokens)
                            {
                                sb.Append(it_tokens.TokenName);
                                if (it_tokens.hasOperator == 1)
                                    sb.Append("ᵡ");
                                if (it_tokens.hasOperator == 2)
                                    sb.Append("⁺");
                            }
                            if (it_op != ops.Last())
                                sb.Append("| ");
                        }
                        MessageBox.Show(sb.ToString());
                    }
                }
            }
            else
                MessageBox.Show("Input Error: Write something!!");
        }

        private void Aumentada_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {

            }
            else
            {
                if (textBox1.Lines.Length > 0)
                {
                    AnalisadorG.FValues.Clear();
                    bool res = AnalisadorG.AnaliseString(textBox1.Lines);
                    if (res)
                    {
                        MessageBox.Show("Input Error: Wrong Epsilon Position!");
                        textBox1.Text = "";
                    }
                    else
                    {
                        string ans = AnalisadorG.GenerateClassif();
                        if (ans == "Regular")
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("ER -> ");
                            List<Options> ops = AnalisadorG.GenerateER();
                            foreach (Options it_op in ops)
                            {
                                foreach (Tokens it_tokens in it_op.OptionTokens)
                                {
                                    if (it_tokens.hasOperator == 1)
                                        sb.Append("{" + it_tokens.TokenName + "}");
                                    else
                                    {
                                        sb.Append(it_tokens.TokenName);
                                    }
                                }
                                if (it_op != ops.Last())
                                    sb.Append("| ");
                            }
                            sb.AppendLine();
                            sb.Append("factoring ER...");
                            sb.AppendLine();
                            for (int i = 0; i < ops.Count; i++)
                            {
                                for (int j = 0; j < ops[i].OptionTokens.Count; j++)
                                {
                                    if (ops[i].OptionTokens[j].hasOperator == 1)
                                    {
                                        for (int k = 0; k < ops[i].OptionTokens.Count; k++)
                                        {
                                            if (ops[i].OptionTokens[k].TokenName == ops[i].OptionTokens[j].TokenName && ops[i].OptionTokens[k].hasOperator == 0)
                                            {
                                                ops[i].OptionTokens.RemoveAt(k);
                                                k = ops[i].OptionTokens.Count;
                                                ops[i].OptionTokens[j - 1].hasOperator = 2;
                                            }
                                        }
                                    }
                                }
                            }
                            //Aumentada
                            string Aum = AnalisadorG.Aumentada(ops);
                            sb.Append(Aum);
                            MessageBox.Show(sb.ToString());
                        }
                    }
                }
                else
                    MessageBox.Show("Input Error: Write something!!");
            }
        }

        private void posfija_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {

            }
            else
            {
                if (textBox1.Lines.Length > 0)
                {
                    AnalisadorG.FValues.Clear();
                    bool res = AnalisadorG.AnaliseString(textBox1.Lines);
                    if (res)
                    {
                        MessageBox.Show("Input Error: Wrong Epsilon Position!");
                        textBox1.Text = "";
                    }
                    else
                    {
                        string ans = AnalisadorG.GenerateClassif();
                        if (ans == "Regular")
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("ER -> ");
                            List<Options> ops = AnalisadorG.GenerateER();
                            foreach (Options it_op in ops)
                            {
                                foreach (Tokens it_tokens in it_op.OptionTokens)
                                {
                                    if (it_tokens.hasOperator == 1)
                                        sb.Append("{" + it_tokens.TokenName + "}");
                                    else
                                    {
                                        sb.Append(it_tokens.TokenName);
                                    }
                                }
                                if (it_op != ops.Last())
                                    sb.Append("| ");
                            }
                            sb.AppendLine();
                            sb.Append("factoring ER...");
                            sb.AppendLine();
                            for (int i = 0; i < ops.Count; i++)
                            {
                                for (int j = 0; j < ops[i].OptionTokens.Count; j++)
                                {
                                    if (ops[i].OptionTokens[j].hasOperator == 1)
                                    {
                                        for (int k = 0; k < ops[i].OptionTokens.Count; k++)
                                        {
                                            if (ops[i].OptionTokens[k].TokenName == ops[i].OptionTokens[j].TokenName && ops[i].OptionTokens[k].hasOperator == 0)
                                            {
                                                ops[i].OptionTokens.RemoveAt(k);
                                                k = ops[i].OptionTokens.Count;
                                                ops[i].OptionTokens[j - 1].hasOperator = 2;
                                            }
                                        }
                                    }
                                }
                            }
                            //Posfija 
                            string posfj = "Pos-Foja -> ";
                            posfj += AnalisadorG.Posfija(ops);
                            sb.Append(posfj);
                            MessageBox.Show(sb.ToString());
                        }
                    }
                }
                else
                    MessageBox.Show("Input Error: Write something!!");
            }
        }

        private void AfnToAfd_Click(object sender, EventArgs e)
        {

        }

        private void Minimizacion_Click(object sender, EventArgs e)
        {

        }

        private void Cleartxt2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void InsertAst_Click(object sender, EventArgs e)
        {
            int Saveindex = textBox2.SelectionStart;
            textBox2.Text = textBox2.Text.Insert(textBox2.SelectionStart, "ᵡ");
            textBox2.SelectionStart = Saveindex + 1;
            textBox2.SelectionLength = 0;
            textBox2.Focus();
        }

        private void InsertPlus_Click(object sender, EventArgs e)
        {
            int Saveindex = textBox2.SelectionStart;
            textBox2.Text = textBox2.Text.Insert(textBox2.SelectionStart, "⁺");
            textBox2.SelectionStart = Saveindex + 1;
            textBox2.SelectionLength = 0;
            textBox2.Focus();
        }

        private async void tommy_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                Image AnsImg = await AnalisadorG.GenerateTommyAsync(textBox2.Text);
                ImgShow ims = new ImgShow(AnsImg);
                ims.Width = AnsImg.Width + 20;
                ims.Height = AnsImg.Height + 40;
                ims.Show();
            }
        }


    }
}
