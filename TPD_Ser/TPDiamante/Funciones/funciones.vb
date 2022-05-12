Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Module funciones
 Public StrProdf As String = conexion_universal.CadenaSBO_Diamante
 Public StrTpmf As String = conexion_universal.CadenaSQL
 Public StrConf As String = conexion_universal.CadenaSQLSAP

 Public Enum TipoDeDato As Integer
  Cadena
  Pesos
  Porecentaje
  ForzarPorcentaje
  NumDecimal
  Entero
 End Enum

 Dim SQL As New Comandos_SQL

 Public Sub Llena_Combo(ByRef Combo As ComboBox, Query As String, Member As String, Value As String)
  Try
   Combo.Items.Clear()

   Dim ConsutaLista As String

   Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpmf)
    Dim DSetTablas As New DataSet
    ConsutaLista = Query
    Dim daGDepto As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

    daGDepto.Fill(DSetTablas, "Inf")

    Combo.DataSource = DSetTablas.Tables("Inf")
    Combo.DisplayMember = Member
    Combo.ValueMember = Value
    Combo.SelectedIndex = 0
   End Using
  Catch
   MsgBox("Se presentó un problema al llenar información de combo")
  End Try
 End Sub

 Public Function Actualizar_Operacion_Entrega(UserAccion As String, UserId As Int16, Status As String, Accion As String, Documento As String, Optional UserAccion2 As String = "", Optional UserId2 As Int16 = 0) As Boolean
  Dim QueryUpdate As String

  QueryUpdate = "UPDATE Operacion_Entrega SET " & UserAccion & " = '" & UserId & "', Status = '" & Status & "', DateUpdate = GETDATE(), Action='" & Accion & "' "
  If Trim(UserAccion2) = "" Then
   QueryUpdate &= " , " & UserAccion2 & " = '" & UserId2 & "'"
  End If

  QueryUpdate &= "WHERE DocEntry = '" & Documento & "' "
  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(QueryUpdate, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
  Catch ex As Exception
   MsgBox("Error al Actualizar el Status en la Orden: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   Return False
  End Try
  Return True
 End Function

 Public Function Actualizar_Operacion_Entrega_Revision(UserAccion As String, UserId As Int16, Status As String, Accion As String, Documento As Integer, Optional UserAccion2 As String = "", Optional UserId2 As Int16 = 0) As Boolean
  Dim QueryUpdate As String

  QueryUpdate = "UPDATE Operacion_Entrega SET " & UserAccion & " = '" & UserId & "', Status = '" & Status & "', Action='" & Accion & "' "
  If Trim(UserAccion2) = "" Then
   QueryUpdate &= " , " & UserAccion2 & " = '" & UserId2 & "'"
  End If

  QueryUpdate &= "WHERE DocEntry = '" & Documento & "' "
  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(QueryUpdate, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
  Catch ex As Exception
   MsgBox("Error al Actualizar el Status en la Orden: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   Return False
  End Try
  Return True
 End Function




 Public Function GetNombreEmpleado(KeyCode As Integer) As String
  Dim SQL As String
  Dim NombreEmpleado As String = ""
  SQL = "SELECT Name FROM Operacion_Empleado WHERE KeyCode = '" + KeyCode + "'"

  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(SQL, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
   'RECORRE LA CONSULTA
   If (conexion_universal.rd_s.Read) Then
    NombreEmpleado = rd_s.Item("Name")
   End If
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
  Catch ex As Exception
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
  End Try
  Return NombreEmpleado
 End Function

 Public Function GetNotaCredito(NumFactura As String, FolioDocto As String) As String
  Dim SQL As String
  Dim NumNotaCredito As String = ""
  'Esta busqueda es para el proceso contable
  'SQL = "SELECT T3.DocNum
  '       FROM SBO_TPD.dbo.ITR1 INNER JOIN
  '       SBO_TPD.dbo.OINV ON ITR1.TransId = OINV.TransId
  '       INNER JOIN SBO_TPD.dbo.ITR1 AS ITR1_1 ON ITR1.ReconNum = ITR1_1.ReconNum AND ITR1_1.SrcObjTyp = 13
  '       INNER JOIN SBO_TPD.dbo.ITR1 AS ITR1_2 ON ITR1.ReconNum = ITR1_2.ReconNum AND ITR1_2.SrcObjTyp = 14
  '       INNER JOIN SBO_TPD.dbo.ORIN AS T3 ON ITR1_2.TransId = T3.TransId
  '       INNER JOIN SBO_TPD.dbo.RIN1 AS T4 ON T3.DocEntry = T4.DocEntry
  '       WHERE OINV.DocNum = " & NumFactura & "		-----> Numero de factura o lista
  '       AND ITR1.IsCredit = 'D'
  '       AND T4.ItemCode <> 'DESCUENTO P.P'"

  'Esta busqueda es para el buscarlo en los campos definidos por el usuario
  SQL = "SELECT CASE WHEN DocNum IS NULL THEN '' ELSE DocNum END as NCR FROM SBO_TPD.dbo.ORIN WHERE CAST(U_Factura as varchar(max)) = '" & NumFactura & "' AND Comments like '%" & FolioDocto & "%'"

  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(SQL, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
   'RECORRE LA CONSULTA
   If (conexion_universal.rd_s.Read) Then
    NumNotaCredito = rd_s.Item("NCR")
   End If
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
  Catch ex As Exception
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
  End Try
  Return NumNotaCredito
 End Function

 Public Function RevisoPermiso(Usuario As String, Proceso As String, Optional SoloSaberSiExiste As Boolean = False) As Integer
  Dim Acceso As Integer = 0
  Try
   SQL.conectarTPM()

   'Primero verifico si existe
   Dim con As New SqlConnection(StrTpm)
   Dim cmd As New SqlCommand()
   Dim strSelect As String = ""
   Dim dReader As SqlDataReader

   If SoloSaberSiExiste = True Then
    strSelect = "SELECT COUNT(*) existe FROM Accesos_Usuarios_TPD WHERE IdUser = '" & Usuario & "' AND Proceso = '" & Proceso & "'"
    cmd.Connection = con
    cmd.CommandText = strSelect
    con.Open()
    cmd.CommandType = CommandType.Text
    dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

    While dReader.Read()
     If dReader("existe") >= 1 Then
      dReader.Close()
      con.Close()
      SQL.Cerrar()
      Return 1
     Else
      dReader.Close()
      con.Close()
      SQL.Cerrar()
      Return 0
     End If
    End While
   End If

   'Como si existe porque llego hasta aca ahora verifico cual es el status de acceso
   strSelect = "SELECT * FROM Accesos_Usuarios_TPD WHERE IdUser = '" & Usuario & "' AND Proceso = '" & Proceso & "'"
   cmd.Connection = con
   cmd.CommandText = strSelect
   con.Open()
   cmd.CommandType = CommandType.Text
   dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
   While dReader.Read()
    Acceso = CInt(dReader("Acceso"))
   End While

   dReader.Close()
   con.Close()
   SQL.Cerrar()
   Return Acceso
  Catch ex As Exception
   Return Acceso
  End Try
 End Function

 Public Function menuAccesoEspecial(Id_Usuario As String) As Boolean
  Dim menuEspecial As Boolean = False
  Try
   SQL.conectarTPM()

   Dim con As New SqlConnection(StrTpm)
   Dim cmd As New SqlCommand()
   Dim strSelect As String = ""
   Dim dReader As SqlDataReader

   strSelect = "SELECT menuAccesoEspecial FROM Usuarios WHERE Id_Usuario = '" & Id_Usuario & "'"
   cmd.Connection = con
   cmd.CommandText = strSelect
   con.Open()
   cmd.CommandType = CommandType.Text
   dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

   While dReader.Read()
    If (dReader("menuAccesoEspecial") = "NO") Then
     Return False
    Else
     Return True
    End If
   End While
  Catch ex As Exception
   Return menuEspecial
  End Try
 End Function

 Sub exporta2Excel(TituloReporte As String, Columnas As String(), TipoColumna As TipoDeDato(), Visible As Boolean(), DataGrid As DataGridView)
  Dim oExcel As Object = CreateObject("Excel.Application")
  Dim oBook As Object
  Dim oSheet As Object

  Dim i As Integer = 0
  Dim Ciclo As Integer = 0
  Dim nCols As Integer = 0 'Numero de columnas visibles
  Dim totalCols As Integer = 0 'Total de columnas a pasar
  Dim ABC As String() = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ"}

  Dim Rangos As String = ""
  Dim Rangos2 As String = ""
  Dim celda As String()

  totalCols = UBound(Columnas)

  i = 0
  For Ciclo = 0 To totalCols
   If Visible(Ciclo) = True Then
    i = i + 1
   End If
  Next

  nCols = i - 1
  ReDim celda(nCols)
  For Ciclo = 0 To nCols
   celda(Ciclo) = ""
  Next

  'Abrimos un nuevo libro
  'oExcel = CreateObject("Excel.Application")

  'System.Threading.Thread.Sleep(15000) ' 1 segundo

  oBook = oExcel.workbooks.add
  oSheet = oBook.worksheets(1)

  'Declaramos el nombre de las columnas
  i = 0
  For Ciclo = 0 To totalCols
   If Visible(Ciclo) = True Then
    oSheet.range(ABC(i) & "3").value = Columnas(Ciclo)
    i = i + 1
   End If
  Next

  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:" & ABC(nCols) & "3").font.bold = True
  oSheet.Range("A3:" & ABC(nCols) & "3").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)
  Dim fila_dt As Integer = 0
  Dim fila_dt_excel As Integer = 0
  Dim tanto_porcentaje As String = ""
  Dim marikona As Integer = 0

  Dim total_reg As Integer = 0

  total_reg = DataGrid.RowCount
  For fila_dt = 0 To total_reg - 1
   i = 0
   For Ciclo = 0 To totalCols
    If Visible(Ciclo) = True Then
     If TipoColumna(Ciclo) <> TipoDeDato.ForzarPorcentaje Then
      celda(i) = IIf(IsDBNull(DataGrid.Item(Ciclo, fila_dt).Value), 0, DataGrid.Item(Ciclo, fila_dt).Value)
     Else
      celda(i) = IIf(IsDBNull(DataGrid.Item(Ciclo, fila_dt).Value), 0, DataGrid.Item(Ciclo, fila_dt).Value / 100)
     End If
     i = i + 1
    End If
   Next

   fila_dt_excel = fila_dt + 4

   'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
   i = 0
   For Ciclo = 0 To totalCols
    If Visible(Ciclo) = True Then
     oSheet.range(ABC(i) & fila_dt_excel).value = celda(i)
     i = i + 1
    End If
   Next
  Next

  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  oSheet.columns("A:l").entirecolumn.autofit()

  'Formato numerico
  i = 0
  For Ciclo = 0 To totalCols
   If Visible(Ciclo) = True Then
    If TipoColumna(Ciclo) = TipoDeDato.Pesos Then
     oExcel.Worksheets("Hoja1").Columns(ABC(i)).NumberFormat = "$ ###,###,##0.00"
    ElseIf TipoColumna(Ciclo) = TipoDeDato.NumDecimal Then
     oExcel.Worksheets("Hoja1").Columns(ABC(i)).NumberFormat = "###,###,##0.00"
    ElseIf TipoColumna(Ciclo) = TipoDeDato.Porecentaje Or TipoColumna(Ciclo) = TipoDeDato.ForzarPorcentaje Then
     oExcel.Worksheets("Hoja1").Columns(ABC(i)).NumberFormat = "0.00 %"
    ElseIf TipoColumna(Ciclo) = TipoDeDato.Entero Then
     oExcel.Worksheets("Hoja1").Columns(ABC(i)).NumberFormat = "0"
    End If
    i = i + 1
   End If
  Next

  'Coloco autofiltro
  Dim objRangoFiltro As Microsoft.Office.Interop.Excel.Range = oSheet.Range("A3:" & ABC(nCols) & "3")
  objRangoFiltro.AutoFilter(1)

  'Titulo del reporte
  oSheet.range("A1").value = TituloReporte

  oSheet.range("C1").value = Rangos
  oSheet.range("C2").value = Rangos2

  'System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  Try
   oExcel.visible = True
  Catch ex As Exception
   MsgBox("Se presento el siguiente error: " & ex.Message)
  End Try

  GC.Collect()
  oSheet = Nothing
  oBook = Nothing
  oExcel = Nothing
 End Sub

 'REFERENCIA A MICROSOFT EXCEL 12.0
 'EL NUMERO 12.0 HACE REFERENCIA A MS OFFICE 2007
 'EL NUMERO 11.0 HARIA REFERENCIA A MS OFFICE 2003

 'METODO PARA CREAR EL REPORTE EN MICROSOFT EXCEL
 Public Sub ExportarGridToExcel(ByVal DATAGRIDVIEW1 As DataGridView, ByVal TITULO As String)
  Dim M_EXCEL As New Excel.Application
  M_EXCEL.Cursor = Excel.XlMousePointer.xlWait
  M_EXCEL.Visible = True
  Dim OBJLIBROEXCEL As Excel.Workbook = M_EXCEL.Workbooks.Add
  Dim OBJHOJAEXCEL As Excel.Worksheet = OBJLIBROEXCEL.Worksheets(1)
  With OBJHOJAEXCEL
   .Visible = Excel.XlSheetVisibility.xlSheetVisible
   .Activate()
   'ENCABEZADO. 
   .Range("A1:L1").Merge()
   .Range("A1:L1").Value = "TITULO."
   .Range("A1:L1").Font.Bold = True
   .Range("A1:L1").Font.Size = 16
   'TEXTO DESPUES DEL ENCABEZADO. 
   .Range("A2:L2").Merge()
   .Range("A2:L2").Value = TITULO
   .Range("A2:L2").Font.Bold = True
   .Range("A2:L2").Font.Size = 10
   'ESPACIO.
   .Range("A3:L3").Merge()
   .Range("A3:L3").Value = ""
   .Range("A3:L3").Font.Bold = True
   .Range("A3:L3").Font.Size = 10
   'ESTILO A TITULOS DE LA TABLA.
   .Range("A4:P4").Font.Bold = True
   'ESTABLECER TIPO DE LETRA AL RANGO DETERMINADO.
   .Range("A1:P100").Font.Name = "TAHOMA"
   'LOS DATOS SE REGISTRAN A PARTIR DE LA COLUMNA A, FILA 4.
   Const PRIMERALETRA As Char = "A"
   Const PRIMERNUMERO As Short = 4
   Dim LETRA As Char, ULTIMALETRA As Char
   Dim NUMERO As Integer, ULTIMONUMERO As Integer
   Dim COD_LETRA As Byte = Asc(PRIMERALETRA) - 1
   Dim SEPDEC As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
   Dim SEPMIL As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator
   Dim STRCOLUMNA As String = ""
   Dim LETRAIZQ As String = ""
   Dim COD_LETRAIZQ As Byte = Asc(PRIMERALETRA) - 1
   LETRA = PRIMERALETRA
   NUMERO = PRIMERNUMERO
   Dim OBJCELDA As Excel.Range
   For Each C As DataGridViewColumn In DATAGRIDVIEW1.Columns
    If C.Visible Then
     If LETRA = "Z" Then
      LETRA = PRIMERALETRA
      COD_LETRA = Asc(PRIMERALETRA)
      COD_LETRAIZQ += 1
      LETRAIZQ = Chr(COD_LETRAIZQ)
     Else
      COD_LETRA += 1
      LETRA = Chr(COD_LETRA)
     End If
     STRCOLUMNA = LETRAIZQ + LETRA + NUMERO.ToString
     OBJCELDA = .Range(STRCOLUMNA, Type.Missing)
     OBJCELDA.Value = C.HeaderText
     OBJCELDA.EntireColumn.Font.Size = 10
     'ESTABLECE UN FORMATO A LOS NUMEROS POR DEFAULT.
     'OBJCELDA.ENTIRECOLUMN.NUMBERFORMAT = C.DEFAULTCELLSTYLE.FORMAT
     If C.ValueType Is GetType(Decimal) OrElse C.ValueType Is GetType(Double) Then
      OBJCELDA.EntireColumn.NumberFormat = "#" + SEPMIL + "0" + SEPDEC + "00"
     End If
    End If
   Next
   Dim OBJRANGOENCAB As Excel.Range = .Range(PRIMERALETRA + NUMERO.ToString, LETRAIZQ + LETRA + NUMERO.ToString)
   OBJRANGOENCAB.BorderAround(1, Excel.XlBorderWeight.xlMedium)
   ULTIMALETRA = LETRA
   Dim ULTIMALETRAIZQ As String = LETRAIZQ
   'CARGAR DATOS DEL DATAGRIDVIEW. 
   Dim I As Integer = NUMERO + 1
   For Each REG As DataGridViewRow In DATAGRIDVIEW1.Rows
    LETRAIZQ = ""
    COD_LETRAIZQ = Asc(PRIMERALETRA) - 1
    LETRA = PRIMERALETRA
    COD_LETRA = Asc(PRIMERALETRA) - 1
    For Each C As DataGridViewColumn In DATAGRIDVIEW1.Columns
     If C.Visible Then
      If LETRA = "Z" Then
       LETRA = PRIMERALETRA
       COD_LETRA = Asc(PRIMERALETRA)
       COD_LETRAIZQ += 1
       LETRAIZQ = Chr(COD_LETRAIZQ)
      Else
       COD_LETRA += 1
       LETRA = Chr(COD_LETRA)
      End If
      STRCOLUMNA = LETRAIZQ + LETRA
      'AQUI SE REALIZA LA CARGA DE DATOS.
      .Cells(I, STRCOLUMNA) = IIf(IsDBNull(REG.ToString), "", REG.Cells(C.Index).Value)
      'ESTABLECE LAS PROPIEDADES DE LOS DATOS DEL DATAGRIDVIEW POR DEFAULT.
      '.CELLS(I, STRCOLUMNA) = IIF(ISDBNULL(REG.(C.DATAPROPERTYNAME)), C.DEFAULTCELLSTYLE.NULLVALUE, REG(C.DATAPROPERTYNAME)) 
      '.RANGE(STRCOLUMNA + I, STRCOLUMNA + I).IN() 
     End If
    Next
    Dim OBJRANGOREG As Excel.Range = .Range(PRIMERALETRA + I.ToString, STRCOLUMNA + I.ToString)
    OBJRANGOREG.Rows.BorderAround()
    OBJRANGOREG.Select()
    I += 1
   Next
   ULTIMONUMERO = I
   'DIBUJAR LAS LÍNEAS DE LAS COLUMNAS.
   LETRAIZQ = ""
   COD_LETRAIZQ = Asc("A")
   COD_LETRA = Asc(PRIMERALETRA)
   LETRA = PRIMERALETRA
   For Each C As DataGridViewColumn In DATAGRIDVIEW1.Columns
    If C.Visible Then
     OBJCELDA = .Range(LETRAIZQ + LETRA + PRIMERNUMERO.ToString, LETRAIZQ + LETRA + (ULTIMONUMERO - 1).ToString)
     OBJCELDA.BorderAround()
     If LETRA = "Z" Then
      LETRA = PRIMERALETRA
      COD_LETRA = Asc(PRIMERALETRA)
      LETRAIZQ = Chr(COD_LETRAIZQ)
      COD_LETRAIZQ += 1
     Else
      COD_LETRA += 1
      LETRA = Chr(COD_LETRA)
     End If
    End If
   Next
   'DIBUJAR EL BORDER EXTERIOR GRUESO DE LA TABLA. 
   Dim OBJRANGO As Excel.Range = .Range(PRIMERALETRA + PRIMERNUMERO.ToString, ULTIMALETRAIZQ + ULTIMALETRA + (ULTIMONUMERO - 1).ToString)
   OBJRANGO.Select()
   OBJRANGO.Columns.AutoFit()
   OBJRANGO.Columns.BorderAround(1, Excel.XlBorderWeight.xlMedium)
  End With
  M_EXCEL.Cursor = Excel.XlMousePointer.xlDefault
 End Sub

 Public Function ConvertFechaNormalToSQL(FechaIn As String) As String
  Dim Fecha_Temporal As Date
  Dim Fechaout As String
  Fecha_Temporal = CDate(FechaIn)
  Fechaout = CStr(Fecha_Temporal.Year) & "-" & CStr(Fecha_Temporal.Month) & "-" & CStr(Fecha_Temporal.Day)
  Return Fechaout
 End Function

End Module
