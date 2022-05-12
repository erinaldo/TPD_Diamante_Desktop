Imports System.Data.SqlClient
Imports System.IO

Public Class PruebaGuardaImg

    Public Shared Sub AddEmployee( _
      lastName As String, _
      firstName As String, _
      title As String, _
      hireDate As DateTime, _
      reportsTo As Integer, _
      photoFilePath As String, _
      connectionString As String)

        Dim photo() As Byte = GetPhoto(photoFilePath)

        Using connection As SqlConnection = New SqlConnection( _
          connectionString)

            Dim command As SqlCommand = New SqlCommand( _
              "INSERT INTO Employees (LastName, FirstName, Title, " & _
              "HireDate, ReportsTo, Photo) " & _
              "Values(@LastName, @FirstName, @Title, " & _
              "@HireDate, @ReportsTo, @Photo)", connection)

            command.Parameters.Add("@LastName", _
              SqlDbType.NVarChar, 20).Value = lastName
            command.Parameters.Add("@FirstName", _
              SqlDbType.NVarChar, 10).Value = firstName
            command.Parameters.Add("@Title", _
              SqlDbType.NVarChar, 30).Value = title
            command.Parameters.Add("@HireDate", _
              SqlDbType.DateTime).Value = hireDate
            command.Parameters.Add("@ReportsTo", _
              SqlDbType.Int).Value = reportsTo

            command.Parameters.Add("@Photo", _
              SqlDbType.Image, photo.Length).Value = photo

            connection.Open()
            command.ExecuteNonQuery()

        End Using
    End Sub

    Public Shared Function GetPhoto(filePath As String) As Byte()
        Dim stream As FileStream = New FileStream( _
           filePath, FileMode.Open, FileAccess.Read)
        Dim reader As BinaryReader = New BinaryReader(stream)

        Dim photo() As Byte = reader.ReadBytes(stream.Length)

        reader.Close()
        stream.Close()

        Return photo
    End Function
End Class