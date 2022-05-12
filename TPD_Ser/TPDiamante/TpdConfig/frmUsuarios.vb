Imports System.Data.SqlClient

Public Class frmUsuarios

  Public StrTpm As String = conexion_universal.CadenaSQL
  'Public StrCon As String = conexion_universal.CadenaSQLSAP

  'VARIABLES PARA ACCEDER A LOS COMANDOS
  Dim SQL As New Comandos_SQL
  Dim DvUsuarios As New DataView
  Dim bNuevo As Boolean = False

  Private Sub frmUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    SQL.conectarTPM()
    LlenaDeptos()
    LlenaEstatus()
    LlenaRol()
    LlenaEsAgente()
    LlenaAgenteVentasActual()
    LlenaAgenteActual()

    LlenaBoAutoriza()
    LlenaAlmacen()

    Limpiar()
  End Sub

  Private Sub LimpiaPanle5()
    txtIDUsr.Text = ""
    txtNombre.Text = ""
    cboDepto.SelectedValue = ""
    txtClave.Text = ""
    cboEstatus.SelectedIndex = 0
    cboRol.SelectedIndex = 0
    txtCorreoEmpresarial.Text = ""
    txtRutaPDF.Text = ""
    txtSerie.Text = ""
    txtClaveC.Text = ""
    txtCVentas.Text = ""
    txtCCVentas.Text = ""
    cboEsAgente.Text = ""
    cboBOAutoriza.Text = ""
    cboAlmacen.Text = ""
    cboAgenteVentas.Text = ""
    btnAccesos.Enabled = False
  End Sub

  Private Sub Limpiar()
    btnAccesos.Enabled = False
    Buscar_Usuarios()
    'ConsultarUsuarios()

    btAgregar.Enabled = True
    btModificar.Enabled = False
    btnGrabar.Enabled = False
    btEliminar.Enabled = False
    LimpiaPanle5()
    Panel5.Enabled = False
  End Sub

  Sub ConsultarUsuarios()
    Dim Consulta As String = "SELECT Id_Usuario, Nombre, Id_Dept, Pw, Estatus, idRol, 
                              CASE WHEN CorreoE IS NULL THEN '' ELSE CorreoE END AS 'Correo Electrónico', 
                              CASE WHEN RutaPdf IS NULL THEN '' ELSE RutaPdf END AS 'Ruta PDF', 
                              CASE WHEN CodAgte IS NULL THEN '' ELSE CodAgte END AS 'CodAgte', 
                              CASE WHEN Serie IS NULL THEN '' ELSE Serie END AS 'Serie', 
                              CASE WHEN Pswmail IS NULL THEN '' ELSE Pswmail END AS 'Pswmail',
                              CASE WHEN CorreoVta IS NULL THEN '' ELSE CorreoVta END AS 'CorreoVta',
                              CASE WHEN CCorreo IS NULL THEN '' ELSE CCorreo END AS 'CCorreo',
                              CASE WHEN Agte IS NULL THEN 0 ELSE Agte END AS 'Agte',
                              CASE WHEN boAutorisa IS NULL THEN 'No definido' 
                                   WHEN boAutorisa = 1 THEN 'SI' ELSE 'NO' END AS 'boAutorisa',
                              CASE WHEN Almacen IS NULL THEN '' ELSE Almacen END AS 'Almacen',
                              CASE WHEN AgteVentas IS NULL THEN 0 ELSE AgteVentas END AS 'AgteVentas'
                              FROM Usuarios"

    dgvUsuarios.DataSource = SQL.ConsultarTabla(Consulta)
  End Sub


  Sub Buscar_Usuarios()
    Try
      SQL.conectarTPM()

      Dim DsUsers As New DataSet

      Dim Consulta As String = "SELECT Id_Usuario, Nombre, Id_Dept, Pw, 
