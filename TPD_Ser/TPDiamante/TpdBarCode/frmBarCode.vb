Imports ZXing.Common
Imports ZXing
Imports ZXing.QrCode
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Drawing
Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Imports System.Text

Public Class frmBarCode

 Dim options As New QrCodeEncodingOptions()
 Dim SQL As New Comandos_SQL()
 Dim BarCode As String


 Private Sub frmBarCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  SQL.ComboBox("SELECT ItemCode,ItemName FROM SBO_TPD.dbo.OITM", "ItemCode,ItemName", cmbArticulos)
 End Sub

 ' Generate QRCode
 Private Sub GenerateCode(name As String)
  Dim writer = New BarcodeWriter()
  writer.Format = BarcodeFormat.CODE_128
  Dim result = writer.Write(name)
  Dim path As String = ("QRImage.png")
  Dim barcodeBitmap = New Bitmap(result)


  Using memory As New MemoryStream()
   Using fs As New FileStream(path, FileMode.Create, FileAccess.ReadWrite)
    barcodeBitmap.Save(memory, ImageFormat.Png)
    Dim bytes As Byte() = memory.ToArray()
    fs.Write(bytes, 0, bytes.Length)
   End Using
  End Using
  pbBarCode.Visible = True
  pbBarCode.ImageLocation = "QRImage.png"
 End Sub

 Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
  GenerateCode(txtBarCode.Text)
 End Sub

 Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
  'If txtBarCode.Text <> "" And cmbArticulos.Text <> "" And txtDescripcion.Text <> "" And txtClasificacion.Text <> "" And txtCopias.Text <> "" Then
  If txtBarCode.Text <> "" And cmbArticulos.Text <> "" And txtDescripcion.Text <> "" And txtCopias.Text <> "" Then
   If rbtnInterno.Checked Then
    zplPrint(txtBarCode.Text, cmbArticulos.Text, txtDescripcion.Text, txtClasificacion.Text, txtCopias.Text)
    'MsgBox("Imprime interno:" & txtBarCode.Text)
   ElseIf rbtnPieza.Checked Then
    zplPrint(txtBCPieza.Text, cmbArticulos.Text, txtDescripcion.Text, txtClasificacion.Text, txtCopias.Text)
    'MsgBox("Imprime txtBCPieza:" & txtBCPieza.Text)
   ElseIf rbtnBolsa.Checked Then
    zplPrint(txtBCBolsa.Text, cmbArticulos.Text, txtDescripcion.Text, txtClasificacion.Text, txtCopias.Text)
    'MsgBox("Imprime txtBCBolsa:" & txtBCBolsa.Text)
   ElseIf rbtnCaja.Checked Then
    zplPrint(txtBCCaja.Text, cmbArticulos.Text, txtDescripcion.Text, txtClasificacion.Text, txtCopias.Text)
    'MsgBox("Imprime txtBCCaja:" & txtBCCaja.Text)
   ElseIf rbtnTarima.Checked Then
    zplPrint(txtBCTarima.Text, cmbArticulos.Text, txtDescripcion.Text, txtClasificacion.Text, txtCopias.Text)
    'MsgBox("Imprime txtBCTarima:" & txtBCTarima.Text)
   End If
  Else
   MessageBox.Show("Verifique que los campos Artículos, descripción y número de copias cuenten con información")
  End If
 End Sub

 'BarCode As String, Articulo As String, Descripcion As String

 Sub zplPrint(BarCode As String, Articulo As String, Descripcion As String, Clasificacion As String, Copias As Integer)
  Dim ipZebra As String = "192.168.8.163"
  Dim port As Integer = 9100
  Dim posX As String


  If Copias <> 0 Then
   For i = 1 To Copias
    'ETIQUETA IZQUIERDA
    Dim ZPLCommand As String = "^XA^CI28"
    'ENCABEZADO
    'ZPLCommand &= "^FO0,20^A0N,17,17^FB330,1,0,C^FDTRACTO PARTES DIAMANTE DE PUEBLA S.A. DE C.V.^FS"
    ZPLCommand &= "^FO50,10^A0N,18,21^FB250,2,0,C^FDTRACTO PARTES DIAMANTE DE PUEBLA S.A. DE C.V.^FS"
    'CLAVE ARTICULO

    ZPLCommand &= "^FO0,50^A0N,17,17^FB355,1,0,C^FD" + Articulo + "^FS"

    'CODIGO DE BARRAS
    posX = "0"
    'Validar el tamaño del codigo de barras
    If BarCode.Length > 17 Then
     MessageBox.Show("Por el tamaño de las etiquetas es imposible generar el 'Codigo de barras'", "Error al generar el codigo de barras", MessageBoxButtons.OK, MessageBoxIcon.Error)
     Return
    End If

    If BarCode.Length = 17 Then
     posX = 65
    End If

    If BarCode.Length = 16 Then
     posX = 75
    End If

    If BarCode.Length = 15 Then
     posX = 65
    End If

    If BarCode.Length = 14 Then
     posX = 90
    End If

    If BarCode.Length = 13 Then
     posX = 95
    End If

    If BarCode.Length = 12 Then
     posX = 130
    End If

    If BarCode.Length = 11 Then
     posX = 105
    End If

    If BarCode.Length = 10 Then
     posX = 110
    End If

    If BarCode.Length = 9 Then
     posX = 115
    End If

    If BarCode.Length = 8 Then
     posX = 120
    End If

    If BarCode.Length = 7 Then
     posX = 125
    End If

    If BarCode.Length = 6 Then
     posX = 130
    End If

    If BarCode.Length = 5 Then
     posX = 135
    End If

    If BarCode.Length = 4 Then
     posX = 140
    End If

    If BarCode.Length = 3 Then
     posX = 145
    End If

    If BarCode.Length = 2 Then
     posX = 150
    End If

    If BarCode.Length = 1 Then
     posX = 155
    End If

    If BarCode.Length = 0 Then
     MessageBox.Show("Imposible generar un codigo de barras sin valor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
     Return
    End If

    'ZPLCommand &= "^FO" + posX + ",55^A0N,17,17^BC,60,Y,N,N^FD" + BarCode + "^FS"
    ZPLCommand &= "^FO20," + posX + "^A0N,17,17^BY1^BC,60,N,N,N,A^FD" + BarCode + "^FS"
    'Se agregar codigo QR
    ZPLCommand &= "^FO245,50^A0N,17,17^BY2,1.0,15^BQN,2,3,H,N,A^FDMM,F" + BarCode + "^FS"

    ZPLCommand &= "^FO0,130^A0N,17,17^FB355,1,0,C^FD" + BarCode + "^FS"

    'ZPLCommand &= "^FO0,145^A0N,17,17^FB355,3,0,C^FD" + Descripcion + "^FS"

    'DESCRIPCION ARTICULO
    If Descripcion.Length <= 33 Then
     ZPLCommand &= "^FO0,145^A0N,17,17^FB355,3,0,C^FD" + Descripcion + "^FS"
    Else
     Dim tam As Integer = Descripcion.Length
     If tam > 66 Then
      ZPLCommand &= "^FO0,147^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(0, 33) + "^FS"
     Else
      ZPLCommand &= "^FO0,155^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(0, 33) + "^FS"
     End If

     tam = tam - 33
      If tam > 33 Then
      ZPLCommand &= "^FO0,162^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(33, 33) + "^FS"
      tam = tam - 33
      ZPLCommand &= "^FO0,177^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(66, tam) + "^FS"
     ElseIf tam <= 33 Then
       ZPLCommand &= "^FO0,170^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(33, tam) + "^FS"
      End If
     End If
     'CLASIFICACION
     ZPLCommand &= "^FO35, 155^A0N, 17, 17^FD" + Clasificacion + "^FS"


    '=======================================================================================================================================
    'ETIQUETA DERECHA---------------------------------------------------------------------------------INICIO DE ETIQUETA DERECHA
    '=======================================================================================================================================
    ZPLCommand &= "^FO390,10^A0N,18,21^FB250,2,0,C^FDTRACTO PARTES DIAMANTE DE PUEBLA S.A. DE C.V.^FS"
    'CLAVE ARTICULO
    ZPLCommand &= "^FO340,50^A0N,17,17^FB355,1,0,C^FD" + Articulo + "^FS"

    'CODIGO DE BARRAS
    posX = "0"
    'Validar el tamaño del codigo de barras
    If BarCode.Length > 17 Then
     MessageBox.Show("Por el tamaño de las etiquetas es imposible generar el 'Codigo de barras'", "Error al generar el codigo de barras", MessageBoxButtons.OK, MessageBoxIcon.Error)
     Return
    End If

    If BarCode.Length = 17 Then
     posX = 370
    End If

    If BarCode.Length = 16 Then
     posX = 370
    End If

    If BarCode.Length = 15 Then
     posX = 370
    End If

    If BarCode.Length = 14 Then
     posX = 430
    End If

    If BarCode.Length = 13 Then
     posX = 435
    End If

    If BarCode.Length = 12 Then
     posX = 470
    End If

    If BarCode.Length = 11 Then
     posX = 445
    End If

    If BarCode.Length = 10 Then
     posX = 450
    End If

    If BarCode.Length = 9 Then
     posX = 455
    End If

    If BarCode.Length = 8 Then
     posX = 460
    End If

    If BarCode.Length = 7 Then
     posX = 465
    End If

    If BarCode.Length = 6 Then
     posX = 470
    End If

    If BarCode.Length = 5 Then
     posX = 475
    End If

    If BarCode.Length = 4 Then
     posX = 480
    End If

    If BarCode.Length = 3 Then
     posX = 485
    End If

    If BarCode.Length = 2 Then
     posX = 490
    End If

    If BarCode.Length = 1 Then
     posX = 495
    End If

    If BarCode.Length = 0 Then
     MessageBox.Show("Imposible generar un codigo de barras sin valor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
     Return
    End If

    'posX = (465 - ((BarCode.Length * 5) / 2)).ToString

    'ZPLCommand &= "^FO" + posX + ",55^A0N,17,17^BY1^BC,60,Y,N,N,A^FD" + BarCode + "^FS"
    ZPLCommand &= "^FO360,65^A0N,17,17^BY1^BC,60,N,N,N,A^FD" + BarCode + "^FS"
    'Se agregar codigo QR
    ZPLCommand &= "^FO585,50^A0N,17,17^BY2,1.0,15^BQN,2,3,H,N,A^FDMM,F" + BarCode + "^FS"

    ZPLCommand &= "^FO340,130^A0N,17,17^FB355,1,0,C^FD" + BarCode + "^FS"

    'DESCRIPCION ARTICULO
    If Descripcion.Length <= 33 Then
     ZPLCommand &= "^FO340,145^A0N,17,17^FB355,3,0,C^FD" + Descripcion + "^FS"
    Else
     Dim tam As Integer = Descripcion.Length
     If tam > 66 Then
      ZPLCommand &= "^FO340,147^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(0, 33) + "^FS"
     Else
      ZPLCommand &= "^FO340,155^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(0, 33) + "^FS"
     End If

     tam = tam - 33
     If tam > 33 Then
      ZPLCommand &= "^FO340,162^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(33, 33) + "^FS"
      tam = tam - 33
      ZPLCommand &= "^FO340,177^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(66, tam) + "^FS"
     ElseIf tam <= 33 Then
      ZPLCommand &= "^FO340,170^A0N,17,17^FB355,1,0,C^FD" + Descripcion.Substring(33, tam) + "^FS"
     End If
    End If
    'CLASIFICACION
    ZPLCommand &= "^FO375, 155^A0N, 17, 17^FD" + Clasificacion + "^FS"

    ZPLCommand &= "^XZ"

    'Exit Sub

    Try
     'Return  'Para detener y que no imprima
     Dim Cliente As New System.Net.Sockets.TcpClient()
     Cliente.Connect(ipZebra, port)

     Dim writer As New System.IO.StreamWriter(Cliente.GetStream())
     writer.Write(ZPLCommand)
     writer.Flush()

     writer.Close()
     Cliente.Close()

    Catch ex As Exception
     MessageBox.Show(ex.ToString)
    End Try
   Next
  Else
   MessageBox.Show("Para generar las etiquetas el número de copias debe de ser diferente a 0(cero)", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
  End If
 End Sub

 Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
  'e.Graphics.DrawImage(pbBarCode.Image, 2, 20, 145, 60)
 End Sub

 Private Sub cmbArticulos_SelectedIndexChanged(sender As Object, e As EventArgs)
  SQL.conectarTPM()
  txtDescripcion.Text = SQL.CampoEspecifico("SELECT ItemName FROM SBO_TPD.dbo.OITM WHERE ItemCode = '" + cmbArticulos.Text + "'", "ItemName")
  txtBarCode.Text = SQL.CampoEspecifico("SELECT BARCODE_INTERNO FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_INTERNO")
  txtBCPieza.Text = SQL.CampoEspecifico("SELECT BARCODE_PZI FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_PZI")
  txtBCBolsa.Text = SQL.CampoEspecifico("SELECT BARCODE_BLI FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_BLI")
  txtBCCaja.Text = SQL.CampoEspecifico("SELECT BARCODE_CJI FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_CJI")
  txtBCTarima.Text = SQL.CampoEspecifico("SELECT BARCODE_TRI FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_TRI")
  SQL.Cerrar()
  rbtnInterno.Checked = True
  GenerateCode(txtBarCode.Text)

  'Try
  ' SQL.conectarTPM()
  ' Dim CodigoB As String = SQL.CampoEspecifico("SELECT TOP 1 Id_Version AS 'Ordenador' FROM V_Equipo WHERE Id_Equipo = '" + Serial + "' AND Usuario = '" + Environment.UserName + "' ORDER BY REPLACE(Id_Version,'.','') DESC;", "Ordenador")
  'Catch ex As Exception
  ' MessageBox.Show("Error en Obtener Versiones: " + ex.ToString(), "Error SQL...", MessageBoxButtons.OK, MessageBoxIcon.Error)
  'End Try
 End Sub

 Private Sub cmbArticulos_TextChanged(sender As Object, e As EventArgs)

 End Sub

 Private Sub rbtnPieza_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnPieza.CheckedChanged
  GenerateCode(txtBCPieza.Text)
  'If rbtnPieza.Checked Then
  '  Dim PartBarCode As Array = txtBarCode.Text.Split("#")
  '  txtBarCode.Text = "PZ#"
  '  For i = 1 To PartBarCode.Length
  '    If i <> 1 Then
  '      If PartBarCode(i - 1) <> "" Then
  '        txtBarCode.Text &= PartBarCode(i - 1) + "#"
  '      End If
  '    End If
  '  Next
  'End If
 End Sub

 Private Sub rbtnBolsa_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnBolsa.CheckedChanged
  GenerateCode(txtBCBolsa.Text)
  'If rbtnBolsa.Checked Then
  '  Dim PartBarCode As Array = txtBarCode.Text.Split("#")
  '  txtBarCode.Text = "BL#"
  '  For i = 1 To PartBarCode.Length
  '    If i <> 1 Then
  '      If PartBarCode(i - 1) <> "" Then
  '        txtBarCode.Text &= PartBarCode(i - 1) + "#"
  '      End If
  '    End If
  '  Next
  'End If
 End Sub

 Private Sub rbtnCaja_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnCaja.CheckedChanged
  GenerateCode(txtBCCaja.Text)
  'If rbtnCaja.Checked Then
  '  Dim PartBarCode As Array = txtBarCode.Text.Split("#")
  '  txtBarCode.Text = "CJ#"
  '  For i = 1 To PartBarCode.Length
  '    If i <> 1 Then
  '      If PartBarCode(i - 1) <> "" Then
  '        txtBarCode.Text &= PartBarCode(i - 1) + "#"
  '      End If
  '    End If
  '  Next
  'End If
 End Sub

 Private Sub rbtnTarima_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnTarima.CheckedChanged
  GenerateCode(txtBCTarima.Text)
  'If rbtnTarima.Checked Then
  '  Dim PartBarCode As Array = txtBarCode.Text.Split("#")
  '  txtBarCode.Text = "TR#"
  '  For i = 1 To PartBarCode.Length
  '    If i <> 1 Then
  '      If PartBarCode(i - 1) <> "" Then
  '        txtBarCode.Text &= PartBarCode(i - 1) + "#"
  '      End If
  '    End If
  '  Next
  'End If
 End Sub

 Private Sub txtDescripcion_TextChanged(sender As Object, e As EventArgs) Handles txtDescripcion.TextChanged
  'Dim PartBarCode As Array = txtBarCode.Text.Split("#")
  'For i = 1 To PartBarCode.Length
  '    If i = 2 Then
  '        If PartBarCode(i - 1) = "" Then
  '            txtBarCode.Text &= cmbArticulos.Text + "#"
  '        Else
  '            txtBarCode.Text &= cmbArticulos.Text + "#"
  '        End If
  '    Else
  '        If PartBarCode(i - 1) <> "" Then
  '            If PartBarCode(i - 1) = "PZ" Or PartBarCode(i - 1) = "BL" Or PartBarCode(i - 1) = "CJ" Or PartBarCode(i - 1) = "TR" Then
  '                txtBarCode.Text = PartBarCode(i - 1) + "#"
  '            Else
  '                txtBarCode.Text &= PartBarCode(i - 1) + "#"
  '            End If
  '        End If
  '    End If
  'Next
 End Sub

 Private Sub cmbArticulos_Leave(sender As Object, e As EventArgs) Handles cmbArticulos.Leave
  'Try
  '  'txtDescripcion.Text = cmbArticulos.SelectedValue.ToString
  '  SQL.conectarTPM()
  '  txtDescripcion.Text = SQL.CampoEspecifico("SELECT ItemName FROM SBO_TPD.dbo.OITM WHERE ItemCode = '" + cmbArticulos.Text + "'", "ItemName")
  '  SQL.Cerrar()
  'Catch ex As Exception

  'End Try
 End Sub

 Private Sub cmbArticulos_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbArticulos.SelectedValueChanged
  If cmbArticulos.SelectedIndex > 0 Then
   SQL.conectarTPM()
   txtDescripcion.Text = SQL.CampoEspecifico("SELECT ItemName FROM SBO_TPD.dbo.OITM WHERE ItemCode = '" + cmbArticulos.Text + "'", "ItemName")
   txtBarCode.Text = SQL.CampoEspecifico("SELECT BARCODE_INTERNO FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_INTERNO")
   txtBCPieza.Text = SQL.CampoEspecifico("SELECT BARCODE_PZI FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_PZI")
   txtBCBolsa.Text = SQL.CampoEspecifico("SELECT BARCODE_BLI FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_BLI")
   txtBCCaja.Text = SQL.CampoEspecifico("SELECT BARCODE_CJI FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_CJI")
   txtBCTarima.Text = SQL.CampoEspecifico("SELECT BARCODE_TRI FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "BARCODE_TRI")
   txtClasificacion.Text = SQL.CampoEspecifico("SELECT Categoria FROM Barcode WHERE articulo = '" + cmbArticulos.Text + "'", "Categoria")
   SQL.Cerrar()
   rbtnInterno.Checked = True
   GenerateCode(txtBarCode.Text)
  End If
 End Sub

End Class