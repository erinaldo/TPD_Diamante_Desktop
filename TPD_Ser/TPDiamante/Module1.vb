Module Module1

  Public FchServer As DateTime
  Public vSerie As String = ""
  Public vRutaPdf As String = ""
  Public NomUsuario As String = ""
  Public IdUsuario As String = ""
  Public vCodAgte As String = 0
  Public vCorreo As String = ""
  Public vPswmail As String = ""
  Public vCorreoVta As String = ""
  Public vCCorreo As String = ""
  Public VEsAgente As String = 0
  Public StrProd As String = conexion_universal.CadenaSBO_Diamante

  'CAMBIAR ESTA LINEA AQUI AL TRMINAR PRUEBAS
  Public StrTpm As String = conexion_universal.CadenaSQL 'BASE DE DATOS ORIGINAL DE TPD
  Public StrCon As String = conexion_universal.CadenaSQLSAP 'BASE DE DATOS ORIGINAL DE SAP

  Public agenteSC As String
  Public mesSC As String
  Public anioSC As String

  Public agenteLOTot As String
  Public mesLOTot As String
  Public anioLOTot As String

  '------TRASPASO
  Public AlmacenD As String
  Public AlmacenOri As String
  '---------------


  '----------Inventario
  Public ItemcodeInv As String
  Public Almacen As String

  Public NumDoc As String
  'Public ValorInv As String

  Public sCliente As String

  'Variable compartida entre CapOrdvta y Ventana para elegir la visita '
  Public num_ordern As String
  Public cliente_visita As String
  'Fin de variables compartidas entre CapOrdvta y Ventana para elegir la visita'

  'RECIBO DE MATERIALES-CALENDARIO
  'var para guardar numero del mes
  Public mes As Integer
  Public aux As Integer
  Public anio As Integer
  Public almacenCalen As String

  'variables para mostrar detalles de la orden
  Public detalle As Integer


  'variables del MÓDULO Inventario-FACTURAS
  Public NumCli As String
  Public NomCli As String

  'variables del MÓDULO CREAR ORDEN DE VENTA
  Public Count As Integer


  '****USUARIOS
  Public UsrTPM As String = ""
  Public AlmTPM As String = String.Empty


  ' '''*****VARIABLES GARANTIAS
  ' ''' 
  ' '''
  Public Factura As String
  Public FecFact As String
  Public CodCliente As String
  Public NomCliente As String
  Public GarAlm As String
  Public Articulo As String
  Public Descripcion As String
  Public Linea As String
  Public Proveedor As String
  Public CantidadGar As String
  Public GarId As String

  Public NumRenglon As Integer

  Public FecRecAlm As Date
    ' '''
    ''*****FIN VARIABLES GARANTIAS


    'Variable Devoluciones

    Public FacturaDev As String
    Public FecFactDev As String
    Public CodClienteDev As String
    Public NomClienteDev As String
    Public GarAlmDev As String
    Public ArticuloDev As String
    Public DescripcionDev As String
    Public LineaDev As String
    Public ProveedorDev As String
    Public CantidadGarDev As String
    Public GarIdDev As String

    Public NumRenglonDev As Integer

    Public FecRecAlmDev As Date


    '*********************

    ''*****VARIABLES Vale de salida
    Public VSArticulo As String
  Public VSDescripcion As String
  Public VSLinea As String

  Public PosRen As Integer

  Public VSMotivo As String
  Public VSComentarios As String
  Public VSEntrega As String

  '' FIN VARIABLES Vale de salida

  ''*****VARIABLES Diferencias de precios
  Public DPFactura As String
  Public DPFecFact As Date
  Public DPArticulo As String
  Public DPDescripcion As String
  Public DPLinea As String
  Public DPProveedor As String
  Public DPComentarios As String
  Public DPComentariosDir As String

  Public DPPosRen As Integer

  Public ClienteNombre As String
  Public Paqueteria_Nombre As String
  Public Cajas As Integer
  ''' FIN VARIABLES Diferencias de precios


  Public CerrarSCClientes As Boolean = False


  Public band_carga_combustible As Boolean = False

  Public Function QuitarCaracteres(cadena As String, Optional chars As String = "'*+&^¨" + Chr(34) + Chr(10)) As String
    '"'+*/()=<>{}[]^+"
    'String = "'+*/()=<>{}[]^+"
    '#.:<>{}[]^+,;_-/*?¿!$%&/¨Ññ()='áéíóúÁÉÍÓÚ¡
    'Chr(34) Son comillas
    'Chr(13) = Retorno de carro - (mueve el cursor hacia el lado izquierdo)
    'Chr(10) = New Line (gotas cursor una línea hacia abajo)

    Dim i As Integer
    Dim nCadena As String
    On Error Resume Next
    'Asignamos valor a la cadena de trabajo para
    'no modificar la que envía el cliente.
    nCadena = cadena
    For i = 1 To Len(chars)
      nCadena = Replace(nCadena, Mid(chars, i, 1), "")
    Next i
    'Devolvemos la cadena tratada
    QuitarCaracteres = nCadena
  End Function

  Public Function GridAExcel(ByVal ElGrid As DataGridView) As Boolean
    'Creamos las variables
    Dim exApp As New Microsoft.Office.Interop.Excel.Application
    Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
    Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

    Try
      'Añadimos el Libro al programa, y la hoja al libro
      exLibro = exApp.Workbooks.Add
      exHoja = exLibro.Worksheets.Add()
      ' ¿Cuantas columnas y cuantas filas?
      Dim NCol As Integer = ElGrid.ColumnCount
      Dim NRow As Integer = ElGrid.RowCount
      'Aqui recorremos todas las filas, y por cada fila todas las columnas
      'y vamos escribiendo.
      For i As Integer = 1 To NCol
        exHoja.Cells.Item(1, i) = ElGrid.Columns(i - 1).Name.ToString
      Next
      For Fila As Integer = 0 To NRow - 1
        For Col As Integer = 0 To NCol - 1
          exHoja.Cells.Item(Fila + 2, Col + 1) =
                  ElGrid.Rows(Fila).Cells(Col).Value
        Next
      Next

      'Titulo en negrita, Alineado al centro y que el tamaño de la columna
      'se ajuste al texto
      exHoja.Rows.Item(1).Font.Bold = 1
      exHoja.Rows.Item(1).HorizontalAlignment = 3
      exHoja.Columns.AutoFit()
      'Aplicación visible
      exApp.Application.Visible = True
      exHoja = Nothing
      exLibro = Nothing
      exApp = Nothing
    Catch ex As Exception
      MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
      Return False
    End Try
    Return True
  End Function

  Public Function EstiloGrid(ByVal Grid As DataGridView, ByRef ds As DataSet) As Boolean
    Try
      With Grid
        .DataSource = ds.Tables(0)
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        '.RowHeadersVisible = False
        .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
        .MultiSelect = True
        .AllowUserToAddRows = False
        .ReadOnly = True
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .RowHeadersWidth = 25
      End With

    Catch ex As Exception

    End Try

  End Function

  Public Sub mObtenDatosPrincipales(ByVal sUser As String)
    Try
      Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
        Dim da As New SqlClient.SqlDataAdapter("select Id_Usuario,Almacen from usuarios " +
                                                        " where Id_Usuario='" + sUser + "'", SqlConnection)

        Dim ds As New DataSet
        da.Fill(ds)

        UsrTPM = DirectCast(ds.Tables(0).Rows(0).Item(0), String)
        AlmTPM = DirectCast(ds.Tables(0).Rows(0).Item(1), String)

      End Using
    Catch ex As Exception

    End Try
  End Sub

  Public Sub mCreaExcel(ByVal Nombre As String, ByVal Grid As DataGridView, ByVal ini As DateTime, ByVal fin As DateTime, ByVal iOrient As Integer)
    Try
      Dim exApp As New Microsoft.Office.Interop.Excel.Application
      Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

      'Añadimos el Libro al programa
      exLibro = exApp.Workbooks.Add

      ' ¿Cuantas columnas y cuantas filas?
      Dim NCol As Integer = Grid.ColumnCount
      Dim NRow As Integer = Grid.RowCount

      If iOrient <> 0 Then
        ''Cambiamos orientacion ala hola
        exLibro.Worksheets("Hoja1").PageSetup.Orientation = iOrient
      End If

      ''Combinamos celdas
      exLibro.Worksheets("Hoja1").Cells.Range("A1:C1").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("A2:C2").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("A3:C3").Merge(True)

      ''aplicamos un color de fondo ala celda o rango de celdas
      exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15

      ''Cambiamos orientacion ala hola
      exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de " + Nombre
      exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Del: " + ini.Date
      exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Al: " + fin.Date

      exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(5, 1).Font.Bold = 1



      'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
      For i As Integer = 1 To NCol
        exLibro.Worksheets("Hoja1").Cells.Item(5, i) = Grid.Columns(i - 1).Name.ToString
      Next

      For Fila As Integer = 0 To NRow - 1
        For Col As Integer = 0 To NCol - 1
          exLibro.Worksheets("Hoja1").Cells.Item(Fila + 6, Col + 1) = Grid.Rows(Fila).Cells(Col).Value
        Next
      Next

      'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
      exLibro.Worksheets("Hoja1").Rows.Item(5).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Rows.Item(5).HorizontalAlignment = 3
      exLibro.Worksheets("Hoja1").Rows.Item(5).Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Columns.AutoFit()
      exLibro.Worksheets("Hoja1").name = "Reporte de " + Nombre

      'Aplicación visible
      exLibro.Worksheets.Application.Visible = True

      exLibro = Nothing
      exApp = Nothing

    Catch ex As Exception

    End Try
  End Sub

 Public Sub ExportarDatosExcel(ByVal DataGridView1 As DataGridView, ByVal descripcion_tabla As String)
  Try
   Dim m_Excel As New Microsoft.Office.Interop.Excel.Application
   m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait
   m_Excel.Visible = True
   Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook = m_Excel.Workbooks.Add
   Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet = objLibroExcel.Worksheets(1)
   With objHojaExcel
    .Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
    .Activate()
    'Encabezado  
    .Range("A1:L1").Merge()
    .Range("A1:L1").Value = "Tracto Partes Diamante"
    .Range("A1:L1").Font.Bold = True
    .Range("A1:L1").Font.Size = 17
    'Copete  
    .Range("A2:L2").Merge()
    .Range("A2:L2").Value = descripcion_tabla
    .Range("A2:L2").Font.Bold = True
    .Range("A2:L2").Font.Size = 15

    Const primeraLetra As Char = "A"
    Const primerNumero As Short = 3
    Dim Letra As Char, UltimaLetra As Char
    Dim Numero As Integer, UltimoNumero As Integer
    Dim cod_letra As Byte = Asc(primeraLetra) - 1
    Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
    Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator
    'Establecer formatos de las columnas de la hija de cálculo  
    Dim strColumna As String = ""
    Dim LetraIzq As String = ""
    Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
    Letra = primeraLetra
    Numero = primerNumero
    Dim objCelda As Microsoft.Office.Interop.Excel.Range
    For Each c As DataGridViewColumn In DataGridView1.Columns
     If c.Name.ToString = "Borrar" Then
      Continue For
     End If
     If c.Visible Then
      If Letra = "Z" Then
       Letra = primeraLetra
       cod_letra = Asc(primeraLetra)
       cod_LetraIzq += 1
       LetraIzq = Chr(cod_LetraIzq)
      Else
       cod_letra += 1
       Letra = Chr(cod_letra)
      End If
      strColumna = LetraIzq + Letra + Numero.ToString
      objCelda = .Range(strColumna, Type.Missing)
      objCelda.Value = c.HeaderText
      objCelda.EntireColumn.Font.Size = 11
      'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format  
      If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
       objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
      End If
     End If
    Next

    Dim objRangoEncab As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
    objRangoEncab.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
    UltimaLetra = Letra
    Dim UltimaLetraIzq As String = LetraIzq

    'CARGA DE DATOS  
    Dim i As Integer = Numero + 1

    For Each reg As DataGridViewRow In DataGridView1.Rows
     LetraIzq = ""
     cod_LetraIzq = Asc(primeraLetra) - 1
     Letra = primeraLetra
     cod_letra = Asc(primeraLetra) - 1
     For Each c As DataGridViewColumn In DataGridView1.Columns
      If c.Name.ToString = "Borrar" Then
       Continue For
      End If
      If c.Visible Then
       If Letra = "Z" Then
        Letra = primeraLetra
        cod_letra = Asc(primeraLetra)
        cod_LetraIzq += 1
        LetraIzq = Chr(cod_LetraIzq)
       Else
        cod_letra += 1
        Letra = Chr(cod_letra)
       End If
       strColumna = LetraIzq + Letra
       ' acá debería realizarse la carga  
       If c.Name.ToString = "validado" Then
        .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", (IIf(reg.Cells(c.Index).Value = "1", "SI", "NO")))
       Else
        .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
       End If

       '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))  
       '.Range(strColumna + i, strColumna + i).In()  

      End If
     Next
     Dim objRangoReg As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
     objRangoReg.Rows.BorderAround()
     objRangoReg.Select()
     i += 1
    Next
    UltimoNumero = i

    'Dibujar las líneas de las columnas  
    LetraIzq = ""
    cod_LetraIzq = Asc("A")
    cod_letra = Asc(primeraLetra)
    Letra = primeraLetra
    For Each c As DataGridViewColumn In DataGridView1.Columns
     If c.Name.ToString = "Borrar" Then
      Continue For
     End If
     If c.Visible Then
      objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
      objCelda.BorderAround()
      If Letra = "Z" Then
       Letra = primeraLetra
       cod_letra = Asc(primeraLetra)
       LetraIzq = Chr(cod_LetraIzq)
       cod_LetraIzq += 1
      Else
       cod_letra += 1
       Letra = Chr(cod_letra)
      End If
     End If
    Next

    'Dibujar el border exterior grueso  
    Dim objRango As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
    objRango.Select()
    objRango.Columns.AutoFit()
    objRango.Columns.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
   End With

   objHojaExcel.Rows.Item(3).Font.Bold = 1
   For ee As Integer = 1 To (DataGridView1.Rows.Count + 3)
    objHojaExcel.Rows.Item(ee).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter

   Next
   m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault
  Catch ex As Exception
   MsgBox(ex.Message.ToString)
  End Try

 End Sub

 'Varible para revisión
 Public Bandera As Boolean = False
 Public Usuario As String = ""

End Module
