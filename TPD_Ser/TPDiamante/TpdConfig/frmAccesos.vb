Imports System.Data.SqlClient
Imports DocumentFormat.OpenXml.EMMA

Public Class frmAccesos

  Dim SQL As New Comandos_SQL

  Private Sub frmAccesos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    lblUsuario.Text = "PERMISOS PARA: " & frmUsuarios.txtNombre.Text
    lblcveuser.Text = frmUsuarios.txtIDUsr.Text
    CreaTree(lblcveuser.Text)
  End Sub

  Private Sub CreaTree(IdUser As String)
    Try
      SQL.conectarTPM()

      'Reviso primero si tiene la opcion de Menu Especial
      Dim QryME As String = "SELECT menuAccesoEspecial FROM Usuarios WHERE Id_Usuario = '" & IdUser & "'" 'Todos los Niveles 1 solamente
      Dim conME As New SqlConnection(StrTpm)
      Dim cmdME As New SqlCommand()
      cmdME.Connection = conME
      cmdME.CommandText = QryME
      conME.Open()
      cmdME.CommandType = CommandType.Text
      Dim dReaderME As SqlDataReader = cmdME.ExecuteReader(CommandBehavior.CloseConnection)
      While dReaderME.Read()
        If (dReaderME("menuAccesoEspecial") = "NO") Then
          chkMenuEspecial.Checked = False
        Else
          chkMenuEspecial.Checked = True
        End If
      End While
      dReaderME.Close()
      conME.Close()

      Dim strSelect As String = "SELECT * FROM Accesos_TPD WHERE NivelMenu=1 ORDER BY Nivel1, Nivel2, Nivel3, Nivel4" 'Todos los Niveles 1 solamente
      Dim con As New SqlConnection(StrTpm)
      Dim cmd As New SqlCommand()
      cmd.Connection = con
      cmd.CommandText = strSelect
      con.Open()
      cmd.CommandType = CommandType.Text

      Dim dReader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
      Dim N1, N2, N3 As Integer
      N1 = 0
      N1 = 2
      N1 = 3
      While dReader.Read()
        Dim Nivel1 As New TreeNode(dReader("Descripcion"))
        Nivel1.Tag = dReader("Proceso")
        Nivel1.Checked = RevisoPermiso(IdUser, dReader("Proceso"))

        Dim con2 As New SqlConnection(StrTpm)
        Dim cmd2 As New SqlCommand()
        cmd2.Connection = con2
        cmd2.CommandType = CommandType.Text
        con2.Open()
        Dim sn2 As String = "SELECT * FROM Accesos_TPD WHERE NivelMenu=2 AND Nivel1 = " & dReader("Nivel1") & " ORDER BY Nivel1, Nivel2, Nivel3, Nivel4" 'Todos los Niveles 1 solamente
        cmd2.CommandText = sn2
        Dim dReader2 As SqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)

        While dReader2.Read()
          Dim Nivel2 As New TreeNode(dReader2("Descripcion"))
          Nivel2.Tag = dReader2("Proceso")
          Nivel2.Checked = RevisoPermiso(IdUser, dReader2("Proceso"))

          Dim con3 As New SqlConnection(StrTpm)
          Dim cmd3 As New SqlCommand()
          cmd3.Connection = con3
          cmd3.CommandType = CommandType.Text
          con3.Open()
          Dim sn3 As String = "SELECT * FROM Accesos_TPD WHERE NivelMenu=3 AND Nivel1 = " & dReader("Nivel1") & " AND Nivel2 = " & dReader2("Nivel2") & " ORDER BY Nivel1, Nivel2, Nivel3, Nivel4" 'Todos los Niveles 1 solamente
          cmd3.CommandText = sn3
          Dim dReader3 As SqlDataReader = cmd3.ExecuteReader(CommandBehavior.CloseConnection)

          While dReader3.Read()
            Dim Nivel3 As New TreeNode(dReader3("Descripcion"))
            Nivel3.Tag = dReader3("Proceso")
            Nivel3.Checked = RevisoPermiso(IdUser, dReader3("Proceso"))

            Dim con4 As New SqlConnection(StrTpm)
            Dim cmd4 As New SqlCommand()
            cmd4.Connection = con4
            cmd4.CommandType = CommandType.Text
            con4.Open()
            Dim sn4 As String = "SELECT * FROM Accesos_TPD WHERE NivelMenu=4 AND Nivel1 = " & dReader("Nivel1") & " AND Nivel2 = " & dReader2("Nivel2") & " AND Nivel3 = " & dReader3("Nivel3") & " ORDER BY Nivel1, Nivel2, Nivel3, Nivel4" 'Todos los Niveles 1 solamente
            cmd4.CommandText = sn4
            Dim dReader4 As SqlDataReader = cmd4.ExecuteReader(CommandBehavior.CloseConnection)

            While dReader4.Read()
              Dim Nivel4 As New TreeNode(dReader4("Descripcion"))
              Nivel4.Tag = dReader4("Proceso")
              Nivel4.Checked = RevisoPermiso(IdUser, dReader4("Proceso"))

              Nivel3.Nodes.Add(Nivel4)
            End While
            dReader4.Close()
            con4.Close()

            Nivel2.Nodes.Add(Nivel3)
          End While
          dReader3.Close()
          con3.Close()

          Nivel1.Nodes.Add(Nivel2)
        End While
        dReader2.Close()
        con2.Close()
        TreeView1.Nodes.Add(Nivel1)
      End While
    Catch ex As Exception
      MessageBox.Show(ex.ToString, "Error al ejecutar la consulta")
    End Try
  End Sub

  Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)
    For Each childNode As TreeNode In parentNode.Nodes
      childNode.Checked = parentNode.Checked
      CheckAllChildNodes(childNode)
    Next
  End Sub

  Private Sub IsEveryChildChecked(ByVal parentNode As TreeNode, ByRef checkValue As Boolean)
    For Each node As TreeNode In parentNode.Nodes
      Call IsEveryChildChecked(node, checkValue)
      If Not node.Checked Then
        checkValue = False
      End If
    Next
  End Sub

  Private Sub AlmenosUnoChecado(ByVal parentNode As TreeNode, ByRef checkValue As Boolean)
    checkValue = True
    For Each node As TreeNode In parentNode.Nodes
      Call AlmenosUnoChecado(node, checkValue)
      If node.Checked Then
        checkValue = True
        Exit Sub
      End If
    Next
    checkValue = False
  End Sub

  Private Sub ShouldParentsBeChecked(ByVal startNode As TreeNode)
    If startNode.Parent Is Nothing = False Then
      Dim allChecked As Boolean = True
      Call IsEveryChildChecked(startNode.Parent, allChecked)
      If allChecked Then
        startNode.Parent.Checked = True
        Call ShouldParentsBeChecked(startNode.Parent)
      End If
    End If
  End Sub

  Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
    Me.Close()
  End Sub

  Private Sub GrabarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GrabarToolStripMenuItem.Click
    Dim nodes As TreeNodeCollection = TreeView1.Nodes
    For Each n As TreeNode In nodes
      RecorrerNodos(n)
    Next

    GraboInfMenu("Id_Usuario", lblcveuser.Text, "Usuarios", "menuAccesoEspecial", chkMenuEspecial.Checked, TipoDato.Booleano)

    MsgBox("Los permisos del usuario usuario fue registrados correctamente", MsgBoxStyle.MsgBoxRight, "Modificiación correcta")
  End Sub

  Private Sub GraboInfMenu(IdCampo As String, IdUser As String, Tabla As String, campo As String, Valor As VariantType, tipo As TipoDato)
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim CadenaSQL As String = ""
    Dim sValor As String = False

    If tipo = TipoDato.Booleano Then
      If CBool(Valor) = True Then
        sValor = "SI"
      Else
        sValor = "NO"
      End If
    End If

    CadenaSQL = "UPDATE " & Tabla
    If tipo = TipoDato.Cadena Then
      CadenaSQL &= " SET " & campo & " = '" & Valor & "'"
    ElseIf tipo = TipoDato.Numerico Then
      CadenaSQL &= " SET " & campo & " = " & Valor
    ElseIf tipo = TipoDato.Booleano Then
      CadenaSQL &= " SET " & campo & " = '" & sValor & "'"
    End If
    CadenaSQL &= " WHERE " & IdCampo & " = '" & IdUser & "'"
    Try
      con.ConnectionString = SQL.StrTpm
      con.Open()
      cmd.Connection = con
      cmd.CommandText = CadenaSQL
      cmd.ExecuteNonQuery()
    Catch ex As Exception
      MessageBox.Show("Error Actualizando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Finally
      con.Close()
      'MsgBox("El permiso del usuario usuario fue registrado correctamente", MsgBoxStyle.MsgBoxRight, "Alta correcta")
    End Try
  End Sub

  Private Sub ActualizaPermiso(IdUser As String, Proceso As String, acceso As Boolean)
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim CadenaSQL As String = ""
    Dim iAcceso As Integer

    iAcceso = CInt(acceso) * -1

    CadenaSQL = "UPDATE TPM.dbo.Accesos_Usuarios_TPD"
    CadenaSQL &= " SET Acceso = " & iAcceso & ""
    CadenaSQL &= " WHERE IdUser = '" & IdUser & "' AND Proceso = '" & Proceso & "'"
    Try
      con.ConnectionString = SQL.StrTpm
      con.Open()
      cmd.Connection = con
      cmd.CommandText = CadenaSQL
      cmd.ExecuteNonQuery()

    Catch ex As Exception
      MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Finally
      con.Close()
      'MsgBox("El permiso del usuario usuario fue registrado correctamente", MsgBoxStyle.MsgBoxRight, "Alta correcta")
    End Try
  End Sub

  Private Sub InsertoPermiso(IdUser As String, Proceso As String, acceso As Boolean)
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim CadenaSQL As String = ""
    Dim iAcceso As Integer

    iAcceso = CInt(acceso) * -1

    CadenaSQL = "INSERT INTO TPM.dbo.Accesos_Usuarios_TPD	(IdUser, Proceso, Especial, Acceso)"
    CadenaSQL &= " VALUES ("
    CadenaSQL &= " '" & IdUser & "',"
    CadenaSQL &= " '" & Proceso & "',"
    CadenaSQL &= " ''," ' No lo uso todavia
    CadenaSQL &= iAcceso & ")"
    Try
      con.ConnectionString = SQL.StrTpm
      con.Open()
      cmd.Connection = con
      cmd.CommandText = CadenaSQL
      cmd.ExecuteNonQuery()

    Catch ex As Exception
      'MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Finally
      con.Close()
      'MsgBox("El permiso del usuario usuario fue registrado correctamente", MsgBoxStyle.MsgBoxRight, "Alta correcta")
    End Try
  End Sub

  Private Sub RecorrerNodos(treeNode As TreeNode)
    Try
      'Si el nodo que recibimos tiene hijos se recorrerá
      'para luego verificar si esta o no checado
      Dim Existe As Boolean

      If treeNode.Level = 0 Then
        Existe = RevisoPermiso(lblcveuser.Text, treeNode.Tag, True)
        If Existe Then 'Actualizo
          ActualizaPermiso(lblcveuser.Text, treeNode.Tag, treeNode.Checked)
        Else 'Inserto
          InsertoPermiso(lblcveuser.Text, treeNode.Tag, treeNode.Checked)
        End If
      End If

      'Reviso childrens
      For Each tn As TreeNode In treeNode.Nodes
        Existe = RevisoPermiso(lblcveuser.Text, tn.Tag, True)
        If Existe Then 'Actualizo
          ActualizaPermiso(lblcveuser.Text, tn.Tag, tn.Checked)
        Else 'Inserto
          InsertoPermiso(lblcveuser.Text, tn.Tag, tn.Checked)
        End If

        RecorrerNodos(tn)
      Next
    Catch ex As Exception
      MessageBox.Show(ex.ToString())
    End Try
  End Sub

  Public Enum TipoDato
    Cadena = 1
    Numerico = 2
    Booleano = 3
  End Enum

  Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
    'Cargo los permisos especiales del item seleccionado
    Dim ConsutaLista As String
    Dim Tag As String = e.Node.Tag

    ChLBoxEspeciales.Enabled = e.Node.Checked
    'If e.Node.Checked = True Then
    '  ChLBoxEspeciales.Enabled = True
    'End If
    Try
      'Limpio la informacion
      ChLBoxEspeciales.DataSource = Nothing
      ChLBoxEspeciales.Items.Clear()

      Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
        Dim DSetTablas As New DataSet
        'ConsutaLista = "SELECT idPermisoEspecial,Descripcion FROM Accesos_Especiales WHERE Proceso = '" & Tag & "'"
        ConsutaLista = "SELECT T0.idPermisoEspecial, T0.Descripcion, CASE WHEN T1.idPermisoEspecial IS NULL THEN 0 ELSE 1 END Acceso
                        FROM Accesos_Especiales T0
                        FULL JOIN Accesos_Especiales_Usuario T1 ON  T0.idPermisoEspecial = T1.idPermisoEspecial
                        AND T1.IdUser = '" & lblcveuser.Text & "'
                        WHERE Proceso = '" & Tag & "'"

        Dim daPermisos As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

        daPermisos.Fill(DSetTablas, "PermisosEspeciales")

        ChLBoxEspeciales.DataSource = DSetTablas.Tables("PermisosEspeciales")
        ChLBoxEspeciales.DisplayMember = "Descripcion"
        ChLBoxEspeciales.ValueMember = "idPermisoEspecial"



      End Using
    Catch ex As Exception
      MsgBox("Error:" & Err.Description, MsgBoxStyle.Exclamation, "Se presentó un error")
    End Try
  End Sub

  Private Sub TreeView1_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterCheck
    RemoveHandler TreeView1.AfterCheck, AddressOf TreeView1_AfterCheck

    ChLBoxEspeciales.Enabled = e.Node.Checked

    Call CheckAllChildNodes(e.Node)

    Dim parentNode As TreeNode = e.Node.Parent
    While parentNode Is Nothing = False
      Dim almenosuno As Boolean = False
      'Recorrer otros child para ver si estan todos desactivados
      AlmenosUnoChecado(parentNode, almenosuno)
      If almenosuno Then
        parentNode.Checked = True
      Else
        parentNode.Checked = False
      End If
      parentNode = parentNode.Parent
    End While

    AddHandler TreeView1.AfterCheck, AddressOf TreeView1_AfterCheck
  End Sub

  Private Sub ChLBoxEspeciales_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles ChLBoxEspeciales.ItemCheck
    'Si el item fue seleccionado entonces inserto el registro en la tabla
    Dim CadenaSQL As String = ""
    If (e.NewValue) Then
      CadenaSQL = "INSERT INTO Accesos_Especiales_Usuario (IdUser, idPermisoEspecial) VALUES ('" & lblcveuser.Text & "', " & ChLBoxEspeciales.Items(e.Index).Row(0) & ")"
    Else
      CadenaSQL = "DELETE FROM INTO Accesos_Especiales_Usuario WHERE IdUser = '" & lblcveuser.Text & "' AND idPermisoEspecial = " & ChLBoxEspeciales.Items(e.Index).Row(0)
    End If

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Try
      con.ConnectionString = StrTpm
      con.Open()
      cmd.Connection = con
      cmd.CommandText = CadenaSQL
      cmd.ExecuteNonQuery()

    Catch ex As Exception
      MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

    Finally
      con.Close()
    End Try

    'Si el item fue desseleccionado entonces elimino de la tabla el registro
  End Sub
End Class