Imports SAPbobsCOM

Module diSAP

  Public vCompany As SAPbobsCOM.Company

  Private Function ConectarSAP() As SAPbobsCOM.Company
    vCompany = New SAPbobsCOM.Company()
    Try
      vCompany.SLDServer = "192.168.8.38"
      vCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017

      vCompany.Server = "SERVIDORDELL"
      vCompany.CompanyDB = "TESTMIGRACIONSAP"
      vCompany.UserName = "manager"
      vCompany.Password = "123456"

      Dim ret As Integer = vCompany.Connect()
      Dim errMsg As String = vCompany.GetLastErrorDescription()
      Dim ErrNo As Integer = vCompany.GetLastErrorCode()
      If (ErrNo <> 0) Then
        Return vCompany
      End If
      'vCompany.Disconnect()
      Return vCompany
    Catch ex As Exception
      Return vCompany
    End Try
  End Function

  Public Sub ImportaOrdenVenta2SAP(NoOrdenVenta As Integer)
    Dim OrdenVenta As ordenVenta
    OrdenVenta = New ordenVenta
    OrdenVenta.Lineas = New List(Of detalle_ordenVenta)
    Dim LineaOV As detalle_ordenVenta

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
      Dim ConsutaLista As String
      ConsutaLista = "SELECT (SELECT MAX(DocNum) + 1 as Siguiente FROM SBO_TPD.dbo.ORDR) Siguiente, 9 as Serie, IdCliente, CASE WHEN Comen IS NULL THEN '' ELSE Comen END Comen,
                      FchOVta DocDate, FchOVta DocDueDate, DocTotal, '99' PaymentMethod 
                      FROM OrdVta 
                      WHERE IdOrdVta = " & NoOrdenVenta

      Dim daOV As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)
      Dim dsOVta As New DataSet
      daOV.Fill(dsOVta, "OrdeVenta")

      For Each linea As DataRow In dsOVta.Tables("OrdeVenta").Rows
        'MsgBox(linea(0) & " " & linea(1))
        OrdenVenta.DocNum = linea("Siguiente") 'Siguiente consecutivo
        OrdenVenta.Series = linea("Serie")
        OrdenVenta.CardCode = linea("IdCliente")
        OrdenVenta.Comments = linea("Comen")
        OrdenVenta.DocDate = linea("DocDate")
        OrdenVenta.DocDueDate = linea("DocDueDate")
        OrdenVenta.DocTotal = linea("DocTotal")
        OrdenVenta.PaymentMethod = linea("PaymentMethod")
      Next

      ConsutaLista = "SELECT Articulo, DesArt, Precio, Totlinea, Cantidad, 'MXP' Currency, DescLin FROM RdVta1 WHERE IdOrdVta = " & NoOrdenVenta & " ORDER BY NumLinea DESC"
      Dim dadOV As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)
      Dim dsdOVta As New DataSet
      dadOV.Fill(dsdOVta, "DetallesOrdeVenta")

      For Each linea As DataRow In dsdOVta.Tables("DetallesOrdeVenta").Rows
        'MsgBox(linea(0) & " " & linea(1))
        LineaOV = New detalle_ordenVenta
        LineaOV.ItemCode = linea("Articulo").ToString()
        LineaOV.ItemDescription = linea("DesArt")
        LineaOV.Price = linea("Precio")
        LineaOV.PriceAfterVAT = linea("Totlinea") 'Precio tras el descuento
        LineaOV.Quantity = linea("Cantidad")
        LineaOV.Currency = linea("Currency")
        LineaOV.DiscountPercent = linea("DescLin")
        OrdenVenta.Lineas.Add(LineaOV)
        'Public ItemCode As String ' = "052022004"
        ''descripcion del articulo
        'Public ItemDescription As String ' = "TONOS MAIZ DULCE 75gr 1X48 [003169]"
        ''Precio después de IVA(impuesto de venta)
        'Public PriceAfterVAT As Double ' = 374.3125
        ''cantidad
        'Public Quantity As Integer ' = 5
        ''moneda
        'Public Currency As String ' = "COL"
        ''% de descuento
        'Public DiscountPercent As Integer ' = 10
        ''Invoice Lines - Set values to the second line

        'Public Lines.Add() ' LINEA IMPORTANTE PARA AGREGAR MAS LINEAS
      Next

      AddOrderClient(OrdenVenta)
    End Using
  End Sub

  Private Sub AddOrderClient(oVenta As ordenVenta)
    Try
      Dim RetVal As Long
      Dim ErrCode As Long
      Dim ErrMsg As String
      Dim oCompany As SAPbobsCOM.Company
      Dim lRetCode, ErrorCode As Long
      Dim ErrorMessage As String

      oCompany = ConectarSAP()


      'Dim oOrder As Documents = DirectCast(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents) ' Order object
      Dim oOrder As Documents
      oOrder = oCompany.GetBusinessObject(BoObjectTypes.oOrders)

      oOrder.HandWritten = BoYesNoEnum.tNO '--> Si el valor es positivo entonces debera indicarse el numero de documento
      If oOrder.HandWritten = BoYesNoEnum.tYES Then
        oOrder.DocNum = oVenta.DocNum
      End If
      'oOrder.Series = oVenta.Series
      'codigo del cliente
      oOrder.CardCode = oVenta.CardCode
      'COmentario de la venta
      oOrder.Comments = "Comentario de prueba"
      'Fecha del creacion del documento
      oOrder.DocDate = Now()
      oOrder.DocDueDate = Now()
      'Monto total del documento
      oOrder.DocTotal = 10
      oOrder.NumAtCard = "Pedido con referencia X"
      oOrder.Reference1 = "Ref1"
      oOrder.Reference2 = "Ref2"

      'Inf Financiera
      'oOrder.PaymentMethod = oVenta.PaymentMethod 'Forma de pago


      'oOrder.Lines.CostingCode
      oOrder.Lines.ItemCode = "FM-C28"
      oOrder.Lines.UnitPrice = 10
      oOrder.Lines.PriceAfterVAT = 10
      oOrder.Lines.Currency = "MXP"
      ''cantidad
      oOrder.Lines.Quantity = 1
      ''moneda
      ''% de descuento
      oOrder.Lines.DiscountPercent = 0.1
      'oOrder.Lines.Currency = linea.Currency
      oOrder.Lines.Add() ' LINEA IMPORTANTE PARA AGREGAR MAS LINEAS


      ''  ' '////////////////////////////////////// ENCABEZADO DE LA ORDEN DE VENTA /////////////////////////////////////////////
      'oOrder.HandWritten = BoYesNoEnum.tNO '--> Si el valor es positivo entonces debera indicarse el numero de documento
      'If oOrder.HandWritten = BoYesNoEnum.tYES Then
      '  oOrder.DocNum = oVenta.DocNum
      'End If
      ''oOrder.Series = oVenta.Series
      ''codigo del cliente
      'oOrder.CardCode = oVenta.CardCode
      ''COmentario de la venta
      'oOrder.Comments = oVenta.Comments
      ''Fecha del creacion del documento
      'oOrder.DocDate = oVenta.DocDate
      'oOrder.DocDueDate = oVenta.DocDueDate
      ''Monto total del documento
      'oOrder.DocTotal = oVenta.DocTotal
      ''Inf Financiera
      ''oOrder.PaymentMethod = oVenta.PaymentMethod 'Forma de pago

      ''////////////////////////////////////// DETALLE DE LA ORDEN DE VENTA /////////////////////////////////////////////
      ''Invoice Lines - Set values to the first line
      'For Each linea As detalle_ordenVenta In oVenta.Lineas
      '  'Codigo del articulo
      '  oOrder.Lines.ItemCode = linea.ItemCode
      '  ''Precio después de IVA(impuesto de venta)
      '  oOrder.Lines.Price = linea.Price
      '  oOrder.Lines.PriceAfterVAT = linea.PriceAfterVAT
      '  ''cantidad
      '  oOrder.Lines.Quantity = linea.Quantity
      '  ''moneda
      '  ''% de descuento
      '  oOrder.Lines.DiscountPercent = linea.DiscountPercent
      '  'oOrder.Lines.Currency = linea.Currency
      '  oOrder.Lines.Add() ' LINEA IMPORTANTE PARA AGREGAR MAS LINEAS
      'Next

      'Add the Invoice
      RetVal = oOrder.Add
      'Check the result
      If RetVal <> 0 Then
        oCompany.GetLastError(ErrCode, ErrMsg)
        MsgBox(ErrCode & " " & ErrMsg)
      Else
        MsgBox("NUEVA ORDEN DE VENTA")
      End If
      'se desconecta de SAP
      oCompany.Disconnect()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

End Module
