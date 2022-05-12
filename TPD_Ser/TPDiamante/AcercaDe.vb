
Public NotInheritable Class AcercaDe

  Private Sub AcercaDe_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' Establezca el título del formulario.
    Dim ApplicationTitle As String
    If My.Application.Info.Title <> "" Then
      ApplicationTitle = My.Application.Info.Title
    Else
      ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
    End If
    Me.Text = String.Format("Acerca de {0}", ApplicationTitle)
    ' Inicialice todo el texto mostrado en el cuadro Acerca de.
    ' TODO: personalice la información del ensamblado de la aplicación en el panel "Aplicación" del 
    '    cuadro de diálogo propiedades del proyecto (bajo el menú "Proyecto").
    Me.LabelProductName.Text = My.Application.Info.ProductName
    Me.LabelCopyright.Text = My.Application.Info.Copyright
    Me.LabelCompanyName.Text = "Tracto Partes Diamante de Puebla"

    'Me.LabelVersion.Text = String.Format("Versión {0}", My.Application.Info.Version.ToString)

    Dim Cambios As String
    Dim Version As String = String.Format("Versión " & My.Application.Info.Version.ToString)

    Cambios = "
[1.0.1] 06.03.2020

- Se agregó él 'acerca de' para validar y verificar la versión actual de cada cliente
- Se corrigió versión de reporte Score card Líneas objetivos
- Se agregó en los reportes de Score Card la columna de Rutas en los clientes

[1.1.1] 11.03.2020

- Se finalizó proceso de Score Card Lineas objetivo
- Se finalizó proceso de Score Card Lineas halcon
- Se finalizó proceso de Score Card
- Se finalizó proceso de Score Card Clientes
- Se finalizó proceso de Score Card general
- Se finalizó proceso de Solicitud de articulos

[1.3.2.7] 21.03.2020

- Versión instalable y autoactualizable

[1.3.2.8] 21.03.2020

- Configuración para arquitectura AnyCPU

[1.3.2.9] 26.03.2020

- Modificación de impresiones de Ordenes para almacén
- Corrección de Solicitud de artículos de compras

[1.6.0.0] 21.05.2020

- Creación del reporte Agentes - Listas de Precio
- Compactación del menu (cambio a la vista de menú general del sistema)

[1.8.0.5] 14.07.2020

- Creción de formulario de devoluciones en inventarios
- El sistema es ahora auto-actualizable, el usuario podra actualizarlo manualmente una vez que exista una nueva versión del mismo.
"

    Me.LabelVersion.Text = Version
    Me.TextBoxDescription.Text = Cambios

  End Sub

  Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
    Me.Close()
  End Sub

End Class
