using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;

using System.Reflection;



namespace TPD_C.TPD_Operacion
{
    public partial class AdminUbicaciones : Form
    {
        public AdminUbicaciones()
        {
            InitializeComponent();
        }
        DataView DVArticulos = new DataView();



        private void AdminUbicaciones_Load(object sender, EventArgs e)
        {
        


            CargarUbicaciones();
            estilo_grid_factura();

        }

        //Valida si el registro existe

            public Boolean validaRegistro(String ubicacion)
        {
            Boolean resultado = false;
            try
            {
                conexion.con.Open();
                String valida = "Select * from TPM.DBO.Articulos_Ubicaciones_Orden where BLQ_SCC_RCK= '" + txtBloque.Text + txtSeccion.Text + txtRack.Text + "'";
                conexion.conectar(true);
                SqlCommand cmdValida = new SqlCommand(valida, conexion.con);
                SqlDataReader dr = cmdValida.ExecuteReader();
                
                if (dr.Read())
                {
                    resultado = true;

                }
                dr.Close();
                conexion.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el procedimiento:" + ex.ToString());
            }

            return resultado;
        }



        public void CargarUbicaciones() {
            String consulta = "select  *  from TPM.DBO.Articulos_Ubicaciones_Orden order by Orden asc";
            conexion.conectar();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, conexion.con);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dgvUbicaciones.DataSource = ds.Tables[0].DefaultView;
            conexion.cerra_conectar();
        }

        public void IngresarUbicacion()
        {
            Boolean bandera = false;

            int posicion = Int32.Parse(txtPosicion.Text);

            foreach (DataGridViewRow row in dgvUbicaciones.Rows)
            {
                string UbicacionAux;
                int aux;
                UbicacionAux = (row.Cells["BLQ_SCC_RCK"].Value.ToString());
                aux = Convert.ToInt32(row.Cells["Orden"].Value);


                if (bandera == false)
                {
                    if (aux == posicion)
                    {

                        conexion.con.Open();
                        bandera = true;
                        String cadena = "INSERT into TPM.dbo.Articulos_Ubicaciones_Orden (BLQ_SCC_RCK,Orden)  values (@Ubicacion, @Orden)";
                        conexion.conectar(true);
                        SqlCommand cmd = new SqlCommand(cadena, conexion.con);
                        cmd.Parameters.AddWithValue("@Ubicacion", txtBloque.Text + txtSeccion.Text + txtRack.Text);
                        cmd.Parameters.AddWithValue("@Orden", posicion);
                        cmd.ExecuteNonQuery();
                        txtBloque.Clear();
                        txtSeccion.Clear();
                        txtRack.Clear();
                        txtPosicion.Clear();


                        String cadena2 = "UPDATE tpm.dbo.Articulos_Ubicaciones_Orden SET Orden = @Orden2 WHERE  BLQ_SCC_RCK= @Ubicacion2";

                        //conexion.conectar(true);
                        SqlCommand cmd2 = new SqlCommand(cadena2, conexion.con);
                        cmd2.Parameters.AddWithValue("@Ubicacion2", UbicacionAux);
                        cmd2.Parameters.AddWithValue("@Orden2", aux + 1);
                        cmd2.ExecuteNonQuery();
                        conexion.con.Close();
                        
                    }
                }
                else
                {
                    conexion.con.Open();
                    String cadena = "UPDATE tpm.dbo.Articulos_Ubicaciones_Orden SET Orden = @Orden3 WHERE  BLQ_SCC_RCK= @Ubicacion3";

                    conexion.conectar(true);
                    SqlCommand cmd = new SqlCommand(cadena, conexion.con);
                    cmd.Parameters.AddWithValue("@Ubicacion3", UbicacionAux);
                    cmd.Parameters.AddWithValue("@Orden3", aux + 1);
                    cmd.CommandTimeout = 999999999;
                    cmd.ExecuteNonQuery();
                    conexion.con.Close();
                }
                

            }
        }


