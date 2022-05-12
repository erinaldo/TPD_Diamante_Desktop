using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPD_C.TOP_Operacion
{
    public partial class comenPedidoDiario : Form
    {
        public comenPedidoDiario()
        {
            InitializeComponent();
        }

        private void comenPedidoDiario_Load(object sender, EventArgs e)
        {
            rbFiltro.Checked = true;
            if (rbFiltro.Checked == true) { 
            CargarComentarios();
            estilo_grid_comentarios(); }
            
            
        }


        public void CargarComentarios()
        {
            conexion.conectar(true);
            SqlCommand cmd = new SqlCommand("consultarComentariosPD", conexion.con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvComentarios.DataSource = dt;
            conexion.cerra_conectar();
        }

        public void CargarComentariosOV()
        {

           
                conexion.conectar(true);
                SqlCommand cmd = new SqlCommand("consultarComentariosPD_OV", conexion.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdenVenta", txtOrdenVenta.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvComentarios.DataSource = dt;
                conexion.cerra_conectar();
            }

        public void CargarComentariosFecha()
        {

            conexion.conectar(true);
            SqlCommand cmd = new SqlCommand("consultarComentariosPD_Fecha", conexion.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fechaInicio", dtpInicio.Value);
            cmd.Parameters.AddWithValue("@fechaFin", dtpFin.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvComentarios.DataSource = dt;
            conexion.cerra_conectar();
        }





        public void estilo_grid_comentarios()
        {
            //ESTILO DEL GRID DE FACTURA
            dgvComentarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite;
            dgvComentarios.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dgvComentarios.DefaultCellStyle.BackColor = Color.AliceBlue;
            dgvComentarios.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dgvComentarios.AllowUserToAddRows = false;

            dgvComentarios.Columns["DocNum"].HeaderText = "Orden de Venta";
            dgvComentarios.Columns["DocNum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["DocNum"].ReadOnly = true;

            dgvComentarios.Columns["Fecha"].HeaderText = "Fecha Orden de Venta";
            dgvComentarios.Columns["Fecha"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["Fecha"].ReadOnly = true;

            dgvComentarios.Columns["LineNum"].HeaderText = "Partidas";
            dgvComentarios.Columns["LineNum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["LineNum"].ReadOnly = true;

            dgvComentarios.Columns["ItemCode"].HeaderText = "Código Artículo";
            dgvComentarios.Columns["ItemCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["ItemCode"].ReadOnly = true;

            dgvComentarios.Columns["Description"].HeaderText = "Artículo";
            dgvComentarios.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["Description"].ReadOnly = true;

            dgvComentarios.Columns["Quantity"].HeaderText = "Solicitado";
            dgvComentarios.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["Quantity"].ReadOnly = true;

            dgvComentarios.Columns["Surtido"].HeaderText = "Surtido";
            dgvComentarios.Columns["Surtido"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["Surtido"].ReadOnly = true;

            dgvComentarios.Columns["Box"].HeaderText = "Cajas";
            dgvComentarios.Columns["Box"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["Box"].ReadOnly = true;

            dgvComentarios.Columns["PesoBox"].HeaderText = "Peso Cajas";
            dgvComentarios.Columns["PesoBox"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["PesoBox"].ReadOnly = true;

            dgvComentarios.Columns["Price"].HeaderText = "Precio";
            dgvComentarios.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["Price"].ReadOnly = true;
            dgvComentarios.Columns["Price"].DefaultCellStyle.Format = "$ ###,###,##0.#0";


            dgvComentarios.Columns["LineTotal"].HeaderText = "Total";
            dgvComentarios.Columns["LineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["LineTotal"].ReadOnly = true;
            dgvComentarios.Columns["LineTotal"].DefaultCellStyle.Format = "$ ###,###,##0.#0";

            dgvComentarios.Columns["EscaneadoParaSurtido"].HeaderText = "Escaneado";
            dgvComentarios.Columns["EscaneadoParaSurtido"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["EscaneadoParaSurtido"].ReadOnly = true;

            dgvComentarios.Columns["ComentarioErrorSurtido"].HeaderText = "Comentario";
            dgvComentarios.Columns["ComentarioErrorSurtido"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvComentarios.Columns["ComentarioErrorSurtido"].ReadOnly = true;

           


            dgvComentarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

       

        private void rbFiltro_CheckedChanged(object sender, EventArgs e)
        {
           // CargarComentariosOV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rbOrdenVenta.Checked == true)
            {
                CargarComentariosOV(); }
            else if (rbFecha.Checked == true)
            {
                CargarComentariosFecha();
                            }
            else
            {
                CargarComentarios();
            }
        }
    }
}
