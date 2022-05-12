Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Threading

Public Class frmOrdenesVta

    Private fechaHoy As String = Format(Now, "dd/MM/yyyy")

#Region "Declaracion de Variables"
    'Fecha inicial obtenida del formulario
    Dim fi As String
    'Fecha Final pero obtenida en tipo de dato DATE para su comparacion
    Dim final As Date
    'Fecha Inicial pero obtenida en tipo de dato DATE para su comparacion
    Dim Inicio As DateTime
    'Dataview contenedor de datos del metodo LlenarDetalle
    Dim ResultadoDetalle As DataView
    'Dataview contenedor de datos del metodo LlenarOrdenes
    Dim ResultadoOrden As DataView
    Dim Date_grid As Date
    'Colores creados para uso en el grid DGVoperacionVta
    Dim rojo As Color
    Dim amarillo As Color
    Dim verde As Color
    Dim Anaranjado As Color
    Dim Gris As Color
    Dim Morado As Color
    Dim Azul As Color
    Dim RojoEmpacado As Color
    Dim bandera_edit As Boolean = False
    'Fecha del dia de hoy
    Dim fecha As String
    'Fecha del dia de hoy sin los dias 
    Dim fecha_3 As String
    'Usuario que ah iniciado sesion
    Dim Userid As String
    'Variable para la consulta de sql para mostrar los cancelados o los no cancelados  
    Dim Cancelados As String
    'Departamento del usuario que ah iniciado sesion
    Dim Dtoid As String
    'Nombre del departamento 
    Dim Dtoname As String
    'Dataview contenedor de datos para LlenarAutorizaciones
    Dim DTVAutorizaciones As DataView
    'Fecha de autorizaciones
    Dim fecha_Aut As String
    'Obtniene la sucursal 
    Dim Sucursal As String
    Dim DsOrdenes As DataSet 'DECLARA DATASET PARA PROCEDIMIENTO DE INSERTAR ORDENES
#End Region

