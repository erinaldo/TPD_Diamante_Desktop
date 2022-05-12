Imports System.Data.SqlClient

Public Class frmVisita_Agente

    'VARIBALE QUE ALMACENA LA FECHA DEL TIME PICKER
    Dim fecha_visita As String
    'VARIBLE GLOBAL A NIVEL FORMLARIO QUE ALMACENA EL USUARIO DE LOGUEO
    Dim UsuarioTPD As String
    'VARIBALE GLOBAL A NIVEL DE FORMULARIO DE UN DATA VIEW PARA PODER HACER EL FILTRO DE USUARIOS
    Dim DVUsuario As DataView = New DataView()
    'VARIBALE GLOBAL A NIVEL DE FORMULARIO DE UN DATA VIEW PARA PODER HACER EL FILTRO DE AGENTES
    Dim DVAgente As DataView = New DataView()
    'VARIBALE GLOBAL A NIVEL DE FORMULARIO DE UN DATA VIEW PARA PODER HACER EL FILTRO DE CLIENTES
    Dim DVCliente As DataView = New DataView()
    'VARIBALE GLOBAL A NIVEL DE FORMULARIO DE UN DATA VIEW PARA PODER HACER EL FILTRO DE CLIENTES
    Dim DVDatos As DataView = New DataView()

    Dim DVDatos2 As DataView = New DataView()
    'VARIABLE PARA LEER EL CARACTER DEL COMBO
    Dim Str As String
    'CREA INSTANCIA DE COLUMNA CON IMAGEN Y DA FORMATO
    Dim C1 As New DataGridViewCheckBoxColumn 'PEDIDO
    Dim C2 As New DataGridViewCheckBoxColumn 'COBRANZA
    Dim C3 As New DataGridViewCheckBoxColumn 'OTROS
    'Fecha de inicio
    Dim fecha_ini As String
    'Fecha de termino
    Dim fecha_ter As String

    Private Sub frmVisita_Agente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MANDA A LLAMAR EL METODO DE BUSCAR USUARIO DE MARKETING
        BuscaUsuario()
        'MANDA A LLAMAR EL METODO DE BUSCAR USUARIO DE AGENTE
        'BuscaAgente()
        ''MANDA A LLAMAR EL METODO DE BUSCAR CLIENTE
        BuscaCliente()
        ''MANDA A LLAMAR EL METODO DE LLENADO DEL GRID CON BASE A LA FECHA
        'LLenaGrid()
        'VERIFICA SI EL USUARIO ES MANAGER DA ACCESO AL TAP PAGE 2 EN CASO CONTRARIO DESAPARECE LA PAGINA 
        If UsrTPM = "MANAGER" Or UsrTPM = "COBRANZ2" Or UsrTPM = "CONTAB1" Then
            CBAgenteM.SelectedIndex = -1

        Else
            tpSurpevisor.Text = "Historial"
        End If
        If UsrTPM = "RROBLES" Then
            cmbAgente_Ventas.SelectedIndex = -1
            CBAgenteV.SelectedIndex = -1
        End If
        If UsrTPM = "VENTAS8" Then
            cmbAgente_Ventas.SelectedIndex = -1
        End If
        If UsrTPM = "MANAGER" Then
            CBAgenteVeNO.Items.Add("TODOS")
            CBAgenteVeNO.Items.Add("JAIME SANCHEZ (28)")
            CBAgenteVeNO.Items.Add("RODOLFO MERCADO (19)")
            CBAgenteVeNO.Items.Add("AURELIO CASTRO (26)")
            CBAgenteVeNO.Items.Add("MARCO LOPEZ (36)")
            CBAgenteVeNO.Items.Add("VICTOR VERGARA (16)")
            CBAgenteVeNO.Items.Add("ALFONSO SERRANO")
            CBAgenteVeNO.Items.Add("RICARDO ROBLES (33)")
            'CBAgenteVeNO.Items.Add("FELIX GUERRERO")

            CBAgenteVeNO.SelectedIndex = -1
        End If
        If UsrTPM = "VVERGARA" Then
            CBAgenteVeNO.Items.Add("VICTOR VERGARA (16)")
            CBAgenteVeNO.SelectedIndex = -1
        End If

        If UsrTPM = "VENTAS8" Then
            CBAgenteVeNO.Items.Add("ALFONSO SERRANO")
            'CBAgenteVeNO.Items.Add("FELIX GUERRERO")
            CBAgenteVeNO.SelectedIndex = -1
        End If
        If UsrTPM = "RROBLES" Then
            CBAgenteVeNO.Items.Add("RICARDO ROBLES (33)")
            CBAgenteVeNO.SelectedIndex = -1
        End If

        If UsrTPM = "ASTRIDY" Then
            CBAgenteVeNO.Items.Add("MARCO LOPEZ (36)")
            CBAgenteVeNO.SelectedIndex = -1
        End If
        If UsrTPM = "VENTAS2" Then
            CBAgenteVeNO.Items.Add("RODOLFO MERCADO (19)")
            CBAgenteVeNO.SelectedIndex = -1
        End If
        If UsrTPM = "VENTAS4" Then
            CBAgenteVeNO.Items.Add("MARCO LOPEZ (36)")
            CBAgenteVeNO.SelectedIndex = -1
        End If
        If UsrTPM = "VENTAS3" Then
            CBAgenteVeNO.Items.Add("AURELIO CASTRO (26)")
            CBAgenteVeNO.SelectedIndex = -1
        End If
        If UsrTPM = "RLIRA" Then
            CBAgenteVeNO.Items.Add("JAIME SANCHEZ (28)")
            CBAgenteVeNO.SelectedIndex = -1
        End If

        'COLOCAR EL SelectedIndex EN -1 PARA QUE NO APAREZCA NINGUN NOMBRE Y SE EJECUTE EL EVENTO
        CBAgenteM.SelectedIndex = -1
        cmbAgente_Marketing.SelectedIndex = -1

    End Sub
#Region "FUNCIONAES"

