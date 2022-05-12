Imports System.Data.SqlClient

Public Class Entrada_Salida_Material

    Dim SQL As New Comandos_SQL()

    Private Sub Entrada_Salida_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EntradaMaterial()
    End Sub

    Sub EntradaMaterial()
        SQL.conectarTPM()
        dgvEntradaSalida.DataSource = SQL.ConsultarTabla("select DocNum as 'No. Factura',DocDate as 'Fecha Documento',DateDeliver as 'Fecha de llegada',TrnspCode as 'Paqueteria',Storer as 'Recibe en Almacén',
                                                        Plant as 'Recibe en Planta',Unity as 'Unidad',Quantity as 'Cantidad',CodeTracking as 'Código de rastreo',Observation as 'Observaciones' from EntradaMaterial")
        SQL.Cerrar()
        codigocombo()
    End Sub

    Private Sub dgvEntradaSalida_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEntradaSalida.CellLeave


    End Sub

    Private Sub dgvEntradaSalida_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dgvEntradaSalida.KeyPress

    End Sub

    Private Sub dgvEntradaSalida_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvEntradaSalida.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                SQL.conectarTPM()
                Dim NoFactura = dgvEntradaSalida.CurrentRow.Cells(0).Value.ToString()
                Dim FechaDocumento = dgvEntradaSalida.CurrentRow.Cells(1).Value.ToString()
                Dim FechaLlegada = dgvEntradaSalida.CurrentRow.Cells(2).Value.ToString()
                Dim Paqueteria = dgvEntradaSalida.CurrentRow.Cells(3).Value.ToString()
                Dim RecibeAlmacen = dgvEntradaSalida.CurrentRow.Cells(4).Value.ToString()
                Dim RecibePlanta = dgvEntradaSalida.CurrentRow.Cells(5).Value.ToString()
                Dim Unidad = dgvEntradaSalida.CurrentRow.Cells(6).Value.ToString()
                Dim Cantidad = dgvEntradaSalida.CurrentRow.Cells(7).Value.ToString()
                Dim CodigoRastreo = dgvEntradaSalida.CurrentRow.Cells(8).Value.ToString()
                Dim Observaciones = dgvEntradaSalida.CurrentRow.Cells(9).Value.ToString()

                If SQL.SiExiste("select * from EntradaMaterial where DocNum = " + NoFactura) Then
                    MessageBox.Show("La factura ingresada ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                If NoFactura <> "" And FechaDocumento <> "" And FechaLlegada <> "" And Paqueteria <> "" And RecibeAlmacen <> "" And RecibePlanta <> "" And Unidad <> "" And Cantidad <> "" And CodigoRastreo <> "" And Observaciones <> "" Then

                    If SQL.EjecutarComando("insert into EntradaMaterial (DocNum,DocDate,DateDeliver,TrnspCode,Storer,Plant,Unity,Quantity,CodeTracking,Observation)values(" + NoFactura + ",'" + FechaDocumento +
                                        "','" + FechaLlegada + "'," + Paqueteria + ",'" + RecibeAlmacen + "','" + RecibePlanta + "','" + Unidad + "'," + Cantidad + "," + CodigoRastreo + ",'" + Observaciones + "')") Then
                        MessageBox.Show("Se cargo correctamente la información " + NoFactura)
                    End If
                Else
                    MessageBox.Show("Es necesario llenar todos los campos")
                End If
                SQL.Cerrar()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
        End If
    End Sub

    Sub codigocombo()
        Try
      Dim string_conexion As String = conexion_universal.CadenaSQL
      Dim sqlcon As New SqlConnection(string_conexion)
            sqlcon.Open()
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            da = New SqlDataAdapter("SELECT id,Nombre FROM Almacenistas", sqlcon)
            da.Fill(ds, "Almacenistas")
            Dim dgccomboCodigo As DataGridViewComboBoxColumn = TryCast(dgvEntradaSalida.Columns("No. Factura"), DataGridViewComboBoxColumn)
            dgccomboCodigo.DataSource = ds.Tables.Item("Almacenistas")
            dgccomboCodigo.DisplayMember = "Nombre"
            sqlcon.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub


End Class