#Region "Metodos"
    Private Sub ImprimirOV(Serie As String, Folio As Integer)
        'CAMPOS DEL DATASET 
        Dim ErrOV As Integer = 0

        Try
            Dim DtOVta As New DataTable("OrdVenta")
            Dim dTable As New DataTable
            Dim detTable As New DataTable
            Dim dgvOVC As New DataGridView
            Dim dtvOvC As New DataView
            Dim dsOVC As New DataSet
            Dim dsOVD As New DataSet

            DtOVta.Columns.Add("IdOrdVta", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Serie", Type.GetType("System.String"))
            DtOVta.Columns.Add("FchOVta", Type.GetType("System.DateTime"))
            DtOVta.Columns.Add("UsrOVta", Type.GetType("System.String"))
            DtOVta.Columns.Add("Agente", Type.GetType("System.String"))
            DtOVta.Columns.Add("NomAgente", Type.GetType("System.String"))
            DtOVta.Columns.Add("IdCliente", Type.GetType("System.String"))
            DtOVta.Columns.Add("DesClte", Type.GetType("System.String"))
            DtOVta.Columns.Add("IdTrnsp", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("DesTrnsp", Type.GetType("System.String"))
            DtOVta.Columns.Add("PerCon", Type.GetType("System.String"))
            DtOVta.Columns.Add("Comen", Type.GetType("System.String"))
            DtOVta.Columns.Add("Direccion", Type.GetType("System.String"))
            DtOVta.Columns.Add("Colonia", Type.GetType("System.String"))
            DtOVta.Columns.Add("CP", Type.GetType("System.String"))
            DtOVta.Columns.Add("Ciudad", Type.GetType("System.String"))
            DtOVta.Columns.Add("Estado", Type.GetType("System.String"))
            DtOVta.Columns.Add("Pais", Type.GetType("System.String"))
            DtOVta.Columns.Add("Rfc", Type.GetType("System.String"))
            DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Articulo", Type.GetType("System.String"))
            DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
            DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
            DtOVta.Columns.Add("ListaP", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Precio", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("DescLin", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Totlinea", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocAntIva", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocIva", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocTotal", Type.GetType("System.Decimal"))

            'SE AGREGAN ESTOS DOS CAMBIOS PARA MONEDA Y TIPO DE DATO: URIEL TORALVA 19/05/2018
            DtOVta.Columns.Add("TipoCambio", Type.GetType("System.String"))
            DtOVta.Columns.Add("Moneda", Type.GetType("System.String"))
            DtOVta.Columns.Add("NumLetra", Type.GetType("System.String")) 'COLOCA EL NUMERO CON LETRA

            Dim columnas As DataColumnCollection = DtOVta.Columns

            Dim series As String = ""
            Dim _filaTemp As DataRow
            Dim numOV As Integer
            Dim fechaOV As String
            Dim UsuarioOV As String
            Dim vTotIva As Decimal = 0
            Dim vTotSIva As Decimal = 0
            Dim vTotDoc As Decimal = 0
            Dim vSinValor As Integer = 0
            Dim Fila As Integer = 0

            Dim idAgte, Agente As String
            Dim idCte, Cliente As String
            Dim idTransporte, formaEnvio As String
            Dim Contacto, Comentario As String
            Dim Direccion, Colonia, CP, Ciudad, Estado, Pais, Rfc As String

            Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
                'Consulta para traer la OV
                Dim ConsutaLista As String
                ConsutaLista = "SELECT *"
                ConsutaLista &= " FROM OrdVta"
                ConsutaLista &= " WHERE Serie = '" & Serie & "' AND IdOrdVta = " & Folio

                Dim daOVC As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)
                daOVC.Fill(dsOVC, "OCabecera")
                daOVC.Fill(dTable)

                For Each row As DataRow In dTable.Rows
                    vTotIva = row("DocIva")
                    vTotSIva = row("SubTot")
                    vTotDoc = row("DocTotal")
                    numOV = row("IdOrdVta")
                    vSerie = row("Serie")
                    UsuarioOV = row("UsrOVta")
                    fechaOV = row("FchOVta")
                    idAgte = row("Agente")
                    Agente = row("NomAgente")
                    idCte = row("IdCliente")
                    Cliente = row("DesClte")
                    idTransporte = row("IdTrnsp")
                    formaEnvio = row("DesTrnsp")
                    Contacto = row("PerCon")
                    Comentario = row("Coment")
                    Direccion = row("Direccion")
                    Colonia = row("Colonia")
                    CP = row("CP")
                    Ciudad = row("Ciudad")
                    Estado = row("Estado")
                    Pais = row("Pais")
                    Rfc = row("Rfc")
                Next
            End Using
            'CONVIERTE EL IMPORTE DE NUMERO EN LETRA
            Dim NumLetra As String = Numalet.ToCardinal(vTotDoc.ToString)

            Using SqlConnectionDet As New Data.SqlClient.SqlConnection(StrTpm)
                'Consulta para traer la OV
                Dim ConsutaListaDet As String
                ConsutaListaDet = "SELECT *"
                ConsutaListaDet &= " FROM RdVta1"
                ConsutaListaDet &= " WHERE Serie = '" & Serie & "' AND IdOrdVta = " & Folio

                Dim daOVD As New SqlClient.SqlDataAdapter(ConsutaListaDet, SqlConnectionDet)
                daOVD.Fill(dsOVD, "ODetalle")
                daOVD.Fill(detTable)

                Dim contador As Integer = 0

                For Each row As DataRow In detTable.Rows
                    contador += 1

                    _filaTemp = DtOVta.NewRow()
                    _filaTemp(columnas(0)) = numOV.ToString
                    _filaTemp(columnas(1)) = "-  " & vSerie & numOV.ToString
                    _filaTemp(columnas(2)) = fechaOV
                    _filaTemp(columnas(3)) = UsrTPM
                    _filaTemp(columnas(4)) = idAgte
                    _filaTemp(columnas(5)) = Agente
                    _filaTemp(columnas(6)) = idCte
                    _filaTemp(columnas(7)) = Cliente
                    _filaTemp(columnas(8)) = idTransporte
                    _filaTemp(columnas(9)) = formaEnvio
                    _filaTemp(columnas(10)) = Contacto
                    _filaTemp(columnas(11)) = Comentario
                    _filaTemp(columnas(12)) = Direccion
                    _filaTemp(columnas(13)) = Colonia
                    _filaTemp(columnas(14)) = CP
                    _filaTemp(columnas(15)) = Ciudad & ", " & Estado & ", " & Pais & ", " & CP
                    _filaTemp(columnas(16)) = Estado
                    _filaTemp(columnas(17)) = Pais
                    _filaTemp(columnas(18)) = Rfc
                    _filaTemp(columnas(19)) = contador
                    _filaTemp(columnas(20)) = row("Articulo")    'ARTICULO
                    _filaTemp(columnas(21)) = row("Linea")    'LINEA
                    _filaTemp(columnas(22)) = row("DesArt")    'DESCRPCION ARTICULO
                    _filaTemp(columnas(23)) = row("ListaP")   'Lista de precios capturada por el agente
                    _filaTemp(columnas(24)) = row("Precio")    'PRECIO
                    _filaTemp(columnas(25)) = row("Cantidad")    'CANTIDAD DE PIEZAS SOLICITADAS
                    _filaTemp(columnas(26)) = row("DescLin")    'DESCUENTO
                    _filaTemp(columnas(27)) = row("Totlinea")    'IMPORTE
                    _filaTemp(columnas(28)) = vTotSIva.ToString
                    _filaTemp(columnas(29)) = vTotIva.ToString
                    _filaTemp(columnas(30)) = vTotDoc.ToString

                    'SE AGREGAN CAMPOS AL REPORTE CON EL DATASET: URIEL TORALVA 19/05/2018
                    '_filaTemp(columnas(31)) = txttipo_cambio.Text.ToString     'TIPO DEL CAMBIO AL DIA
                    _filaTemp(columnas(32)) = "MXP"               'MONEDA USD O MXP
                    _filaTemp(columnas(33)) = NumLetra               'COLOCA EL IMPORTE EN LETRA

                    DtOVta.Rows.Add(_filaTemp)

                Next
            End Using


            Dim informe As New rpteOV

            'RepComsultaP.MdiParent = frmOrdenesVta
            informe.SetDataSource(DtOVta)
            RepComsultaP.CrVConsulta.ReportSource = informe

            RepComsultaP.Show()

            'una vez impresa grabo la fecha y hora de su revision y cierro panel de orden de venta
            If (ActualizaInf(Serie, Folio)) Then
                dgvNuevaOV.DataSource = Nothing
                panelOV.Visible = False
                btnNuevaOrdenVenta.Visible = False
                Timer3.Enabled = False
                BuscarNuevasOV()
            End If

        Catch
            ErrOV = 1
            MessageBox.Show("No fue posible mostrar la orden de venta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function ActualizaInf(Serie As String, Folio As Integer) As Boolean
        Try
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            command.Connection = SqlConnection

            Dim strcadena As String = "UPDATE OrdVta SET FechaVistaTelemarketing = GETDATE() WHERE Serie = '" & Serie & "' AND IdOrdVta = " & Folio & " AND FechaVistaTelemarketing IS NULL"
            command.CommandText = strcadena
            command.ExecuteNonQuery()

            Return True
            'MessageBox.Show("Datos Guardados Correctamente",
            '                     "Aviso.", MessageBoxButtons.OK,
            '                     MessageBoxIcon.Information)
        Catch ex As Exception
            Return False
            'MessageBox.Show("Ocurrio un Error: " & ex.Message,
            '                     "ERROR.", MessageBoxButtons.OK,
            '                     MessageBoxIcon.Error)
        End Try
    End Function


    Sub LlenarDgvOperacion(USUID As String)
        'Este metodo llena el gridOperacionVta  con un procedimiento almacenado
        Try
            'VARIABLES DE CONEXION DE LLENADO
            Dim cmd As SqlCommand
            Dim cnn As SqlConnection = Nothing
            Dim da As SqlDataAdapter
            Dim DsOrdenes = New DataSet
            'Obtiene la fecha de hoy
            fecha = dtpFecha.Value.ToString("yyyy-MM-dd")
            'obtiene la fecha de este mes y este año
            fecha_3 = dtpFecha.Value.ToString("yyyy-MM")
            'Agregamos el dia manualmente para  restarle los 3 meses que es le numero maximo en que
            'Podemos tener una Back Order 
            fecha_3 &= "-01"
            cnn = New SqlConnection(StrTpm)
            'ALMACENA LA CONSULTA EN UN COMMAND SQL SP_Operacion_Vtas
            'cmd = New SqlCommand("SP_Operacion_Vtas2", cnn)
            cmd = New SqlCommand("SP_Operacion_Vtas", cnn)
            'Pasa los parametros del procedimiento almacenado 
            cmd.CommandType = CommandType.StoredProcedure
            'Es la fecha de hoy 
            cmd.Parameters.AddWithValue("@fecha", fecha) ' 220-03-30
            'Es la fecha ala que se le restaran los 3 meses por el back order
            cmd.Parameters.AddWithValue("@fecha_3", fecha_3) ' 220-03-01
            'Es el id del usuario obtenido al iniciar sesion en TPD
            cmd.Parameters.AddWithValue("@Usuario", USUID) '"
            'Mostrara las ordenes canceladas o las que estan vivas 
            If CBMosctrarCan.Checked = True Then
                Cancelados = "Y"
            Else
                Cancelados = "N"
            End If
            'Enviamos el parametro de cancelacion para saber que datos mostraremos 
            cmd.Parameters.AddWithValue("@Canceled", Cancelados)
            'APERTURA LA CONEXION
            cnn.Open()
            'INSTANCIA UN ADAPTER
            da = New SqlDataAdapter
            'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
            da.SelectCommand = cmd
            'LO EJECUTA CON LA CONEXION
            da.SelectCommand.Connection = cnn
            'TIEMPO DE ESPERA DE LA CONEXION
            da.SelectCommand.CommandTimeout = 10000
            'EJECUTA LA CONSULTA
            cmd.ExecuteNonQuery()
            'CIERRA EL COMMAND DE SQL
            cmd.Connection.Close()
            'CIERRA LA CONEXION
            cnn.Close()
            'LLENA EL ADAPTER A UN DATA SET
            da.Fill(DsOrdenes)
            'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
            ResultadoOrden = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA
            ResultadoOrden.Table = DsOrdenes.Tables(0)
            'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
            dgvOperacionOvTA.DataSource = ResultadoOrden
            'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
            EstiloDgvOperacion()
        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
    End Sub

    Sub BuscarNuevasOV()
        'Este metodo llena el gridOperacionVta  con un procedimiento almacenado
        Try
            'VARIABLES DE CONEXION DE LLENADO
            Dim cmd As SqlCommand
            Dim cnn As SqlConnection = Nothing
            Dim daOV As SqlDataAdapter
            Dim DsOrdenesVenta = New DataSet
            cnn = New SqlConnection(StrTpm)
            'ALMACENA LA CONSULTA EN UN COMMAND
            cmd = New SqlCommand("TPD_GETOrdenesVenta", cnn)
            'Pasa los parametros del procedimiento almacenado 
            cmd.CommandType = CommandType.StoredProcedure
            'Id del telemarketing actual
            cmd.Parameters.AddWithValue("@AgenteTelemarketing", UsrTPM)
            cnn.Open()
            'INSTANCIA UN ADAPTER
            daOV = New SqlDataAdapter
            'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
            daOV.SelectCommand = cmd
            'LO EJECUTA CON LA CONEXION
            daOV.SelectCommand.Connection = cnn
            'TIEMPO DE ESPERA DE LA CONEXION
            daOV.SelectCommand.CommandTimeout = 10000
            'EJECUTA LA CONSULTA
            cmd.ExecuteNonQuery()
            'CIERRA EL COMMAND DE SQL
            cmd.Connection.Close()
            'CIERRA LA CONEXION
            cnn.Close()
            'LLENA EL ADAPTER A UN DATA SET
            daOV.Fill(DsOrdenesVenta, "OVs")

            'If (DsOrdenesVenta.Tables.Count > 0) Then
            If (DsOrdenesVenta.Tables("OVs").Rows.Count > 0) Then
                btnNuevaOrdenVenta.Visible = True
                Timer3.Enabled = True
            Else
                btnNuevaOrdenVenta.Visible = False
                Timer3.Enabled = False
            End If

            'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
            ResultadoOrden = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA
            ResultadoOrden.Table = DsOrdenesVenta.Tables(0)
            'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
            dgvNuevaOV.DataSource = ResultadoOrden
            'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES DE VENTA
            EstiloDgvOrdenesVenta()

            txtSerie.Text = ""
            txtFolio.Text = ""

        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
    End Sub

    Sub ObtenerUsuario()
        'Dependiendo del Usuario que ingrese al TPD es lo que se le mostrara en la pantalla OperacionVta
        Dim ent As String
        Try
            'Si el usuuario tiene privilegios vera todos los datos de ventas 
            If UsrTPM = "MANAGER" Or UsrTPM = "COMPRAS1" Or UsrTPM = "RHUMANOS" Then
                'En caso contrario al usuario solo se le mostraran los productos de venta cada usuario mas no todos
            Else
                Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
                    Dim da As New SqlClient.SqlDataAdapter("select USERID from Accesos where Id_Usuario='" + UsrTPM + "'", SqlConnection)
                    Dim ds As New DataSet
                    da.Fill(ds)
                    'En caso de que no encuetre al Usuario nada esta validacion ya esta hecha en el tpd al iniciar secion por lo cual nunca llegara a este punto  
                    If ds.Tables(0).Rows(0).Item(0).ToString = "" Then

                    End If
                    'Caso contrario se le agregara una cadena ala consulta donde unicamente buscara las ventas del usuario que ingreso en caso de que no sea ADM
                    ent = ds.Tables(0).Rows(0).Item(0).ToString
                    Userid = ent.ToString
                End Using
            End If
        Catch ex As Exception
            'MsgBox("Error: Usuario no valido intente con Usuario de SAP")
        End Try
    End Sub
    Sub ObtenerDto()
        'Dependiendo del Usuario Y AL DEPARTAMENTO  que ingrese al TPD es lo que se le mostrara en la pantalla OperacionVta
        Dim ent As String
        Try
            'Si el usuuario tiene privilegios vera todos los datos de ventas y cobranza
            If UsrTPM = "MANAGER" Or UsrTPM = "COMPRAS1" Or UsrTPM = "AINVEN" Or UsrTPM = "RHUMANOS" Then
                Dtoid = " and (T0.ProcesStat<>'C' AND T0.ProcesStat<>'P') "
                'En caso contrario al usuario solo se le mostraran los productos de venta cada usuario mas no todos
            ElseIf UsrTPM = "COBRANZ2" Then
                Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
                    'Se obtiene el USERID que es el identificador del usuario en sap, el departamento y la sucursal de donde es el agente
                    Dim da As New SqlClient.SqlDataAdapter("select USERID,Id_Dept,Sucursal from Accesos inner join Usuarios On Accesos.Id_Usuario=Usuarios.Id_Usuario where Accesos.Nombre='" + CBBuscarUsuario.SelectedItem + "'", SqlConnection)
                    Dim ds As New DataSet
                    da.Fill(ds)
                    'Si el agente es de cobranza solo vera las autorizaciones que autorizo o que puede autorizar              
                    ent = ds.Tables(0).Rows(0).Item(0).ToString
                    Dtoid = "and (T0.Status='W') and T0.ProcesStat<>'C' and T5.UserID=" + ent.ToString + " ORDER BY FechaCreacion ASC "
                    'Obtenemos el nombre del departamento del data source
                    Dtoname = ds.Tables(0).Rows(0).Item(1).ToString
                    'Obtenemos el nombre de la sucursal del data source
                    Sucursal = ds.Tables(0).Rows(0).Item(2).ToString
                    'Si el usuario es de ventas vera las que mando ha autorizar    
                End Using


            Else
                Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
                    'Se obtiene el USERID que es el identificador del usuario en sap, el departamento y la sucursal de donde es el agente
                    Dim da As New SqlClient.SqlDataAdapter("select USERID,Id_Dept,Sucursal from Accesos inner join Usuarios On Accesos.Id_Usuario=Usuarios.Id_Usuario where Accesos.Id_Usuario='" + UsrTPM + "'", SqlConnection)
                    Dim ds As New DataSet
                    da.Fill(ds)
                    'Si el agente es de cobranza solo vera las autorizaciones que autorizo o que puede autorizar
                    If ds.Tables(0).Rows(0).Item(1).ToString = "SIST" Then
                        ent = ds.Tables(0).Rows(0).Item(0).ToString
                        Dtoid = "and (T0.Status='W') and T0.ProcesStat<>'C' and T5.UserID=" + ent.ToString + " ORDER BY FechaCreacion ASC "
                        'Obtenemos el nombre del departamento del data source
                        Dtoname = ds.Tables(0).Rows(0).Item(1).ToString
                        'Obtenemos el nombre de la sucursal del data source
                        Sucursal = ds.Tables(0).Rows(0).Item(2).ToString
                        'Si el usuario es de ventas vera las que mando ha autorizar     
                    Else
                        ent = ds.Tables(0).Rows(0).Item(0).ToString
                        Dtoid = "and (T0.ProcesStat<>'C' AND T0.ProcesStat<>'P') and T1.UserID=" + ent.ToString + "ORDER BY FechaCreacion ASC"
                        'Obtenemos el nombre del departamento del data source
                        Dtoname = ds.Tables(0).Rows(0).Item(1).ToString
                        'Obtenemos el nombre de la sucursal del data source
                        Sucursal = ds.Tables(0).Rows(0).Item(2).ToString
                    End If
                End Using
            End If
        Catch ex As Exception
            MsgBox("Error: Usuario no valido intente con Usuario de SAP")
        End Try
    End Sub
    Sub LlenarConCB(Estatus As String)
        'Dependiendo del Usuario Y AL DEPARTAMENTO  que ingrese al TPD es lo que se le mostrara en la pantalla OperacionVta

        'ent= USERID  que es el identificado del usuario en sap
        Dim ent As String

        Try
            'Validamos el estatus del combobox y validamos si el usuario es manager 
            If UsrTPM = "MANAGER" Or UsrTPM = "COMPRAS1" Or UsrTPM = "RHUMANOS" Then

                If Estatus = "Pendiente" Then
                    Estatus = "'W'"
                End If
                If Estatus = "Rechazado" Then
                    Estatus = "'N'"
                End If
                If Estatus = "Cancelado" Then
                    Estatus = "'C'"
                End If
                If Estatus = "Creado" Then
                    Estatus = "'P'"
                End If
                If Estatus = "Autorizando" Then
                    Estatus = "'A'"
                End If
                If Estatus = "Autorizado" Then
                    Estatus = "'Y'"
                End If

                Dtoid = " and T0.ProcesStat=" + Estatus + " "
                'En caso contrario al usuario solo se le mostraran los productos de venta cada usuario mas no todos

            ElseIf UsrTPM = "COBRANZ2" Then
                If Estatus = "Pendiente" Then
                    Estatus = "'W'"
                End If
                If Estatus = "Rechazado" Then
                    Estatus = "'N'"
                End If
                If Estatus = "Cancelado" Then
                    Estatus = "'C'"
                End If
                If Estatus = "Creado" Then
                    Estatus = "'P'"
                End If
                If Estatus = "Autorizando" Then
                    Estatus = "'A'"
                End If
                If Estatus = "Autorizado" Then
                    Estatus = "'Y'"
                End If
                'Hacemos una consulta que nos traera los datos que pedimos del combobox dependiendo del usuario que sea
                Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
                    Dim da As New SqlClient.SqlDataAdapter("select USERID,Id_Dept from Accesos inner join Usuarios On Accesos.Id_Usuario=Usuarios.Id_Usuario where Accesos.Nombre='" + CBBuscarUsuario.SelectedItem + "'", SqlConnection)
                    Dim ds As New DataSet
                    da.Fill(ds)
                    'Si el usuario es de cobranza 
                    If ds.Tables(0).Rows(0).Item(1).ToString = "SIST" Then
                        'obtenemos el identificador del usuario en sap
                        ent = ds.Tables(0).Rows(0).Item(0).ToString
                        'Esta es la cadena que nos mostrara los datos del combobox dependiendo lo seleccionado y el susario que ingreso 
                        Dtoid = "and T0.ProcesStat=" + Estatus + " and T5.UserID=" + ent.ToString + " order by Decision"
                        'Aqui obtenemos el id del departamento del usuario
                        Dtoname = ds.Tables(0).Rows(0).Item(1).ToString
                        'si el usuario es de ventas
                    Else
                        'obtenemos el identificador del usuario en sap
                        ent = ds.Tables(0).Rows(0).Item(0).ToString
                        'Esta es la cadena que nos mostrara los datos del combobox dependiendo lo seleccionado y el susario que ingreso 
                        Dtoid = "and T0.ProcesStat=" + Estatus + " and T1.UserID=" + ent.ToString + " order by Decision"
                        'Aqui obtenemos el id del departamento del usuario
                        Dtoname = ds.Tables(0).Rows(0).Item(1).ToString
                    End If
                End Using


            Else
                If Estatus = "Pendiente" Then
                    Estatus = "'W'"
                End If
                If Estatus = "Rechazado" Then
                    Estatus = "'N'"
                End If
                If Estatus = "Cancelado" Then
                    Estatus = "'C'"
                End If
                If Estatus = "Creado" Then
                    Estatus = "'P'"
                End If
                If Estatus = "Autorizando" Then
                    Estatus = "'A'"
                End If
                If Estatus = "Autorizado" Then
                    Estatus = "'Y'"
                End If
                'Hacemos una consulta que nos traera los datos que pedimos del combobox dependiendo del usuario que sea
                Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
                    Dim da As New SqlClient.SqlDataAdapter("select USERID,Id_Dept from Accesos inner join Usuarios On Accesos.Id_Usuario=Usuarios.Id_Usuario where Accesos.Id_Usuario='" + UsrTPM + "'", SqlConnection)
                    Dim ds As New DataSet
                    da.Fill(ds)
                    'Si el usuario es de cobranza 
                    If ds.Tables(0).Rows(0).Item(1).ToString = "SIST" Then
                        'obtenemos el identificador del usuario en sap
                        ent = ds.Tables(0).Rows(0).Item(0).ToString
                        'Esta es la cadena que nos mostrara los datos del combobox dependiendo lo seleccionado y el susario que ingreso 
                        Dtoid = "and T0.ProcesStat=" + Estatus + " and T5.UserID=" + ent.ToString + " order by Decision"
                        'Aqui obtenemos el id del departamento del usuario
                        Dtoname = ds.Tables(0).Rows(0).Item(1).ToString
                        'si el usuario es de ventas
                    Else
                        'obtenemos el identificador del usuario en sap
                        ent = ds.Tables(0).Rows(0).Item(0).ToString
                        'Esta es la cadena que nos mostrara los datos del combobox dependiendo lo seleccionado y el susario que ingreso 
                        Dtoid = "and T0.ProcesStat=" + Estatus + " and T1.UserID=" + ent.ToString + " order by Decision"
                        'Aqui obtenemos el id del departamento del usuario
                        Dtoname = ds.Tables(0).Rows(0).Item(1).ToString
                    End If
                End Using
            End If
            ' mandamos a llamar al metodo llenarautorizaciones que nos llenarael grid view con los datos seleccionados del combobox
            ' y con el usuario que ingreso 
            LlenarAutorizaciones()
        Catch ex As Exception
            MsgBox("Error: Usuario no valido intente con Usuario de SAP")
        End Try
    End Sub

    Sub LlenarDetalleOperacionVta(Articulo As String)
        'llena a travez de una consulta de sql el gridview detalle
        Try
            'VARIABLE DE CADENA DE SQL
            Dim SQLOrdenes As String
            'VARIABLES DE CONEXION DE LLENADO
            Dim cmd As SqlCommand
            Dim cnn As SqlConnection = Nothing
            Dim da As SqlDataAdapter
            Dim DsOrdenes = New DataSet
            'ALAMACENA LA CONSULTA
            'SQLOrdenes = "SELECT ROW_NUMBER() OVER(ORDER BY T1.LineNum ASC) AS 'Partidas', T1.ItemCode AS Articulo, T1.Dscription AS Descripcion, T1.Quantity AS Cantidad, "
            'SQLOrdenes &= "CASE WHEN T4.Status = 'A' THEN NULL WHEN T4.Status = 'S' THEN NULL WHEN T4.Status = 'ST' THEN IIF(T5.Surtido is null,0,T5.Surtido) ELSE NULL END AS Surtido, "
            'SQLOrdenes &= "CAST(t2.SWeight1*Surtido AS DECIMAL(10,2)) as Peso "
            'SQLOrdenes &= "FROM  ORDR T0 INNER JOIN  RDR1 T1 ON T0.DocEntry = T1.DocEntry "
            'SQLOrdenes &= "LEFT JOIN   OITM T2 ON T2.ItemCode = T1.ItemCode "
            'SQLOrdenes &= "LEFT JOIN   OITB T3 ON T3.ItmsGrpCod = T2.ItmsGrpCod "
            'SQLOrdenes &= "LEFT JOIN TPM.dbo.Operacion_Orden T4 ON T4.DocEntry = T0.DocEntry "
            'SQLOrdenes &= "LEFT JOIN TPM.dbo.Operacion_Detalle T5 ON T5.DocEntry = T0.DocEntry AND T5.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS = T1.ItemCode "
            'SQLOrdenes &= "WHERE T1.DocEntry ='" + Articulo + "' and T2.ItmsGrpCod <> 150 "
            'SQLOrdenes &= "GROUP BY T1.LineNum, T1.ItemCode, T1.Dscription, T1.Quantity, Surtido, T4.Status,T4.Status,t2.SWeight1 "
            SQLOrdenes = "SELECT ROW_NUMBER() OVER(ORDER BY T1.LineNum ASC) AS 'Partidas', T1.ItemCode AS Articulo, T1.Dscription AS Descripcion, T1.Quantity AS Cantidad, "
            SQLOrdenes &= "CASE WHEN T4.Status = 'A' THEN NULL WHEN T4.Status = 'S' THEN NULL else  IIF(T5.Surtido is null,0,T5.Surtido)  END AS Surtido,  "
            SQLOrdenes &= "CAST(t2.SWeight1*Surtido AS DECIMAL(10,2)) as Peso "
            SQLOrdenes &= "FROM  SBO_TPD.DBO.ORDR T0 INNER JOIN  SBO_TPD.DBO.RDR1 T1 ON T0.DocEntry = T1.DocEntry "
            SQLOrdenes &= "LEFT JOIN   SBO_TPD.DBO.OITM T2 ON T2.ItemCode = T1.ItemCode "
            SQLOrdenes &= "LEFT JOIN   SBO_TPD.DBO.OITB T3 ON T3.ItmsGrpCod = T2.ItmsGrpCod "
            SQLOrdenes &= "LEFT JOIN TPM.dbo.Operacion_Orden T4 ON T4.DocEntry = T0.DocEntry "
            SQLOrdenes &= "LEFT JOIN TPM.dbo.Operacion_Detalle T5 ON T5.DocEntry = T0.DocEntry AND T5.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS = T1.ItemCode "
            SQLOrdenes &= "WHERE T1.DocEntry ='" + Articulo + "' and T2.ItmsGrpCod <> 150 "
            SQLOrdenes &= "GROUP BY T1.LineNum, T1.ItemCode, T1.Dscription, T1.Quantity, Surtido, T4.Status,T4.Status,t2.SWeight1 "
            cnn = New SqlConnection(StrCon)
            'ALMACENA LA CONSULTA EN UN COMMAND SQL
            cmd = New SqlCommand(SQLOrdenes, cnn)
            'CONVIERTE EL TEXTO EN CONSULTA
            cmd.CommandType = CommandType.Text
            'APERTURA LA CONEXION
            cnn.Open()
            'INSTANCIA UN ADAPTER
            da = New SqlDataAdapter
            'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
            da.SelectCommand = cmd
            'LO EJECUTA CON LA CONEXION
            da.SelectCommand.Connection = cnn
            'TIEMPO DE ESPERA DE LA CONEXION
            da.SelectCommand.CommandTimeout = 10000
            'EJECUTA LA CONSULTA
            cmd.ExecuteNonQuery()
            'CIERRA EL COMMAND DE SQL
            cmd.Connection.Close()
            'CIERRA LA CONEXION
            cnn.Close()
            'LLENA EL ADAPTER A UN DATA SET
            da.Fill(DsOrdenes)
            'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
            ResultadoDetalle = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA
            ResultadoDetalle.Table = DsOrdenes.Tables(0)
            'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
            dgvOperacionDetalle.DataSource = Nothing
            'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
            dgvOperacionDetalle.DataSource = ResultadoDetalle
            'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE DETALLE ORDENES
            EstiloDetalleOperacionVta()
        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
    End Sub

    Sub LlenarAutorizaciones()
        'llena a travez de una consulta de sql el gridview detalle
        Try
            'Obtenemos la fecha del dia actual 
            fecha_Aut = dtpAutorizacion.Value.ToString("yyyy-MM-dd")
            'VARIABLE DE CADENA DE SQL
            Dim SQLOrdenes_aut As String
            'VARIABLES DE CONEXION DE LLENADO
            Dim cmd1 As SqlCommand
            Dim cnn1 As SqlConnection = Nothing
            Dim da1 As SqlDataAdapter
            Dim DsOrdenes_aut = New DataSet
            'ALAMACENA LA CONSULTA
            'SQLOrdenes_aut = "SELECT DISTINCT (T0.DocEntry) as OrdenDeVenta, FORMAT( T5.CreateDate, 'yyyy-MM-dd') AS FechaCreacion, "
            'SQLOrdenes_aut &= "CASE WHEN Len(T5.CreateTime)=4 THEN Stuff(T5.CreateTime,3,0,':') "
            'SQLOrdenes_aut &= "WHEN LEN(T5.CreateTime) =3 THEN Stuff(T5.CreateTime,2,0,':')  "
            'SQLOrdenes_aut &= "END AS horaCreacion, "
            'SQLOrdenes_aut &= "T2.CardCode As CodCliente, T2.CardName As NombreCliente, CONVERT(varchar(50), CONVERT(MONEY, T2.DocTotal), 1) As TotalBorrador,T6.Balance AS SaldoCliente, T6.CreditLine AS LimiteCredito,  "
            'SQLOrdenes_aut &= "Case WHEN T0.Status = 'Y' THEN 'Autorizado' WHEN T0.Status = 'W' THEN 'Pendiente' WHEN T0.Status = 'N' THEN 'Rechazado' End As Decision, "
            'SQLOrdenes_aut &= "Case WHEN T0.ProcesStat = 'Y' THEN 'Autorizado' WHEN T0.ProcesStat = 'W' THEN 'Pendiente' WHEN T0.ProcesStat = 'N' THEN 'Rechazado' "
            'SQLOrdenes_aut &= "WHEN T0.ProcesStat = 'C' THEN 'Cancelado' WHEN T0.ProcesStat = 'P' THEN 'Creado' WHEN T0.ProcesStat = 'A' THEN 'Autorizando' End As Estatus, T7.Remarks AS tipoAP , "
            'SQLOrdenes_aut &= "T1.U_NAME AS SolicitadoPor, (SELECT FORMAT(UpdateDate, 'yyyy-MM-dd') FROM WDD1 WHERE WddCode = T0.WddCode AND (Status = 'Y' OR Status = 'N')) AS FechaDecision, "

            'SQLOrdenes_aut &= " CASE WHEN CONVERT(varchar(50),LEN((SELECT UpdateTime FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N'))))  = '3'  THEN "
            'SQLOrdenes_aut &= "STUFF((SELECT UpdateTime FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')),2,0,':') "
            'SQLOrdenes_aut &= " WHEN CONVERT(varchar(50),LEN((SELECT UpdateTime FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N'))))  = '4'  THEN "
            'SQLOrdenes_aut &= "STUFF((SELECT UpdateTime FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')),3,0,':') "
            'SQLOrdenes_aut &= "END AS HoraDecision, "

            'SQLOrdenes_aut &= "(SELECT Remarks FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')) AS ComentariosCobranza , "
            'SQLOrdenes_aut &= "t0.Remarks AS ComentarioVentas, "
            'SQLOrdenes_aut &= "Stuff((Select  ', '  + V.U_NAME  FROM WDD1 T INNER JOIN OUSR V ON T.UserID = V.USERID WHERE WddCode =T0.WddCode FOR XML PATH('')),1, 2,'') AS Autorizadores, "
            'SQLOrdenes_aut &= "(SELECT V.U_NAME FROM WDD1 T INNER JOIN OUSR V ON T.UserID = V.USERID WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')) AS Autorizo, "
            'SQLOrdenes_aut &= " t2.JrnlMemo as TipoDocumento "
            'SQLOrdenes_aut &= "From OWDD T0 INNER Join OUSR T1 ON T0.OwnerID = T1.USERID  INNER Join ODRF T2 ON T2.DocEntry = T0.DraftEntry "
            'SQLOrdenes_aut &= "INNER Join DRF1 T3 ON T2.DocEntry = T3.DocEntry  INNER Join OWHS T4 ON T3.WhsCode = T4.WhsCode "
            'SQLOrdenes_aut &= "INNER JOIN WDD1 T5 ON T0.ProcessID=T5.WddCode  INNER JOIN OCRD T6 ON T2.CardCode=T6.CardCode "
            'SQLOrdenes_aut &= " INNER JOIN OWTM T7 ON T0.WtmCode=T7.WtmCode "
            'SQLOrdenes_aut &= "WHERE T2.DocDate BETWEEN '2019-05-26' AND '" + fecha_Aut + "'  " + Dtoid + " "
            SQLOrdenes_aut = "SELECT DISTINCT (T0.DocEntry) as OrdenDeVenta, FORMAT( T5.CreateDate, 'yyyy-MM-dd') AS FechaCreacion, "
            SQLOrdenes_aut &= "CASE WHEN Len(T5.CreateTime)=4 THEN Stuff(T5.CreateTime,3,0,':') "
            SQLOrdenes_aut &= "WHEN LEN(T5.CreateTime) =3 THEN Stuff(T5.CreateTime,2,0,':')  "
            SQLOrdenes_aut &= "END AS horaCreacion, "
            SQLOrdenes_aut &= "T2.CardCode As CodCliente, T2.CardName As NombreCliente, CONVERT(varchar(50), CONVERT(MONEY, T2.DocTotal), 1) As TotalBorrador,T6.Balance AS SaldoCliente, T6.CreditLine AS LimiteCredito,  "
            SQLOrdenes_aut &= "Case WHEN T0.Status = 'Y' THEN 'Autorizado' WHEN T0.Status = 'W' THEN 'Pendiente' WHEN T0.Status = 'N' THEN 'Rechazado' End As Decision, "
            SQLOrdenes_aut &= "Case WHEN T0.ProcesStat = 'Y' THEN 'Autorizado' WHEN T0.ProcesStat = 'W' THEN 'Pendiente' WHEN T0.ProcesStat = 'N' THEN 'Rechazado' "
            SQLOrdenes_aut &= "WHEN T0.ProcesStat = 'C' THEN 'Cancelado' WHEN T0.ProcesStat = 'P' THEN 'Creado' WHEN T0.ProcesStat = 'A' THEN 'Autorizando' End As Estatus, T7.Remarks AS tipoAP , "
            SQLOrdenes_aut &= "T1.U_NAME AS SolicitadoPor, (SELECT FORMAT(UpdateDate, 'yyyy-MM-dd') FROM WDD1 WHERE WddCode = T0.WddCode AND (Status = 'Y' OR Status = 'N')) AS FechaDecision, "

            SQLOrdenes_aut &= " CASE WHEN CONVERT(varchar(50),LEN((SELECT UpdateTime FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N'))))  = '3'  THEN "
            SQLOrdenes_aut &= "STUFF((SELECT UpdateTime FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')),2,0,':') "
            SQLOrdenes_aut &= " WHEN CONVERT(varchar(50),LEN((SELECT UpdateTime FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N'))))  = '4'  THEN "
            SQLOrdenes_aut &= "STUFF((SELECT UpdateTime FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')),3,0,':') "
            SQLOrdenes_aut &= "END AS HoraDecision, "

            SQLOrdenes_aut &= "(SELECT Remarks FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')) AS ComentariosCobranza , "
            SQLOrdenes_aut &= "t0.Remarks AS ComentarioVentas, "
            SQLOrdenes_aut &= "Stuff((Select  ', '  + V.U_NAME  FROM WDD1 T INNER JOIN OUSR V ON T.UserID = V.USERID WHERE WddCode =T0.WddCode FOR XML PATH('')),1, 2,'') AS Autorizadores, "
            SQLOrdenes_aut &= "(SELECT V.U_NAME FROM WDD1 T INNER JOIN OUSR V ON T.UserID = V.USERID WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')) AS Autorizo, "
            SQLOrdenes_aut &= " t2.JrnlMemo as TipoDocumento, CASE WHEN T6.U_BXP_Ruta IS NULL THEN 'No definida' ELSE T6.U_BXP_Ruta END AS Ruta "
            SQLOrdenes_aut &= "From OWDD T0 INNER Join OUSR T1 ON T0.OwnerID = T1.USERID  INNER Join ODRF T2 ON T2.DocEntry = T0.DraftEntry "
            SQLOrdenes_aut &= "INNER Join DRF1 T3 ON T2.DocEntry = T3.DocEntry  INNER Join OWHS T4 ON T3.WhsCode = T4.WhsCode "
            SQLOrdenes_aut &= "INNER JOIN WDD1 T5 ON T0.ProcessID=T5.WddCode  INNER JOIN OCRD T6 ON T2.CardCode=T6.CardCode "
            SQLOrdenes_aut &= " INNER JOIN OWTM T7 ON T0.WtmCode=T7.WtmCode "
            'SQLOrdenes_aut &= "WHERE T2.DocDate BETWEEN '2019-06-25' AND '" + fecha_Aut + "'  " + Dtoid + " "
            SQLOrdenes_aut &= "WHERE T2.DocDate BETWEEN '2019-06-25' AND '" + fecha_Aut + "' "

            'Se agrega la condicacion del filtrado por hora
            SQLOrdenes_aut &= " AND CASE WHEN Len(T5.CreateTime)=4 THEN convert (time, Stuff(T5.CreateTime,3,0,':')) WHEN LEN(T5.CreateTime)=3 THEN convert (time, Stuff(T5.CreateTime,2,0,':')) END BETWEEN '" & Format(dtpHoraI.Value, "HH:mm:ss") & "' AND '" & Format(dtpHoraF.Value, "HH:mm:ss") & "'"

            'Finalizo query
            SQLOrdenes_aut &= " " + Dtoid

            cnn1 = New SqlConnection(StrCon)
            'ALMACENA LA CONSULTA EN UN COMMAND SQL
            cmd1 = New SqlCommand(SQLOrdenes_aut, cnn1)
            'CONVIERTE EL TEXTO EN CONSULTA
            cmd1.CommandType = CommandType.Text
            'APERTURA LA CONEXION
            cnn1.Open()
            'INSTANCIA UN ADAPTER
            da1 = New SqlDataAdapter
            'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
            da1.SelectCommand = cmd1
            'LO EJECUTA CON LA CONEXION
            da1.SelectCommand.Connection = cnn1
            'TIEMPO DE ESPERA DE LA CONEXION
            da1.SelectCommand.CommandTimeout = 10000
            'EJECUTA LA CONSULTA
            cmd1.ExecuteNonQuery()
            'CIERRA EL COMMAND DE SQL
            cmd1.Connection.Close()
            'CIERRA LA CONEXION
            cnn1.Close()
            'LLENA EL ADAPTER A UN DATA SET
            da1.Fill(DsOrdenes_aut)
            'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
            DTVAutorizaciones = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA
            DTVAutorizaciones.Table = DsOrdenes_aut.Tables(0)
            'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
            'DgvAutorizaciones.DataSource = Nothing
            'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
            DgvAutorizaciones.DataSource = DTVAutorizaciones
            'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW
            EstiloDgvAutorizaciones()
        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
    End Sub
    Sub MEjecuta_Orden()
        'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
        Dim cmd4 As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsOrdenes = New DataSet

        'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
        Try
            cnn = New SqlConnection(StrTpm) 'ORIGINAL
            'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBAS
            cmd4 = New SqlCommand("SP_Operacion_Ord", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value)) 'Fecha de hoy
            'MsgBox(DTPFecha.Value.ToString("dddd"))
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 10000
            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            cnn.Close()
        Catch ex As Exception
            'MessageBox.Show("Error al insertar las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End Try
        'FIN OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
    End Sub
    Sub MEjecuta_Orden_Ped()
        'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsOrdenes = New DataSet

        'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
        Try
            cnn = New SqlConnection(StrTpm) 'ORIGINAL
            'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBA
            cmd = New SqlCommand("SP_Operacion_Ord_Ped", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
            'MsgBox(DTPFecha.Value.ToString("dddd"))
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 10000
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            cnn.Close()
        Catch ex As Exception
            MessageBox.Show("Error al Mostrar los datos de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End Try
    End Sub
    Sub MEjecuta_Orden_Ped_Det()
        'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsOrdenes = New DataSet

        'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
        Try
            cnn = New SqlConnection(StrTpm) 'ORIGINAL
            'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBAS
            cmd = New SqlCommand("SP_Operacion_Ord_Ped_Det", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
            'MsgBox(DTPFecha.Value.ToString("dddd"))
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 10000
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            cnn.Close()
        Catch ex As Exception
            MessageBox.Show("Error al Insertar el detalle de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End Try
    End Sub
#End Region
#Region "Timers"
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'MANDA A LLAMAR EL MÉTODO DE INSERTAR LAS ORDENES DE VENTA DEL DÍA
        MEjecuta_Orden()
        'MANDA A LLAMAR EL MÉTODO DE MOSTRAR LAS ORDENES DE VENTA DEL DIA
        MEjecuta_Orden_Ped()
        'Verifica que el grido tenga datos y los actualiza ambos grid en este evento se actualiza cada cierto tiempo 
        LlenarDgvOperacion(Userid)

        If dgvOperacionOvTA.RowCount <> 0 Then

            dgvOperacionOvTA.EndEdit()
            bandera_edit = True
            Try
                Dim row As DataGridViewRow = dgvOperacionOvTA.CurrentRow()
                'Llena el datagrid detalle 
                LlenarDetalleOperacionVta(CStr(row.Cells("OrdenVenta").Value).ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        Else
        End If

        'Proceso para buscar nuevas ordenes creadas
        Try
            BuscarNuevasOV()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'Verificamos que el grid view tenga por lo menos una fila 
        If DgvAutorizaciones.RowCount <> 0 Then
            Try
                'LLENAMOS EL GRID CON LAS AUTORIZACIONES Y LAS VARIABLES GLOBALES (DEPARTAMENTO, ID USUARIO Y SUCURSAL)
                LlenarAutorizaciones()

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If

        Try
            BuscarNuevasOV()
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Eventos"
    Private Sub FrmOrdenVta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Si el usuario es la jefa de cobranza podra ver las autorizaciones de las demas chicas de cobranza 
        btnNuevaOrdenVenta.Visible = False
        panelOV.Visible = False

        BuscarNuevasOV()

        dtpHoraI.Value = CDate(fechaHoy & " 00:00:01")
        dtpHoraF.Value = CDate(fechaHoy & " 23:59:59")

        If UsrTPM = "COBRANZ2" Then
            CBBuscarUsuario.Visible = True
            CBBuscarUsuario.Enabled = True
            LBLbuscar_usuario.Visible = True
            LBLbuscar_usuario.Enabled = True
            CBBuscarUsuario.Items.Add("JACQUELINE")
            CBBuscarUsuario.Items.Add("Reina Uc")
            CBBuscarUsuario.Items.Add("YLIANA DOMINGUEZ")
            CBBuscarUsuario.Items.Add("ARACELI TRUJILLO")
            CBBuscarUsuario.SelectedIndex = 0
        End If
        'MANDA A LLAMAR EL MÉTODO DE INSERTAR LAS ORDENES DE VENTA DEL DÍA
        MEjecuta_Orden()
        'MANDA A LLAMAR EL MÉTODO DE MOSTRAR LAS ORDENES DE VENTA DEL DIA
        MEjecuta_Orden_Ped()
        Userid = ""
        'SE ASIGNA EL TIEMPO DE RECARGA DEL GRID EN MILISEGUNDOS
        Timer1.Interval = 90000
        'Activamos el evento timer para que el grid se actualize solo 
        Timer1.Enabled = True
        'Se obtiene el usuario del sap y se asignan las variables globales
        ObtenerUsuario()
        'Se llena el dataview pasandole como parametro el id del usuario en sap 
        LlenarDgvOperacion(Userid)
        'MANDA A LLAMAR EL METODO DE ESTILO DEL GRID
        EstiloDgvOperacion()

        'Obtiene el ARticulo seleccionado
        Dim row As DataGridViewRow = dgvOperacionOvTA.CurrentRow()
        'Llena el datagrid con el detalle validando que este vacio  para llenar el detalle 
        If dgvOperacionOvTA.RowCount > 0 Then
            LlenarDetalleOperacionVta(CStr(row.Cells("OrdenVenta").Value).ToString)
        End If
        'obtenemos el departamento y asignamos variables globales
        ObtenerDto()
        'Verificamos si el deparmento es de cobranza o de otra sucursal para ocultar la pagina de ordenes de venta / SIST = COBRANZA, CALG = VENTAS
        If (Dtoname = "SIST" Or Sucursal = "Merida" Or Sucursal = "Tuxtla") And Userid <> "30" Then 'Se incluyo en el codgo al usuario AINVEN como permiso especial
            'If Dtoname = "SIST" Or Sucursal = "Merida" Or Sucursal = "Tuxtla" Then
            TabPage1.Parent = Nothing
        End If
        'Llenamos autorizaciones con las variables globales que asignamos en ObtenerUsuario() y ObtenerDto()
        '  LlenarAutorizaciones()
        'ACtivamos el evento Timer 2
        Timer2.Interval = 160000
        'Activamos el evento timer para que el grid se actualize solo 
        Timer2.Enabled = True
        ' asignamos los valores al combobox
        CBAutorizaciones.Items.Add("Pendiente")
        CBAutorizaciones.Items.Add("Autorizado")
        CBAutorizaciones.Items.Add("Rechazado")
        'Dependiendo el departamento es el Item que se quedara por default en el Combobox
        If Dtoname = "CALG" Or UsrTPM = "MANAGER" Or UsrTPM = "RHUMANOS" Then
            CBAutorizaciones.Items.Add("Cancelado")
            CBAutorizaciones.Items.Add("Default(Pendiente, Autorizado, Rechazado)")
            CBAutorizaciones.SelectedIndex = 4
        Else
            CBAutorizaciones.SelectedIndex = 0
        End If

        'Bloquemos que el combobox sea solo de lectura 
        CBAutorizaciones.DropDownStyle = ComboBoxStyle.DropDownList
        CBBuscarUsuario.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub dgvOperacionOvTA_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOperacionOvTA.CellContentClick
        'Al dar click en alguna columna del GridView Operacion Vta Se actualizara el Detalle en otro grid
        Try
            Dim row As DataGridViewRow = dgvOperacionOvTA.CurrentRow()
            'Llena el datagrid detalle 
            LlenarDetalleOperacionVta(CStr(row.Cells("OrdenVenta").Value).ToString)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub dgvOperacionOvTA_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvOperacionOvTA.CellFormatting
        'Se definen los colores con paleta de coroles en RGB
        rojo = ColorTranslator.FromHtml("#FFC6C6")
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        verde = ColorTranslator.FromHtml("#74F242")
        Anaranjado = ColorTranslator.FromHtml("#FFCC80")
        Azul = ColorTranslator.FromHtml("#BAC9FC")
        Morado = ColorTranslator.FromHtml("#E8CCE8")
        'Morado = ColorTranslator.FromHtml("#0cb7f2") celeste   
        Gris = ColorTranslator.FromHtml("#CCCBCC")
        RojoEmpacado = ColorTranslator.FromHtml("#AFFDEB")


        'Dependiendo del estado de la columna Estatus la fila se pintara de un cierto color y en algunos casos cambiara las letras a negritas
        For i As Integer = 0 To Me.dgvOperacionOvTA.Rows.Count - 1

            If dgvOperacionOvTA.Rows(i).Cells("Estatus").Value Is DBNull.Value Then
            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "En Cola" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = Color.Empty
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)


            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "Surtiendo" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = Anaranjado
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)

            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "Surtido" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = RojoEmpacado
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "Cancelado" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = rojo
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "NO SURTIR" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = Color.Empty
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "EN ESPERA" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = amarillo
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "Por Empacar" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = Gris
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "Empacando" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = Azul
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "Empacado" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = verde
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)



                'NUEVOS ESTATUS
            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "En Revision" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = Color.SkyBlue
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            ElseIf Me.dgvOperacionOvTA.Rows(i).Cells("Estatus").Value = "Revisado" Then
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.BackColor = Color.LightPink
                Me.dgvOperacionOvTA.Rows(i).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)



            End If
        Next

        'Pondremos las letras en negritas de la Columna EstadoSurtido 
        If Me.dgvOperacionOvTA.Columns(e.ColumnIndex).Name = "EstadoSurtido" Then
            Dim estado As String = CStr(e.Value)
            Select Case estado
                Case "SI"
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
                Case "NO"
                    e.CellStyle.ForeColor = Color.Red
                    e.CellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
                Case "EN ESPERA"
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            End Select
        End If
    End Sub
    Private Sub btnActualizarFecha_Click(sender As Object, e As EventArgs) Handles btnActualizarFecha.Click
        'Este metodo actualiza ambos grid tomando como referencia la fecha que se requiere 
        dgvOperacionOvTA.Columns.Clear()
        LlenarDgvOperacion(Userid)
        'Obtiene el ARticulo seleccionado
        Dim row As DataGridViewRow = dgvOperacionOvTA.CurrentRow()
        'Llena el datagrid con el detalle 
        If dgvOperacionOvTA.RowCount > 0 Then
            LlenarDetalleOperacionVta(CStr(row.Cells("OrdenVenta").Value))
        Else
            'BORRAMOS LO QUE ESTA EN EL GRID Y LO ACTUALIZAMOS
            dgvOperacionDetalle.DataSource = Nothing
            dgvOperacionDetalle.Refresh()
        End If

        Try
            BuscarNuevasOV()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub dgvOperacionDetalle_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvOperacionDetalle.CellFormatting
        'Este metodo da color amarillo al grid view detalle siempre  y cuando exista una discrepancia entre el surtido real y lo pedido por el cliente
        'Define los colores que se mostraran en el detalle de la Orden
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        'Un ciclo para recorrer todos los datos del grid view y asi saber cuales pintaremos 
        For i As Integer = 0 To Me.dgvOperacionDetalle.Rows.Count - 1

            'Si el surtido real =Surtido  regresa valores nulos entonces no tendra color la fila 
            If dgvOperacionDetalle.Rows(i).Cells("Surtido").Value Is DBNull.Value Then
                Me.dgvOperacionDetalle.Rows(i).DefaultCellStyle.SelectionBackColor = Color.Empty
            Else
                'Si el surtido real es menos ala cantidad solicitada por el cliente  entonces se pintara de amarillo 
                If Convert.ToInt32(Me.dgvOperacionDetalle.Rows(i).Cells("Surtido").Value) < Convert.ToInt32(Me.dgvOperacionDetalle.Rows(i).Cells("Cantidad").Value) Then
                    Me.dgvOperacionDetalle.Rows(i).DefaultCellStyle.BackColor = amarillo
                    'Si el surtido real es mayor o igual ala cantidad solicitada por el cliente entonces no pintaremos la fila 
                ElseIf Convert.ToInt32(Me.dgvOperacionDetalle.Rows(i).Cells("Surtido").Value) >= Convert.ToInt32(Me.dgvOperacionDetalle.Rows(i).Cells("Cantidad").Value) Then
                    Me.dgvOperacionDetalle.Rows(i).DefaultCellStyle.BackColor = Color.Empty
                End If
            End If
        Next
    End Sub
    Private Sub dgvOperacionOvTA_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOperacionOvTA.SelectionChanged
        'En este evento actualizaremos el grid detalle cuando el usuario se mueva entre celdas 
        dgvOperacionDetalle.Columns.Clear()
        'Valida el ordenamiento del grid para el cambio de posicion 
        If dgvOperacionOvTA.CurrentRow Is Nothing Then
        Else
            Try
                Dim row As DataGridViewRow = dgvOperacionOvTA.CurrentRow()
                'Llena el datagrid detalle 
                LlenarDetalleOperacionVta(CStr(row.Cells("OrdenVenta").Value).ToString)
            Catch ex As Exception
                MsgBox("Error al obtener la Orden en llenado de Grid Detalle." + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Dato")
            End Try
        End If
    End Sub
    Private Sub txtbuscar_TextChanged(sender As Object, e As EventArgs) Handles txtbuscar.TextChanged
        'Filtra el dataview de una consulta ya hecha para filtrar campos ya existentes en un datagridview
        'ResultadoOrden.RowFilter = "OrdenVenta like '%" & CStr(txtbuscar.Text) & "%' OR Cliente like '%" & CStr(txtbuscar.Text) & "%' OR  Codigo like '%" & CStr(txtbuscar.Text) & "%'"

    End Sub
    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub
    Private Sub CBMosctrarCan_CheckedChanged(sender As Object, e As EventArgs) Handles CBMosctrarCan.CheckedChanged
        'Metodo al cambiar el checkbox mostrara las ordenes de venta canceladas o en caso de desmarca te mostrara las no canceladas
        LlenarDgvOperacion(Userid)

        If dgvOperacionOvTA.RowCount <> 0 Then

            dgvOperacionOvTA.EndEdit()
            bandera_edit = True
            Try
                Dim row As DataGridViewRow = dgvOperacionOvTA.CurrentRow()
                'Llena el datagrid detalle 
                LlenarDetalleOperacionVta(CStr(row.Cells("OrdenVenta").Value).ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
        End If
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub
    Private Sub DgvAutorizaciones_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvAutorizaciones.CellFormatting
        'Se definen los colores con paleta de coroles en RGB
        rojo = ColorTranslator.FromHtml("#FFC6C6")
        amarillo = ColorTranslator.FromHtml("#FFFF99")
        verde = ColorTranslator.FromHtml("#A6FFA0")
        Anaranjado = ColorTranslator.FromHtml("#FFCC80")
        'Dependiendo del estado de la columna Estatus la fila se pintara de un cierto color 
        For i As Integer = 0 To Me.DgvAutorizaciones.Rows.Count - 1
            'Si esta autorizado se pintara en verde
            If Me.DgvAutorizaciones.Rows(i).Cells("Estatus").Value = "Autorizado" Then
                Me.DgvAutorizaciones.Rows(i).DefaultCellStyle.BackColor = verde
                'Si esta rechazado se pintara en color rojo
            ElseIf Me.DgvAutorizaciones.Rows(i).Cells("Estatus").Value = "Rechazado" Then
                Me.DgvAutorizaciones.Rows(i).DefaultCellStyle.BackColor = rojo

                'Si esta pendiente Sera color amarillo

            ElseIf Me.DgvAutorizaciones.Rows(i).Cells("Estatus").Value = "Pendiente" Then
                Me.DgvAutorizaciones.Rows(i).DefaultCellStyle.BackColor = amarillo

            End If
        Next

    End Sub
    Private Sub BtnActualizar_autorizacion_Click(sender As Object, e As EventArgs) Handles BtnActualizar_autorizacion.Click
        'MANDA A LLAMAR EL MÉTODO DE INSERTAR LAS ORDENES DE VENTA DEL DÍA
        MEjecuta_Orden()
        'MANDA A LLAMAR EL MÉTODO DE MOSTRAR LAS ORDENES DE VENTA DEL DIA
        MEjecuta_Orden_Ped()
        'Limpiamos el grid autorizaciones
        DgvAutorizaciones.Columns.Clear()
        'Obtenemos el Departamento 
        ObtenerDto()
        'Llenamos el grid y ponemos el combobox por default dependiento el departamento 
        LlenarAutorizaciones()
        If Dtoname = "CALG" Or UsrTPM = "MANAGER" Or UsrTPM = "RHUMANOS" Then
            CBAutorizaciones.SelectedIndex = 0
        Else
            CBAutorizaciones.SelectedIndex = 0

        End If

        txtbuscarautorizaciones.Text = ""

        'Proceso para buscar nuevas ordenes creadas
        Try
            BuscarNuevasOV()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CBAutorizaciones_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBAutorizaciones.SelectionChangeCommitted
        'Si el combobox esta marcado como default se llenara con los campos especificos 
        If CBAutorizaciones.SelectedItem.ToString = "Default(Pendiente, Autorizado, Rechazado)" Then
            'limpiaremos el data grid view
            DgvAutorizaciones.Columns.Clear()
            'Obtendremos el departamento
            ObtenerDto()
            'llenaremos el grid view con las variables globales que llenamos en el metodo obtenerDto
            LlenarAutorizaciones()
        Else
            'Llenamos la consulta con el combobox seleccionado 
            LlenarConCB(CBAutorizaciones.SelectedItem.ToString)
        End If
    End Sub
    Private Sub CBAutorizaciones_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CBAutorizaciones.KeyPress
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtbuscarautorizaciones.TextChanged
        'TEXTBOX AUTORIZACIONES
        'Filtra el dataview de una consulta ya hecha para filtrar campos ya existentes en un datagridview
        DTVAutorizaciones.RowFilter = "NombreCliente like '%" & CStr(txtbuscarautorizaciones.Text) & "%' OR  CodCliente like '%" & CStr(txtbuscarautorizaciones.Text) & "%'"

    End Sub
    Private Sub lblBuscar_Click(sender As Object, e As EventArgs) Handles lblBuscar.Click

    End Sub
    Private Sub DgvAutorizaciones_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvAutorizaciones.CellContentClick



    End Sub
#End Region
#Region "Estilos"
    Sub EstiloDgvOperacion()
        'cambia el estilo de las columnas del gridview detalle operacion  
        'Cambiar el estido del campo Orden de Venta a Negritas
        Dim style As New DataGridViewCellStyle
        style.Font = New Font(dgvOperacionDetalle.Font, FontStyle.Bold)
        With Me.dgvOperacionOvTA
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Orden de ventta
            .Columns("OrdenVenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("OrdenVenta").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            .Columns("OrdenVenta").DefaultCellStyle = style
            .Columns("OrdenVenta").HeaderText = "Orden Venta"
            .Columns("OrdenVenta").Width = 60
            .Columns("OrdenVenta").ReadOnly = True
            'OrdenEntrega        
            .Columns("OrdenEntrega").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("OrdenEntrega").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            .Columns("OrdenEntrega").DefaultCellStyle = style
            .Columns("OrdenEntrega").HeaderText = "Orden Entrega"
            .Columns("OrdenEntrega").Width = 60
            .Columns("OrdenEntrega").ReadOnly = True
            'Fecha de documento 
            .Columns("FechaDocumento").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaDocumento").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("FechaDocumento").HeaderText = "Fecha Documento"
            .Columns("FechaDocumento").Width = 80
            .Columns("FechaDocumento").ReadOnly = True
            'nombre del cliente
            .Columns("Cliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Cliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cliente").HeaderText = "Cliente"
            .Columns("Cliente").Width = 200
            .Columns("Cliente").ReadOnly = True
            'codigo del cliente
            .Columns("Codigo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Codigo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Codigo").HeaderText = "Código"
            .Columns("Codigo").DefaultCellStyle.Format = "N0"
            .Columns("Codigo").Width = 55
            .Columns("Codigo").ReadOnly = True
            .Columns("Codigo").Frozen = True
            'hora de impresion
            .Columns("HoraImpresion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("HoraImpresion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("HoraImpresion").HeaderText = "Hora Creación"
            .Columns("HoraImpresion").Width = 140
            .Columns("HoraImpresion").ReadOnly = True
            'partidas
            .Columns("partidas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("partidas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("partidas").HeaderText = "Partidas"
            .Columns("partidas").Width = 60
            .Columns("partidas").ReadOnly = True
            .Columns("partidas").Visible = False
            'estatus 
            .Columns("Estatus").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Estatus").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Estatus").HeaderText = "Estatus"
            .Columns("Estatus").Width = 80
            .Columns("Estatus").ReadOnly = True
            'estado surtiemiento
            .Columns("EstadoSurtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("EstadoSurtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("EstadoSurtido").HeaderText = "Estado Surtido"
            .Columns("EstadoSurtido").Width = 80
            .Columns("EstadoSurtido").ReadOnly = True
            'hora tomada
            .Columns("HoraTomada").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("HoraTomada").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("HoraTomada").HeaderText = "Inicio Surtido"
            .Columns("HoraTomada").Width = 140
            .Columns("HoraTomada").ReadOnly = True
            'hora fin surtido 
            .Columns("HoraFinSurtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("HoraFinSurtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("HoraFinSurtido").HeaderText = "Término Surtido"
            .Columns("HoraFinSurtido").Width = 140
            .Columns("HoraFinSurtido").ReadOnly = True
            'tiempo de empaque 
            .Columns("TiempoEmpaque").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("TiempoEmpaque").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("TiempoEmpaque").HeaderText = "Inicio Empaque"
            .Columns("TiempoEmpaque").Width = 140
            .Columns("TiempoEmpaque").ReadOnly = True
            'tiempo empacado 
            .Columns("TiempoEmpacado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("TiempoEmpacado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("TiempoEmpacado").HeaderText = "Término Empaque"
            .Columns("TiempoEmpacado").Width = 140
            .Columns("TiempoEmpacado").ReadOnly = True
            'tiempo dinalizado 
            .Columns("TiempoFinalizado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("TiempoFinalizado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("TiempoFinalizado").HeaderText = "Tiempo Finalizado"
            .Columns("TiempoFinalizado").Width = 140
            .Columns("TiempoFinalizado").ReadOnly = True
            .Columns("TiempoFinalizado").Visible = False
            'tiempo total 
            .Columns("TiempoTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("TiempoTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("TiempoTotal").HeaderText = "Tiempo Consumido"
            .Columns("TiempoTotal").Width = 150
            .Columns("TiempoTotal").ReadOnly = True
            'Numero de cajas 
            .Columns("Cajas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Cajas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cajas").HeaderText = "Cajas"
            .Columns("Cajas").DefaultCellStyle.Format = "N0"
            .Columns("Cajas").Width = 35
            .Columns("Cajas").ReadOnly = True
            'Comentario de ventas para almacen 
            .Columns("Comentario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comentario").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Comentario").HeaderText = "Comentario"
            .Columns("Comentario").Width = 150
            .Columns("Comentario").ReadOnly = True
            'Observacion
            .Columns("Observacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Observacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Observacion").HeaderText = "Observación"
            .Columns("Observacion").Width = 150
            .Columns("Observacion").ReadOnly = True
            'Peso
            .Columns("Peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Peso").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Peso").HeaderText = "Peso Total"
            .Columns("Peso").DefaultCellStyle.Format = "N2"
            .Columns("Peso").Width = 50
            .Columns("Peso").ReadOnly = True
        End With
    End Sub

    Sub EstiloDgvOrdenesVenta()
        'cambia el estilo de las columnas del gridview detalle operacion  
        'Cambiar el estido del campo Orden de Venta a Negritas
        Dim style As New DataGridViewCellStyle
        style.Font = New Font(dgvOperacionDetalle.Font, FontStyle.Bold)
        With Me.dgvNuevaOV
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Serie
            .Columns("Serie").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Serie").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            .Columns("Serie").DefaultCellStyle = style
            .Columns("Serie").HeaderText = "Serie"
            .Columns("Serie").Width = 30
            .Columns("Serie").ReadOnly = True
            'IdOrdenVenta
            .Columns("IdOrdVta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("IdOrdVta").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            .Columns("IdOrdVta").DefaultCellStyle = style
            .Columns("IdOrdVta").HeaderText = "Id Orden Venta"
            .Columns("IdOrdVta").Width = 50
            .Columns("IdOrdVta").ReadOnly = True
            'Id Cliente
            .Columns("IdCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("IdCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("IdCliente").HeaderText = "Id Cliente"
            .Columns("IdCliente").Width = 50
            .Columns("IdCliente").ReadOnly = True
            'Nombre Cliente
            .Columns("DesClte").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("DesClte").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("DesClte").HeaderText = "Cliente"
            .Columns("DesClte").Width = 250
            .Columns("DesClte").ReadOnly = True
            'Importe total
            .Columns("DocTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("DocTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("DocTotal").HeaderText = "Total documento"
            .Columns("DocTotal").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("DocTotal").Width = 80
            .Columns("DocTotal").ReadOnly = True
            .Columns("DocTotal").Frozen = True
        End With
    End Sub

    Sub EstiloDetalleOperacionVta()
        'Este metodo cambia el estilo del gridview detalle 
        With Me.dgvOperacionDetalle
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Partidas
            .Columns("Partidas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("Partidas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Partidas").HeaderText = "Partidas"
            .Columns("Partidas").Width = 45
            .Columns("Partidas").ReadOnly = True
            'Bloquea el ordenamiento del gridview
            .Columns("Partidas").SortMode = DataGridViewColumnSortMode.NotSortable
            'Articulo
            .Columns("Articulo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Articulo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Articulo").HeaderText = "Artículo"
            .Columns("Articulo").Width = 75
            .Columns("Articulo").ReadOnly = True
            'Bloquea el ordenamiento del gridview
            .Columns("Articulo").SortMode = DataGridViewColumnSortMode.NotSortable
            'Descripcion
            .Columns("Descripcion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Descripcion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Descripcion").HeaderText = "Descripción"
            .Columns("Descripcion").Width = 240
            .Columns("Descripcion").ReadOnly = True
            'Bloquea el ordenamiento del gridview
            .Columns("Descripcion").SortMode = DataGridViewColumnSortMode.NotSortable
            'Cantidad
            .Columns("Cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Cantidad").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cantidad").HeaderText = "Cantidad"
            .Columns("Cantidad").DefaultCellStyle.Format = "N0"
            .Columns("Cantidad").Width = 50
            .Columns("Cantidad").ReadOnly = True
            'Bloquea el ordenamiento del gridview
            .Columns("Cantidad").SortMode = DataGridViewColumnSortMode.NotSortable
            'Surtido
            .Columns("Surtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Surtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Surtido").HeaderText = "Surtido"
            .Columns("Surtido").Width = 50
            .Columns("Surtido").ReadOnly = True
            .Columns("Surtido").SortMode = DataGridViewColumnSortMode.NotSortable
            'Peso 
            .Columns("Peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Peso").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Peso").HeaderText = "Peso"
            .Columns("Peso").Width = 50
            .Columns("Peso").ReadOnly = True
            .Columns("Peso").SortMode = DataGridViewColumnSortMode.NotSortable
        End With
    End Sub
    Sub EstiloDgvAutorizaciones()
        Dim style As New DataGridViewCellStyle
        style.Font = New Font(dgvOperacionDetalle.Font, FontStyle.Bold)

        With Me.DgvAutorizaciones
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Orden de venta
            .Columns("OrdenDeVenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("OrdenDeVenta").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            .Columns("OrdenDeVenta").DefaultCellStyle = style
            .Columns("OrdenDeVenta").HeaderText = "Orden De Venta"
            .Columns("OrdenDeVenta").Width = 60
            .Columns("OrdenDeVenta").ReadOnly = True
            .Columns("OrdenDeVenta").Visible = False
            'Fecha de creacion
            .Columns("FechaCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("FechaCreacion").HeaderText = "Fecha Creacion "
            .Columns("FechaCreacion").Width = 80
            .Columns("FechaCreacion").ReadOnly = True
            'hora de Creacion
            .Columns("horaCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("horaCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("horaCreacion").HeaderText = "Hora Creacion"
            .Columns("horaCreacion").Width = 50
            .Columns("horaCreacion").ReadOnly = True
            'Codigo del cliente
            .Columns("CodCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("CodCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("CodCliente").HeaderText = "Codigo Cliente"
            .Columns("CodCliente").Width = 55
            .Columns("CodCliente").ReadOnly = True
            'Nombre del cliente
            .Columns("NombreCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("NombreCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("NombreCliente").HeaderText = "Nombre Cliente"
            .Columns("NombreCliente").Width = 200
            .Columns("NombreCliente").ReadOnly = True
            'decicion que tomo cobranza
            .Columns("Decision").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Decision").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Decision").HeaderText = "Decision"
            .Columns("Decision").Width = 70
            .Columns("Decision").ReadOnly = True
            .Columns("Decision").Visible = False
            'estatus 
            .Columns("Estatus").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Estatus").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Estatus").HeaderText = "Estatus"
            .Columns("Estatus").Width = 80
            .Columns("Estatus").ReadOnly = True
            'Tipo de aprovacion 
            .Columns("tipoAP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("tipoAP").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("tipoAP").HeaderText = "Tipo Aprovación"
            .Columns("tipoAP").Width = 150
            .Columns("tipoAP").ReadOnly = True
            'Quien solicita la autorizacion
            .Columns("SolicitadoPor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("SolicitadoPor").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("SolicitadoPor").HeaderText = "Solicitado Por"
            .Columns("SolicitadoPor").Width = 95
            .Columns("SolicitadoPor").ReadOnly = True
            'Unicamente se mostrara si quien solicita es del departamento de cobranza
            If Dtoname = "CALG" Then
                .Columns("SolicitadoPor").Visible = False
            End If
            'Fecha de desicio 
            .Columns("FechaDecision").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("FechaDecision").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("FechaDecision").HeaderText = "Fecha Decision"
            .Columns("FechaDecision").Width = 70
            .Columns("FechaDecision").ReadOnly = True
            'hora de decision 
            .Columns("HoraDecision").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("HoraDecision").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("HoraDecision").HeaderText = "Hora Decision"
            .Columns("HoraDecision").Width = 50
            .Columns("HoraDecision").ReadOnly = True
            'comentarios de cobranza hacia ventas
            .Columns("ComentariosCobranza").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("ComentariosCobranza").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("ComentariosCobranza").HeaderText = "Comentarios Cobranza"
            .Columns("ComentariosCobranza").Width = 120
            .Columns("ComentariosCobranza").ReadOnly = True
            'Solo se mostraran los comentarios de cobranza a las personas de ventas
            If Dtoname = "SIST" Then
                .Columns("ComentariosCobranza").Visible = False
            End If
            'Comentarios de ventas 
            .Columns("ComentarioVentas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("ComentarioVentas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("ComentarioVentas").HeaderText = "ComentarioVentas"
            .Columns("ComentarioVentas").Width = 120
            .Columns("ComentarioVentas").ReadOnly = True
            'Los comentarios de ventas solo lo veran las personas que sean de cobranza
            If Dtoname <> "SIST" Then
                .Columns("ComentarioVentas").Visible = False
            End If
            'Autorizadores
            .Columns("Autorizadores").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Autorizadores").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Autorizadores").HeaderText = "Autorizadores"
            .Columns("Autorizadores").Width = 100
            .Columns("Autorizadores").ReadOnly = True
            .Columns("Autorizadores").Visible = False
            'Autorizo
            .Columns("Autorizo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Autorizo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Autorizo").HeaderText = "Autorizo"
            .Columns("Autorizo").Width = 100
            .Columns("Autorizo").ReadOnly = True
            'Solo las personas de ventas veran quien autorizo el documento por posibles aclaraciones
            If Dtoname = "SIST" Then
                .Columns("Autorizo").Visible = False
            End If
            'Total del documento por autorizar
            .Columns("TotalBorrador").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("TotalBorrador").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("TotalBorrador").HeaderText = "Monto a Autorizar"
            .Columns("TotalBorrador").DefaultCellStyle.Format = "N2"
            .Columns("TotalBorrador").Width = 75
            .Columns("TotalBorrador").ReadOnly = True
            'Saldo del cliente
            .Columns("SaldoCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("SaldoCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("SaldoCliente").HeaderText = "Saldo Cliente"
            .Columns("SaldoCliente").Width = 75
            .Columns("SaldoCliente").DefaultCellStyle.Format = "N2"
            .Columns("SaldoCliente").ReadOnly = True
            'Limite de credio del cliente 
            .Columns("LimiteCredito").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("LimiteCredito").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("LimiteCredito").HeaderText = "Limite Credito"
            .Columns("LimiteCredito").Width = 75
            .Columns("LimiteCredito").DefaultCellStyle.Format = "N2"
            .Columns("LimiteCredito").ReadOnly = True
            'Tipo de documento
            .Columns("TipoDocumento").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("TipoDocumento").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("TipoDocumento").HeaderText = "Tipo Documento"
            .Columns("TipoDocumento").Width = 150
            .Columns("TipoDocumento").ReadOnly = True
            'Ruta
            .Columns("Ruta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Ruta").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Ruta").HeaderText = "Ruta"
            .Columns("Ruta").Width = 100
            .Columns("Ruta").ReadOnly = True
        End With
    End Sub

    Private Sub dgvOperacionDetalle_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOperacionDetalle.CellContentClick

    End Sub

    Private Sub CBBuscarUsuario_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBBuscarUsuario.SelectionChangeCommitted
        CBAutorizaciones.SelectedIndex = 0
        DgvAutorizaciones.Columns.Clear()
        'Obtendremos el departamento
        ObtenerDto()
        'llenaremos el grid view con las variables globales que llenamos en el metodo obtenerDto
        LlenarAutorizaciones()
    End Sub

    Private Sub CBAutorizaciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBAutorizaciones.SelectedIndexChanged
    End Sub

    Private Sub btnNuevaOrdenVenta_Click(sender As Object, e As EventArgs) Handles btnNuevaOrdenVenta.Click
        panelOV.Visible = True
    End Sub

    Private Sub btnCancelarOV_Click(sender As Object, e As EventArgs) Handles btnCancelarOV.Click
        panelOV.Visible = False
    End Sub

    Private Sub dgvNuevaOV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvNuevaOV.CellContentClick
        'Check
    End Sub

    Private Sub txtFolio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFolio.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btnPrintOV_Click(sender As Object, e As EventArgs) Handles btnPrintOV.Click
        Dim Serie As String
        Dim Folio As Int16

        If txtSerie.Text.Trim() <> "" And txtFolio.Text.Trim() <> "" Then
            ImprimirOV(txtSerie.Text.Trim(), txtFolio.Text.Trim())
        Else
            If (dgvNuevaOV.SelectedRows.Count > 0) Then
                For Each row As DataGridViewRow In dgvNuevaOV.Rows
                    If row.Selected Then
                        If IsNothing(row.Cells(0).Value) = False Then
                            Serie = row.Cells(0).Value.ToString
                            Folio = Integer.Parse(row.Cells(1).Value.ToString)

                            If Trim(Serie) <> "" And Folio > 0 Then
                                ImprimirOV(Serie, Folio)
                            End If
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If (btnNuevaOrdenVenta.BackColor = Color.Red) Then
            btnNuevaOrdenVenta.BackColor = Color.Yellow
        Else
            btnNuevaOrdenVenta.BackColor = Color.Red
        End If
    End Sub

    Private Sub chkActualizar_CheckedChanged(sender As Object, e As EventArgs) Handles chkActualizar.CheckedChanged
        If chkActualizar.CheckState = CheckState.Checked Then
            Timer1.Enabled = True
        Else
            Timer1.Enabled = False
        End If
    End Sub

#End Region
End Class