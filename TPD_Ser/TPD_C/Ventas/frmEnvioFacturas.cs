  using System;
  using System.Data;
  using System.Drawing;
  using System.Windows.Forms;
  using System.Data.SqlClient;
  using CrystalDecisions.CrystalReports.Engine;
  using CrystalDecisions.Shared;
  using System.Net.Mail;
  using System.Net;
  using System.IO;

  //private System.Windows.Forms.DataGridViewCheckBoxColumn Select;

  namespace TPD_C.Ventas
  {
  public partial class frmEnvioFacturas : Form
  {
  private enum TipoDoctos
  {
    Facturas = 1,
    Notas_Credito = 2,
    pagos = 3
  }

  public frmEnvioFacturas()
      {
          InitializeComponent();
      }
      DataView DVCliente = new DataView();
      //VARIABLE PARA LEER EL CARACTER DEL COMBO
      string Str;

      //VARIABLE BANDERA PARA VER SI SE MANDA CORREO O SI SE CREA EL PDF
      Boolean createPDF_OK = false;
      Boolean send_OK = false;
      //VARIABLE QUE VALIDA SI HAY UNA FACTURA SELECCFIONADA O NO
      Boolean VMarcada = false;

      //CODIGO GENRAL =======================================================================================================INICIO

      #region Codigo General

      //CARGADO 
      private void frmEnvioFacturas_Load(object sender, EventArgs e)
      {
          //MANDA A LLENAR EL COMBO DE CLIENTES
          MLlenaClientes();
          //MANDA A LLAMAR AL METODO DE ESTILO DEL GRID DE FACTURA
          estilo_grid_factura();
          //COLOCA EL COMBO EN BLANCO PARA NO MOSTRAR EL PRIMER CLIENTE
          cmbTipoDoc.SelectedIndex = 0;
          cmbCardCode.SelectedIndex = -1;
      }

        //Funcion para devolver el fragmento del query para poder buscar XML de algun docto, factura, Pago o Nota de credito
        private string GetRutaXML(TipoDoctos TipoDocto) {
        //A partir del 2019-02-13 se modifico la ruta de los XML, lo mismo con respecto al formato del archivo XML
        String SrcObjType = "";
        String CarpetaAnterior = "";
        String CarpetaNueva = "";
        String Tabla = "";

        if (TipoDocto == TipoDoctos.Facturas) {
          SrcObjType = "13";
          CarpetaAnterior = " + case when T0.ObjType = 13 then  'IN\\' else 'CM\\' END +";
          CarpetaNueva = "INV";
          Tabla = "OINV";
        }
        if (TipoDocto == TipoDoctos.Notas_Credito) {
          SrcObjType = "14";
          CarpetaAnterior = " + case when T0.ObjType = 14 then  'CM\\' else 'IN\\' END +";
          CarpetaNueva = "RIN";
          Tabla = "ORIN";
        }
        if (TipoDocto == TipoDoctos.pagos) {
          SrcObjType = "24";
          CarpetaAnterior = " + case when T0.ObjType = 24 then  'RC\\' else 'IN\\' END +";
          CarpetaNueva = "RCT";
          Tabla = "ORCT";
        }

        String ObtenRutaXML = " CASE WHEN T0.CreateDate < '2019-02-13' OR T0.CreateDate > '2020-07-27' THEN " +
                              " substring(convert(nvarchar(4), T0.CreateDate, 112), 1, 5) + '-' + substring(convert(nvarchar(6), T0.CreateDate, 112), 5, 7) + '\\' + T0.CardCode + '\\' " +
                              CarpetaAnterior +
                              " (SELECT eccc2.ReportID FROM SBO_TPD.dbo.ECM2 eccc2 WHERE eccc2.SrcObjAbs = T0.DocEntry and eccc2.SrcObjType = " + SrcObjType + ")" +
                              " ELSE" +
                              " '" + CarpetaNueva + "' + '\\' + convert(varchar(4), year(T0.CreateDate)) + '\\'" +
                              " + convert(varchar(2), month(T0.CreateDate)) + '\\'" +
                              " + convert(varchar(2), day(T0.CreateDate)) + '\\'" +
                              " + (SELECT series.SeriesName + convert(varchar, docto.DocNum) as ReportID FROM SBO_TPD.dbo." + Tabla + " docto INNER JOIN SBO_TPD.dbo.NNM1 series ON docto.Series = series.Series WHERE docto.DocNum = T0.DocNum and docto.ObjType = " + SrcObjType + ")" +
                              " END" +
                              " + '.xml' as 'EDocNum'";

        return ObtenRutaXML;
        }

      //BOTON DE BUSQUEDA FACTURA POR CLIENTE.
      private void btnSearch_Click(object sender, EventArgs e)
      {
          //DECLARACIÓN DE VARIABLE PARA ALMACENAR LA CONSULTA
          string SQLFactura = "";
          //VARIABLE PARA SABER SI SE ENCONTRARON VALORES O NO
          Boolean VDatos_OK = false;
          //DECLRACION DE VARIABLES PARA ALMACEAMIENTO DE FECHAS
          String fi = "";
          String ff = "";

          //ALMACENAN LA FECHA OBTENIDA EN EL FORMULARIO          
          fi = dtpfecha_ini.Value.ToString("yyyy-MM-dd");
          ff = dtpfecha_fin.Value.ToString("yyyy-MM-dd");

          //VARIABLES DE TIPO FECHA
          DateTime fecha_ini, fecha_fin;
          //CONVIERTE A TIPO FECHA PARA PORDER VALIDAR RANGOS        
          fecha_ini = Convert.ToDateTime(fi).Date;
          fecha_fin = Convert.ToDateTime(ff).Date;

          //REFRESCA EL DATAGRID VIEW DE RESULTADO
          if (dgvFacturas.RowCount > 0) {
              dgvFacturas.Rows.Clear();
          }

          //VALIDA QUE SE HAYA SELECCIONADO UN TIPO DE DOCUMENTO
          if (cmbTipoDoc.SelectedIndex == -1)
          {
              MessageBox.Show("Favor de seleccionar un tipo de documento. ", "Alerta de selección", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              cmbTipoDoc.Focus();
              return;
          }

          //VALIDA QUE SE HAYA SELECCIONADO UN CLIENTE
          if (cmbCardCode.SelectedIndex == -1)
          {
              MessageBox.Show("Favor de seleccionar un cliente. ", "Alerta de selección", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              cmbCardCode.Focus();
              return;
          }
          //VALIDA QUE LA FECHA INCIAL NO SEA MAYOY QUE LA FINAL
          if (fecha_ini > fecha_fin)
          {
              MessageBox.Show("La fecha Inicial no puede ser mayor que la Final. ", "Alerta de captura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              dtpfecha_ini.Focus();
              return;
          }
          //ALMACENA LA CONSULTA
          //VALIDA QUE CONSULTE TODOS LOS TIPOS DE DOCUMENTO

            

          if (cmbTipoDoc.SelectedIndex == 3) {
              //OBTIENE LOS XML DE FACTURAS
              SQLFactura = "SELECT 'F' as Tipo, T0.DocNum, ISNULL(CONVERT(varchar(35), FORMAT(T0.DocDate, 'yyyy-MM-dd'), 126),'') as DocDate, T0.CardCode, T0.CardName, CONVERT(varchar(50), CONVERT(MONEY, DocTotal), 1) as DocTotal, T1.E_Mail, T0.DocEntry, " +
              GetRutaXML(TipoDoctos.Facturas) +
              "FROM OINV T0 INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode " +
              "WHERE T0.DocDate BETWEEN '" + fi + "' AND '" + ff + "' " +
              "AND T0.CardCode = '" + cmbCardCode.SelectedValue + "' ";

              SQLFactura += "UNION ALL ";
              //OBTIENE LOS XML DE LAS NC
              SQLFactura += "SELECT 'NC' as Tipo, T0.DocNum, ISNULL(CONVERT(varchar(35), FORMAT(T0.DocDate, 'yyyy-MM-dd'), 126),'') as DocDate, T0.CardCode, T0.CardName, CONVERT(varchar(50), CONVERT(MONEY, DocTotal), 1) as DocTotal, T1.E_Mail, T0.DocEntry, ";
              //SQLFactura += "substring(convert(nvarchar(4),T0.DocDate ,112),1,5) + '-' + substring(convert(nvarchar(6),T0.DocDate,112),5,7) + '\\' + ";
              //SQLFactura += "T0.CardCode + '\\' + case when T0.ObjType = 14 then  'CM\\' else 'IN\\' END + ";
              //SQLFactura += "(select eccc2.ReportID from SBO_TPD.dbo.ECM2 eccc2 where eccc2.SrcObjAbs = T0.DocEntry and eccc2.SrcObjType = 14) + '.xml' as 'EDocNum' ";
              SQLFactura += GetRutaXML(TipoDoctos.Notas_Credito);
              SQLFactura += "FROM ORIN T0 INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode ";
              SQLFactura += "WHERE T0.DocDate BETWEEN '" + fi + "' AND '" + ff + "' ";
              SQLFactura += "AND T0.CardCode = '" + cmbCardCode.SelectedValue + "' ";

              SQLFactura += "UNION ALL ";

              //OBTIENE LOS XML DE LOS PAGOS
              SQLFactura += "SELECT 'P' as Tipo, T0.DocNum, ISNULL(CONVERT(varchar(35), FORMAT(T0.DocDate, 'yyyy-MM-dd'), 126),'') as DocDate, T0.CardCode, T0.CardName, CONVERT(varchar(50), CONVERT(MONEY, DocTotal), 1) as DocTotal, T1.E_Mail, T0.DocEntry, ";
              //SQLFactura += "substring(convert(nvarchar(4),T0.DocDate ,112),1,5) + '-' + substring(convert(nvarchar(6),T0.DocDate,112),5,7) + '\\' + ";
              //SQLFactura += "T0.CardCode + '\\' + case when T0.ObjType = 24 then  'RC\\' else 'IN\\' END + ";
              //SQLFactura += "(select eccc2.ReportID from SBO_TPD.dbo.ECM2 eccc2 where eccc2.SrcObjAbs = T0.DocEntry and eccc2.SrcObjType = 24) + '.xml' as 'EDocNum' ";
              SQLFactura += GetRutaXML(TipoDoctos.pagos);
              SQLFactura += "FROM ORCT T0 INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode ";
              SQLFactura += "WHERE T0.DocDate BETWEEN '" + fi + "' AND '" + ff + "' ";
              SQLFactura += "AND T0.CardCode = '" + cmbCardCode.SelectedValue + "' ";

          }else if (cmbTipoDoc.SelectedIndex == 0) { //VALIDA QUE CONSULTE TODAS LAS FACTURAS
              //OBTIENE LOS XML DE FACTURAS
              SQLFactura = "SELECT 'F' as Tipo, T0.DocNum, ISNULL(CONVERT(varchar(35), FORMAT(T0.DocDate, 'yyyy-MM-dd'), 126),'') as DocDate, T0.CardCode, T0.CardName, CONVERT(varchar(50), CONVERT(MONEY, DocTotal), 1) as DocTotal, T1.E_Mail, T0.DocEntry, " +

              //"substring(convert(nvarchar(4),T0.DocDate ,112),1,5) + '-' + substring(convert(nvarchar(6),T0.DocDate,112),5,7) + '\\' + " +
              //"T0.CardCode + '\\' + case when T0.ObjType = 13 then  'IN\\' else 'CM\\' END + " +
              //"(CASE WHEN T0.DocDate < '2019-02-13' THEN" +
              //" (SELECT eccc2.ReportID FROM SBO_TPD.dbo.ECM2 eccc2 WHERE eccc2.SrcObjAbs = T0.DocEntry and eccc2.SrcObjType = 13)" +
              //"ELSE" +
              //" (SELECT eccc2.U_BXP_UUID as ReportID FROM SBO_TPD.dbo.OINV eccc2 WHERE eccc2.DocNum = T0.DocNum and eccc2.ObjType = 13)" +
              //"END) " +
              //" + '.xml' as 'EDocNum'" +

              GetRutaXML(TipoDoctos.Facturas) +

              "FROM OINV T0 INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode " +
              "WHERE T0.DocDate BETWEEN '" + fi + "' AND '" + ff + "' " +
              "AND T0.CardCode = '" + cmbCardCode.SelectedValue + "' ";
          }
          else if(cmbTipoDoc.SelectedIndex == 1){ //VALIDA QUE CONSULTE TODAS LAS NOTAS DE CREDITO
              //OBTIENE LOS XML DE LAS NC
              SQLFactura = "SELECT 'NC' as Tipo, T0.DocNum, ISNULL(CONVERT(varchar(35), FORMAT(T0.DocDate, 'yyyy-MM-dd'), 126),'') as DocDate, T0.CardCode, T0.CardName, CONVERT(varchar(50), CONVERT(MONEY, DocTotal), 1) as DocTotal, T1.E_Mail, T0.DocEntry, ";
              //SQLFactura += "substring(convert(nvarchar(4),T0.DocDate ,112),1,5) + '-' + substring(convert(nvarchar(6),T0.DocDate,112),5,7) + '\\' + ";
              //SQLFactura += "T0.CardCode + '\\' + case when T0.ObjType = 14 then  'CM\\' else 'IN\\' END + ";
              //SQLFactura += "(select eccc2.ReportID from SBO_TPD.dbo.ECM2 eccc2 where eccc2.SrcObjAbs = T0.DocEntry and eccc2.SrcObjType = 14) + '.xml' as 'EDocNum' ";
              SQLFactura += GetRutaXML(TipoDoctos.Notas_Credito);
              SQLFactura += "FROM ORIN T0 INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode ";
              SQLFactura += "WHERE T0.DocDate BETWEEN '" + fi + "' AND '" + ff + "' ";
              SQLFactura += "AND T0.CardCode = '" + cmbCardCode.SelectedValue + "' ";

          }else if(cmbTipoDoc.SelectedIndex == 2) { //VALIDA QUE CONSULTE TODOS LOS PAGOS
              //OBTIENE LOS XML DE LOS PAGOS
              SQLFactura = "SELECT 'P' as Tipo, T0.DocNum, ISNULL(CONVERT(varchar(35), FORMAT(T0.DocDate, 'yyyy-MM-dd'), 126),'') as DocDate, T0.CardCode, T0.CardName, CONVERT(varchar(50), CONVERT(MONEY, DocTotal), 1) as DocTotal, T1.E_Mail, T0.DocEntry, ";
              //SQLFactura += "substring(convert(nvarchar(4),T0.DocDate ,112),1,5) + '-' + substring(convert(nvarchar(6),T0.DocDate,112),5,7) + '\\' + ";
              //SQLFactura += "T0.CardCode + '\\' + case when T0.ObjType = 24 then  'RC\\' else 'IN\\' END + ";
              //SQLFactura += "(select eccc2.ReportID from SBO_TPD.dbo.ECM2 eccc2 where eccc2.SrcObjAbs = T0.DocEntry and eccc2.SrcObjType = 24) + '.xml' as 'EDocNum' ";
              SQLFactura += GetRutaXML(TipoDoctos.pagos);
              SQLFactura += "FROM ORCT T0 INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode ";
              SQLFactura += "WHERE T0.DocDate BETWEEN '" + fi + "' AND '" + ff + "' ";
              SQLFactura += "AND T0.CardCode = '" + cmbCardCode.SelectedValue + "' ";
          }

          try
          {
              //CONECTA A LA BASE DE DATOS
              conexion.conectar();
              //ALMACEN LA CONSULTA EN UN CMD
              conexion.cmd = new SqlCommand(SQLFactura, conexion.con);
              //EJECUTA LA CONSULTA
              conexion.dr = conexion.cmd.ExecuteReader();
              //RECORRE LA CONSULTA
              while (conexion.dr.Read()) {
                  VDatos_OK = true;
                  //VALIDA SI EL DATAGRID TIENE FILAS EXISTENTES
                  if (dgvFacturas.RowCount > 0)
                  {
                      try
                      {
                          //AGREGA LOS ELEMENTOS DE LA CONSULTA AL DATAGRIDVIEW
                          dgvFacturas.Rows.Add(false, conexion.dr["Tipo"].ToString(), conexion.dr["DocNum"].ToString(), conexion.dr["DocDate"].ToString(), conexion.dr["CardCode"].ToString(), conexion.dr["CardName"].ToString(),
                              conexion.dr["DocTotal"].ToString(), conexion.dr["E_Mail"].ToString(), conexion.dr["DocEntry"].ToString(), conexion.dr["EDocNum"].ToString());
                          //ESTABLECE LA CELDA ACTUAL
                          dgvFacturas.CurrentCell = dgvFacturas.Rows[dgvFacturas.Rows.Count - 1].Cells[0];
                          //ALAMACENA EN EL COMBO EL CORREO DEL CLIENTE
                          txtE_Mail.Text = conexion.dr["E_Mail"].ToString();
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show("Error al agregar el resultado: " + ex.ToString(), "Alerta de llenado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                          VDatos_OK = false;
                          return;
                      }
                  }
                  else
                  {
                      try
                      {
                          //AGREGA LOS ELEMENTOS DE LA CONSULTA AL DATAGRIDVIEW
                          dgvFacturas.Rows.Add(false, conexion.dr["Tipo"].ToString(), conexion.dr["DocNum"].ToString(), conexion.dr["DocDate"].ToString(), conexion.dr["CardCode"].ToString(), conexion.dr["CardName"].ToString(),
                              conexion.dr["DocTotal"].ToString(), conexion.dr["E_Mail"].ToString(), conexion.dr["DocEntry"].ToString(), conexion.dr["EDocNum"].ToString());
                          //ESTABLECE LA CELDA ACTUAL
                          dgvFacturas.CurrentCell = dgvFacturas.Rows[dgvFacturas.Rows.Count - 1].Cells[0];
                          //ALAMACENA EN EL COMBO EL CORREO DEL CLIENTE
                          txtE_Mail.Text = conexion.dr["E_Mail"].ToString();
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show("Error al agregar el resultado: " + ex.ToString(), "Alerta de llenado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                          VDatos_OK = false;
                          return;
                      }
                  }
              }
              conexion.cerra_conectar();

              //VLAIDA SI ENCONTRO VALORES
              if (VDatos_OK == false)
              {
                  MessageBox.Show("No se encontraron datos para Mostrar. ", "Datos no encontrados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  cmbCardCode.Focus();
                  return;
              }
              //MANDA A LLAMAR EL METODO DE DESHABILITAR CAMPOOS
              MDeshabilitarCampos();

          }
          catch (Exception ex)
          {
              MessageBox.Show("Error en consulta o conexión:  " + ex.ToString(), "Error en Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
              conexion.cerra_conectar();
              return;
          }
      }

      //BOTON NUEVA BUSQUEDA
      private void btnNew_Click(object sender, EventArgs e)
      {
          //MANADA A LLAMAR AL METODO DE HABILITAR
          MHabilitarCampos();
      }


      //BOTON DE ENVIO DE FACTURA.
      private void btnSend_Click(object sender, EventArgs e)
      {
          //MANDA A LLAMAR AL METODO DE ENVIO DE FACTURA
          MEnviaFacturas();
          //VALIDA SI SE PUDO CREAAR EL PDF
          if (createPDF_OK == false && VMarcada == true)
          {
              //MENSAJE DE ERROR
              MessageBox.Show("No fue posible crear el PDF, favor de dar aviso al area de Sistemas.", "Alerta de PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              cmbCardCode.Focus();
          }else if (send_OK == false && VMarcada == true)
          {
              MessageBox.Show("No fue posible el envio del correo, favor de dar aviso al area de Sistemas.", "Alerta de envio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              cmbCardCode.Focus();
          }else if (createPDF_OK == false && send_OK == false && VMarcada == true)
          {
              MessageBox.Show("Ninguna acción se realizo, favor de dar aviso al area de Sistemas.", "Acciones fallidas", MessageBoxButtons.OK, MessageBoxIcon.Error);
              cmbCardCode.Focus();
          }else if(createPDF_OK == true && send_OK == true && VMarcada == true)
          {
              MessageBox.Show("Comprobantes fiscales enviados correctamente..", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
              cmbCardCode.Focus();
              //EJECUTA EL BOTON DE NUEVA BUSQUEDA
              btnNew.PerformClick();
          }
      }



      #endregion

      //CODIGO GENRAL =======================================================================================================FIN


      //METODOS======================================================================================INICIO

      #region Metodos

      //METODO LLENAR COMBO DE CLIENTES======================================================================================INICIO
      public void MLlenaClientes()
      {
          //VARIABLE DE CONSULTA
          string VLlenaClientes = "";
          VLlenaClientes = "SELECT CardCode, CardName, CardCode + '  -  ' + CardName as Cliente FROM OCRD WHERE CardType = 'C' and frozenFor <> 'Y' order by CardCode ";
          try
          {
              //ABE LA CONEXION
              conexion.conectar();
              //EJECUTA LA CONSULTA
              SqlDataAdapter DACliente = new SqlDataAdapter(VLlenaClientes, conexion.con);
              //INSTACIA EL DATASET Y DATAVIEW
              DataSet DSCliente = new DataSet();
              //ASIGNA AL DATASET
              DACliente.Fill(DSCliente, "Clientes");
              //ASIGNAN AL DATAVIEW
              DVCliente.Table = DSCliente.Tables["Clientes"];
              //AUTOCOMPLETADO DEL COMBOBOX
              //cmbCardCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
              //cmbCardCode.AutoCompleteSource = AutoCompleteSource.ListItems;
              cmbCardCode.DataSource = DVCliente;
              //cmbCardCode.DisplayMember = "CardName";
              cmbCardCode.DisplayMember = "Cliente";
              cmbCardCode.ValueMember = "CardCode";
              //CIERRA LA CONEXION
              conexion.cerra_conectar();
          }
          catch (Exception ex)
          {
              MessageBox.Show("Error en consulta o conexión: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return;
          }
      }
      //METODO LLENAR COMBO DE CLIENTES======================================================================================FIN

      //METODO LLENAR DATA GRID VIEW DE CLIENTES======================================================================================INICIO

      public void estilo_grid_factura() {
          //ESTILO DEL GRID DE FACTURA
          dgvFacturas.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite;
          dgvFacturas.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
          dgvFacturas.DefaultCellStyle.BackColor = Color.AliceBlue;
          dgvFacturas.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
          //Propiedad para no mostrar el cuadro que se encuentra en la parte
          //Superior Izquierda del gridview
          //.RowHeadersVisible = False
          dgvFacturas.AllowUserToAddRows = false;
          //CHECKBOX
          dgvFacturas.Columns["Select"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          dgvFacturas.Columns["Select"].ReadOnly = false;
          //dgvFacturas.Columns["DocNum"].Width = 60;
          //TIPO
          dgvFacturas.Columns["Tipo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
          dgvFacturas.Columns["Tipo"].ReadOnly = true;
          //FACTURA
          dgvFacturas.Columns["DocNum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
          dgvFacturas.Columns["DocNum"].ReadOnly = true;
          //FECHA
          //dgvFacturas.Columns["DocDate"].Width = 60;
          dgvFacturas.Columns["DocDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          dgvFacturas.Columns["DocDate"].ReadOnly = true;
          //CLIENTE
          //dgvFacturas.Columns["CardCode"].Width = 60;
          dgvFacturas.Columns["CardCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
          dgvFacturas.Columns["CardCode"].ReadOnly = true;
          //NOMBRE CLIENTE
          //dgvFacturas.Columns("CardName").Width = 70;
          dgvFacturas.Columns["CardName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
          dgvFacturas.Columns["CardName"].ReadOnly = true;
          //TOTAL
          dgvFacturas.Columns["DocTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          dgvFacturas.Columns["DocTotal"].ReadOnly = true;
          dgvFacturas.Columns["DocTotal"].DefaultCellStyle.Format = "$ ###,###,###.00";
          //CORREO
          dgvFacturas.Columns["E_mail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
          dgvFacturas.Columns["E_mail"].ReadOnly = true;
          dgvFacturas.Columns["E_mail"].Visible = false;
          //DOCENTRY
          dgvFacturas.Columns["DocEntry"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          dgvFacturas.Columns["DocEntry"].ReadOnly = true;
          dgvFacturas.Columns["DocEntry"].Visible = false;
          //NOMBRE DEL DOCUMENTO XML
          dgvFacturas.Columns["EDocNum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          dgvFacturas.Columns["EDocNum"].ReadOnly = true;
          dgvFacturas.Columns["EDocNum"].Visible = true;
      }

      //METODO LLENAR DATA GRID VIEW DE CLIENTES======================================================================================FIN

      //METODO HABILITAR COMPONENTES======================================================================================INICIO
      public void MHabilitarCampos()
      {
          MLlenaClientes();
          cmbCardCode.Enabled = true;
          cmbCardCode.SelectedIndex = -1;
          dtpfecha_ini.Enabled = true;
          dtpfecha_fin.Enabled = true;
          btnSearch.Enabled = true;
          btnSend.Enabled = false;
          txtE_Mail.Enabled = false;
          txtE_Mail.Text = "";
          //REFRESCA EL DATAGRID VIEW DE RESULTADO
          if (dgvFacturas.RowCount > 0)
          {
              dgvFacturas.Rows.Clear();
          }
          cmbCardCode.Focus();

      }
      //METODO HABILITAR COMPONENTES======================================================================================FIN

      //METODO DESHABILITAR COMPONENTES======================================================================================INICIO
      public void MDeshabilitarCampos()
      {
          cmbCardCode.Enabled = false;
          dtpfecha_ini.Enabled = false;
          dtpfecha_fin.Enabled = false;
          btnSearch.Enabled = false;
          btnNew.Enabled = true;
          btnSend.Enabled = true;
          txtE_Mail.Enabled = true;
      }
      //METODO DESHABILITAR COMPONENTES======================================================================================FIN
      //METODO DE ENVIO DE CORREO POR VARIAS FACTURAS A LA VEZ ==============================================================INICIO

      public void MEnviaFacturas()
      {
          //VARIABLES ALMACENABLES DE USO
          //DECLARACION DE VARIABLE DE REPORTE Y INSTANCIA DEL MISMO
          ReportDocument DocFacturas;
          DocFacturas = new ReportDocument();
          string Tipo; //VARIABLE PARA ALMACENAR EL TIPO DE DOCUMENTO (FACTURA,NC O PAGO)
          string DocNum; //VARIABLES PARA OBTENER LOS NUMEROS DE DOCUMENTOS
          string DocKey = string.Empty;
          string EDocNum; //ALAMACENA LA CADENA ELECTRONICA DEL DATA GRID
          string E_Mail = "";
          string CardName = "";
          string _rutaPDF; // ALMACENA LA RUTA DEL PDF
          string _rutaXML; //ALAMACENA LA RUTA DEL XML
          String correo1 = ""; //VARIABLE PARA ALMACENAR LOS  CORREOS
          DateTime fecha11082018 = Convert.ToDateTime("2018-08-11").Date; //VARIABLES PARA VALIDAR QUE FORMATO CREAR
          DateTime fecha01082018 = Convert.ToDateTime("2018-08-01").Date;
          DateTime fechainvoice = Convert.ToDateTime("2019-02-11").Date;
          DateTime fechaMigracionAgo2020 = Convert.ToDateTime("2020-08-23").Date;
          DateTime DocDate;
          VMarcada = false;

          //VARIABLE PARA LA EL CORREO
          System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

          #region CREACION DE RTP DE CRYSTAL Y ENVIO DEL CORREO
          //OBTIENE LOS CHECK ACTIVOS Y PARA LA CREACIÓN DE LOS FORMATOS DE FACTURA
          foreach (DataGridViewRow fila in dgvFacturas.Rows)
          {
              //VALIDA SI HAY UN COMBOBOX ACTIVO EN EL GRID
              if (Convert.ToBoolean(fila.Cells["Select"].Value))
              {
                  //VALIDA SI HAY FACTURAS MARCADAS
                  VMarcada = true;
                  //ALAMACENA LOS VALORES DEL DATAGRIDVIEW
                  Tipo = fila.Cells["Tipo"].Value.ToString();
                  DocNum = fila.Cells["DocNum"].Value.ToString();
                  DocKey = fila.Cells["DocEntry"].Value.ToString();
                  DocDate = Convert.ToDateTime(fila.Cells["DocDate"].Value.ToString()).Date; //CONVIERTE A FECHA EL VALOR DEL GRID
                  EDocNum = fila.Cells["EDocNum"].Value.ToString();
                  CardName = fila.Cells["CardName"].Value.ToString();
                  //E_Mail = fila.Cells["E_Mail"].Value.ToString();
                  E_Mail = txtE_Mail.Text;

                  //VALIDA QUE EL TIPO DE DOCUMENTO SEA FACTURA
                  if (Tipo == "F")
                  {
                      //VALIDA LA FECHA PARA SABER QUE FORMATO EJECUTAR
                      if (DocDate <= fecha11082018){
                        DocFacturas.Load(@"\\" + conexion.RutaReportes + @"\b1_shr\TPD\Factura 3_3 19ABR2018.rpt"); //RUTA DEL ARCHIVO .rpt
                      }
                      else if(DocDate > fecha11082018 & DocDate < fechainvoice ) {
                        DocFacturas.Load(@"\\" + conexion.RutaReportes + @"\b1_shr\TPD\Factura 3.3-93_5.rpt"); //RUTA DEL ARCHIVO .rpt
                      }
                      else if(DocDate > fechainvoice & DocDate < fechaMigracionAgo2020) { //Formato pre migracion AGO-2020
                        DocFacturas.Load(@"\\" + conexion.RutaReportes + @"\b1_shr\TPD\Factura 3.3-93_6_AddOn_DLL.rpt"); //RUTA DEL ARCHIVO .rpt
                      }
                      else{
                        DocFacturas.Load(@"\\" + conexion.RutaReportes + @"\b1_shr\TPD\Factura 3.3-93_6AddOn9NF2020.rpt"); //Formato post Migracion AGo 2020
                      }


                  //VALIDA QUE SOLO SEAN NOTAS DE CREDITO
                  }else if (Tipo == "NC")
                  {
                      if (DocDate <= fecha11082018){
                          DocFacturas.Load(@"\\" + conexion.RutaReportes + @"\b1_shr\TPD\NC 3_3 19ABR2018.rpt"); //RUTA DEL ARCHIVO .rpt
                      }else if (DocDate < fechaMigracionAgo2020)            {
                          DocFacturas.Load(@"\\" + conexion.RutaReportes + @"\b1_shr\TPD\NC 3.3_93_05.rpt"); //ruta del archivo .rpt
                      }else{
                          DocFacturas.Load(@"\\" + conexion.RutaReportes + @"\b1_shr\TPD\NC 3.3-93_6AddOn9NF2020.rpt"); //Formato post Migracion AGo 2020
                      }

                  //VALIDA QUW SOLO SEAN PAGOS
                  }else if (Tipo == "P"){
                      //VALIDA LA FECHA PARA VER QUE FORMATO SE TENDRA QUE EJECUTAR
                      if (DocDate >= fecha01082018 & DocDate < fechaMigracionAgo2020){
                          //CARGA EL FORMATO DE CRYSTAL REPORTS DE LOS PAGOS
                          DocFacturas.Load(@"\\" + conexion.RutaReportes + @"\b1_shr\TPD\PAGOS.rpt"); //ruta del archivo .rpt
                      }
                      else{
                        DocFacturas.Load(@"\\" + conexion.RutaReportes + @"\b1_shr\TPD\Pago 33_DLL_ModV2NF2020.rpt"); //Formato post Migracion AGo 2020
                      }
                  }
                  //PARAMETROS DE CONEXION PARA EL RPT
                  TableLogOnInfo tInfo = new TableLogOnInfo();
                  ConnectionInfo connectionInfo = tInfo.ConnectionInfo;

                  connectionInfo.Password = conexion.cPassword;
                  connectionInfo.UserID = conexion.cUserID;
                  connectionInfo.ServerName = conexion.cServerName;  // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS
                  connectionInfo.DatabaseName = conexion.cDatabaseNameSAP;

                  //PASA EL PARAMETRO AL ARCHIVO RPT (DocEntry)
                  DocFacturas.SetParameterValue("DocKey@", DocKey);

                  //ESTABLE LA CONEXION CON EL REPORTE
                  SetTableLocation(DocFacturas, connectionInfo);

                  //ALMACENA RUTA Y NOMBRE DEL ARCHIVO
                  _rutaPDF = DocNum + ".pdf";

                  //ALMACENA RUTA DE XML
                  _rutaXML = @"\\" + conexion.RutaReportes + @"\b1_shr\xml\TPD051215UZ1\" + EDocNum;

                  //Valido que exista el archvo, en caso contrario elimino o aviso
                  if (File.Exists(_rutaXML) == false){
                    //if (MessageBox.Show("No se pudo localizar el archivo XML, el archivo no será enviado, desea continuar con el envío del archivo PDF?", "Desea continuar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No ){
                    //  DocFacturas.Close();
                    //  return;
                    //}
                    EDocNum = "";
                  }

                  //GENERA PDF EN CARPETA TEMPORAL
                  try
                  {
                      //ALMACENA EL PDF EN LA RUTA TEMPORAL
                      DocFacturas.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, _rutaPDF);
                      createPDF_OK = true;
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show("No se pudo crear el archivo PDF: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      createPDF_OK = false;
                      DocFacturas.Close();
                      return;
                  }

                  //ADJUNTAR LOS ARCHIVOS PDF'S Y XML'S
                  try
                  {
                      System.Net.Mail.Attachment ArchiveRutaPDF = new System.Net.Mail.Attachment(_rutaPDF);
                      msg.Attachments.Add(ArchiveRutaPDF);
                      if (EDocNum != "") //VALIDA SI EL XML NO ES NULO
                      {
                          System.Net.Mail.Attachment ArchiveRutaXML = new System.Net.Mail.Attachment(_rutaXML);
                          msg.Attachments.Add(ArchiveRutaXML);
                      }
                      createPDF_OK = true;
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show("Error al adjuntar el archivo PDF: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      createPDF_OK = false;
                      DocFacturas.Close();
                      return;
                  }

              }//FIN VALIDA SI HAY UN COMBOBOX ACTIVO EN EL GRID
          }//FIN FOR OBTIENE LOS CHECK ACTIVOS Y PARA LA CREACIÓN DE LOS FORMATOS DE FACTURA

          //VALIDA SI SIGUE CON EL ENVIO O NO
          if (VMarcada == false)
          {
              MessageBox.Show("No es posible continuar, favor de marcar por lo menos una factura o validar si existen los archivos (PDF, XML)", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              VMarcada = false;
              DocFacturas.Close();
              return;
          }

          //SE CONCATENA ";" PARA QUE LO TOME LA VALIDACION DE CORREOS ILIMITADOS.
          E_Mail = E_Mail + ";";

          //SE COLOCA ARREGLO PARA VALIDAR SI TRAE 2 O MAS DESTINATARIOS
          char[] des = E_Mail.ToCharArray();
          //RECORRE TODA LA CADENA DE DESTINO DECLARADA EN ARREGLO
          for (int i = 0; i < des.Length; i++)
          {
              //VALIDA QUE SI TRAE DIFERENTE A ESPACIO CONCATENE, DE LO CONTRRIO NO HACE NADA
              if (des[i] != ' ')
              {
                  //VALIDA LA DIVISION DEL CORREO SI ES QUE HAY MAS DE TRES
                  if (des[i] == ',' || des[i] == ';' || des[i] == ':')
                  {
                      msg.To.Add(correo1); //ADJUNTA EL CORREO DESTINO
                      correo1 = ""; //QUITA EL CORREO ALMACENADO
                  }
                  else
                  {
                      correo1 = correo1 + des[i];
                  }//FIN VALIDA LA DIVISION DEL CORREO SI ES QUE HAY MAS DE TRES
              }//FIN VALIDA QUE SI TRAE DIFERENTE A ESPACIO CONCATENE, DE LO CONTRRIO NO HACE NADA
          }//FIN RECORRE TODA LA CADENA DE DESTINO DECLARADA EN ARREGLO

          //COLOCA LA PRIORIDAD DEL CORREO
          msg.Priority = System.Net.Mail.MailPriority.High;//Prioridad
          //COLOCA EL ASUNTO DEL CORREO
          msg.Subject = "Comprobante Fiscal Digital";
          //VISTA DE TODO EL CONTENIDO DEL CORREO EN HTML
          AlternateView htmlview = AlternateView.CreateAlternateViewFromString("<img src= cid:companylogo width='250' height='40'><p>Atencíón:</p><p>" + CardName + "</p><p>Estimado Cliente:</p><p>Por medio de la presente le informamos que  TRACTO PARTES DIAMANTE DE PUEBLA SA DE CV, le ha enviado un nuevo Comprobante Fiscal Digital.</p><p>Este mensaje es un envío automático, Favor de No Responder.</p><p>Si tiene alguna duda, le agradecemos contactarnos a la siguiente direccion de correo: info@tractopartesdiamante.com.mx.<br></p><p>Saludos Cordiales.</p>", null, "text/html");
          //OBTIENE EL LOGO DE LA EMPRESA
          LinkedResource logo = new LinkedResource("\\\\" + conexion.RutaReportes + @"\\b1_shr\\TPD\\Facturas\\Facturas\\Facturas\\tpd.png");
          logo.ContentId = "companylogo";
          //LO AGREGA AL CUERPO DEL CORREO
          htmlview.LinkedResources.Add(logo);
          //msg.AlternateViews.Add(planview);
          msg.AlternateViews.Add(htmlview);

          //DIRECCION DE EMISOR DEL CORREO
          msg.From = new System.Net.Mail.MailAddress("facturacion@tractopartesdiamante.com.mx", "Tracto Partes Diamante de Puebla");//Remitente

          //SE CREA EL CLIENTE DE SMTP DEL CORREO
          System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
          //ESPECIFICA EL SERVIDOR DEL HOST ENVIANTE
          client.Host = "mail.tractopartesdiamante.com.mx";
          //ASIGNA AL CLIENTE  EL PUERTO 26 DE USO
          client.Port = 26;
          //client.EnableSsl = true; -- no tenemos SSL
          //QUE EL EMISOR SIEMPRE SOLICITE LA CONTRASEÑA
          client.UseDefaultCredentials = true;
          //client.Credentials = new NetworkCredential("CorreoRemitente", "Contraseña");
          client.Credentials = new NetworkCredential("facturacion@tractopartesdiamante.com.mx", "FaCt017nKcrWfo%"); //EMISOR Y CONTRASEÑA DEL CORREO
          //CIERRA EL DOCUEMTNO DE RPT
          DocFacturas.Close();

          try
          {
              //REALIZA EL ENVIO DEL CORREO
              client.Send(msg); //Veamso cuanto tarda en este proceso
              //CIERRA LA APERTURA DEL CLIENTE DEL ENVIO DE CORREO
              client.Dispose();
              //CIERRA EL CUERPO HTML DEL CORREO
              msg.Dispose();
              send_OK = true;
          }
          catch (System.Net.Mail.SmtpException ex)
          {
              MessageBox.Show("Error al enviar el correo, : " + ex.Message, "Error de envio", MessageBoxButtons.OK, MessageBoxIcon.Error);
              send_OK = false;
              return;
          }

          #endregion

      }
      //METODO DE ENVIO DE CORREO POR VARIAS FACTURAS A LA VEZ ==============================================================FIN

      //METODO PARA EL CONSTRUCTOR DEL REPORTE CON LAS TABLAS DE CONEXION ===================================================INICIO
      private int SetTableLocation(ReportDocument report, ConnectionInfo connectionInfo)
      {
          foreach (CrystalDecisions.CrystalReports.Engine.Table table in report.Database.Tables)
          {
              //MessageBox.Show("Tabla: " + table.Name);
              TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
              tableLogOnInfo.ConnectionInfo = connectionInfo;
              table.ApplyLogOnInfo(tableLogOnInfo);
          }
          return 0;
      }//METODO PARA EL CONSTRUCTOR DEL REPORTE CON LAS TABLAS DE CONEXION ===================================================FIN




      #endregion

      //METODOS======================================================================================FIN


      //EVENTOS======================================================================================INICIO

      #region EVENTOS

      private void cmbCardCode_KeyUp(object sender, KeyEventArgs e) //AL SOLTAR UNA TECLA
      {
          // --------- AUTOCOMPLETADO DEL COMBO BOX AL BUSCAR POR CODIGO Y NOMBRE DEL CLIENTE
          try
          {
              //VALIDA QUE SEA DE LA "A" A LA "Z"
              if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
              {
                  //ALMACENA LO QUE TRAE EL COMBO
                  Str = cmbCardCode.Text;
                  //VALIDA SI VIENE VACIO EL COMBO
                  if (Str.CompareTo(string.Empty) == 0)
                  {
                      //NO COLOCA NADA
                      DVCliente.RowFilter = string.Empty;
                  }
                  else
                  {
                      //COMPARA CON EL DATASED EL VALOR QUE TRAIGA EN EL COMBO
                      //string RFCliente = string.Concat("CardName like '%", cmbCardCode.Text, "%' Or CardCode like '%", cmbCardCode.Text, "%'");
                      string RFCliente = string.Concat("CardName like '%", cmbCardCode.Text, "%' ");
                      DVCliente.RowFilter = RFCliente;
                  }
                  cmbCardCode.Text = "";
                  cmbCardCode.Text = Str;
                  cmbCardCode.SelectionStart = Str.Length;
                  cmbCardCode.SelectedIndex = -1;
                  cmbCardCode.DroppedDown = true;
                  cmbCardCode.SelectedIndex = -1;
                  cmbCardCode.Text = "";
                  cmbCardCode.Text = Str;
                  cmbCardCode.SelectionStart = Str.Length;
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show("Error encontrado en elemento del cliente: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
      }
      private void cmbCardCode_DropDown(object sender, EventArgs e)
      {
          Cursor = Cursors.Arrow;
          cmbCardCode.Text = Str;
      }

      #endregion

      //EVENTOS======================================================================================FIN

  }
  }
