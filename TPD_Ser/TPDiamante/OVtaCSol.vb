Imports System.Data.SqlClient
Imports System.Data
Public Class OVtaCSol

    Dim dvdet As New DataView
    Dim dvfildet As New DataView
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim valor As Decimal
        Dim nreg As Decimal


        For Each row As DataGridViewRow In Me.DGVEncOrdVta.Rows

            valor = row.Cells(10).Value
            If row.Cells(10).Value <> 0 Then
                dvfildet.RowFilter = "Filtro ='" & DGVEncOrdVta.Item(1, DGVEncOrdVta.CurrentRow.Index).Value.ToString & "'"

                nreg = dvfildet.Count

                For Each fila As DataRowView In dvfildet
                    nreg &= -1

                    If nreg = 0 Then


                    Else

                    End If


                    If fila("Cantidad") = 0 Then



                    Else




                    End If

                Next









            End If


        Next


        'For Each renglon As DataRowView In dvfildet

        '    If renglon("Cantidad") = 0 Then

        '       lo que sea

        '    Else

        '       step -1


        '    End If

        'Next
    End Sub
    Sub cargar_registros()

        Dim DTRefacciones As New DataTable

        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT T6.U_NAME AS UsrCaptura,T0.CreateDate AS FchCreacion,T0.DocEntry AS OrdVta,T0.CardCode AS CodClte,"
        SQLTPD &= "T0.CardName AS Clte,T0.DocTotal AS TotalPed,T1.ItemCode AS Filtro,T1.ItemCode AS Articulo,T1.Dscription AS Descripcion,"
        SQLTPD &= "T4.ItmsGrpNam as Linea,T1.Quantity AS Solicitado,T2.OnHand AS Existencia,T1.Quantity - T2.OnHand AS Solicitar,"
        SQLTPD &= "T1.Price AS Precio,(T1.Quantity - T2.OnHand) * T1.Price  AS MontVtaSol,T1.LineNum AS Partida  "
        SQLTPD &= "INTO #T_ORDSOL  FROM ORDR T0 INNER JOIN RDR1 T1 ON T0.DocEntry = T1.DocEntry  "
        SQLTPD &= "LEFT JOIN OITW T2 ON T1.ItemCode = T2.ItemCode AND T2.WhsCode = 01 "
        SQLTPD &= "LEFT JOIN OITM T3 ON T1.ItemCode = T3.ItemCode "
        SQLTPD &= "LEFT JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod "
        SQLTPD &= "LEFT JOIN OSLP T5 ON T0.SlpCode = T5.SlpCode "
        SQLTPD &= "INNER JOIN OUSR T6  ON T0.UserSign = T6.USERID "
        SQLTPD &= "WHERE T0.PaidToDate = 0 AND T0.CANCELED = 'N' AND T0.DocStatus = 'O' AND T1.Quantity - T2.OnHand > 0 "
        SQLTPD &= "AND T3.ItmsGrpCod <> 187 "

        SQLTPD &= "SELECT * FROM #T_ORDSOL; "

        SQLTPD &= "SELECT rank() OVER (ORDER BY Articulo,Descripcion,Linea,Existencia) as Num,Articulo AS Filtro,Articulo,Descripcion,Linea,SUM(Solicitado) AS Solicitado,Existencia,"
        SQLTPD &= "SUM(Solicitar) AS Solicitar,SUM(MontVtaSol) / SUM(Solicitar) AS PrcProm,SUM(MontVtaSol)  AS MontVtaSol,CAST('0' AS int) AS PzaSol "
        SQLTPD &= "FROM #T_ORDSOL  GROUP BY Articulo,Descripcion,Linea,Existencia; "


        SQLTPD &= "DROP TABLE #T_ORDSOL  "


        ' Nuevo objeto Dataset   
        Dim DsVtasDet As New DataSet

        With comando
            ' Asignar el sql para seleccionar los datos de la tabla Maestro   
            .CommandText = SQLTPD
            .Connection = conexion2
        End With

        '/***************************parte de codigo'
        With Adaptador
            .SelectCommand = comando
            ' llenar el dataset   
            .Fill(DsVtasDet)
        End With

        Dim dvenc As New DataView


        DsVtasDet.Tables(0).TableName = "DetOrdVta"
        DsVtasDet.Tables(1).TableName = "EncOrdVta"
        dvfildet.Table = DsVtasDet.Tables("DetOrdVta")
        dvdet.Table = DsVtasDet.Tables("DetOrdVta")

        With DGVDetOrdVta
            .DataSource = dvdet
            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = True
            .AllowUserToAddRows = False
            'Color de linea del grid
            .Columns(0).HeaderText = "Usurario Ventas"
            .Columns(0).Width = 90

            .Columns(1).HeaderText = "Fecha Creación"
            .Columns(1).Width = 70

            .Columns(2).HeaderText = "Orden de Vta"
            .Columns(2).Width = 45

            .Columns(3).HeaderText = "Clave Cliente"
            .Columns(3).Width = 50

            .Columns(4).HeaderText = "Nombre Cliente"
            .Columns(4).Width = 175

            .Columns(5).HeaderText = "Total del Pedido"
            .Columns(5).Width = 60
            .Columns(5).DefaultCellStyle.Format = "###,###,###.##"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(6).Visible = False

            .Columns(7).HeaderText = "Articulo"
            .Columns(7).Width = 100

            .Columns(8).HeaderText = "Descripción"
            .Columns(8).Width = 180

            .Columns(9).Visible = False

            .Columns(10).HeaderText = "Pedido del Cliente"
            .Columns(10).Width = 55
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(10).DefaultCellStyle.Format = "###,###,###"


            .Columns(11).HeaderText = "Stock Actual"
            .Columns(11).Width = 55
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(11).DefaultCellStyle.Format = "###,###,###"

            .Columns(12).HeaderText = "Piezas Faltantes"
            .Columns(12).Width = 55
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(12).DefaultCellStyle.Format = "###,###,###"

            .Columns(13).HeaderText = "$ Precio Vta"
            .Columns(13).Width = 60
            .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(13).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(14).HeaderText = "$ Monto Vta Pzas Faltantes"
            .Columns(14).Width = 70
            .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(14).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(15).Visible = False

        End With



        dvenc.Table = DsVtasDet.Tables("EncOrdVta")
        '**********************************************************************************************************************************

        With DGVEncOrdVta
            .DataSource = dvenc 'DsVtasDet.Tables("EncOrdVta")
            '.ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = False
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            '.MultiSelect = True
            .AllowUserToAddRows = False

            .Columns(0).HeaderText = "#"
            .Columns(0).Width = 30
            .Columns(0).ReadOnly = True

            .Columns(1).Visible = False

            .Columns(2).HeaderText = "Articulo"
            .Columns(2).Width = 100
            .Columns(2).ReadOnly = True

            .Columns(3).HeaderText = "Descripción"
            .Columns(3).Width = 325
            .Columns(3).ReadOnly = True

            .Columns(4).HeaderText = "Línea"
            .Columns(4).Width = 100
            .Columns(4).ReadOnly = True

            .Columns(5).HeaderText = "Pedido del Cliente"
            .Columns(5).Width = 55
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(5).DefaultCellStyle.Format = "###,###,###"
            .Columns(5).ReadOnly = True

            .Columns(6).HeaderText = "Stock Actual"
            .Columns(6).Width = 55
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(6).DefaultCellStyle.Format = "###,###,###"
            .Columns(6).ReadOnly = True

            .Columns(7).HeaderText = "Piezas Faltantes"
            .Columns(7).Width = 55
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(7).DefaultCellStyle.Format = "###,###,###"
            .Columns(7).ReadOnly = True

            .Columns(8).HeaderText = "$ Precio Promedio Vta"
            .Columns(8).Width = 60
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(8).ReadOnly = True

            .Columns(9).HeaderText = "$ Monto Vta Pzas Faltantes"
            .Columns(9).Width = 70
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(9).ReadOnly = True

            .Columns(10).HeaderText = "Pzas a Solicitar"
            .Columns(10).Width = 52
            .Columns(10).DefaultCellStyle.Format = "###,###,###"
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).ReadOnly = False


        End With


        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With

    End Sub

    Private Sub DGVEncOrdVta_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles DGVEncOrdVta.SelectionChanged
        BuscaOrdenes()
    End Sub

    Sub BuscaOrdenes()
        Try
            dvdet.RowFilter = "Filtro ='" & DGVEncOrdVta.Item(1, DGVEncOrdVta.CurrentRow.Index).Value.ToString & "'"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub OVtaCSol_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        cargar_registros()
        BuscaOrdenes()

        With DGVEncOrdVta
            'Establecemos la celda actual
            '
            .CurrentCell = .Rows(0).Cells(10)

            ' Y la ponemos en modo de edición.
            '
            .BeginEdit(True)
        End With

    End Sub
    Private Sub DGVEncOrdVta_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVEncOrdVta.CellEndEdit
        Try
            DGVEncOrdVta.CurrentRow.Cells(10).Value = Convert.ToDecimal(DGVEncOrdVta.CurrentRow.Cells(10).Value)
        Catch
        End Try
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        ' obtener indice de la columna 
        Dim columna As Integer = DGVEncOrdVta.CurrentCell.ColumnIndex
        ' comprobar si la celda en edicin corresponde a la columna 0 o 1
        If columna = 10 Then
            ' Obtener caracter 
            Dim caracter As Char = e.KeyChar
            If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
                'Me.Text = e.KeyChar 
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub

    Private Sub DGVEncOrdVta_EditingControlShowing(sender As System.Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGVEncOrdVta.EditingControlShowing
        ' referencia a la celda 
        Dim validar As TextBox = CType(e.Control, TextBox)
        ' agregar el controlador de eventos para el KeyPress 
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub

End Class