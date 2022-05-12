Option Explicit On

' Para acceder a OleDB  
Imports System.Data.OleDb

Public Class Form1

    Private Sub Form1_Load( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        btnLoad.Text = "Visualizar hoja"

    End Sub

    Private Sub btnLoad_Click( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles btnLoad.Click

        Cargar(DataGridView1, "Libro1.xls", "sheet1")

    End Sub

    Sub Cargar( _
        ByVal dgView As DataGridView, _
        ByVal SLibro As String, _
        ByVal sHoja As String)

        'HDR=YES : Con encabezado  
        Dim cs As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                           "Data Source=" & SLibro & ";" & _
                           "Extended Properties=""Excel 8.0;HDR=YES"""
        Try
            ' cadena de conexión  
            Dim cn As New OleDbConnection(cs)

            If Not System.IO.File.Exists(SLibro) Then
                MsgBox("No se encontró el Libro: " & _
                        SLibro, MsgBoxStyle.Critical, _
                        "Ruta inválida")
                Exit Sub
            End If

            ' se conecta con la hoja sheet 1  
            Dim dAdapter As New OleDbDataAdapter("Select * From [" & sHoja & "$]", cs)

            Dim datos As New DataSet

            ' agrega los datos  
            dAdapter.Fill(datos)

            With DataGridView1
                ' llena el DataGridView  
                .DataSource = datos.Tables(0)

                ' DefaultCellStyle: formato currency   
                'para los encabezados 1,2 y 3 del DataGrid  
                .Columns(1).DefaultCellStyle.Format = "c"
                .Columns(2).DefaultCellStyle.Format = "c"
                .Columns(3).DefaultCellStyle.Format = "c"
            End With
        Catch oMsg As Exception
            MsgBox(oMsg.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Class
