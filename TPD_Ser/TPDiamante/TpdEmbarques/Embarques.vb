Imports System.Data.SqlClient
Public Class Embarques
 Dim vActvalor As Integer = 0
 Dim DvFletes As New DataView
 Dim vCant As Integer = 0
 Dim vIni As DateTime 'Fecha de Inicio
 Dim vFin As Date 'Fecha de Termino
 Dim vFchComp As Date 'Fecha comprometida
 Dim vGarantia As String = ""
 Sub cargar_registros()
  Try
   ' crear nueva conexión    
   Dim conexion2 As New SqlConnection(StrCon)

   ' abrir la conexión con la base de datos   
   conexion2.Open()

   Dim Adaptador As New SqlDataAdapter()
   Dim comando As New SqlCommand

   Dim SQLTPD As String

   SQLTPD = "SELECT row_number() over (order by (select null)) as 'numRow',"
   SQLTPD &= "'False' as Entregado, ISNULL(T0.EDocNum, T0.DocNum) as Factura,T0.[DocDate] as Fchfact,T0.[CardCode] as Codclte, "
   SQLTPD &= "T0.[CardName] as 'Cliente',T2.City AS Ciudad,T2.State1 AS Estado,T2.Phone1 AS Telefono,T2.CntctPrsn AS Contacto, "
   SQLTPD &= "T0.DocTotal AS 'TotalFactura', T0.Comments as 'Comentario', CASE WHEN T0.docstatus='C' THEN 'Cerrado' WHEN T0.printed='N' AND T0.docstatus='O' THEN 'Abierto' "
   SQLTPD &= "WHEN (T0.printed='Y' OR T0.printed='A') AND T0.docstatus='O' THEN 'Abrir: Imprimido' ELSE '' END as 'status', "
   SQLTPD &= "TrnspName AS Fletera,T1.ItemCode AS Articulo,T1.Quantity AS Cantidad, T0.[DocDate] as Fchemb,CAST(0 AS varchar(20)) AS crastreo, "
   SQLTPD &= "CAST('' AS nvarchar(150)) AS Recibeguia,CAST('' AS varchar(5)) AS Horarec,CAST('' AS datetime) AS Fchrec,ISNULL(CAST(T2.U_BXP_ZONA AS numeric(2, 0)), CAST(0 AS numeric(2, 0))) AS Zona, "
   SQLTPD &= "CAST('' AS int) AS Pdeclarado,CAST(0 AS numeric(19, 6)) AS vguiadec,CAST('' AS int) AS Pesocob, "
   SQLTPD &= "CAST(0 AS numeric(19, 6)) AS vguiacob,CAST('' AS int) AS Difpeso,CAST(0 AS numeric(19, 6)) AS Difvalor,T1.LineTotal / T1.Quantity AS LineTotal, "
   SQLTPD &= "CAST('' AS datetime) AS Fchent,CAST('' AS nvarchar(150)) AS Matrecpor,CAST('' AS int) AS Diasent,CAST('' AS varchar(250)) AS Garantia, "
   SQLTPD &= "CAST('' AS varchar(250)) AS comentarios,T3.Trnspcode AS Idfletera,CAST('' AS varchar(10)) AS Usuariocap, T1.Dscription AS Descripcion,'False' AS Excluir,'False' AS Cortesia,  "
   SQLTPD &= "CAST(0 AS decimal(12,2)) AS Reexpedicion , CAST(0 AS decimal(12,2)) AS CorrecDatos, CAST(0 AS decimal(12,2)) AS EnvIrreg "
   SQLTPD &= "into #tmp1 "
   SQLTPD &= "FROM [SBO_TPD].dbo.OINV T0 "
   SQLTPD &= "INNER JOIN [SBO_TPD].dbo.INV1 T1 ON T0.DocEntry = T1.DocEntry "
   SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OCRD T2 ON T0.CardCode = T2.CardCode "
   SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OSHP T3 ON T0.Trnspcode = T3.Trnspcode "
   'SQLTPD &= "WHERE T0.DocDate >= '2017-05-18' AND T0.DocDate <= '2017-05-18' "
   'SE AGREGA LA ULTIMA EXCEPCIÓN PARA NO MOSTRAR EL FLETE DE LA FACTURA 2001127, A PETICION DE ELIZABETH DE EMBARQUES
   SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer and t0.DocNum <> 2001127"

   If Me.CmbEnvio.SelectedValue <> 0 Then
    'MsgBox("entre combo envio")
    SQLTPD &= "AND T3.Trnspcode = " + Me.CmbEnvio.SelectedValue.ToString + " "
   End If

   If Me.CmbFlete.SelectedValue = "SERVLOG" Then
    SQLTPD &= " AND T1.ItemCode IN ('FL-005','FL-006','FL-007','FL-008','FL-009','FL-011','FL-012') "
   ElseIf Me.CmbFlete.SelectedValue = "FL-001" Then
    SQLTPD &= " AND (T1.ItemCode = 'FL-001' OR T1.ItemCode = 'FL-018') "
   ElseIf Me.CmbFlete.SelectedValue = "FL-002" Then
    SQLTPD &= " AND (T1.ItemCode = 'FL-002' OR T1.ItemCode = 'FL-018') "
   ElseIf Me.CmbFlete.SelectedValue = "FL-003" Then
    SQLTPD &= " AND (T1.ItemCode = 'FL-003' OR T1.ItemCode = 'FL-018') "

   ElseIf Me.CmbFlete.SelectedValue = "FL-004" Then
    SQLTPD &= " AND (T1.ItemCode = 'FL-004' OR T1.ItemCode = 'FL-018' OR T1.ItemCode = 'FL-013') "

   ElseIf Me.CmbFlete.SelectedValue <> "TODOS" Then
    SQLTPD &= " AND T1.ItemCode = '" + Me.CmbFlete.SelectedValue + "' "
   End If

   SQLTPD &= " AND T1.ItemCode IN "
   SQLTPD &= " (SELECT T4.ItemCode FROM [SBO_TPD].dbo.OITM T4 WHERE T4.ItmsGrpCod = 150) "

   SQLTPD &= "Create Table #real (numRow int, Entregado varchar(10), Factura varchar(50), Fchfact datetime, Codclte varchar(15), "
   SQLTPD &= "Cliente varchar(100), Ciudad varchar(100), Estado varchar(5), Telefono varchar(50), Contacto varchar(90), Total numeric(19, 6), Comentario varchar(500), Estatus varchar(100), "
   SQLTPD &= "Fletera varchar(40), Articulo varchar(20), Cantidad numeric(19, 6), Fchemb datetime, crastreo varchar(20), Recibeguia varchar(150), "
   SQLTPD &= "Horarec varchar(5),	Fchrec datetime, Zona numeric(2, 0), Pdeclarado int, vguiadec numeric(19, 6), Pesocob int, "
   SQLTPD &= "vguiacob numeric(19, 6), Difpeso int, Difvalor numeric(19, 6), LineTotal numeric(19, 6), Fchent datetime, "
   SQLTPD &= "Matrecpor varchar(150), Diasent int, Garantia varchar(8), comentarios varchar(250), Idfletera smallint,  "
   SQLTPD &= "Usuariocap varchar(10), Descripcion nvarchar(100), Excluir varchar(10), Cortesia varchar(10), Reexpedicion decimal(12, 2),  "
   SQLTPD &= "CorrecDatos decimal(12, 2), EnvIrreg decimal(12, 2), flete int ) "
   SQLTPD &= "DECLARE @inicio INT "
   SQLTPD &= "set @inicio = 1 "
   SQLTPD &= "DECLARE @fin INT "
   SQLTPD &= "set @fin = (select COUNT(*) from #tmp1) "
   SQLTPD &= "DECLARE @inicio_bucle INT "
   SQLTPD &= "set @inicio_bucle = 1 "
   SQLTPD &= "DECLARE @fin_bucle INT "
   SQLTPD &= "DECLARE @num_flete INT "
   SQLTPD &= "set @num_flete = 1 "
   SQLTPD &= "while (@inicio <= @fin) "
   SQLTPD &= "begin "
   SQLTPD &= "set @fin_bucle = (select CAST(Cantidad as int) from #tmp1 where numRow = @inicio) "
   SQLTPD &= "while(@inicio_bucle <= @fin_bucle) "
   SQLTPD &= "begin "
   SQLTPD &= "insert into #real(numRow, Entregado, Factura, Fchfact, Codclte, Cliente, Ciudad, Estado, Telefono, Contacto, Total, Comentario, Estatus, "
   SQLTPD &= "Fletera, Articulo, Cantidad, Fchemb, crastreo, Recibeguia, Horarec, Fchrec, Zona, Pdeclarado, vguiadec, Pesocob, "
   SQLTPD &= "vguiacob, Difpeso, Difvalor, LineTotal, Fchent, Matrecpor, Diasent, Garantia, comentarios, Idfletera, Usuariocap, "
   SQLTPD &= "Descripcion, Excluir, Cortesia, Reexpedicion, CorrecDatos, EnvIrreg, flete) "
   SQLTPD &= "select numRow, Entregado, Factura, Fchfact, Codclte, Cliente, Ciudad, Estado,Telefono, Contacto, TotalFactura, Comentario, status, Fletera, Articulo, "
   SQLTPD &= "Cantidad, Fchemb, crastreo, Recibeguia, Horarec, Fchrec, Zona, Pdeclarado, vguiadec, Pesocob, vguiacob, Difpeso, "
   SQLTPD &= "Difvalor, LineTotal, Fchent, Matrecpor, Diasent, Garantia, comentarios, Idfletera, Usuariocap, Descripcion, Excluir, "
   SQLTPD &= "Cortesia, Reexpedicion, CorrecDatos, EnvIrreg, @num_flete from #tmp1 where numRow = @inicio "
   SQLTPD &= "set @inicio_bucle = @inicio_bucle + 1 "
   SQLTPD &= "set @num_flete = @num_flete + 1 "
   SQLTPD &= "end "
   SQLTPD &= "set @inicio_bucle = 1 "
   SQLTPD &= "if((select Factura from #tmp1 where numRow = @inicio) <> (select Factura from #tmp1 where numRow = @inicio + 1)) "
   SQLTPD &= "set @num_flete = 1; "
   SQLTPD &= "set @inicio = @inicio + 1 "
   SQLTPD &= "end "

   SQLTPD &= "SELECT T8.Entregado, T8.Factura COLLATE Modern_Spanish_CI_AI AS Factura,  "
   SQLTPD &= "T8.Fchfact, T8.Flete, T8.Codclte COLLATE Modern_Spanish_CI_AI AS Codclte, "
   SQLTPD &= "T8.Cliente COLLATE Modern_Spanish_CI_AI AS Cliente, T8.Ciudad COLLATE Modern_Spanish_CI_AI AS Ciudad, "
   SQLTPD &= "T8.Estado COLLATE Modern_Spanish_CI_AI AS Estado, T8.Telefono COLLATE Modern_Spanish_CI_AI AS Telefono, "
   'SQLTPD &= "T8.Contacto COLLATE Modern_Spanish_CI_AI AS Contacto, 'T9.total' COLLATE Modern_Spanish_CI_AI AS 'Total', "
   SQLTPD &= "T8.Contacto COLLATE Modern_Spanish_CI_AI AS Contacto,"
   SQLTPD &= "T9.Comments COLLATE Modern_Spanish_CI_AI AS 'ComentarioSAP', "
   SQLTPD &= "CASE WHEN T9.docstatus='C' THEN 'Cerrado' WHEN T9.printed='N' AND T9.docstatus='O' THEN 'Abierto' "
   SQLTPD &= "WHEN (T9.printed='Y' OR T9.printed='A') AND T9.docstatus='O' THEN 'Abrir: Imprimido' ELSE '' END as 'Estatus', "
   SQLTPD &= "T9.DocTotal as 'Total',"
   'T9.Estatus' COLLATE Modern_Spanish_CI_AI AS 'Estatus', "
   SQLTPD &= "T8.Fletera COLLATE Modern_Spanish_CI_AI AS Fletera, "
   SQLTPD &= "T8.Articulo COLLATE Modern_Spanish_CI_AI AS Articulo, T8.Cantidad, T8.Fchemb,  "
   SQLTPD &= "T8.crastreo COLLATE Modern_Spanish_CI_AI AS crastreo, T8.Recibeguia COLLATE Modern_Spanish_CI_AI AS Recibeguia,  "
   SQLTPD &= "T8.Horarec COLLATE Modern_Spanish_CI_AI AS Horarec, T8.Fchrec, CASE WHEN T8.Zona is null THEN 0 ELSE T8.Zona END as Zona, "
   SQLTPD &= "T8.Pdeclarado,T8.vguiadec, T8.Pesocob, T8.vguiacob, T8.Difpeso,T8.Difvalor,T8.LineTotal, T8.Fchent, "
   SQLTPD &= "T8.Matrecpor COLLATE Modern_Spanish_CI_AI AS Matrecpor, T8.Diasent, T8.Garantia COLLATE Modern_Spanish_CI_AI AS Garantia, "
   SQLTPD &= "T8.comentarios COLLATE Modern_Spanish_CI_AI AS comentarios, T8.Idfletera, "
   SQLTPD &= "T8.Usuariocap COLLATE Modern_Spanish_CI_AI AS Usuariocap, T8.Descripcion COLLATE Modern_Spanish_CI_AI AS Descripcion, "
   SQLTPD &= "CASE WHEN T8.Excluir IS NULL THEN 'False' ELSE T8.Excluir END AS Excluir, "
   SQLTPD &= "CASE WHEN T8.Cortesia IS NULL THEN 'False' ELSE T8.Cortesia END AS Cortesia,  "
   SQLTPD &= "t8.Reexpedicion,t8.CorrecDatos, T8.EnvIrreg "
   SQLTPD &= "FROM [TPM].dbo.EMBENT T8 inner join SBO_TPD.dbo.OINV T9 ON T8.Factura = T9.DocNum   "
   If RadBEmb.Checked = True Then
    'SE AGREGA LA ULTIMA EXCEPCIÓN PARA QUE NO MUESTRE EL FLETE DE LA FACTURA 2001127, SOLICITADO POR ELIZABETH
    SQLTPD &= "WHERE T8.Fchemb >= @FechaIni AND T8.Fchemb <= @FechaTer and T9.DocNum <> 2001127"
   Else
    'SE AGREGA LA ULTIMA EXCEPCIÓN PARA QUE NO MUESTRE EL FLETE DE LA FACTURA 2001127, SOLICITADO POR ELIZABETH
    SQLTPD &= "WHERE T8.Fchfact >= @FechaIni AND T8.Fchfact <= @FechaTer and T9.DocNum <> 2001127"
   End If


   If Me.CmbEnvio.SelectedValue <> 0 Then
    SQLTPD &= "AND T8.Idfletera = " + Me.CmbEnvio.SelectedValue.ToString + " "
   End If



   If Me.CmbFlete.SelectedValue = "SERVLOG" Then
    SQLTPD &= "AND T8.Articulo IN ('FL-005','FL-006','FL-007','FL-008','FL-009','FL-011','FL-012') "

   ElseIf Me.CmbFlete.SelectedValue = "FL-001" Then
    SQLTPD &= "AND (T8.Articulo = 'FL-001' OR T8.Articulo = 'FL-018') "

   ElseIf Me.CmbFlete.SelectedValue = "FL-003" Then
    SQLTPD &= "AND (T8.Articulo = 'FL-003' OR T8.Articulo = 'FL-018') "

   ElseIf Me.CmbFlete.SelectedValue = "FL-002" Then
    SQLTPD &= "AND (T8.Articulo = 'FL-002' OR T8.Articulo = 'FL-018') "

   ElseIf Me.CmbFlete.SelectedValue = "FL-004" Then
    SQLTPD &= "AND (T8.Articulo = 'FL-004' OR T8.Articulo = 'FL-018' OR T8.Articulo = 'FL-013') "


   ElseIf Me.CmbFlete.SelectedValue <> "TODOS" Then
    SQLTPD &= "AND T8.Articulo = '" + Me.CmbFlete.SelectedValue + "' "

   End If

   SQLTPD &= "union all "
   SQLTPD &= "select Entregado, Factura, Fchfact, flete as 'Flete', Codclte, Cliente, Ciudad, Estado, Telefono, Contacto, Comentario as 'ComentarioSAP', Estatus, Total,"
   SQLTPD &= "Fletera, Articulo, Cantidad, Fchemb, crastreo, Recibeguia, Horarec, Fchrec, Zona, Pdeclarado, vguiadec, Pesocob, "
   SQLTPD &= "vguiacob, Difpeso, Difvalor, LineTotal, Fchent, Matrecpor, Diasent, Garantia, comentarios, Idfletera, Usuariocap, "
   SQLTPD &= "Descripcion, Excluir, Cortesia, Reexpedicion, CorrecDatos, EnvIrreg "
   SQLTPD &= "from #real "
   SQLTPD &= "where Factura + Articulo + cast(flete as varchar) not in  "
   SQLTPD &= "(select Factura + Articulo + cast(Flete as varchar) COLLATE Modern_Spanish_CI_AI AS 'id' FROM [TPM].dbo.EMBENT T9  "
   SQLTPD &= "WHERE T9.Fchfact >= @FechaIni AND T9.Fchfact <= @FechaTer) order by Factura, Flete asc "
   SQLTPD &= "drop table #tmp1 "
   SQLTPD &= "drop table #real "


   'SQLTPD = "SELECT T8.Entregado, T8.Factura COLLATE Modern_Spanish_CI_AI AS Factura, T8.Fchfact,T8.Flete, "
   'SQLTPD &= "T8.Codclte COLLATE Modern_Spanish_CI_AI AS Codclte,T8.Cliente COLLATE Modern_Spanish_CI_AI AS Cliente, "
   'SQLTPD &= "T8.Ciudad COLLATE Modern_Spanish_CI_AI AS Ciudad,T8.Estado COLLATE Modern_Spanish_CI_AI AS Estado,"
   'SQLTPD &= "T8.Telefono COLLATE Modern_Spanish_CI_AI AS Telefono,T8.Contacto COLLATE Modern_Spanish_CI_AI AS Contacto, "
   'SQLTPD &= "T8.Fletera COLLATE Modern_Spanish_CI_AI AS Fletera,T8.Articulo COLLATE Modern_Spanish_CI_AI AS Articulo, T8.Cantidad,"
   'SQLTPD &= "T8.Fchemb, T8.crastreo,T8.Recibeguia COLLATE Modern_Spanish_CI_AI AS Recibeguia, T8.Horarec, T8.Fchrec,CASE WHEN T8.Zona is null THEN 0 ELSE T8.Zona END as Zona,T8.Pdeclarado,"
   'SQLTPD &= "T8.vguiadec,T8.Pesocob, T8.vguiacob,T8.Difpeso,T8.Difvalor,T8.LineTotal, T8.Fchent,"
   'SQLTPD &= "T8.Matrecpor COLLATE Modern_Spanish_CI_AI AS Matrecpor, T8.Diasent, T8.Garantia,"
   'SQLTPD &= " T8.comentarios, T8.Idfletera,T8.Usuariocap, T8.Descripcion COLLATE Modern_Spanish_CI_AI AS Descripcion,CASE WHEN T8.Excluir IS NULL THEN 'False' ELSE T8.Excluir END AS Excluir,CASE WHEN T8.Cortesia IS NULL THEN 'False' ELSE T8.Cortesia END AS Cortesia, "
   'SQLTPD &= " t8.Reexpedicion Reexpedicion,t8.CorrecDatos CorrecDatos,T8.EnvIrreg "
   'SQLTPD &= "FROM [TPM19ABR17].dbo.EMBENT T8 "

   'If RadBEmb.Checked = True Then
   '    SQLTPD &= "WHERE T8.Fchemb >= @FechaIni AND T8.Fchemb <= @FechaTer "
   'Else
   '    SQLTPD &= "WHERE T8.Fchfact >= @FechaIni AND T8.Fchfact <= @FechaTer "
   'End If


   'If Me.CmbEnvio.SelectedValue <> 0 Then
   '    SQLTPD &= "AND T8.Idfletera = " + Me.CmbEnvio.SelectedValue.ToString + " "
   'End If



   'If Me.CmbFlete.SelectedValue = "SERVLOG" Then
   '    SQLTPD &= "AND T8.Articulo IN ('FL-005','FL-006','FL-007','FL-008','FL-009','FL-011','FL-012') "

   'ElseIf Me.CmbFlete.SelectedValue = "FL-001" Then
   '    SQLTPD &= "AND (T8.Articulo = 'FL-001' OR T8.Articulo = 'FL-018') "

   'ElseIf Me.CmbFlete.SelectedValue = "FL-003" Then
   '    SQLTPD &= "AND (T8.Articulo = 'FL-003' OR T8.Articulo = 'FL-018') "

   'ElseIf Me.CmbFlete.SelectedValue = "FL-002" Then
   '    SQLTPD &= "AND (T8.Articulo = 'FL-002' OR T8.Articulo = 'FL-018') "

   'ElseIf Me.CmbFlete.SelectedValue = "FL-004" Then
   '    SQLTPD &= "AND (T8.Articulo = 'FL-004' OR T8.Articulo = 'FL-018' OR T8.Articulo = 'FL-013') "


   'ElseIf Me.CmbFlete.SelectedValue <> "TODOS" Then
   '    SQLTPD &= "AND T8.Articulo = '" + Me.CmbFlete.SelectedValue + "' "

   'End If


   ''If Me.CmbFlete.SelectedValue <> "TODOS" Then
   ''    SQLTPD &= "AND T8.Articulo = '" + Me.CmbFlete.SelectedValue + "' "
   ''End If


   'SQLTPD &= "UNION ALL "
   'SQLTPD &= "SELECT 'False' as Entregado,T0.EDocNum as Factura,T0.[DocDate] as Fchfact,CAST(1 AS int) AS Flete,T0.[CardCode] as Codclte,"
   'SQLTPD &= "T0.[CardName] as 'Cliente',T2.City AS Ciudad,T2.State1 AS Estado,T2.Phone1 AS Telefono,T2.CntctPrsn AS Contacto,"
   'SQLTPD &= "TrnspName AS Fletera,T1.ItemCode AS Articulo,T1.Quantity AS Cantidad, T0.[DocDate] as Fchemb,CAST(0 AS varchar(20)) AS crastreo,"
   'SQLTPD &= "CAST('' AS nvarchar(150)) AS Recibeguia,CAST('' AS varchar(5)) AS Horarec,CAST('' AS datetime) AS Fchrec,CAST(0 AS numeric(2, 0)) AS Zona,"
   'SQLTPD &= "CAST('' AS int) AS Pdeclarado,CAST(0 AS numeric(19, 6)) AS vguiadec,CAST('' AS int) AS Pesocob,"
   'SQLTPD &= "CAST(0 AS numeric(19, 6)) AS vguiacob,CAST('' AS int) AS Difpeso,CAST(0 AS numeric(19, 6)) AS Difvalor,T1.LineTotal / T1.Quantity AS LineTotal,"
   'SQLTPD &= "CAST('' AS datetime) AS Fchent,CAST('' AS nvarchar(150)) AS Matrecpor,CAST('' AS int) AS Diasent,CAST('' AS varchar(250)) AS Garantia,"
   'SQLTPD &= "CAST('' AS varchar(250)) AS comentarios,T3.Trnspcode AS Idfletera,CAST('' AS varchar(10)) AS Usuariocap, T1.Dscription AS Descripcion,'False' AS Excluir,'False' AS Cortesia, "
   'SQLTPD &= "CAST(0 AS decimal(12,2)) AS Reexpedicion , CAST(0 AS decimal(12,2)) AS CorrecDatos, CAST(0 AS decimal(12,2)) AS EnvIrreg "
   'SQLTPD &= "FROM [SBO_TPD].dbo.OINV T0 "
   'SQLTPD &= "INNER JOIN [SBO_TPD].dbo.INV1 T1 ON T0.DocEntry = T1.DocEntry "
   'SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OCRD T2 ON T0.CardCode = T2.CardCode "
   'SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OSHP T3 ON T0.Trnspcode = T3.Trnspcode "
   'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "


   ''SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.EDocCancel = 'N' AND T0.DocType = 'I' "


   'If Me.CmbEnvio.SelectedValue <> 0 Then
   '    SQLTPD &= "AND T3.Trnspcode = " + Me.CmbEnvio.SelectedValue.ToString + " "
   'End If



   'If Me.CmbFlete.SelectedValue = "SERVLOG" Then
   '    SQLTPD &= "AND T1.ItemCode IN ('FL-005','FL-006','FL-007','FL-008','FL-009','FL-011','FL-012') "
   'ElseIf Me.CmbFlete.SelectedValue = "FL-001" Then
   '    SQLTPD &= "AND (T1.ItemCode = 'FL-001' OR T1.ItemCode = 'FL-018') "
   'ElseIf Me.CmbFlete.SelectedValue = "FL-002" Then
   '    SQLTPD &= "AND (T1.ItemCode = 'FL-002' OR T1.ItemCode = 'FL-018') "
   'ElseIf Me.CmbFlete.SelectedValue = "FL-003" Then
   '    SQLTPD &= "AND (T1.ItemCode = 'FL-003' OR T1.ItemCode = 'FL-018') "

   'ElseIf Me.CmbFlete.SelectedValue = "FL-004" Then
   '    SQLTPD &= "AND (T1.ItemCode = 'FL-004' OR T1.ItemCode = 'FL-018' OR T1.ItemCode = 'FL-013') "

   'ElseIf Me.CmbFlete.SelectedValue <> "TODOS" Then
   '    SQLTPD &= "AND T1.ItemCode = '" + Me.CmbFlete.SelectedValue + "' "
   'End If

   ''AND T4.ItemCode <> 'FL-003' 

   'SQLTPD &= "AND T1.ItemCode IN "
   'SQLTPD &= "(SELECT T4.ItemCode FROM [SBO_TPD].dbo.OITM T4 WHERE T4.ItmsGrpCod = 150) "
   ''SQLTPD &= "AND T4.ItemCode <> 'FL-002') "  ANTERIOR LINEA DE CODIGO 10/MARZO/2017
   'SQLTPD &= "AND T0.EDocNum + T1.ItemCode NOT IN "
   'SQLTPD &= "(SELECT T9.Factura + T9.Articulo COLLATE Modern_Spanish_CI_AI AS Factura FROM [TPM19ABR17].dbo.EMBENT T9 "
   'SQLTPD &= "WHERE T9.Fchfact >= @FechaIni AND T9.Fchfact <= @FechaTer)"




   With comando
    .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
    .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)

    ' Asignar el sql para seleccionar los datos de la tabla Maestro   
    .CommandText = SQLTPD
    .Connection = conexion2
   End With

   Dim DtFactProv As New DataTable

   With Adaptador
    .SelectCommand = comando
    ' llenar el dataset   
    .Fill(DtFactProv)
   End With


   'Clona la estructura del datatable pero sin registros
   Dim DtFormatFlete As DataTable = DtFactProv.Clone()
   ''Dim DtFormatFlete As New DataTable

   'DtFormatFlete = DtFactProv
   Dim vFlete As Integer = 1

   For Each row As DataRow In DtFactProv.Rows
    vCant = 0
    vIni = row("Fchemb")
    vFchComp = row("Fchemb")
    vFin = row("Fchent")

    vGarantia = ""

    If vFin.Year <> 1900 Then
     DiasHab()
     row("Diasent") = vCant
     row("Garantia") = vGarantia
    End If

    'If row("Cantidad") > 1 And row("Usuariocap").ToString = "" Then
    '    For i = 0 To row("Cantidad") - 1

    '        'dt1.ImportRow(dr1)
    '        'DtFactProv.ImportRow(DtFormatFlete)
    '        'DtFormatFlete()
    '        row("Flete") = vFlete

    '        DtFormatFlete.ImportRow(row)
    '        vFlete += 1

    '    Next
    'Else
    DtFormatFlete.ImportRow(row)
    'End If
    'row("Flete") = 1
    vFlete = 1
   Next

   DvFletes = DtFormatFlete.DefaultView

   'DvFactProv = DtFactProv.DefaultView

   With Me.DgFactProv
    .DataSource = DvFletes 'DtFactProv 'DtFormatFlete    
    '.DataSource = DtFactProv 'DtFactProv 'DtFormatFlete    
    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    .DefaultCellStyle.BackColor = Color.AliceBlue
    '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


    '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    .Columns("Total").DefaultCellStyle.Format = "$ ###,###,###,###.#0"
    Dim numfilas As Integer
    numfilas = DgFactProv.RowCount 'cuenta las filas del DataGrid

    'recorre las filas del DataGrid
    For i = 0 To (numfilas - 1)

     If DgFactProv.Item("Articulo", i).Value = "FL-003" Or DgFactProv.Item("Articulo", i).Value = "FL-103" Then

      'DgFactProv.Item(18, i).ReadOnly = True
      'DgFactProv.Item(19, i).ReadOnly = True
      'DgFactProv.Item(20, i).ReadOnly = True
      'DgFactProv.Item(21, i).ReadOnly = True

      DgFactProv.Item("vguiacob", i).Value = 94.83
     End If

    Next
   End With

   DgFactProv.Sort(DgFactProv.Columns("Fchemb"), System.ComponentModel.ListSortDirection.Ascending)

   With conexion2
    If .State = ConnectionState.Open Then
     .Close()
    End If
    .Dispose()
   End With

   'With DgFactProv

   '    .Columns(36).HeaderText = "Reexpe- dicion"
   '    .Columns(36).Width = 50

   '    .Columns(37).Width = 55

   'End With


  Catch ex As Exception
   MsgBox(ex.Message)
  End Try

 End Sub

 Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
  Button1.Enabled = False
  If IsNothing(CmbFlete.SelectedValue) Then
   MessageBox.Show("Seleccione un agente de ventas",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
   CmbFlete.Focus()
   Return
  End If

  cargar_registros()
  VerEntregados()

  Button1.Enabled = True

  If CmbFlete.Text = "FL-003 SERVICIOS DE LOGISTICA PAQ EXP" Or CmbFlete.Text = "FL-004 SERVICIOS DE LOGISTICA LOGEX" Or CmbFlete.Text = "FL-103 SERVICIOS DE LOGISTICA PAQ EXP BF" Or CmbFlete.Text = "FL-104 SERVICIOS DE LOGISTICA LOGEX BF" Then        'UsrTPM = "RLIRA"
   DgFactProv.Columns.Item("zona").Visible = False
   DgFactProv.Columns.Item("pdeclarado").Visible = False
   DgFactProv.Columns.Item("pesocob").Visible = False
   DgFactProv.Columns.Item("vguiadec").Visible = False

  Else
   DgFactProv.Columns.Item("zona").Visible = True
   DgFactProv.Columns.Item("pdeclarado").Visible = True
   DgFactProv.Columns.Item("pesocob").Visible = True
   DgFactProv.Columns.Item("vguiadec").Visible = True
  End If

 End Sub

 Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  RadBEmb.Checked = True
  Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
  Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

  Dim Consulta As String
  Dim DsetFletes As New DataSet

  Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

   'Consulta = "SELECT ItemCode,ItemCode + ' ' + ItemName AS ItemName  FROM OITM T0 WHERE T0.ItmsGrpCod = 150 "
   'Consulta &= "AND ItemCode <> 'FL-002' AND ItemCode <> 'FL-003' AND ItemCode <> 'FL-004'"

   Consulta = "SELECT ItemCode,ItemCode + ' ' + ItemName AS ItemName FROM OITM T0 "
   'Consulta &= "WHERE T0.ItmsGrpCod = 150 AND  (ItemCode = 'FL-001' OR ItemCode = 'FL-002' OR ItemCode = 'FL-003' OR ItemCode = 'FL-004' OR ItemCode = 'FL-010' or ItemCode = 'FL-101' OR ItemCode = 'FL-103' OR ItemCode = 'FL-104')  "
   Consulta &= "WHERE T0.ItmsGrpCod = 150 "
   'Analizando un poco esto y para que de forma automatica el sistema tome o considere los fletes que se registren en sap pondre la siguiente condicion
   'Exceptuando los que tengan en su descripcion la palabra "kms" que son los que actualmente no esta considerando.

   Consulta &= "AND CHARINDEX('kms',ItemName) = 0 "

   Dim DadFlete As New Data.SqlClient.SqlDataAdapter(Consulta, StrCon)

   DadFlete.Fill(DsetFletes, "Fletes")

   Dim FilaFlete As Data.DataRow

   FilaFlete = DsetFletes.Tables("Fletes").NewRow

   FilaFlete("ItemCode") = "SERVLOG"
   FilaFlete("ItemName") = "SERVICIOS LOGISTICOS EN TARIMA"

   DsetFletes.Tables("Fletes").Rows.Add(FilaFlete)


   FilaFlete = DsetFletes.Tables("Fletes").NewRow

   FilaFlete("ItemCode") = "TODOS"
   FilaFlete("ItemName") = "TODOS"

   DsetFletes.Tables("Fletes").Rows.Add(FilaFlete)
   Me.CmbFlete.DataSource = DsetFletes.Tables("Fletes")
   Me.CmbFlete.DisplayMember = "ItemName"
   Me.CmbFlete.ValueMember = "ItemCode"
   Me.CmbFlete.SelectedValue = "TODOS"


   Consulta = "SELECT Trnspcode,TrnspName FROM OSHP ORDER BY TrnspName"
   Dim daEnvio As New SqlClient.SqlDataAdapter(Consulta, SqlConnection)

   Dim FilaPaqueteria As Data.DataRow

   daEnvio.Fill(DsetFletes, "Paqueteria")

   FilaPaqueteria = DsetFletes.Tables("Paqueteria").NewRow

   FilaPaqueteria("Trnspcode") = 0
   FilaPaqueteria("TrnspName") = "TODOS"

   DsetFletes.Tables("Paqueteria").Rows.Add(FilaPaqueteria)

   Me.CmbEnvio.DataSource = DsetFletes.Tables("Paqueteria")
   Me.CmbEnvio.DisplayMember = "TrnspName"
   Me.CmbEnvio.ValueMember = "Trnspcode"

   CmbEnvio.SelectedValue = 0

  End Using


  If UsrTPM = "RLIRA" Or UsrTPM = "LMARTINEZ" Then
   'TxtTotFletes.Text = Format(DgFactProv.RowCount, "##,###,###,###")


   'TxtPesoDec.Text = Format(VPesoDec, "##,###,###,###")
   TxtValorDec.Visible = False
   TxtCostoSap.Visible = False
   TxtValorDec.Visible = False
   LblValorDec2.Visible = False
   LblDifValor1.Visible = False
   LblPrecioSAP.Visible = False
   LblTotalEstafeta.Visible = False
   TxtTotEstafeta.Visible = False
   'TxtPesoCob.Text = Format(VPesoCob, "##,###,###,###")
   TxtValorCob.Visible = False
   LblValorCob2.Visible = False

   'TxtDifPeso.Text = Format(VDifPeso, "##,###,###,###")
   TxtDifValor.Visible = False
   LblDifValor1.Visible = False



   TxtCostoSap.Visible = False



   DgFactProv.Columns.Item("vguiadec").Visible = False
   DgFactProv.Columns.Item("vguiacob").Visible = False
   DgFactProv.Columns.Item("Difvalor").Visible = False
   DgFactProv.Columns.Item("LineTotal").Visible = False
  End If
  DgFactProv.Columns.Item("Garantia").Visible = False



 End Sub

 Sub VerEntregados()
  Dim VFiltro As String = " "

  'If ChkFEntregado.Checked = False Then
  '    VFiltro = "Entregado  = 0"
  'End If

  'If VFiltro = " " Then
  '    DvFletes.RowFilter = String.Empty
  'Else
  '    DvFletes.RowFilter = VFiltro
  'End If



  'False' AS Excluir,'False' AS Cortesia 


  If ChkFEntregado.Checked = False Then
   VFiltro = "Excluir = 'False' AND Cortesia = 'False' AND Entregado = 0"
  Else
   VFiltro = "Excluir = 'False' AND Cortesia = 'False'"
  End If


  If ChkExcCortesia.Checked = True Then
   If ChkFEntregado.Checked = False Then

    VFiltro = "Excluir = 'True' OR Cortesia = 'True' AND Entregado  = 0"
   Else
    VFiltro = "Excluir = 'True' OR Cortesia = 'True'"

   End If
  End If


  If VFiltro = " " Then
   DvFletes.RowFilter = String.Empty
  Else
   DvFletes.RowFilter = VFiltro
  End If


  TotalFacturas()
 End Sub

 Private Sub ChkFEntregado_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkFEntregado.CheckedChanged
  VerEntregados()
 End Sub

 Sub TotalFacturas()
  Dim VPesoDec As Decimal = 0
  Dim VValorDec As Decimal = 0
  Dim VPesoCob As Decimal = 0
  Dim VValorCob As Decimal = 0
  Dim VDifPeso As Decimal = 0
  Dim VDifValor As Decimal = 0
  Dim VLineTotal As Decimal = 0
  Dim VReexpedicion As Decimal = 0
  Dim VCorrecDts As Decimal = 0
  Dim VEnvIrreg As Decimal = 0

  For Each row As DataGridViewRow In Me.DgFactProv.Rows

   If row.Cells("Excluir").Value = False Then

    If row.Cells("Cortesia").Value = False Then

     VPesoDec += row.Cells("Pdeclarado").Value
     VValorDec += row.Cells("vguiadec").Value
     VPesoCob += row.Cells("Pesocob").Value
     VValorCob += row.Cells("vguiacob").Value
     VDifPeso += row.Cells("Difpeso").Value
     VDifValor += row.Cells("Difvalor").Value
     VReexpedicion += IIf(IsDBNull(row.Cells("Reexpedicion").Value), 0, row.Cells("Reexpedicion").Value)
     VCorrecDts += IIf(IsDBNull(row.Cells("CorrecDatos").Value), 0, row.Cells("CorrecDatos").Value)
     VEnvIrreg += IIf(IsDBNull(row.Cells("EnvIrreg").Value), 0, row.Cells("EnvIrreg").Value)
    End If

    VLineTotal += IIf(IsDBNull(row.Cells("LineTotal").Value), 0, row.Cells("LineTotal").Value)
   End If


   'If row.Cells("RegSel").Value = False Then
   '    VTotFProv += row.Cells("SaldoPesos").Value
   'End If

  Next

  TxtTotFletes.Text = Format(DgFactProv.RowCount, "##,###,###,###")


  TxtPesoDec.Text = Format(VPesoDec, "##,###,###,###")
  TxtValorDec.Text = Format(VValorDec, "$ ##,###,###,###.00")
  TxtPesoCob.Text = Format(VPesoCob, "##,###,###,###")
  TxtValorCob.Text = Format(VValorCob, "$ ##,###,###,###.00")
  TxtDifPeso.Text = Format(VDifPeso, "##,###,###,###")
  TxtDifValor.Text = Format(VDifValor, "$ ##,###,###,###.00")
  TxtCostoSap.Text = Format(VLineTotal, "$ ##,###,###,###.00")
  txtReexpedicion.Text = Format(VReexpedicion, "$ ##,###,###,###.00")
  txtCorreccionDts.Text = Format(VCorrecDts, "$ ##,###,###,###.00")
  TxtEnvIrreg.Text = Format(VEnvIrreg, "$ ##,###,###,###.00")
  TxtTotEstafeta.Text = Format(VEnvIrreg + VCorrecDts + VReexpedicion + VValorCob, "$ ##,###,###,###.00")


 End Sub

 Private Sub BtnDetalle_Click(sender As System.Object, e As System.EventArgs) Handles BtnDetalle.Click
  ''Creamos las variables
  'Dim exApp As New Microsoft.Office.Interop.Excel.Application
  'Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
  'Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
  'Try
  '    'Añadimos el Libro al programa, y la hoja al libro
  '    exLibro = exApp.Workbooks.Add
  '    exHoja = exLibro.Worksheets.Add()
  '    ' ¿Cuantas columnas y cuantas filas?
  '    Dim NCol As Integer = DgFactProv.ColumnCount
  '    Dim NRow As Integer = DgFactProv.RowCount
  '    'Aqui recorremos todas las filas, y por cada fila todas las columnas
  '    'y vamos escribiendo.
  '    For i As Integer = 1 To NCol
  '        exHoja.Cells.Item(1, i) = DgFactProv.Columns(i - 1).Name.ToString
  '    Next
  '    For Fila As Integer = 0 To NRow - 1
  '        For Col As Integer = 0 To NCol - 1
  '            exHoja.Cells.Item(Fila + 2, Col + 1) = _
  '            DgFactProv.Rows(Fila).Cells(Col).Value
  '        Next
  '    Next
  '    'Titulo en negrita, Alineado al centro y que el tamaño de la columna
  '    'se ajuste al texto
  '    exHoja.Rows.Item(1).Font.Bold = 1
  '    exHoja.Rows.Item(1).HorizontalAlignment = 3
  '    exHoja.Columns.AutoFit()
  '    'Aplicación visible
  '    exApp.Application.Visible = True
  '    exHoja = Nothing
  '    exLibro = Nothing
  '    exApp = Nothing
  'Catch ex As Exception
  '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
  '    Return
  'End Try


  'GridAExcel(DgFactProv)

  Dim Titulo As String

  Dim sqlConnection1 As New SqlConnection(conexion_universal.CadenaSQLSAP)
  Dim cmd As New SqlCommand

  Dim cnn As SqlConnection = Nothing
  Titulo = "Reporte de Embarques"

  Dim Columnas As String() = {"Entregado",
                                "Factura",
                                "Fecha Factura",
                                "# Paq.",
                                "Codclte",
                                "Cliente",
                                "Ciudad",
                                "Estado",
                                "Teléfono",
                                "Contacto",
                                "Comentario SAP",
                                "Estatus",
                                "Total Factura",
                                "Servicio",
                                "Artículo",
                                "Total paq.",
                                "Fecha embarque",
                                "Cod. Rastreo",
                                "Persona Recibe cod. rastreo",
                                "Horario confirmación",
                                "Día confirmación cod. rastreo",
                                "Zona",
                                "Peso declarado",
                                "$ Valor guia dec",
                                "Peso cobrado",
                                "$ Valor guia cob",
                                "Dif. peso",
                                "$ Dif. valor guia",
                                "$ Costo SAP",
                                "Fecha entrega",
                                "Persona recibe material",
                                "Dias trans. entrega",
                                "Garantia", 'Oculto
                                "Comentarios",
                                "Idfletera", 'Oculto
                                "Usuariocap", 'Oculto
                                "Descripcion", 'Oculto
                                "Excluir",
                                "Cortesía",
                                "Reexpedición",
                                "Corrección de datos",
                                "Envios irregulares"}
  Dim TipoColumna As TipoDeDato() = {TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Entero,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.NumDecimal,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.NumDecimal,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.NumDecimal,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena}
  Dim Visible As Boolean() = {True,
                                True,
                                True,
                                True,
                                False,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                False,
                                True,
                                False,
                                False,
                                False,
                                True,
                                True,
                                True,
                                True,
                                True}

  funciones.exporta2Excel(Titulo, Columnas, TipoColumna, Visible, DgFactProv)

 End Sub

 Sub ActualizaValores()
  ' crear nueva conexión    
  Dim conexion As New SqlConnection(StrTpm)

  '' abrir la conexión con la base de datos   
  'conexion.Open()

  Dim Adaptador As New SqlDataAdapter()
  Dim comando As New SqlCommand

  Dim SQLTPD As String

  SQLTPD = "SELECT count(*) AS Num FROM EMBENT WHERE Factura = '" + DgFactProv.CurrentRow.Cells("Factura").Value + "' and Flete = " & DgFactProv.CurrentRow.Cells("Flete").Value

  Dim DsVtasDet As New DataSet

  'DsVtasDet.Tables.Add(DTRefacciones)

  Dim vRegFact As Integer = 0
  With comando
   .CommandText = SQLTPD
   .Connection = conexion
   .Connection.Open()
   vRegFact = IIf(IsDBNull(.ExecuteScalar), 0, .ExecuteScalar)
   .Connection.Close()
  End With

  'MsgBox("Numero de apariciones: " & vRegFact)
  'Return
  'DrFact.Read()

  'Dim vDes As String
  'vDes = QuitarCaracteres(fila("Descripcion").ToString)

  If vRegFact >= 1 Then

   SQLTPD = "UPDATE EMBENT SET Entregado = "
   SQLTPD &= IIf(DgFactProv.CurrentRow.Cells("Entregado").Value = False, 0, 1).ToString
   SQLTPD &= ", "
   SQLTPD &= "Fchemb = @Fchemb" 'DgFactProv.CurrentRow.Cells("Fchemb").Value
   SQLTPD &= ", "
   SQLTPD &= "crastreo = '" + DgFactProv.CurrentRow.Cells("crastreo").Value.ToString
   SQLTPD &= "', "
   SQLTPD &= "Pdeclarado = " + DgFactProv.CurrentRow.Cells("Pdeclarado").Value.ToString
   SQLTPD &= ", "
   SQLTPD &= "vguiadec = " + DgFactProv.CurrentRow.Cells("vguiadec").Value.ToString
   SQLTPD &= ", "
   SQLTPD &= "Zona = " + DgFactProv.CurrentRow.Cells("Zona").Value.ToString
   SQLTPD &= ", "
   SQLTPD &= "Pesocob = " + DgFactProv.CurrentRow.Cells("Pesocob").Value.ToString
   SQLTPD &= ", "
   SQLTPD &= "vguiacob = " + DgFactProv.CurrentRow.Cells("vguiacob").Value.ToString
   SQLTPD &= ", "
   SQLTPD &= "Difpeso = " + DgFactProv.CurrentRow.Cells("Difpeso").Value.ToString
   SQLTPD &= ", "
   SQLTPD &= "Difvalor = " + DgFactProv.CurrentRow.Cells("Difvalor").Value.ToString
   SQLTPD &= ", "
   SQLTPD &= "LineTotal = " + IIf(IsDBNull(DgFactProv.CurrentRow.Cells("LineTotal").Value), 0, DgFactProv.CurrentRow.Cells("LineTotal").Value).ToString
   SQLTPD &= ", "
   SQLTPD &= "Fchent = @Fchent" 'DgFactProv.CurrentRow.Cells("Fchent").Value
   SQLTPD &= ", "
   SQLTPD &= "Matrecpor = '" + DgFactProv.CurrentRow.Cells("Matrecpor").Value + "'"
   SQLTPD &= ", "
   SQLTPD &= "Diasent = " + DgFactProv.CurrentRow.Cells("Diasent").Value.ToString
   SQLTPD &= ", "
   SQLTPD &= "Garantia = '" + IIf(IsDBNull(DgFactProv.CurrentRow.Cells("Garantia").Value), "", DgFactProv.CurrentRow.Cells("Garantia").Value).ToString + "'"
   SQLTPD &= ", "
   SQLTPD &= "Recibeguia = '" + DgFactProv.CurrentRow.Cells("Recibeguia").Value + "'"
   SQLTPD &= ", "
   SQLTPD &= "Horarec = '" + DgFactProv.CurrentRow.Cells("Horarec").Value + "'"
   SQLTPD &= ", "
   SQLTPD &= "Fchrec = @Fchrec" 'DgFactProv.CurrentRow.Cells("Fchrec").Value
   SQLTPD &= ", "
   SQLTPD &= "comentarios = '" + QuitarCaracteres(DgFactProv.CurrentRow.Cells("comentarios").Value) + "'"
   'SQLTPD &= "comentarios = '" + DgFactProv.CurrentRow.Cells("comentarios").Value + "'"
   SQLTPD &= ", "
   SQLTPD &= "Usuarioact = '" + UsrTPM + "'"
   SQLTPD &= ", "
   SQLTPD &= "Fchact = @Fchact "
   SQLTPD &= ", "
   SQLTPD &= "Excluir = " + IIf(DgFactProv.CurrentRow.Cells("Excluir").Value = False, 0, 1).ToString
   SQLTPD &= ", "
   SQLTPD &= "Cortesia = " + IIf(DgFactProv.CurrentRow.Cells("Cortesia").Value = False, 0, 1).ToString + " "
   SQLTPD &= ", "
   SQLTPD &= "Reexpedicion = " + IIf(IsDBNull(DgFactProv.CurrentRow.Cells("Reexpedicion").Value), 0, DgFactProv.CurrentRow.Cells("Reexpedicion").Value).ToString
   SQLTPD &= ", "
   SQLTPD &= "CorrecDatos  = " + IIf(IsDBNull(DgFactProv.CurrentRow.Cells("CorrecDatos").Value), 0, DgFactProv.CurrentRow.Cells("CorrecDatos").Value).ToString
   SQLTPD &= ", "
   SQLTPD &= "EnvIrreg = " + IIf(IsDBNull(DgFactProv.CurrentRow.Cells("EnvIrreg").Value), 0, DgFactProv.CurrentRow.Cells("EnvIrreg").Value).ToString
   'False' AS Excluir,'False' AS Cortesia


   SQLTPD &= " WHERE Factura = '" + DgFactProv.CurrentRow.Cells("Factura").Value + "' "
   SQLTPD &= " AND Flete = " + DgFactProv.CurrentRow.Cells("Flete").Value.ToString
   'DgFactProv.CurrentRow.Cells("Flete").Value

   Dim CmdActFlet As Data.SqlClient.SqlCommand

   CmdActFlet = New Data.SqlClient.SqlCommand()
   With CmdActFlet
    .Parameters.AddWithValue("@Fchemb", DgFactProv.CurrentRow.Cells("Fchemb").Value)
    .Parameters.AddWithValue("@Fchent", DgFactProv.CurrentRow.Cells("Fchent").Value)
    .Parameters.AddWithValue("@Fchrec", DgFactProv.CurrentRow.Cells("Fchrec").Value)
    .Parameters.AddWithValue("@Fchact", DateTime.Now)
    .Connection = New Data.SqlClient.SqlConnection(StrTpm)
    .Connection.Open()
    .CommandText = SQLTPD
    .ExecuteNonQuery()
    .Connection.Close()
   End With

  Else
   'For i = 0 To Val(DgFactProv.CurrentRow.Cells("Cantidad").Value) - 1
   'DgFactProv.Rows(e.RowIndex).Cells("LineTotal").Style.BackColor = Color.Red
   'DgFactProv.Rows(e.RowIndex).Cells("LineTotal").Style.ForeColor = Color.White


   SQLTPD = "INSERT INTO EMBENT (Entregado, Factura, Fchfact, Flete, Codclte, Cliente, Ciudad, Estado, Fletera, Articulo, Descripcion, "
   SQLTPD &= "Cantidad, Fchemb, crastreo, Zona, Pdeclarado, vguiadec, Pesocob, vguiacob, Difpeso, Difvalor,LineTotal, Fchent, Matrecpor, Diasent, "
   SQLTPD &= "Garantia, Recibeguia, Horarec, Fchrec, Telefono, Contacto, comentarios, Idfletera, Usuariocap, Fchcap, Usuarioact, Fchact, Excluir, Cortesia, "
   SQLTPD &= "Reexpedicion,CorrecDatos, EnvIrreg) "
   SQLTPD &= "VALUES("

   'If i + 1 = DgFactProv.CurrentRow.Cells("Flete").Value Then

   SQLTPD &= IIf(DgFactProv.CurrentRow.Cells("Entregado").Value = False, 0, 1).ToString
   SQLTPD &= ",'"

   SQLTPD &= DgFactProv.CurrentRow.Cells("Factura").Value
   SQLTPD &= "',"
   SQLTPD &= "@Fchfact" 'DgFactProv.CurrentRow.Cells("Fchfact").Value
   SQLTPD &= ","
   'SQLTPD &= (i + 1).ToString 'DgFactProv.CurrentRow.Cells("Flete").Value
   SQLTPD &= DgFactProv.CurrentRow.Cells("Flete").Value
   SQLTPD &= ",'"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Codclte").Value
   SQLTPD &= "','"
   SQLTPD &= QuitarCaracteres(DgFactProv.CurrentRow.Cells("Cliente").Value.ToString)
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Cliente").Value
   SQLTPD &= "','"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Ciudad").Value
   SQLTPD &= "','"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Estado").Value
   SQLTPD &= "','"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Fletera").Value
   SQLTPD &= "','"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Articulo").Value
   SQLTPD &= "','"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Descripcion").Value
   SQLTPD &= "',"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Cantidad").Value.ToString
   SQLTPD &= ","
   SQLTPD &= "@Fchemb" 'DgFactProv.CurrentRow.Cells("Fchemb").Value
   SQLTPD &= ",'"
   SQLTPD &= DgFactProv.CurrentRow.Cells("crastreo").Value
   SQLTPD &= "',"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Zona").Value
   SQLTPD &= ","
   SQLTPD &= DgFactProv.CurrentRow.Cells("Pdeclarado").Value
   SQLTPD &= ","
   SQLTPD &= DgFactProv.CurrentRow.Cells("vguiadec").Value
   SQLTPD &= ","
   SQLTPD &= DgFactProv.CurrentRow.Cells("Pesocob").Value
   SQLTPD &= ","
   SQLTPD &= DgFactProv.CurrentRow.Cells("vguiacob").Value
   SQLTPD &= ","
   SQLTPD &= DgFactProv.CurrentRow.Cells("Difpeso").Value
   SQLTPD &= ","
   SQLTPD &= DgFactProv.CurrentRow.Cells("Difvalor").Value
   SQLTPD &= ","
   SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("LineTotal").Value), 0, DgFactProv.CurrentRow.Cells("LineTotal").Value).ToString
   SQLTPD &= ","
   SQLTPD &= "@Fchent" 'DgFactProv.CurrentRow.Cells("Fchent").Value
   SQLTPD &= ",'"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Matrecpor").Value
   SQLTPD &= "',"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Diasent").Value
   SQLTPD &= ",'"
   SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("Garantia").Value), "", DgFactProv.CurrentRow.Cells("Garantia").Value).ToString
   SQLTPD &= "',' "
   SQLTPD &= DgFactProv.CurrentRow.Cells("Recibeguia").Value
   SQLTPD &= "','"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Horarec").Value
   SQLTPD &= "',"
   SQLTPD &= "@Fchrec" 'DgFactProv.CurrentRow.Cells("Fchrec").Value
   SQLTPD &= ",'"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Telefono").Value
   SQLTPD &= "','"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Contacto").Value
   SQLTPD &= "','"
   SQLTPD &= QuitarCaracteres(DgFactProv.CurrentRow.Cells("comentarios").Value)
   'SQLTPD &= DgFactProv.CurrentRow.Cells("comentarios").Value
   SQLTPD &= "',"
   SQLTPD &= DgFactProv.CurrentRow.Cells("Idfletera").Value
   SQLTPD &= ",'"
   SQLTPD &= UsrTPM
   SQLTPD &= "',"
   SQLTPD &= "@Fchcap" 'DgFactProv.CurrentRow.Cells("Fchcap").Value
   SQLTPD &= ",'"
   SQLTPD &= UsrTPM
   SQLTPD &= "',"
   SQLTPD &= "@Fchact" 'DgFactProv.CurrentRow.Cells("Fchact").Value
   SQLTPD &= ","
   SQLTPD &= IIf(DgFactProv.CurrentRow.Cells("Excluir").Value = False, 0, 1).ToString
   SQLTPD &= ","
   SQLTPD &= IIf(DgFactProv.CurrentRow.Cells("Cortesia").Value = False, 0, 1).ToString
   SQLTPD &= ","
   SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("Reexpedicion").Value), 0, DgFactProv.CurrentRow.Cells("Reexpedicion").Value).ToString
   SQLTPD &= ","
   SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("CorrecDatos").Value), 0, DgFactProv.CurrentRow.Cells("CorrecDatos").Value).ToString
   SQLTPD &= ","
   SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("EnvIrreg").Value), 0, DgFactProv.CurrentRow.Cells("EnvIrreg").Value).ToString
   'SQLTPD &= ", "
   'SQLTPD &= "Excluir = " + IIf(DgFactProv.CurrentRow.Cells("Excluir").Value = False, 0, 1)
   'SQLTPD &= ", "
   'SQLTPD &= "Cortesia = " + IIf(DgFactProv.CurrentRow.Cells("Cortesia").Value = False, 0, 1) + " "


   SQLTPD &= ")"
   'Else
   'SQLTPD &= "0"
   'SQLTPD &= ",'"

   'SQLTPD &= DgFactProv.CurrentRow.Cells("Factura").Value
   'SQLTPD &= "',"
   'SQLTPD &= "@Fchfact" 'DgFactProv.CurrentRow.Cells("Fchfact").Value
   'SQLTPD &= ","
   'SQLTPD &= (i + 1).ToString 'DgFactProv.CurrentRow.Cells("Flete").Value
   'SQLTPD &= ",'"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Codclte").Value
   'SQLTPD &= "','"
   'SQLTPD &= QuitarCaracteres(DgFactProv.CurrentRow.Cells("Cliente").Value.ToString)
   ''SQLTPD &= DgFactProv.CurrentRow.Cells("Cliente").Value
   'SQLTPD &= "','"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Ciudad").Value
   'SQLTPD &= "','"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Estado").Value
   'SQLTPD &= "','"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Fletera").Value
   'SQLTPD &= "','"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Articulo").Value
   'SQLTPD &= "','"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Descripcion").Value
   'SQLTPD &= "',"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Cantidad").Value
   'SQLTPD &= ","
   'SQLTPD &= "@Fchemb" 'DgFactProv.CurrentRow.Cells("Fchemb").Value
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("LineTotal").Value), 0, DgFactProv.CurrentRow.Cells("LineTotal").Value).ToString
   'SQLTPD &= ","
   'SQLTPD &= "@Fchent" 'DgFactProv.CurrentRow.Cells("Fchent").Value
   'SQLTPD &= ",'"
   'SQLTPD &= ""
   'SQLTPD &= "',"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Diasent").Value
   'SQLTPD &= ",'"
   'SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("Garantia").Value), "", DgFactProv.CurrentRow.Cells("Garantia").Value).ToString
   'SQLTPD &= "',' "
   'SQLTPD &= ""
   'SQLTPD &= "','"
   'SQLTPD &= ""
   'SQLTPD &= "',"
   'SQLTPD &= "@Fchrec" 'DgFactProv.CurrentRow.Cells("Fchrec").Value
   'SQLTPD &= ",'"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Telefono").Value
   'SQLTPD &= "','"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Contacto").Value
   'SQLTPD &= "','"
   'SQLTPD &= ""
   'SQLTPD &= "',"
   'SQLTPD &= DgFactProv.CurrentRow.Cells("Idfletera").Value
   'SQLTPD &= ",'"
   'SQLTPD &= UsrTPM
   'SQLTPD &= "',"
   'SQLTPD &= "@Fchcap" 'DgFactProv.CurrentRow.Cells("Fchcap").Value
   'SQLTPD &= ",'"
   'SQLTPD &= UsrTPM
   'SQLTPD &= "',"
   'SQLTPD &= "@Fchact" 'DgFactProv.CurrentRow.Cells("Fchact").Value
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ","
   'SQLTPD &= "0"
   'SQLTPD &= ")"
   'End If


   'Try
   Dim CmdWom As Data.SqlClient.SqlCommand

   CmdWom = New Data.SqlClient.SqlCommand()
   With CmdWom

    .Parameters.AddWithValue("@Fchfact", DgFactProv.CurrentRow.Cells("Fchfact").Value)
    .Parameters.AddWithValue("@Fchcap", DateTime.Now)
    .Parameters.AddWithValue("@Fchact", DateTime.Now)
    .Parameters.AddWithValue("@Fchemb", DgFactProv.CurrentRow.Cells("Fchemb").Value)
    'If i + 1 = DgFactProv.CurrentRow.Cells("Flete").Value Then
    .Parameters.AddWithValue("@Fchent", DgFactProv.CurrentRow.Cells("Fchent").Value)
    .Parameters.AddWithValue("@Fchrec", DgFactProv.CurrentRow.Cells("Fchrec").Value)
    'Else
    '.Parameters.AddWithValue("@Fchent", "1900/01/01")
    '.Parameters.AddWithValue("@Fchrec", "1900/01/01")
    'End If
    '.Parameters.AddWithValue("@Fecha", DateTime.Now)
    .Connection = New Data.SqlClient.SqlConnection(StrTpm)
    .Connection.Open()
    .CommandText = SQLTPD
    .ExecuteNonQuery()
    .Connection.Close()
   End With
   'Catch exSql As SqlClient.SqlException
   '    MessageBox.Show("INSTRUCCION SQL AL MODIFICAR DATOS" + exSql.Message.ToString, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error)
   'Catch ex As Exception
   '    MessageBox.Show("AL MODIFICAR DATOS" + Convert.ToString(ex), " E R R O R ! ! !", MessageBoxButtons.OK, MessageBoxIcon.Error)
   'Finally
   'End Try

   'Next

  End If

 End Sub

 Sub DiasHab()
  While vFin <> vIni
   vIni = vIni.AddDays(1)
   If Not (vIni.DayOfWeek = DayOfWeek.Sunday Or vIni.DayOfWeek = DayOfWeek.Saturday) Then
    vCant = vCant + 1
   End If
  End While

  If vFchComp.DayOfWeek = DayOfWeek.Friday Then
   vFchComp = vFchComp.AddDays(3)

  ElseIf vFchComp.DayOfWeek = DayOfWeek.Saturday Then
   vFchComp = vFchComp.AddDays(2)
  Else
   vFchComp = vFchComp.AddDays(1)
  End If

  If vFin > vFchComp Then
   vGarantia = "RECLAMAR"
  End If

 End Sub

 Private Sub DgFactProv_CancelRowEdit(sender As System.Object, e As System.Windows.Forms.QuestionEventArgs)
  vActvalor = 0
 End Sub

 Private Sub ChkComsitex_CheckedChanged(sender As System.Object, e As System.EventArgs)
  VerEntregados()
 End Sub

 Private Sub ChkExcCortesia_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkExcCortesia.CheckedChanged
  VerEntregados()
 End Sub

 '    'Crer dataset para exportar
 '    Dim dset As New DataSet
 '    'add table to dataset
 '    dset.Tables.Add()
 '    'agregar columnas a la tabla
 '    For i As Integer = 0 To DgFactProv.ColumnCount - 1
 '        dset.Tables(0).Columns.Add(DgFactProv.Columns(i).HeaderText)
 '    Next
 '    'agregar filas a la tabla
 '    Dim dr1 As DataRow
 '    For i As Integer = 0 To DgFactProv.RowCount - 1
 '        dr1 = dset.Tables(0).NewRow
 '        For j As Integer = 0 To DgFactProv.Columns.Count - 1
 '            dr1(j) = DgFactProv.Rows(i).Cells(j).Value
 '        Next
 '        dset.Tables(0).Rows.Add(dr1)
 '    Next
 '    Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
 '    Dim wBook As Microsoft.Office.Interop.Excel.Workbook
 '    Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
 '    wBook = excel.Workbooks.Add()
 '    wSheet = wBook.ActiveSheet()
 '    wSheet.Name = "Ejemplo"
 '    Dim dt As System.Data.DataTable = dset.Tables(0)
 '    Dim dc As System.Data.DataColumn
 '    Dim dr As System.Data.DataRow
 '    Dim colIndex As Integer = 0
 '    Dim rowIndex As Integer = 0
 '    For Each dc In dt.Columns
 '        colIndex = colIndex + 1
 '        excel.Cells(1, colIndex) = dc.ColumnName
 '    Next
 '    For Each dr In dt.Rows
 '        rowIndex = rowIndex + 1
 '        colIndex = 0
 '        For Each dc In dt.Columns
 '            colIndex = colIndex + 1
 '            excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
 '        Next
 '    Next

 '    'excel.Range("T:T").EntireRow.Delete()
 '    'excel.Range("V:V").EntireRow.Delete()
 '    'excel.Range("X:Y").EntireRow.Delete()

 '    wSheet.Columns.AutoFit()

 '    excel.Visible = True
 'End Sub

 Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
  Try

   Dim xlApp As New Microsoft.Office.Interop.Excel.Application
   Dim openFileDialog1 As New OpenFileDialog()

   openFileDialog1.InitialDirectory = "c:\"
   openFileDialog1.Filter = "txt files (*.xlx)|*.xlx|All files (*.*)|*.*"
   openFileDialog1.FilterIndex = 2
   openFileDialog1.RestoreDirectory = True

   If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
    xlApp.Workbooks.Open(openFileDialog1.FileName)
   End If

   Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
   Dim MyConnection As System.Data.OleDb.OleDbConnection

   MyConnection = New System.Data.OleDb.OleDbConnection(
                  "provider=Microsoft.Jet.OLEDB.4.0; " &
                  "-{}-data source=" & openFileDialog1.FileName & ";" &
                  "Extended Properties=Excel 8.0;")

   MyCommand = New System.Data.OleDb.OleDbDataAdapter(
                "select * from [Sheet1$]", MyConnection)

   Dim DS As System.Data.DataSet
   DS = New System.Data.DataSet()
   MyCommand.Fill(DS)
   MyConnection.Close()

  Catch ex As Exception

  End Try
 End Sub

 Private Sub DgFactProv_CancelRowEdit_1(sender As Object, e As QuestionEventArgs) Handles DgFactProv.CancelRowEdit
  vActvalor = 0
 End Sub

 Private Sub DgFactProv_CurrentCellDirtyStateChanged_1(sender As Object, e As EventArgs) Handles DgFactProv.CurrentCellDirtyStateChanged
  ' evento que detecta cuando se cambio o actualizo el valor de una celda
  'Este codigo sirve para que se pueda identificar el proceso del checkbox dentro del datagridview junto
  'con el evento de DgFactProv_CellContentClick

  If DgFactProv.IsCurrentCellDirty Then
   vActvalor = 1
   DgFactProv.CommitEdit(DataGridViewDataErrorContexts.Commit)
  End If
 End Sub

 Private Sub DgFactProv_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgFactProv.CellEndEdit, DgFactProv.CellContentClick
  'DgFactProv_CellValidating(Me, Nothing)

  If vActvalor = 1 Then

   '' obtener indice de la columna 
   Dim columna As Integer = DgFactProv.CurrentCell.ColumnIndex


   'se obtiene el nombre de la columna
   Dim NombreCol As String = DgFactProv.Columns.Item(columna).Name

   If vActvalor = 1 And (NombreCol = "Recibeguia" Or NombreCol = "Horarec" Or NombreCol = "Fchrec") Then

    If DgFactProv.CurrentRow.Cells("crastreo").Value = "0" Then
     MessageBox.Show("Antes capture el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

     Return
    End If
   End If

   If NombreCol = "Fchrec" And vActvalor = 1 Then
    If DgFactProv.CurrentRow.Cells("Recibeguia").Value = "" Then
     MessageBox.Show("Antes capture el nombre de la persona que recibio el el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

     Return
    End If

    If DgFactProv.CurrentRow.Cells("Horarec").Value = "" Then
     MessageBox.Show("Antes capture la hora en la que se confirmo el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

     Return
    End If
   End If


   If NombreCol = "Horarec" Then

    If DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue <> "" Then

     Dim vcadena As String

     vcadena = Replace(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue, ":", "")

     If Len(vcadena) < 4 Then
      MessageBox.Show("capture los 4 digitos de la hora", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      DgFactProv.CurrentRow.Cells("Horarec").Value = ""

      Return
     End If

     If Val(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue.SubString(0, 2)) > 23 Then
      MessageBox.Show("La hora no puede ser mayor a 23", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

      Return
     End If

     If Val(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue.SubString(2, 2)) > 59 Then
      MessageBox.Show("Los minutos no pueden ser mayor a 59", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

      Return
     End If
    End If
   End If

   If NombreCol = "Zona" And vActvalor = 1 Then
    If DgFactProv.CurrentRow.Cells("Zona").Value < 0 Or DgFactProv.CurrentRow.Cells("Zona").Value > 7 Then
     MessageBox.Show("La zona no puede estar fuera del rango de 1 a 7", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     Return
    End If
   End If

   If NombreCol = "Pdeclarado" And vActvalor = 1 Then
    If DgFactProv.CurrentRow.Cells("Pdeclarado").Value = 0 Then
     MessageBox.Show("Primero capture el peso declarado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     Return
    End If
   End If

   If NombreCol = "crastreo" And vActvalor = 1 Then
    If Len(DgFactProv.CurrentRow.Cells("crastreo").EditedFormattedValue) < 10 And DgFactProv.CurrentRow.Cells("crastreo").EditedFormattedValue <> "0" Then
     MessageBox.Show("El codigo de rastreo no puede ser menor a 10 caracteres", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     Return
    End If
   End If


   If NombreCol = "Fchent" And vActvalor = 1 Then

    Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
    Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").EditedFormattedValue


    If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
     MessageBox.Show("La fecha de entrega no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     Return
    End If
   End If


   If NombreCol = "Fchemb" And vActvalor = 1 Then

    Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
    Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").EditedFormattedValue

    If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
     MessageBox.Show("La fecha de entrega no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     Return

    End If
   End If


   If NombreCol = "Fchrec" And vActvalor = 1 Then

    Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
    Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchrec").EditedFormattedValue

    If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
     MessageBox.Show("El día de confirmación de guía no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     Return

    End If
   End If


   'UsrTPM = "MANAGER"
   '***********************************************************************************************************UsrTPM = "MANAGER"
   Dim vpuntos As Int16 = 0
   Dim i As Integer

   Dim nCadena As String
   'On Error Resume Next
   'Asignamos valor a la cadena de trabajo para
   'no modificar la que envía el cliente.
   nCadena = DgFactProv.CurrentRow.Cells("Horarec").Value
   For i = 1 To Len(nCadena)
    If Mid(nCadena, i, 1) = ":" Then
     vpuntos = 1
    End If
   Next i

   Dim vHora As String = ""

   If DgFactProv.CurrentRow.Cells("Horarec").Value <> "" Then
    If vpuntos = 1 Then
     vHora = DgFactProv.CurrentRow.Cells("Horarec").Value.SubString(0, 2) + ":" + DgFactProv.CurrentRow.Cells("Horarec").Value.SubString(3, 2)
    Else
     vHora = DgFactProv.CurrentRow.Cells("Horarec").Value.SubString(0, 2) + ":" + DgFactProv.CurrentRow.Cells("Horarec").Value.SubString(2, 2)
    End If
    DgFactProv.CurrentRow.Cells("Horarec").Value = vHora
   End If

   'INICIO CALCULO TERRESTRE DE ESTAFETA==========================================================================================
   If DgFactProv.CurrentRow.Cells("articulo").Value = "FL-001" Or DgFactProv.CurrentRow.Cells("articulo").Value = "FL-101" Then
    Dim VCargoComb As Double

    Select Case Val(DgFactProv.CurrentRow.Cells("Zona").Value)
                    'CARGO ADICIONAL COMBUSTIBLE TERRESTRE (Mto. CB Serv.) X ZONA
     Case 0
      VCargoComb = 0
     Case 1
      'AÑO 2018
      'VCargoComb = 17.47
      'AÑO 2019
      VCargoComb = 24.18
     Case 2
      'AÑO 2018
      'VCargoComb = 18.89
      'AÑO 2019
      VCargoComb = 26.15
     Case 3
      'AÑO 2018
      'VCargoComb = 21.44
      'AÑO 2019
      VCargoComb = 29.68
     Case 4
      'AÑO 2018
      'VCargoComb = 22.47
      'AÑO 2019
      VCargoComb = 31.1
     Case 5
      'AÑO 2018
      'VCargoComb = 24.42
      'AÑO 2019
      VCargoComb = 33.8
     Case 6
      'AÑO 2018
      'VCargoComb = 26.26
      'AÑO 2019
      VCargoComb = 36.36
     Case 7
      'AÑO 2018
      'VCargoComb = 27.97
      'AÑO 2019
      VCargoComb = 38.71
    End Select


    Dim VCargoAdCombSP As Double

    Select Case Val(DgFactProv.CurrentRow.Cells("Zona").Value)

                    'CARGO ADICIONAL COMBUSTIBLE TERRESTRE SP (Mto. CBS) X ZONA
     Case 0
      VCargoAdCombSP = 0
     Case 1
      'AÑO 2018
      'VCargoAdCombSP = 0.56
      'AÑO 2019
      VCargoAdCombSP = 0.77
     Case 2
      'AÑO 2018
      'VCargoAdCombSP = 0.7
      'AÑO 2019
      VCargoAdCombSP = 0.97
     Case 3
      'AÑO 2018
      'VCargoAdCombSP = 1.01
      'AÑO 2019
      VCargoAdCombSP = 1.39
     Case 4
      'AÑO 2018
      'VCargoAdCombSP = 1.14
      'AÑO 2019
      VCargoAdCombSP = 1.58
     Case 5
      'AÑO 2018
      'VCargoAdCombSP = 1.28
      'AÑO 2019
      VCargoAdCombSP = 1.77
     Case 6
      'AÑO 2018
      'VCargoAdCombSP = 1.42
      'AÑO 2019
      VCargoAdCombSP = 1.97
     Case 7
      'AÑO 2018
      'VCargoAdCombSP = 1.57
      'AÑO 2019
      VCargoAdCombSP = 2.17
    End Select

    '- 5
    Dim ValorDec As Double
    If VCargoComb <> 0 Then

     If Val(DgFactProv.CurrentRow.Cells("Pdeclarado").Value) > 0 Then
      'SE MODIFICA EL VALOR DE GARANTIA SERVICIO TERRESTRE CONSUMO (Mto. Serv.) Y GARANTIA SERVICIO TERRESTRE SOBREPESO (Monto Sp)
      'ANTERIOR AL 2018
      'ValorDec = 72.1 + (1.53 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1)) + VCargoComb + (VCargoAdCombSP * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1))
      'MODIFICACIÓN 2019: SE COLOCA EN VEZ DEL MENOS 1, ES MENOS 5, ESTO SE COLOCA DEBIDO A QUE ES EL SOBREPESO DECLARADO POR ESTAFETA
      ValorDec = 72.1 + (1.53 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 5)) + VCargoComb + (VCargoAdCombSP * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 5))

      DgFactProv.CurrentRow.Cells("vguiadec").Value = ValorDec.ToString
     End If

     If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) > 0 Then
      'ANTERIOR AL 2018
      'ValorDec = 72.1 + (1.53 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1)) + VCargoComb + (VCargoAdCombSP * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1))
      'MODIFICACIÓN 2019: SE COLOCA EN VEZ DEL MENOS 1, ES MENOS 5, ESTO SE COLOCA DEBIDO A QUE ES EL SOBREPESO DECLARADO POR ESTAFETA
      ValorDec = 72.1 + (1.53 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 5)) + VCargoComb + (VCargoAdCombSP * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 5))

      DgFactProv.CurrentRow.Cells("vguiacob").Value = ValorDec.ToString
     End If

    End If
   End If
   'FIN CALCULO TERRESTRE DE ESTAFETA==========================================================================================

   'FL-002 ----------------------------------------------------------------------------------------------------------------------------------------------
   'INICIO CALCULO DIA SIGUIENTE DE ESTAFETA==========================================================================================

   If DgFactProv.CurrentRow.Cells("articulo").Value = "FL-002" Then
    Dim VCargoComb As Double

    Select Case Val(DgFactProv.CurrentRow.Cells("Zona").Value)
                    'CARGO ADICIONAL COMBUSTIBLE 1 DIA (Mto. CB Serv.)
     Case 0
      VCargoComb = 0
     Case 1

      VCargoComb = 17.43
     Case 2

      VCargoComb = 18.73
     Case 3

      VCargoComb = 21.29
     Case 4

      VCargoComb = 17.94
     Case 5

      VCargoComb = 18.58
     Case 6

      VCargoComb = 19.49
     Case 7

      VCargoComb = 20.37
    End Select


    Dim VCargoAdCombSP As Double

    Select Case Val(DgFactProv.CurrentRow.Cells("Zona").Value)

                    'CARGO ADICIONAL COMBUSTIBLE 1 DIA SP (Mto. CBS)
     Case 0
      VCargoAdCombSP = 0
     Case 1

      VCargoAdCombSP = 2.26
     Case 2

      VCargoAdCombSP = 2.56
     Case 3

      VCargoAdCombSP = 3.42
     Case 4

      VCargoAdCombSP = 5.23
     Case 5

      VCargoAdCombSP = 5.57
     Case 6

      VCargoAdCombSP = 6.02
     Case 7

      VCargoAdCombSP = 6.48
    End Select



    Dim ValorDec As Double
    If VCargoComb <> 0 Then

     If Val(DgFactProv.CurrentRow.Cells("Pdeclarado").Value) > 0 Then
      'GARANTIA 1 DIA CONSUMO (Mto. Serv.) Y GARANTIA 1 DIA SOBREPESO (Monto Sp)
      ValorDec = 62.97 + (6.6 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1)) + VCargoComb + (VCargoAdCombSP * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1))

      DgFactProv.CurrentRow.Cells("vguiadec").Value = ValorDec.ToString
     End If

     If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) > 0 Then

      ValorDec = 62.97 + (6.6 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1)) + VCargoComb + (VCargoAdCombSP * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1))

      DgFactProv.CurrentRow.Cells("vguiacob").Value = ValorDec.ToString
     End If

    End If
   End If

   'FIN CALCULO DIA SIGUIENTE DE ESTAFETA==========================================================================================

   If Val(DgFactProv.CurrentRow.Cells("Pdeclarado").Value) = 0 Then
    DgFactProv.CurrentRow.Cells("vguiadec").Value = 0
   End If

   If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) = 0 Then
    DgFactProv.CurrentRow.Cells("vguiacob").Value = 0
   End If

   If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) = 0 Then
    DgFactProv.CurrentRow.Cells("Difpeso").Value = 0
    DgFactProv.CurrentRow.Cells("Difvalor").Value = 0
   Else
    DgFactProv.CurrentRow.Cells("Difpeso").Value = DgFactProv.CurrentRow.Cells("Pesocob").Value - DgFactProv.CurrentRow.Cells("Pdeclarado").Value
    DgFactProv.CurrentRow.Cells("Difvalor").Value = DgFactProv.CurrentRow.Cells("vguiacob").Value - DgFactProv.CurrentRow.Cells("vguiadec").Value

   End If

   vIni = DgFactProv.CurrentRow.Cells("Fchemb").Value
   vFin = DgFactProv.CurrentRow.Cells("Fchent").Value

   If vIni.Year > 1900 And vFin.Year > 1900 Then

    vCant = 0
    vIni = DgFactProv.CurrentRow.Cells("Fchemb").Value
    vFin = DgFactProv.CurrentRow.Cells("Fchent").Value

    DiasHab()
    DgFactProv.CurrentRow.Cells("Diasent").Value = vCant

   End If


   vpuntos = 0

   ActualizaValores()

   vActvalor = 0
   TotalFacturas()
  End If

 End Sub

 Private Sub DgFactProv_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DgFactProv.CellValidating
  ' obtener indice de la columna 
  Dim columna As Integer = DgFactProv.CurrentCell.ColumnIndex

  'se obtiene el nombre de la columna
  Dim NombreCol As String = DgFactProv.Columns.Item(columna).Name

  If NombreCol = "Zona" And vActvalor = 1 Then
   If DgFactProv.CurrentRow.Cells("Zona").Value < 0 Or DgFactProv.CurrentRow.Cells("Zona").Value > 7 Then
    MessageBox.Show("La zona no puede estar fuera del rango de 1 a 7", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    e.Cancel = True
    Return
   End If
  End If

  If NombreCol = "Pdeclarado" And vActvalor = 1 Then
   If DgFactProv.CurrentRow.Cells("Pdeclarado").Value = 0 Then
    MessageBox.Show("Primero capture el peso declarado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    e.Cancel = True
    Return
   End If
  End If

  If NombreCol = "crastreo" And vActvalor = 1 Then
   If Len(DgFactProv.CurrentRow.Cells("crastreo").EditedFormattedValue) < 10 And DgFactProv.CurrentRow.Cells("crastreo").EditedFormattedValue <> "0" Then
    MessageBox.Show("El codigo de rastreo no puede ser menor a 10 caracteres", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    e.Cancel = True
    Return
   End If
  End If


  If NombreCol = "Fchent" And vActvalor = 1 Then

   Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
   Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").EditedFormattedValue


   If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
    MessageBox.Show("La fecha de entrega no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    e.Cancel = True
    Return
   End If
  End If

  '****************************************************************
  If NombreCol = "Fchemb" And vActvalor = 1 Then

   Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
   Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").EditedFormattedValue

   If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
    MessageBox.Show("La fecha de entrega no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    e.Cancel = True
    Return

   End If
  End If


  If NombreCol = "Fchrec" And vActvalor = 1 Then

   Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
   Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchrec").EditedFormattedValue

   If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
    MessageBox.Show("El día de confirmación de guía no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    e.Cancel = True
    Return

   End If
  End If


  '------------------------------
  '     'Horarec,Recibeguia
  If vActvalor = 1 And (NombreCol = "Recibeguia" Or NombreCol = "Horarec" Or NombreCol = "Fchrec") Then

   If DgFactProv.CurrentRow.Cells("crastreo").Value = "0" Then
    MessageBox.Show("Antes capture el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    e.Cancel = True
    Return
   End If
  End If

  If NombreCol = "Fchrec" And vActvalor = 1 Then
   If DgFactProv.CurrentRow.Cells("Recibeguia").Value = "" Then
    MessageBox.Show("Antes capture el nombre de la persona que recibio el el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    e.Cancel = True
    Return
   End If

   If DgFactProv.CurrentRow.Cells("Horarec").Value = "" Then
    MessageBox.Show("Antes capture la hora en la que se confirmo el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    e.Cancel = True
    Return
   End If
  End If

  'Fchrec


  If NombreCol = "Horarec" Then
   Dim vpuntos As Int16 = 0
   Dim i As Integer
   Dim nCadena As String
   On Error Resume Next
   'Asignamos valor a la cadena de trabajo para
   'no modificar la que envía el cliente.
   nCadena = DgFactProv.CurrentRow.Cells("Horarec").Value
   For i = 1 To Len(nCadena)
    If Mid(nCadena, i, 1) = ":" Then
     vpuntos = 1
    End If
   Next i


   Dim vHora As String = ""


   If DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue <> "" Then

    Dim vcadena As String
    'DgFactProv.CurrentCell = DgFactProv.Item("ColumnName", 5)
    vcadena = Replace(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue, ":", "")

    If Len(vcadena) < 4 Then
     MessageBox.Show("capture los 4 digitos de la hora", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     DgFactProv.CurrentRow.Cells("Horarec").Value = ""
     e.Cancel = True
     Return
    End If

    If Val(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue.SubString(0, 2)) > 23 Then
     MessageBox.Show("La hora no puede ser mayor a 23", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     e.Cancel = True
     Return
    End If

    If Val(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue.SubString(2, 2)) > 59 Then
     MessageBox.Show("Los minutos no pueden ser mayor a 59", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     e.Cancel = True
     Return
    End If
   End If
  End If
 End Sub

 Private Sub DgFactProv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
  Dim VErrorG As Integer = 0
  Dim strValue As String


  If e.ColumnIndex = 0 And vActvalor = 1 Then
   Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").Value
   If VFchEnt.Year = 1900 Then
    MessageBox.Show("Primero capture la fecha de entrega", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 0
    Me.DgFactProv.RefreshEdit()
    Return
   End If

  End If

  'e.RowIndex >= 0
  If e.ColumnIndex = 0 And vActvalor = 1 Then
   Dim row As DataGridViewRow = DgFactProv.Rows(e.RowIndex)
   Try

    If Me.DgFactProv.Columns(e.ColumnIndex).Name = "Entregado" And DgFactProv.Rows(e.RowIndex).Cells("Entregado").EditedFormattedValue = True Then
     'The user clicked on the checkbox column
     strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
     If MessageBox.Show("¿Confirma que este flete fue entregado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

      DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 1
      ActualizaValores()
      Me.DgFactProv.RefreshEdit()

     Else
      DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 0
      Me.DgFactProv.RefreshEdit()

     End If

    Else

     If MessageBox.Show("¿Confirma que este flete" + Chr(13) + "No fue entregado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
      DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 0
      ActualizaValores()
      Me.DgFactProv.RefreshEdit()
     Else
      DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 1
      Me.DgFactProv.RefreshEdit()

     End If

    End If


   Catch ex As Exception
    VErrorG = 1
   End Try

   If VErrorG = 1 Then
    MessageBox.Show("Error al actualizar registros", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Return
   End If
  End If

  '---------------------------------------------------------------------------------------------------------------

  Try
   If DgFactProv.Columns(e.ColumnIndex).Name = "Excluir" And vActvalor = 1 And DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = True Then
    MessageBox.Show("Primero desmarque Cortesia", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 0
    Me.DgFactProv.RefreshEdit()
    Return
   End If
  Catch
  End Try


  If DgFactProv.Columns(e.ColumnIndex).Name = "Excluir" And vActvalor = 1 Then
   Dim row As DataGridViewRow = DgFactProv.Rows(e.RowIndex)
   Try

    If Me.DgFactProv.Columns(e.ColumnIndex).Name = "Excluir" And DgFactProv.Rows(e.RowIndex).Cells("Excluir").EditedFormattedValue = True Then
     'The user clicked on the checkbox column
     strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
     If MessageBox.Show("¿Confirma que este flete se va a Excluir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

      DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 1
      ActualizaValores()
      Me.DgFactProv.RefreshEdit()

     Else
      DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 0
      Me.DgFactProv.RefreshEdit()

     End If

    Else

     If MessageBox.Show("¿Confirma que este flete" + Chr(13) + "No se va a Excluir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
      DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 0
      ActualizaValores()
      Me.DgFactProv.RefreshEdit()
     Else
      DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 1
      Me.DgFactProv.RefreshEdit()

     End If

    End If


   Catch ex As Exception
    VErrorG = 1
   End Try

   If VErrorG = 1 Then
    MessageBox.Show("Error al actualizar registros", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Return
   End If
  End If



  '--------------------------------------------------------------------------------------------------------------------------------------

  Try
   If DgFactProv.Columns(e.ColumnIndex).Name = "Cortesia" And vActvalor = 1 And DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = True Then
    MessageBox.Show("Primero desmarque Excluir", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 0
    Me.DgFactProv.RefreshEdit()
    Return
   End If
  Catch
  End Try


  If DgFactProv.Columns(e.ColumnIndex).Name = "Cortesia" And vActvalor = 1 Then
   Dim row As DataGridViewRow = DgFactProv.Rows(e.RowIndex)
   Try

    If Me.DgFactProv.Columns(e.ColumnIndex).Name = "Cortesia" And DgFactProv.Rows(e.RowIndex).Cells("Cortesia").EditedFormattedValue = True Then
     'The user clicked on the checkbox column
     strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
     If MessageBox.Show("¿Confirma que este flete es Cortesia?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

      DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 1
      ActualizaValores()
      Me.DgFactProv.RefreshEdit()

     Else
      DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 0
      Me.DgFactProv.RefreshEdit()

     End If

    Else

     If MessageBox.Show("¿Confirma que este flete" + Chr(13) + "No es Cortesia?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
      DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 0
      ActualizaValores()
      Me.DgFactProv.RefreshEdit()
     Else
      DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 1
      Me.DgFactProv.RefreshEdit()

     End If

    End If


   Catch ex As Exception
    VErrorG = 1
   End Try

   If VErrorG = 1 Then
    MessageBox.Show("Error al actualizar registros", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Return
   End If
  End If


  vActvalor = 0
  TotalFacturas()
 End Sub

 Private Sub DgFactProv_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DgFactProv.RowPrePaint
  'If Not IsDBNull(DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value) Then

  If DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = True Then

   DgFactProv.Rows(e.RowIndex).Cells("Entregado").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Entregado").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Fchfact").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Fchfact").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Flete").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Flete").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Codclte").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Codclte").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Cliente").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Cliente").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Ciudad").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Ciudad").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Estado").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Estado").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Fletera").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Fletera").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Articulo").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Descripcion").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Descripcion").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Cantidad").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Cantidad").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Fchemb").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Fchemb").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("crastreo").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("crastreo").Style.ForeColor = Color.Black


   DgFactProv.Rows(e.RowIndex).Cells("vguiadec").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("vguiadec").Style.ForeColor = Color.White


   DgFactProv.Rows(e.RowIndex).Cells("Zona").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Zona").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Pesocob").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Pesocob").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("vguiacob").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("vguiacob").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Difpeso").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Difpeso").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Difvalor").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Difvalor").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("LineTotal").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("LineTotal").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Fchent").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Fchent").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Matrecpor").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Matrecpor").Style.ForeColor = Color.Black

   If Not IsDBNull(DgFactProv.Rows(e.RowIndex).Cells("Garantia").Value) Then
    If DgFactProv.Rows(e.RowIndex).Cells("Garantia").Value = "RECLAMAR" Then
     DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.BackColor = Color.Yellow
     DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.ForeColor = Color.Black
    Else
     DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.BackColor = Color.Red
     DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.ForeColor = Color.White
    End If
   Else
    DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.BackColor = Color.Red
    DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.ForeColor = Color.White
   End If

   DgFactProv.Rows(e.RowIndex).Cells("Diasent").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Diasent").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Recibeguia").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Recibeguia").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Horarec").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Horarec").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Fchrec").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Fchrec").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Zona").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Zona").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Pdeclarado").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Pdeclarado").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Telefono").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Telefono").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("Contacto").Style.BackColor = Color.Red
   DgFactProv.Rows(e.RowIndex).Cells("Contacto").Style.ForeColor = Color.White

   DgFactProv.Rows(e.RowIndex).Cells("comentarios").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("comentarios").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Reexpedicion").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Reexpedicion").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("CorrecDatos").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("CorrecDatos").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("EnvIrreg").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("EnvIrreg").Style.ForeColor = Color.Black

  Else

   With Me.DgFactProv
    .DataSource = DvFletes 'DtFactProv 'DtFormatFlete    
    '.DataSource = DtFactProv 'DtFactProv 'DtFormatFlete    
    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    .DefaultCellStyle.BackColor = Color.AliceBlue
   End With

   DgFactProv.Rows(e.RowIndex).Cells("Fchemb").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Fchemb").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("crastreo").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("crastreo").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Pesocob").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Pesocob").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Fchent").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Fchent").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Matrecpor").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Matrecpor").Style.ForeColor = Color.Black

   If Not IsDBNull(DgFactProv.Rows(e.RowIndex).Cells("Garantia").Value) Then
    If DgFactProv.Rows(e.RowIndex).Cells("Garantia").Value = "RECLAMAR" Then
     DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.BackColor = Color.Yellow
     DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.ForeColor = Color.Black
    End If
   End If

   DgFactProv.Rows(e.RowIndex).Cells("Recibeguia").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Recibeguia").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Horarec").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Horarec").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Fchrec").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Fchrec").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Zona").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Zona").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Pdeclarado").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Pdeclarado").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("comentarios").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("comentarios").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("Reexpedicion").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("Reexpedicion").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("CorrecDatos").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("CorrecDatos").Style.ForeColor = Color.Black

   DgFactProv.Rows(e.RowIndex).Cells("EnvIrreg").Style.BackColor = Color.Gainsboro
   DgFactProv.Rows(e.RowIndex).Cells("EnvIrreg").Style.ForeColor = Color.Black



  End If
  'End If

 End Sub

 Private Sub DgFactProv_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DgFactProv.EditingControlShowing
  ' obtener indice de la columna 
  Dim columna As Integer = DgFactProv.CurrentCell.ColumnIndex
  'se obtiene el nombre de la columna
  Dim NombreCol As String = DgFactProv.Columns.Item(columna).Name
  'If NombreCol = "crastreo" Or NombreCol = "Pdeclarado" Or NombreCol = "Pesocob" Or NombreCol = "Horarec" Or NombreCol = "Zona" Or NombreCol = "EnvIrreg" Or NombreCol = "Reexpedicion" Or NombreCol = "CorrecDatos Then" Then

  If NombreCol = "Pdeclarado" Or NombreCol = "Pesocob" Or NombreCol = "Horarec" Or NombreCol = "Zona" Or NombreCol = "EnvIrreg" Or NombreCol = "Reexpedicion" Or NombreCol = "CorrecDatos Then" Then
   ' referencia a la celda 
   Dim validar As TextBox = CType(e.Control, TextBox)
   ' agregar el controlador de eventos para el KeyPress 
   AddHandler validar.KeyPress, AddressOf validar_Keypress
  End If
 End Sub
 Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
  ' obtener indice de la columna 
  Dim columna As Integer = DgFactProv.CurrentCell.ColumnIndex

  'se obtiene el nombre de la columna
  Dim NombreCol As String = DgFactProv.Columns.Item(columna).Name

  If NombreCol = "Pdeclarado" Or NombreCol = "Pesocob" Or NombreCol = "Horarec" Or NombreCol = "Zona" Then
   'If NombreCol = "crastreo" Or NombreCol = "Pdeclarado" Or NombreCol = "Pesocob" Or NombreCol = "Horarec" Or NombreCol = "Zona" Then
   ' Obtener caracter 
   Dim caracter As Char = e.KeyChar
   If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
    'Me.Text = e.KeyChar 
    e.KeyChar = Chr(0)
   End If
  End If



  If NombreCol = "EnvIrreg" Or NombreCol = "Reexpedicion" Or NombreCol = "CorrecDatos" Then
   ' Obtener caracter 
   Dim caracter As Char = e.KeyChar
   If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False And Not UCase(e.KeyChar) Like "[.]" Then
    'Me.Text = e.KeyChar 
    e.KeyChar = Chr(0)
   End If

  End If

 End Sub

 Private Sub Button2_Click(sender As Object, e As EventArgs)



 End Sub

 'Private Sub DgFactProv_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgFactProv.CurrentCellChanged



 '    Try


 '        Dim VCargoComb As Double

 '        Select Case Val(DgFactProv.CurrentRow.Cells("Zona").Value)
 '            Case 0
 '                VCargoComb = 0
 '            Case 1
 '                'VCargoComb = 10.42
 '                VCargoComb = 10.81
 '            Case 2
 '                'VCargoComb = 11.09
 '                VCargoComb = 11.5
 '            Case 3
 '                'VCargoComb = 8.6
 '                VCargoComb = 8.92
 '            Case 4
 '                'VCargoComb = 9.11
 '                VCargoComb = 9.72
 '            Case 5
 '                'VCargoComb = 9.57
 '                VCargoComb = 10.17
 '            Case 6
 '                'VCargoComb = 10.09
 '                VCargoComb = 10.71
 '            Case 7
 '                'VCargoComb = 10.55
 '                VCargoComb = 11.2
 '        End Select





 '        Dim ValorDec As Double
 '        If VCargoComb <> 0 Then

 '            If Val(DgFactProv.CurrentRow.Cells("Pdeclarado").Value) > 0 Then

 '                'valor peso declarado

 '                'ValorDec = 54.45 + (1.33 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1)) + VCargoComb + (0.32 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1))
 '                ValorDec = 57.17 + (2.33 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1)) + VCargoComb + (0.64 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1))


 '                'ValorDec = 63.05 + (1.65 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1))
 '                'ValorDec = 49.5 + 7.47 + (1.37 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1))
 '                'If DgFactProv.CurrentRow.Cells("Pdeclarado").Value = 1 Then
 '                '    ValorDec = 52.79
 '                'Else
 '                '    ValorDec = 52.79 + (1.1 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1))
 '                'End If
 '                DgFactProv.CurrentRow.Cells("vguiadec").Value = ValorDec.ToString
 '            End If

 '            If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) > 0 Then

 '                'ValorDec = 54.45 + (1.33 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1)) + VCargoComb + (0.32 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1))
 '                ValorDec = 57.17 + (2.33 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1)) + VCargoComb + (0.64 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1))

 '                'ValorDec = 63.05 + (1.65 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1))
 '                'ValorDec = 49.5 + 7.47 + (1.37 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1))
 '                'If DgFactProv.CurrentRow.Cells("Pesocob").Value = 1 Then
 '                '    ValorDec = 52.79
 '                'Else
 '                '    ValorDec = 52.79 + (1.1 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1))
 '                'End If
 '                DgFactProv.CurrentRow.Cells("vguiacob").Value = ValorDec.ToString
 '            End If

 '        End If

 '        If Val(DgFactProv.CurrentRow.Cells("Pdeclarado").Value) = 0 Then
 '            DgFactProv.CurrentRow.Cells("vguiadec").Value = 0
 '        End If

 '        If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) = 0 Then
 '            DgFactProv.CurrentRow.Cells("vguiacob").Value = 0
 '        End If


 '        If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) = 0 Then
 '            DgFactProv.CurrentRow.Cells("Difpeso").Value = 0
 '            DgFactProv.CurrentRow.Cells("Difvalor").Value = 0
 '        Else
 '            DgFactProv.CurrentRow.Cells("Difpeso").Value = DgFactProv.CurrentRow.Cells("Pesocob").Value - DgFactProv.CurrentRow.Cells("Pdeclarado").Value
 '            DgFactProv.CurrentRow.Cells("Difvalor").Value = DgFactProv.CurrentRow.Cells("vguiacob").Value - DgFactProv.CurrentRow.Cells("vguiadec").Value

 '        End If

 '        'If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) = 0 Then

 '        'End If

 '    Catch ex As Exception

 '    End Try
 '    ActualizaValores()
 'End Sub

 Private Sub DgFactProv_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgFactProv.CurrentCellChanged

  'If DgFactProv.Item(11, DgFactProv.CurrentCell.RowIndex).Value = "FL-003" Then
  '    DgFactProv.Item(18, DgFactProv.CurrentCell.RowIndex).ReadOnly = True
  '    DgFactProv.Item(19, DgFactProv.CurrentCell.RowIndex).ReadOnly = True
  '    DgFactProv.Item(20, DgFactProv.CurrentCell.RowIndex).ReadOnly = True
  '    DgFactProv.Item(21, DgFactProv.CurrentCell.RowIndex).ReadOnly = True
  'End If
 End Sub

 Private Sub CmbFlete_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFlete.SelectedIndexChanged

 End Sub
End Class