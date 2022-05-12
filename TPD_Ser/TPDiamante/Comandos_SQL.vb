Imports System.Data.SqlClient
Imports System.Management

Public Class Comandos_SQL
    'CONEXION PRINCIPAL
    Public conec As New SqlConnection
    Public cmd As New SqlCommand
    Public adaptador As New SqlDataAdapter
  Public disk As ManagementObject

  Dim Lista As ArrayList
    Dim dr As SqlDataReader
    Dim bandera As Integer
    Dim HDD As String

    Public ds As New DataSet
    Public dv As New DataView

    Public Consulta As String
  Public StrTpm As String = conexion_universal.CadenaSQL


  'CONECTAR A CUALQUIER BASE DE DATOS.
  Public Function conectarSBO_TPD()
    conec = New SqlConnection(conexion_universal.CadenaSQLSAP)
    Try
            conec.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

  'CONECTAR A CUALQUIER BASE DE DATOS.
  Public Function conectarTPM()
    conec = New SqlConnection(conexion_universal.CadenaSQL)
    Try
      conec.Open()
      Return True
    Catch ex As Exception
      Return False
    End Try
  End Function

  'CERRAR CUALQUIER CONEXION ABIERTA
  Public Sub Cerrar()
    conec.Close()
  End Sub

  ''CONSULTA Y DEVUELVE CUALQUIER TABLA
  'Public Function ConsultarTabla(Consulta As String)
  '    conec.Close()
  '    conec.Open()
  '    Try
  '        adaptador = New SqlDataAdapter(Consulta, conec)
  '        adaptador.Fill(ds)
  '        Return ds.Tables(0)
  '    Catch ex As Exception
  '        Return False
  '    End Try
  'End Function

  'CONSULTA UN DATO PARA VERIFICAR SI EXISTE EN BD SEGUN LA CONSULTA
  Public Function SiExiste(Consulta As String)
    conec.Close()
    conec.Open()
    Try
      cmd = New SqlCommand(Consulta, conec)
      dr = cmd.ExecuteReader()
      If dr.Read() Then
        dr.Close()
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      Return False
    End Try
  End Function

  'OBTIENE EL CAMPO DESEADO SEGUN LA CONSULTA
  Public Function CampoEspecifico(Consulta As String, Campo As String)
    conec.Close()
    conec.Open()
    Try
      cmd = New SqlCommand(Consulta, conec)
      dr = cmd.ExecuteReader()
      While dr.Read()
        Return dr(Campo).ToString()
      End While
      Return False
    Catch ex As Exception
      Return False
    End Try
  End Function

  'LLENA CUALQUIER COMBOBOX DEPENDIENDO LA CONSULTA
  Public Function LlenarComboBox(Consulta As String)
    conec.Close()
    conec.Open()
    Try
      cmd = New SqlCommand(Consulta, conec)
      dr = cmd.ExecuteReader()
      While dr.Read()
        Lista.Add(dr.GetString(0))
      End While
      Return Lista
    Catch ex As Exception
      Return False
    End Try
  End Function

  'EJECUTA CONSULTAS DE TIPO INSERT, UPDATE, DELETE
  Public Function EjecutarComando(Consulta As String)
    Try
      conec.Close()
      cmd = New SqlCommand(Consulta, conec)
      conec.Open()
      bandera = cmd.ExecuteNonQuery()
      conec.Close()
      If bandera >= 1 Then
        Return True
      End If
      Return False
    Catch ex As Exception
      Return False
    End Try
  End Function

  'OBTIENE EL SERIAL DEL ORDENADOR
  'Public Function GetProcessorId()
  '  Dim manClass As ManagementClass = New ManagementClass("Win32_Processor")
  '  Dim manObjCol As ManagementObjectCollection = manClass.GetInstances()
  '  Dim ProcessorId As String = String.Empty
  '  For Each manObj As ManagementObject In manObjCol
  '    ProcessorId = manObj.Properties("ProcessorId").Value.ToString()
  '  Next
  '  Return ProcessorId
  'End Function

  'EJECUTAR PROCEDIMIENTO ALMACENADO DEVUELVE DATAVIEW
  Public Function EjecutarProcedimiento(NombreProcedimiento As String, NombreParametros As String, NumeroParametros As Integer, ValorParametro As String)
    Try
      conec = New SqlConnection(StrTpm)
      cmd = New SqlCommand(NombreProcedimiento, conec)
      cmd.CommandType = CommandType.StoredProcedure
      ds = New DataSet()

      If NumeroParametros <> 0 Then
        For index = 1 To NumeroParametros
          Dim arrayN As Array = NombreParametros.Split(",")
          Dim arrayV As Array = ValorParametro.Split(",")
          cmd.Parameters.AddWithValue(arrayN(index - 1), arrayV(index - 1))
        Next
      End If

      conec.Open()
      adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()
      adaptador.Fill(ds)
      dv = New DataView
      dv.Table = ds.Tables(0)
      Return dv.Table
    Catch ex As Exception
      MessageBox.Show("¡Error al ejecutar procedimiento almacenado: " + NombreProcedimiento + Environment.NewLine + ex.ToString() + "!", "¡Error en EjecutarProcedimiento!", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    End Try
  End Function

  'EJECUTAR PROCEDIMIENTO ALMACENADO Y DEVUELCE DATASET CON LAS TABLAS
  Public Function EjecutarProcedimientoTB(NombreProcedimiento As String, NombreParametros As String, NumeroParametros As Integer, ValorParametro As String)
    Try
      conec = New SqlConnection(StrTpm)
      cmd = New SqlCommand(NombreProcedimiento, conec)
      cmd.CommandType = CommandType.StoredProcedure
      ds = New DataSet()

      If NumeroParametros <> 0 Then
        For index = 1 To NumeroParametros
          Dim arrayN As Array = NombreParametros.Split(",")
          Dim arrayV As Array = ValorParametro.Split(",")
          cmd.Parameters.AddWithValue(arrayN(index - 1), arrayV(index - 1))
        Next
      End If

      conec.Open()
      adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()
      adaptador.Fill(ds)

      Return ds
    Catch ex As Exception
      MessageBox.Show("¡Error al ejecutar procedimiento almacenado: " + NombreProcedimiento + Environment.NewLine + ex.ToString() + "!", "¡Error en EjecutarProcedimiento!", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    End Try
  End Function

  'EJECUTAR PROCEDIMIENTO ALMACENADO PARA INSERT, UPDATE, DELETE
  Public Function EjecutarProcedimientoIUD(NombreProcedimiento As String, NombreParametros As String, NumeroParametros As Integer, ValorParametro As String)
    Try
      conec = New SqlConnection(StrTpm)
      cmd = New SqlCommand(NombreProcedimiento, conec)
      cmd.CommandType = CommandType.StoredProcedure

      If NumeroParametros <> 0 Then
        For index = 1 To NumeroParametros
          Dim arrayN As Array = NombreParametros.Split(",")
          Dim arrayV As Array = ValorParametro.Split(",")
          cmd.Parameters.AddWithValue(arrayN(index - 1), arrayV(index - 1))
        Next
      End If

      conec.Open()
      adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()

      Return True
    Catch ex As Exception
      MessageBox.Show("¡Error al ejecutar procedimiento almacenado: " + NombreProcedimiento + Environment.NewLine + ex.ToString() + "!", "¡Error en EjecutarProcedimiento!", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return False
    End Try
  End Function

  'EJECUTAR CONSULTA PARA CREATE, READ, UPDATE, DELETE
  Public Function EjecutarCRUD(Consulta As String)
    Try
      conec = New SqlConnection(StrTpm)
      cmd = New SqlCommand(Consulta, conec)
      cmd.CommandType = CommandType.Text

      conec.Open()
      adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()

      Return True
    Catch ex As Exception
      MessageBox.Show(ex.ToString(), "¡Error al Ejecutar CRUD!", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return False
    End Try
  End Function

  'EJECUTAR PROCEDIMIENTO ALMACENADO
  Public Function ConsultarTabla(Consulta As String)
    Try
      conec = New SqlConnection(StrTpm)
      cmd = New SqlCommand(Consulta, conec)
      cmd.CommandType = CommandType.Text
      ds = New DataSet()

      conec.Open()
      adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()
      adaptador.Fill(ds)
      dv = New DataView
      dv.Table = ds.Tables(0)
      Return dv.Table
    Catch ex As Exception
      MessageBox.Show("¡Error al consultar tabla: " + Consulta + Environment.NewLine + ex.ToString() + "!", "¡Error en ConsultarTabla!", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    End Try
  End Function

  '
  Public Function ConsultarTB(Consulta As String)
    Try
      conec = New SqlConnection(StrTpm)
      cmd = New SqlCommand(Consulta, conec)
      cmd.CommandType = CommandType.Text
      ds = New DataSet()
      Dim SqlDR As SqlDataReader

      conec.Open()
      adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      SqlDR = cmd.ExecuteReader()
      'cmd.Connection.Close()
      'conec.Close()

      Return SqlDR

    Catch ex As Exception
      MessageBox.Show("¡Error al consultar tabla: " + Consulta + Environment.NewLine + ex.ToString() + "!", "¡Error en ConsultarTabla!", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    End Try
  End Function

  'LLENA CUALQUIER COMBOBOX, DATASOURCE,DISPLAYMENBER,VALUEMENBER
  Public Function LlenarComboBox1(consulta As String, Headers As String, ByVal combo As ComboBox)
    Try
      conec = New SqlConnection(StrTpm)
      cmd = New SqlCommand(consulta, conec)
      cmd.CommandType = CommandType.Text
      ds = New DataSet()

      conec.Open()
      adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()
      adaptador.Fill(ds, "Datos")
      Dim DHeaders As Array
      DHeaders = Split(Headers, ",")
      combo.DataSource = ds.Tables("Datos")
      combo.DisplayMember = DHeaders(1)
      combo.ValueMember = DHeaders(0)
      combo.SelectedIndex = 0

      Return combo

    Catch ex As Exception
      MessageBox.Show("¡Error al llenar ComboBox: " + Environment.NewLine + ex.ToString() + "!", "¡Error en LlenarComboBox!", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    End Try
  End Function

  'LLENAR COMBOBOX
  Public Function ComboBox(consulta As String, Headers As String, ByVal combo As ComboBox)
    Try

      Dim ConsutaLista As String

      Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

        Dim DSetTablas As New DataSet
        Dim DvLineasT As New DataView
        ConsutaLista = consulta

        Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

        daAgte.Fill(DSetTablas, "tb")

        Dim filaAgte As Data.DataRow

        filaAgte = DSetTablas.Tables("tb").NewRow

        Dim DHeaders As Array
        DHeaders = Split(Headers, ",")
        'filaAgte("ItmsGrpCod") = 0


        DSetTablas.Tables("tb").Rows.Add(filaAgte)
        DvLineasT.Table = DSetTablas.Tables("tb")
        combo.DataSource = DvLineasT
        combo.DisplayMember = DHeaders(0)
        combo.ValueMember = DHeaders(1)
        combo.SelectedValue = 0
      End Using
    Catch ex As Exception
      MsgBox("Error al cargar COMBOBOX: " + ex.Message)
    End Try
  End Function

  'CONSULTA CUALQUIER DATO DESDE UN PROCEDIMIENTO ALMACENADO
  Public Function ConsultarDato(Procedimiento As String, NombreParametros As String, NumeroParametros As Integer, ValorParametros As String)
    Try
      Dim valor As Integer
      conec = New SqlConnection(StrTpm)
      cmd = New SqlCommand(Procedimiento, conec)
      cmd.CommandType = CommandType.StoredProcedure
      ds = New DataSet()

      If NumeroParametros <> 0 Then
        For index = 1 To NumeroParametros
          Dim arrayN As Array = NombreParametros.Split(",")
          Dim arrayV As Array = ValorParametros.Split(",")
          cmd.Parameters.AddWithValue(arrayN(index - 1), arrayV(index - 1))
        Next
      End If

      conec.Open()
      valor = CInt(cmd.ExecuteScalar()).ToString()
      conec.Close()
      Return valor

    Catch ex As Exception
      MessageBox.Show("¡Error al Consultar dato en especifico: " + Environment.NewLine + ex.ToString() + "!", "¡Error en ConsultarDato!", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    End Try
  End Function

End Class