#End Region
#Region "METODOS"
    ' METODO DE BUSQUEDA DE USUARIOS PARA COLOCAR EL AGENTE DE MARKETING
    Sub BuscaUsuario()
        'COLOCAR EL FOCO EN AMBOS CBMARCKETING PARA QUE SE EJECUTE EL EVENTO LEAVE
        'MessageBox.Show(tcVisitas.SelectedTab.Name)
        'cmbAgente_Marketing.Select()
        'CBAgenteM.Select()

        'VARIABLE DE CONSULTA
        Dim SQLUsuario As String
        Try
            '//ABE LA CONEXION
            conexion_uni.Open()
            'VALIDA SI SE MUESTRAN TODOS LOS USUARIOS O NO DEPENDIENDO SI ES MANAGER
            If UsrTPM = "MANAGER" Or UsrTPM = "CONTAB1" Then
                SQLUsuario = "SELECT Id_Usuario, Nombre FROM TPM.dbo.USUARIOS WHERE Id_Usuario IN ('VVERGARA','VENTAS8','RROBLES','ASTRIDY','VENTAS2', 'VENTAS4', 'VENTAS3', 'RLIRA', 'MANAGER','CONTAB1') "
            Else
                'ALMACENA LA CONSULTA
                SQLUsuario = "SELECT Id_Usuario, Nombre FROM TPM.dbo.USUARIOS WHERE Id_Usuario = '" + UsrTPM + "'"
            End If
            'CREA EL ADAPTER
            Dim DAUsuario As SqlDataAdapter = New SqlDataAdapter(SQLUsuario, conexion_uni)
            '//INSTACIA EL DATASET Y DATAVIEW
            Dim DSUsuario As DataSet = New DataSet()
            '//ASIGNA AL DATASET
            DAUsuario.Fill(DSUsuario, "Usuarios")
            '//ASIGNAN AL DATAVIEW
            DVUsuario.Table = DSUsuario.Tables("Usuarios")
            'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
            cmbAgente_Marketing.DataSource = DVUsuario
            '//cmbCardCode.DisplayMember = "CardName";
            cmbAgente_Marketing.DisplayMember = "Nombre"
            cmbAgente_Marketing.ValueMember = "Id_Usuario"
            'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
            CBAgenteM.DataSource = DVUsuario
            CBAgenteM.DisplayMember = "Nombre"
            If CBAgenteM.DisplayMember = "SALVADOR DÍAZ" Then

                CBAgenteM.DisplayMember = "TODOS"
            Else
                CBAgenteM.ValueMember = "Id_Usuario"
            End If
            CBagenteMARNO.DataSource = DVUsuario
            '//cmbCardCode.DisplayMember = "CardName";
            CBagenteMARNO.DisplayMember = "Nombre"
            CBagenteMARNO.ValueMember = "Id_Usuario"
            CBagenteMARNO.Enabled = False


            '//CIERRA LA CONEXION
            conexion_uni.Close()
        Catch ex As Exception
            'SOLO SI SE OCUPA
            MsgBox("Error:" + ex.ToString)
            '//CIERRA LA CONEXION
            conexion_uni.Close()
        End Try
    End Sub
    ' METODO DE BUSQUEDA DE AGENTES PARA COLOCAR EL AGENTE DE VENTAS
    Sub BuscaAgente()
        'VARIABLE DE CONSULTA
        Dim SQLAgente As String
        Try
            '//ABE LA CONEXION
            conexion_uni.Open()
            'ALMACENA LA CONSULTA
            SQLAgente = "SELECT T0.Id_Asociado, T1.Nombre " +
                        "FROM TPM.dbo.Visita_Asociado T0 LEFT JOIN TPM.dbo.Usuarios T1 ON T0.Id_Asociado = T1.Id_Usuario " +
                        "WHERE T0.Id_Usuario = '" + cmbAgente_Marketing.SelectedValue + "' "
            'CREA EL ADAPTER
            Dim DAAgente As SqlDataAdapter = New SqlDataAdapter(SQLAgente, conexion_uni)
            '//INSTACIA EL DATASET Y DATAVIEW
            Dim DSAgente As DataSet = New DataSet()
            '//ASIGNA AL DATASET
            DAAgente.Fill(DSAgente, "Agentes")
            '//ASIGNAN AL DATAVIEW
            DVAgente.Table = DSAgente.Tables("Agentes")
            'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
            cmbAgente_Ventas.DataSource = DVAgente
            '//cmbCardCode.DisplayMember = "CardName";
            cmbAgente_Ventas.DisplayMember = "Nombre"
            cmbAgente_Ventas.ValueMember = "Id_Asociado"
            'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
            CBAgenteV.DataSource = DVAgente
            CBAgenteV.DisplayMember = "Nombre"
            CBAgenteV.ValueMember = "Id_Asociado"
            '//CIERRA LA CONEXION

            conexion_uni.Close()
        Catch ex As Exception
            'SOLO SI SE OCUPA
            MsgBox("eRROR:" + ex.ToString)
            '//CIERRA LA CONEXION
            conexion_uni.Close()
        End Try
    End Sub

    Sub BuscaAgenteSU()
        'VARIABLE DE CONSULTA
        Dim SQLAgente As String
        Try
            '//ABE LA CONEXION
            conexion_uni.Open()
            'ALMACENA LA CONSULTA
            SQLAgente = "SELECT T0.Id_Asociado, T1.Nombre " +
                        "FROM TPM.dbo.Visita_Asociado T0 LEFT JOIN TPM.dbo.Usuarios T1 ON T0.Id_Asociado = T1.Id_Usuario " +
                        "WHERE T0.Id_Usuario = '" + CBAgenteM.SelectedValue + "' "
            'CREA EL ADAPTER
            Dim DAAgente As SqlDataAdapter = New SqlDataAdapter(SQLAgente, conexion_uni)
            '//INSTACIA EL DATASET Y DATAVIEW
            Dim DSAgente As DataSet = New DataSet()
            '//ASIGNA AL DATASET
            DAAgente.Fill(DSAgente, "Agentes")
            '//ASIGNAN AL DATAVIEW
            DVAgente.Table = DSAgente.Tables("Agentes")
            'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
            cmbAgente_Ventas.DataSource = DVAgente
            '//cmbCardCode.DisplayMember = "CardName";
            cmbAgente_Ventas.DisplayMember = "Nombre"
            cmbAgente_Ventas.ValueMember = "Id_Asociado"
            'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
            CBAgenteV.DataSource = DVAgente
            CBAgenteV.DisplayMember = "Nombre"
            CBAgenteV.ValueMember = "Id_Asociado"
            '//CIERRA LA CONEXION
            conexion_uni.Close()
        Catch ex As Exception
            'SOLO SI SE OCUPA
            MsgBox("eRROR:" + ex.ToString)
            '//CIERRA LA CONEXION
            conexion_uni.Close()
        End Try
    End Sub

    'METODO DE BUSQUEDA DE CLIENTE
    Sub BuscaCliente()
        'VARIABLE DE CONSULTA
        Dim SQLCliente As String
        Try
            '//ABE LA CONEXION
            conexion_uni.Open()
            'ALMACENA LA CONSULTA
            SQLCliente = "SELECT CardCode, CardName, CardCode + '  -  ' + CardName AS Cliente FROM SBO_TPD.dbo.OCRD WHERE CardType = 'C' "
            'CREA EL ADAPTER
            Dim DACliente As SqlDataAdapter = New SqlDataAdapter(SQLCliente, conexion_uni)
            '//INSTACIA EL DATASET Y DATAVIEW
            Dim DSCliente As DataSet = New DataSet()
            '//ASIGNA AL DATASET
            DACliente.Fill(DSCliente, "Clientes")
            '//ASIGNAN AL DATAVIEW
            DVCliente.Table = DSCliente.Tables("Clientes")
            'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
            cmbCliente.DataSource = DVCliente
            ComboBox1.DataSource = DVCliente

            '//cmbCardCode.DisplayMember = "CardName";
            cmbCliente.DisplayMember = "CardName"
            cmbCliente.ValueMember = "CardCode"

            ComboBox1.DisplayMember = "CardCode"
            ComboBox1.ValueMember = "CardName"
            '//CIERRA LA CONEXION
            conexion_uni.Close()
            'COLOCA EN NADA EL COMBO
            cmbCliente.SelectedIndex = -1
        Catch ex As Exception
            'SOLO SI SE OCUPA
            MsgBox("ERROR:" + ex.ToString)
            '//CIERRA LA CONEXION
            conexion_uni.Close()
        End Try
    End Sub
    Sub EstiloVisita_Agente()
        Dim style As New DataGridViewCellStyle
        style.Font = New Font(dgvDetalle.Font, FontStyle.Bold)
        With Me.dgvDetalle
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Visitas
            .Columns("Visitas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("Visitas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            .Columns("Visitas").DefaultCellStyle = style
            .Columns("Visitas").HeaderText = "Visitas"
            .Columns("Visitas").Width = 50
            .Columns("Visitas").ReadOnly = True
            'Nombre_Marketing,
            .Columns("Nombre_Marketing").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Nombre_Marketing").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Nombre_Marketing").HeaderText = " Personal Marketing"
            .Columns("Nombre_Marketing").Width = 100
            .Columns("Nombre_Marketing").ReadOnly = True
            'Fecha_Visita
            .Columns("Fecha_Visita").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Fecha_Visita").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Fecha_Visita").HeaderText = "Fecha Visita"
            .Columns("Fecha_Visita").Width = 75
            .Columns("Fecha_Visita").ReadOnly = True
            'Agente
            .Columns("Agente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Agente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Agente").HeaderText = "Agente"
            .Columns("Agente").Width = 130
            .Columns("Agente").ReadOnly = True
            'ID_Cliente
            .Columns("ID_Cliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("ID_Cliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("ID_Cliente").HeaderText = "ID_Cliente"
            .Columns("ID_Cliente").Width = 65
            .Columns("ID_Cliente").ReadOnly = True
            'Cliente
            .Columns("Cliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Cliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cliente").HeaderText = "Cliente"
            .Columns("Cliente").Width = 100
            .Columns("Cliente").ReadOnly = True
            'Fecha_Ultima 
            .Columns("Fecha_Ultima").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Fecha_Ultima").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Fecha_Ultima").HeaderText = "Fecha Ultima"
            .Columns("Fecha_Ultima").Width = 70
            .Columns("Fecha_Ultima").ReadOnly = True
            'Localidad
            .Columns("Localidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Localidad").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Localidad").HeaderText = "Localidad"
            .Columns("Localidad").Width = 140
            .Columns("Localidad").ReadOnly = True
            'Comentarios 
            .Columns("Comentarios").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comentarios").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Comentarios").HeaderText = "Comentarios"
            .Columns("Comentarios").Width = 100
            .Columns("Comentarios").ReadOnly = True
            'Ubicacion 
            .Columns("Ubicacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Ubicacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Ubicacion").HeaderText = "Ubicacion"
            .Columns("Ubicacion").Width = 100
            .Columns("Ubicacion").ReadOnly = True

            'Ubicacion 
            .Columns("Pedido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Pedido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Pedido").HeaderText = "Pedido"
            .Columns("Pedido").Width = 140
            .Columns("Pedido").ReadOnly = True
            'Ubicacion 
            .Columns("Cobranza").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Cobranza").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cobranza").HeaderText = "Cobranza"
            .Columns("Cobranza").Width = 140
            .Columns("Cobranza").ReadOnly = True
            'Ubicacion 
            .Columns("Otro").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Otro").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Otro").HeaderText = "Otro"
            .Columns("Otro").Width = 140
            .Columns("Otro").ReadOnly = True
            'Hora
            .Columns("Hora_recibida").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Hora_recibida").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Hora_recibida").HeaderText = "Hora recibida"
            .Columns("Hora_recibida").Width = 60
            .Columns("Hora_recibida").ReadOnly = True
        End With
    End Sub
    Sub EstiloVisita_AgenteSu()
        Dim style As New DataGridViewCellStyle
        style.Font = New Font(dgvDetalle.Font, FontStyle.Bold)
        With Me.DgvRegistroCom
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Visitas
            .Columns("Visitas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("Visitas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            .Columns("Visitas").DefaultCellStyle = style
            .Columns("Visitas").HeaderText = "Visitas"
            .Columns("Visitas").Width = 50
            .Columns("Visitas").ReadOnly = True
            'Nombre_Marketing,
            .Columns("Nombre_Marketing").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Nombre_Marketing").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Nombre_Marketing").HeaderText = " Personal Marketing"
            .Columns("Nombre_Marketing").Width = 100
            .Columns("Nombre_Marketing").ReadOnly = True
            'Fecha_Visita
            .Columns("Fecha_Visita").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Fecha_Visita").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Fecha_Visita").HeaderText = "Fecha Visita"
            .Columns("Fecha_Visita").Width = 75
            .Columns("Fecha_Visita").ReadOnly = True
            'Agente
            .Columns("Agente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Agente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Agente").HeaderText = "Agente"
            .Columns("Agente").Width = 130
            .Columns("Agente").ReadOnly = True
            'ID_Cliente
            .Columns("ID_Cliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("ID_Cliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("ID_Cliente").HeaderText = "ID_Cliente"
            .Columns("ID_Cliente").Width = 65
            .Columns("ID_Cliente").ReadOnly = True
            'Cliente
            .Columns("Cliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Cliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cliente").HeaderText = "Cliente"
            .Columns("Cliente").Width = 100
            .Columns("Cliente").ReadOnly = True
            'Fecha_Ultima 
            .Columns("Fecha_Ultima").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Fecha_Ultima").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Fecha_Ultima").HeaderText = "Fecha Ultima"
            .Columns("Fecha_Ultima").Width = 70
            .Columns("Fecha_Ultima").ReadOnly = True
            'Localidad
            .Columns("Localidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Localidad").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Localidad").HeaderText = "Localidad"
            .Columns("Localidad").Width = 140
            .Columns("Localidad").ReadOnly = True
            'Comentarios 
            .Columns("Comentarios").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comentarios").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Comentarios").HeaderText = "Comentarios"
            .Columns("Comentarios").Width = 100
            .Columns("Comentarios").ReadOnly = True
            'Ubicacion 
            .Columns("Ubicacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Ubicacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Ubicacion").HeaderText = "Ubicacion"
            .Columns("Ubicacion").Width = 100
            .Columns("Ubicacion").ReadOnly = True

            'Ubicacion 
            .Columns("Pedido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Pedido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Pedido").HeaderText = "Pedido"
            .Columns("Pedido").Width = 140
            .Columns("Pedido").ReadOnly = True
            'Ubicacion 
            .Columns("Cobranza").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Cobranza").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cobranza").HeaderText = "Cobranza"
            .Columns("Cobranza").Width = 140
            .Columns("Cobranza").ReadOnly = True
            'Ubicacion 
            .Columns("Otro").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Otro").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Otro").HeaderText = "Otro"
            .Columns("Otro").Width = 140
            .Columns("Otro").ReadOnly = True
            'Hora
            .Columns("Hora_recibida").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Hora_recibida").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Hora_recibida").HeaderText = "Hora recibida"
            .Columns("Hora_recibida").Width = 60
            .Columns("Hora_recibida").ReadOnly = True
        End With
    End Sub
    Sub acomodarColumnas()
        dgvDetalle.Columns("Visitas").DisplayIndex = 0
        dgvDetalle.Columns("Nombre_Marketing").DisplayIndex = 1
        dgvDetalle.Columns("Fecha_Visita").DisplayIndex = 2
        dgvDetalle.Columns("Hora_recibida").DisplayIndex = 3
        dgvDetalle.Columns("Agente").DisplayIndex = 4
        dgvDetalle.Columns("ID_Cliente").DisplayIndex = 5
        dgvDetalle.Columns("Cliente").DisplayIndex = 6
        dgvDetalle.Columns("Fecha_Ultima").DisplayIndex = 7
        dgvDetalle.Columns("Localidad").DisplayIndex = 8
        dgvDetalle.Columns("Pedido").DisplayIndex = 9
        dgvDetalle.Columns("Cobranza").DisplayIndex = 10
        dgvDetalle.Columns("Otro").DisplayIndex = 11
        dgvDetalle.Columns("Comentarios").DisplayIndex = 12
        dgvDetalle.Columns("Ubicacion").DisplayIndex = 13
    End Sub
    Sub acomodarColumnasSup()
        If DgvRegistroCom.Rows.Count = 0 Then
        Else
            DgvRegistroCom.Columns("Visitas").DisplayIndex = 0
            DgvRegistroCom.Columns("Nombre_Marketing").DisplayIndex = 1
            DgvRegistroCom.Columns("Fecha_Visita").DisplayIndex = 2
            DgvRegistroCom.Columns("Hora_recibida").DisplayIndex = 3
            DgvRegistroCom.Columns("Agente").DisplayIndex = 4
            DgvRegistroCom.Columns("ID_Cliente").DisplayIndex = 5
            DgvRegistroCom.Columns("Cliente").DisplayIndex = 6
            DgvRegistroCom.Columns("Fecha_Ultima").DisplayIndex = 7
            DgvRegistroCom.Columns("Localidad").DisplayIndex = 8
            DgvRegistroCom.Columns("Pedido").DisplayIndex = 9
            DgvRegistroCom.Columns("Cobranza").DisplayIndex = 10
            DgvRegistroCom.Columns("Otro").DisplayIndex = 11
            DgvRegistroCom.Columns("Comentarios").DisplayIndex = 12
            DgvRegistroCom.Columns("Ubicacion").DisplayIndex = 13
        End If
    End Sub

    'METODO QUE  TODOS LOS CAMPOS DESPUES DE AGREGAR EL REGISTRO
    Sub LimpiaDatos()
        txtCardCode.Text = ""
        txtComentario.Text = ""
        txtLocalidad.Text = ""
        txtUbicacion.Text = ""
        cbxCobranza.Checked = False
        cbxOtro.Checked = False
        cbxPedido.Checked = False
        'MANDA A LLAMAR EL METODO DE BUSCAR USUARIO DE MARKETING
        BuscaUsuario()
        'MANDA A LLAMAR EL METODO DE BUSCAR USUARIO DE AGENTE
        BuscaAgente()
        'MANDA A LLAMAR EL METODO DE BUSCAR CLIENTE
        BuscaCliente()
    End Sub
    'METODO QUE LISTA LOS REGISTROS DEL DIA
    Sub LLenaGrid()
        'VARIABLE DE CONSULTA
        Dim SQLConsultaL As String
        Try
            If dgvDetalle.RowCount > 0 Then
                'dgvDetalle.Columns().Remove("Pedido")
                'dgvDetalle.Columns().Remove("Cobranza")
                'dgvDetalle.Columns().Remove("Otro")
            End If
            '//ABE LA CONEXION
            conexion_uni.Open()
            'VALIDA SI SE MUESTRAN TODOS LOS USUARIOS O NO DEPENDIENDO SI ES MANAGER
            'If UsrTPM = "MANAGER" Then
            SQLConsultaL = "SELECT ROW_NUMBER() OVER(ORDER BY T0.Fecha_Visita ASC) AS 'Visitas', T0.Nombre_Marketing, T0.Fecha_Visita,T0.Hora_recibida, T0.Agente, T0.ID_Cliente, T0.Cliente, T1.Fecha_Ultima, T0.Localidad, T0.Pedido, T0.Cobranza, T0.Otro, T0.Comentarios, T0.Ubicacion "
            SQLConsultaL += "FROM TPM.dbo.Visita_Agente T0 INNER JOIN TPM.dbo.Visita_Cliente T1 ON T0.ID_Cliente = T1.ID_Cliente "
            SQLConsultaL += "WHERE T0.Fecha_Visita >= '" + dtpFecha_Visita.Value.ToString("yyyy-MM-dd") + "' "
            SQLConsultaL += "AND T0.Usuario_Marketing = '" + cmbAgente_Marketing.SelectedValue.ToString + "' "
            SQLConsultaL += "AND T0.ID_Agente = '" + cmbAgente_Ventas.SelectedValue.ToString + "' "
            'CREA EL ADAPTER
            Dim DADatos As SqlDataAdapter = New SqlDataAdapter(SQLConsultaL, conexion_uni)
            '//INSTACIA EL DATASET Y DATAVIEW
            Dim DSDatos As DataSet = New DataSet()
            '//ASIGNA AL DATASET
            DADatos.Fill(DSDatos, "Datos")
            '//ASIGNAN AL DATAVIEW
            DVDatos.Table = DSDatos.Tables("Datos")
            'VALIDA SI EL DATATABLE TRAE DATOS
            If (DVDatos.Count > 0) Then
                'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
                dgvDetalle.DataSource = Nothing
                'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
                dgvDetalle.DataSource = DVDatos
                'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
                EstiloVisita_Agente()
                'ELIMINA LA COLUMNA PEDIDO DEL DATA GRID TRAIDA DEL DATATABLE
                dgvDetalle.Columns().Remove("Pedido")
                C1.DataPropertyName = "Pedido" 'COLOCA LA PROPIEDAD
                C1.HeaderText = "Pedido" 'COLOCA EL ENCABEZADO
                C1.Name = "Pedido" 'COLOCA EL NOMBRE DE LA COLUMNA
                C1.SortMode = DataGridViewColumnSortMode.Automatic 'DEFINE EL ORDENADO DE LA COLUMNA
                C1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
                C1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular) 'FORMATO DE LETRA
                C1.Width = 50 'ANCHO DE LA CELDA
                C1.ReadOnly = True 'QUE SOLO SEA DE LECTURA
                'COLOCA LA COLUMNA DE PEDIDO DE MANERA CHECKBOX, EL 8 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
                'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
                dgvDetalle.Columns.Insert(9, C1)
                '----
                'ELIMINA LA COLUMNA IMPRIMIR DEL DATA GRID TRAIDA DEL DATATABLE
                dgvDetalle.Columns().Remove("Cobranza")
                C2.DataPropertyName = "Cobranza" 'COLOCA LA PROPIEDAD
                C2.HeaderText = "Cobranza" 'COLOCA EL ENCABEZADO
                C2.Name = "Cobranza" 'COLOCA EL NOMBRE DE LA COLUMNA
                C2.SortMode = DataGridViewColumnSortMode.Automatic
                C2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
                C2.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular) 'FORMATO DE LETRA
                C2.Width = 60 'ANCHO DE LA CELDA
                C2.ReadOnly = True 'QUE SOLO SEA DE LECTURA
                'COLOCA LA COLUMNA DE COBRANZA DE MANERA BOTON, EL 9 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
                'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
                dgvDetalle.Columns.Insert(10, C2)
                '-----
                'ELIMINA LA COLUMNA IMPRIMIR DEL DATA GRID TRAIDA DEL DATATABLE
                dgvDetalle.Columns().Remove("Otro")
                C3.DataPropertyName = "Otro" 'COLOCA LA PROPIEDAD
                C3.HeaderText = "Otro" 'COLOCA EL ENCABEZADO
                C3.Name = "Otro" 'COLOCA EL NOMBRE DE LA COLUMNA
                C3.SortMode = DataGridViewColumnSortMode.Automatic
                C3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
                C3.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular) 'FORMATO DE LETRA
                C3.Width = 50 'ANCHO DE LA CELDA
                C3.ReadOnly = True 'QUE SOLO SEA DE LECTURA
                'COLOCA LA COLUMNA DE OTRO DE MANERA BOTON, EL 9 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
                'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
                dgvDetalle.Columns.Insert(11, C3)
            End If
            'FIN VALIDA SI EL DATATABLE TRAE DATOS
            '//CIERRA LA CONEXION
            conexion_uni.Close()
        Catch ex As Exception
            'SOLO SI SE OCUPA
            MsgBox("Error:" + ex.ToString)
            '//CIERRA LA CONEXION
            conexion_uni.Close()
        End Try
    End Sub

    Sub LLenaGridSupervisor()
        'VARIABLE DE CONSULTA
        Dim SQLConsultaL As String
        Try
            If DgvRegistroCom.RowCount > 0 Then
                'dgvDetalle.Columns().Remove("Pedido")
                'dgvDetalle.Columns().Remove("Cobranza")
                'dgvDetalle.Columns().Remove("Otro")
            End If
            '//ABE LA CONEXION
            conexion_uni.Open()
            'VALIDA SI SE MUESTRAN TODOS LOS USUARIOS O NO DEPENDIENDO SI ES MANAGER
            'If UsrTPM = "MANAGER" Then
            SQLConsultaL = "SELECT ROW_NUMBER() OVER(ORDER BY T0.Fecha_Visita ASC) AS 'Visitas', T0.Nombre_Marketing, T0.Fecha_Visita,T0.Hora_recibida, T0.Agente, T0.ID_Cliente, T0.Cliente, T1.Fecha_Ultima, T0.Localidad, T0.Pedido, T0.Cobranza, T0.Otro, T0.Comentarios, T0.Ubicacion "
            SQLConsultaL += "FROM TPM.dbo.Visita_Agente T0 INNER JOIN TPM.dbo.Visita_Cliente T1 ON T0.ID_Cliente = T1.ID_Cliente "
            SQLConsultaL += "WHERE T0.Fecha_Visita between'" + DTPInicio.Value.ToString("yyyy-MM-dd") + "' and  '" + DTPTermino.Value.ToString("yyyy-MM-dd") + "' "
            'If cmbAgente_Marketing.SelectedValue.ToString = "MANAGER" Then
            'Else
            If CBAgenteM.SelectedValue.ToString = "MANAGER" Or CBAgenteM.SelectedValue.ToString = "CONTAB1" Then

            Else
                SQLConsultaL += "AND T0.Usuario_Marketing = '" + CBAgenteM.SelectedValue.ToString + "' "
            End If

            SQLConsultaL += "AND T0.ID_Agente = '" + CBAgenteV.SelectedValue.ToString + "' "
            'End If
            'CREA EL ADAPTER
            Dim DADatos As SqlDataAdapter = New SqlDataAdapter(SQLConsultaL, conexion_uni)
            '//INSTACIA EL DATASET Y DATAVIEW
            Dim DSDatos As DataSet = New DataSet()
            '//ASIGNA AL DATASET
            DADatos.Fill(DSDatos, "Datos")
            '//ASIGNAN AL DATAVIEW
            DVDatos.Table = DSDatos.Tables("Datos")
            'VALIDA SI EL DATATABLE TRAE DATOS
            If (DVDatos.Count > 0) Then
                'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
                DgvRegistroCom.DataSource = Nothing
                'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
                DgvRegistroCom.DataSource = DVDatos
                'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
                EstiloVisita_AgenteSu()

                'ELIMINA LA COLUMNA PEDIDO DEL DATA GRID TRAIDA DEL DATATABLE
                DgvRegistroCom.Columns().Remove("Pedido")

                C1.DataPropertyName = "Pedido" 'COLOCA LA PROPIEDAD
                C1.HeaderText = "Pedido" 'COLOCA EL ENCABEZADO
                C1.Name = "Pedido" 'COLOCA EL NOMBRE DE LA COLUMNA
                C1.SortMode = DataGridViewColumnSortMode.Automatic 'DEFINE EL ORDENADO DE LA COLUMNA
                C1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
                C1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular) 'FORMATO DE LETRA
                C1.Width = 50 'ANCHO DE LA CELDA
                C1.ReadOnly = True 'QUE SOLO SEA DE LECTURA
                'COLOCA LA COLUMNA DE PEDIDO DE MANERA CHECKBOX, EL 8 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
                'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
                DgvRegistroCom.Columns.Insert(9, C1)

                '----
                'ELIMINA LA COLUMNA IMPRIMIR DEL DATA GRID TRAIDA DEL DATATABLE
                DgvRegistroCom.Columns().Remove("Cobranza")

                C2.DataPropertyName = "Cobranza" 'COLOCA LA PROPIEDAD
                C2.HeaderText = "Cobranza" 'COLOCA EL ENCABEZADO
                C2.Name = "Cobranza" 'COLOCA EL NOMBRE DE LA COLUMNA
                C2.SortMode = DataGridViewColumnSortMode.Automatic
                C2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
                C2.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular) 'FORMATO DE LETRA
                C2.Width = 60 'ANCHO DE LA CELDA
                C2.ReadOnly = True 'QUE SOLO SEA DE LECTURA
                'COLOCA LA COLUMNA DE COBRANZA DE MANERA BOTON, EL 9 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
                'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
                DgvRegistroCom.Columns.Insert(10, C2)

                '-----
                'ELIMINA LA COLUMNA IMPRIMIR DEL DATA GRID TRAIDA DEL DATATABLE
                DgvRegistroCom.Columns().Remove("Otro")

                C3.DataPropertyName = "Otro" 'COLOCA LA PROPIEDAD
                C3.HeaderText = "Otro" 'COLOCA EL ENCABEZADO
                C3.Name = "Otro" 'COLOCA EL NOMBRE DE LA COLUMNA
                C3.SortMode = DataGridViewColumnSortMode.Automatic
                C3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
                C3.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular) 'FORMATO DE LETRA
                C3.Width = 50 'ANCHO DE LA CELDA
                C3.ReadOnly = True 'QUE SOLO SEA DE LECTURA
                'COLOCA LA COLUMNA DE OTRO DE MANERA BOTON, EL 9 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
                'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
                DgvRegistroCom.Columns.Insert(11, C3)


            End If
            'FIN VALIDA SI EL DATATABLE TRAE DATOS

            '//CIERRA LA CONEXION
            conexion_uni.Close()
        Catch ex As Exception
            'SOLO SI SE OCUPA
            MsgBox("Error:" + ex.ToString)
            '//CIERRA LA CONEXION
            conexion_uni.Close()
        End Try
    End Sub
