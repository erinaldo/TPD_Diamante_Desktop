'IMPORTA LAS LIBRERIAS REQUERIDAS
Imports System.Data.SqlClient

Public Class frmvalor_inventario
    'VARIABLES DE CONEXION
    Dim dt As DataTable
  Dim cn_temp As New SqlConnection(conexion_universal.CadenaSQLSAP)
  Dim consultaCombo As SqlCommand
    Dim consultaCheck As SqlCommand
    Dim res As SqlDataReader
    Dim Lista As New ArrayList
    'VARIABLE DE FECHA
    Dim fecha As String
    'PARA BUSQUEDA AUTOMATICA DE LINEA
    Private dv As New DataView
    Private Sub frmvalor_inventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmblineas.Items.Add("TODAS")
        'CONECTA A LA BASE DE DATOS
        metodos.conectar()
        'RELLENAR EL COMBO BOX DE LINEAS
        consultaCombo = New SqlCommand("select ItmsGrpNam from OITB where ItmsGrpCod <> 200 and ItmsGrpCod <> 150 order by ItmsGrpNam", conexion)
        res = consultaCombo.ExecuteReader()
        While res.Read
            Lista.Add(res.GetValue(0).ToString)
        End While
        'ASIGNA LA PALABRA SI SE QUIEREN TODAS LAS LINEAS
        Lista.Add("TODAS")

        res.Close()
        With cmblineas
            .DropDownStyle = ComboBoxStyle.DropDown
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.ListItems
            .DataSource = Lista
            .DisplayMember = "ItmsGrpNam"
            '.ValueMember = "ItmsGrpNam"
        End With
        'cmblineas.SelectedItem = -1
        cmblineas.SelectedIndex = -1
        'MANDA A LLAMAR EL METODO DE ESTILO PARA LOS GRID
        estilo_grid_lineas()
        estilo_grid_res()

        'MANDA A LLAMAR AL METODO AUTOCOMPLETAR DEL COMBO BOX
        'autoCompletarTextbox(cmblineas)
    End Sub

    Sub estilo_grid_lineas() 'ESTILO DEL GRID DE LINEAS
        With Me.dgvlineas
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = False
            .AllowUserToAddRows = False
            .Columns(0).Width = 170
            .Columns(0).ReadOnly = False
        End With
    End Sub
    Sub estilo_grid_res() 'ESTILO DEL GRID DE LINEAS
        With Me.dgvres
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = False
            .AllowUserToAddRows = False
            .Columns(0).Width = 172
            .Columns(0).ReadOnly = False

            .Columns(1).Width = 100
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(1).DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns(1).ReadOnly = False
        End With
    End Sub


    Sub agregar_linea() 'METODO AGREGAR LINEA
        ''VARIABLE QUE ALMACENA EL VALOR SELECCIONADO EN EL COMBO BOX
        'Dim linea As String = cmblineas.Text
        ''RECORRE EL DATA GRID VIEW DE LINEAS PARA VALIDAR SI LA LINEA YA EXISTE
        'For i As Integer = 0 To dgvlineas.RowCount - 2
        '    If linea = dgvlineas.Item(0, i).Value.ToString Then 'VALIDA SI YA ESTA AGREGADA LA LINEA
        '        'MANDA MENSAJE DE ERROR A PANTALLA
        '        MessageBox.Show("La línea ya ha sido agregada.", "Error al agregar línea",
        '        MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        cmblineas.Focus()
        '        'COLOCA EN 0 LA FILA QUE SE IBA AGREGAR
        '        'dgvlineas.CurrentCell = dgvlineas.Rows(i).Cells(0)

        '        'RETORNA EL VALOR
        '        Return
        '    End If
        'Next
        'Try 'CAPTURA EL ERROR AL AGREGAR LINEA
        '    Dim n As Integer = dgvlineas.Rows.Add() 'TOTAL DE FILAS EN EL GRID
        '    dgvlineas.Rows(n).Cells(0).Value = cmblineas.Text 'ALMACENA EL VALOR QUE TRAIGA EL COMBOBOX
        'Catch ex As Exception
        '    MsgBox("Error al agregar la linea " + ex.Message, MsgBoxStyle.Critical, "Error de visualización") 'MANDA MENSAJE DE ERROR
        'End Try





        





        'VARIABLE QUE ALMACENA EL VALOR SELECCIONADO EN EL COMBO BOX
        Dim linea As String = cmblineas.Text
        If dgvlineas.RowCount > 0 Then 'VALIDA SI YA ESTA AGREGADA LA LINEA
            For i As Integer = 0 To dgvlineas.RowCount - 1 'FOR QUE RECORRE EL DATA GRID VIEW DE LINEAS
                If linea = dgvlineas.Item(0, i).Value.ToString Then 'COMPARA QUE EL VALOR DEL COMBO NO ESTE EN EL GRID
                    'MANDA MENSAJE DE ERROR A PANTALLA
                    MessageBox.Show("La línea ya ha sido agregada.", "Error al agregar línea",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'COLOCA EN 0 LA FILA QUE SE IBA AGREGAR
                    dgvlineas.CurrentCell = dgvlineas.Rows(i).Cells(0)
                    'RETORNA EL VALOR
                    Return
                End If 'COMPARA QUE EL VALOR DEL COMBO NO ESTE EN EL GRID
            Next 'FIN FOR QUE RECORRE EL DATA GRID VIEW DE LINEAS
            Try 'CAPTURA EL ERROR
                Me.dgvlineas.Rows.Add(linea)
            Catch ex As Exception 'CATCH DE CAPTURA EL ERROR
                MsgBox(ex.Message) 'MANDA MENSAJE DE ERROS
            End Try 'FIN CAPTURA EL ERROR
            With dgvlineas
                'ESTABLECEMOS LA CELDA ACTUAL
                .CurrentCell = .Rows(Me.dgvlineas.Rows.Count - 1).Cells(0)
            End With
        Else 'ELSE VALIDA SI YA ESTA AGREGADA LA LINEA
            Try 'OBTIENE EL ERROR EN CASO DE QUE LO HUBIERA
                Me.dgvlineas.Rows.Add(linea)
                With dgvlineas
                    'ESTABLECE LA CELDA ACTUAL
                    .CurrentCell = .Rows(Me.dgvlineas.Rows.Count - 1).Cells(0)
                End With
            Catch ex As Exception 'CATCH OBTIENE EL ERROR EN CASO DE QUE LO HUBIERA
                'MANDA EL MENSAJE DE ERROR
                MsgBox("Error al agregar la linea: " & ex.Message, MsgBoxStyle.Critical)
            End Try 'FIN OBTIENE EL ERROR EN CASO DE QUE LO HUBIERA
        End If 'FIN VALIDA SI YA ESTA AGREGADA LA LINEA
    End Sub 'FIN METODO AGREGAR LINEA

    Sub agregar_linea_todas() 'METODO AGREGAR TODAS LAS LINEA
        'Dim n As Integer = dgvlineas.Rows.Add()
        'RELLENAR EL COMBO BOX DE LINEAS
        consultaCheck = New SqlCommand("select ItmsGrpNam from OITB where ItmsGrpCod <> 200 and ItmsGrpCod <> 150 order by ItmsGrpNam", conexion)
        res = consultaCheck.ExecuteReader()
        While res.Read
            Try 'CAPTURA EL ERROR AL AGREGAR LINEA
                Dim c As Integer = dgvlineas.Rows.Add() 'TOTAL DE FILAS EN EL GRID
                dgvlineas.Rows(c).Cells(0).Value = res.GetValue(0).ToString 'ALMACENA EL VALOR QUE TRAIGA EL COMBOBOX
            Catch ex As Exception
                MsgBox("Error al agregar la linea " + ex.Message, MsgBoxStyle.Critical, "Error de visualización") 'MANDA MENSAJE DE ERROR
            End Try
        End While
        res.Close()

    End Sub 'FIN METODO AGREGAR LINEA

    Private Sub btnagregar_linea_Click(sender As Object, e As EventArgs) Handles btnagregar_linea.Click
        'VALIDA SI SON TODAS LAS LINEAS O POR FILTRO
        '''''''''''If chbtodas_lineas.Checked = True Then  'SE COMENTO ESTA LINEA SOLO POR PRUEBA.
        If cmblineas.Text = "TODAS" Then
            'MANDA A LLAMAR AL METODO DE TODAS LAS LINEAS
            agregar_linea_todas()
        Else
            'MANDA A LLAMAR EL METODO VALIDAR LINEA
            valida_linea(cmblineas.Text)
            'VALIDA SI EXISTE LA LINEA O NO
            If linea_OK = True Then
                'MANDA A LLAMARA AL METODO DE AGREGAR
                agregar_linea()
            Else
                MsgBox("La linea que desea agregar no existe.", MsgBoxStyle.Critical, "Error de Selección")
            End If
        End If
        'APUNTAR NUEVAMENTE AL COMBO
        cmblineas.Focus()
    End Sub

    Private Sub dgvlineas_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs)
        'PERMITE PODER SELECIONAR LA FILA DEL DATAGRIDVIEW PERO NO EDITARLA
        Dim cellTextBox As TextBox = _
            DirectCast(e.Control, DataGridViewTextBoxEditingControl)
        cellTextBox.ReadOnly = True
    End Sub

    Private Sub btnquitar_linea_Click(sender As Object, e As EventArgs) Handles btnquitar_linea.Click
        Try
            'QUITA LA FILA SELECCIONADA
            Me.dgvlineas.Rows.Remove(Me.dgvlineas.CurrentRow)
        Catch ex As Exception
            'MANDA MENSAJE DE ERROR A LA HORA DE QUITAR LA FILA
            MsgBox("No hay datos que Eliminar. " + ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub cmblineas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmblineas.KeyPress
        'PERMITE CONVERTIR TODAS LAS LETRAS EN MAYUSCULA EN UN COMBOBOX
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub btnconsultar_Click(sender As Object, e As EventArgs) Handles btnconsultar.Click
        'REFRESCA EL DATA GRID VIEW DE RESULTADO
        If dgvres.RowCount > 0 Then
            dgvres.Rows.Clear()
        End If
        'VARIABLE QUE CONCATENEA LAS LINEAS PARA LA CONSULTA
        Dim lineas_filtro As String
        'INICALIZA LA VARIABLE
        lineas_filtro = ""
        'VALIDA QUE TENGA UN VALOR EL DATA GRID VIEW DE LINEA
        If dgvlineas.RowCount = 0 Then
            MsgBox("No hay lineas que mostrar", MsgBoxStyle.Critical, "Dato no Encontrado")
            Return
        End If
        'RECORRE TODO EL DATA GRID VIEW
        For j As Integer = 0 To dgvlineas.RowCount - 1
            If j > 0 Then
                'lineas_filtro = lineas_filtro + "','" + dgvlineas.Item(0, j).Value.ToString + "'"
                lineas_filtro = lineas_filtro + "','" + dgvlineas.Item(0, j).Value.ToString '+ "'"
            Else
                lineas_filtro = lineas_filtro + "'" + dgvlineas.Item(0, j).Value.ToString '+ "'"
            End If
            'AMERICAN', 'BENDIX', 'CLEVITE', 'FLEET MASTER', 'GOODYEAR', 'GRC', 'HOLLAND', 'NATIONAL', 'STEMCO', 'TIMKEN'
        Next 'FIN RECORRE TODO EL DATA GRID VIEW
        'CONCATENA EL ULTIMO APOSTROFE
        lineas_filtro = lineas_filtro + "'"

        'COLOCA LA FECHA EN EL FORMATO DESEADO
        fecha = dtpfecha.Value.ToString("yyyy-MM-dd")
        'OBTIENE Y CONCATENA LAS LINEAS QUE SE REQUIEREN EN LA CONSULTA
        Try 'CAPTURA EL ERROR
            'CONSULTA PARA OBTENER EL VALOR DEL INVENTARIO
            consulta_s = New SqlCommand("SELECT T2.ItmsGrpNam AS LINEA, CONVERT(varchar(50), CONVERT (money, SUM(TransValue)), 1) as valor FROM OINM T0 " +
                                      " INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode " +
                                      " INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod " +
                                      " WHERE DocDate >= '20120101' AND DocDate <= '" + fecha + "' AND T0.Warehouse <> '02'" +
                                      " AND T2.ItmsGrpNam IN (" + lineas_filtro + " ) GROUP BY T2.ItmsGrpNam ", conexion)
            'EJECUTA LA CONSULTA
            respuesta_s = consulta_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While respuesta_s.Read
                If dgvres.RowCount > 0 Then 'VALIDA SI YA TIENE UN DATO EL DATA GRID VIEW
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'Me.dgvres.Rows.Add(respuesta_s.Item("LINEA"), respuesta_s.Item("VALOR").ToString)
                        Me.dgvres.Rows.Add(respuesta_s.Item("LINEA"), respuesta_s.Item("VALOR").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvres
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvres.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical)
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                Else 'ELSE VALIDA SI YA TIENE UN DATO EL DATA GRID VIEW
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvres.Rows.Add(respuesta_s.Item("LINEA"), respuesta_s.Item("VALOR").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvres
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvres.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical)
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                End If 'FIN VALIDA SI YA TIENE UN DATO EL DATA GRID VIEW
            End While
            'CIERRA LA RESPUESTA DEL SQLDATAREADER
            respuesta_s.Close()
        Catch ex As Exception 'CATCH CAPTURA EL ERROR
            MsgBox("Error de consulta o Conexión: " + ex.ToString, MsgBoxStyle.Critical, "Error en conexión")
        End Try 'FIN CAPTURA EL ERROR
    End Sub

    Private Sub btnexportar_Click(sender As Object, e As EventArgs) Handles btnexportar.Click
        '-----------------------------EXPORTAR A EXCEL LOS RESULTADOS OBTENIDOS ---------------------------------------------------
        'VALIDA QUE EL DATAGRID VIEW NO ESTE VACIO
        If dgvres.RowCount > 0 Then
            'CONTADOR DE LAS COLUMNAS DONDE TENDRA QUE EMPEZAR EL EMPATE CON EL EXCEL
            Dim con, confila As Integer
            con = 4
            confila = 0
            'DECLARACIÓN DE VARIABLES PARA LIBRO DE EXCEL CREANDO UN OBJETO DE EXCEL
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
            Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
            Try
                'AÑADIMOS EL LIBRO AL PROGRAMA , Y LA HOJA AL LIBRO
                exLibro = exApp.Workbooks.Add
                exHoja = exLibro.Worksheets.Add()
                ' ¿CUANTAS COLUMNAS Y CUANTAS FILAS?
                Dim NCol As Integer = dgvres.ColumnCount
                Dim NRow As Integer = dgvres.RowCount
                'AQUI Aqui recorremos todas las filas, y por cada fila todas las columnas
                'y vamos escribiendo.
                exHoja.Cells.Item(1, 1) = "Valor del Inventario al " + fecha
                For i As Integer = 1 To NCol
                    'COLOCA EL NOMBRE DE LAS COLUMNAS DEL DATAGRID
                    'exHoja.Cells.Item(1, i) = dgvres.Columns(i - 1).Name.ToString
                    exHoja.Cells.Item(3, 1) = "Lineas"
                    exHoja.Cells.Item(3, 2) = "Valor de Inventario"
                Next
                For Fila As Integer = 0 To NRow - 1
                    For Col As Integer = 0 To NCol - 1
                        exHoja.Cells.Item(Fila + 4, Col + 1) = _
                        dgvres.Rows(Fila).Cells(Col).Value
                    Next
                Next
                'TITULO EN NEGRITA, ALINEADO AL CENTRO Y QUE EL TAMAÑO DE LA COLUMNA
                'COLOCA NEGRITAS LA FILA 1
                exHoja.Rows.Item(1).Font.Bold = 1
                'COLOCA NEGRITAS LA FILA 3
                exHoja.Rows.Item(3).Font.Bold = 1
                'ALINEACION DE LA FILA 3
                exHoja.Rows.Item(3).HorizontalAlignment = 3
                'SE AJUSTE AL TEXTO
                exHoja.Columns.AutoFit()
                'COLOCA EL NOMBRE DE LA HOJA
                exHoja.Name = "Valor del Inventario".ToString
                'MUESTRA EN PANTALLA EL EXCEL
                exApp.Application.Visible = True
                exHoja = Nothing
                exLibro = Nothing
                exApp = Nothing
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
            End Try
        Else
            'MANDA MENSAJE DE ERROR
            MsgBox("No hay datos que Exportar", MsgBoxStyle.Critical, "Datos no Encontrados")
        End If
    End Sub

    Private Sub chbtodas_lineas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles chbtodas_lineas.KeyPress
        If Asc(e.KeyChar) = 13 Or Asc(e.KeyChar) = 9 Then
            btnagregar_linea.Focus()
        End If
    End Sub

    Private Sub chbtodas_lineas_CheckedChanged(sender As Object, e As EventArgs) Handles chbtodas_lineas.CheckedChanged
        If chbtodas_lineas.Checked = True Then
            cmblineas.Enabled = False
        Else
            cmblineas.Enabled = True
        End If
    End Sub

    Private Sub btnlimpiar_Click(sender As Object, e As EventArgs) Handles btnlimpiar.Click
        'REFRESCA EL DATA GRID VIEW DE LINEAS
        If dgvlineas.RowCount > 0 Then
            dgvlineas.Rows.Clear()
        End If
        'REFRESCA EL DATA GRID VIEW DE RESULTADO
        If dgvres.RowCount > 0 Then
            dgvres.Rows.Clear()
        End If
        'COLOCA EN ESTADO DESMARCADO EL CHECKLIST DE TODAS LAS LINEAS
        chbtodas_lineas.Checked = False
        'POSICIONA EL FOCUS EN LA FECHA
        dtpfecha.Focus()
    End Sub
End Class