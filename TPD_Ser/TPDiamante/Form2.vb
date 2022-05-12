
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient


Public Class FacturasDetalleCliente

  Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)
  Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)

  Public DvDetalle As New DataView
  Public Origen As String = ""

  Private Sub FacturasDetalleCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Try

      conexion2.Open()
      Dim cmd4 As SqlCommand = Nothing
      cmd4 = New SqlCommand("DetalleFacturasCliente", conexion2)
      cmd4.CommandType = CommandType.StoredProcedure

      cmd4.Parameters.AddWithValue("@CardCode", Module1.NumCli)

      cmd4.ExecuteNonQuery()
      cmd4.Connection.Close()
      Dim da2 As New SqlDataAdapter
      da2.SelectCommand = cmd4
      da2.SelectCommand.Connection = conexion2


      ''--------------------------------------------
      Dim DsVtas As New DataSet
      da2.Fill(DsVtas, "DsVtas")

      DsVtas.Tables(0).TableName = "Detalle"


      DvDetalle.Table = DsVtas.Tables("Detalle")

      DGDetalle.DataSource = DvDetalle

      DisenoGrid()

    Catch ex As Exception
      MsgBox(ex.Message)
    Finally
      If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
        conexion2.Close()
      End If
    End Try
  End Sub


  Private Sub DisenoGrid()
    '-------Diseño de DATAGRID Totales
    With Me.DGDetalle
      '.DataSource = DtAgte
      .ReadOnly = True
      'Color de Renglones en Grid
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
      'Color de linea del grid

      DGDetalle.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try

        .Columns(0).HeaderText = "#"
        .Columns(0).Width = 30
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "Documento"
        .Columns(1).Width = 60
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(2).HeaderText = "Fecha de Contab."
        .Columns(2).Width = 80
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(3).HeaderText = "Cliente"
        .Columns(3).Width = 150
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(4).HeaderText = "Comentarios"
        .Columns(4).Width = 120
        .Columns(4).DefaultCellStyle.Format = "#,##0"
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(5).HeaderText = "Fecha de Venc."
        .Columns(5).Width = 80
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(6).HeaderText = "Número de Ref."
        .Columns(6).Width = 120
        '.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(7).HeaderText = "Doc. electrónico "
        .Columns(7).Width = 120
        '.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(8).HeaderText = "Tipo de doc."
        .Columns(8).Width = 80
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(9).HeaderText = "Total"
        .Columns(9).Width = 90
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(9).DefaultCellStyle.Format = "$ #,###,###.##"

      Catch ex As Exception

      End Try


    End With
  End Sub

  Private Sub DGDetalle_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalle.CellClick
    Try

      FacturaAuditoria.Close()
      FacturaConsulta.Close()

      ControlEnvios.Close()

    Catch ex As Exception

    End Try

    'Try
    '    'MsgBox("cierre de form")

    'Catch ex As Exception
    '    MsgBox(ex.Message)
    'End Try

    If Origen = "CambioComisiones" Then
      CambioComisiones.TBDocNum.Text = DGDetalle.Item(1, DGDetalle.CurrentCell.RowIndex).Value.ToString()
    Else
      Module1.NumDoc = DGDetalle.Item(1, DGDetalle.CurrentCell.RowIndex).Value
    End If

    Me.Close()

    'Try
    '    'For Each frms As Form In Application.OpenForms
    '    '    'Filtramos solo aquellos de tipo TextBox  
    '    '    frms.Close()
    '    'Next

    '    FacturaAuditoria.Close()

    'Catch ex As Exception
    '    MsgBox(ex.Message)
    'End Try

    'Dim frm As New FacturaAuditoria
    If Origen = "CambioComisiones" Then
      CambioComisiones.Show()
      Origen = ""
    Else
      FacturaAuditoria.Show()
    End If
  End Sub

End Class