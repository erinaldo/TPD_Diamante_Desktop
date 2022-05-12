Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Public Class frmBonoMensual
  'Conexiones a la Base de datos
  Public StrProd As String = conexion_universal.CadenaSBO_Diamante
  Public StrTpm As String = conexion_universal.CadenaSQL
  Public StrCon As String = conexion_universal.CadenaSQLSAP

  Dim DvAgentes As New DataView

  Dim DvInformacionGeneral As New DataView
  Dim DvSCGeneral As New DataView
  Dim DvSCClientes As New DataView
  Dim DvSCLineas As New DataView
  Dim DvSCHalcon As New DataView

  Dim DvCboAgentes As New DataView

  Dim SQL As New Comandos_SQL()


  'Dim th As New Threading.Thread(AddressOf Buscar_Informacion)

  Private Sub SCClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    iniciar()
  End Sub

  Private Sub iniciar()
    Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")

    lblEspera.Width = 1051
    lblEspera.Height = 235
    lblEspera.Visible = False

    If UsrTPM <> "MANAGER" And UsrTPM <> "COMERCIAL" And UsrTPM <> "SISTEMAS" Then
      GroupBox3.Visible = False
      btnParametros.Visible = False
      Me.Width = 1290
      Me.Height = 360
    Else
      GroupBox3.Visible = True
      btnParametros.Visible = True
      Me.Width = 1290
      Me.Height = 647
    End If
  End Sub

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    lblEspera.Visible = True
    lblEspera.Refresh()
    Buscar_Informacion(UsrTPM)
    lblEspera.Visible = False
  End Sub

  Private Sub LimpiarCalculos()
    txtPorcentajeMinimoParaBono.Text = ""
    txtPorcCorrGeneral.Text = ""
    txtPorcCorrHalcon.Text = ""
    txtPorcCorrLineas.Text = ""
    txtPorcCorrClientes.Text = ""
    txtImporteBono.Text = ""
    txtPesosCorrGeneral.Text = ""
    txtPesosCorrHalcon.Text = ""
    txtPesosCorrLineas.Text = ""
    txtPesosCorrClientes.Text = ""
    txtObjGeneral.Text = ""
    txtAcumGeneral.Text = ""
    txtObjHalcon.Text = ""
    txtAcumHalcon.Text = ""
    txtObjLineas.Text = ""
    txtAcumLineas.Text = ""
    txtObjClientes.Text = ""
    txtAcumClientes.Text = ""
    txtPorLogradoGeneral.Text = ""
    txtPorLogradoHalcon.Text = ""
    txtPorLogradoLineas.Text = ""
    txtPorLogradoClientes.Text = ""
    txtPorExcedente.Text = ""
    txtBonoExcedente.Text = ""
    txtBonoGeneral.Text = ""
    txtBonoHalcon.Text = ""
    txtBonoLineas.Text = ""
    txtBonoClientes.Text = ""
    txtBonoTotalAlcanzado.Text = ""
  End Sub

  Sub Buscar_Informacion(Agente As String)
    Dim vDiasMes As Integer
    Dim vDiasTrans As Integer


    Dim fecha As String = Me.DtpFechaIni.Value.ToString("yyyy-MM-dd")
    Try
      SQL.conectarTPM()

      LimpiarCalculos()

      Dim mont As String = Month(DtpFechaIni.Value.ToString("dd-MM-yyyy"))
      Dim yea As String = Year(DtpFechaIni.Value.ToString("dd-MM-yyyy"))

      'Reviso, si no existen parametros para el mes y año indicados entoneces me salgo
      Dim RevisaParametros As Integer = SQL.CampoEspecifico("SELECT COUNT(*) Total FROM BonoMensual_ParametrosGenerales WHERE Año = " & yea & " AND Mes = " & mont, "Total")

      If RevisaParametros <= 0 Then
        MessageBox.Show("No existen parametros indicados para el mes y año indicados", "Sin paramentros para el mes " & mont & " del " & yea)
        Exit Sub
      End If

      vDiasMes = SQL.CampoEspecifico("EXEC TPD_DiasHabiles " + mont + ", " + yea, "Dias")
      txtDiasMes.Text = vDiasMes.ToString

      vDiasTrans = SQL.CampoEspecifico("EXEC Indicadores '" + DtpFechaIni.Value.ToString("yyyy-MM-dd") + "'," + "2", "diasTrans")
      txtDiasTranscurridos.Text = vDiasTrans.ToString

      txtDiasRestantes.Text = Convert.ToString(vDiasMes - vDiasTrans)

      Dim DvInfBono As New DataSet
      Dim DsGTotal As New DataSet

      Dim conec = New SqlConnection(StrTpm)
      Dim cmd = New SqlCommand("[ScoreCard_Final_4KPI]", conec)
      cmd.CommandType = CommandType.StoredProcedure
      cmd.Parameters.Add("@fFechaFinal", SqlDbType.Date).Value = Me.DtpFechaIni.Value

      'Variables para rellenar SP----------------------------------------------------
      Dim DiasRestantes As Integer
      DiasRestantes = vDiasMes - vDiasTrans
      'If DiasRestantes = 0 Then
      '  DiasRestantes = 1
      'End If
      cmd.Parameters.Add("@fDiasTrans", SqlDbType.Int).Value = vDiasTrans
      cmd.Parameters.Add("@fDiasMes", SqlDbType.Int).Value = vDiasMes
      cmd.Parameters.Add("@fDiasRest", SqlDbType.Int).Value = DiasRestantes
      cmd.Parameters.Add("@fSucursal", SqlDbType.Int).Value = 99
      '-------------------------------------------------------------------------------
      '-------------------------------------------------------------------------------

      If Agente = "SISTEMAS" Or Agente = "MANAGER" Or Agente = "COMERCIAL" Then
        cmd.Parameters.Add("@fAgente", SqlDbType.Int).Value = 999 ' Busco a todos
      Else
        Dim CodAgte As Integer = CInt(SQL.CampoEspecifico("SELECT CodAgte FROM Usuarios WHERE Id_Usuario = '" & Agente & "'", "CodAgte"))
        cmd.Parameters.Add("@fAgente", SqlDbType.Int).Value = CodAgte ' POr el momento lo mando siempre con 999 y valido despues
      End If

      conec.Open()
      Dim adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()
      adaptador.Fill(DvInfBono)

      'Limpio  tablas
      DvInformacionGeneral.Table = Nothing
      DvAgentes.Table = Nothing
      DvSCGeneral.Table = Nothing
      DvSCClientes.Table = Nothing
      DvSCLineas.Table = Nothing
      DvSCHalcon.Table = Nothing

      'Descargo la informacion de las tablas que tra la consulta
      DvInfBono.Tables(0).TableName = "InfGeneral"

      DvInformacionGeneral.Table = DvInfBono.Tables("InfGeneral")

      DvInfBono.Tables(1).TableName = "InfPorAgente"
      DvAgentes.Table = DvInfBono.Tables("InfPorAgente")
      DgAgentes.DataSource = DvAgentes

      DvInfBono.Tables(2).TableName = "SCGeneral"
      DvSCGeneral.Table = DvInfBono.Tables("SCGeneral")
      
      DvInfBono.Tables(3).TableName = "SCClientes"
      DvSCClientes.Table = DvInfBono.Tables("SCClientes")

      DvInfBono.Tables(4).TableName = "SCLineas"
      DvSCLineas.Table = DvInfBono.Tables("SCLineas")

      DvInfBono.Tables(5).TableName = "SCHalcon"
      DvSCHalcon.Table = DvInfBono.Tables("SCHalcon")

      EstiloAgentes()

      CargaAgente(0)

      SQL.Cerrar()
    Catch ex As Exception
      MessageBox.Show(ex.ToString, "Error al ejecutar los procedimientos almacenados")
    End Try
  End Sub

  '************************************************************************
  'INICIAN ESTILOS
  '************************************************************************
  Sub EstiloAgentes()
    With Me.DgAgentes
      .ReadOnly = True
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        .Columns("slpcode").HeaderText = "Clave"
        .Columns("slpcode").Width = 50
        .Columns("slpcode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("slpname").HeaderText = "Agente"
        .Columns("slpname").Width = 200
        .Columns("ImporteBonoMensual").HeaderText = "  "
        .Columns("ImporteBonoMensual").Width = 100
        .Columns("ImporteBonoMensual").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns("ImporteBonoMensual").DefaultCellStyle.Format = "$ ###,###,##0.00"
      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub

  Private Sub ScoreCardCliente_Activated(sender As Object, e As EventArgs) Handles Me.Activated

  End Sub

  Private Sub btnParametros_Click(sender As Object, e As EventArgs) Handles btnParametros.Click
    Dim forma As Form = frmBonoMensualParametros
    forma.Show()
  End Sub

  Private Sub DgAgentes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgAgentes.CellContentClick
    'CargaAgente(e.RowIndex)
  End Sub

  Private Sub CargaAgente(Renglon As Integer)
    If Renglon >= 0 Then
      Dim NumAgente As Integer

      Dim ExcedenteBase As Double = 0.00
      Dim ExcedentePorcentaje As Double = 0.00
      Dim ExcedenteImporte As Double = 0.00
      Dim PorcentajeLogradoGeneral As Double = 0.00

      For Each rowView As DataRowView In DvInformacionGeneral
        Dim row As DataRow = rowView.Row
        txtPorcentajeMinimoParaBono.Text = Trim(row("MinimoParaBono")).ToString() & "%"
        txtPorcCorrGeneral.Text = Trim(row("Porc_ScoreCard")).ToString() & "%"
        txtPorcCorrHalcon.Text = Trim(row("Porc_Halcon")).ToString() & "%"
        txtPorcCorrLineas.Text = Trim(row("Porc_LineasObjetivo")).ToString() & "%"
        txtPorcCorrClientes.Text = Trim(row("Porc_Clientes")).ToString() & "%"
      Next

      Dim rowInfPA As DataGridViewRow = DgAgentes.Rows(Renglon)
      txtImporteBono.Text = Format(rowInfPA.Cells("ImporteBonoMensual").Value, "$ ###,##0.00")
      NumAgente = rowInfPA.Cells("slpcode").Value

      txtPesosCorrGeneral.Text = Format(CDbl(txtImporteBono.Text) * (CDbl(Val(txtPorcCorrGeneral.Text)) / 100), "$ ###,##0.00")
      txtPesosCorrHalcon.Text = Format(CDbl(txtImporteBono.Text) * (CDbl(Val(txtPorcCorrHalcon.Text)) / 100), "$ ###,##0.00")
      txtPesosCorrLineas.Text = Format(CDbl(txtImporteBono.Text) * (CDbl(Val(txtPorcCorrLineas.Text)) / 100), "$ ###,##0.00")
      txtPesosCorrClientes.Text = Format(CDbl(txtImporteBono.Text) * (CDbl(Val(txtPorcCorrClientes.Text)) / 100), "$ ###,##0.00")

      txtObjGeneral.Text = "$ 0.00"
      txtAcumGeneral.Text = "$ 0.00"
      For Each rowView As DataRowView In DvSCGeneral
        Dim row As DataRow = rowView.Row
        If NumAgente = row("slpcode") Then
          txtObjGeneral.Text = Format(row("objetivo"), "$ ###,##0.00")
          txtAcumGeneral.Text = Format(row("alcanzado"), "$ ###,##0.00")
          Exit For
        End If
      Next

      txtObjHalcon.Text = "$ 0.00"
      txtAcumHalcon.Text = "$ 0.00"
      For Each rowView As DataRowView In DvSCHalcon
        Dim row As DataRow = rowView.Row
        If NumAgente = row("slpcode") Then
          txtObjHalcon.Text = Format(row("objetivo"), "$ ###,##0.00")
          txtAcumHalcon.Text = Format(row("Venta"), "$ ###,##0.00")
          Exit For
        End If
      Next

      txtObjLineas.Text = "$ 0.00"
      txtAcumLineas.Text = "$ 0.00"
      For Each rowView As DataRowView In DvSCLineas
        Dim row As DataRow = rowView.Row
        If NumAgente = row("slpcode") Then
          txtObjLineas.Text = Format(row("objetivo"), "$ ###,##0.00")
          txtAcumLineas.Text = Format(row("Acumulado"), "$ ###,##0.00")
          Exit For
        End If
      Next

      txtObjClientes.Text = "$ 0.00"
      txtAcumClientes.Text = "$ 0.00"
      For Each rowView As DataRowView In DvSCClientes
        Dim row As DataRow = rowView.Row
        If NumAgente = row("CveAgt") Then
          txtObjClientes.Text = Format(row("objetivo"), "##0")
          txtAcumClientes.Text = Format(row("Acumulado"), "##0")
          Exit For
        End If
      Next

      'Ahora calculo porcentajes
      If CDbl(txtObjGeneral.Text) > 0 Then
        txtPorLogradoGeneral.Text = Format((CDbl(txtAcumGeneral.Text) / CDbl(txtObjGeneral.Text)) * 100, "##0.00") & " %"
        PorcentajeLogradoGeneral = Format(CDbl(txtAcumGeneral.Text) / CDbl(txtObjGeneral.Text) * 100, "##0.00")
      Else
        txtPorLogradoGeneral.Text = "0.00 %"
        PorcentajeLogradoGeneral = 0.00
      End If

      If CDbl(txtObjHalcon.Text) > 0 Then
        txtPorLogradoHalcon.Text = Format((CDbl(txtAcumHalcon.Text) / CDbl(txtObjHalcon.Text)) * 100, "##0.00") & " %"
      Else
        txtPorLogradoHalcon.Text = "0.00 %"
      End If
      If CDbl(txtObjLineas.Text) > 0 Then
        txtPorLogradoLineas.Text = Format((CDbl(txtAcumLineas.Text) / CDbl(txtObjLineas.Text)) * 100, "##0.00") & " %"
      Else
        txtPorLogradoLineas.Text = "0.00 %"
      End If
      If CDbl(txtObjClientes.Text) > 0 Then
        txtPorLogradoClientes.Text = Format((CDbl(txtAcumClientes.Text) / CDbl(txtObjClientes.Text)) * 100, "##0.00") & " %"
      Else
        txtPorLogradoClientes.Text = "0.00 %"
      End If

      'Calculo para el excedente
      ExcedentePorcentaje = Format(PorcentajeLogradoGeneral - 100, "##0.00") 'Format((CDbl(Val(txtPorLogradoGeneral.Text)) - 100), "##0.00")
        If ExcedentePorcentaje > 0 Then
          'Muestra el renglon de excedentes
          TableLayoutPanel1.RowStyles.Item(2).SizeType = TableLayoutPanel1.RowStyles.Item(1).SizeType
          TableLayoutPanel1.RowStyles.Item(2).Height = TableLayoutPanel1.RowStyles.Item(1).Height
          ExcedenteBase = CDbl(txtPesosCorrGeneral.Text) * 4  'Obtengo la base del excedente para el calculo en caso de haber excedente
          ExcedenteImporte = Format(ExcedenteBase * (ExcedentePorcentaje / 100), "###,##0.00")

          txtPorExcedente.Text = Format(ExcedentePorcentaje, "##0.00").ToString() & " %"
          txtBonoExcedente.Text = Format(ExcedenteImporte, "$ ###,##0.00")
        Else
          'Oculta el renglon de excedentes
          TableLayoutPanel1.RowStyles.Item(2).SizeType = SizeType.Absolute
          TableLayoutPanel1.RowStyles.Item(2).Height = 0

          ExcedenteBase = 0
          ExcedenteImporte = Format(0.00, "###,##0.00")
          txtPorExcedente.Text = Format(0.00, "##0.00").ToString() & " %"
          txtBonoExcedente.Text = Format(ExcedenteImporte, "$ ###,##0.00")
        End If

        'Cambio colores de porcentajes
        If CDbl(Val(txtPorLogradoGeneral.Text.Replace("%", ""))) < CDbl(Val(txtPorcentajeMinimoParaBono.Text.Replace("%", ""))) Then
          txtPorLogradoGeneral.BackColor = Color.FromArgb(255, 128, 128)
          txtBonoGeneral.Text = "$ 0.00"
        Else
          txtPorLogradoGeneral.BackColor = Color.FromArgb(128, 255, 128)
          txtBonoGeneral.Text = Format(CDbl(Val(txtPorLogradoGeneral.Text.Replace("%", ""))) / 100 * CDbl(txtPesosCorrGeneral.Text), "$ ###,##0.00")
        End If

        If CDbl(Val(txtPorLogradoHalcon.Text.Replace("%", ""))) < CDbl(Val(txtPorcentajeMinimoParaBono.Text.Replace("%", ""))) Then
          txtPorLogradoHalcon.BackColor = Color.FromArgb(255, 128, 128)
          txtBonoHalcon.Text = "$ 0.00"
        Else
          txtPorLogradoHalcon.BackColor = Color.FromArgb(128, 255, 128)
          txtBonoHalcon.Text = Format(CDbl(Val(txtPorLogradoHalcon.Text.Replace("%", ""))) / 100 * CDbl(txtPesosCorrHalcon.Text), "$ ###,##0.00")
        End If

        If CDbl(Val(txtPorLogradoLineas.Text.Replace("%", ""))) < CDbl(Val(txtPorcentajeMinimoParaBono.Text.Replace("%", ""))) Then
          txtPorLogradoLineas.BackColor = Color.FromArgb(255, 128, 128)
          txtBonoLineas.Text = "$ 0.00"
        Else
          txtPorLogradoLineas.BackColor = Color.FromArgb(128, 255, 128)
          txtBonoLineas.Text = Format(CDbl(Val(txtPorLogradoLineas.Text.Replace("%", ""))) / 100 * CDbl(txtPesosCorrLineas.Text), "$ ###,##0.00")
        End If

        If CDbl(Val(txtPorLogradoClientes.Text.Replace("%", ""))) < CDbl(Val(txtPorcentajeMinimoParaBono.Text.Replace("%", ""))) Then
          txtPorLogradoClientes.BackColor = Color.FromArgb(255, 128, 128)
          txtBonoClientes.Text = "$ 0.00"
        Else
          txtPorLogradoClientes.BackColor = Color.FromArgb(128, 255, 128)
          txtBonoClientes.Text = Format(CDbl(Val(txtPorLogradoClientes.Text.Replace("%", ""))) / 100 * CDbl(txtPesosCorrClientes.Text), "$ ###,##0.00")
        End If

        'Bono alcanzado
        txtBonoTotalAlcanzado.Text = Format(ExcedenteImporte + CDbl(txtBonoGeneral.Text) + CDbl(txtBonoHalcon.Text) + CDbl(txtBonoLineas.Text) + CDbl(txtBonoClientes.Text), "$ ###,###,##0.00")
      End If
  End Sub

  Private Sub DgAgentes_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgAgentes.RowEnter
    If Not IsNothing(DvSCGeneral.Table) Then
      CargaAgente(e.RowIndex)
    End If
  End Sub
End Class