#End Region
#Region "BOTONES"

    'BTON PARA AGREGAR EL REGISTRO
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        'VARIABLE DE CONSULTA
        Dim SQLInsert As String
        Dim SQLCliente As String
        Dim Dato_ok As Boolean

        'VARIABLE DE CONVERSION DE FECHA
        Dim FechaVisita As String

        'CONVIERTE LA FECHA AL FORMATO CORRECTO DE SQL
        FechaVisita = dtpFecha_Visita.Value.ToString("yyyy-MM-dd")

        'VALIDACIONES DATO POR DATO PARA VER QUE NO FALTE UN VALOR CRITICO

        'VALIDA QUE NO VAYA EL AGENTE DE MARKETING VACIO
        If cmbAgente_Marketing.SelectedIndex = -1 Then
            MsgBox("Favor de Seleccionar un Agente de Marketing", MsgBoxStyle.Exclamation, "Captura de dato")
            cmbAgente_Marketing.Focus()
            Return
        End If

        'VALIDA QUE NO VAYA EL AGENTE DE VENTAS VACIO
        If cmbAgente_Ventas.SelectedIndex = -1 Then
            MsgBox("Favor de Seleccionar un Agente de Ventas", MsgBoxStyle.Exclamation, "Captura de dato")
            cmbAgente_Ventas.Focus()
            Return
        End If

        'VALIDA QUE NO VAYA EL CLIENTE VACIO
        If cmbCliente.SelectedIndex = -1 Then
            MsgBox("Favor de Seleccionar un Cliente", MsgBoxStyle.Exclamation, "Captura de dato")
            cmbCliente.Focus()
            Return
        End If

        If txtCardCode.Text = "" Then
            MsgBox("Favor de Seleccionar un Cliente", MsgBoxStyle.Exclamation, "Captura de dato")
            cmbCliente.Focus()
            Return
        End If

        'VALIDA QUE EL MOTIVO NO VAYA VACIO
        If cbxPedido.Checked = False And cbxCobranza.Checked = False And cbxOtro.Checked = False Then
            MsgBox("Favor de marcar por lo menos un motivo.", MsgBoxStyle.Exclamation, "Captura de dato")
            cbxPedido.Focus()
            Return
        End If

        If txtUbicacion.Text = "" Then
            MsgBox("Favor de poner la ubicacion Cliente", MsgBoxStyle.Exclamation, "Captura de dato")
            txtUbicacion.Focus()
            Return
        End If

        'CODIGO PARA VALIDAR SI EL CLIENTE YA ESTA EN FECHA DE ULTIMA VISITA
        Try
            'USA LA CONEXION
            Using cn As SqlConnection = New SqlConnection(conexion_universal.CadenaSQL)
                'APERTURA LA CONEXION
                cn.Open()
                'ALAMACENA LA CONSULTA
                SQLCliente = "SELECT ID_Cliente FROM TPM.dbo.Visita_Cliente "
                SQLCliente += "WHERE ID_Cliente = '" + cmbCliente.SelectedValue.ToString + "' "
                'CREA EL COMMAND
                Dim cmd As SqlCommand = New SqlCommand(SQLCliente, cn)
                'EJECUTA LA CONSULTA
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                'RECORRE LA CONSULTA
                If (dr.Read()) Then
                    'ALMACENA SI YA EXISTE O NO
                    Dato_ok = True
                End If
                'CIERRA LA CONEXION
                cn.Close()
            End Using
        Catch ex As Exception
            MsgBox("Error en busqueda de existencia del cliente: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de conexion o consulta.")
            Return
        End Try

        Try

            'USA LA CONEXION
            Using cn As SqlConnection = New SqlConnection(conexion_universal.CadenaSQL)
                'APERTURA LA CONEXION
                cn.Open()

                'ALAMACENA LA CONSULTA
                SQLInsert = "INSERT INTO TPM.dbo.Visita_Agente (Fecha_Visita, ID_Agente, Agente, ID_Cliente, Cliente, Localidad, "
                SQLInsert += "Pedido, Cobranza, Otro, Comentarios, Ubicacion, Usuario_Marketing, Nombre_Marketing,Hora_recibida) VALUES ( "
                SQLInsert += "'" + FechaVisita + "', '" + cmbAgente_Ventas.SelectedValue.ToString + "', '" + cmbAgente_Ventas.Text + "', '" + cmbCliente.SelectedValue.ToString + "', '" + cmbCliente.Text + "', "
                SQLInsert += "'" + txtLocalidad.Text + "', '" + cbxPedido.Checked.ToString + "', '" + cbxCobranza.Checked.ToString + "', '" + cbxOtro.Checked.ToString + "', "
                SQLInsert += "'" + txtComentario.Text + "', '" + txtUbicacion.Text + "', '" + cmbAgente_Marketing.SelectedValue.ToString + "', '" + cmbAgente_Marketing.Text + "' ,  '" + DTPHORA.Value.ToString("HH:mm") + "' "
                SQLInsert += ") "
                'VALIDA SI INSERTA O ACTUALIZA LA FECHA ULTIMA DE VISITA
                If Dato_ok = True Then
                    SQLInsert += "UPDATE TPM.dbo.Visita_Cliente SET Fecha_Ultima = '" + FechaVisita + "' "
                    SQLInsert += "WHERE ID_Cliente = '" + cmbCliente.SelectedValue + "' "
                Else
                    SQLInsert += "INSERT INTO TPM.dbo.Visita_Cliente (ID_Cliente, Cliente, Fecha_Ultima) VALUES( "
                    SQLInsert += "'" + cmbCliente.SelectedValue.ToString + "', '" + cmbCliente.Text + "', '" + FechaVisita + "' "
                    SQLInsert += ") "
                End If

                'CREA EL COMMAND
                Dim cmd As SqlCommand = New SqlCommand(SQLInsert, cn)
                'EJECUTA LA CONSULTA
                Dim dr As SqlDataReader = cmd.ExecuteScalar

                'CIERRA LA CONEXION
                cn.Close()

                'MANDA MENSAJE DE OPERACION EXITOS
                MsgBox("Registro de visita exitosa.", MsgBoxStyle.Information, "Finalizado")

                'MANDA A LLAMAR EL METODO DE LIMPIADO
                LimpiaDatos()

                'MANDA A LLAMAR EL LLENADO DE 
                LLenaGrid()

                acomodarColumnas()






            End Using

        Catch ex As Exception
            MsgBox("Error en registro de cliente: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de conexion o consulta.")
            Return
        End Try

    End Sub
