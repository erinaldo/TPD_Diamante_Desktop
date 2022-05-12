Option Explicit On
'Option Strict On
Imports System.Data.SqlClient



Public Class DetDevolucionSeg

 Public conexion As New SqlConnection(conexion_universal.CadenaSQL)
 Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)


 Private Sub DetDevolucionSeg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  CBDictamen.DropDownStyle = ComboBoxStyle.DropDownList
  CBDictamen.SelectedIndex = 0



  If UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" Or UsrTPM = "RLIRA" Or UsrTPM = "COMERCIAL" _
           Or UsrTPM = "ASTRIDY" Or UsrTPM = "VENTAS5" Or UsrTPM = "VENTAS4" Or UsrTPM = "RROBLES" Or UsrTPM = "VENTAS8" _
           Or UsrTPM = "VVERGARA" Or UsrTPM = "VENTAS1" Then
   'Campos almacén
   DTPFecDic.Enabled = False
   TBFolio.ReadOnly = True
   'TBCausa.ReadOnly = True
   'Campos compras
   DTPRecComp.Enabled = False
   'DTPFecEntProv.Enabled = False
   'TBDiasTransFecProv.ReadOnly = True
   'TBComentarios.ReadOnly = True
   'Dictamen
   GroupBox1.Enabled = False

   'Si procede
   GroupBox5.Enabled = False

   'No procede
   GroupBox7.Enabled = False

   TBDiasTransTot.ReadOnly = True

   BSave.Visible = False
  End If

  ''COMENZAR DE AQUI

  conexion.Open()
  Dim cmd As SqlCommand = New SqlCommand("SELECT * FROM DetalleFacturaDevolucion WHERE Factura=@Factura AND Id=@Id AND Itemcode=@ItemCode ", conexion)

  cmd.Parameters.AddWithValue("@Factura", Module1.FacturaDev)
  cmd.Parameters.AddWithValue("@ItemCode", Module1.ArticuloDev)
  cmd.Parameters.AddWithValue("@Id", Module1.GarIdDev)


  Try

   Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
   Dim dt As New DataTable
   da.Fill(dt)

   If dt.Rows.Count > 0 Then
    Dim row As DataRow = dt.Rows(0)
    Dim NumNC As String = funciones.GetNotaCredito(CStr(row("factura")), CStr(row("Folio"))) '

    TBCardCode.Text = CStr(row("cardcode"))
    TBCardName.Text = CStr(row("cardname"))
    TBItemCode.Text = CStr(row("itemcode"))
    TBItemName.Text = CStr(row("itemname"))
    TBCantidad.Text = CStr(row("cantidad"))

    txtObservaciones.Text = Convert.ToString(row.Item("observaciones"))
    cbCausa.Text = Convert.ToString(row.Item("causa"))

    If Convert.ToString(row("CondicionesOptimas")) = "1" Then
     rbOptimas.Checked = True
     cbCondiciones.Text = " "

    Else

     rbDetalle.Checked = True

     cbCondiciones.Text = Convert.ToString(row.Item("CondicionesDevolucion"))

     Dim aux As String = Convert.ToString(row("Empaque en mal estado"))
     Dim aux2 As String = Convert.ToString(row("Incompleto/Faltante"))
     Dim aux3 As String = Convert.ToString(row("Ha sido instalada"))
     Dim aux4 As String = Convert.ToString(row("Fecha de fabricación mayor a 1 año"))



     If aux = "1" Then
      cbx1.Checked = True
     Else
      cbx1.Checked = False
     End If

     If aux2 = "2" = 1 Then
      cbx2.Checked = True
     Else
      cbx2.Checked = False
     End If

     If aux3 = "1" Then
      cbx3.Checked = True
     Else
      cbx3.Checked = False
     End If

     If aux4 = "1" Then
      cbx4.Checked = True
     Else
      cbx4.Checked = False
     End If
    End If

    'TBNumDoc.Text = IIf(CStr(row("NotCre")) Is DBNull.Value, NumNC, (CStr(row("NotCre"))))
    TBNumDoc.Text = IIf(String.IsNullOrEmpty(CStr(row("NotCre"))), NumNC, (CStr(row("NotCre"))))

                DTPRecComp.Text = IIf(CStr(row("FecEntCompRec")) Is DBNull.Value, "", (CStr(row("FecEntCompRec"))))
                DTPFecIniRev.Text = IIf(CStr(row("FechaInicioRevision")) Is DBNull.Value, "", (CStr(row("FechaInicioRevision"))))
                CBDictamen.Text = IIf(CStr(row("Dictamen")) Is DBNull.Value, "", (CStr(row("Dictamen"))))
    DTPFecDic.Text = CStr(row("FecDic"))
    DTPEnvCli.Text = CStr(row("FecEnv"))
    ' dtpFechaEntregaCobranza.Text = CStr(row("FechaCobranza"))
    TBFecRespCli.Text = CStr(row("FecNota"))
    TBFolio.Text = IIf(CStr(row("Folio")) Is DBNull.Value, "", (CStr(row("Folio"))))
    TBNumTrasp.Text = CStr(row("Docto"))
    TBFecTrasp.Text = CStr(row("FecDoc"))
    TBNumGuia.Text = CStr(row("NumGuiEnv"))
    TBDiasTransGuia.Text = CStr(row("NumGuiEnv"))
    TBDiasTransAlm2.Text = 0
    dtpFechaEntregaCobranza.Text = Convert.ToString(row.Item("FechaCobranza"))

    ObtenerNotaCredito()

    'dtpFechaEntregaCobranza.Text = IIf(CStr(row("FechaCobranza")) Is DBNull.Value, "", (CStr(row("FechaCobranza"))))

    ''OBTENER DIAS TOTALES TRANSCURRIDOS




    'If Module1.FecRecAlm <> "01/01/2015" Then
    '    If CStr(row("Dictamen")) = "" Then
    '        TBDiasTransTot.Text = DateDiff("d", Module1.FecRecAlm, Date.Now)
    '    ElseIf CStr(row("Dictamen")) = "Sí procede" Then
    '        If CStr(row("RespProv")) = "Nota de crédito" Or CStr(row("RespProv")) = "Cambio físico" Or CStr(row("RespProv")) = "Otros" Then
    '            If CStr(row("FecNota")) <> "01/01/2015" Then
    '                TBDiasTransTot.Text = DateDiff("d", Module1.FecRecAlm, TBFecRespCli.Value)    '
    '            Else
    '                TBDiasTransTot.Text = DateDiff("d", Module1.FecRecAlm, Date.Now)
    '            End If

    '        Else
    '            If CStr(row("FecRespProv")) <> "01/01/2015" Then
    '                TBDiasTransTot.Text = DateDiff("d", Module1.FecRecAlm, CStr(row("FecRespProv")))
    '            Else
    '                TBDiasTransTot.Text = DateDiff("d", Module1.FecRecAlm, Date.Now)
    '            End If
    '        End If
    '    ElseIf CStr(row("Dictamen")) = "No procede" Then
    '        If CStr(row("FecRecSuc")) <> "01/01/2015" Then
    '            TBDiasTransTot.Text = DateDiff("d", Module1.FecRecAlm, CStr(row("FecRecSuc")))
    '        Else
    '            TBDiasTransTot.Text = DateDiff("d", Module1.FecRecAlm, Date.Now)
    '        End If

    '    Else
    '        TBDiasTransTot.Text = 0
    '    End If
    'End If

    '--
    'If DTPFecEntProv.Value <> "01/01/2015" Then
    'If CBDictamen.Text = "" Then
    '        'TBDiasTransFecProv.Text = DateDiff("d", DTPFecEntProv.Value, Date.Now)
    '    Else
    '        'TBDiasTransFecProv.Text = DateDiff("d", DTPFecEntProv.Value, DTPFecDic.Value)
    '    End If
    'Else
    '    'TBDiasTransFecProv.Text = 0
    'End If

    'End If


    'If DTPEnvCli.Value <> "01/01/2015" And DTPFecRecAlm.Value = "01/01/2015" Then
    '    TBDiasTransGuia.Text = DateDiff("d", DTPEnvCli.Value, Date.Now)
    'Else
    '    TBDiasTransGuia.Text = DateDiff("d", DTPEnvCli.Value, DTPFecRecAlm.Value)  'DTPFecEntProv.Value - DTPRecComp.Value
   End If



  Catch ex As Exception
   MsgBox(ex.Message)
  Finally
   If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
    conexion.Close()
   End If

  End Try


  ''OBTENER NOTA DE CREDITO 

  conexion.Open()
  Dim cmd2 As SqlCommand = New SqlCommand("SELECT CONVERT(VARCHAR(20),T0.DocNum) AS 'NumFac',T0.DocDate, T1.ItemCode  " +
       "INTO #TempFact " +
       "FROM SBO_TPD.dbo.ORIN T0 " +
       "INNER JOIN SBO_TPD.dbo.RIN1 T1 ON T0.DocEntry=T1.DocEntry " +
       "WHERE CONVERT(VARCHAR(20),T0.U_Factura)= @NumFac AND T1.ItemCode=@Articulo " +
       "AND T0.DocType='I' AND T0.EDocNum IS NOT NULL  " +
       "SELECT NumFac,DocDate FROM #TempFact   " +
       "group by NumFac,DocDate " +
       "DROP TABLE #TempFact ", conexion)


  cmd2.Parameters.AddWithValue("@NumFac", Module1.FacturaDev)
  cmd2.Parameters.AddWithValue("@Articulo", Module1.ArticuloDev)

  Try

   Dim da As SqlDataAdapter = New SqlDataAdapter(cmd2)
   Dim dt As New DataTable
   da.Fill(dt)

   If dt.Rows.Count > 0 Then



    Dim row As DataRow = dt.Rows(0)


    TBNumDoc.Text = CStr(row("NumFac"))
    TBFecRespCli.Text = CStr(row("DocDate"))


    Dim VarFec As Integer

    VarFec = DateDiff("d", Module1.FecRecAlmDev, TBFecRespCli.Value)
    TBDiasTransAlm2.Text = VarFec

   End If

  Catch ex As Exception
   MsgBox(ex.Message)
  Finally
   If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
    conexion.Close()
   End If
   'If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
   '    conexion2.Close()
   'End If
  End Try



 End Sub

 Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
  'ACTUALIZAR REGISTROS EN FACTGAR
  Try

   Dim cnn As SqlConnection = Nothing

   Dim cmd4 As SqlCommand = Nothing

   'For i = 0 To DGGarantias.RowCount - 1

   Try

    cnn = New SqlConnection(StrTpm)

    cmd4 = New SqlCommand("SPActualizaDetFactDevolucion3Seg", cnn)
    cmd4.CommandType = CommandType.StoredProcedure
    cmd4.Parameters.AddWithValue("@Folio", TBFolio.Text)
    cmd4.Parameters.AddWithValue("@Factura", Module1.FacturaDev)
    cmd4.Parameters.AddWithValue("@Itemcode", Module1.ArticuloDev)
    cmd4.Parameters.AddWithValue("@Observaciones", txtObservaciones.Text)
    cmd4.Parameters.AddWithValue("@Causa", cbCausa.Text)
    cmd4.Parameters.AddWithValue("@FecEntcompRec", Date.Parse(DTPRecComp.Value).Date.ToString("yyyyMMdd"))
    cmd4.Parameters.AddWithValue("@FecInicRev", Date.Parse(DTPFecIniRev.Value).Date.ToString("yyyyMMdd"))




    If rbOptimas.Checked = True Then
     cmd4.Parameters.AddWithValue("@CondicionesOptimas", 1)
     cmd4.Parameters.AddWithValue("@Condiciones", "Condiciones Óptimas")
     cmd4.Parameters.AddWithValue("@CondicionDetalle1", 0)
     cmd4.Parameters.AddWithValue("@CondicionDetalle2", 0)
     cmd4.Parameters.AddWithValue("@CondicionDetalle3", 0)
     cmd4.Parameters.AddWithValue("@CondicionDetalle4", 0)

    Else
     cmd4.Parameters.AddWithValue("@CondicionesOptimas", 0)


     If (cbCondiciones.Text) <> "OTRA" Then
      cmd4.Parameters.AddWithValue("@Condiciones", cbCondiciones.Text)
      cmd4.Parameters.AddWithValue("@CondicionDetalle1", 0)
      cmd4.Parameters.AddWithValue("@CondicionDetalle2", 0)
      cmd4.Parameters.AddWithValue("@CondicionDetalle3", 0)
      cmd4.Parameters.AddWithValue("@CondicionDetalle4", 0)

     Else
      cmd4.Parameters.AddWithValue("@Condiciones", cbCondiciones.Text)

      If cbx1.Checked = True Then
       cmd4.Parameters.AddWithValue("@CondicionDetalle1", 1)
      Else
       cmd4.Parameters.AddWithValue("@CondicionDetalle1", 0)

      End If
      If cbx2.Checked = True Then
       cmd4.Parameters.AddWithValue("@CondicionDetalle2", 1)
      Else
       cmd4.Parameters.AddWithValue("@CondicionDetalle2", 0)

      End If

      If cbx3.Checked = True Then
       cmd4.Parameters.AddWithValue("@CondicionDetalle3", 1)
      Else
       cmd4.Parameters.AddWithValue("@CondicionDetalle3", 0)

      End If

      If cbx4.Checked = True Then
       cmd4.Parameters.AddWithValue("@CondicionDetalle4", 1)
      Else
       cmd4.Parameters.AddWithValue("@CondicionDetalle4", 0)

      End If

     End If
    End If





    cmd4.Parameters.AddWithValue("@Dictamen", CBDictamen.Text)
    cmd4.Parameters.AddWithValue("@FecDic", Date.Parse(DTPFecDic.Value).Date.ToString("yyyyMMdd"))
    cmd4.Parameters.AddWithValue("@NumGuiEnv", TBNumGuia.Text)
    cmd4.Parameters.AddWithValue("@FecEnv", Date.Parse(DTPEnvCli.Value).Date.ToString("yyyyMMdd"))
    cmd4.Parameters.AddWithValue("@NotCre", TBNumDoc.Text)

    cmd4.Parameters.AddWithValue("@FechaCobranza", Date.Parse(dtpFechaEntregaCobranza.Value).Date.ToString("yyyyMMdd"))

    cmd4.Parameters.AddWithValue("@FecNota", Date.Parse(TBFecRespCli.Text).Date.ToString("yyyyMMdd"))
    cmd4.Parameters.AddWithValue("@Docto", TBNumTrasp.Text)
    cmd4.Parameters.AddWithValue("@FecDoc", Date.Parse(TBFecTrasp.Text).Date.ToString("yyyyMMdd"))
    cmd4.Parameters.AddWithValue("@Id", Module1.GarIdDev)
    cmd4.Parameters.AddWithValue("@DiasTransEnvio", TBDiasTransGuia.Text)
    cmd4.Parameters.AddWithValue("@DiasTransTot", TBDiasTransTot.Text)

    cnn.Open()

    cmd4.ExecuteNonQuery()
    cmd4.Connection.Close()

    Dim da As New SqlDataAdapter
    da.SelectCommand = cmd4
    da.SelectCommand.Connection = cnn

   Catch ex As Exception
    'Return
    MsgBox(ex.Message)
   Finally
    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
     cnn.Close()
    End If
   End Try

   'Next
   'FIN ACTUALIZAR REGISTROS EN FACTGAR

   ' '' ''ESTATUS DE DETALLE FACTURA DEVOLUCION
   Dim conexion As New SqlConnection(StrTpm)
   '' abrir la conexión con la base de datos   
   'conexion.Open()
   Dim Adaptador As New SqlDataAdapter()
   Dim comando As New SqlCommand

   Dim SQLTPD As String


   If CBDictamen.Text = "Sí procede" Then

    'If CBRespProv.Text = "" Then
    SQLTPD = "UPDATE DetalleFacturaDevolucion SET Estado='APROBADA' WHERE FACTURA='" & Module1.FacturaDev & "' AND Id=" & Module1.GarIdDev & " "
    'ElseIf CBRespProv.Text = "Nota de crédito" Or CBRespProv.Text = "Cambio físico" Or CBRespProv.Text = "Otros" Then
    'SQLTPD = "UPDATE DetFactGar SET Estado='EN CURSO' WHERE FACTURA='" & Module1.Factura & "' AND Id=" & Module1.GarId & " "
   ElseIf CBDictamen.Text = "No procede" Then
    SQLTPD = "UPDATE DetalleFacturaDevolucion SET Estado='RECHAZADA' WHERE FACTURA='" & Module1.FacturaDev & "' AND Id=" & Module1.GarIdDev & " "

   ElseIf CBDictamen.Text = "Pendiente autorización" Then
    SQLTPD = "UPDATE DetalleFacturaDevolucion SET Estado='PENDIENTE AUTORIZACIÓN' WHERE FACTURA='" & Module1.FacturaDev & "' AND Id=" & Module1.GarIdDev & " "


   End If


   'End If




   Dim CmdActFlet As Data.SqlClient.SqlCommand

   CmdActFlet = New Data.SqlClient.SqlCommand()
   With CmdActFlet

    .Connection = New Data.SqlClient.SqlConnection(StrTpm)
    .Connection.Open()
    .CommandText = SQLTPD
    .ExecuteNonQuery()
    .Connection.Close()
   End With


   ' '' ''ESTATUS DE DETFACGAR
   ' ''If DTPEnvCli.Text <> "01/01/2015" And TBNumGuia.Text <> "" And DTPFecRecAlm.Value <> "01/01/2015" Then
   ' ''    Dim conexion As New SqlConnection(StrTpm)

   ' ''    '' abrir la conexión con la base de datos   
   ' ''    'conexion.Open()

   ' ''    Dim Adaptador As New SqlDataAdapter()
   ' ''    Dim comando As New SqlCommand

   ' ''    Dim SQLTPD As String

   ' ''    SQLTPD = "UPDATE DetFactGar SET Estado='TERMINADA' WHERE FACTURA='" & Module1.Factura & "' AND Id=" & Module1.GarId & " "

   ' ''    Dim CmdActFlet As Data.SqlClient.SqlCommand

   ' ''    CmdActFlet = New Data.SqlClient.SqlCommand()
   ' ''    With CmdActFlet
   ' ''        '.Parameters.AddWithValue("@Fchemb", DgFactProv.CurrentRow.Cells("Fchemb").Value)
   ' ''        '.Parameters.AddWithValue("@Fchent", DgFactProv.CurrentRow.Cells("Fchent").Value)
   ' ''        '.Parameters.AddWithValue("@Fchrec", DgFactProv.CurrentRow.Cells("Fchrec").Value)
   ' ''        '.Parameters.AddWithValue("@Fchact", DateTime.Now)
   ' ''        .Connection = New Data.SqlClient.SqlConnection(StrTpm)
   ' ''        .Connection.Open()
   ' ''        .CommandText = SQLTPD
   ' ''        .ExecuteNonQuery()
   ' ''        .Connection.Close()
   ' ''    End With

   ' ''End If


   MessageBox.Show("Datos actualizados correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

  Catch ex As Exception
   MsgBox(ex.Message)
   Return
  End Try
 End Sub

 Private Sub DTPRecComp_ValueChanged(sender As Object, e As EventArgs) Handles DTPRecComp.ValueChanged


  'TBDiasTransFecProv.Text = DateDiff("d", DTPRecComp.Value, Date.Now)

  ''Dim conexion As New SqlConnection(StrTpm)

  '' '' abrir la conexión con la base de datos   
  ' ''conexion.Open()

  ''Dim Adaptador As New SqlDataAdapter()
  ''Dim comando As New SqlCommand

  ''Dim SQLTPD As String

  ''SQLTPD = "UPDATE TPM.DBO.DetFactGar SET Estado='EN CURSO',FecEntcompRec='" & Date.Parse(DTPRecComp.Value).Date.ToString("yyyyMMdd") & "' WHERE Factura=@Factura AND Itemcode=@Itemcode AND Id=@Id "
  ' ''Date.Parse(DTPRecComp.Value).Date.ToString("yyyyMMdd")

  ''Dim CmdActFlet As Data.SqlClient.SqlCommand

  ''CmdActFlet = New Data.SqlClient.SqlCommand()
  ''With CmdActFlet
  ''    .Parameters.AddWithValue("@Factura", Module1.Factura)
  ''    .Parameters.AddWithValue("@Itemcode", Module1.Articulo)
  ''    .Parameters.AddWithValue("@Id", Module1.GarId)
  ''    '.Parameters.AddWithValue("@Fchact", DateTime.Now)
  ''    .Connection = New Data.SqlClient.SqlConnection(StrTpm)
  ''    .Connection.Open()
  ''    .CommandText = SQLTPD
  ''    .ExecuteNonQuery()
  ''    .Connection.Close()
  ''End With

  'Else
  '    'TBDiasTransFecProv.Text = DateDiff("d", DTPRecComp.Value, DTPFecEntProv.Value)  'DTPFecEntProv.Value - DTPRecComp.Value



  'End If

 End Sub

 Private Sub DTPFecEntProv_ValueChanged(sender As Object, e As EventArgs)

  ' If DTPFecEntProv.Text <> "01/01/2015" Then

  'TBDiasTransFecProv.Text = DateDiff("d", DTPRecComp.Value, DTPFecEntProv.Value)  'DTPFecEntProv.Value - DTPRecComp.Value


  'Dim conexion As New SqlConnection(StrTpm)

  ' '' abrir la conexión con la base de datos   
  ''conexion.Open()

  'Dim Adaptador As New SqlDataAdapter()
  'Dim comando As New SqlCommand

  'Dim SQLTPD As String

  'SQLTPD = "UPDATE TPM.DBO.DetFactGar SET Estado='CON EL PROVEEDOR', FecEntProvRev='" & Date.Parse(DTPFecEntProv.Value).Date.ToString("yyyyMMdd") & "' WHERE Factura=@Factura AND Itemcode=@Itemcode AND Id=@Id "

  'Dim CmdActFlet As Data.SqlClient.SqlCommand

  'CmdActFlet = New Data.SqlClient.SqlCommand()
  'With CmdActFlet
  '    .Parameters.AddWithValue("@Factura", Module1.Factura)
  '    .Parameters.AddWithValue("@Itemcode", Module1.Articulo)
  '    .Parameters.AddWithValue("@Id", Module1.GarId)
  '    '.Parameters.AddWithValue("@Fchact", DateTime.Now)
  '    .Connection = New Data.SqlClient.SqlConnection(StrTpm)
  '    .Connection.Open()
  '    .CommandText = SQLTPD
  '    .ExecuteNonQuery()
  '    .Connection.Close()
  'End With

  'End If

 End Sub

 Private Sub DTPFecRecAlm_ValueChanged(sender As Object, e As EventArgs)
  '  TBDiasTransGuia.Text = DateDiff("d", DTPEnvCli.Value, DTPFecRecAlm.Value)

  'If DTPFecRecAlm.Value <> "01/01/2015" Then

  '    Try

  '        Dim conexion As New SqlConnection(StrTpm)

  '        '' abrir la conexión con la base de datos   
  '        'conexion.Open()

  '        Dim Adaptador As New SqlDataAdapter()
  '        Dim comando As New SqlCommand

  '        Dim SQLTPD As String

  '        SQLTPD = "UPDATE TPM.DBO.DetFactGar SET Estado='TERMINADA', FecEntProvRev='" & Date.Parse(DTPFecRecAlm.Value).Date.ToString("yyyyMMdd") & "' WHERE Factura=@Factura AND Itemcode=@Itemcode AND Id=@Id "

  '        Dim CmdActFlet As Data.SqlClient.SqlCommand

  '        CmdActFlet = New Data.SqlClient.SqlCommand()
  '        With CmdActFlet
  '            .Parameters.AddWithValue("@Factura", Module1.Factura)
  '            .Parameters.AddWithValue("@Itemcode", Module1.Articulo)
  '            .Parameters.AddWithValue("@Id", Module1.GarId)
  '            '.Parameters.AddWithValue("@Fchact", DateTime.Now)
  '            .Connection = New Data.SqlClient.SqlConnection(StrTpm)
  '            .Connection.Open()
  '            .CommandText = SQLTPD
  '            .ExecuteNonQuery()
  '            .Connection.Close()
  '        End With


  '    Catch ex As Exception
  '        MsgBox(ex.Message)
  '    End Try

  'End If
 End Sub

 Private Sub DTPEnvCli_ValueChanged(sender As Object, e As EventArgs) Handles DTPEnvCli.ValueChanged
  'TBDiasTransGuia.Text = DateDiff("d", DTPEnvCli.Value, DTPFecRecAlm.Value)
 End Sub

 Private Sub CBDictamen_SelectedIndexChanged(sender As Object, e As EventArgs)
  If CBDictamen.Text = "Sí procede" Then

   ''CAMPOS SI PORCEDE
   DTPFecDic.Enabled = True
   'CBRespProv.Enabled = True
   'DTPFecRec.Enabled = True
   'TBNumRespprov.Enabled = True
   'CBRespCli.Enabled = True
   dtpFechaEntregaCobranza.Enabled = True
   TBNumDoc.Enabled = True
   TBFecRespCli.Enabled = True
   TBNumTrasp.Enabled = True
   TBFecTrasp.Enabled = True
   'DTPFecAlm2.Enabled = True
   'TBDiasTransAlm2.Enabled = True
   'DTPFecNotif.Enabled = True

   ''CAMPOS NO PORCEDE
   DTPEnvCli.Enabled = False
   TBNumGuia.Enabled = False
   'DTPFecRecAlm.Enabled = False
   'TBDiasTransGuia.Enabled = False

  ElseIf CBDictamen.Text = "No procede" Then
   ''CAMPOS SI PORCEDE
   'DTPFecDic.Enabled = False
   'CBRespProv.Enabled = False
   'DTPFecRec.Enabled = False
   'TBNumRespprov.Enabled = False
   'CBRespCli.Enabled = False
   TBNumDoc.Enabled = False
   TBFecRespCli.Enabled = False
   TBNumTrasp.Enabled = False
   TBFecTrasp.Enabled = False
   'DTPFecAlm2.Enabled = False
   'DTPFecNotif.Enabled = False
   'TBDiasTransAlm2.Enabled = False
   dtpFechaEntregaCobranza.Enabled = False
   ''CAMPOS NO PORCEDE
   DTPFecDic.Enabled = True
   DTPEnvCli.Enabled = True
   TBNumGuia.Enabled = True
   'DTPFecRecAlm.Enabled = True
   'TBDiasTransGuia.Enabled = False
  ElseIf CBDictamen.Text = "Pendiente autorización" Then

   dtpFechaEntregaCobranza.Enabled = False
   TBNumDoc.Enabled = False
   TBFecRespCli.Enabled = False
   TBNumTrasp.Enabled = False
   TBFecTrasp.Enabled = False
   TBDiasTransAlm2.Enabled = False
   DTPEnvCli.Enabled = False
   TBNumGuia.Enabled = False
   TBDiasTransGuia.Enabled = False
   TBDiasTransTot.Enabled = False


  Else
   ''CAMPOS SI PORCEDE
   DTPFecDic.Enabled = False
   TBNumDoc.Enabled = False
   TBFecRespCli.Enabled = False
   TBNumTrasp.Enabled = False
   TBFecTrasp.Enabled = False
   dtpFechaEntregaCobranza.Enabled = False

   ''CAMPOS NO PORCEDE
   DTPEnvCli.Enabled = False
   TBNumGuia.Enabled = False


  End If

 End Sub


 Private Sub CBRespProv_SelectedIndexChanged(sender As Object, e As EventArgs)

  'If CBDictamen.Text = "Sí procede" Then
  '    If CBRespProv.Text = "Nota de crédito" Or CBRespProv.Text = "Cambio físico" Or CBRespProv.Text = "Otros" Then
  '        TBNumRespprov.Enabled = True
  '        DTPFecRec.Enabled = True
  '        CBRespCli.Enabled = True
  '        TBNumDoc.Enabled = True
  '        TBFecRespCli.Enabled = True
  '        TBNumTrasp.Enabled = True
  '        TBFecTrasp.Enabled = True
  '        DTPFecAlm2.Enabled = True
  '    Else
  '        TBNumRespprov.Enabled = False
  '        'DTPFecRec.Enabled = False
  '        CBRespCli.Enabled = False
  '        TBNumDoc.Enabled = False
  '        TBFecRespCli.Enabled = False
  '        TBNumTrasp.Enabled = False
  '        TBFecTrasp.Enabled = False
  '        DTPFecAlm2.Enabled = False
  '    End If
  'End If
 End Sub

 Private Sub TBFecRespCli_ValueChanged(sender As Object, e As EventArgs)
  'If CBRespCli.Text = "Cambio físico" And TBFecRespCli.Value <> "01/01/2015" And Module1.FecRecAlm <> "01/01/2015" Then
  '    TBDiasTransAlm2.Text = DateDiff("d", Module1.FecRecAlm, TBFecRespCli.Value, )
  'End If
 End Sub

 Private Sub TBNumTrasp_TextChanged(sender As Object, e As EventArgs)
  ''OBTENER NOTA DE CREDITO 

  'conexion.Open()
  Dim cmd2 As SqlCommand = New SqlCommand("SELECT DOCDATE FROM SBO_TPD.dbo.OWTR where DocNum= @NumTras ", conexion)

  cmd2.Parameters.AddWithValue("@NumTras", TBNumTrasp.Text)
  'cmd.Parameters.AddWithValue("@ItemCode", Module1.Articulo)
  ''cmd.Parameters.AddWithValue("@Descripcion", Module1.Descripcion)
  'cmd.Parameters.AddWithValue("@Id", Module1.GarId)

  Try

   Dim da As SqlDataAdapter = New SqlDataAdapter(cmd2)
   Dim dt As New DataTable
   da.Fill(dt)

   If dt.Rows.Count > 0 Then

    'TBDocNum.BackColor = Color.White
    'TBCliente.BackColor = Color.White
    'TBNomCli.BackColor = Color.White

    Dim row As DataRow = dt.Rows(0)

    'TBFolio.Text = CStr(row("docentry"))
    TBFecTrasp.Text = CStr(row("DOCDATE"))

    'TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

    'If TBFecRespCli.Value <> "01/01/2015" Then

    'Dim VarFec As Integer

    'VarFec = DateDiff("d", Module1.FecRecAlm, TBFecRespCli.Value)
    'TBDiasTransAlm2.Text = VarFec
    'End If

    'Else
    '    TBDiasTransAlm2.Text = DateDiff("d", Module1.FecRecAlm, Today)
   End If

  Catch ex As Exception
   MsgBox(ex.Message)
  Finally
   If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
    conexion.Close()
   End If
   'If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
   '    conexion2.Close()
   'End If
  End Try
 End Sub

 Private Sub TBNumDoc_TextChanged(sender As Object, e As EventArgs)
  ObtenerNotaCredito()
 End Sub

 Private Sub ObtenerNotaCredito()
  ''OBTENER NOTA DE CREDITO 

  'conexion.Open()
  Dim cmd2 As SqlCommand = New SqlCommand("SELECT DOCDATE FROM SBO_TPD.dbo.ORIN where DocNum= @NumTras ", conexion)

  cmd2.Parameters.AddWithValue("@NumTras", TBNumDoc.Text)
  'cmd.Parameters.AddWithValue("@ItemCode", Module1.Articulo)
  ''cmd.Parameters.AddWithValue("@Descripcion", Module1.Descripcion)
  'cmd.Parameters.AddWithValue("@Id", Module1.GarId)

  Try

   Dim da As SqlDataAdapter = New SqlDataAdapter(cmd2)
   Dim dt As New DataTable
   da.Fill(dt)

   If dt.Rows.Count > 0 Then

    'TBDocNum.BackColor = Color.White
    'TBCliente.BackColor = Color.White
    'TBNomCli.BackColor = Color.White

    Dim row As DataRow = dt.Rows(0)

    'TBFolio.Text = CStr(row("docentry"))
    TBFecRespCli.Text = CStr(row("DOCDATE"))

    'TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

    'If TBFecRespCli.Value <> "01/01/2015" Then

    'Dim VarFec As Integer

    'VarFec = DateDiff("d", Module1.FecRecAlm, TBFecRespCli.Value)
    'TBDiasTransAlm2.Text = VarFec
    'End If

    'Else
    '    TBDiasTransAlm2.Text = DateDiff("d", Module1.FecRecAlm, Today)
   End If

  Catch ex As Exception
   MsgBox(ex.Message)
  Finally
   If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
    conexion.Close()
   End If
   'If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
   '    conexion2.Close()
   'End If
  End Try
 End Sub

 Private Sub TBFolio_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TBFolio.Validating

  ' ''MsgBox("ENTRO")
  ' ''MsgBox(Module1.Factura)

  ' ''conexion.Open()
  ''Dim cmd2 As SqlCommand = New SqlCommand("SELECT * FROM TPM.dbo.DetFactGar where Factura=@Factura AND ItemCode = @ItemCode ", conexion)   'AND Id=@Id 
  ''With cmd2

  ''    .Parameters.AddWithValue("@Factura", Module1.Factura)
  ''    .Parameters.AddWithValue("@Itemcode", Module1.Articulo)
  ''    '.Parameters.AddWithValue("@Id", Module1.GarId)
  ''End With
  ''Try

  ''    Dim da As SqlDataAdapter = New SqlDataAdapter(cmd2)
  ''    Dim dt As New DataTable
  ''    da.Fill(dt)

  ''    If dt.Rows.Count > 0 Then

  ''        'TBDocNum.BackColor = Color.White
  ''        'TBCliente.BackColor = Color.White
  ''        'TBNomCli.BackColor = Color.White

  ''        Dim row As DataRow = dt.Rows(0)

  ''        Dim factura As String
  ''        Dim articulo As String
  ''        Dim id As String
  ''        Dim folio As String

  ''        factura = CStr(row("factura"))
  ''        articulo = CStr(row("itemcode"))
  ''        id = CStr(row("id"))
  ''        folio = CStr(row("Folio"))

  ''        'MsgBox(folio)

  ''        If factura = Module1.Factura And folio = TBFolio.Text Then
  ''            MsgBox("Este número de folio ya existe, verifique.")
  ''            Return
  ''        End If



  ''        'TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

  ''        'If TBFecRespCli.Value <> "01/01/2015" Then

  ''        'Dim VarFec As Integer

  ''        'VarFec = DateDiff("d", Module1.FecRecAlm, TBFecRespCli.Value)
  ''        'TBDiasTransAlm2.Text = VarFec
  ''        'End If

  ''        'Else
  ''        '    TBDiasTransAlm2.Text = DateDiff("d", Module1.FecRecAlm, Today)
  ''    End If

  ''Catch ex As Exception
  ''    MsgBox(ex.Message)
  ''Finally

  ''End Try
  ''If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
  ''    conexion.Close()
  ''End If
 End Sub

 Private Sub CBRespProv_SelectedIndexChanged_1(sender As Object, e As EventArgs)
  'If CBDictamen.Text = "Sí procede" Then

  '    If CBRespProv.Text = "Nota de crédito" Or CBRespProv.Text = "Cambio físico" Or CBRespProv.Text = "Otros" Then
  '        TBNumRespprov.Enabled = True
  '        DTPFecRec.Enabled = True
  '        CBRespCli.Enabled = True
  '        TBNumDoc.Enabled = True
  '        TBFecRespCli.Enabled = True
  '        TBNumTrasp.Enabled = True
  '        TBFecTrasp.Enabled = True
  '        DTPFecAlm2.Enabled = True
  '    Else
  '        TBNumRespprov.Enabled = False
  '        'DTPFecRec.Enabled = False
  '        CBRespCli.Enabled = False
  '        TBNumDoc.Enabled = False
  '        TBFecRespCli.Enabled = False
  '        TBNumTrasp.Enabled = False
  '        TBFecTrasp.Enabled = False
  '        DTPFecAlm2.Enabled = False
  '    End If
  'End If

 End Sub

 Private Sub TBNumDoc_TextChanged_1(sender As Object, e As EventArgs) Handles TBNumDoc.TextChanged
  ''OBTENER NOTA DE CREDITO 

  'conexion.Open()
  Dim cmd2 As SqlCommand = New SqlCommand("SELECT DOCDATE FROM SBO_TPD.dbo.ORIN where DocNum= @NumTras ", conexion)

  cmd2.Parameters.AddWithValue("@NumTras", TBNumDoc.Text)
  'cmd.Parameters.AddWithValue("@ItemCode", Module1.Articulo)
  ''cmd.Parameters.AddWithValue("@Descripcion", Module1.Descripcion)
  'cmd.Parameters.AddWithValue("@Id", Module1.GarId)

  Try

   Dim da As SqlDataAdapter = New SqlDataAdapter(cmd2)
   Dim dt As New DataTable
   da.Fill(dt)

   If dt.Rows.Count > 0 Then

    'TBDocNum.BackColor = Color.White
    'TBCliente.BackColor = Color.White
    'TBNomCli.BackColor = Color.White

    Dim row As DataRow = dt.Rows(0)

    'TBFolio.Text = CStr(row("docentry"))
    TBFecRespCli.Text = CStr(row("DOCDATE"))

    'TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

    'If TBFecRespCli.Value <> "01/01/2015" Then

    'Dim VarFec As Integer

    'VarFec = DateDiff("d", Module1.FecRecAlm, TBFecRespCli.Value)
    'TBDiasTransAlm2.Text = VarFec
    'End If

    'Else
    '    TBDiasTransAlm2.Text = DateDiff("d", Module1.FecRecAlm, Today)
   End If

  Catch ex As Exception
   MsgBox(ex.Message)
  Finally
   If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
    conexion.Close()
   End If
   'If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
   '    conexion2.Close()
   'End If
  End Try
 End Sub

 Private Sub TBNumTrasp_TextChanged_1(sender As Object, e As EventArgs) Handles TBNumTrasp.TextChanged
  ''OBTENER NOTA DE CREDITO 

  'conexion.Open()
  Dim cmd2 As SqlCommand = New SqlCommand("SELECT DOCDATE FROM SBO_TPD.dbo.OWTR where DocNum= @NumTras ", conexion)

  cmd2.Parameters.AddWithValue("@NumTras", TBNumTrasp.Text)

  Try

   Dim da As SqlDataAdapter = New SqlDataAdapter(cmd2)
   Dim dt As New DataTable
   da.Fill(dt)

   If dt.Rows.Count > 0 Then


    Dim row As DataRow = dt.Rows(0)


    TBFecTrasp.Text = CStr(row("DOCDATE"))


   End If

  Catch ex As Exception
   MsgBox(ex.Message)
  Finally
   If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
    conexion.Close()
   End If

  End Try



 End Sub

 Private Sub Button1_Click(sender As Object, e As EventArgs)

 End Sub

 Private Sub TBItemName_TextChanged(sender As Object, e As EventArgs) Handles TBItemName.TextChanged

 End Sub

 Private Sub cbCondiciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCondiciones.SelectedIndexChanged
  If cbCondiciones.Text <> "OTRA" Then
   GroupBox2.Enabled = False
  Else
   GroupBox2.Enabled = True
  End If
 End Sub

 Private Sub rbOptimas_CheckedChanged(sender As Object, e As EventArgs) Handles rbOptimas.CheckedChanged
  If rbOptimas.Checked = True Then
   'rbDetalle.Enabled = False
   GroupBox2.Enabled = False
   'cbCondiciones.Text = ""
   cbCondiciones.Enabled = False


  Else

   rbDetalle.Enabled = True
   GroupBox2.Enabled = True
   cbCondiciones.Enabled = True

  End If
 End Sub

 Private Sub CBDictamen_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles CBDictamen.SelectedIndexChanged
  If CBDictamen.Text = "Sí procede" Then

   ''CAMPOS SI PORCEDE
   DTPFecDic.Enabled = True
   'CBRespProv.Enabled = True
   'DTPFecRec.Enabled = True
   'TBNumRespprov.Enabled = True
   'CBRespCli.Enabled = True
   dtpFechaEntregaCobranza.Enabled = True
   TBNumDoc.Enabled = True
   TBFecRespCli.Enabled = True
   TBNumTrasp.Enabled = True
   TBFecTrasp.Enabled = True
   'DTPFecAlm2.Enabled = True
   'TBDiasTransAlm2.Enabled = True
   'DTPFecNotif.Enabled = True

   ''CAMPOS NO PORCEDE
   DTPEnvCli.Enabled = False
   TBNumGuia.Enabled = False
   'DTPFecRecAlm.Enabled = False
   'TBDiasTransGuia.Enabled = False

  ElseIf CBDictamen.Text = "No procede" Then
   ''CAMPOS SI PORCEDE
   'DTPFecDic.Enabled = False
   'CBRespProv.Enabled = False
   'DTPFecRec.Enabled = False
   'TBNumRespprov.Enabled = False
   'CBRespCli.Enabled = False
   TBNumDoc.Enabled = False
   TBFecRespCli.Enabled = False
   TBNumTrasp.Enabled = False
   TBFecTrasp.Enabled = False
   'DTPFecAlm2.Enabled = False
   'DTPFecNotif.Enabled = False
   'TBDiasTransAlm2.Enabled = False
   dtpFechaEntregaCobranza.Enabled = False
   ''CAMPOS NO PORCEDE
   DTPFecDic.Enabled = True
   DTPEnvCli.Enabled = True
   TBNumGuia.Enabled = True
   'DTPFecRecAlm.Enabled = True
   'TBDiasTransGuia.Enabled = False
  ElseIf CBDictamen.Text = "Pendiente autorización" Then

   dtpFechaEntregaCobranza.Enabled = False
   TBNumDoc.Enabled = False
   TBFecRespCli.Enabled = False
   TBNumTrasp.Enabled = False
   TBFecTrasp.Enabled = False
   TBDiasTransAlm2.Enabled = False
   DTPEnvCli.Enabled = False
   TBNumGuia.Enabled = False
   TBDiasTransGuia.Enabled = False
   TBDiasTransTot.Enabled = False


  Else
   ''CAMPOS SI PORCEDE
   DTPFecDic.Enabled = False
   TBNumDoc.Enabled = False
   TBFecRespCli.Enabled = False
   TBNumTrasp.Enabled = False
   TBFecTrasp.Enabled = False
   dtpFechaEntregaCobranza.Enabled = False

   ''CAMPOS NO PORCEDE
   DTPEnvCli.Enabled = False
   TBNumGuia.Enabled = False


  End If
 End Sub

    Private Sub DTPFecIniRev_ValueChanged(sender As Object, e As EventArgs) Handles DTPFecIniRev.ValueChanged

    End Sub
End Class