        public void OrdenarUbicaciones()
        {
            int t,u,v,w;
            for (int fila = 1; fila < dgvUbicaciones.Rows.Count; fila++)
            {

                for (int col = 1; col < dgvUbicaciones.Rows[fila].Cells.Count; col++)
                {
                    //MessageBox.Show((dgvUbicaciones.Rows[col].ToString()));

                    u = Convert.ToInt32( dgvUbicaciones.Rows[fila-1].Cells[col].Value);
                    v = Convert.ToInt32(dgvUbicaciones.Rows[fila].Cells[col].Value);

                    if (u > v )
                    {
                        t = Convert.ToInt32(dgvUbicaciones.Rows[fila - 1].Cells[col].Value);

                        w = Convert.ToInt32 (dgvUbicaciones.Rows[fila].Cells[col].Value);


                        dgvUbicaciones.Rows[fila - 1].Cells[col].Value = w;

                        dgvUbicaciones.Rows[fila].Cells[col].Value = t;

                        //dgvUbicaciones.Rows[col] = t;
                    }


                    //int valor = Convert.ToInt32(dgvUbicaciones.Rows[fila].Cells[col].Value.ToString());
                    //MessageBox.Show(valor.ToString());                }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if ((txtBloque.Text.Equals("")== false) && (txtBloque.Text.Equals("") == false) && (txtBloque.Text.Equals("") == false)){


                if (validaRegistro(txtBloque.Text + txtSeccion.Text + txtRack.Text) == false)
                {
                    IngresarUbicacion();
                    
                    CargarUbicaciones();
                }
                else {
                    MessageBox.Show("El registro ya existe");
                }

            }
            
                }


        private void Insertar()
        {
            String cadena = "INSERT into TPM.dbo.Articulos_Ubicaciones_Orden (BLQ_SCC_RCK,Orden)  values (@Ubicacion, @Orden)";
            conexion.conectar(true);
            SqlCommand cmd = new SqlCommand(cadena, conexion.con);
            cmd.Parameters.AddWithValue("@Ubicacion", txtBloque.Text + txtSeccion.Text + txtRack.Text);
            cmd.Parameters.AddWithValue("@Orden", Int32.Parse(txtPosicion.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Insertado");
        }
        
        private void button4_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show ("¿Realmente desea eliminar este registro?","Eliminar registro",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                conexion.con.Open();
                string variable = (dgvUbicaciones.CurrentRow.Cells["BLQ_SCC_RCK"].Value.ToString());
                String cadenaEliminar = "delete from Articulos_Ubicaciones_Orden where BLQ_SCC_RCK = @Ubicacion";
                conexion.conectar(true);
                SqlCommand cmdE = new SqlCommand(cadenaEliminar,conexion.con);
                cmdE.Parameters.AddWithValue("@Ubicacion",variable);
                cmdE.ExecuteNonQuery();
                conexion.con.Close();
                int posicionAux = Convert.ToInt32(dgvUbicaciones.CurrentRow.Cells["Orden"].Value.ToString());
                CargarUbicaciones();
                

                 foreach (DataGridViewRow row in dgvUbicaciones.Rows)
                    
                {
                    string UbicacionAux2= (row.Cells["BLQ_SCC_RCK"].Value.ToString());


                    int aux2 = Convert.ToInt32(row.Cells["Orden"].Value);
                    int prueba = row.Index+1;

                    if ((prueba) >= posicionAux)
                    {
                        conexion.con.Open();
                        String cadenaU = "UPDATE tpm.dbo.Articulos_Ubicaciones_Orden SET Orden = @Orden4 WHERE  BLQ_SCC_RCK= @Ubicacion4";

                        conexion.conectar(true);
                        SqlCommand cmdU = new SqlCommand(cadenaU, conexion.con);
                        cmdU.Parameters.AddWithValue("@Ubicacion4", UbicacionAux2);
                        cmdU.Parameters.AddWithValue("@Orden4", aux2 - 1);
                      
                        cmdU.ExecuteNonQuery();
                        conexion.con.Close();

                    }

                }

                CargarUbicaciones();
            }

        }

        public void estilo_grid_factura()
        {
            //ESTILO DEL GRID DE FACTURA
            dgvUbicaciones.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite;
            dgvUbicaciones.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dgvUbicaciones.DefaultCellStyle.BackColor = Color.AliceBlue;
            dgvUbicaciones.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dgvUbicaciones.AllowUserToAddRows = false;
            
            dgvUbicaciones.Columns["BLQ_SCC_RCK"].HeaderText = "UBICACIÓN";
            
            dgvUbicaciones.Columns["BLQ_SCC_RCK"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvUbicaciones.Columns["BLQ_SCC_RCK"].ReadOnly = true;
            //FACTURA
            dgvUbicaciones.Columns["Orden"].HeaderText = "POSICIÓN";
            dgvUbicaciones.Columns["Orden"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvUbicaciones.Columns["Orden"].ReadOnly = true;

            dgvUbicaciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
     
        }




        private void button3_Click(object sender, EventArgs e)
        {
            if (rbUbicacion.Checked == true)
            {
                
                txtPosicion.Enabled = false;
                String cadenaBuscar = ("Select * from TPM.DBO.Articulos_Ubicaciones_Orden where BLQ_SCC_RCK = @Ubicacion ");
                SqlCommand cmdBuscar = new SqlCommand(cadenaBuscar, conexion.con);
                cmdBuscar.Parameters.AddWithValue("@Ubicacion",txtUbi.Text);
                conexion.con.Open();
                SqlDataReader registro = cmdBuscar.ExecuteReader();
                if (registro.Read())
                {
                    txtPos.Text = registro["Orden"].ToString();
                }
                conexion.con.Close();


            } else if (rbPosicion.Checked == true)
            {
                txtPosicion.Enabled = false;
                String cadenaBuscar = ("Select * from TPM.DBO.Articulos_Ubicaciones_Orden where Orden= @Posicion ");
                SqlCommand cmdBuscar = new SqlCommand(cadenaBuscar, conexion.con);
                cmdBuscar.Parameters.AddWithValue("@Posicion", txtPos.Text);
                conexion.con.Open();
                SqlDataReader registro2 = cmdBuscar.ExecuteReader();
                if (registro2.Read())
                {
                    txtUbi.Text = registro2["BLQ_SCC_RCK"].ToString();
                }
                conexion.con.Close();


            }
        }

        private void rbUbicacion_CheckedChanged(object sender, EventArgs e)
        {
            txtPosicion.Enabled = false;
            
        }


        //public void ExportToExcel(DataGridView dgView, ProgressBar pBar)
        //{

        //    try
        //    {
        //        if (pBar != null)
        //        {
        //            pBar.Maximum = dgView.RowCount;
        //            pBar.Value = 0;
        //            if (!pBar.Visible) pBar.Visible = true;
        //        }
        //        string sFont = "Verdana";
        //        int iSize = 8;
        //        //CREACIÓN DE LOS OBJETOS DE EXCEL
        //        Excel.Application xlsApp = new Excel.Application();
        //        Excel.Worksheet xlsSheet;
        //        Excel.Workbook xlsBook;
        //        //AGREGAMOS EL LIBRO Y HOJA DE EXCEL
        //        xlsBook = xlsApp.Workbooks.Add(true);
        //        xlsSheet = (Excel.Worksheet)xlsBook.ActiveSheet;
        //        //ESPECIFICAMOS EL TIPO DE LETRA Y TAMAÑO DE LA LETRA DEL LIBRO
        //        xlsSheet.Rows.Cells.Font.Size = iSize;
        //        xlsSheet.Rows.Cells.Font.Name = sFont;
        //        //AGREGAMOS LOS ENCABEZADOS
        //        int iFil = 0, iCol = 0;
        //        foreach (DataGridViewColumn column in dgView.Columns)
        //            if (column.Visible)
        //                xlsSheet.Cells[1, ++iCol] = column.HeaderText;
        //        //MARCAMOS LAS CELDAS DEL ENCABEZADO EN NEGRITA Y EN COLOR DE RELLENO GRIS
        //        //xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dgView.ColumnCount]).Font.Bold = true;
        //        xlsSheet.Range["A1"].Value2 = "Range 1";
        //        xlsSheet.Range["B1"].Value2 = "Range 2";

        //        // The following line of code specifies multiple cells.
        //        xlsSheet.Range["A2", "B2"].Value2 = "Range 2";

        //        // The following line of code uses an Excel.Range for 
        //        // the second parameter of the Range property.
        //        Excel.Range range1 = xlsSheet.Range["C8"];


        //        //xlsSheet.Range(xlsSheet.Cells["A3", "B4"], xlsSheet.Cells[1, dgView.ColumnCount]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
        //        //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
        //        Excel.Range r;
        //        Color c;
        //        for (iFil = 0; iFil < dgView.RowCount; iFil++)
        //        {
        //            for (iCol = 0; iCol < dgView.ColumnCount; iCol++)
        //            {
        //                xlsSheet.Cells[iFil + 2, iCol + 1] = dgView.Rows[iFil].Cells[iCol].Value.ToString();
        //                c = dgView.Rows[iFil].Cells[iCol].Style.BackColor;
        //                if (!c.IsEmpty)
        //                {// COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
        //                    r = (Excel.Range)xlsSheet.Cells[iFil + 2, iCol + 1];
        //                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[iFil].Cells[iCol].Style.BackColor);
        //                }
        //            }
        //            pBar.Value += 1;
        //        }
        //        xlsSheet.Columns.AutoFit();
        //        xlsSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
        //        xlsSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;
        //        xlsSheet.PageSetup.Zoom = 80;

        //        Excel.Range rango = xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[dgView.RowCount + 1, dgView.ColumnCount]);
        //        rango.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
        //        rango.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
        //        //rango.Cells.AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatList1, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
        //        xlsApp.Visible = true;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString());
        //    }
        //    finally
        //    {
        //        if (pBar != null)
        //        {
        //            pBar.Value = 0;
        //            pBar.Visible = false;
        //        }
        //    }
        //}

        //private void ExportarDataGridViewExcel(DataGridView grd)
        //{
        //    SaveFileDialog fichero = new SaveFileDialog();
        //    fichero.Filter = "Excel (*.xls)|*.xls";
        //    if (fichero.ShowDialog() == DialogResult.OK)
        //    {
        //        Microsoft.Office.Interop.Excel.Application aplicacion;
        //        Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
        //        Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
        //        aplicacion = new Microsoft.Office.Interop.Excel.Application();
        //        libros_trabajo = aplicacion.Workbooks.Add();
        //        hoja_trabajo =
        //            (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
        //        //Recorremos el DataGridView rellenando la hoja de trabajo
        //        for (int i = 0; i < grd.Rows.Count - 1; i++)
        //        {
        //            for (int j = 0; j < grd.Columns.Count; j++)
        //            {
        //                hoja_trabajo.Cells[i + 1, j + 1] = grd.Rows[i].Cells[j].Value.ToString();
        //            }
        //        }
        //        libros_trabajo.SaveAs(fichero.FileName,
        //            Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
        //        libros_trabajo.Close(true);
        //        aplicacion.Quit();
        //    }
        //}



        private void button5_Click(object sender, EventArgs e)
        {
            Exportar();
        }

        public void Exportar() {
            //Creating DataTable

            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in dgvUbicaciones.Columns)
            {
                dt.Columns.Add(column.HeaderText, column.ValueType);
            }

            //Adding the Rows
            foreach (DataGridViewRow row in dgvUbicaciones.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                }
            }

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Excel|*.xlsx";
            guardar.Title = "Save Excel File";
            guardar.FileName = "Export_" + dgvUbicaciones.Name.ToString() + ".xlsx";
            guardar.ShowDialog();
            guardar.InitialDirectory = "C:/";


         

            String strFileName = guardar.FileName;
            Boolean bl = false;
             



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Ubicaciones");
                wb.SaveAs(strFileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtBloque.Enabled = true;
            txtPosicion.Enabled = true;
            txtRack.Enabled = true;
            txtPosicion.Enabled = true;
            txtBloque.Clear();
            txtSeccion.Clear();
            txtRack.Clear();
            txtPosicion.Clear();
        }
    }
}
    

    


