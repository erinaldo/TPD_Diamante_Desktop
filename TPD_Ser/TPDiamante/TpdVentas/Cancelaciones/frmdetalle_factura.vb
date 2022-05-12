Imports System.Data.SqlClient

Public Class frmDetalleOrden
    Dim vf As String
    Private Sub frmdetalle_factura_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'MANDA A LLAMAR EL ESTILO DEL GRID
        estilo_grid_detalle()

        'VARIBALE PARA ALMACENAR LA CONSULTA
        Dim SQLObtener As String = ""

        'BORRA LOS DATOS DEL GRID
        If dgvdetalle_f.RowCount > 0 Then
            dgvdetalle_f.Rows.Clear()
        End If

        '-----

        'AGREGA LA FACTURA SELECCIONADA
        txtfactura_cancelar.Text = vf

        Try
            'CONECTA A LA BASE DE DATOS DEL SAP
            conexion_universal.conectar_sap()
            'ALMACENA LA CADENA DE LA CONSULTA
            SQLObtener = "select T0.DocEntry, T0.DocNum, T0.Series, T0.CardCode, T0.CardName, "
            SQLObtener &= "T0.CntctCode, ISNULL(T2.CntctPrsn, '') AS Contacto, T0.DocDate, T0.DocDueDate, T0.TaxDate, T0.SlpCode, T1.SlpName AS Agente, "
            SQLObtener &= "T0.Comments, ISNULL(T3.WhsCode, '') AS WhsCode, T3.ItemCode, T3.Dscription, T3.Quantity, T3.U_BXP_ListaP AS LP, "
            SQLObtener &= "CONVERT(varchar(50), CONVERT(MONEY, T3.Price), 1) as Price, "
            SQLObtener &= "CONVERT(varchar(50), CONVERT(MONEY, T3.LineTotal), 1) as LineTotal, "
            SQLObtener &= "CONVERT(varchar(50), CONVERT(MONEY, (T0.DocTotalSy - T0.VatSumSy)) , 1) as SubTotal, "
            SQLObtener &= "CONVERT(varchar(50), CONVERT(MONEY, T0.VatSumSy), 1) as VatSumSy, "
            SQLObtener &= "CONVERT(varchar(50), CONVERT(MONEY, T0.DocTotalSy), 1) as DocTotalSy  "
            SQLObtener &= "from OINV T0 INNER JOIN INV1 T3 ON T0.DocEntry = T3.DocEntry "
            SQLObtener &= "INNER JOIN OSLP T1 ON T1.SlpCode = T0.SlpCode "
            SQLObtener &= "INNER JOIN OCRD T2 ON T0.CardCode = T2.CardCode "
            SQLObtener &= "WHERE T0.DOCNUM = " + vf + " "

            'ALMACENA EN UN COMMAND LA CONSULTA
            conexion_universal.slq_s = New SqlCommand(SQLObtener, conexion_universal.conexion_uni_sap)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                txtalmacen.Text = conexion_universal.rd_s.Item("WhsCode")
                txtcliente.Text = conexion_universal.rd_s.Item("CardCode")
                txtnombre.Text = conexion_universal.rd_s.Item("CardName")
                txtcontacto.Text = conexion_universal.rd_s.Item("Contacto")
                txtfecha.Text = conexion_universal.rd_s.Item("DocDate")
                txtfecha_ven.Text = conexion_universal.rd_s.Item("DocDueDate")
                txtfecha_doc.Text = conexion_universal.rd_s.Item("TaxDate")
                txtagente.Text = conexion_universal.rd_s.Item("Agente")
                txtcom_factura.Text = conexion_universal.rd_s.Item("Comments")
                txtsubtotal.Text = conexion_universal.rd_s.Item("SubTotal").ToString
                txtimpuesto.Text = conexion_universal.rd_s.Item("VatSumSy").ToString
                txttotal.Text = conexion_universal.rd_s.Item("DocTotalSy").ToString

                '-----

                'LLENA EL DATA GRID
                If dgvdetalle_f.RowCount > 0 Then
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvdetalle_f.Rows.Add(rd_s.Item("ItemCode"), rd_s.Item("Dscription").ToString, CInt(rd_s.Item("Quantity")).ToString, rd_s.Item("LP").ToString,
                        rd_s.Item("Price").ToString, rd_s.Item("LineTotal").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvdetalle_f
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvdetalle_f.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        'CIERRA EL READER
                        conexion_universal.rd_s.Close()
                        conexion_universal.cerrar_conectar_sap()
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                Else
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvdetalle_f.Rows.Add(rd_s.Item("ItemCode"), rd_s.Item("Dscription").ToString, CInt(rd_s.Item("Quantity")).ToString, rd_s.Item("LP").ToString,
                        rd_s.Item("Price").ToString, rd_s.Item("LineTotal").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvdetalle_f
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvdetalle_f.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        'CIERRA EL READER
                        conexion_universal.rd_s.Close()
                        conexion_universal.cerrar_conectar_sap()
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                End If

                '-----
            End While
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            'CIERRA LA CONEXION DEL SAP
            conexion_universal.cerrar_conectar_sap()
        Catch ex As Exception
            'MANDA EL MENSAJE DE ERROR
            MsgBox("Error de conexión o Conuslta en detalle de factura: " & ex.Message, MsgBoxStyle.Critical, "Error Conexion o consulta")
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar_sap()
            Return
        End Try
    End Sub

    Public Sub ValorFactura(ByRef f As String)
        'ASIGNA AL TXT DE FACTURA EL VALOR QUE MANDARON
        vf = f
    End Sub
    Sub estilo_grid_detalle() 'ESTILO DEL GRID DE LINEAS
        With Me.dgvdetalle_f
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = False
            'ARTICULO
            .AllowUserToAddRows = False
            .Columns("ItemCode").Width = 130
            .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("ItemCode").ReadOnly = False
            'DESCRIPCION
            .Columns("Dscription").Width = 250
            .Columns("Dscription").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Dscription").ReadOnly = False
            'CANTIDAD
            .Columns("Quantity").Width = 60
            .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Quantity").ReadOnly = False
            'LISTA DE PRECIO
            .Columns("ListaP").Width = 70
            .Columns("ListaP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(3).DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("ListaP").ReadOnly = False
            'PRECIO
            .Columns("Price").Width = 100
            .Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Price").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("Price").ReadOnly = False
            'IMPORTE
            .Columns("LineTotal").Width = 100
            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("LineTotal").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("LineTotal").ReadOnly = False
        End With
    End Sub

End Class