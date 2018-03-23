namespace Cliente2Hilos
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbSensor1 = new System.Windows.Forms.TextBox();
            this.tbSensorVal1 = new System.Windows.Forms.TextBox();
            this.labelSensor1 = new System.Windows.Forms.Label();
            this.labelSensor1Val = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnDesconectar = new System.Windows.Forms.Button();
            this.btnPrueba = new System.Windows.Forms.Button();
            this.textBoxPrueba = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdelante = new System.Windows.Forms.Button();
            this.tbAvanzar = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbSensor1
            // 
            this.tbSensor1.Location = new System.Drawing.Point(78, 165);
            this.tbSensor1.Name = "tbSensor1";
            this.tbSensor1.Size = new System.Drawing.Size(100, 20);
            this.tbSensor1.TabIndex = 0;
            // 
            // tbSensorVal1
            // 
            this.tbSensorVal1.Location = new System.Drawing.Point(78, 202);
            this.tbSensorVal1.Name = "tbSensorVal1";
            this.tbSensorVal1.Size = new System.Drawing.Size(100, 20);
            this.tbSensorVal1.TabIndex = 1;
            // 
            // labelSensor1
            // 
            this.labelSensor1.AutoSize = true;
            this.labelSensor1.Location = new System.Drawing.Point(5, 168);
            this.labelSensor1.Name = "labelSensor1";
            this.labelSensor1.Size = new System.Drawing.Size(40, 13);
            this.labelSensor1.TabIndex = 2;
            this.labelSensor1.Text = "Sensor";
            // 
            // labelSensor1Val
            // 
            this.labelSensor1Val.AutoSize = true;
            this.labelSensor1Val.Location = new System.Drawing.Point(5, 205);
            this.labelSensor1Val.Name = "labelSensor1Val";
            this.labelSensor1Val.Size = new System.Drawing.Size(31, 13);
            this.labelSensor1Val.TabIndex = 3;
            this.labelSensor1Val.Text = "Valor";
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(92, 39);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(86, 23);
            this.btnConectar.TabIndex = 4;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnDesconectar
            // 
            this.btnDesconectar.Location = new System.Drawing.Point(92, 83);
            this.btnDesconectar.Name = "btnDesconectar";
            this.btnDesconectar.Size = new System.Drawing.Size(86, 23);
            this.btnDesconectar.TabIndex = 5;
            this.btnDesconectar.Text = "Desconectar";
            this.btnDesconectar.UseVisualStyleBackColor = true;
            this.btnDesconectar.Click += new System.EventHandler(this.btnDesconectar_Click);
            // 
            // btnPrueba
            // 
            this.btnPrueba.Location = new System.Drawing.Point(249, 39);
            this.btnPrueba.Name = "btnPrueba";
            this.btnPrueba.Size = new System.Drawing.Size(75, 23);
            this.btnPrueba.TabIndex = 6;
            this.btnPrueba.Text = "Prueba";
            this.btnPrueba.UseVisualStyleBackColor = true;
            this.btnPrueba.Click += new System.EventHandler(this.btnPrueba_Click);
            // 
            // textBoxPrueba
            // 
            this.textBoxPrueba.Location = new System.Drawing.Point(103, 243);
            this.textBoxPrueba.Name = "textBoxPrueba";
            this.textBoxPrueba.Size = new System.Drawing.Size(75, 20);
            this.textBoxPrueba.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Eventos Recibidos";
            // 
            // btnAdelante
            // 
            this.btnAdelante.Location = new System.Drawing.Point(475, 274);
            this.btnAdelante.Name = "btnAdelante";
            this.btnAdelante.Size = new System.Drawing.Size(63, 55);
            this.btnAdelante.TabIndex = 9;
            this.btnAdelante.Text = "Adelante";
            this.btnAdelante.UseVisualStyleBackColor = true;
            this.btnAdelante.Click += new System.EventHandler(this.btnAdelante_Click);
            // 
            // tbAvanzar
            // 
            this.tbAvanzar.Location = new System.Drawing.Point(475, 221);
            this.tbAvanzar.Name = "tbAvanzar";
            this.tbAvanzar.Size = new System.Drawing.Size(63, 20);
            this.tbAvanzar.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(475, 364);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(63, 20);
            this.textBox1.TabIndex = 11;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(558, 292);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(63, 20);
            this.textBox2.TabIndex = 12;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(392, 292);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(63, 20);
            this.textBox3.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 441);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tbAvanzar);
            this.Controls.Add(this.btnAdelante);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPrueba);
            this.Controls.Add(this.btnPrueba);
            this.Controls.Add(this.btnDesconectar);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.labelSensor1Val);
            this.Controls.Add(this.labelSensor1);
            this.Controls.Add(this.tbSensorVal1);
            this.Controls.Add(this.tbSensor1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSensor1;
        private System.Windows.Forms.TextBox tbSensorVal1;
        private System.Windows.Forms.Label labelSensor1;
        private System.Windows.Forms.Label labelSensor1Val;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnDesconectar;
        private System.Windows.Forms.Button btnPrueba;
        private System.Windows.Forms.TextBox textBoxPrueba;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdelante;
        private System.Windows.Forms.TextBox tbAvanzar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
    }
}