#End Region
#Region "EVENTOS"

    'EVENTO DE CAPTURAR EL CAMBIO DE PARAMETRO
    Private Sub cmbAgente_Marketing_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbAgente_Marketing.SelectionChangeCommitted
        'MANDA A LLAMAR EL METODO DE BUSCAR AGENTE
        BuscaAgente()
        'MANDA A LLAMAR EL METODO DE LLENADO DE GRID
        LLenaGrid()
    End Sub

    Private Sub cmbCliente_KeyUp(sender As Object, e As KeyEventArgs) Handles cmbCliente.KeyUp
        '// --------- AUTOCOMPLETADO DEL COMBO BOX AL BUSCAR POR CODIGO Y NOMBRE DEL CLIENTE
        Try
            '//VALIDA QUE SEA DE LA "A" A LA "Z"
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete) Then
                '//ALMACENA LO QUE TRAE EL COMBO
                Str = cmbCliente.Text
                '//VALIDA SI VIENE VACIO EL COMBO
                If (Str.CompareTo(String.Empty) = 0) Then
                    '//NO COLOCA NADA
                    DVCliente.RowFilter = String.Empty
                Else
                    '//COMPARA CON EL DATASED EL VALOR QUE TRAIGA EN EL COMBO
                    'Dim RFCliente As String = String.Concat("Cliente like '%", cmbCliente.Text, "%' OR Cliente like '%", cmbCliente.Text, "%' ")
                    Dim RFCliente As String = String.Concat("CardName like '%", cmbCliente.Text, "%' ")
                    DVCliente.RowFilter = RFCliente
                End If
                cmbCliente.Text = ""
                cmbCliente.Text = Str
                cmbCliente.SelectionStart = Str.Length
                cmbCliente.SelectedIndex = -1
                cmbCliente.DroppedDown = True
                cmbCliente.SelectedIndex = -1
                cmbCliente.Text = ""
                cmbCliente.Text = Str
                cmbCliente.SelectionStart = Str.Length
            End If
        Catch ex As Exception
            MessageBox.Show("Error encontrado en elemento del cliente: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmbCliente_DropDown(sender As Object, e As EventArgs) Handles cmbCliente.DropDown
        Cursor = Cursors.Arrow
        cmbCliente.Text = Str
    End Sub

    Private Sub cmbCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCliente.SelectedIndexChanged
        'VARIABLE DE CONSULTA
        Dim SQLClienteBus As String
        'USA LA CONEXION
        Using cn As SqlConnection = New SqlConnection(conexion_universal.CadenaSQL)
            'APERTURA LA CONEXION
            cn.Open()
            'ALAMACENA LA CONSULTA
            SQLClienteBus = "SELECT T0.CardCode, IIF(T1.County IS NULL, '-', T1.County) AS County, IIF(T1.State IS NULL, '-', T1.State) AS State "
            SQLClienteBus += "FROM SBO_TPD.dbo.OCRD T0 INNER JOIN SBO_TPD.dbo.CRD1 T1 ON T0.CardCode = T1.CardCode "
            SQLClienteBus += "WHERE T0.CardCode =  @id AND T1.Address = 'ENTREGA' "
            'CREA EL COMMAND
            Dim cmd As SqlCommand = New SqlCommand(SQLClienteBus, cn)
            'PASA EL PARAMETRO A LA CONSULTA Y CONVIERTE A STRING EL VALOR DEL CAMPO
            cmd.Parameters.AddWithValue("@id", Convert.ToString(cmbCliente.SelectedValue))
            'EJECUTA LA CONSULTA
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            'RECORRE LA CONSULTA
            If (dr.Read()) Then
                'MUESTRA EL CARDCODE EN EL TEXBOX
                txtCardCode.Text = Convert.ToString(dr.Item("CardCode"))
                'MUESTRA EL CONDADO Y EL ESTADO DEL CLIENTE EN EL TEXTBOX
                txtLocalidad.Text = Convert.ToString(dr.Item("County")) + ", " + Convert.ToString(dr.Item("State"))
            End If
        End Using

    End Sub

    Private Sub cmbAgente_Ventas_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbAgente_Ventas.SelectionChangeCommitted

        'MANDA A LLAMAR EL LLENADO DEL GRID
        LLenaGrid()

    End Sub

    Private Sub btnVer_Click(sender As Object, e As EventArgs) Handles btnVer.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        LLenaGridSupervisor()
        acomodarColumnasSup()

    End Sub

    Private Sub CBAgenteM_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBAgenteM.SelectionChangeCommitted
        'MANDA A LLAMAR EL METODO DE BUSCAR AGENTE
        BuscaAgenteSU()
    End Sub

    Private Sub CBAgenteV_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBAgenteV.SelectionChangeCommitted

    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'Creamos las variables
        Dim exApp As New Microsoft.Office.Interop.Excel.Application
        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
        Try
            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()
            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DgvRegistroCom.ColumnCount()
            Dim NRow As Integer = DgvRegistroCom.RowCount
            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(1, i) = DgvRegistroCom.Columns(i - 1).Name.ToString

                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next
            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    exHoja.Cells.Item(Fila + 2, Col + 1) = DgvRegistroCom.Rows(Fila).Cells(Col).Value
                Next
            Next
            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
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
        End Try
    End Sub

    Private Sub lblVisista_Click(sender As Object, e As EventArgs) Handles lblVisista.Click

    End Sub

    Private Sub tpUsuario_Click(sender As Object, e As EventArgs) Handles tpUsuario.Click

    End Sub

    Private Sub txtCardCode_TextChanged(sender As Object, e As EventArgs) Handles txtCardCode.TextChanged
        'VARIABLE DE CONSULTA

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'VARIABLE DE CONSULTA
        Dim SQLClienteBus As String
        'USA LA CONEXION
        Using cn As SqlConnection = New SqlConnection(conexion_universal.CadenaSQL)
            'APERTURA LA CONEXION
            cn.Open()
            'ALAMACENA LA CONSULTA
            SQLClienteBus = "SELECT T0.CardCode, IIF(T1.County IS NULL, '-', T1.County) AS County, IIF(T1.State IS NULL, '-', T1.State) AS State "
            SQLClienteBus += "FROM SBO_TPD.dbo.OCRD T0 INNER JOIN SBO_TPD.dbo.CRD1 T1 ON T0.CardCode = T1.CardCode "
            SQLClienteBus += "WHERE T0.CardName =  @id AND T1.Address = 'ENTREGA' "
            'CREA EL COMMAND
            Dim cmd As SqlCommand = New SqlCommand(SQLClienteBus, cn)
            'PASA EL PARAMETRO A LA CONSULTA Y CONVIERTE A STRING EL VALOR DEL CAMPO
            cmd.Parameters.AddWithValue("@id", Convert.ToString(cmbCliente.SelectedValue))
            'EJECUTA LA CONSULTA
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            'RECORRE LA CONSULTA
            If (dr.Read()) Then
                'MUESTRA EL CARDCODE EN EL TEXBOX
                cmbCliente.Text = Convert.ToString(dr.Item("CardCode"))
                'MUESTRA EL CONDADO Y EL ESTADO DEL CLIENTE EN EL TEXTBOX
                txtLocalidad.Text = Convert.ToString(dr.Item("County")) + ", " + Convert.ToString(dr.Item("State"))

            End If
        End Using

    End Sub

    Private Sub ComboBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles ComboBox1.KeyUp
        '// --------- AUTOCOMPLETADO DEL COMBO BOX AL BUSCAR POR CODIGO Y NOMBRE DEL CLIENTE
        Try
            '//VALIDA QUE SEA DE LA "A" A LA "Z"
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete) Then
                '//ALMACENA LO QUE TRAE EL COMBO
                Str = ComboBox1.Text
                '//VALIDA SI VIENE VACIO EL COMBO
                If (Str.CompareTo(String.Empty) = 0) Then
                    '//NO COLOCA NADA
                    DVCliente.RowFilter = String.Empty
                Else
                    '//COMPARA CON EL DATASED EL VALOR QUE TRAIGA EN EL COMBO
                    'Dim RFCliente As String = String.Concat("Cliente like '%", cmbCliente.Text, "%' OR Cliente like '%", cmbCliente.Text, "%' ")
                    Dim RFCliente As String = String.Concat("CardCode like '%", ComboBox1.Text, "%' ")
                    DVCliente.RowFilter = RFCliente
                End If
                ComboBox1.Text = ""
                ComboBox1.Text = Str
                If ComboBox1.Text = "" Then
                    cmbCliente.SelectedIndex = -1
                    txtLocalidad.Text = ""
                End If

                ComboBox1.SelectionStart = Str.Length
                ComboBox1.SelectedIndex = -1
                ComboBox1.DroppedDown = True
                ComboBox1.SelectedIndex = -1
                ComboBox1.Text = ""
                ComboBox1.Text = Str
                ComboBox1.SelectionStart = Str.Length
            End If
        Catch ex As Exception
            MessageBox.Show("Error encontrado en elemento del cliente: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub TbNovisitados_Click(sender As Object, e As EventArgs) Handles TbNovisitados.Click

    End Sub
    Sub llenarNo_Clientes()
        'Obtenemos la fecha del dia actual 
        fecha_ini = DateTimePicker2.Value.ToString("yyyy-MM-dd")
        'Obtenemos la fecha del dia actual 
        fecha_ter = DateTimePicker1.Value.ToString("yyyy-MM-dd")

        'VARIABLE DE CONSULTA
        Dim SQLConsultaNoC As String
        '//ABE LA CONEXION
        conexion_uni.Open()
        'VALIDA SI SE MUESTRAN TODOS LOS USUARIOS O NO DEPENDIENDO SI ES MANAGER
        SQLConsultaNoC = "select Distinct (CardCode) as Codigo,CardName as Cliente,IIf(T2.County is null,'-',T2.County) +' ,- '+ IIF(T2.State1 IS NULL,'-',T2.State1) AS Localidad, (Select top(1) Fecha_Visita  from Visita_Agente TT1 where TT1.ID_Cliente=t1.ID_Cliente  ORDER BY Fecha_Visita Desc) as Fecha_Visita ,  Address ,SlpName as Agente  "
        SQLConsultaNoC += ",T4.Nombre from Visita_Agente T1 RIGHT JOIN  SBO_TPD.dbo.OCRD T2 ON T1.ID_Cliente COLLATE  SQL_Latin1_General_CP850_CI_AS =  T2.CardCode "

        SQLConsultaNoC += "INNER JOIN SBO_TPD.dbo.OSLP T3 ON T2.SlpCode=T3.SlpCode  INNER JOIN Usuarios T4 ON T3.SlpCode=T4.AgteVentas "
        SQLConsultaNoC += "WHERE T2.frozenFor ='N' and (U_BXP_Estatus='03' or U_BXP_Estatus='04' or  U_BXP_Estatus is null ) "
        SQLConsultaNoC += "and (CardCode collate Modern_Spanish_CI_AS  not in (Select ID_Cliente  From Visita_Agente  ) or (Select top(1) Fecha_Visita  from Visita_Agente TT1 where TT1.ID_Cliente= T1.ID_Cliente  ORDER BY Fecha_Visita Desc) not between '" + fecha_ini + "' and '" + fecha_ter + "')     and CardType='C' "

        If CBAgenteVeNO.SelectedItem.ToString = "TODOS" Then
            SQLConsultaNoC += "and T3.SlpCode in (15,17,20,8,42,10,13,41)"
        Else
            SQLConsultaNoC += "and  T3.SlpName='" + CBAgenteVeNO.SelectedItem.ToString + "' "

        End If

        'CREA EL ADAPTER
        Dim DADatos2 As SqlDataAdapter = New SqlDataAdapter(SQLConsultaNoC, conexion_uni)
        '//INSTACIA EL DATASET Y DATAVIEW
        Dim DSDatos2 As DataSet = New DataSet()
        '//ASIGNA AL DATASET
        DADatos2.Fill(DSDatos2, "Datos")
        '//ASIGNAN AL DATAVIEW
        DVDatos2.Table = DSDatos2.Tables("Datos")
        'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
        DataGridView1.DataSource = Nothing
        'ASIGNA EL DATA VIEW AL DATA SOURCE DEL COMBO
        DataGridView1.DataSource = DVDatos2
        'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
        conexion_uni.Close()
        EstiloVisita_AgenteNO()


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        llenarNo_Clientes()
    End Sub
    Sub EstiloVisita_AgenteNO()
        Dim style As New DataGridViewCellStyle
        style.Font = New Font(DataGridView1.Font, FontStyle.Bold)
        With Me.DataGridView1
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            ''Visitas
            '.Columns("#").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("#").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            '.Columns("#").DefaultCellStyle = style
            '.Columns("#").HeaderText = "#"
            '.Columns("#").Width = 50
            '.Columns("#").ReadOnly = True
            'Visitas
            .Columns("Codigo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Codigo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            .Columns("Codigo").DefaultCellStyle = style
            .Columns("Codigo").HeaderText = "Codigo"
            .Columns("Codigo").Width = 80
            .Columns("Codigo").ReadOnly = True
            'Nombre_Marketing,
            .Columns("Cliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Cliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cliente").HeaderText = "Cliente"
            .Columns("Cliente").Width = 250
            .Columns("Cliente").ReadOnly = True
            'Fecha_Visita
            .Columns("Agente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Agente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Agente").HeaderText = "Agente"
            .Columns("Agente").Width = 150
            .Columns("Agente").ReadOnly = True
            'Agente
            .Columns("Localidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Localidad").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Localidad").HeaderText = "Localidad"
            .Columns("Localidad").Width = 150
            .Columns("Localidad").ReadOnly = True
            'Address
            .Columns("Address").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Address").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Address").HeaderText = "Direccion SAP"
            .Columns("Address").Width = 200
            .Columns("Address").ReadOnly = True
            'Address
            .Columns("Nombre").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Nombre").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Nombre").HeaderText = "Agente Marketing"
            .Columns("Nombre").Width = 150
            .Columns("Nombre").ReadOnly = True
            'Address
            .Columns("Fecha_Visita").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("Fecha_Visita").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Fecha_Visita").HeaderText = "Fecha ultima visita"
            .Columns("Fecha_Visita").Width = 80
            .Columns("Fecha_Visita").ReadOnly = True
            .Columns("Fecha_Visita").DefaultCellStyle.Format = "yyyy-MM-dd"
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Creamos las variables
        Dim exApp As New Microsoft.Office.Interop.Excel.Application
        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
        Try
            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()
            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DataGridView1.ColumnCount()
            Dim NRow As Integer = DataGridView1.RowCount
            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(1, i) = DataGridView1.Columns(i - 1).Name.ToString

                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next
            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    exHoja.Cells.Item(Fila + 2, Col + 1) = DataGridView1.Rows(Fila).Cells(Col).Value
                Next
            Next
            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
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
        End Try
    End Sub

    Private Sub DataGridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Try
            Dim NumeroFila As String = (e.RowIndex + 1).ToString 'Obtiene el número de filas
            While NumeroFila.Length < DataGridView1.RowCount.ToString.Length
                NumeroFila = "0" & NumeroFila 'Agrega un cero a los que tienen un dígito menos
            End While
            Dim size As SizeF = e.Graphics.MeasureString(NumeroFila, Me.Font)
            If DataGridView1.RowHeadersWidth < CInt(size.Width + 20) Then
                DataGridView1.RowHeadersWidth = CInt(size.Width + 20)
            End If
            Dim Obj As Brush = SystemBrushes.ControlText
            'Dibuja el número dentro del controltext
            e.Graphics.DrawString(NumeroFila, Me.Font, Obj, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub

    Private Sub tpSurpevisor_Click(sender As Object, e As EventArgs) Handles tpSurpevisor.Click

    End Sub


#End Region
End Class