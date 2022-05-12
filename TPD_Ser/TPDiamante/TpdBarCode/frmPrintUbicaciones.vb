Imports ZXing
Imports ZXing.QrCode
Imports System.IO
Imports System.Drawing.Imaging

Public Class frmPrintUbicaciones

 Dim options As New QrCodeEncodingOptions()
 Dim BarCode As String

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
  If txtBLQ.Text.Trim <> "" And txtSCC.Text.Trim <> "" And txtRCK.Text.Trim <> "" And txtNVL.Text.Trim <> "" And txtSPCIO.Text.Trim <> "" Then

   'Valido longitudes
   If (txtBLQ.Text.Length <> 2) Then
    MessageBox.Show("Verifique la longitud del campo BLOQUE debe ser de 2 caracteres")
    txtBLQ.Focus()
    Exit Sub
   End If

   If (txtSCC.Text.Length <> 2) Then
    MessageBox.Show("Verifique la longitud del campo SECCION debe ser de 2 caracteres")
    txtSCC.Focus()
    Exit Sub
   End If

   If (txtRCK.Text.Trim.Length <> 1) Then
    MessageBox.Show("Verifique la longitud del campo RACK debe ser de 1 caracter")
    txtRCK.Focus()
    Exit Sub
   End If

   If (txtNVL.Text.Trim.Length <> 1) Then
    MessageBox.Show("Verifique la longitud del campo NIVEL debe ser de 1 caracter")
    txtNVL.Focus()
    Exit Sub
   End If

   If (txtSPCIO.Text.Length <> 3) Then
    MessageBox.Show("Verifique la longitud del campo ESPACIO debe ser de 3 caracteres")
    txtSPCIO.Focus()
    Exit Sub
   End If

   Dim DescEtiqueta As String = txtBLQ.Text.Trim & txtSCC.Text.Trim & txtRCK.Text.Trim & txtNVL.Text.Trim & txtSPCIO.Text.Trim
   GenerateCode(DescEtiqueta)
   Button1.Enabled = True
  Else
   MessageBox.Show("Verifique que los campos para la generacion de la ubicación esten todos llenos con información")
  End If
 End Sub

 Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
  'If txtBarCode.Text <> "" And cmbArticulos.Text <> "" And txtDescripcion.Text <> "" And txtClasificacion.Text <> "" And txtCopias.Text <> "" Then
  If txtBLQ.Text.Trim <> "" And txtSCC.Text.Trim <> "" And txtRCK.Text.Trim <> "" And txtNVL.Text.Trim <> "" And txtSPCIO.Text.Trim <> "" Then
   'Valido longitudes
   If (txtBLQ.Text.Length <> 2) Then
    MessageBox.Show("Verifique la longitud del campo BLOQUE debe ser de 2 caracteres")
    txtBLQ.Focus()
    Exit Sub
   End If

   If (txtSCC.Text.Length <> 2) Then
    MessageBox.Show("Verifique la longitud del campo SECCION debe ser de 2 caracteres")
    txtSCC.Focus()
    Exit Sub
   End If

   If (txtRCK.Text.Trim.Length <> 1) Then
    MessageBox.Show("Verifique la longitud del campo RACK debe ser de 1 caracter")
    txtRCK.Focus()
    Exit Sub
   End If

   If (txtNVL.Text.Trim.Length <> 1) Then
    MessageBox.Show("Verifique la longitud del campo NIVEL debe ser de 1 caracter")
    txtNVL.Focus()
    Exit Sub
   End If

   If (txtSPCIO.Text.Length <> 3) Then
    MessageBox.Show("Verifique la longitud del campo ESPACIO debe ser de 3 caracteres")
    txtSPCIO.Focus()
    Exit Sub
   End If

   If (txtCopias.Text.Trim().Equals("")) Then
    MessageBox.Show("Deberá indicar el número de copias a imprimir")
    txtCopias.Focus()
    Exit Sub
   ElseIf (IsNumeric(txtCopias.Text.Trim()).Equals(False)) Then
    MessageBox.Show("Deberá indicar un valor numerico de copias a imprimir")
    txtCopias.Focus()
    Exit Sub
   ElseIf (Integer.Parse(txtCopias.Text.Trim()) <= 0) Then
    MessageBox.Show("Deberá indicar un valor superior a cero para el número de copias a imprimir")
    txtCopias.Focus()
    Exit Sub
   End If

   zplPrint(txtBLQ.Text, txtSCC.Text, txtRCK.Text, txtNVL.Text, txtSPCIO.Text, txtCopias.Text)
  Else
   MessageBox.Show("Verifique que los campos para la generacion de la ubicación esten todos llenos con información")
  End If
 End Sub

 Sub zplPrint(Bloque As String, Seccion As String, Rack As String, Nivel As String, Espacio As String, Copias As Integer)
  Dim ipZebra As String = "192.168.8.163"
  Dim port As Integer = 9100
  Dim posX As String

  'Los codigos tendran siempre la misma longitud en caracteres
  '  BLQ: 2
  '  SCC: 2
  '  RCK: 1
  '  NVL: 1
  'SPCIO: 3

  Dim DescEtiqueta As String = Bloque & Seccion & Rack & Nivel & Espacio
  Dim BarCode As String = DescEtiqueta
  Dim DescUbicacion = "BLQ:" & Bloque & "  SCC:" & Seccion & "  RCK:" & Rack & "  NVL:" & Nivel & "  SPCIO:" & Espacio

  If Copias <> 0 Then
   For i = 1 To Copias
    'ETIQUETA IZQUIERDA
    Dim ZPLCommand As String = "^XA^CI28^PW792"
    'ENCABEZADO
    ZPLCommand &= "^FO60,70^A0N,30,30^FB700,1,0,C^FDTRACTO PARTES DIAMANTE DE PUEBLA S.A. DE C.V.^FS"

    'Desc de ubicacion
    ZPLCommand &= "^FO70,120^A0N,30,30^FB700,1,0,C^FD" + DescUbicacion + "^FS"

    'CODIGO DE BARRAS
    posX = "0"
    'Validar el tamaño del codigo de barras
    If BarCode.Length > 17 Then
     MessageBox.Show("Por el tamaño de las etiquetas es imposible generar el 'Codigo de barras'", "Error al generar el codigo de barras", MessageBoxButtons.OK, MessageBoxIcon.Error)
     Return
    End If

    If BarCode.Length = 0 Then
     MessageBox.Show("Imposible generar un codigo de barras sin valor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
     Return
    End If

    'SIN QRCODE
    'ZPLCommand &= "^FO100,180^A0N,25,25^BY5,1.0,50^BC,150,N,N,N,A^FD" + BarCode + "^FS"

    'CON QRCODE
    ZPLCommand &= "^FO50,180^A0N,25,25^BY4,1.0,50^BC,150,N,N,N,A^FD" + BarCode + "^FS"
    ZPLCommand &= "^FO610,135^A0N,25,25^BY5,1.0,50^BQ,2,7,N,N,A^FDMA," + BarCode + "^FS"

    ZPLCommand &= "^FO200,350^A0N,35,35^FB455,1,0,C^FD" + BarCode + "^FS"

    ZPLCommand &= "^XZ" 'Creo que con esto manda a imprimir

    '    Return

    Try
     Dim Cliente As New System.Net.Sockets.TcpClient()
     Cliente.Connect(ipZebra, port)

     Dim writer As New System.IO.StreamWriter(Cliente.GetStream())
     writer.Write(ZPLCommand)
     writer.Flush()

     writer.Close()
     Cliente.Close()

     If (Integer.Parse(txtCopias.Text.Trim()) > 1) Then
      MessageBox.Show("Las etiquetas fueron impresas correctamente", "Impresión correcta", MessageBoxButtons.OK, MessageBoxIcon.Information)
     Else
      MessageBox.Show("La etiqueta fue impresa correctamente", "Impresión correcta", MessageBoxButtons.OK, MessageBoxIcon.Information)
     End If

    Catch ex As Exception
     MessageBox.Show(ex.ToString)
    End Try
   Next
  Else
   MessageBox.Show("Para generar las etiquetas el número de copias debe de ser diferente a 0(cero)", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
  End If
 End Sub

End Class