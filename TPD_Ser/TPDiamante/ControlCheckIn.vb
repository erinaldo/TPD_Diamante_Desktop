
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Public Class ControlCheckIn
    Dim DvLP As New DataView
    Dim DvLP2 As New DataView
    Public DvDetalle As New DataView
    Public DvOrdenes As New DataView
    Public DvOrdDetail As New DataView
    Dim bandera_todos As Boolean = False
    Dim bandera_uno As Boolean = False
    Dim strTemp As String = ""
    Dim strTemp_toCombobox1 As String = ""
    Dim fechafiltroinicio As Date
    Dim fechafiltrofin As Date

    Private Sub ControlCheckIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim dtFecha As Date = DateSerial(Year(Date.Today), Date.Now.Month, 1)
        'Me.DTP1.Value = dtFecha

        Dim cadenaConexion As String = "Server=67.227.237.109;Database=tractop2_TPD-Check;User=tractop2_Sistems;Pwd=S1t10_H0sp3d@nd00;"
        Dim conn As New MySqlConnection(cadenaConexion)
        Dim consulta As String = ""
        Try
            conn.Open()
            If UsrTPM = "RROBLES" Then
                consulta = "Select SlpName as 'Nombre', SlpCode from Agentes where SlpCode = 102 or SlpCode = 20;  "
                consulta &= "select CardCode, CONCAT(CardCode, ' --> ', CardName) as 'Name', SlpCode, 'C' as 'Serie' from Clientes where SlpCode = 102 UNION ALL select CardCode, CONCAT(CardCode, ' --> ', CardName) as 'Name', SlpCode, 'A' as 'Serie' from Clientes where SlpCode = 20 order by SlpCode, CONVERT(SUBSTRING(CardCode,LOCATE('-', CardCode) + 1), UNSIGNED INTEGER) "
            ElseIf UsrTPM = "VVERGARA" Then
                consulta = "Select SlpName as 'Nombre', SlpCode from Agentes where SlpCode = 103; select CardCode, CONCAT(CardCode, ' --> ', CardName) as 'Name', SlpCode, 'C' as 'Serie' from Clientes where SlpCode = 103 order by CONVERT(SUBSTRING(CardCode,LOCATE('-', CardCode) + 1), UNSIGNED INTEGER) "
            ElseIf UsrTPM = "LMARTINEZ" Then
                consulta = "Select SlpName as 'Nombre', SlpCode from Agentes where SlpCode = 101; select CardCode, CONCAT(CardCode, ' --> ', CardName) as 'Name', SlpCode, 'C' as 'Serie' from Clientes where SlpCode = 101 order by CONVERT(SUBSTRING(CardCode,LOCATE('-', CardCode) + 1), UNSIGNED INTEGER) "
            Else
                consulta = "Select SlpName as 'Nombre', SlpCode from Agentes where (SlpCode in (select DISTINCT SlpCode from Clientes) or SlpCode = 104) and SlpCode <> 989 and SlpCode <> 990; select t0.CardCode, CONCAT(t0.CardCode, ' --> ', t0.CardName) as 'Name', t0.SlpCode, t1.Serie from Clientes t0 inner join Agentes t1 on t0.SlpCode = t1.SlpCode order by CONVERT(SUBSTRING(CardCode,LOCATE('-', CardCode) + 1), UNSIGNED INTEGER) "
                'consulta = "Select SlpName as 'Nombre', SlpCode from Agentes where SlpCode in (select DISTINCT SlpCode from Clientes) and SlpCode <> 990; select t0.CardCode, CONCAT(t0.CardCode, ' --> ', t0.CardName) as 'Name', t0.SlpCode, t1.Serie from Clientes t0 inner join Agentes t1 on t0.SlpCode = t1.SlpCode order by CONVERT(SUBSTRING(CardCode,LOCATE('-', CardCode) + 1), UNSIGNED INTEGER) "
            End If
            Dim MySqlAdapater As New MySqlDataAdapter(Consulta, conn)
            Dim Agentes As New DataSet
            MySqlAdapater.Fill(Agentes)

            Dim filaClte As Data.DataRow
            filaClte = Agentes.Tables(0).NewRow
            filaClte("Nombre") = "--Ningun Resultado--"
            filaClte("SlpCode") = "1010"
            Agentes.Tables(0).Rows.Add(filaClte)

            filaClte = Agentes.Tables(0).NewRow
            filaClte("Nombre") = "TODOS (Agentes)"
            filaClte("SlpCode") = "9191"
            'Agentes.Tables(0).Rows.Add(filaClte)
            If UsrTPM <> "RROBLES" And UsrTPM <> "VVERGARA" And UsrTPM <> "LMARTINEZ" Then
                Agentes.Tables(0).Rows.Add(filaClte)
            End If

            filaClte = Agentes.Tables(0).NewRow
            filaClte("Nombre") = "TODOS (Choferes)"
            filaClte("SlpCode") = "9090"
            'Agentes.Tables(0).Rows.Add(filaClte)
            If UsrTPM <> "RROBLES" And UsrTPM <> "VVERGARA" And UsrTPM <> "LMARTINEZ" Then
                Agentes.Tables(0).Rows.Add(filaClte)
            End If

            filaClte = Agentes.Tables(0).NewRow
            filaClte("Nombre") = "TODOS"
            filaClte("SlpCode") = "9999"
            'Agentes.Tables(0).Rows.Add(filaClte)
            If UsrTPM <> "RROBLES" And UsrTPM <> "VVERGARA" And UsrTPM <> "LMARTINEZ" Then
                Agentes.Tables(0).Rows.Add(filaClte)
            End If



            DvLP.Table = Agentes.Tables(0)
            DvLP.RowFilter = "SlpCode <> 1010"
            Me.CmbCliente.DataSource = DvLP
            Me.CmbCliente.DisplayMember = "Nombre"
            Me.CmbCliente.ValueMember = "SlpCode"

            filaClte = Agentes.Tables(1).NewRow
            filaClte("CardCode") = "8888"
            filaClte("Name") = "TODOS"
            filaClte("SlpCode") = "8888"
            filaClte("Serie") = "AACC"
            Agentes.Tables(1).Rows.Add(filaClte)

            filaClte = Agentes.Tables(1).NewRow
            filaClte("CardCode") = "10101"
            filaClte("Name") = "--Ningun Resultado--"
            filaClte("SlpCode") = "10101"
            filaClte("Serie") = "CCAA"
            Agentes.Tables(1).Rows.Add(filaClte)

            DvLP2.Table = Agentes.Tables(1)
            DvLP2.RowFilter = "SlpCode <> 10101"
            Me.ComboBox1.DataSource = DvLP2
            Me.ComboBox1.DisplayMember = "Name"
            Me.ComboBox1.ValueMember = "CardCode"

            If UsrTPM <> "RROBLES" And UsrTPM <> "VVERGARA" And UsrTPM <> "LMARTINEZ" Then
                Me.CmbCliente.SelectedValue = "9999"
                Me.ComboBox1.SelectedValue = "8888"
            Else
                Me.CmbCliente.SelectedIndex = 0
                Me.ComboBox1.SelectedValue = "8888"
            End If

            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Consulta_Click(sender As Object, e As EventArgs) Handles Consulta.Click
        Dim consulta As String = ""

        If CmbCliente.SelectedIndex = -1 Then 'no selecciona ningun agente'
            If ComboBox1.SelectedIndex <> -1 Then
                If ComboBox1.SelectedValue.ToString = "8888" Then
                    bandera_todos = True
                    bandera_uno = False
                    'MsgBox("estas seleccionando todos los clientes sin haber elegido nada en los agentes")
                    consulta &= "select distinct t1.SlpCode, -1 as 'id', '' as 'CardCode', t1.SlpName as 'Nombre', '' as 'ordenFecha', '' as 'Fecha', '' as 'Hora Comienzo', '' as 'Hora Fin', '' as 'Duracion de Visita', "
                    consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', '' as 'Pedido', '' as 'Valor', '' as 'Comentario', '' as 'Numero de Visita', -1 as 'Prioridad', '' as 'Veces' "
                    consulta &= "from Visita t0 inner join Agentes t1 on t0.SlpCode = t1.SlpCode "
                    consulta &= "where cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
                    consulta &= "union all "
                    consulta &= "select t0.SlpCode, t0.id, '' as 'CardCode', 'Inicio de Ruta' as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                    consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo', '' as 'Hora Fin', '' as 'Duracion de Visita',  "
                    consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', '' as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                    consulta &= "from Visita t0 where t0.Prioridad = 1 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
                    consulta &= "union ALL "
                    consulta &= "select t0.SlpCode, t0.id, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                    consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                    consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                    consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', t0.Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                    consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad,  "
                    consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                    consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                    consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode where t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                    consulta &= "union all "
                    consulta &= "select t0.SlpCode, t0.id, '' as 'CardCode', CONCAT('Fin de Ruta ::::: ', 'Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = t0.SlpCode and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                    consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = t0.SlpCode and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Nombre', "
                    consulta &= "cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHFin,'%d-%m-%Y') as 'Fecha', "
                    consulta &= "'' as 'Hora Comienzo', cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) as 'Hora Fin', '' as 'Duracion de Visita', "
                    consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', CONCAT('Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = t0.SlpCode and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                    consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = t0.SlpCode and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Pedido', '' as 'Valor', "
                    consulta &= "case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                    consulta &= "from Visita t0 where t0.Prioridad = 3 and cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                    consulta &= "order by SlpCode, ordenFecha, Prioridad, id ASC "
                    BuscaXAgente(consulta, "T")


                    'traeTodo()'
                Else
                    'MsgBox("estas filtrando un cliente sin haber elegido nada en los agentes")
                    bandera_todos = False
                    bandera_uno = True
                    consulta &= "select t0.id, t2.SlpName, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                    consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                    consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                    consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                    consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad, "
                    consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                    consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                    consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode inner join Agentes t2 on t0.SlpCode = t2.SlpCode where t0.CardCode = '" & ComboBox1.SelectedValue.ToString & "' AND t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                    consulta &= "order by id "
                    BuscaXAgente(consulta, "U")
                    'funcion1(Cliente)
                End If
            Else
                'MsgBox("no estas seleccionando nada de nada")
                MsgBox("Selecciona un agente de ventas o un cliente")
                Return

            End If

        Else
            If CmbCliente.SelectedValue.ToString = "9999" Then 'selecciona todos los agentes'

                If ComboBox1.SelectedIndex <> -1 Then
                    If ComboBox1.SelectedValue.ToString = "8888" Then
                        bandera_todos = True
                        bandera_uno = False
                        'MsgBox("estas seleccionando todo de todo")
                        consulta &= "select distinct t1.SlpCode, -1 as 'id', '' as 'CardCode', t1.SlpName as 'Nombre', '' as 'ordenFecha', '' as 'Fecha', '' as 'Hora Comienzo', '' as 'Hora Fin', '' as 'Duracion de Visita', "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', '' as 'Pedido', '' as 'Valor', '' as 'Comentario', '' as 'Numero de Visita', -1 as 'Prioridad', '' as 'Veces' "
                        consulta &= "from Visita t0 inner join Agentes t1 on t0.SlpCode = t1.SlpCode "
                        consulta &= "where cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
                        consulta &= "union all "
                        consulta &= "select t0.SlpCode, t0.id, '' as 'CardCode', 'Inicio de Ruta' as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo', '' as 'Hora Fin', '' as 'Duracion de Visita',  "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', '' as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                        consulta &= "from Visita t0 where t0.Prioridad = 1 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
                        consulta &= "union ALL "
                        consulta &= "select t0.SlpCode, t0.id, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                        consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                        consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', t0.Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                        consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad,  "
                        consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                        consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                        consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode where t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "union all "
                        consulta &= "select t0.SlpCode, t0.id, '' as 'CardCode', CONCAT('Fin de Ruta ::::: ', 'Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = t0.SlpCode and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                        consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = t0.SlpCode and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Nombre', "
                        consulta &= "cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHFin,'%d-%m-%Y') as 'Fecha', "
                        consulta &= "'' as 'Hora Comienzo', cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) as 'Hora Fin', '' as 'Duracion de Visita', "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', CONCAT('Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = t0.SlpCode and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                        consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = t0.SlpCode and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Pedido', '' as 'Valor', "
                        consulta &= "case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                        consulta &= "from Visita t0 where t0.Prioridad = 3 and cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "order by SlpCode, ordenFecha, Prioridad, id ASC "
                        BuscaXAgente(consulta, "T")
                        'traeTodo()'

                    Else
                        'MsgBox("estas filtrando un cliente de todos los agentes")
                        bandera_todos = False
                        bandera_uno = True
                        consulta &= "select t0.id, t2.SlpName, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                        consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                        consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                        consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad,  "
                        consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                        consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                        consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode inner join Agentes t2 on t0.SlpCode = t2.SlpCode where t0.CardCode = '" & ComboBox1.SelectedValue.ToString & "' AND t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "order by id "
                        BuscaXAgente(consulta, "U")
                        'funcion1(Cliente)
                    End If
                Else
                    'MsgBox("seleccionaste todos los agentes pero no estas seleccionando nigun cliente")
                    MsgBox("Selecciona un cliente")
                    Return
                End If

            ElseIf CmbCliente.SelectedValue.ToString = "9191" Then 'selecciona todos los TODOS(agentes)'
                If ComboBox1.SelectedIndex <> -1 Then
                    If ComboBox1.SelectedValue.ToString = "8888" Then
                        bandera_todos = True
                        bandera_uno = False
                        'MsgBox("estas seleccionando todo de todo")
                        consulta &= "select distinct t1.SlpCode, -1 as 'id', '' as 'CardCode', t1.SlpName as 'Nombre', '' as 'ordenFecha', '' as 'Fecha', '' as 'Hora Comienzo', '' as 'Hora Fin', '' as 'Duracion de Visita', "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', '' as 'Pedido', '' as 'Valor', '' as 'Comentario', '' as 'Numero de Visita', -1 as 'Prioridad', '' as 'Veces' "
                        consulta &= "from Visita t0 inner join Agentes t1 on t0.SlpCode = t1.SlpCode "
                        consulta &= "where t1.Serie = 'A' and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
                        consulta &= "union all "
                        consulta &= "select t0.SlpCode, t0.id, '' as 'CardCode', 'Inicio de Ruta' as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo', '' as 'Hora Fin', '' as 'Duracion de Visita',  "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', '' as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                        consulta &= "from Visita t0 where t0.SlpCode in (select ttt.SlpCode from Agentes ttt where ttt.Serie = 'A') and t0.Prioridad = 1 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
                        consulta &= "union ALL "
                        consulta &= "select t0.SlpCode, t0.id, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                        consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                        consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', t0.Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                        consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad,  "
                        consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                        consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                        consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode where t0.SlpCode in (select ttt.SlpCode from Agentes ttt where ttt.Serie = 'A') and t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "union all "
                        consulta &= "select t0.SlpCode, t0.id, '' as 'CardCode', CONCAT('Fin de Ruta ::::: ', 'Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = t0.SlpCode and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                        consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = t0.SlpCode and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Nombre', "
                        consulta &= "cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHFin,'%d-%m-%Y') as 'Fecha', "
                        consulta &= "'' as 'Hora Comienzo', cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) as 'Hora Fin', '' as 'Duracion de Visita', "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', CONCAT('Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = t0.SlpCode and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                        consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = t0.SlpCode and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Pedido', '' as 'Valor', "
                        consulta &= "case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                        consulta &= "from Visita t0 where t0.SlpCode in (select ttt.SlpCode from Agentes ttt where ttt.Serie = 'A') and t0.Prioridad = 3 and cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "order by SlpCode, ordenFecha, Prioridad, id ASC "
                        BuscaXAgente(consulta, "T")
                        'traeTodo()'

                    Else
                        'MsgBox("estas filtrando un cliente de todos los agentes")
                        bandera_todos = False
                        bandera_uno = True
                        consulta &= "select t0.id, t2.SlpName, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                        consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                        consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                        consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad,  "
                        consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                        consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                        consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode inner join Agentes t2 on t0.SlpCode = t2.SlpCode where t0.CardCode = '" & ComboBox1.SelectedValue.ToString & "' AND t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "order by id "
                        BuscaXAgente(consulta, "U")
                        'funcion1(Cliente)
                    End If
                Else
                    'MsgBox("seleccionaste todos los agentes pero no estas seleccionando nigun cliente")
                    MsgBox("Selecciona un cliente")
                    Return
                End If


            ElseIf CmbCliente.SelectedValue.ToString = "9090" Then 'selecciona todos los TODOS(choferes)'
                If ComboBox1.SelectedIndex <> -1 Then
                    If ComboBox1.SelectedValue.ToString = "8888" Then
                        bandera_todos = True
                        bandera_uno = False
                        'MsgBox("estas seleccionando todo de todo")
                        consulta &= "select distinct t1.SlpCode, -1 as 'id', '' as 'CardCode', t1.SlpName as 'Nombre', '' as 'ordenFecha', '' as 'Fecha', '' as 'Hora Comienzo', '' as 'Hora Fin', '' as 'Duracion de Visita', "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', '' as 'Pedido', '' as 'Valor', '' as 'Comentario', '' as 'Numero de Visita', -1 as 'Prioridad', '' as 'Veces' "
                        consulta &= "from Visita t0 inner join Agentes t1 on t0.SlpCode = t1.SlpCode "
                        consulta &= "where t1.Serie = 'C' and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
                        consulta &= "union all "
                        consulta &= "select t0.SlpCode, t0.id, '' as 'CardCode', 'Inicio de Ruta' as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo', '' as 'Hora Fin', '' as 'Duracion de Visita',  "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', '' as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                        consulta &= "from Visita t0 where t0.SlpCode in (select ttt.SlpCode from Agentes ttt where ttt.Serie = 'C') and t0.Prioridad = 1 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
                        consulta &= "union ALL "
                        consulta &= "select t0.SlpCode, t0.id, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                        consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                        consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', t0.Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                        consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad,  "
                        consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                        consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                        consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode where t0.SlpCode in (select ttt.SlpCode from Agentes ttt where ttt.Serie = 'C') and t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "union all "
                        consulta &= "select t0.SlpCode, t0.id, '' as 'CardCode', CONCAT('Fin de Ruta ::::: ', 'Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = t0.SlpCode and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                        consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = t0.SlpCode and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Nombre', "
                        consulta &= "cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHFin,'%d-%m-%Y') as 'Fecha', "
                        consulta &= "'' as 'Hora Comienzo', cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) as 'Hora Fin', '' as 'Duracion de Visita', "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', CONCAT('Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = t0.SlpCode and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                        consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = t0.SlpCode and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Pedido', '' as 'Valor', "
                        consulta &= "case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                        consulta &= "from Visita t0 where t0.SlpCode in (select ttt.SlpCode from Agentes ttt where ttt.Serie = 'C') and t0.Prioridad = 3 and cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "order by SlpCode, ordenFecha, Prioridad, id ASC "
                        BuscaXAgente(consulta, "NA")
                        'traeTodo()'

                    Else
                        'MsgBox("estas filtrando un cliente de todos los agentes")
                        bandera_todos = False
                        bandera_uno = True
                        consulta &= "select t0.id, t2.SlpName, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                        consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                        consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                        consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad,  "
                        consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                        consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                        consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode inner join Agentes t2 on t0.SlpCode = t2.SlpCode where t0.CardCode = '" & ComboBox1.SelectedValue.ToString & "' AND t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "order by id "
                        BuscaXAgente(consulta, "U")
                        'funcion1(Cliente)
                    End If
                Else
                    'MsgBox("seleccionaste todos los agentes pero no estas seleccionando nigun cliente")
                    MsgBox("Selecciona un cliente")
                    Return
                End If

            Else 'selecciona un agente en especifico'
                If ComboBox1.SelectedIndex <> -1 Then
                    If ComboBox1.SelectedValue.ToString = "8888" Then
                        bandera_todos = False
                        bandera_uno = False
                        'MsgBox("estas seleccionando todos los clientes de un agente en especifico")
                        consulta &= "select t0.id, '' as 'CardCode', 'Inicio de Ruta' as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo', '' as 'Hora Fin', '' as 'Duracion de Visita',  "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', '' as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                        consulta &= "from Visita t0 where t0.SlpCode = " & CmbCliente.SelectedValue.ToString & " AND t0.Prioridad = 1 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
                        consulta &= "union ALL "
                        consulta &= "select t0.id, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                        consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                        consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', t0.Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                        consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad, "
                        consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                        consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                        consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode where t0.SlpCode = " & CmbCliente.SelectedValue.ToString & " AND t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "union all "
                        consulta &= "select t0.id, '' as 'CardCode', CONCAT('Fin de Ruta ::::: ', 'Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = " & CmbCliente.SelectedValue.ToString & " and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                        consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = " & CmbCliente.SelectedValue.ToString & " and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Nombre', "
                        consulta &= "cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHFin,'%d-%m-%Y') as 'Fecha', "
                        consulta &= "'' as 'Hora Comienzo', cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) as 'Hora Fin', '' as 'Duracion de Visita', "
                        consulta &= "'' as 'Ciudad', '' as 'Estado', '' as 'Direccion', '' as 'Coordenadas', "
                        consulta &= "CONCAT('Total Pedidos: ', (select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt4 where tt4.SlpCode = " & CmbCliente.SelectedValue.ToString & " and tt4.Prioridad = 2 and DATE_FORMAT(tt4.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d') and tt4.Pedido = 'SI') , '/', "
                        consulta &= "(select cast( COUNT(DISTINCT CardCode) as char(10)) from Visita tt5 where tt5.SlpCode = " & CmbCliente.SelectedValue.ToString & " and tt5.Prioridad = 2 and DATE_FORMAT(tt5.FHInicio,'%Y-%m-%d') = DATE_FORMAT(t0.FHFin,'%Y-%m-%d'))) as 'Pedido', '' as 'Valor', "
                        consulta &= "case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario', '' as 'Numero de Visita', t0.Prioridad, '' as 'Veces'  "
                        consulta &= "from Visita t0 where t0.SlpCode = " & CmbCliente.SelectedValue.ToString & " AND t0.Prioridad = 3 and cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHFin,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "order by ordenFecha, Prioridad, id ASC "
                        BuscaXAgente(consulta, "A")
                    Else
                        'MsgBox("estas filtrando un cliente de UN agente en especifico")
                        bandera_todos = False
                        bandera_uno = True
                        consulta &= "select t0.id, t2.SlpName, t0.CardCode, t1.CardName as 'Nombre', cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) as 'ordenFecha', DATE_FORMAT(t0.FHInicio,'%d-%m-%Y') as 'Fecha',  "
                        consulta &= "cast(DATE_FORMAT(t0.FHInicio, '%H:%i:%S' ) as char(40)) as 'Hora Comienzo',  "
                        consulta &= "case when t0.FHFin is not null then cast(DATE_FORMAT(t0.FHFin, '%H:%i:%S' ) as char(40)) else 'No se registró' end as 'Hora Fin', case when t0.FHFin is not null then SUBSTRING_INDEX(CAST(timediff(t0.FHInicio, t0.FHFin) as char(50)), '-', -1) else '' end as 'Duracion de Visita',  "
                        consulta &= "t1.City as 'Ciudad', t1.State1 as 'Estado', t1.Address as 'Direccion', CONCAT(t0.CordIni, ' ', t0.CordFin) as 'Coordenadas', Pedido as 'Pedido', '' as 'Valor', case when t0.Comentario is null then '' else t0.Comentario end as 'Comentario',  "
                        consulta &= "cast(t0.Num_Visita as char(40)) as 'Numero de Visita', t0.Prioridad,  "
                        consulta &= "case when t0.id = (select MAX(h1.id) from Visita h1 where h1.CardCode = t0.CardCode and DATE_FORMAT(h1.FHInicio,'%Y-%m-%d') =  "
                        consulta &= "DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') and h1.Prioridad = 2) then 1 else 0 end as 'Veces' "
                        consulta &= "from Visita t0 inner join Clientes t1 on t0.CardCode = t1.CardCode inner join Agentes t2 on t0.SlpCode = t2.SlpCode where t0.CardCode = '" & ComboBox1.SelectedValue.ToString & "' AND t0.Prioridad = 2 and cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) >= '" & Format(DTP1.Value, "yyyy-MM-dd") & "' AND cast(DATE_FORMAT(t0.FHInicio,'%Y-%m-%d') as date) <= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'  "
                        consulta &= "order by id "
                        BuscaXAgente(consulta, "U")
                        'funcion1(Cliente)
                    End If
                Else
                    'MsgBox("seleccionaste un agente en especifico pero no estas seleccionando nigun cliente")
                    MsgBox("Selecciona un cliente")
                    Return
                End If

            End If

        End If
    End Sub

    Public Sub filtraClientes()
        Try
            If (CmbCliente.SelectedValue.ToString() = "9999") Then
                DvLP2.RowFilter = String.Empty
                DvLP2.RowFilter = "SlpCode <> 10101"
                ComboBox1.SelectedValue = "8888"

            ElseIf (CmbCliente.SelectedValue.ToString = "9191") Then
                DvLP2.RowFilter = "Serie = 'A' or SlpCode = 8888"
                ComboBox1.SelectedValue = "8888"

            ElseIf (CmbCliente.SelectedValue.ToString = "9090") Then
                DvLP2.RowFilter = "Serie = 'C' or SlpCode = 8888"
                ComboBox1.SelectedValue = "8888"

            Else
                DvLP2.RowFilter = "SlpCode = " & CmbCliente.SelectedValue.ToString & " or SlpCode = 8888 "
                'ComboBox1.SelectedValue = -1
                ComboBox1.SelectedValue = "8888"
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub CmbCliente_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbCliente.KeyUp
        Try
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z) Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
                strTemp = CmbCliente.Text
                If strTemp.Trim.CompareTo(String.Empty) = 0 Then
                    DvLP.RowFilter = String.Empty
                    DvLP.RowFilter = "SlpCode <> 1010"
                Else
                    Dim strRowFilter As String = String.Concat("Nombre LIKE '%", CmbCliente.Text, "%' and SlpCode <> 1010 ")
                    DvLP.RowFilter = strRowFilter
                    'MsgBox(DvLP.Count)
                    If DvLP.Count = 0 Then
                        DvLP.RowFilter = "SlpCode = 1010"
                    End If

                End If


                CmbCliente.Text = ""
                CmbCliente.Text = strTemp
                CmbCliente.SelectionStart = strTemp.Length
                CmbCliente.SelectedIndex = -1
                CmbCliente.DroppedDown = True
                CmbCliente.SelectedIndex = -1
                CmbCliente.Text = ""
                CmbCliente.Text = strTemp
                CmbCliente.SelectionStart = strTemp.Length

            End If



            'DvClte.RowFilter = "Nombre2 like '%" & CmbCliente.Text & "%'"
            'CmbCliente.DroppedDown = True
        Catch ex As Exception
            'MsgBox("errror en filtro nuevo " & ex.Message)
        End Try
    End Sub

    Private Sub CmbCliente_DropDown(sender As Object, e As EventArgs) Handles CmbCliente.DropDown
        Me.Cursor = Cursors.Arrow

        If strTemp <> "" Then
            CmbCliente.Text = strTemp
            CmbCliente.SelectionStart = strTemp.Length
        End If
        'CBNomEmp.SelectionStart = strTemp.Length
    End Sub

    Private Sub CmbCliente_Leave(sender As Object, e As EventArgs) Handles CmbCliente.Leave
        Try
            If CmbCliente.SelectedIndex.ToString = "-1" Then
                If strTemp <> "" Then
                    CmbCliente.Text = strTemp
                    CmbCliente.SelectionStart = strTemp.Length
                End If
                CmbCliente.SelectedIndex = -1
                DvLP2.RowFilter = String.Empty
                DvLP2.RowFilter = "SlpCode <> 10101"
                ComboBox1.SelectedIndex = -1
                Return
            End If

            If CmbCliente.SelectedValue = 1010 Then
                CmbCliente.SelectedIndex = -1
                DvLP2.RowFilter = String.Empty
                DvLP2.RowFilter = "SlpCode <> 10101"
                ComboBox1.SelectedIndex = -1
                CmbCliente.Text = strTemp
                CmbCliente.SelectionStart = strTemp.Length
                Return
            End If
            filtraClientes()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbCliente_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbCliente.SelectionChangeCommitted
        filtraClientes()
    End Sub

    Private Sub ComboBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles ComboBox1.KeyUp
        Dim strRowFilter As String = ""
        Try
            'MsgBox(e.KeyValue.ToString)
            'MsgBox(e.KeyCode.ToString)
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z) Or (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
                strTemp_toCombobox1 = ComboBox1.Text
                If strTemp_toCombobox1.Trim.CompareTo(String.Empty) = 0 Then
                    If CmbCliente.SelectedIndex <> -1 Then
                        If CmbCliente.SelectedValue = 9999 Then
                            DvLP2.RowFilter = String.Empty
                            DvLP2.RowFilter = "SlpCode <> 10101"

                        ElseIf CmbCliente.SelectedValue = 9191 Then
                            DvLP2.RowFilter = String.Empty
                            DvLP2.RowFilter = "(Serie = 'A' and SlpCode <> 10101) or SlpCode = 8888 "

                        ElseIf CmbCliente.SelectedValue = 9090 Then
                            DvLP2.RowFilter = String.Empty
                            DvLP2.RowFilter = "(Serie = 'C' and SlpCode <> 10101) or SlpCode = 8888 "

                        Else
                            DvLP2.RowFilter = String.Empty
                            DvLP2.RowFilter = "(SlpCode = " & CmbCliente.SelectedValue.ToString & " and SlpCode <> 10101) or SlpCode = 8888 "
                        End If
                    Else
                        DvLP2.RowFilter = String.Empty
                        DvLP2.RowFilter = "SlpCode <> 10101"
                    End If

                Else
                    If CmbCliente.SelectedIndex <> -1 Then
                        If CmbCliente.SelectedValue = 9999 Then
                            strRowFilter = String.Concat("(Name LIKE '%", ComboBox1.Text, "%' and SlpCode <> 10101)")
                            DvLP2.RowFilter = strRowFilter

                        ElseIf CmbCliente.SelectedValue = 9191 Then
                            strRowFilter = String.Concat("(Name LIKE '%", ComboBox1.Text, "%' and (Serie = 'A' or SlpCode = 8888) and SlpCode <> 10101) ")
                            DvLP2.RowFilter = strRowFilter

                        ElseIf CmbCliente.SelectedValue = 9090 Then
                            strRowFilter = String.Concat("(Name LIKE '%", ComboBox1.Text, "%' and (Serie = 'C' or SlpCode = 8888) and SlpCode <> 10101) ")
                            DvLP2.RowFilter = strRowFilter

                        Else
                            strRowFilter = String.Concat("(Name LIKE '%", ComboBox1.Text, "%' and (SlpCode = ", CmbCliente.SelectedValue.ToString, " or SlpCode = 8888) and SlpCode <> 10101) ")
                            DvLP2.RowFilter = strRowFilter
                        End If
                    Else
                        strRowFilter = String.Concat("(Name LIKE '%", ComboBox1.Text, "%' and SlpCode <> 10101) ")
                        DvLP2.RowFilter = strRowFilter
                    End If

                    If DvLP2.Count = 0 Then
                        DvLP2.RowFilter = "SlpCode = 10101"
                        'Else
                        '    strRowFilter = String.Concat(strRowFilter, " or SlpCode = 8888 ")
                        '    DvLP2.RowFilter = strRowFilter
                    End If

                End If
                ComboBox1.Text = ""
                ComboBox1.Text = strTemp_toCombobox1
                ComboBox1.SelectionStart = strTemp_toCombobox1.Length
                ComboBox1.SelectedIndex = -1
                ComboBox1.DroppedDown = True
                ComboBox1.SelectedIndex = -1
                ComboBox1.Text = ""
                ComboBox1.Text = strTemp_toCombobox1
                ComboBox1.SelectionStart = strTemp_toCombobox1.Length

            End If

        Catch ex As Exception
            'MsgBox("algo salio mal en filtro nuevo ")
        End Try
    End Sub

    Private Sub ComboBox1_DropDown(sender As Object, e As EventArgs) Handles ComboBox1.DropDown
        Me.Cursor = Cursors.Arrow

        If strTemp_toCombobox1 <> "" Then
            ComboBox1.Text = strTemp_toCombobox1
            ComboBox1.SelectionStart = strTemp_toCombobox1.Length
        End If
        'CBNomEmp.SelectionStart = strTemp.Length
    End Sub

    Private Sub ComboBox1_Leave(sender As Object, e As EventArgs) Handles ComboBox1.Leave
        Try
            If ComboBox1.SelectedIndex.ToString = "-1" Then
                If strTemp_toCombobox1 <> "" Then
                    ComboBox1.Text = strTemp_toCombobox1
                    ComboBox1.SelectionStart = strTemp_toCombobox1.Length
                End If
                ComboBox1.SelectedIndex = -1
                Return
            End If

            If ComboBox1.SelectedValue = 10101 Then
                ComboBox1.SelectedIndex = -1
                ComboBox1.Text = strTemp_toCombobox1
                ComboBox1.SelectionStart = strTemp_toCombobox1.Length
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BuscaXAgente(ByVal cadena_consulta As String, ByVal tipo As String)
        DGVResultado.DataSource = Nothing

        Dim fchini As Date
        Dim fchfin As Date

        Dim rowFilterstr As String = ""
        Dim consulta_cad As String
        Dim consulta_ordVta As String = ""
        consulta_cad = cadena_consulta
        Dim cadenaConexion As String = "Server=67.227.237.109;Database=tractop2_TPD-Check;Uid=tractop2_Sistems;Pwd=S1t10_H0sp3d@nd00;"
        ' Dim cadenaConexion As String = "server=67.227.237.109;database=tractop2_TPD-Check;user id=tractop2_manager;password=Dinomarvel55"
        Dim conn As New MySqlConnection(cadenaConexion)
        Dim conn2 As SqlConnection
        Dim MySqlAdapater As MySqlDataAdapter
        Dim AdaptadorSQL As SqlDataAdapter
        Dim Agentes As New DataSet
        Dim Ordenes As New DataSet

        Try


            conn.Open()
            MySqlAdapater = New MySqlDataAdapter(consulta_cad, conn)
            Agentes = New DataSet
            MySqlAdapater.Fill(Agentes)

            DvDetalle.Table = Agentes.Tables(0)
            DGVResultado.DataSource = DvDetalle



            If DGVResultado.RowCount = 0 Then
                Try
                    DGOrdVta.DataSource = Nothing
                    DGVResultado.DataSource = Nothing
                    DGOrdVtaDet.DataSource = Nothing
                Catch ex As Exception
                End Try
                MessageBox.Show("No hay visitas registradas para el rango de fecha seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else
                DGVResultado.CurrentRow.Selected = False
                Try
                    DGOrdVta.DataSource = Nothing
                    DGOrdVtaDet.DataSource = Nothing
                Catch ex As Exception
                End Try
                conn2 = New SqlConnection(StrCon)
                Try
                    conn2.Open()
                    consulta_ordVta &= "select DocEntry, DocNum, DocType, Printed, DocStatus, InvntSttus, CardCode, CardName, SlpCode, (DocTotal - VatSum) as 'DocTotal', "
                    consulta_ordVta &= "convert(varchar, DocDate, 105) as 'Fecha', CONVERT(date, DocDate) as 'FechaFiltro' into #OrdHeader "
                    consulta_ordVta &= "from ORDR where DocDate between '" & Format(DTP1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker1.Value.AddDays(2), "yyyy-MM-dd") & "' "
                    If (tipo = "U") Then
                        consulta_ordVta &= " and CardCode = '" & ComboBox1.SelectedValue.ToString & "' "
                    ElseIf (tipo = "A") Then
                        consulta_ordVta &= " and SlpCode = " & CmbCliente.SelectedValue.ToString & " "
                    ElseIf (tipo = "NA") Then
                        consulta_ordVta &= " and SlpCode = 987456321"
                    End If

                    consulta_ordVta &= "select * from #OrdHeader "
                    consulta_ordVta &= "select t1.DocEntry, t1.LineNum, t1.ItemCode, t1.Dscription, t1.Quantity, t1.U_BXP_ListaP,  "
                    consulta_ordVta &= "t1.PriceBefDi, (t1.DiscPrcnt / 100 ) as 'DiscPrcnt', t1.Price, t1.LineTotal, t1.WhsCode   "
                    consulta_ordVta &= "from #OrdHeader t0 inner join RDR1 t1 on t0.DocEntry = t1.DocEntry "
                    consulta_ordVta &= "drop table #OrdHeader "

                    'MsgBox(consulta_ordVta)
                    AdaptadorSQL = New SqlDataAdapter(consulta_ordVta, conn2)
                    Ordenes = New DataSet
                    AdaptadorSQL.Fill(Ordenes)

                    DvOrdenes.Table = Ordenes.Tables(0)
                    DvOrdDetail.Table = Ordenes.Tables(1)
                    'DGOrdVta.DataSource = DvOrdenes

                    '/////////////////////////////////////////////////////////////////////////////////////////////////
                    With DGVResultado
                        For Each row As DataGridViewRow In .Rows
                            If (row.Cells("Prioridad").Value = 2) Then
                                If (row.Cells("Pedido").Value.ToString = "SI") Then
                                    If (row.Cells("Veces").Value.ToString = "1") Then
                                        Dim codigo_cte As String = ""
                                        codigo_cte = row.Cells("CardCode").Value.ToString.Substring(0, 1) & row.Cells("CardCode").Value.ToString.Substring(row.Cells("CardCode").Value.ToString.IndexOf("-"))
                                        'MsgBox(codigo_cte)
                                        fchini = row.Cells("ordenFecha").Value
                                        fchfin = fchini.AddDays(2)
                                        rowFilterstr = ""
                                        'rowFilterstr = String.Concat("CardCode = '", row.Cells("CardCode").Value.ToString, "' and FechaFiltro >= '", fchini.ToString("yyyy-MM-dd"), "' and FechaFiltro <= '", fchfin.ToString("yyyy-MM-dd"), "'")
                                        rowFilterstr = String.Concat("CardCode = '", codigo_cte, "' and FechaFiltro >= '", fchini.ToString("yyyy-MM-dd"), "' and FechaFiltro <= '", fchfin.ToString("yyyy-MM-dd"), "'")

                                        Dim suma_valor As Double = 0
                                        Dim aux As Double = 0
                                        DvOrdenes.RowFilter = rowFilterstr
                                        For Each row2 As DataRowView In DvOrdenes
                                            'MsgBox(row2.Item("DocTotal").ToString)
                                            Double.TryParse(row2.Item("DocTotal").ToString, aux)
                                            suma_valor = suma_valor + aux


                                        Next
                                        'MsgBox(suma_valor.ToString("$$ ###,###,###,##0.00"))


                                        DvOrdenes.RowFilter = String.Empty



                                        'If suma_valor.ToString.Substring(suma_valor.ToString.IndexOf(".") + 1).Length = 1 Then
                                        '    row.Cells("Valor").Value = "$ " & suma_valor.ToString & "0"
                                        'Else
                                        '    row.Cells("Valor").Value = "$ " & suma_valor.ToString
                                        'End If
                                        row.Cells("Valor").Value = suma_valor.ToString("$ ###,###,###,##0.00")
                                        'row.Cells("Valor").Value = fchini
                                    End If
                                End If
                            End If

                        Next

                    End With

                    '/////////////////////////////////////////////////////////////////////////////////////////////////

                    conn2.Close()

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    conn2.Close()
                End Try

                DisenoGrid()
            End If


            'MsgBox(DGVResultado.RowCount.ToString)

            conn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Public Sub DisenoGrid()
        With Me.DGVResultado
            '.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

            '.DefaultCellStyle.BackColor = Color.AliceBlue
            '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            '.Columns("Tipo").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)

            For Each row As DataGridViewRow In .Rows
                If (row.Cells("Prioridad").Value = 1) Then
                    'MsgBox("es uno")
                    row.Cells("Nombre").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    For Each cel As DataGridViewCell In row.Cells
                        cel.Style.BackColor = Color.LightGreen
                    Next
                    'row.Cells("AntiguedadParaPeriodo").Style.BackColor = Color.LightGreen
                    'row.Cells("DiasVaca").Style.BackColor = Color.LightGreen
                ElseIf (row.Cells("Prioridad").Value = 3) Then
                    row.Cells("Nombre").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    row.Cells("Pedido").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    'MsgBox("es dos")
                    For Each cel As DataGridViewCell In row.Cells
                        cel.Style.BackColor = Color.Red
                        cel.Style.ForeColor = Color.White
                    Next
                ElseIf (row.Cells("Prioridad").Value = 2) Then
                    For Each cel As DataGridViewCell In row.Cells
                        cel.Style.BackColor = Color.FloralWhite
                    Next
                    row.Cells("Pedido").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    row.Cells("Pedido").Style.ForeColor = Color.Red

                    row.Cells("Valor").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    row.Cells("Valor").Style.ForeColor = Color.BlueViolet
                Else
                    row.Cells("Nombre").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    For Each cel As DataGridViewCell In row.Cells
                        cel.Style.BackColor = Color.Gold
                    Next
                End If
                Try
                    '.Columns("Tipo").HeaderText = ""
                    '.Columns("Tipo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    '.Columns("Tipo").Width = 200
                    .Columns("CardCode").HeaderText = "Codigo Cliente"
                    .Columns("Valor").HeaderText = "Valor Aprox. de Vta."
                    .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("id").Visible = False
                    .Columns("Prioridad").Visible = False
                    '.Columns("ordenFecha").Visible = False
                    .Columns("ordenFecha").DefaultCellStyle.Format = "yyyy-MM-dd"
                    .Columns("ordenFecha").Visible = False
                    .Columns("Veces").Visible = False
                    .Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Hora Comienzo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Hora Fin").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Duracion de Visita").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Comentario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Pedido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Ciudad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Coordenadas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    '.Columns("Coordenada Y").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Estado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Valor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Comentario").Width = 300
                    .Columns("Nombre").Width = 250
                    .Columns("Direccion").Width = 200
                    .Columns("Coordenadas").Width = 140
                    .Columns("CardCode").Width = 68
                    .Columns("Fecha").Width = 65
                    .Columns("Pedido").Width = 125
                    .Columns("Ciudad").Width = 85
                    .Columns("Estado").Width = 60
                    .Columns("Hora Comienzo").Width = 60
                    .Columns("Hora Fin").Width = 60
                    .Columns("Duracion de Visita").Width = 60
                    '.Columns("Estado").Width = 60

                    .Columns("CardCode").Frozen = True
                    .Columns("Nombre").Frozen = True
                    .Columns("Fecha").Frozen = True
                    .Columns("Hora Comienzo").Frozen = True
                    .Columns("Hora Fin").Frozen = True
                    .Columns("Duracion de Visita").Frozen = True

                    .Columns("Numero de Visita").HeaderText = "Numero de Visita por dia"
                    .Columns("Numero de Visita").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    If bandera_todos = True Then
                        .Columns("SlpCode").Visible = False
                        '.Columns("SlpCode").HeaderText = "Codigo Agente"
                        '.Columns("SlpCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    End If

                    If bandera_uno = True Then
                        'MsgBox("hay una col de mas")
                        '.Columns("Agente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Columns("SlpName").HeaderText = "Nombre Agente"
                        .Columns("SlpName").Width = 110
                        '.Columns("SlpName").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    End If





                Catch ex As Exception
                    ' MsgBox("trono" & ex.Message)
                End Try


                'If row.Cells("DiasGozados").Value.ToString <> "" Then
                '    row.Cells("DiasGozados").Style.BackColor = Color.Gold
                'End If

                'If row.Cells("Pendiente").Value.ToString <> "" Then
                '    row.Cells("Pendiente").Style.BackColor = Color.DarkRed
                'End If
            Next

        End With
    End Sub

    Public Sub DisenoGrid_Ord()
        With DGOrdVta
            '.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.AliceBlue
            .DefaultCellStyle.BackColor = Color.FloralWhite
            '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns("DocEntry").Visible = False
            .Columns("DocType").Visible = False
            .Columns("Printed").Visible = False
            .Columns("DocStatus").Visible = False
            .Columns("InvntSttus").Visible = False
            .Columns("CardCode").Visible = False
            .Columns("CardName").Visible = False
            .Columns("SlpCode").Visible = False
            .Columns("FechaFiltro").Visible = False

            .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns("DocNum").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
            .Columns("DocNum").DefaultCellStyle.ForeColor = Color.BlueViolet

            .Columns("DocTotal").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
            .Columns("DocTotal").DefaultCellStyle.ForeColor = Color.DarkRed

            .Columns("Fecha").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)

            .Columns("DocTotal").DefaultCellStyle.Format = "$ ###,###,###,##0.00"

            .Columns("DocNum").HeaderText = "# Orden"
            .Columns("DocTotal").HeaderText = "Total"

            .Columns("DocNum").Width = 70
            'row.Cells("Pedido").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
            'row.Cells("Pedido").Style.ForeColor = Color.Red

        End With
    End Sub

    Public Sub DisenoGrid_OrdDetail()
        With DGOrdVtaDet
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.AliceBlue
            .DefaultCellStyle.BackColor = Color.FloralWhite
            '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocEntry").Visible = False
            .Columns("LineNum").Visible = False
            .Columns("Quantity").DefaultCellStyle.Format = "###,###,###,##0"
            .Columns("PriceBefDi").DefaultCellStyle.Format = "$ ###,###,###,##0.00"
            .Columns("Price").DefaultCellStyle.Format = "$ ###,###,###,##0.00"
            .Columns("LineTotal").DefaultCellStyle.Format = "$ ###,###,###,##0.00"
            .Columns("DiscPrcnt").DefaultCellStyle.Format = "P"

            .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("U_BXP_ListaP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PriceBefDi").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DiscPrcnt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("WhsCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns("ItemCode").HeaderText = "Codigo Articulo"
            .Columns("Dscription").HeaderText = "Descripcion"
            .Columns("Quantity").HeaderText = "Pzas."
            .Columns("U_BXP_ListaP").HeaderText = "L.P."
            .Columns("PriceBefDi").HeaderText = "Precio Unitario"
            .Columns("DiscPrcnt").HeaderText = "Desc."
            .Columns("Price").HeaderText = "Precio tras desc."
            .Columns("LineTotal").HeaderText = "TOTAL"
            .Columns("WhsCode").HeaderText = "Almc."


            .Columns("ItemCode").Width = 80
            .Columns("Dscription").Width = 400
            .Columns("Quantity").Width = 40
            .Columns("U_BXP_ListaP").Width = 33
            .Columns("PriceBefDi").Width = 75
            .Columns("DiscPrcnt").Width = 50
            .Columns("Price").Width = 85
            .Columns("LineTotal").Width = 110
            .Columns("WhsCode").Width = 35

            '.Columns("ItemCode").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
            .Columns("ItemCode").DefaultCellStyle.ForeColor = Color.DarkBlue
            .Columns("LineTotal").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
            .Columns("LineTotal").DefaultCellStyle.ForeColor = Color.DarkRed
        End With

    End Sub

    Private Sub DGVResultado_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGVResultado.RowPrePaint
        With Me.DGVResultado
            '.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            '.DefaultCellStyle.BackColor = Color.AliceBlue
            '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            '.Columns("Tipo").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)

            For Each row As DataGridViewRow In .Rows

                If (row.Cells("Prioridad").Value = 1) Then
                    'MsgBox("es uno")
                    row.Cells("Nombre").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    For Each cel As DataGridViewCell In row.Cells
                        cel.Style.BackColor = Color.LightGreen
                    Next
                    'row.Cells("AntiguedadParaPeriodo").Style.BackColor = Color.LightGreen
                    'row.Cells("DiasVaca").Style.BackColor = Color.LightGreen
                ElseIf (row.Cells("Prioridad").Value = 3) Then
                    row.Cells("Nombre").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    row.Cells("Pedido").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    'MsgBox("es dos")
                    For Each cel As DataGridViewCell In row.Cells
                        cel.Style.BackColor = Color.Red
                        cel.Style.ForeColor = Color.White
                    Next
                ElseIf (row.Cells("Prioridad").Value = 2) Then
                    For Each cel As DataGridViewCell In row.Cells
                        cel.Style.BackColor = Color.FloralWhite
                    Next
                    row.Cells("Pedido").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    row.Cells("Pedido").Style.ForeColor = Color.Red

                    row.Cells("Valor").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    row.Cells("Valor").Style.ForeColor = Color.BlueViolet
                Else
                    row.Cells("Nombre").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                    For Each cel As DataGridViewCell In row.Cells
                        cel.Style.BackColor = Color.Gold
                    Next
                End If




                'If row.Cells("DiasGozados").Value.ToString <> "" Then
                '    row.Cells("DiasGozados").Style.BackColor = Color.Gold
                'End If

                'If row.Cells("Pendiente").Value.ToString <> "" Then
                '    row.Cells("Pendiente").Style.BackColor = Color.DarkRed
                'End If
            Next

        End With
    End Sub

    Private Sub DGVResultado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVResultado.CellContentClick
        'If e.RowIndex >= 0 Then
        '    Dim row As DataGridViewRow = DGVResultado.Rows(e.RowIndex)
        '    Try
        '        MsgBox(row.Cells("Pedido").Value.ToString)
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try

        'End If
    End Sub

    Private Sub DGVResultado_SelectionChanged(sender As Object, e As EventArgs) Handles DGVResultado.SelectionChanged
        Dim strFiltro As String = ""
        Try
            If DGVResultado.CurrentRow.Cells("Prioridad").Value.ToString = "2" Then
                If DGVResultado.CurrentRow.Cells("Pedido").Value.ToString = "SI" Then
                    'MsgBox("hola")
                    fechafiltroinicio = DGVResultado.CurrentRow.Cells("ordenFecha").Value
                    fechafiltrofin = fechafiltroinicio.AddDays(2)
                    'MsgBox(fechafiltroinicio.ToString("yyyy-MM-dd"))
                    'MsgBox(fechafiltrofin.ToString("yyyy-MM-dd"))

                    'MsgBox(DGVResultado.CurrentRow.Cells("ordenFecha").Value.ToString("yyyy-MM-dd"))
                    Try
                        Dim codigo_cte As String = ""
                        codigo_cte = DGVResultado.CurrentRow.Cells("CardCode").Value.ToString.Substring(0, 1) & DGVResultado.CurrentRow.Cells("CardCode").Value.ToString.Substring(DGVResultado.CurrentRow.Cells("CardCode").Value.ToString.IndexOf("-"))
                        'strFiltro = String.Concat("CardCode = '", DGVResultado.CurrentRow.Cells("CardCode").Value.ToString, "' and FechaFiltro >= '", fechafiltroinicio.ToString("yyyy-MM-dd"), "' and FechaFiltro <= '", fechafiltrofin.ToString("yyyy-MM-dd"), "'")
                        strFiltro = String.Concat("CardCode = '", codigo_cte, "' and FechaFiltro >= '", fechafiltroinicio.ToString("yyyy-MM-dd"), "' and FechaFiltro <= '", fechafiltrofin.ToString("yyyy-MM-dd"), "'")
                        'MsgBox(strFiltro)
                        DvOrdenes.RowFilter = strFiltro
                        'strFiltro = String.Concat("CardCode = '", DGVResultado.CurrentRow.Cells("CardCode").Value.ToString, "'")
                        'DvOrdenes.RowFilter = strFiltro
                        DGOrdVta.DataSource = DvOrdenes
                        'Me.DGOrdVta.CurrentRow.Selected = False
                        'Me.DGOrdVtaDet.CurrentRow.Selected = False
                        If (DGOrdVta.RowCount = 0) Then
                            DGOrdVtaDet.DataSource = Nothing
                        End If
                        'DGOrdVtaDet.DataSource = Nothing
                        DisenoGrid_Ord()
                    Catch ex As Exception

                    End Try
                Else
                    Try
                        DGOrdVta.DataSource = Nothing
                        DGOrdVtaDet.DataSource = Nothing
                    Catch ex As Exception

                    End Try

                End If
            End If
        Catch ex As Exception

        End Try

        'MsgBox(DGVResultado.CurrentRow.Index)
        'MsgBox(DGVResultado.CurrentRow.Cells("Pedido").Value.ToString)
    End Sub

    Private Sub DGOrdVta_SelectionChanged(sender As Object, e As EventArgs) Handles DGOrdVta.SelectionChanged
        Dim strFiltro As String = ""
        Try
            strFiltro = String.Concat("DocEntry = " & DGOrdVta.CurrentRow.Cells("DocEntry").Value.ToString)
            DvOrdDetail.RowFilter = strFiltro
            DGOrdVtaDet.DataSource = DvOrdDetail
            DisenoGrid_OrdDetail()
            DGOrdVtaDet.CurrentRow.Selected = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnRecibos_Click(sender As Object, e As EventArgs) Handles BtnRecibos.Click
        ExportarDatosExcel_Visita(DGVResultado, "Visitas")
    End Sub

    Public Sub ExportarDatosExcel_Visita(ByVal DataGridView1 As DataGridView, ByVal descripcion_tabla As String)
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
                    If (c.Name.ToString = "Hora Comienzo" Or c.Name.ToString = "Hora Fin" Or c.Name.ToString = "Duracion de Visita" Or c.Name.ToString = "Direccion" Or c.Name.ToString = "Coordenadas") Then
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
                Dim mi_color As New Color
                Dim mi_color2 As New Color
                Dim bandera As Boolean = False

                For Each reg As DataGridViewRow In DataGridView1.Rows
                    LetraIzq = ""
                    cod_LetraIzq = Asc(primeraLetra) - 1
                    Letra = primeraLetra
                    cod_letra = Asc(primeraLetra) - 1
                    If reg.Cells("Prioridad").Value = -1 Then
                        mi_color = Color.Gold
                        'mi_color2 = Color.Black
                        'bandera = True
                    ElseIf reg.Cells("Prioridad").Value = 1 Then
                        mi_color = Color.LightGreen
                        'mi_color2 = Color.Black
                        'bandera = True
                    ElseIf reg.Cells("Prioridad").Value = 2 Then
                        mi_color = Color.FloralWhite
                        'mi_color2 = Color.Black
                        'bandera = False
                    ElseIf reg.Cells("Prioridad").Value = 3 Then
                        mi_color = Color.Red
                        'mi_color2 = Color.White
                        'bandera = True

                    End If
                    For Each c As DataGridViewColumn In DataGridView1.Columns
                        If (c.Name.ToString = "Hora Comienzo" Or c.Name.ToString = "Hora Fin" Or c.Name.ToString = "Duracion de Visita" Or c.Name.ToString = "Direccion" Or c.Name.ToString = "Coordenadas") Then
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

                            .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
                            .Cells(i, strColumna).interior.color = mi_color
                            '.Cells(i, strColumna).Font.Color = mi_color2

                            'If bandera Then
                            '    .Cells(i, strColumna).Font.Bold = True
                            'Else
                            '    .Cells(i, strColumna).Font.Bold = False
                            'End If


                            'If c.Name.ToString = "Pedido" Then
                            '    .Cells(i, strColumna).Font.Color = Color.Red
                            '    '.Cells(i, strColumna).Font.Bold = True
                            'End If

                            'If c.Name.ToString = "Valor" Then
                            '    .Cells(i, strColumna).Font.Color = Color.BlueViolet
                            '    '.Cells(i, strColumna).Font.Bold = True
                            'End If





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
                    If (c.Name.ToString = "Hora Comienzo" Or c.Name.ToString = "Hora Fin" Or c.Name.ToString = "Duracion de Visita" Or c.Name.ToString = "Direccion" Or c.Name.ToString = "Coordenadas") Then
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

            'objHojaExcel.Rows.Item(5).Font.Bold = 1
            'objHojaExcel.Rows.Item(5).ForeColor = Color.Red
            For ee As Integer = 1 To (DataGridView1.Rows.Count + 3)
                objHojaExcel.Rows.Item(ee).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter


            Next
            m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
End Class