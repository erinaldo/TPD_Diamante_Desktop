Public Class probando

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim CadenaFact As String = ""
        Dim UltDato As String = ""

        Dim PosComa As Integer = 0
        Dim NumFact As Integer = 0

        Dim comprueba As String = ""

        CadenaFact = "100,101, 102, 103,  104"

        While CadenaFact.Length > 0
            PosComa = InStr(CadenaFact, ",")

            If PosComa = 0 Then
                UltDato = CadenaFact
                CadenaFact = ""
            Else
                UltDato = CadenaFact.Substring(0, PosComa - 1)
                CadenaFact = CadenaFact.Substring(PosComa, CadenaFact.Length - PosComa)

            End If

            If Val(UltDato) > 0 Then
                comprueba &= UltDato

            End If
            'PosComa = substrin(',',@VCadenaFact)
            TextBox1.Text = comprueba



        End While
        Console.WriteLine(comprueba)
    End Sub
End Class