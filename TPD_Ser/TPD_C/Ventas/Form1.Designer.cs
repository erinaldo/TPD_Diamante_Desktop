namespace TPD_C.Ventas
{
    partial class frmArticuloRemates
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpfecha_ini = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpfecha_fin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbalmacen = new System.Windows.Forms.ComboBox();
            this.cblinea = new System.Windows.Forms.ComboBox();
            this.cmdconsultar = new System.Windows.Forms.Button();
            this.dgvarticulos = new System.Windows.Forms.DataGridView();
            this.cmdexportar = new System.Windows.Forms.Button();
            this.cbarticulos_ventas = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvarticulos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(468, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Articulos de Remates";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha de Inicio:";
            // 
            // dtpfecha_ini
            // 
            this.dtpfecha_ini.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfecha_ini.Location = new System.Drawing.Point(142, 78);
            this.dtpfecha_ini.Name = "dtpfecha_ini";
            this.dtpfecha_ini.Size = new System.Drawing.Size(240, 22);
            this.dtpfecha_ini.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fecha Final:";
            // 
            // dtpfecha_fin
            // 
            this.dtpfecha_fin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfecha_fin.Location = new System.Drawing.Point(142, 111);
            this.dtpfecha_fin.Name = "dtpfecha_fin";
            this.dtpfecha_fin.Size = new System.Drawing.Size(240, 22);
            this.dtpfecha_fin.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(415, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Almacén:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(415, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Linea:";
            // 
            // cbalmacen
            // 
            this.cbalmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbalmacen.FormattingEnabled = true;
            this.cbalmacen.Location = new System.Drawing.Point(485, 78);
            this.cbalmacen.Name = "cbalmacen";
            this.cbalmacen.Size = new System.Drawing.Size(139, 24);
            this.cbalmacen.TabIndex = 7;
            // 
            // cblinea
            // 
            this.cblinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cblinea.FormattingEnabled = true;
            this.cblinea.Location = new System.Drawing.Point(485, 110);
            this.cblinea.Name = "cblinea";
            this.cblinea.Size = new System.Drawing.Size(139, 24);
            this.cblinea.TabIndex = 8;
            // 
            // cmdconsultar
            // 
            this.cmdconsultar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdconsultar.Location = new System.Drawing.Point(829, 78);
            this.cmdconsultar.Name = "cmdconsultar";
            this.cmdconsultar.Size = new System.Drawing.Size(75, 54);
            this.cmdconsultar.TabIndex = 9;
            this.cmdconsultar.Text = "Consultar";
            this.cmdconsultar.UseVisualStyleBackColor = true;
            this.cmdconsultar.Click += new System.EventHandler(this.cmdconsultar_Click);
            // 
            // dgvarticulos
            // 
            this.dgvarticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvarticulos.Location = new System.Drawing.Point(36, 157);
            this.dgvarticulos.Name = "dgvarticulos";
            this.dgvarticulos.Size = new System.Drawing.Size(1216, 460);
            this.dgvarticulos.TabIndex = 10;
            // 
            // cmdexportar
            // 
            this.cmdexportar.BackgroundImage = global::TPD_C.Properties.Resources.Excel2016;
            this.cmdexportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdexportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdexportar.Location = new System.Drawing.Point(923, 78);
            this.cmdexportar.Name = "cmdexportar";
            this.cmdexportar.Size = new System.Drawing.Size(75, 54);
            this.cmdexportar.TabIndex = 11;
            this.cmdexportar.UseVisualStyleBackColor = true;
            this.cmdexportar.Click += new System.EventHandler(this.cmdexportar_Click);
            // 
            // cbarticulos_ventas
            // 
            this.cbarticulos_ventas.AutoSize = true;
            this.cbarticulos_ventas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbarticulos_ventas.Location = new System.Drawing.Point(655, 86);
            this.cbarticulos_ventas.Name = "cbarticulos_ventas";
            this.cbarticulos_ventas.Size = new System.Drawing.Size(148, 20);
            this.cbarticulos_ventas.TabIndex = 12;
            this.cbarticulos_ventas.Text = "Articulos con Ventas";
            this.cbarticulos_ventas.UseVisualStyleBackColor = true;
            // 
            // frmArticuloRemates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 641);
            this.Controls.Add(this.cbarticulos_ventas);
            this.Controls.Add(this.cmdexportar);
            this.Controls.Add(this.dgvarticulos);
            this.Controls.Add(this.cmdconsultar);
            this.Controls.Add(this.cblinea);
            this.Controls.Add(this.cbalmacen);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpfecha_fin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpfecha_ini);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmArticuloRemates";
            this.Text = "Articulos de Remates";
            this.Load += new System.EventHandler(this.frmArticuloRemates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvarticulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpfecha_ini;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpfecha_fin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbalmacen;
        private System.Windows.Forms.ComboBox cblinea;
        private System.Windows.Forms.Button cmdconsultar;
        private System.Windows.Forms.DataGridView dgvarticulos;
        private System.Windows.Forms.Button cmdexportar;
        private System.Windows.Forms.CheckBox cbarticulos_ventas;
    }
}