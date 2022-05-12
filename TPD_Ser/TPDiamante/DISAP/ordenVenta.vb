Public Class ordenVenta
  Public DocNum As Long ' = 25030000
  Public Series As Integer
  'codigo del cliente
  Public CardCode As String ' = "C502-0147"
  Public Comments As String
  'Escritor a mano
  Public HandWritten As Integer ' = 1
  'codigo del grupo de pago (credito de 8 ,15 o los dias que tenga)
  Public PaymentGroupCode As String ' = "1"
  'Fecha del creacion del documento
  Public DocDate As Date ' = "02/8/2013"
  Public DocDueDate As Date ' = "02/8/2013"
  'Monto total del documento
  Public DocTotal As Double ' = 264.6
  Public Lineas As List(Of detalle_ordenVenta)
  '--------------INF FINANCIERA CFDI
  Public PaymentMethod As String
End Class

Public Class detalle_ordenVenta
  'Codigo del articulo
  Public ItemCode As String ' = "052022004"
  'descripcion del articulo
  Public ItemDescription As String ' = "TONOS MAIZ DULCE 75gr 1X48 [003169]"
  Public Price As Double
  'Precio después de IVA(impuesto de venta)
  Public PriceAfterVAT As Double ' = 374.3125
  'cantidad
  Public Quantity As Integer ' = 5
  'moneda
  Public Currency As String ' = "MXP"
  '% de descuento
  Public DiscountPercent As Decimal ' = 10
  'Invoice Lines - Set values to the second line
End Class