Estatus as CveStatus, CASE WHEN Estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, 
t0.idRol, t1.Descripcion, 
CASE WHEN CorreoE IS NULL THEN '' ELSE CorreoE END AS 'Correo Electrónico', 
CASE WHEN RutaPdf IS NULL THEN '' ELSE RutaPdf END AS 'Ruta PDF', 
CASE WHEN CodAgte IS NULL THEN '' ELSE CodAgte END AS 'CodAgte', 
CASE WHEN t2.SlpName IS NULL THEN '' ELSE t2.SlpName END NombreAgente,
CASE WHEN Serie IS NULL THEN '' ELSE Serie END AS 'Serie', 
CASE WHEN Pswmail IS NULL THEN '' ELSE Pswmail END AS 'Pswmail',
CASE WHEN CorreoVta IS NULL THEN '' ELSE CorreoVta END AS 'CorreoVta',
CASE WHEN CCorreo IS NULL THEN '' ELSE CCorreo END AS 'CCorreo',
CASE WHEN Agte IS NULL THEN 0 ELSE Agte END AS 'Agte',
CASE WHEN Agte IS NULL THEN 'NO' ELSE 'SI' END AS 'UsuarioAgente',
CASE WHEN boAutorisa IS NULL THEN 'No definido' 
WHEN boAutorisa = 1 THEN 'SI' ELSE 'NO' END AS 'boAutorisa',
CASE WHEN Almacen IS NULL THEN '' ELSE Almacen END AS 'Almacen',
CASE WHEN t3.WhsName IS NULL THEN '' ELSE t3.WhsName END AS 'DescAlmacen',
CASE WHEN AgteVentas IS NULL THEN 0 ELSE AgteVentas END AS 'AgteVentas',
CASE WHEN t4.SlpName IS NULL THEN '' ELSE t4.SlpName END AS 'NombreAgteVentas'
FROM Usuarios T0
INNER JOIN UserRol t1 ON T0.idRol = t1.id
LEFT JOIN SBO_TPD.dbo.OSLP t2 ON t0.CodAgte = t2.SlpCode
LEFT JOIN SBO_TPD.dbo.OWHS t3 ON t0.Almacen COLLATE Modern_Spanish_CI_AS = t3.WhsCode
LEFT JOIN SBO_TPD.dbo.OSLP t4 ON t0.AgteVentas = t4.SlpCode"

      Dim conec = New SqlConnection(StrTpm)
      Dim cmd = New SqlCommand(Consulta, conec)
      'cmd.CommandType = CommandType.StoredProcedure
      cmd.CommandType = CommandType.Text

      conec.Open()
      Dim adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()
      adaptador.Fill(DsUsers)

      DsUsers.Tables(0).TableName = "Usuarios"

      DvUsuarios.Table = DsUsers.Tables("Usuarios")

      dgvUsuarios.DataSource = DvUsuarios
      EstiloDgUsuarios()

      SQL.Cerrar()
    Catch ex As Exception
      MessageBox.Show(ex.ToString, "Error al ejecutar la consulta")
    End Try
  End Sub


  Sub EstiloDgUsuarios()
    With Me.dgvUsuarios
      .ReadOnly = True
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      .ReadOnly = True
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        .Columns("Id_Usuario").HeaderText = "Id Usuario"
        .Columns("Id_Usuario").Width = 80
        .Columns("Id_Usuario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("Id_Usuario").ReadOnly = True

        .Columns("Nombre").HeaderText = "Nombre"
        .Columns("Nombre").Width = 200

        .Columns("Id_Dept").HeaderText = "Id_Dept"
        .Columns("Id_Dept").Width = 100

        .Columns("Pw").HeaderText = "Pw"
        .Columns("Pw").Width = 100

        .Columns("CveStatus").HeaderText = "CveStatus"
        .Columns("CveStatus").Width = 5
        .Columns("CveStatus").Visible = False

        .Columns("Estatus").HeaderText = "Estatus"
        .Columns("Estatus").Width = 80

        .Columns("idRol").HeaderText = "idRol"
        .Columns("idRol").Visible = False

        .Columns("Descripcion").HeaderText = "Descripcion"
        .Columns("Descripcion").Width = 120

        .Columns("Correo Electrónico").HeaderText = "Correo Electrónico"
        .Columns("Correo Electrónico").Width = 250

        .Columns("Ruta PDF").HeaderText = "Ruta PDF"
        .Columns("Ruta PDF").Width = 250

        .Columns("CodAgte").HeaderText = "CodAgte"
        .Columns("CodAgte").Visible = False

        .Columns("NombreAgente").HeaderText = "NombreAgente"
        .Columns("NombreAgente").Width = 200

        .Columns("Serie").HeaderText = "Serie"
        .Columns("Serie").Width = 50

        .Columns("Pswmail").HeaderText = "Pswmail"
        .Columns("Pswmail").Width = 100

        .Columns("CorreoVta").HeaderText = "CorreoVta"
        .Columns("CorreoVta").Width = 250

        .Columns("CCorreo").HeaderText = "CCorreo"
        .Columns("CCorreo").Width = 100

        .Columns("Agte").HeaderText = "Agte"
        .Columns("Agte").Visible = False

        .Columns("UsuarioAgente").HeaderText = "Es Agente"
        .Columns("UsuarioAgente").Width = 50

        .Columns("boAutorisa").HeaderText = "Usa BackOrder"
        .Columns("boAutorisa").Width = 150

        .Columns("Almacen").HeaderText = "Almacen"
        .Columns("Almacen").Width = 80

        .Columns("DescAlmacen").HeaderText = "DescAlmacen"
        .Columns("DescAlmacen").Width = 150

        .Columns("AgteVentas").HeaderText = "AgteVentas"
        .Columns("AgteVentas").Visible = False

        .Columns("NombreAgteVentas").HeaderText = "NombreAgteVentas"
        .Columns("NombreAgteVentas").Width = 200

        .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        '.Columns("objetivo").HeaderText = "Objetivo"
        '.Columns("objetivo").Width = 80
        '.Columns("objetivo").DefaultCellStyle.Format = "$ ###,###,##0.#0"
        '.Columns("objetivo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '.Columns("objetivo").Visible = True

        Dim numfilas As Integer
        numfilas = dgvUsuarios.RowCount 'cuenta las filas del DataGrid
        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)
          If dgvUsuarios.Item(5, i).Value = "INACTIVO" Then
            dgvUsuarios.Rows(i).Cells(5).Style.BackColor = Color.Yellow
            'Else
            '  dgvUsuarios.Rows(i).Cells(7).Style.BackColor = Color.Yellow
          End If
        Next

      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub

  Private Sub LlenaDeptos()
    Dim ConsutaLista As String
    ConsutaLista = "SELECT Id_Dept, Des_Dept, Estatus FROM Usr_Dept WHERE Estatus = 'A' "
    ConsutaLista &= " ORDER BY Des_Dept ASC"

    Llena_Combo(cboDepto, ConsutaLista, "Des_Dept", "Id_Dept")
  End Sub

  Private Sub LlenaRol()
    Dim ConsutaLista As String
    ConsutaLista = "SELECT id, Descripcion FROM UserRol WHERE Status = 'A'"
    ConsutaLista &= " ORDER BY Descripcion ASC"

    Llena_Combo(cboRol, ConsutaLista, "Descripcion", "id")
  End Sub

  Private Sub LlenaEstatus()
    cboEstatus.Items.Clear()
    cboEstatus.Items.Add("Activo")
    cboEstatus.Items.Add("Inactivo")
    cboEstatus.SelectedIndex = 0
  End Sub

  Private Sub LlenaEsAgente()
    cboEsAgente.Items.Clear()
    cboEsAgente.Items.Add("SI")
    cboEsAgente.Items.Add("NO")
    cboEsAgente.SelectedIndex = 1
  End Sub

  Private Sub LlenaBoAutoriza()
    cboBOAutoriza.Items.Clear()
    cboBOAutoriza.Items.Add("SI")
    cboBOAutoriza.Items.Add("NO")
    cboBOAutoriza.SelectedIndex = 1
  End Sub

  Private Sub LlenaAgenteActual()
    Dim ConsutaLista As String
    cboAgente.DataSource = Nothing
    cboAgente.Items.Clear()
    ConsutaLista = "SELECT SlpCode, SlpName FROM Cat_Agentes WHERE Status = 'Activo'"
    ConsutaLista &= " UNION SELECT '' SlpCode,'NINGÚNO' as SlpName ORDER BY SlpCode ASC"

    Llena_Combo(cboAgente, ConsutaLista, "SlpName", "SlpCode")
  End Sub

  Private Sub LlenaAgenteNuevo()
    Dim ConsutaLista As String

    cboAgente.DataSource = Nothing
    cboAgente.Items.Clear()
    ConsutaLista = "SELECT SlpCode, SlpName FROM Cat_Agentes WHERE Status = 'Activo'"
    ConsutaLista &= " AND SlpCode NOT IN (Select CodAgte from Usuarios WHERE CodAgte IS NOT NULL)"
    ConsutaLista &= " UNION "
    ConsutaLista &= " SELECT SlpCode, SlpName FROM SBO_TPD.dbo.OSLP "
    ConsutaLista &= " WHERE U_ESTATUS = 'ACTIVO'"
    ConsutaLista &= " AND SlpCode NOT IN (SELECT SlpCode FROM Cat_Agentes)"
    ConsutaLista &= " AND SlpCode NOT IN (Select CodAgte from Usuarios WHERE CodAgte IS NOT NULL)"
    ConsutaLista &= " ORDER BY SlpCode ASC"

    Llena_Combo(cboAgente, ConsutaLista, "SlpName", "SlpCode")
  End Sub

  Private Sub LlenaAgenteVentasActual()
    Dim ConsutaLista As String
    cboAgenteVentas.DataSource = Nothing
    cboAgenteVentas.Items.Clear()
    ConsutaLista = "SELECT SlpCode, SlpName FROM SBO_TPD.dbo.OSLP WHERE U_ESTATUS = 'ACTIVO'"
    ConsutaLista &= " UNION SELECT '' SlpCode,'NINGÚNO' as SlpName ORDER BY SlpCode ASC"

    Llena_Combo(cboAgenteVentas, ConsutaLista, "SlpName", "SlpCode")
  End Sub

  Private Sub LlenaAgenteVentasNuevo()
    Dim ConsutaLista As String

    cboAgente.DataSource = Nothing
    cboAgente.Items.Clear()
    ConsutaLista = "SELECT SlpCode, SlpName FROM Cat_Agentes WHERE Status = 'Activo'"
    ConsutaLista &= " AND SlpCode NOT IN (Select CodAgte from Usuarios WHERE CodAgte IS NOT NULL)"
    ConsutaLista &= " UNION "
    ConsutaLista &= " SELECT SlpCode, SlpName FROM SBO_TPD.dbo.OSLP "
    ConsutaLista &= " WHERE U_ESTATUS = 'ACTIVO'"
    ConsutaLista &= " AND SlpCode NOT IN (SELECT SlpCode FROM Cat_Agentes)"
    ConsutaLista &= " AND SlpCode NOT IN (Select CodAgte from Usuarios WHERE CodAgte IS NOT NULL)"
    ConsutaLista &= " ORDER BY SlpCode ASC"

    Llena_Combo(cboAgente, ConsutaLista, "SlpName", "SlpCode")
  End Sub

  Private Sub LlenaAlmacen()
    Dim ConsutaLista As String
    ConsutaLista = "SELECT WhsCode, WhsName FROM SBO_TPD.dbo.OWHS WHERE WhsCode IN ('01','03','07')" 'Por el momento se dejan estos 3 fijos que son los actuales
    ConsutaLista &= " UNION SELECT '' WhsCode,'NINGÚNO' as WhsName ORDER BY WhsCode ASC"

    Llena_Combo(cboAlmacen, ConsutaLista, "WhsName", "WhsCode")
  End Sub

  Private Sub frmUsuarios_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    SQL.Cerrar()
  End Sub

  Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
    'Graba informacion modificada o nueva
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim CadenaSQL As String = ""
    If bNuevo Then
      CadenaSQL = "INSERT INTO TPM.dbo.Usuarios	(Id_Usuario, Nombre, Id_Dept, Pw, idRol, Aut_Wom, Estatus, Usr_Alta, Fecha_Alta, Fecha_mod, CorreoE, RutaPdf, CodAgte, Serie, Pswmail, CorreoVta, CCorreo, Agte, boAutorisa, Almacen, AgteVentas)"
      CadenaSQL &= " VALUES ("
      CadenaSQL &= " '" & txtIDUsr.Text & "'," 'Id_Usuario
      CadenaSQL &= " '" & txtNombre.Text & "'," 'Nombre
      CadenaSQL &= " '" & cboDepto.SelectedValue & "'," 'Id_Dept
      CadenaSQL &= " '" & txtClave.Text & "'," 'Pw
      CadenaSQL &= " " & cboRol.SelectedValue & "," 'idRol
      CadenaSQL &= " 'N'," ' ver que es Aut_Wom ------------------------?
      If cboEstatus.SelectedIndex = 0 Then
        CadenaSQL &= " 'A'," 'Estatus
      Else
        CadenaSQL &= " 'I'," 'Estatus
      End If

      CadenaSQL &= " '" & UsrTPM & "'," 'Usr_Alta
      CadenaSQL &= " '" & DateTime.Now.ToString("yyyy-MM-dd") & "'," 'Fecha_Alta
      CadenaSQL &= " '" & DateTime.Now.ToString("yyyy-MM-dd") & "'," 'Fecha_mod
      CadenaSQL &= " '" & txtCorreoEmpresarial.Text & "'," 'CorreoE
      CadenaSQL &= " '" & txtRutaPDF.Text & "'," 'RutaPdf
      CadenaSQL &= " " & cboAgente.SelectedValue & "," 'CodAgte 'CadenaSQL &= " '" & txtCodAgente.Text & "'," 'CodAgte
      CadenaSQL &= " '" & txtSerie.Text & "'," 'Serie
      CadenaSQL &= " '" & txtClaveC.Text & "'," 'Pswmail
      CadenaSQL &= " '" & txtCVentas.Text & "'," 'CorreoVta
      CadenaSQL &= " '" & txtCCVentas.Text & "'," 'CCorreo
      If cboEsAgente.SelectedValue = "SI" Then
        CadenaSQL &= " 1," 'Es Agte
      Else
        CadenaSQL &= " 0," 'Es Agte
      End If

      If cboBOAutoriza.Text = "NO" Then
        CadenaSQL &= " 0," 'boAutorisa
      Else
        CadenaSQL &= " 1," 'boAutorisa
      End If

      CadenaSQL &= " '" & cboAlmacen.SelectedValue & "'," 'Almacen
      CadenaSQL &= " " & cboAgenteVentas.SelectedValue & ")" 'AgteVentas
      Try
        con.ConnectionString = SQL.StrTpm
        con.Open()
        cmd.Connection = con
        cmd.CommandText = CadenaSQL
        cmd.ExecuteNonQuery()

        con.Close()
        MsgBox("El usuario fue registrado correctamente", MsgBoxStyle.MsgBoxRight, "Alta correcta")
        Limpiar()
        LimpiaPanle5()

      Catch ex As Exception
        MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
        con.Close()
        Exit Sub
      Finally

      End Try
    Else
      'Modifico registro
      CadenaSQL = "UPDATE TPM.dbo.Usuarios "
      CadenaSQL &= " SET Nombre = '" & txtNombre.Text & "'," 'Id_Usuario
      CadenaSQL &= " Id_Dept = '" & cboDepto.SelectedValue & "'," 'Id_Dept
      CadenaSQL &= " Pw = '" & txtClave.Text & "'," 'Pw
      CadenaSQL &= " idRol = " & cboRol.SelectedValue & "," 'idRol
      CadenaSQL &= " Aut_Wom =  'N'," ' ver que es Aut_Wom ------------------------?
      If cboEstatus.SelectedIndex = 0 Then
        CadenaSQL &= " Estatus = 'A'," 'Estatus
      Else
        CadenaSQL &= " Estatus = 'I'," 'Estatus
      End If
      CadenaSQL &= " Fecha_mod = '" & DateTime.Now.ToString("yyyy-MM-dd") & "'," 'Fecha_mod
      CadenaSQL &= " CorreoE = '" & txtCorreoEmpresarial.Text & "'," 'CorreoE
      CadenaSQL &= " RutaPdf = '" & txtRutaPDF.Text & "'," 'RutaPdf

      If (cboAgente.SelectedValue = Nothing) Then
        CadenaSQL &= " CodAgte = 0," 'CodAgte 
      Else
        CadenaSQL &= " CodAgte = " & cboAgente.SelectedValue & "," 'CodAgte 'CadenaSQL &= " '" & txtCodAgente.Text & "'," 'CodAgte
      End If


      CadenaSQL &= " Serie = '" & txtSerie.Text & "'," 'Serie
      CadenaSQL &= " Pswmail = '" & txtClaveC.Text & "'," 'Pswmail
      CadenaSQL &= " CorreoVta = '" & txtCVentas.Text & "'," 'CorreoVta
      CadenaSQL &= " CCorreo = '" & txtCCVentas.Text & "'," 'CCorreo

      If (cboEsAgente.SelectedValue = "SI") Then
        CadenaSQL &= " Agte = 1," 'Agte 1 = Si es Agente, 0 o NULL si no lo es
      Else
        CadenaSQL &= " Agte = NULL," 'Agte 1 = Si es Agente, 0 o NULL si no lo es
      End If
      If (cboBOAutoriza.Text = "NO") Then
        CadenaSQL &= " boAutorisa = 0," 'boAutorisa Tipo bit
      Else
        CadenaSQL &= " boAutorisa = 1," 'boAutorisa Tipo bit
      End If
      CadenaSQL &= " Almacen = '" & cboAlmacen.Text & "'," 'Almacen

      If (cboAgenteVentas.SelectedValue = Nothing) Then
        CadenaSQL &= " AgteVentas = NULL" 'AgteVentas 
      Else
        CadenaSQL &= " AgteVentas = " & cboAgenteVentas.SelectedValue 'AgteVentas
      End If
      CadenaSQL &= " WHERE Id_Usuario = '" & txtIDUsr.Text & "'" 'Id_Usuario

      Try
        con.ConnectionString = SQL.StrTpm
        con.Open()
        cmd.Connection = con
        cmd.CommandText = CadenaSQL
        cmd.ExecuteNonQuery()
      Catch ex As Exception
        MessageBox.Show("Error modificando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Finally
        con.Close()

        MsgBox("El usuario fue modificado correctamente", MsgBoxStyle.MsgBoxRight, "Modificación correcta")

        Limpiar()
        LimpiaPanle5()
      End Try

    End If
  End Sub

  Private Sub btAgregar_Click(sender As Object, e As EventArgs) Handles btAgregar.Click
    bNuevo = True
    btAgregar.Enabled = False
    btModificar.Enabled = False
    btnGrabar.Enabled = True
    btEliminar.Enabled = False
    Panel5.Enabled = True
    LlenaAgenteNuevo()
    LimpiaPanle5()
  End Sub

  Private Sub btModificar_Click(sender As Object, e As EventArgs) Handles btModificar.Click
    With dgvUsuarios
      bNuevo = False

      btAgregar.Enabled = False
      btModificar.Enabled = False
      btnGrabar.Enabled = True
      btEliminar.Enabled = False
      Panel5.Enabled = True
    End With
  End Sub

  Private Sub btEliminar_Click(sender As Object, e As EventArgs) Handles btEliminar.Click
    With dgvUsuarios
      Dim RowActual As Integer = .CurrentRow.Index
    End With

    btAgregar.Enabled = False
    btModificar.Enabled = False
    btnGrabar.Enabled = False
    btEliminar.Enabled = False
    Panel5.Enabled = False
  End Sub

  Private Sub btLimpiar_Click(sender As Object, e As EventArgs) Handles btLimpiar.Click
    Limpiar()
  End Sub

  Private Sub dgvUsuarios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsuarios.CellContentClick
    CargaUsuario(e.RowIndex)
  End Sub

  Private Sub CargaUsuario(renglon As Integer)
    If renglon >= 0 Then
      'Si se elije otro renglon entonces vuelvo a bloquear el panel de datos


      btAgregar.Enabled = True
      btModificar.Enabled = False
      btnGrabar.Enabled = False
      btEliminar.Enabled = False
      LimpiaPanle5()
      Panel5.Enabled = False

      btnAccesos.Enabled = True
      Dim row As DataGridViewRow = dgvUsuarios.Rows(renglon)
      txtIDUsr.Text = row.Cells("Id_Usuario").Value
      txtNombre.Text = row.Cells("Nombre").Value
      cboDepto.SelectedValue = row.Cells("Id_Dept").Value
      txtClave.Text = row.Cells("Pw").Value
      If (row.Cells("CveStatus").Value) = "A" Then
        cboEstatus.SelectedIndex = 0
      Else
        cboEstatus.SelectedIndex = 1
      End If
      cboRol.SelectedValue = row.Cells("idRol").Value
      txtCorreoEmpresarial.Text = row.Cells("Correo Electrónico").Value
      txtRutaPDF.Text = row.Cells("Ruta PDF").Value
      If row.Cells("Agte").Value = 0 Or IsDBNull(row.Cells("Agte").Value) Then
        cboEsAgente.SelectedIndex = 1
      Else
        cboEsAgente.SelectedIndex = 0
      End If

      If IsNumeric(row.Cells("CodAgte").Value) = True Then
        cboAgente.SelectedValue = row.Cells("CodAgte").Value
      Else
        cboAgente.Text = ""
      End If
      'las series se usan para identificar las cotizaciones de los agentes, en teoria no deberia de existir un agente conla misma seria, salvo que uno este inactivo
      txtSerie.Text = row.Cells("Serie").Value
      txtClaveC.Text = row.Cells("Pswmail").Value
      txtCVentas.Text = row.Cells("CorreoVta").Value
      txtCCVentas.Text = row.Cells("CCorreo").Value
      If row.Cells("boAutorisa").Value = "NO" Or IsDBNull(row.Cells("boAutorisa").Value) Then
        cboBOAutoriza.SelectedIndex = 1
      Else
        cboBOAutoriza.SelectedIndex = 0
      End If

      If IsDBNull(row.Cells("Almacen").Value) Or IsNumeric(row.Cells("Almacen").Value) = False Then
        cboAlmacen.SelectedIndex = 0
      Else
        cboAlmacen.SelectedValue = row.Cells("Almacen").Value
      End If

      If IsNumeric(row.Cells("AgteVentas").Value) = True Then
        cboAgenteVentas.SelectedValue = row.Cells("AgteVentas").Value
      Else
        cboAgenteVentas.Text = ""
      End If

      btAgregar.Enabled = True
      btModificar.Enabled = True
      btnGrabar.Enabled = False
      btEliminar.Enabled = True
    End If
  End Sub

  Private Sub dgvUsuarios_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsuarios.RowEnter
    CargaUsuario(e.RowIndex)
  End Sub

  Private Sub txtNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNombre.KeyPress
    e.KeyChar = Char.ToUpper(e.KeyChar)
  End Sub

  Private Sub txtIDUsr_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIDUsr.KeyPress
    e.KeyChar = Char.ToUpper(e.KeyChar)
  End Sub

  Private Sub txtClave_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtClave.KeyPress
    e.KeyChar = Char.ToUpper(e.KeyChar)
  End Sub

  Private Sub btnAccesos_Click(sender As Object, e As EventArgs) Handles btnAccesos.Click
    Dim Forma As Form = New frmAccesos
    Forma.Show()
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    diSAP.ImportaOrdenVenta2SAP(6260)
  End Sub
End Class