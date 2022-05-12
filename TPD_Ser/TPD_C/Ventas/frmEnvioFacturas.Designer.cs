namespace TPD_C.Ventas
{
    partial class frmEnvioFacturas
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
            this.pfacturas = new System.Windows.Forms.Panel();
            this.txtE_Mail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpfecha_fin = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpfecha_ini = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCardCode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbTipoDoc = new System.Windows.Forms.ComboBox();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Mail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EDocNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pfacturas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(681, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Envio de facturas";
            // 
            // pfacturas
            // 
            this.pfacturas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pfacturas.Controls.Add(this.cmbTipoDoc);
            this.pfacturas.Controls.Add(this.label8);
            this.pfacturas.Controls.Add(this.txtE_Mail);
            this.pfacturas.Controls.Add(this.label7);
            this.pfacturas.Controls.Add(this.dgvFacturas);
            this.pfacturas.Controls.Add(this.groupBox1);
            this.pfacturas.Controls.Add(this.dtpfecha_fin);
            this.pfacturas.Controls.Add(this.label6);
            this.pfacturas.Controls.Add(this.dtpfecha_ini);
            this.pfacturas.Controls.Add(this.label2);
            this.pfacturas.Controls.Add(this.cmbCardCode);
            this.pfacturas.Controls.Add(this.label4);
            this.pfacturas.Controls.Add(this.label3);
            this.pfacturas.Controls.Add(this.label5);
            this.pfacturas.Location = new System.Drawing.Point(12, 50);
            this.pfacturas.Name = "pfacturas";
            this.pfacturas.Size = new System.Drawing.Size(776, 511);
            this.pfacturas.TabIndex = 1;
            // 
            // txtE_Mail
            // 
            this.txtE_Mail.Enabled = false;
            this.txtE_Mail.Location = new System.Drawing.Point(491, 198);
            this.txtE_Mail.Name = "txtE_Mail";
            this.txtE_Mail.Size = new System.Drawing.Size(267, 20);
            this.txtE_Mail.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(389, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Correo electrónico:";
            // 
            // dgvFacturas
            // 
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.Tipo,
            this.DocNum,
            this.DocDate,
            this.CardCode,
            this.CardName,
            this.DocTotal,
            this.E_Mail,
            this.DocEntry,
            this.EDocNum});
            this.dgvFacturas.Location = new System.Drawing.Point(20, 234);
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.RowHeadersWidth = 20;
            this.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas.Size = new System.Drawing.Size(738, 263);
            this.dgvFacturas.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(392, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 68);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Acciones";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.Control;
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(249, 20);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 34);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Enviar";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.SystemColors.Control;
            this.btnNew.Enabled = false;
            this.btnNew.Location = new System.Drawing.Point(151, 20);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 34);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "Nueva busqueda";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.Location = new System.Drawing.Point(43, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 34);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpfecha_fin
            // 
            this.dtpfecha_fin.Location = new System.Drawing.Point(109, 195);
            this.dtpfecha_fin.Name = "dtpfecha_fin";
            this.dtpfecha_fin.Size = new System.Drawing.Size(211, 20);
            this.dtpfecha_fin.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Fecha final:";
            // 
            // dtpfecha_ini
            // 
            this.dtpfecha_ini.Location = new System.Drawing.Point(109, 163);
            this.dtpfecha_ini.Name = "dtpfecha_ini";
            this.dtpfecha_ini.Size = new System.Drawing.Size(211, 20);
            this.dtpfecha_ini.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Fecha inicial:";
            // 
            // cmbCardCode
            // 
            this.cmbCardCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCardCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbCardCode.FormattingEnabled = true;
            this.cmbCardCode.Location = new System.Drawing.Point(20, 128);
            this.cmbCardCode.Name = "cmbCardCode";
            this.cmbCardCode.Size = new System.Drawing.Size(300, 21);
            this.cmbCardCode.TabIndex = 4;
            this.cmbCardCode.DropDown += new System.EventHandler(this.cmbCardCode_DropDown);
            this.cmbCardCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbCardCode_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Busqueda del Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Cliente:";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label5.Location = new System.Drawing.Point(17, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(741, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "_________________________________________________________________________________" +
    "____________________________________________________";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Tipo de documento:";
            // 
            // cmbTipoDoc
            // 
            this.cmbTipoDoc.FormattingEnabled = true;
            this.cmbTipoDoc.Items.AddRange(new object[] {
            "FACTURA",
            "NOTA DE CREDITO",
            "PAGO",
            "TODOS"});
            this.cmbTipoDoc.Location = new System.Drawing.Point(125, 60);
            this.cmbTipoDoc.Name = "cmbTipoDoc";
            this.cmbTipoDoc.Size = new System.Drawing.Size(195, 21);
            this.cmbTipoDoc.TabIndex = 14;
            // 
            // Select
            // 
            this.Select.Frozen = true;
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Width = 25;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.Width = 50;
            // 
            // DocNum
            // 
            this.DocNum.HeaderText = "Factura";
            this.DocNum.Name = "DocNum";
            this.DocNum.ReadOnly = true;
            // 
            // DocDate
            // 
            this.DocDate.HeaderText = "Fecha";
            this.DocDate.Name = "DocDate";
            this.DocDate.ReadOnly = true;
            // 
            // CardCode
            // 
            this.CardCode.HeaderText = "Cliente";
            this.CardCode.Name = "CardCode";
            this.CardCode.ReadOnly = true;
            // 
            // CardName
            // 
            this.CardName.HeaderText = "Nombre";
            this.CardName.Name = "CardName";
            this.CardName.ReadOnly = true;
            this.CardName.Width = 265;
            // 
            // DocTotal
            // 
            this.DocTotal.HeaderText = "Total";
            this.DocTotal.Name = "DocTotal";
            this.DocTotal.ReadOnly = true;
            this.DocTotal.Width = 80;
            // 
            // E_Mail
            // 
            this.E_Mail.HeaderText = "Correo";
            this.E_Mail.Name = "E_Mail";
            this.E_Mail.ReadOnly = true;
            this.E_Mail.Width = 180;
            // 
            // DocEntry
            // 
            this.DocEntry.HeaderText = "DocEntry";
            this.DocEntry.Name = "DocEntry";
            this.DocEntry.ReadOnly = true;
            // 
            // EDocNum
            // 
            this.EDocNum.HeaderText = "EDocNum";
            this.EDocNum.Name = "EDocNum";
            this.EDocNum.ReadOnly = true;
            // 
            // frmEnvioFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 573);
            this.Controls.Add(this.pfacturas);
            this.Controls.Add(this.label1);
            this.Name = "frmEnvioFacturas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Envio de facturas por Cliente";
            this.Load += new System.EventHandler(this.frmEnvioFacturas_Load);
            this.pfacturas.ResumeLayout(false);
            this.pfacturas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pfacturas;
        private System.Windows.Forms.DateTimePicker dtpfecha_fin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpfecha_ini;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCardCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.TextBox txtE_Mail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTipoDoc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Mail;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocEntry;
        private System.Windows.Forms.DataGridViewTextBoxColumn EDocNum;
    }
}