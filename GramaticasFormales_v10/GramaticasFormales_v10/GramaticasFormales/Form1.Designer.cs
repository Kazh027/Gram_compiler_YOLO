namespace GramaticasFormales
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Done = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Epsilon = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.GClasifButton = new System.Windows.Forms.Button();
            this.GenER = new System.Windows.Forms.Button();
            this.Aumentada = new System.Windows.Forms.Button();
            this.posfija = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tommy = new System.Windows.Forms.Button();
            this.AfnToAfd = new System.Windows.Forms.Button();
            this.Minimizacion = new System.Windows.Forms.Button();
            this.Cleartxt2 = new System.Windows.Forms.Button();
            this.InsertAst = new System.Windows.Forms.Button();
            this.InsertPlus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(260, 135);
            this.textBox1.TabIndex = 0;
            // 
            // Done
            // 
            this.Done.BackColor = System.Drawing.SystemColors.Menu;
            this.Done.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Done.Location = new System.Drawing.Point(179, 166);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(93, 23);
            this.Done.TabIndex = 1;
            this.Done.Text = "Generate G";
            this.Done.UseVisualStyleBackColor = false;
            this.Done.Click += new System.EventHandler(this.Done_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Input String ";
            // 
            // Epsilon
            // 
            this.Epsilon.BackColor = System.Drawing.SystemColors.Menu;
            this.Epsilon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Epsilon.Location = new System.Drawing.Point(90, 166);
            this.Epsilon.Name = "Epsilon";
            this.Epsilon.Size = new System.Drawing.Size(43, 23);
            this.Epsilon.TabIndex = 4;
            this.Epsilon.Text = "Ɛ ";
            this.Epsilon.UseVisualStyleBackColor = false;
            this.Epsilon.Click += new System.EventHandler(this.Epsilon_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Menu;
            this.ClearButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearButton.Location = new System.Drawing.Point(12, 166);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(72, 23);
            this.ClearButton.TabIndex = 5;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // GClasifButton
            // 
            this.GClasifButton.BackColor = System.Drawing.SystemColors.Menu;
            this.GClasifButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GClasifButton.Location = new System.Drawing.Point(150, 195);
            this.GClasifButton.Name = "GClasifButton";
            this.GClasifButton.Size = new System.Drawing.Size(122, 23);
            this.GClasifButton.TabIndex = 6;
            this.GClasifButton.Text = "Grammar classification";
            this.GClasifButton.UseVisualStyleBackColor = false;
            this.GClasifButton.Click += new System.EventHandler(this.GClasifButton_Click);
            // 
            // GenER
            // 
            this.GenER.BackColor = System.Drawing.SystemColors.Menu;
            this.GenER.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GenER.Location = new System.Drawing.Point(12, 195);
            this.GenER.Name = "GenER";
            this.GenER.Size = new System.Drawing.Size(79, 23);
            this.GenER.TabIndex = 7;
            this.GenER.Text = "Generate ER";
            this.GenER.UseVisualStyleBackColor = false;
            this.GenER.Click += new System.EventHandler(this.GenER_Click);
            // 
            // Aumentada
            // 
            this.Aumentada.BackColor = System.Drawing.SystemColors.Menu;
            this.Aumentada.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Aumentada.Location = new System.Drawing.Point(193, 224);
            this.Aumentada.Name = "Aumentada";
            this.Aumentada.Size = new System.Drawing.Size(79, 23);
            this.Aumentada.TabIndex = 8;
            this.Aumentada.Text = "Aumentada";
            this.Aumentada.UseVisualStyleBackColor = false;
            this.Aumentada.Click += new System.EventHandler(this.Aumentada_Click);
            // 
            // posfija
            // 
            this.posfija.BackColor = System.Drawing.SystemColors.Menu;
            this.posfija.Cursor = System.Windows.Forms.Cursors.Hand;
            this.posfija.Location = new System.Drawing.Point(12, 224);
            this.posfija.Name = "posfija";
            this.posfija.Size = new System.Drawing.Size(83, 23);
            this.posfija.TabIndex = 9;
            this.posfija.Text = "Pos-fija";
            this.posfija.UseVisualStyleBackColor = false;
            this.posfija.Click += new System.EventHandler(this.posfija_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "ER Input String";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 262);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(261, 20);
            this.textBox2.TabIndex = 11;
            // 
            // tommy
            // 
            this.tommy.BackColor = System.Drawing.SystemColors.Menu;
            this.tommy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tommy.Location = new System.Drawing.Point(12, 312);
            this.tommy.Name = "tommy";
            this.tommy.Size = new System.Drawing.Size(83, 23);
            this.tommy.TabIndex = 12;
            this.tommy.Text = "Thompson";
            this.tommy.UseVisualStyleBackColor = false;
            this.tommy.Click += new System.EventHandler(this.tommy_Click);
            // 
            // AfnToAfd
            // 
            this.AfnToAfd.BackColor = System.Drawing.SystemColors.Menu;
            this.AfnToAfd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AfnToAfd.Location = new System.Drawing.Point(101, 312);
            this.AfnToAfd.Name = "AfnToAfd";
            this.AfnToAfd.Size = new System.Drawing.Size(83, 23);
            this.AfnToAfd.TabIndex = 13;
            this.AfnToAfd.Text = "AFN -> AFD";
            this.AfnToAfd.UseVisualStyleBackColor = false;
            this.AfnToAfd.Click += new System.EventHandler(this.AfnToAfd_Click);
            // 
            // Minimizacion
            // 
            this.Minimizacion.BackColor = System.Drawing.SystemColors.Menu;
            this.Minimizacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Minimizacion.Location = new System.Drawing.Point(190, 312);
            this.Minimizacion.Name = "Minimizacion";
            this.Minimizacion.Size = new System.Drawing.Size(83, 23);
            this.Minimizacion.TabIndex = 14;
            this.Minimizacion.Text = "Minimización";
            this.Minimizacion.UseVisualStyleBackColor = false;
            this.Minimizacion.Click += new System.EventHandler(this.Minimizacion_Click);
            // 
            // Cleartxt2
            // 
            this.Cleartxt2.BackColor = System.Drawing.SystemColors.Menu;
            this.Cleartxt2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cleartxt2.Location = new System.Drawing.Point(12, 288);
            this.Cleartxt2.Name = "Cleartxt2";
            this.Cleartxt2.Size = new System.Drawing.Size(72, 23);
            this.Cleartxt2.TabIndex = 15;
            this.Cleartxt2.Text = "Clear";
            this.Cleartxt2.UseVisualStyleBackColor = false;
            this.Cleartxt2.Click += new System.EventHandler(this.Cleartxt2_Click);
            // 
            // InsertAst
            // 
            this.InsertAst.BackColor = System.Drawing.SystemColors.Menu;
            this.InsertAst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InsertAst.Location = new System.Drawing.Point(180, 288);
            this.InsertAst.Name = "InsertAst";
            this.InsertAst.Size = new System.Drawing.Size(43, 23);
            this.InsertAst.TabIndex = 16;
            this.InsertAst.Text = "aᵡ";
            this.InsertAst.UseVisualStyleBackColor = false;
            this.InsertAst.Click += new System.EventHandler(this.InsertAst_Click);
            // 
            // InsertPlus
            // 
            this.InsertPlus.BackColor = System.Drawing.SystemColors.Menu;
            this.InsertPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InsertPlus.Location = new System.Drawing.Point(229, 288);
            this.InsertPlus.Name = "InsertPlus";
            this.InsertPlus.Size = new System.Drawing.Size(43, 23);
            this.InsertPlus.TabIndex = 17;
            this.InsertPlus.Text = "a⁺";
            this.InsertPlus.UseVisualStyleBackColor = false;
            this.InsertPlus.Click += new System.EventHandler(this.InsertPlus_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 344);
            this.Controls.Add(this.InsertPlus);
            this.Controls.Add(this.InsertAst);
            this.Controls.Add(this.Cleartxt2);
            this.Controls.Add(this.Minimizacion);
            this.Controls.Add(this.AfnToAfd);
            this.Controls.Add(this.tommy);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.posfija);
            this.Controls.Add(this.Aumentada);
            this.Controls.Add(this.GenER);
            this.Controls.Add(this.GClasifButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.Epsilon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.textBox1);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Done;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Epsilon;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button GClasifButton;
        private System.Windows.Forms.Button GenER;
        private System.Windows.Forms.Button Aumentada;
        private System.Windows.Forms.Button posfija;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button tommy;
        private System.Windows.Forms.Button AfnToAfd;
        private System.Windows.Forms.Button Minimizacion;
        private System.Windows.Forms.Button Cleartxt2;
        private System.Windows.Forms.Button InsertAst;
        private System.Windows.Forms.Button InsertPlus;
    }
}

