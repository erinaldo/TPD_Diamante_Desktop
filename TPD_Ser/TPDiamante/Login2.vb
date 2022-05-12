Imports System.Data.SqlClient
Public Class Login2
  Dim varsalir As Boolean = False
  Dim drlinea As Data.SqlClient.SqlDataReader
  Dim cmdlinea1 As Data.SqlClient.SqlCommand
  Dim strcadena As String = ""
  Dim NombreUsuario As String = ""
  'VARIABLES DE CARLOS IVAN PARA LA ACTUALIZACION
  Dim sql As New Comandos_SQL
  Dim Serial As String = ""
  Dim Version_Ord As String = ""
  Dim Version_Serv As String = ""

  Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'PROCESO DE VALIDACION DE VERSIONES
    'Serial_Ordenador()
    Obtener_Versiones()

    'InstallUpdateSyncWithInfo()

    If Version_Ord = Version_Serv Then


      'Else
      '  If MessageBox.Show("Para seguir utilizando el sistema TPD, es necesario actualizar el sistema, ¿Deseas actualizar el sistema?", "Advertencia...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
      '    'EJECUTAR EL SISTEMA DE ACTUALIZADOR
      '    Abrir_Sistema()
      '    varsalir = True
      '    Application.Exit()
      '  Else
      '    'CERRAR EL SISTEMA DE TPD
      '    varsalir = True
      '    Application.Exit()
      '  End If
    End If

    TxtUsuario.Text = "Usuario"
    TxtUsuario.ForeColor = Color.Gray
    TxtPw.Text = "Contraseña"
    TxtPw.ForeColor = Color.Gray
  End Sub

  'Public Sub InstallUpdateSyncWithInfo()
  '  Dim info As UpdateCheckInfo = Nothing
  '  If (ApplicationDeployment.IsNetworkDeployed) Then
  '    Dim AD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment
  '    Try
  '      info = AD.CheckForDetailedUpdate()
  '    Catch dde As DeploymentDownloadException
  '    Catch ioe As InvalidOperationException
  '    End Try
  '    If (info.UpdateAvailable) Then
  '      Try
  '        Inicio.txtmsg.Visible = True
  '        varsalir = True
  '        AD.Update()
  '        Application.Restart()
  '      Catch dde As DeploymentDownloadException
  '      End Try
  '    End If
  '  End If
  'End Sub

  Private Sub btinicio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

  End Sub

  Private Sub TxtUsuario_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtUsuario.KeyPress
    e.KeyChar = Char.ToUpper(e.KeyChar)
  End Sub

  Private Sub Login_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
    Dim form As New Login
    Me.ActivateMdiChild(form)
    form.Focus()
    Me.TxtPw.Focus()

  End Sub

  Private Sub Login_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    If varsalir = False Then
      If MessageBox.Show("¿Confirma que desea salir del sistema?", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) =
                Windows.Forms.DialogResult.OK Then
        varsalir = True
        Inicio.Close()
      Else
        e.Cancel = True
      End If
    End If
  End Sub

  Sub consulta()
    cmdlinea1 = New Data.SqlClient.SqlCommand()
    With cmdlinea1
      .Connection = New Data.SqlClient.SqlConnection(StrTpm)
      .Connection.Open()
      .CommandText = strcadena
      drlinea = .ExecuteReader()
    End With
  End Sub
  Private Sub Login_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
    Me.TxtUsuario.Focus()
  End Sub

  Sub Abrir_Sistema()
    Try
      Dim obj As New Process
      obj.Start("C:\Users\" + Environment.UserName + "\Documents\TPDiamante\Actualizador\Actualizador TPD.exe", AppWinStyle.MaximizedFocus)
    Catch ex As Exception
      MessageBox.Show("Error al abrir Actualizador TPD.exe", "Error de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try

  End Sub

  'Sub Serial_Ordenador()
  '  Try
  '    sql.conectarTPM()
  '    Serial = sql.GetProcessorId.ToString()
  '    sql.Cerrar()
  '    'MessageBox.Show(Environment.UserName)
  '  Catch ex As Exception
  '    MessageBox.Show("Error en Obtener Serial del ordenador: " + ex.ToString(), "Error SQL...", MessageBoxButtons.OK, MessageBoxIcon.Error)
  '  End Try
  'End Sub

  Sub Obtener_Versiones()
    Try
      sql.conectarTPM()
      Version_Ord = sql.CampoEspecifico("SELECT TOP 1 Id_Version AS 'Ordenador' FROM V_Equipo WHERE Id_Equipo = '" + Serial + "' AND Usuario = '" + Environment.UserName + "' ORDER BY REPLACE(Id_Version,'.','') DESC;", "Ordenador")
      Version_Serv = sql.CampoEspecifico("SELECT TOP 1 Actualizacion AS 'Servidor' FROM Actualizador;", "Servidor")
    Catch ex As Exception
      MessageBox.Show("Error en Obtener Versiones: " + ex.ToString(), "Error SQL...", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub



  Private Sub TxtUsuario_LostFocus(sender As Object, e As EventArgs) Handles TxtUsuario.LostFocus
    If TxtUsuario.Text = "" Then
      TxtUsuario.Text = "Usuario"
      TxtUsuario.ForeColor = Color.Gray
    End If
  End Sub

  Private Sub TxtUsuario_TextChanged(sender As Object, e As EventArgs) Handles TxtUsuario.TextChanged
    TxtUsuario.ForeColor = Color.Black
  End Sub

  Private Sub TxtPw_LostFocus(sender As Object, e As EventArgs) Handles TxtPw.LostFocus
    If TxtPw.Text = "" Or TxtPw.Text = "Contraseña" Then
      Me.TxtPw.PasswordChar = CType(ChrW(0), Char)
      TxtPw.ForeColor = Color.Gray
      TxtPw.Text = "Contraseña"
    End If
  End Sub

  Private Sub TxtPw_GotFocus(sender As Object, e As EventArgs) Handles TxtPw.GotFocus
    'If TxtPw.Text = "Contraseña" Then
    TxtPw.ForeColor = Color.Black
    TxtPw.PasswordChar = "*"
    'End If
  End Sub


  Private Sub TxtPw_TextChanged_1(sender As Object, e As EventArgs) Handles TxtPw.TextChanged
    'TxtPw.ForeColor = Color.Black
    'TxtPw.PasswordChar = "*"
  End Sub

  Private Sub RecorrerEstructuraMenu(ByVal oMenu As MenuStrip)
    For Each oOpcionMenu As ToolStripMenuItem In oMenu.Items
      If Trim(oOpcionMenu.Tag) = "" Then
        oOpcionMenu.Visible = True
      Else
        oOpcionMenu.Visible = funciones.RevisoPermiso(IdUsuario, oOpcionMenu.Tag)
      End If

      If oOpcionMenu.DropDownItems.Count > 0 Then
        Me.RecorrerSubmenu(oOpcionMenu.DropDownItems, "-")
      End If
    Next
  End Sub

  Private Sub RecorrerSubmenu(ByVal oSubmenuItems As ToolStripItemCollection, ByVal sGuiones As String)
    For Each oSubitem As ToolStripItem In oSubmenuItems
      If oSubitem.GetType Is GetType(ToolStripMenuItem) Then
        If Trim(oSubitem.Tag) = "" Then
          oSubitem.Visible = True
        Else
          oSubitem.Visible = funciones.RevisoPermiso(IdUsuario, oSubitem.Tag)
        End If
        If CType(oSubitem, ToolStripMenuItem).DropDownItems.Count >
    0 Then
          Me.RecorrerSubmenu(CType(oSubitem, ToolStripMenuItem).DropDownItems, sGuiones & "-")
        End If
      End If
    Next
  End Sub

  Private Sub btinicio_Click_2(sender As Object, e As EventArgs) Handles btinicio.Click
    Try

      strcadena = "SELECT Usuarios.*,GETDATE() AS FechaServer FROM Usuarios"
      strcadena &= " WHERE Usuarios.Id_Usuario = '" & TxtUsuario.Text & "' AND Usuarios.Pw = '" & Me.TxtPw.Text & "'"

      consulta()
      'sql.conectarTPM()
      'drlinea = sql.ConsultarTB(strcadena)
      'sql.Cerrar()

      If drlinea.HasRows = False Then
        cmdlinea1.Connection.Close()
        strcadena = "SELECT * FROM Usuarios WHERE "
        strcadena &= "Id_Usuario = '" & TxtUsuario.Text & "'"

        'sql.conectarTPM()
        'drlinea = sql.ConsultarTB(strcadena)
        'sql.Cerrar()
        consulta()

        If drlinea.HasRows = False Then
          'cmdlinea1.Connection.Close()
          MessageBox.Show("El usuario no existe", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          Me.TxtUsuario.Focus()
        Else
          MessageBox.Show("La contraseña es incorrecta", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          'cmdlinea1.Connection.Close()
          Me.TxtPw.Text = ""
          Me.TxtPw.Focus()
        End If
        cmdlinea1.Connection.Close()

      Else
        Inicio.TSMenuInicio.Visible = True
        Inicio.Text = "TPD Usuario: " + TxtUsuario.Text

        Do While drlinea.Read()
          Try
            FchServer = drlinea.Item("FechaServer")
            vSerie = IIf((drlinea.Item("Serie") Is DBNull.Value), "", Convert.ToString(drlinea.Item("Serie")))
            vRutaPdf = IIf((drlinea.Item("RutaPdf") Is DBNull.Value), "", Convert.ToString(drlinea.Item("RutaPdf")))
            vCodAgte = IIf((drlinea.Item("CodAgte") Is DBNull.Value), "", Convert.ToString(drlinea.Item("CodAgte")))
            vCorreo = IIf((drlinea.Item("CorreoE") Is DBNull.Value), "", Convert.ToString(drlinea.Item("CorreoE")))
            vPswmail = IIf((drlinea.Item("Pswmail") Is DBNull.Value), "", Convert.ToString(drlinea.Item("Pswmail")))    'drlinea.Item("Pswmail")
            vCorreoVta = If((drlinea.Item("CorreoVta") Is DBNull.Value), "", Convert.ToString(drlinea.Item("CorreoVta")))  'drlinea.Item("CorreoVta")
            VEsAgente = IIf((drlinea.Item("Agte") Is DBNull.Value), 2, Convert.ToString(drlinea.Item("Agte")))   'drlinea.Item("Agte")
            vCCorreo = IIf((drlinea.Item("CCorreo") Is DBNull.Value), "", Convert.ToString(drlinea.Item("CCorreo")))  'drlinea.Item("CCorreo")

            NomUsuario = IIf((drlinea.Item("Nombre") Is DBNull.Value), "", Convert.ToString(drlinea.Item("Nombre")))
            IdUsuario = TxtUsuario.Text
          Catch ex As Exception
            MsgBox(ex.Message)
          End Try
        Loop

        'Aqui inicia la validacion del menu
        Me.RecorrerEstructuraMenu(Inicio.TSMenuInicio)


        'Inicio.ToolStripMenuItem2.Visible = False

        ''limites
        'If TxtUsuario.Text = "COBRANZ2" Or TxtUsuario.Text = "MANAGER" Then
        '  Inicio.DefinicionDeLimitesDeCreditoToolStripMenuItem.Visible = True
        'End If

        ''Administrador de cuentas
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "ACTAS" Then
        '  Inicio.AnalisisToolStripMenuItem.Visible = True
        '  Inicio.ErroresDeTimbradoToolStripMenuItem.Visible = True
        '  Inicio.ErroresDeTimbradoToolStripMenuItem.Enabled = True
        '  Inicio.ToolStripMenuItem2.Visible = True
        'End If

        ''Operacion vta
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "VENTAS1" Or TxtUsuario.Text = "VENTAS2" _
        '        Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "VENTAS4" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "VENTAS7" Or TxtUsuario.Text = "COMPRAS1" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "COBRANZ4" _
        '        Or TxtUsuario.Text = "COBRANZ5" Or TxtUsuario.Text = "COBRANZ1" _
        '        Or TxtUsuario.Text = "COBRANZ3" Or TxtUsuario.Text = "VVERGARA" _
        '        Or TxtUsuario.Text = "NGOMEZ" Or TxtUsuario.Text = "ASTRIDY" _
        '        Or TxtUsuario.Text = "VENTAS6" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" _
        '        Or TxtUsuario.Text = "COBRANZ2" Or TxtUsuario.Text = "VMERIDA" Then

        '  Inicio.EstatusOrdenToolStripMenuItem.Visible = True
        '  Inicio.EstatusOrdenToolStripMenuItem.Enabled = True

        'End If

        ''Surtimiento
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "ALMACEN1" _
        '        Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "ALMACENS" Or TxtUsuario.Text = "PCOMPRAS" Then


        '  Inicio.SurtirToolStripMenuItem.Visible = True
        '  Inicio.SurtirToolStripMenuItem.Enabled = True
        '  Inicio.SurtimientoToolStripMenuItem.Visible = True
        '  Inicio.SurtimientoToolStripMenuItem.Enabled = True
        'End If

        ''EMPAQUE
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "ALMACEN1" _
        '        Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "ALMACENE" Or TxtUsuario.Text = "PCOMPRAS" Then
        '  Inicio.SurtimientoToolStripMenuItem.Visible = True
        '  Inicio.SurtimientoToolStripMenuItem.Enabled = True
        '  Inicio.EmpaqueToolStripMenuItem.Visible = True
        '  Inicio.EmpaqueToolStripMenuItem.Enabled = True
        'End If
        ''Surtimiento
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "ALMACEN1" Or TxtUsuario.Text = "COMPRAS1" _
        '        Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "LMARTINEZ" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "AINVEN" Then

        '  Inicio.SurtimientoToolStripMenuItem.Visible = True
        '  Inicio.SurtimientoToolStripMenuItem.Enabled = True
        '  Inicio.EmpaqueToolStripMenuItem.Visible = True
        '  Inicio.EmpaqueToolStripMenuItem.Enabled = True
        '  Inicio.ConsultaModificaciónToolStripMenuItem.Visible = True
        '  Inicio.ConsultaModificaciónToolStripMenuItem.Enabled = True
        'End If


        ''Liberacion de Material
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "ALMACEN1" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" _
        '        Then
        '  Inicio.LiberacionDeEntregaToolStripMenuItem.Visible = True
        '  Inicio.LiberacionDeEntregaToolStripMenuItem.Enabled = True
        'End If

        ''Liberacion de Guias
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "LMARTINEZ" _
        '        Then
        '  Inicio.LiberacionDeGuiasToolStripMenuItem.Visible = True
        '  Inicio.LiberacionDeGuiasToolStripMenuItem.Enabled = True
        'End If

        ''Salida De Material
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "VIGILANCIA" _
        '        Then
        '  Inicio.SalidaDeMaterialToolStripMenuItem.Enabled = True
        '  Inicio.SalidaDeMaterialToolStripMenuItem.Visible = True
        'End If

        ''Seguimiento de Entrega
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "ALMACEN1" Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Then
        '  Inicio.SeguimientoDeEntregaToolStripMenuItem.Enabled = True
        '  Inicio.SeguimientoDeEntregaToolStripMenuItem.Visible = True

        'End If


        ''•	BACK ORDER
        ''o	Por recuperar


        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "AINVEN" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" _
        '        Or TxtUsuario.Text = "RMERCADO" Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "ATABASCO" _
        '        Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "PCOMPRAS" _
        '        Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMBoPorRec.Visible = True
        '  Inicio.SMBoPorRec.Enabled = True

        'End If

        ''o	Back Order - VENTAS

        'If TxtUsuario.Text = "OPERACIOND" Then
        '  Inicio.SMValInvPRO.Visible = True
        '  Inicio.SMValInvPRO.Enabled = True
        'End If

        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '            Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "COMERCIAL" Then

        '  Inicio.SMBOVentas.Visible = True
        '  Inicio.SMBOVentas.Enabled = True

        '  Inicio.SMValInvPRO.Visible = True
        '  Inicio.SMValInvPRO.Enabled = True

        '  Inicio.MAutos.Visible = True

        'End If

        ''o	Recuperado
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" _
        '        Or TxtUsuario.Text = "AINVEN" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" Then

        '  Inicio.SMBoRec.Visible = True
        '  Inicio.SMBoRec.Enabled = True

        'End If

        ''o	Registro de BO recuperado
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Then

        '  Inicio.SMCapBo.Visible = True
        '  Inicio.SMCapBo.Enabled = True

        'End If

        ''o	BOLineas

        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" _
        '        Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "AINVEN" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "RMERCADO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "VMERIDA" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '        Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMBOLineas.Visible = True
        '  Inicio.SMBOLineas.Enabled = True

        'End If

        ''•	COBRANZA
        ''o	Cobranza recuperada
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "COBRANZ1" Or TxtUsuario.Text = "COBRANZ2" Or TxtUsuario.Text = "COBRANZ4" _
        '        Or TxtUsuario.Text = "COBRANZ5" Or TxtUsuario.Text = "NGOMEZ" Or TxtUsuario.Text = "COBRANZ3" Then

        '  Inicio.SMCobRec.Visible = True
        '  Inicio.SMCobRec.Enabled = True

        'End If


        ''o	Cobranza cliente
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COBRANZ3" _
        '        Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "VVERGARA" _
        '        Or TxtUsuario.Text = "COBRANZ1" Or TxtUsuario.Text = "COBRANZ4" _
        '        Or TxtUsuario.Text = "COBRANZ5" Or TxtUsuario.Text = "NGOMEZ" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" Or TxtUsuario.Text = "RMERCADO" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "VMERIDA" _
        '        Or TxtUsuario.Text = "COBRANZ2" Or TxtUsuario.Text = "COMERCIAL" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMCobClientes.Visible = True
        '  Inicio.SMCobClientes.Enabled = True

        'End If

        ''o	Comisiones
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMERCIAL" Then

        '  Inicio.SMPagoCom.Visible = True
        '  Inicio.SMPagoCom.Enabled = True

        'End If


        ''o	Estatus del cliente
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COBRANZ3" _
        '        Or TxtUsuario.Text = "COBRANZ1" Or TxtUsuario.Text = "COBRANZ4" _
        '        Or TxtUsuario.Text = "COBRANZ5" Or TxtUsuario.Text = "NGOMEZ" Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "COBRANZ2" Or TxtUsuario.Text = "COMERCIAL" Then

        '  Inicio.SMEstatusCli.Visible = True
        '  Inicio.SMEstatusCli.Enabled = True

        'End If


        ''o	Notas de credito
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "COBRANZ3" Or TxtUsuario.Text = "COBRANZ2" Or TxtUsuario.Text = "CONTAB1" Then

        '  Inicio.SMNotCredito.Visible = True
        '  Inicio.SMNotCredito.Enabled = True


        'End If

        ''o	Antiguedad de saldos de clientes
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "COBRANZ3" _
        '            Or TxtUsuario.Text = "COBRANZ1" Or TxtUsuario.Text = "COBRANZ4" _
        '            Or TxtUsuario.Text = "COBRANZ5" Or TxtUsuario.Text = "NGOMEZ" Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COBRANZ2" _
        '            Or TxtUsuario.Text = "COMERCIAL" Then

        '  Inicio.SMAntiguedadCli.Visible = True
        '  Inicio.SMAntiguedadCli.Enabled = True


        'End If

        ''•	EMBARQUES
        ''o Fletes
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "LMARTINEZ" Or TxtUsuario.Text = "AINVEN" Then

        '  Inicio.SMFletes.Visible = True
        '  Inicio.SMFletes.Enabled = True

        'End If
        ''•	ORDEN DE VENTA
        ''o	Creadas por facturar
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "AINVEN" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" Or TxtUsuario.Text = "RMERCADO" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "LCEBALLOS" _
        '        Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "VMERIDA" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '        Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMCreadasPorFacturar.Visible = True
        '  Inicio.SMCreadasPorFacturar.Enabled = True

        'End If


        ''o	Registrar orden de venta
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "ABAJIO" _
        '        Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" Or TxtUsuario.Text = "RMERCADO" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "VMERIDA" _
        '        Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMOvtaCrearOV.Visible = True
        '  Inicio.SMOvtaCrearOV.Enabled = True

        'End If

        ''o Cotizacion
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" _
        '            Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "ABAJIO" _
        '        Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" Or TxtUsuario.Text = "RMERCADO" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "LCEBALLOS" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "ANCAR" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '        Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMCotizacion.Visible = True
        '  Inicio.SMCotizacion.Enabled = True

        'End If


        ''•	VENTAS
        ''o Agentes

        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "CONTAB1" _
        '        Or TxtUsuario.Text = "ACONTABLE" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMPRAS1" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" Or TxtUsuario.Text = "RMERCADO" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "VMERIDA" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '        Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMAgentes.Visible = True
        '  Inicio.SMAgentes.Enabled = True

        'End If

        ''o	Agente-Cliente
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMPRAS1" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" Or TxtUsuario.Text = "RMERCADO" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "VMERIDA" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '        Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMAgteClite.Visible = True
        '  Inicio.SMAgteClite.Enabled = True

        'End If

        ''o	Agente-Cliente(Rutas)
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" Or TxtUsuario.Text = "RMERCADO" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "VMERIDA" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '        Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMAgteClteRutas.Visible = True
        '  Inicio.SMAgteClteRutas.Enabled = True

        'End If


        ''o	Agente-Línea
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMPRAS1" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" Or TxtUsuario.Text = "RMERCADO" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "VMERIDA" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '        Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMAgteLinea.Visible = True
        '  Inicio.SMAgteLinea.Enabled = True

        'End If


        ''o	Devolución de Materiales
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" _
        '        Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '        Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '        Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '        Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" Or TxtUsuario.Text = "RMERCADO" _
        '        Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "VMERIDA" _
        '        Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" _
        '        Or TxtUsuario.Text = "COMERCIAL" Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Then

        '  Inicio.SMDevoluciones.Visible = True
        '  Inicio.SMDevoluciones.Enabled = True

        'End If


        ''o	Líneas 
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "COMERCIAL" Then

        '  Inicio.SMVtaLineas.Visible = True
        '  Inicio.SMVtaLineas.Enabled = True

        'End If

        ''o  Ventas Netas Totales x Mes
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '        Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMERCIAL" Then
        '  Inicio.SMVentasTotales.Visible = True
        '  Inicio.SMVentasTotales.Enabled = True
        'End If


        ''o	Venta caída 
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMERCIAL" Then

        '  Inicio.SMVentaCaida.Visible = True
        '  Inicio.SMVentaCaida.Enabled = True

        'End If


        ''o	Ranking de líneas
        'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMERCIAL" Then

        '  Inicio.SMRankingLineas.Visible = True
        '  Inicio.SMRankingLineas.Enabled = True

        'End If

        '  'o	ScoreCard
        '  'se modifico para que no se escriban los accesos en codigo duro
        '  'Modifico Ivan Gonzalez
        '  Dim SQL As New Comandos_SQL()
        '  SQL.conectarTPM()
        '  Dim CodAgte As String = SQL.CampoEspecifico("SELECT CodAgte FROM Usuarios where Id_Usuario = '" + TxtUsuario.Text + "'", "CodAgte")
        '  Dim AgteVentas As String = SQL.CampoEspecifico("SELECT AgteVentas FROM Usuarios where Id_Usuario = '" + TxtUsuario.Text + "'", "AgteVentas")
        '  SQL.Cerrar()
        '  If CodAgte <> "" Or AgteVentas <> "" Then

        '    Inicio.SMScoreCard.Visible = True
        '    Inicio.SMScoreCard.Enabled = True

        '  End If
        '  'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMERCIAL" _
        '  '    Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "RMERCADO" Then

        '  '    Inicio.SMScoreCard.Visible = True
        '  '    Inicio.SMScoreCard.Enabled = True

        '  'End If

        '  'o	Líneas Halcon             
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMERCIAL" Then

        '    Inicio.SMLíneasHalcon.Visible = True
        '    Inicio.SMLíneasHalcon.Enabled = True

        '  End If

        '  'o	Líneas Objetivo
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMERCIAL" Then

        '    Inicio.SMLíneasObjetivo.Visible = True
        '    Inicio.SMLíneasObjetivo.Enabled = True

        '  End If


        '  'o	Pedio sugerido
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMERCIAL" Then

        '    Inicio.SMPedidoSugerido.Visible = True
        '    Inicio.SMPedidoSugerido.Enabled = True

        '  End If


        '  'o	Descuentos
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "COMERCIAL" Then

        '    Inicio.SMDescuentos.Visible = True
        '    Inicio.SMDescuentos.Enabled = True

        '  End If

        '  'o	Envio de facturas
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Then
        '    Inicio.SMEnvioFacturas.Visible = True
        '    Inicio.SMEnvioFacturas.Enabled = True
        '  End If

        '  'o	Ventas Cliente Mensual
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COBRANZ3" Or TxtUsuario.Text = "COMERCIAL" _
        '          Or TxtUsuario.Text = "COBRANZ2" Then
        '    Inicio.SMVtaCliMen.Visible = True
        '    Inicio.SMVtaCliMen.Enabled = True
        '  End If

        '  'o	Reporte Tracking Diario
        '  'SE MODIFICO PARA QUE PUEDAN ENTRAR LOS AGENTES DE VENTAS
        '  'MODIFICADO POR IVAN GONZALEZ
        '  'Dim SQL As New Comandos_SQL()
        '  SQL.conectarTPM()
        '  Dim CodAgente As String = SQL.CampoEspecifico("select CodAgte from Usuarios where Id_Usuario = '" + UsrTPM + "'", "CodAgte")
        '  SQL.Cerrar()
        '  If CodAgte <> "" Or TxtUsuario.Text = "MANAGER" Then
        '    Inicio.SMReporteTracking.Enabled = True
        '    Inicio.SMReporteTracking.Visible = True
        '  End If

        '  'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMERCIAL" Or TxtUsuario.Text = "RROBLES" Then
        '  '    Inicio.SMReporteTracking.Enabled = True
        '  '    Inicio.SMReporteTracking.Visible = True
        '  'End If

        '  'o	Estatus Ord. Venta
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "RHUMANOS" Or TxtUsuario.Text = "VENTAS2" _
        '          Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" Or TxtUsuario.Text = "COMERCIAL" Then
        '    Inicio.SMEstatusOrdVenta.Enabled = True
        '    Inicio.SMEstatusOrdVenta.Visible = True
        '  End If

        '  ' Solicitud de Cancelaciones

        '  'OBTIENE TODOS LOS USUARIOS CON DERECHOSW
        '  Try 'CAPTURA EL ERROR
        '    'CONECTA A LA BASE DE DATOS DEL TPD
        '    conexion_universal.conectar()
        '    'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS
        '    conexion_universal.slq_s = New SqlCommand("SELECT * FROM Documents_Accesos WHERE Id_Usuario = '" + TxtUsuario.Text + "' AND Modulo = 'Y0'", conexion_universal.conexion_uni)
        '    'EJECUTA LA CONSULTA
        '    conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
        '    'RECORRE LA CONSULTA
        '    If conexion_universal.rd_s.Read() Then
        '      'CONSULTAR
        '      Inicio.SolicitudDeCancelaciones.Enabled = True
        '      Inicio.SolicitudDeCancelaciones.Visible = True

        '      If TxtUsuario.Text = "MANAGER" Then
        '        Inicio.SolicitudDeCancelaciones.Enabled = False
        '        Inicio.SolicitudDeCancelaciones.Visible = False
        '      End If

        '    End If
        '    conexion_universal.rd_s.Close()
        '    'CIERRA LA CONEXION
        '    conexion_universal.cerrar_conectar()
        '  Catch ex As Exception
        '    'MsgBox("Error de Acceso al apartado d del TPD: " & ex.Message, MsgBoxStyle.Critical)
        '    conexion_universal.cerrar_conectar()
        '    'Me.Close()
        '    'Return
        '  End Try 'FIN CAPTURA EL ERROR

        '  'If TxtUsuario.Text = "MANAGER" _
        '  'Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "AINVEN" _
        '  'Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '  'Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '  'Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "ATUXTLA" _
        '  'Or TxtUsuario.Text = "RMERCADO" Or TxtUsuario.Text = "ACASTRO" Or TxtUsuario.Text = "JSANCHEZ" _
        '  'Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "ATABASCO" _
        '  'Or TxtUsuario.Text = "VMERIDA" _
        '  'Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Then
        '  '    Inicio.SolicitudDeCancelaciones.Enabled = True
        '  '    Inicio.SolicitudDeCancelaciones.Visible = True
        '  'End If

        '  'SOLICITUD DE ARTICULOS ESPECIALES
        '  If TxtUsuario.Text = "COMERCIAL" Then
        '    Inicio.SolDeArticulosEspecialesToolStripMenuItem.Visible = True
        '    Inicio.SolDeArticulosEspecialesToolStripMenuItem.Enabled = True
        '  End If

        '  'Envio de facturas por cliente
        '  If TxtUsuario.Text = "MANAGER" _
        '          Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "AINVEN" _
        '          Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '          Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '          Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" _
        '          Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "COBRANZ2" Or TxtUsuario.Text = "COBRANZ4" Or TxtUsuario.Text = "COBRANZ5" _
        '          Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "NGOMEZ" Or TxtUsuario.Text = "CONTAB1" _
        '          Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '          Or TxtUsuario.Text = "VMERIDA" Then
        '    Inicio.SMEnvioFacturasCli.Enabled = True
        '    Inicio.SMEnvioFacturasCli.Visible = True
        '  End If

        '  'Visita Agente

        '  'Envio de facturas por cliente
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "VENTAS4" _
        '          Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" _
        '          Or TxtUsuario.Text = "RLIRA" Or TxtUsuario.Text = "ASTRIDY" _
        '          Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "RROBLES" _
        '            Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "COBRANZ2" _
        '          Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '          Or TxtUsuario.Text = "VMERIDA" Then

        '    Inicio.RegistroDeVisitasDeAgentesToolStripMenuItem.Enabled = True
        '    Inicio.RegistroDeVisitasDeAgentesToolStripMenuItem.Visible = True
        '  End If



        '  '•	LISTAS
        '  'o	De precio
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" _
        '          Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "AINVEN" _
        '          Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '          Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '          Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" _
        '          Or TxtUsuario.Text = "RMERCADO" Or TxtUsuario.Text = "ABAJIO" Or TxtUsuario.Text = "JSANCHEZ" Or TxtUsuario.Text = "AFUENTES" _
        '          Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" _
        '          Or TxtUsuario.Text = "LCEBALLOS" Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "ANCAR" _
        '          Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VENTAS10" Or TxtUsuario.Text = "VENTAS11" Or TxtUsuario.Text = "COMERCIAL" _
        '          Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "AVERACRUZ" Or TxtUsuario.Text = "SINERGIA" Then

        '    Inicio.SMListPrecio.Visible = True
        '    Inicio.SMListPrecio.Enabled = True

        '  End If

        '  'o	De inventario
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "AINVEN" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "COMERCIAL" Then
        '    Inicio.SMListInv.Visible = True
        '    Inicio.SMListInv.Enabled = True

        '  End If



        '  '•	COMPRAS
        '  If TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "LMARTINEZ" _
        '              Or TxtUsuario.Text = "ARAMOS" Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Or TxtUsuario.Text = "COBRANZ3" _
        '              Or TxtUsuario.Text = "COBRANZ4" Or TxtUsuario.Text = "NGOMEZ" Or TxtUsuario.Text = "COBRANZ5" Or TxtUsuario.Text = "COBRANZ2" Or TxtUsuario.Text = "ASTRIDY" Then
        '    Inicio.MCompras.Visible = True
        '  End If


        '  'o	Pagos a proveedores
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" _
        '          Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Or TxtUsuario.Text = "AINVEN" Then

        '    Inicio.SMPagoProv.Visible = True
        '    Inicio.SMPagoProv.Enabled = True

        '  End If


        '  'o	Recibo de materiales
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" _
        '          Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "LMARTINEZ" Or TxtUsuario.Text = "ARAMOS" _
        '          Or TxtUsuario.Text = "VOTNIEL" Or TxtUsuario.Text = "MCHABLE" Or TxtUsuario.Text = "AINVEN" Or TxtUsuario.Text = "PCOMPRAS" Then

        '    Inicio.SMRecibosMat.Visible = True
        '    Inicio.SMRecibosMat.Enabled = True

        '  End If

        '  'o	Análisis de compras
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "AINVEN" Or TxtUsuario.Text = "PCOMPRAS" Then

        '    Inicio.SMAnalCompras.Visible = True
        '    Inicio.SMAnalCompras.Enabled = True

        '  End If


        '  'o	Diferencias en precios
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Then

        '    Inicio.SMDifPrecios.Visible = True
        '    Inicio.SMDifPrecios.Enabled = True

        '  End If

        '  'o	Factores 
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Then

        '    Inicio.SMFactores.Visible = True
        '    Inicio.SMFactores.Enabled = True

        '  End If

        '  'o	Traspaso entre almacenes
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "PCOMPRAS" Then

        '    Inicio.SMTraspaso.Visible = True
        '    Inicio.SMTraspaso.Enabled = True

        '  End If

        '  'o	Solicitud de artículos
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "PCOMPRAS" Then

        '    Inicio.SMSolArt.Visible = True
        '    Inicio.SMSolArt.Enabled = True

        '  End If

        '  'o	Boletin
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Then

        '    Inicio.SMBoletin.Visible = True
        '    Inicio.SMBoletin.Enabled = True

        '  End If

        '  'o	Categorias
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.SMCategorias.Visible = True
        '    Inicio.SMCategorias.Enabled = True

        '  End If

        '  'o	Recibo de Mat Cal

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" _
        '          Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "VVERGARA" _
        '          Or TxtUsuario.Text = "MCHABLE" Or TxtUsuario.Text = "VOTNIEL" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "VENTAS2" _
        '            Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" Or TxtUsuario.Text = "COMERCIAL" Then

        '    Inicio.MCompras.Visible = True
        '    Inicio.SMRecMatCal.Visible = True
        '    Inicio.SMRecMatCal.Enabled = True

        '  End If




        '  'o	Garantias

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '              Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "AINVEN" Or TxtUsuario.Text = "COBRANZ3" _
        '              Or TxtUsuario.Text = "COBRANZ4" Or TxtUsuario.Text = "NGOMEZ" Or TxtUsuario.Text = "COBRANZ5" _
        '              Or TxtUsuario.Text = "ACONTABLE" Or TxtUsuario.Text = "CONTAB1" _
        '              Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "ALMACEN1" Or TxtUsuario.Text = "MCHABLE" Or TxtUsuario.Text = "VOTNIEL" Or TxtUsuario.Text = "PCOMPRAS" _
        '              Or TxtUsuario.Text = "COBRANZ2" _
        '              Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" Or TxtUsuario.Text = "COMERCIAL" _
        '              Or TxtUsuario.Text = "ASTRIDY" Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "VENTAS8" _
        '              Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "VENTAS1" Then

        '    Inicio.SMGarantias.Visible = True
        '    Inicio.SMGarantias.Enabled = True

        '  End If


        '  ''o	Articulos en boletin

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "COMPRAS1" Then

        '    Inicio.SMBoletin2.Visible = True
        '    Inicio.SMBoletin2.Enabled = True

        '  End If

        '  ''o	Salida de Materiales

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "COMPRAS1" _
        '             Or TxtUsuario.Text = "AINVEN" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.SMSalidas.Visible = True
        '    Inicio.SMSalidas.Enabled = True

        '  End If

        '  ''o	Analisis Vtas GRAFICP

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "PCOMPRAS" Then

        '    Inicio.SMAnalisisGraf.Visible = True
        '    Inicio.SMAnalisisGraf.Enabled = True

        '  End If
        '  '' o Partidas Abiertas
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "PCOMPRAS" Then
        '    Inicio.MCompras.Visible = True
        '    Inicio.SMPartidasAbiertas.Visible = True
        '    Inicio.SMPartidasAbiertas.Enabled = True
        '  End If

        '  'ACCESO AL APARTADO DE ARTICULOS ESPECIALES
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" _
        '              Or TxtUsuario.Text = "VENTAS1" Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" _
        '              Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "VENTAS8" _
        '              Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "RLIRA" Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "VMERIDA" Then
        '    Inicio.SolArticulosEspecialesToolStripMenuItem.Visible = True
        '    Inicio.SolArticulosEspecialesToolStripMenuItem.Enabled = True
        '  End If


        '  '•	INVENTARIO
        '  'o	Auditoria de Inventario  
        '  'se comento Or TxtUsuario.Text = "MMAZZOCO"
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '            Or TxtUsuario.Text = "COMPRAS1" _
        '          Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "CONTAB1" _
        '          Or TxtUsuario.Text = "AINVEN" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.SMAuditoriaInv.Visible = True
        '    Inicio.SMAuditoriaInv.Enabled = True

        '  End If


        '  'o	Inventario
        '  'se comento Or TxtUsuario.Text = "MMAZZOCO"
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '            Or TxtUsuario.Text = "COMPRAS1" _
        '          Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "AINVEN" _
        '          Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '          Or TxtUsuario.Text = "MCHABLE" Or TxtUsuario.Text = "VOTNIEL" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.SMInventarioM.Visible = True
        '    Inicio.SMInventarioM.Enabled = True

        '  End If

        '  'Inventario
        '  'o Informe de auditoria de stock
        '  'se comento  Or TxtUsuario.Text = "MMAZZOCO" 
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "COMPRAS1" _
        '          Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "AINVEN" _
        '          Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '          Or TxtUsuario.Text = "MCHABLE" Or TxtUsuario.Text = "VOTNIEL" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.SMAuditoriaStock.Visible = True
        '    Inicio.SMAuditoriaStock.Enabled = True

        '  End If

        '  'o Facturas
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "COMPRAS1" _
        '          Or TxtUsuario.Text = "ACOMPRAS" _
        '          Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '          Or TxtUsuario.Text = "MCHABLE" Or TxtUsuario.Text = "VOTNIEL" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.SMFactura.Visible = True
        '    Inicio.SMFactura.Enabled = True

        '  End If

        '  'o Notas de credito
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "COMPRAS1" _
        '          Or TxtUsuario.Text = "ACOMPRAS" _
        '          Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '          Or TxtUsuario.Text = "MCHABLE" Or TxtUsuario.Text = "VOTNIEL" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.SMNotasC.Visible = True
        '    Inicio.SMNotasC.Enabled = True

        '  End If

        '  'o Transferencia de stock
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '          Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "COMPRAS1" _
        '          Or TxtUsuario.Text = "ACOMPRAS" _
        '          Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "RROBLES" _
        '          Or TxtUsuario.Text = "MCHABLE" Or TxtUsuario.Text = "VOTNIEL" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.SMTransferencia.Visible = True
        '    Inicio.SMTransferencia.Enabled = True

        '  End If

        '  'o Valoración de inventarios por almacen

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "OPERACIOND" Then
        '    Inicio.SMValoracionInv.Visible = True
        '    Inicio.SMValoracionInv.Enabled = True
        '  End If


        '  'o Valoración de inventarios por producto

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "OPERACIOND" Then
        '    'Inicio.SMValoracionInv.Visible = True
        '    'Inicio.SMValoracionInv.Enabled = True
        '  End If

        '  ' 'Operacion vta
        '  ' 'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '  ' If TxtUsuario.Text = "PRUEBAS" _
        '  'Or TxtUsuario.Text = "COMPRAS1" _
        '  'Or TxtUsuario.Text = "VENTAS1" Or TxtUsuario.Text = "VENTAS2" _
        '  'Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "VENTAS4" _
        '  'Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "VENTAS7" _
        '  'Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "RLIRA" _
        '  'Or TxtUsuario.Text = "VENTAS6" Or TxtUsuario.Text = "DDORANTES" Then

        '  '     Inicio.OperacionToolStripMenuItem.Visible = True
        '  '     Inicio.OperacionToolStripMenuItem.Enabled = True
        '  '     Inicio.ProcesoDeOrdenVtaToolStripMenuItem.Visible = True
        '  '     Inicio.ProcesoDeOrdenVtaToolStripMenuItem.Enabled = True

        '  ' End If

        '  ' If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '  'Or TxtUsuario.Text = "COMPRAS1" Then

        '  '     Inicio.OperacionToolStripMenuItem.Visible = True
        '  '     Inicio.OperacionToolStripMenuItem.Enabled = True
        '  '     Inicio.ProcesoDeOrdenVtaToolStripMenuItem.Visible = True
        '  '     Inicio.ProcesoDeOrdenVtaToolStripMenuItem.Enabled = True

        '  ' End If

        '  ' 'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "VENTAS1" Or TxtUsuario.Text = "VENTAS2" _
        '  ' If TxtUsuario.Text = "VENTAS1" Or TxtUsuario.Text = "VENTAS2" _
        '  'Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "VENTAS4" _
        '  'Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "VENTAS7" _
        '  'Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "RLIRA" _
        '  'Or TxtUsuario.Text = "VENTAS6" Or TxtUsuario.Text = "DDORANTES" Then

        '  '     Inicio.OperacionToolStripMenuItem.Visible = True
        '  '     Inicio.OperacionToolStripMenuItem.Enabled = True
        '  '     Inicio.SurtimientoToolStripMenuItem.Visible = False
        '  '     Inicio.SurtimientoToolStripMenuItem.Enabled = False
        '  '     Inicio.ProcesoDeOrdenVtaToolStripMenuItem.Visible = True
        '  '     Inicio.ProcesoDeOrdenVtaToolStripMenuItem.Enabled = True

        '  ' End If


        '  'Inventario/Surtir
        '  ' If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '  'Or TxtUsuario.Text = "AINVEN" _
        '  'Or TxtUsuario.Text = "ALMACEN1" Or TxtUsuario.Text = "MCHABLE" _
        '  'Or TxtUsuario.Text = "VENTAS6" Then

        '  '     Inicio.SurtimientoToolStripMenuItem.Visible = True
        '  '     Inicio.SurtimientoToolStripMenuItem.Enabled = True


        '  ' End If
        '  'Valoracion inventario por linea
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.ValorDelInventarioPorLineaToolStripMenuItem.Visible = True
        '    Inicio.ValorDelInventarioPorLineaToolStripMenuItem.Enabled = True

        '  End If

        '  'o Control de envíos

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "COMPRAS1" Then
        '    Inicio.SMControlEnvios.Visible = True
        '    Inicio.SMControlEnvios.Enabled = True
        '  End If

        '  'o Detalle Facturas-Articulos

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "AINVEN" Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "PCOMPRAS" Or TxtUsuario.Text = "OPERACIOND" Then
        '    Inicio.SMFacturaArticulos.Visible = True
        '    Inicio.SMFacturaArticulos.Enabled = True
        '  End If

        '  '•	CONTABILIDAD
        '  'o	Facturas y NC Contabilidad

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Then

        '    Inicio.SMFacturasXmlConta.Visible = True
        '    Inicio.SMFacturasXmlConta.Enabled = True

        '  End If

        '  'o	Facturas y NC Compras

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '            Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Then

        '    Inicio.SMFacturasXmlComp.Visible = True
        '    Inicio.SMFacturasXmlComp.Enabled = True

        '  End If

        '  'o	AGENTES

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '            Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Then

        '    Inicio.SMAgentesConta.Visible = True
        '    Inicio.SMAgentesConta.Enabled = True

        '  End If

        '  '•	RECURSOS HUMANOS

        '  'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '  'Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Or TxtUsuario.Text = "RHUMANOS" Then

        '  '    Inicio.MVacaciones.Visible = True

        '  'End If

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" Or TxtUsuario.Text = "RHUMANOS" Then
        '    Inicio.SMAltaEmp.Visible = True
        '    Inicio.SMConsultaVac.Visible = True
        '    Inicio.SMSolicitudVac.Visible = True

        '    Inicio.SMAltaEmp.Enabled = True
        '    Inicio.SMConsultaVac.Enabled = True
        '    Inicio.SMSolicitudVac.Enabled = True
        '  End If

        '  If TxtUsuario.Text = "RHUMANOS" Then
        '    Inicio.MCompras.Visible = False
        '    'Inicio.MInventario.Visible = False
        '    Inicio.MInventario.Visible = True
        '    Inicio.MContabilidad.Visible = False
        '    Inicio.MAutos.Visible = False

        '    Inicio.SMAltaEmp.Enabled = True
        '    Inicio.SMConsultaVac.Enabled = True
        '    Inicio.SMSolicitudVac.Enabled = True
        '  End If

        '  If TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Then
        '    Inicio.SMConsultaVac.Visible = True
        '    Inicio.SMConsultaVac.Enabled = True
        '  End If

        '  If TxtUsuario.Text = "CONTAB1" Then
        '    Inicio.SMAltaEmp.Visible = True
        '    Inicio.SMConsultaVac.Visible = True
        '    Inicio.SMSolicitudVac.Visible = True

        '    Inicio.SMAltaEmp.Enabled = True
        '    Inicio.SMConsultaVac.Enabled = True
        '    Inicio.SMSolicitudVac.Enabled = True
        '  End If

        '  'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "RHUMANOS" Then
        '  '    Inicio.SMOrganigrama.Visible = True
        '  '    Inicio.SMOrganigrama.Enabled = True
        '  'End If

        '  'If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "RHUMANOS" Then
        '  '    Inicio.SMPerfilesDePuesto.Visible = True
        '  '    Inicio.SMPerfilesDePuesto.Enabled = True
        '  'End If

        '  '----------------------------------------
        '  '----------------------------------------

        '  If VEsAgente = 1 Or TxtUsuario.Text = "NGOMEZ" Or TxtUsuario.Text = "COBRANZ5" Then
        '    Inicio.SMCobClientes.Enabled = True
        '    Inicio.SMBoPorRec.Enabled = True
        '    Inicio.SMBoRec.Enabled = True
        '    'PARA CARGA Y BITACORA DE COMBISTIBLE
        '    Inicio.MAutos.Enabled = True
        '    Inicio.MAutos.Visible = True
        '  End If
        '  'PARA CARGA Y BITACORA DE COMBISTIBLE
        '  If TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "AVERACRUZ" Then
        '    Inicio.MAutos.Enabled = True
        '    Inicio.MAutos.Visible = True
        '  End If
        '  'Inventario
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '            Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "AINVEN" _
        '            Or TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "ALMACEN1" _
        '            Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "VOTNIEL" Or TxtUsuario.Text = "MCHABLE" _
        '            Or TxtUsuario.Text = "VENTAS1" Or TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" _
        '            Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "RLIRA" _
        '            Or TxtUsuario.Text = "VENTAS6" Or TxtUsuario.Text = "VENTAS8" _
        '            Or TxtUsuario.Text = "AINVEN" Or TxtUsuario.Text = "DDORANTES" _
        '            Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "PCOMPRAS" _
        '            Or TxtUsuario.Text = "VMERIDA" Or TxtUsuario.Text = "OPERACIOND" Then

        '    Inicio.MCompras.Visible = True
        '    Inicio.MInventario.Visible = True

        '  End If

        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PRUEBAS" _
        '            Or TxtUsuario.Text = "COMPRAS1" Or TxtUsuario.Text = "ACOMPRAS" Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Then

        '    Inicio.MContabilidad.Visible = True
        '  End If

        '  'Control de acceso para el menu "Alta de Vehiculo" dentro de la barra de menu "Control de Vehiculos"'
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Then
        '    Inicio.SMAutoAlta.Visible = True
        '  End If

        '  If TxtUsuario.Text = "VENTAS2" Or TxtUsuario.Text = "VENTAS3" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "RLIRA" Then
        '    Inicio.MCompras.Visible = True
        '    Inicio.SMSolArt.Visible = True
        '    Inicio.SMSolArt.Enabled = True
        '    'Inicio.MCompras.Visible = True
        '  End If

        '  '*********************************************************************************************

        '  If TxtUsuario.Text = "VENTAS4" Or TxtUsuario.Text = "VENTAS5" Or TxtUsuario.Text = "AMERIDA" Or TxtUsuario.Text = "ASTRIDY" _
        '          Or TxtUsuario.Text = "RJIMENEZ" Or TxtUsuario.Text = "ATABASCO" Or TxtUsuario.Text = "CSANTOS" Or TxtUsuario.Text = "ATUXTLA" Or TxtUsuario.Text = "VVERGARA" Or TxtUsuario.Text = "VMERIDA" _
        '          Or TxtUsuario.Text = "VENTAS8" Or TxtUsuario.Text = "VENTAS9" Or TxtUsuario.Text = "VMERIDA" Then
        '    Inicio.SMCotizacion.Visible = True
        '    Inicio.SMCotizacion.Enabled = True
        '  End If

        '  If TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "ACONTABLE" Or TxtUsuario.Text = "MANAGER" Then
        '    Inicio.FacturasCanceladas.Visible = True
        '  End If

        '  'PACKING'
        '  If TxtUsuario.Text = "MANAGER1111" Or TxtUsuario.Text = "OPERACIOND" Then
        '    'Inicio.FacturasCanceladas.Visible = True
        '    Inicio.PackinToolStripMenuItem.Enabled = True
        '    Inicio.PackinToolStripMenuItem.Visible = True

        '    'Inicio.SMCheckIn.Enabled = True
        '    'Inicio.SMCheckIn.Visible = True
        '  End If

        '  'ARTICULOS REMATE
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "AINVEN" Or TxtUsuario.Text = "COMERCIAL" Then
        '    Inicio.ArticulosRemateToolStripMenuItem.Enabled = True
        '    Inicio.ArticulosRemateToolStripMenuItem.Visible = True
        '  End If

        '  'CONTROL DE VISITAS CLIENTES (VENTAS)
        '  If TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "DDORANTES" Or TxtUsuario.Text = "RROBLES" Or TxtUsuario.Text = "VVERGARA" _
        '              Or TxtUsuario.Text = "LMARTINEZ" Or TxtUsuario.Text = "CONTAB1" Or TxtUsuario.Text = "COMERCIAL" Then
        '    Inicio.SMCheckIn.Enabled = True
        '    Inicio.SMCheckIn.Visible = True
        '  End If

        '  If TxtUsuario.Text = "CONTAB1" Then
        '    Inicio.SMAntiguedadCli.Visible = True
        '    Inicio.SMAntiguedadCli.Enabled = True
        '  End If
        '  If TxtUsuario.Text = "ANCAR" Then
        '    Inicio.SMOrganigrama.Visible = False
        '    Inicio.SMOrganigrama.Enabled = False
        '    Inicio.SMPerfilesDePuesto.Visible = False
        '    Inicio.SMPerfilesDePuesto.Enabled = False
        '  End If

        '  'reporte f,nc,p sin cancelados
        '  If TxtUsuario.Text = "CONTAB1" And TxtUsuario.Text = "MANAGER" Then
        '    Inicio.FNCPToolStripMenuItem.Visible = True
        '    Inicio.FNCPToolStripMenuItem.Enabled = True
        '  End If

        '  'UsrTPM = Me.TxtUsuario.Text
        '  mObtenDatosPrincipales(Me.TxtUsuario.Text)

        '  If VEsAgente = 1 Then
        '    Inicio.SMOvtaCrearOV.Enabled = True
        '    Inicio.SMOvtaCrearOV.PerformClick()
        '  End If


        '  If (DateTime.DaysInMonth(FchServer.Year, FchServer.Month) - FchServer.Day) <= 7 And UsrTPM <> "MANAGER" And UsrTPM <> "PRUEBAS" _
        '              And UsrTPM <> "COMPRAS1" And UsrTPM <> "ACOMPRAS" And UsrTPM <> "Ventas1" And UsrTPM <> "DDORANTES" _
        '              And UsrTPM <> "CONTAB1" And TxtUsuario.Text <> "ACONTABLE" And TxtUsuario.Text <> "RROBLES" And TxtUsuario.Text <> "COMERCIAL" Then

        '    Inicio.SMAgentes.Enabled = False
        '    Inicio.SMAgteClite.Enabled = False
        '    Inicio.SMAgteLinea.Enabled = False
        '    Inicio.SMAgteClteRutas.Enabled = False
        '  End If

        '  'If UsrTPM <> "MANAGER" And UsrTPM <> "PRUEBAS" _
        '  '    And UsrTPM <> "COMPRAS1" And UsrTPM <> "ACOMPRAS" And UsrTPM <> "Ventas1" And UsrTPM <> "DDORANTES" _
        '  '    And UsrTPM <> "CONTAB1" And TxtUsuario.Text <> "ACONTABLE" Then

        '  '    Inicio.SMAgentes.Enabled = False
        '  '    Inicio.SMAgteClite.Enabled = False
        '  '    Inicio.SMAgteLinea.Enabled = False
        '  '    Inicio.SMAgteClteRutas.Enabled = False
        '  'End If

        '  If TxtUsuario.Text = "MMAZZOCO" Or TxtUsuario.Text = "OPERACIOND" Or TxtUsuario.Text = "MANAGER" Or TxtUsuario.Text = "PCOMPRAS" Then
        '    Inicio.BarCodeToolStripMenuItem.Visible = True
        '  End If

        '  varsalir = True
        '  SQL.Cerrar()
        '  'cmdlinea1.Connection.Close()
        '  Me.Close()
      End If

    Catch ex As Exception
      MessageBox.Show("AL CONSULTAR A LOS USUARIOS " + Convert.ToString(ex), " E R R O R ! ! !", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return
    End Try
  End Sub
End Class