Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.IO
'LIBRERIA REQUERIDA PARA CARGAR EL CRYSTAL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing

Public Class DetalleEmpaquePeso
  Dim hora As Date = DateTime.Now.ToString("dd/MM/yyyy") + " " + Now.ToString("hh:mm:ss")
  Dim PesoMax As Integer
  Dim dta As New DataTable
  Dim PesoMayor As Boolean
  Dim paqueteria As Boolean
  Dim paqueteriaName As String
  Dim TARIMA As Boolean


  Sub ValidaPeso()
    PesoMayor = False
    TARIMA = False
    'llena a travez de una consulta de sql el gridview detalle
    Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      Dim da As SqlDataAdapter
      Dim DsOrdenes = New DataSet
      'ALAMACENA LA CONSULTA
      SQLOrdenes = "Select (Select counT(ItemCode) From [Temporal_Articulos_Peso] p1 where p1.Itemcode collate SQL_Latin1_General_CP850_CI_AS =T0.ItemCode) as AMPeso,ItemCode, IIF(T2.BoxName IS NULL,'-',T2.BoxName)as BoxName ,T1.TrnspCode from SBO_TPD.dbo.DLN1 T0 "
      SQLOrdenes &= "INNER JOIN TPM.dbo.Operacion_Entrega T1  on T0.DocEntry=T1.DocEntry INNER join TPM.dbo.Operacion_Orden T2 on T2.DocNum =T1.DocNum  "
      SQLOrdenes &= " WHERE T0.DocEntry='" + DocNumEmpacar + "'"
      cnn = New SqlConnection(StrTpm)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL 
      cmd = New SqlCommand(SQLOrdenes, cnn)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd.CommandType = CommandType.Text
      'APERTURA LA CONEXION
      cnn.Open()
      'INSTANCIA UN ADAPTER
      da = New SqlDataAdapter
      'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
      da.SelectCommand = cmd
      'LO EJECUTA CON LA CONEXION
      da.SelectCommand.Connection = cnn
      'TIEMPO DE ESPERA DE LA CONEXION
      da.SelectCommand.CommandTimeout = 10000
      'EJECUTA LA CONSULTA
      cmd.ExecuteNonQuery()
      'CIERRA EL COMMAND DE SQL
      cmd.Connection.Close()
      'CIERRA LA CONEXION
      cnn.Close()
      'LLENA EL ADAPTER A UN DATA SET
      da.Fill(dta)

      For Each row As DataRow In dta.Rows

        Dim valor As String = CStr(row("BoxName"))
        If valor = "TARIMA" Then
          TARIMA = True
        End If
      Next

      For Each row As DataRow In dta.Rows

        Dim valor As String = CStr(row("AMPeso"))
        If valor > 0 Then
          PesoMayor = True
        End If
      Next
      For Each row As DataRow In dta.Rows

        Dim valor As String = CStr(row("TrnspCode"))
        If valor = 9 Or valor = 10 Or valor = 20 Or valor = 21 Or valor = 43 Or valor = 44 Or valor = -1 Then
          paqueteria = True
          paqueteriaName = valor
        End If
      Next
      If TARIMA = True Then
        PesoMax = 1100
      Else
        If PesoMayor = True Then
          PesoMax = 50
        Else
          If paqueteriaName = 9 Or paqueteriaName = 10 Or paqueteriaName = 20 Or paqueteriaName = 21 Or paqueteriaName = -1 Then
            PesoMax = 40
          Else
            PesoMax = 35
          End If
        End If
      End If

    Catch ex As Exception
      MsgBox("Error: " + ex.ToString)
    End Try
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
    Dim TotalPeso As Decimal = 0
    If (MessageBox.Show("Realmente desea terminar el empaque." & vbCrLf & vbCrLf & "Al aceptar todos los datos se guardaran.",
                          "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.No) Then
    Else
      If txtcaja1.Enabled = True Then
        'VARIABLE DE CADENA DE SQL
        Dim SQLOrdenes As String
        'VARIABLES DE CONEXION DE LLENADO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing
        If txtcaja1.Enabled = True Then
          SQLOrdenes = " insert into TPM.dbo.SP_Operacion_Peso_Cajas values(" + DocNumEmpacar + "," + txtcaja1.Text + ",1) "
          TotalPeso += Decimal.Parse(txtcaja1.Text)
        End If
        If txtcaja2.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja2.Text + ",2 )"
          TotalPeso += Decimal.Parse(txtcaja2.Text)
        End If
        If txtcaja3.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja3.Text + ",3 )"
          TotalPeso += Decimal.Parse(txtcaja3.Text)
        End If
        If txtcaja4.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja4.Text + ",4 )"
          TotalPeso += Decimal.Parse(txtcaja4.Text)
        End If
        If txtcaja5.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja5.Text + ",5 )"
          TotalPeso += Decimal.Parse(txtcaja5.Text)
        End If
        If txtcaja6.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja6.Text + ",6 )"
          TotalPeso += Decimal.Parse(txtcaja6.Text)
        End If
        If txtcaja7.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja7.Text + ",7 )"
          TotalPeso += Decimal.Parse(txtcaja7.Text)
        End If
        If txtcaja8.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja8.Text + ",8 )"
          TotalPeso += Decimal.Parse(txtcaja8.Text)
        End If
        If txtcaja9.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja9.Text + ",9 )"
          TotalPeso += Decimal.Parse(txtcaja9.Text)
        End If
        If txtcaja10.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja10.Text + ",10 )"
          TotalPeso += Decimal.Parse(txtcaja10.Text)
        End If
        If txtcaja11.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja11.Text + ",11 )"
          TotalPeso += Decimal.Parse(txtcaja11.Text)
        End If
        If txtcaja12.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja12.Text + ",12 )"
          TotalPeso += Decimal.Parse(txtcaja12.Text)
        End If
        If txtcaja13.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja13.Text + ",13 )"
          TotalPeso += Decimal.Parse(txtcaja13.Text)
        End If
        If txtcaja14.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja14.Text + ",14 )"
          TotalPeso += Decimal.Parse(txtcaja14.Text)
        End If
        If txtcaja15.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja15.Text + ",15 )"
          TotalPeso += Decimal.Parse(txtcaja15.Text)
        End If
        If txtcaja16.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja16.Text + ",16 )"
          TotalPeso += Decimal.Parse(txtcaja16.Text)
        End If
        If txtcaja17.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja17.Text + ",17 )"
          TotalPeso += Decimal.Parse(txtcaja17.Text)
        End If
        If txtcaja18.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja18.Text + ",18 )"
          TotalPeso += Decimal.Parse(txtcaja18.Text)
        End If
        If txtcaja19.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja19.Text + ",19 )"
          TotalPeso += Decimal.Parse(txtcaja19.Text)
        End If
        If txtcaja20.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja20.Text + ",20 )"
          TotalPeso += Decimal.Parse(txtcaja20.Text)
        End If
        If txtcaja21.Enabled = True Then
          SQLOrdenes += ",(" + DocNumEmpacar + "," + txtcaja21.Text + ",21 )"
          TotalPeso += Decimal.Parse(txtcaja21.Text)
        End If
        cnn = New SqlConnection(StrTpm)
        'ALMACENA LA CONSULTA EN UN COMMAND SQL
        cmd = New SqlCommand(SQLOrdenes, cnn)
        'CONVIERTE EL TEXTO EN CONSULTA
        cmd.CommandType = CommandType.Text
        'APERTURA LA CONEXION
        cnn.Open()
        cmd.ExecuteNonQuery()
        'CIERRA EL COMMAND DE SQL
        cmd.Connection.Close()
        'CIERRA LA CONEXION
        cnn.Close()
      Else
        MsgBox("Para guardar los datos debes registrar el numero de cajas.")

      End If
      'ACTUALIZA EL ESTATUS A EMPACADO
      Try

        'VARIABLE DE CADENA DE SQL
        Dim SQLOrdenes As String
        'VARIABLES DE CONEXION DE LLENADO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing

        SQLOrdenes = "Update TPM.dbo.Operacion_Entrega SET Status='EP', Action='Empacado', BoxTotal=" + Txtcajas.Text + ", Peso=" + TotalPeso.ToString() + " WHERE DocEntry=" + DocNumEmpacar + " "
        cnn = New SqlConnection(StrTpm)
        'ALMACENA LA CONSULTA EN UN COMMAND SQL
        cmd = New SqlCommand(SQLOrdenes, cnn)
        'CONVIERTE EL TEXTO EN CONSULTA
        cmd.CommandType = CommandType.Text
        'APERTURA LA CONEXION
        cnn.Open()
        cmd.ExecuteNonQuery()
        'CIERRA EL COMMAND DE SQL
        cmd.Connection.Close()
        'CIERRA LA CONEXION
        cnn.Close()
      Catch ex As Exception
        MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empacado " + ex.ToString)

      End Try
      'Actualiza operacion Orden 
      Try
        'VARIABLE DE CADENA DE SQL
        Dim SQLOrdenes As String
        'VARIABLES DE CONEXION DE LLENADO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing
        SQLOrdenes = "Update TPM.dbo.Operacion_Orden  SET Status='EP', Action='Empacado', BoxTotal=" + Txtcajas.Text + "  WHERE DocNum=" + DocNumSurtido + ""
        cnn = New SqlConnection(StrTpm)
        'ALMACENA LA CONSULTA EN UN COMMAND SQL
        cmd = New SqlCommand(SQLOrdenes, cnn)
        'CONVIERTE EL TEXTO EN CONSULTA
        cmd.CommandType = CommandType.Text
        'APERTURA LA CONEXION
        cnn.Open()
        cmd.ExecuteNonQuery()
        'CIERRA EL COMMAND DE SQL
        cmd.Connection.Close()
        'CIERRA LA CONEXION
        cnn.Close()
      Catch ex As Exception
        MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empacado " + ex.ToString)
      End Try
      'Actualiza operacion analicis
      Try
        'VARIABLE DE CADENA DE SQL
        Dim SQLOrdenes As String
        'VARIABLES DE CONEXION DE LLENADO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing
        SQLOrdenes = "Update TPM.dbo.Operacion_Analisis set TimeEmpacado=GETDATE()  where DocEntry='" + DocNumSurtido + "'"
        cnn = New SqlConnection(StrTpm)
        'ALMACENA LA CONSULTA EN UN COMMAND SQL
        cmd = New SqlCommand(SQLOrdenes, cnn)
        'CONVIERTE EL TEXTO EN CONSULTA
        cmd.CommandType = CommandType.Text
        'APERTURA LA CONEXION
        cnn.Open()
        cmd.ExecuteNonQuery()
        'CIERRA EL COMMAND DE SQL
        cmd.Connection.Close()
        'CIERRA LA CONEXION
        cnn.Close()
      Catch ex As Exception
        MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empacando " + ex.ToString)
      End Try
      MsgBox("El empaque se termino con Exito.", MsgBoxStyle.Information, "Termino Empaque")
      Me.Close()
    End If

    'ACTIVAR EL TIMER DE EMPAQUE CUANDO SE TERMINEN DE GUARDAR LOS DATOS
    frmMostrarOrdenes.Timer1.Enabled = True
  End Sub
  Private Sub DetalleEmpaquePeso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ValidaPeso()
    Label3.Text = DocNumSurtido
    Label5.Text = ClienteNombre
    lblpaqueteria.Text = Paqueteria_Nombre
  End Sub

  Private Sub Txtcajas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txtcajas.KeyPress
    If Not Char.IsDigit(e.KeyChar) And e.KeyChar <> vbBack Then
      e.Handled = True

    End If
  End Sub

  Private Sub Txtcajas_Leave(sender As Object, e As EventArgs) Handles Txtcajas.Leave
    If Txtcajas.Text = "" Then
    Else
      If Txtcajas.Text > 0 And Txtcajas.Text >= 1 Then
        txtcaja1.Visible = True
        txtcaja1.Enabled = True
        lblcaja1.Visible = True
        lblcaja1.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 2 Then

        txtcaja2.Visible = True
        txtcaja2.Enabled = True
        lblcaja2.Visible = True
        lblcaja2.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 3 Then

        txtcaja3.Visible = True
        txtcaja3.Enabled = True
        lblcaja3.Visible = True
        lblcaja3.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 4 Then
        txtcaja4.Visible = True
        txtcaja4.Enabled = True
        lblcaja4.Visible = True
        lblcaja4.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 5 Then
        txtcaja5.Visible = True
        txtcaja5.Enabled = True
        lblcaja5.Visible = True
        lblcaja5.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 6 Then
        txtcaja6.Visible = True
        txtcaja6.Enabled = True
        lblcaja6.Visible = True
        lblcaja6.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 7 Then
        txtcaja7.Visible = True
        txtcaja7.Enabled = True
        lblcaja7.Visible = True
        lblcaja7.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 8 Then
        txtcaja8.Visible = True
        txtcaja8.Enabled = True
        lblcaja8.Visible = True
        lblcaja8.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 9 Then
        txtcaja9.Visible = True
        txtcaja9.Enabled = True
        lblcaja9.Visible = True
        lblcaja9.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 10 Then
        txtcaja10.Visible = True
        txtcaja10.Enabled = True
        lblcaja10.Visible = True
        lblcaja10.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 11 Then
        txtcaja11.Visible = True
        txtcaja11.Enabled = True
        lblcaja11.Visible = True
        lblcaja11.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 12 Then
        txtcaja12.Visible = True
        txtcaja12.Enabled = True
        lblcaja12.Visible = True
        lblcaja12.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 13 Then
        txtcaja13.Visible = True
        txtcaja13.Enabled = True
        lblcaja13.Visible = True
        lblcaja13.Enabled = True
      End If

      If Txtcajas.Text > 0 And Txtcajas.Text >= 14 Then
        txtcaja14.Visible = True
        txtcaja14.Enabled = True
        lblcaja14.Visible = True
        lblcaja14.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 15 Then
        txtcaja15.Visible = True
        txtcaja15.Enabled = True
        lblcaja15.Visible = True
        lblcaja15.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 16 Then
        txtcaja16.Visible = True
        txtcaja16.Enabled = True
        lblcaja16.Visible = True
        lblcaja16.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 17 Then
        txtcaja17.Visible = True
        txtcaja17.Enabled = True
        lblcaja17.Visible = True
        lblcaja17.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 18 Then
        txtcaja18.Visible = True
        txtcaja18.Enabled = True
        lblcaja18.Visible = True
        lblcaja18.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 19 Then
        txtcaja19.Visible = True
        txtcaja19.Enabled = True
        lblcaja19.Visible = True
        lblcaja19.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 20 Then
        txtcaja20.Visible = True
        txtcaja20.Enabled = True
        lblcaja20.Visible = True
        lblcaja20.Enabled = True
      End If
      If Txtcajas.Text > 0 And Txtcajas.Text >= 21 Then
        txtcaja21.Visible = True
        txtcaja21.Enabled = True
        lblcaja21.Visible = True
        lblcaja21.Enabled = True
      End If

      If Txtcajas.Text > 21 Then
        MessageBox.Show("El Numero maximo de cajas es 21")
        Txtcajas.Clear()
        Txtcajas.Text = 21
      End If
      txtcaja1.Select()
    End If





  End Sub

  Private Sub Txtcajas_Enter(sender As Object, e As EventArgs) Handles Txtcajas.Enter

    Txtcajas.Text = Cajas

    txtcaja1.Visible = False
    txtcaja1.Enabled = False
    lblcaja1.Visible = False
    lblcaja1.Enabled = False


    txtcaja2.Visible = False
    txtcaja2.Enabled = False
    lblcaja2.Visible = False
    lblcaja2.Enabled = False


    txtcaja3.Visible = False
    txtcaja3.Enabled = False
    lblcaja3.Visible = False
    lblcaja3.Enabled = False

    txtcaja4.Visible = False
    txtcaja4.Enabled = False
    lblcaja4.Visible = False
    lblcaja4.Enabled = False

    txtcaja5.Visible = False
    txtcaja5.Enabled = False
    lblcaja5.Visible = False
    lblcaja5.Enabled = False

    txtcaja6.Visible = False
    txtcaja6.Enabled = False
    lblcaja6.Visible = False
    lblcaja6.Enabled = False

    txtcaja7.Visible = False
    txtcaja7.Enabled = False
    lblcaja7.Visible = False
    lblcaja7.Enabled = False

    txtcaja8.Visible = False
    txtcaja8.Enabled = False
    lblcaja8.Visible = False
    lblcaja8.Enabled = False

    txtcaja9.Visible = False
    txtcaja9.Enabled = False
    lblcaja9.Visible = False
    lblcaja9.Enabled = False

    txtcaja10.Visible = False
    txtcaja10.Enabled = False
    lblcaja10.Visible = False
    lblcaja10.Enabled = False

    txtcaja11.Visible = False
    txtcaja11.Enabled = False
    lblcaja11.Visible = False
    lblcaja11.Enabled = False

    txtcaja12.Visible = False
    txtcaja12.Enabled = False
    lblcaja12.Visible = False
    lblcaja12.Enabled = False

    txtcaja13.Visible = False
    txtcaja13.Enabled = False
    lblcaja13.Visible = False
    lblcaja13.Enabled = False

    txtcaja14.Visible = False
    txtcaja14.Enabled = False
    lblcaja14.Visible = False
    lblcaja14.Enabled = False

    txtcaja15.Visible = False
    txtcaja15.Enabled = False
    lblcaja15.Visible = False
    lblcaja15.Enabled = False

    txtcaja16.Visible = False
    txtcaja16.Enabled = False
    lblcaja16.Visible = False
    lblcaja16.Enabled = False

    txtcaja17.Visible = False
    txtcaja17.Enabled = False
    lblcaja17.Visible = False
    lblcaja17.Enabled = False

    txtcaja18.Visible = False
    txtcaja18.Enabled = False
    lblcaja18.Visible = False
    lblcaja18.Enabled = False

    txtcaja19.Visible = False
    txtcaja19.Enabled = False
    lblcaja19.Visible = False
    lblcaja19.Enabled = False

    txtcaja20.Visible = False
    txtcaja20.Enabled = False
    lblcaja20.Visible = False
    lblcaja20.Enabled = False

    txtcaja21.Visible = False
    txtcaja21.Enabled = False
    lblcaja21.Visible = False
    lblcaja21.Enabled = False

  End Sub

  Private Sub Txtcajas_TextChanged(sender As Object, e As EventArgs) Handles Txtcajas.TextChanged

  End Sub

  Private Sub DetalleEmpaquePeso_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    'INSTANCIA OBJECTO DE TIPO FORMULARIO DE MOSTRAR ORDENES PARA REFRESCARLO
    Dim Form As frmEstatusEmpaque = Application.OpenForms.OfType(Of frmEstatusEmpaque)().FirstOrDefault()
    'VALIDA SI ENCUENTRA LA INSTANCIA (FORMULARIO) ABIERTA        
    If (Form IsNot Nothing) Then
      'ACTIVA EL FORMULARIO DE MOSTRAR ORDENES
      Form.Activate()
      Form.Refresh()
      'EJECUTA LOS METODOS DEL FORMULARIO DE MOSTRAR ORDENES
      Form.MEjecuta_Full_Empaque()
      'Form.MEjecuta_Entrega() '5 ms
      'Form.MEjecuta_Entrega_Ped() '1,990 ms
      'Form.MEjecuta_Entrega_Ped_Det() '13,372 ms
      Form.LlenarEmpaque()
      'REFRESCA EL FORMULARIO
      Form.Refresh()
    End If

  End Sub



#Region "Eventos"
  Private Sub Txtcajas_KeyDown(sender As Object, e As KeyEventArgs) Handles Txtcajas.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      txtcaja1.Select()
    End If
  End Sub

#End Region

#Region "Eventos KeyDown TXT"
  Private Sub txtcaja1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja1.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja2.Enabled = True Then
        txtcaja2.Select()
      Else
        BtnGuardar.Select()

      End If
    End If

  End Sub

  Private Sub txtcaja2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja2.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja3.Enabled = True Then
        txtcaja3.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja3_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja3.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja4.Enabled = True Then
        txtcaja4.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja4_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja4.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja5.Enabled = True Then
        txtcaja5.Select()
      Else
        BtnGuardar.Select()
      End If
    End If

  End Sub

  Private Sub txtcaja5_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja5.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja6.Enabled = True Then
        txtcaja6.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja6_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja6.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja7.Enabled = True Then
        txtcaja7.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja7_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja7.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja8.Enabled = True Then
        txtcaja8.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja8_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja8.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja9.Enabled = True Then
        txtcaja9.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja9_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja9.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja10.Enabled = True Then
        txtcaja10.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja10_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja10.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja11.Enabled = True Then
        txtcaja11.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja11_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja11.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja12.Enabled = True Then
        txtcaja12.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja12_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja12.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja13.Enabled = True Then
        txtcaja13.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja13_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja13.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja14.Enabled = True Then
        txtcaja14.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja14_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja14.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja15.Enabled = True Then
        txtcaja15.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja15_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja15.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja16.Enabled = True Then
        txtcaja16.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja16_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja16.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja17.Enabled = True Then
        txtcaja17.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja17_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja17.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja18.Enabled = True Then
        txtcaja18.Select()
      Else
        BtnGuardar.Select()

      End If
    End If
  End Sub

  Private Sub txtcaja18_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja18.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja19.Enabled = True Then
        txtcaja19.Select()
      Else
        BtnGuardar.Select()
      End If
    End If
  End Sub

  Private Sub txtcaja19_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja19.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja20.Enabled = True Then
        txtcaja20.Select()
      Else
        BtnGuardar.Select()
      End If
    End If
  End Sub

  Private Sub txtcaja20_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja20.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      If txtcaja21.Enabled = True Then
        txtcaja21.Select()
      Else
        BtnGuardar.Select()


      End If
    End If
  End Sub

  Private Sub txtcaja21_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcaja21.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True

      BtnGuardar.Select()


    End If
  End Sub

  Sub VALIDAMAXPESO(txtcaja As System.Windows.Forms.TextBox)
    If txtcaja.Text = "" Then

    Else
      If txtcaja.Text > PesoMax Then
        MessageBox.Show("El peso exede los 40 kg" + PesoMax.ToString + "")

      End If
    End If
  End Sub
  Private Sub txtcaja1_Leave(sender As Object, e As EventArgs) Handles txtcaja1.Leave

    VALIDAMAXPESO(txtcaja1)


  End Sub

  Private Sub txtcaja2_Leave(sender As Object, e As EventArgs) Handles txtcaja2.Leave
    VALIDAMAXPESO(txtcaja2)
  End Sub

  Private Sub txtcaja3_Leave(sender As Object, e As EventArgs) Handles txtcaja3.Leave

    VALIDAMAXPESO(txtcaja3)
  End Sub

  Private Sub txtcaja1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja1.KeyPress
    NumerosyDecimal(txtcaja1, e)
  End Sub

  Public Sub NumerosyDecimal(ByVal CajaTexto As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    'METODO QUE PERMITE ESCRIBIR SOLO NUMEROS Y PUNTO DECIMAL ADEMAS DE PERSMITIR LA TECLA RETROCESO 
    If Char.IsDigit(e.KeyChar) Then
      e.Handled = False
    ElseIf Char.IsControl(e.KeyChar) Then
      e.Handled = False
    ElseIf e.KeyChar = "." And Not CajaTexto.Text.IndexOf(".") Then
      e.Handled = True
    ElseIf e.KeyChar = "." Then
      e.Handled = False
    Else
      e.Handled = True
    End If
  End Sub

  Private Sub txtcaja2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja2.KeyPress
    NumerosyDecimal(txtcaja2, e)
  End Sub

  Private Sub txtcaja3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja3.KeyPress
    NumerosyDecimal(txtcaja3, e)
  End Sub

  Private Sub txtcaja4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja4.KeyPress
    NumerosyDecimal(txtcaja4, e)
  End Sub

  Private Sub txtcaja5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja5.KeyPress
    NumerosyDecimal(txtcaja5, e)
  End Sub

  Private Sub txtcaja6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja6.KeyPress
    NumerosyDecimal(txtcaja6, e)
  End Sub

  Private Sub txtcaja7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja7.KeyPress
    NumerosyDecimal(txtcaja7, e)
  End Sub

  Private Sub txtcaja8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja8.KeyPress
    NumerosyDecimal(txtcaja8, e)
  End Sub

  Private Sub txtcaja9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja9.KeyPress
    NumerosyDecimal(txtcaja9, e)
  End Sub

  Private Sub txtcaja10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja10.KeyPress
    NumerosyDecimal(txtcaja10, e)
  End Sub

  Private Sub txtcaja11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja11.KeyPress
    NumerosyDecimal(txtcaja11, e)
  End Sub

  Private Sub txtcaja12_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja12.KeyPress
    NumerosyDecimal(txtcaja12, e)
  End Sub

  Private Sub txtcaja13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja13.KeyPress
    NumerosyDecimal(txtcaja13, e)
  End Sub

  Private Sub txtcaja14_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja14.KeyPress
    NumerosyDecimal(txtcaja14, e)
  End Sub

  Private Sub txtcaja15_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja15.KeyPress
    NumerosyDecimal(txtcaja15, e)
  End Sub

  Private Sub txtcaja16_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja16.KeyPress
    NumerosyDecimal(txtcaja16, e)
  End Sub

  Private Sub txtcaja17_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja17.KeyPress
    NumerosyDecimal(txtcaja17, e)
  End Sub

  Private Sub txtcaja18_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja18.KeyPress
    NumerosyDecimal(txtcaja18, e)
  End Sub

  Private Sub txtcaja19_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja19.KeyPress
    NumerosyDecimal(txtcaja19, e)
  End Sub

  Private Sub txtcaja20_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja20.KeyPress
    NumerosyDecimal(txtcaja20, e)
  End Sub

  Private Sub txtcaja21_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcaja21.KeyPress
    NumerosyDecimal(txtcaja21, e)
  End Sub

  Private Sub txtcaja4_Leave(sender As Object, e As EventArgs) Handles txtcaja4.Leave
    VALIDAMAXPESO(txtcaja4)
  End Sub

  Private Sub txtcaja5_Leave(sender As Object, e As EventArgs) Handles txtcaja5.Leave
    VALIDAMAXPESO(txtcaja5)
  End Sub



  Private Sub txtcaja7_Leave(sender As Object, e As EventArgs) Handles txtcaja7.Leave
    VALIDAMAXPESO(txtcaja7)
  End Sub

  Private Sub txtcaja8_Leave(sender As Object, e As EventArgs) Handles txtcaja8.Leave
    VALIDAMAXPESO(txtcaja8)
  End Sub

  Private Sub txtcaja9_Leave(sender As Object, e As EventArgs) Handles txtcaja9.Leave
    VALIDAMAXPESO(txtcaja9)
  End Sub

  Private Sub txtcaja10_Leave(sender As Object, e As EventArgs) Handles txtcaja10.Leave
    VALIDAMAXPESO(txtcaja10)
  End Sub

  Private Sub txtcaja11_Leave(sender As Object, e As EventArgs) Handles txtcaja11.Leave
    VALIDAMAXPESO(txtcaja11)
  End Sub

  Private Sub txtcaja6_Leave(sender As Object, e As EventArgs) Handles txtcaja6.Leave
    VALIDAMAXPESO(txtcaja6)
  End Sub

  Private Sub txtcaja12_Leave(sender As Object, e As EventArgs) Handles txtcaja12.Leave
    VALIDAMAXPESO(txtcaja12)
  End Sub

  Private Sub txtcaja13_Leave(sender As Object, e As EventArgs) Handles txtcaja13.Leave
    VALIDAMAXPESO(txtcaja13)
  End Sub

  Private Sub txtcaja14_Leave(sender As Object, e As EventArgs) Handles txtcaja14.Leave
    VALIDAMAXPESO(txtcaja14)
  End Sub

  Private Sub txtcaja15_Leave(sender As Object, e As EventArgs) Handles txtcaja15.Leave
    VALIDAMAXPESO(txtcaja15)
  End Sub

  Private Sub txtcaja16_Leave(sender As Object, e As EventArgs) Handles txtcaja16.Leave
    VALIDAMAXPESO(txtcaja16)
  End Sub

  Private Sub txtcaja17_Leave(sender As Object, e As EventArgs) Handles txtcaja17.Leave
    VALIDAMAXPESO(txtcaja17)
  End Sub

  Private Sub txtcaja18_Leave(sender As Object, e As EventArgs) Handles txtcaja18.Leave
    VALIDAMAXPESO(txtcaja18)
  End Sub

  Private Sub txtcaja19_Leave(sender As Object, e As EventArgs) Handles txtcaja19.Leave
    VALIDAMAXPESO(txtcaja19)
  End Sub

  Private Sub txtcaja20_Leave(sender As Object, e As EventArgs) Handles txtcaja20.Leave
    VALIDAMAXPESO(txtcaja20)
  End Sub

  Private Sub txtcaja21_Leave(sender As Object, e As EventArgs) Handles txtcaja21.Leave
    VALIDAMAXPESO(txtcaja21)
  End Sub

  Private Sub txtcaja1_TextChanged(sender As Object, e As EventArgs) Handles txtcaja1.TextChanged

  End Sub
#End Region
End Class