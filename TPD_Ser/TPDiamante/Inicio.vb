Imports System.Data.SqlClient
Imports System.Deployment.Application
Imports System.Net
Imports System.Security
Imports System.Security.Permissions
Imports System.Text
Imports System.Xml
Imports TPD_C.Ventas
Imports TPD_C.TPD_Operacion

Public Class Inicio

 Private Sub Inicio_Load2(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
  'ConsultaProd.MdiParent = Me
  'ConsultaProd.Show()
  TSMenuInicio.Visible = False
  System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-MX")

  '  Login.MdiParent = Me
  'txtmsg.Width = Me.Width
  'Login.Show()

  Login.MdiParent = Me
  'Login.StartPosition = FormStartPosition.CenterParent
  txtmsg.Width = Me.Width
  Login.Show()

  If My.Settings.AMBIENTE_PRODUCCION Then
   toolSBase.Text = "PROD(" & My.Settings.gServerNameProduccion & ")"
  Else
   toolSBase.Text = "TEST(" & My.Settings.gServerNamePruebas & ")"
  End If

  panelAvisoAlerta.Visible = False
  lblAlertaUsr.Text = ""

  'Login2.MdiParent = Me
  'txtmsg.Width = Me.Width
  'Login2.Show()
 End Sub

 Private Sub MnuSalir_Click2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuSalir.Click
  Me.Close()
 End Sub

 Private Sub SMBoPorRec_Click2(sender As System.Object, e As System.EventArgs) Handles SMBoPorRec.Click
  BackOrder.MdiParent = Me
  BackOrder.Show()
 End Sub

 Private Sub SMBoRec_Click2(sender As System.Object, e As System.EventArgs) Handles SMBoRec.Click
  BORec.MdiParent = Me
  BORec.Show()
 End Sub

 Private Sub SMCapBo_Click2(sender As System.Object, e As System.EventArgs) Handles SMCapBo.Click
  CtrlBO.MdiParent = Me
  CtrlBO.Show()
 End Sub

 Private Sub SMCEstCob_Click2(sender As System.Object, e As System.EventArgs) Handles SMCobClientes.Click
  cobranzagral.MdiParent = Me
  cobranzagral.Show()
 End Sub

 Private Sub SMCobRec_Click2(sender As System.Object, e As System.EventArgs) Handles SMCobRec.Click
  Cobranza.MdiParent = Me
  Cobranza.Show()
 End Sub

 Private Sub SMListPrecio_Click2(sender As System.Object, e As System.EventArgs) Handles SMListPrecio.Click
  Pagos.MdiParent = Me
  Pagos.Show()
 End Sub

 Private Sub SMListInv_Click2(sender As System.Object, e As System.EventArgs) Handles SMListInv.Click
  ArticulosInventario.MdiParent = Me
  ArticulosInventario.Show()
 End Sub

 Private Sub SMObtaCrearOV_Click2(sender As System.Object, e As System.EventArgs) Handles SMOvtaCrearOV.Click
  Dim SQL3 As New Comandos_SQL()
  SQL3.conectarTPM()

  Dim estatus As Boolean = SQL3.CampoEspecifico("SELECT bloqueado FROM Bloqueo_Ventas", "bloqueado")
  If estatus = True And UsrTPM <> "MANAGER" Then
   Advertencia.Show()
  Else
   CapOrdVta.MdiParent = Me
   CapOrdVta.Show()
  End If
 End Sub

 Private Sub SMVtaGral_Click2(sender As System.Object, e As System.EventArgs) Handles SMAgentes2.Click

 End Sub
 Private Sub SMVtaLinea_Click2(sender As System.Object, e As System.EventArgs) Handles SMVtaLineas.Click
  Reporte_Ventas_Lineas.MdiParent = Me
  Reporte_Ventas_Lineas.Show()
 End Sub

 Private Sub SMVtaAgte_Click2(sender As System.Object, e As System.EventArgs)
  If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
   Reporte_de_Ventas_DetallePA.MdiParent = Me
   Reporte_de_Ventas_DetallePA.Show()
  Else
   Reporte_de_Ventas_Detalle.MdiParent = Me
   Reporte_de_Ventas_Detalle.Show()
  End If
 End Sub

 Private Sub SMVtaCliente_Click2(sender As System.Object, e As System.EventArgs)


 End Sub

 Private Sub SMPagoCom_Click2(sender As System.Object, e As System.EventArgs) Handles SMPagoCom.Click
  Comisiones.MdiParent = Me
  Comisiones.Show()
 End Sub

 Private Sub FletesToolStripMenuItem_Click2(sender As System.Object, e As System.EventArgs) Handles SMFletes.Click
  Embarques.MdiParent = Me
  Embarques.Show()
 End Sub

 'Private Sub SMNotCredito_Click(sender As System.Object, e As System.EventArgs) Handles SMNotCredito2.Click
 '    NC_Facts.MdiParent = Me
 '    NC_Facts.Show()
 'End Sub

 Private Sub SMOvtaAut_Click2(sender As System.Object, e As System.EventArgs) Handles SMCreadasPorFacturar.Click
  OrdVtaAut.MdiParent = Me
  OrdVtaAut.Show()
 End Sub

 Private Sub SMDevoluciones_Click2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMDevoluciones.Click
  DevolucionMat.MdiParent = Me
  DevolucionMat.Show()
 End Sub

 Private Sub SMRecibosMat_Click2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMRecibosMat.Click

  If UsrTPM = "ARAMOS" Or UsrTPM = "MCHABLE" Or UsrTPM = "VOTNIEL" Then
   ''MsgBox("ENTRE A IF")
   ReciboMatSeg.MdiParent = Me
   ReciboMatSeg.Show()
  Else
   ''MsgBox("ENTRE AL ELSE")
   ReciboMat.MdiParent = Me
   ReciboMat.Show()
  End If

 End Sub

 Private Sub SMPagoProv_Click2(ByVal sender As System.Object, ByVal e As System.EventArgs)
  PagoProveedores.MdiParent = Me
  PagoProveedores.Show()
 End Sub

 Private Sub AnalisisDeComprasToolStripMenuItem_Click2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMAnalCompras.Click
  AnalisisCompras.MdiParent = Me
  AnalisisCompras.Show()
 End Sub

 Private Sub BOLineasToolStripMenuItem_Click2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMBOLineas.Click
  BOLineas.MdiParent = Me
  BOLineas.Show()
 End Sub

 Private Sub EstatusDelClienteToolStripMenuItem_Click2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMEstatusCli.Click
  CodClienteEstatus.MdiParent = Me
  CodClienteEstatus.Show()
  EstatusCliente.MdiParent = Me
 End Sub

 Private Sub DiferenciasEnPreciosToolStripMenuItem_Click2(sender As Object, e As EventArgs) Handles SMDifPrecios.Click
  DiferenciaPrecioCompras.MdiParent = Me
  DiferenciaPrecioCompras.Show()
 End Sub

 Private Sub MToolStripMenuItem_Click2(sender As Object, e As EventArgs) Handles SMFactores.Click
  fCmpFactores.MdiParent = Me
  fCmpFactores.Show()
 End Sub

 'Private Sub TraspasoMeridaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TraspasoMeridaToolStripMenuItem.Click
 '    fCmpTrasMerida.MdiParent = Me
 '    fCmpTrasMerida.Show()
 'End Sub


 Private Sub VentaCaídaToolStripMenuItem_Click2(sender As Object, e As EventArgs) Handles SMVentaCaida.Click

  If UsrTPM = "MANAGER" Or UsrTPM = "DDORANTES" Or UsrTPM = "PRUEBAS" Then
   VentaCaida.Enabled = True
  End If

  VentaCaida.MdiParent = Me
  VentaCaida.Show()
 End Sub


 Private Sub SBInventario_Click2(sender As Object, e As EventArgs)
  Inventario.MdiParent = Me
  Inventario.Show()
 End Sub

 Private Sub SMScoreCard_Click2(sender As Object, e As EventArgs)
  'If UsrTPM = "MANAGER" Or UsrTPM = "DDORANTES" Or UsrTPM = "PRUEBAS" Then
  '    ScoreCardTPD.Enabled = True
  'End If
  ScoreCardTPD.MdiParent = Me
  ScoreCardTPD.Show()
 End Sub

 Private Sub SMLíneasHalcon_Click2(sender As Object, e As EventArgs)

 End Sub

 Private Sub SMLíneasObjetivo_Click2(sender As Object, e As EventArgs)

 End Sub

 Private Sub SMRankingLineas_Click2(sender As Object, e As EventArgs) Handles SMRankingLineas.Click
  If UsrTPM = "MANAGER" Or UsrTPM = "DDORANTES" Or UsrTPM = "PRUEBAS" Then
   RankingLineas.Enabled = True
  End If

  RankingLineas.MdiParent = Me
  RankingLineas.Show()
 End Sub


 Private Sub SolicitudDeArticulosToolStripMenuItem_Click2(sender As Object, e As EventArgs) Handles SMSolArt.Click
  'Solicitud de Artículos
  OVtaCSinM.MdiParent = Me
  OVtaCSinM.Show()
 End Sub

 Private Sub AuditoríaDeInventarioToolStripMenuItem_Click2(sender As Object, e As EventArgs) Handles SMAuditoriaInv.Click
  Inventario.MdiParent = Me
  Inventario.Show()
 End Sub

 Private Sub NotasDeCréditoToolStripMenuItem_Click2(sender As Object, e As EventArgs) Handles SMNotCredito.Click
  NC_Facts.MdiParent = Me
  NC_Facts.Show()
 End Sub

 Private Sub SMCategorias_Click2(sender As Object, e As EventArgs) Handles SMCategorias.Click
  Categorias.MdiParent = Me
  Categorias.Show()
 End Sub

 Private Sub SMInventarioM_Click2(sender As Object, e As EventArgs) Handles SMInventarioM.Click
        Dim SQL As New Comandos_SQL()
        SQL.conectarTPM()
        Dim almacen As String = SQL.CampoEspecifico("SELECT Almacen FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", "Almacen")
        SQL.Cerrar()
        Dim SQL2 As New Comandos_SQL()
        SQL2.conectarTPM()
        Dim estatus As Boolean = SQL2.CampoEspecifico("select Bloqueado from Bloque_Inventarios where Almacen = '" + almacen + "'", "bloqueado")
        'MessageBox.Show(estatus)
        SQL2.Cerrar()
        If estatus = True And (UsrTPM <> "MANAGER" Or UsrTPM <> "AINVEN" Or UsrTPM <> "PCOMPRAS" Or UsrTPM <> "COMPRAS1") Then
            Advertencia.Show()
        Else
            InventarioM.MdiParent = Me
            InventarioM.Show()
        End If
    End Sub

 Private Sub FacturasToolStripMenuItem_Click2(sender As Object, e As EventArgs) Handles SMFactura.Click
  FacturaConsulta.MdiParent = Me
  FacturaConsulta.Show()
 End Sub

 Private Sub NotasDeCréditoToolStripMenuItem_Click_12(sender As Object, e As EventArgs) Handles SMNotasC.Click
  NCConsulta.MdiParent = Me
  NCConsulta.Show()
 End Sub

 Private Sub ToolStripMenuItem1_Click2(sender As Object, e As EventArgs) Handles SMAuditoriaStock.Click
  InventarioStock.MdiParent = Me
  InventarioStock.Show()
  InventarioStockDetalle.MdiParent = Me
 End Sub

 Private Sub SMTransferencia_Click2(sender As Object, e As EventArgs) Handles SMTransferencia.Click
  TransferenciaAuditoria.MdiParent = Me
  TransferenciaAuditoria.Show()
  DiasInv.MdiParent = Me
 End Sub

 Private Sub SMTraspaso_Click2(sender As Object, e As EventArgs) Handles SMTraspaso.Click
  TraspasoAlm.MdiParent = Me
  TraspasoAlm.Show()
  DiasInv.MdiParent = Me
 End Sub

 Private Sub BoletínToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMBoletin.Click
  Boletin.MdiParent = Me
  Boletin.Show()
 End Sub

 Private Sub ReciboDeMatCalendarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMRecMatCal.Click
  ReciboMatCalendario.MdiParent = Me
  ReciboMatCalendario.Show()
 End Sub


 Private Sub SMFacturasXmlConta_Click(sender As Object, e As EventArgs) Handles SMFacturasXmlConta.Click
  FacturasXmlConta.MdiParent = Me
  FacturasXmlConta.Show()
 End Sub


 Private Sub AgenteClienteRutasToolStripMenuItem_Click(sender As Object, e As EventArgs)

 End Sub

 Private Sub SMFacturasXmlComp_Click(sender As Object, e As EventArgs) Handles SMFacturasXmlComp.Click
  FacturasNCCompras.MdiParent = Me
  FacturasNCCompras.Show()
 End Sub

 Private Sub PedidoSugeridoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMPedidoSugerido.Click
  Pedido_Sugerido.MdiParent = Me
  Pedido_Sugerido.Show()
 End Sub

 Private Sub SMBOVentas_Click(sender As Object, e As EventArgs) Handles SMBOVentas.Click
  BackOrderVtas.MdiParent = Me
  BackOrderVtas.Show()
 End Sub


 Private Sub SMAntiguedadCli_Click(sender As Object, e As EventArgs) Handles SMAntiguedadCli.Click
  AntiguedadCli.MdiParent = Me
  AntiguedadCli.Show()
 End Sub

 Private Sub ValoraciónDeInventariosPorAlmacenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMValoracionInv2.Click

 End Sub

 Private Sub ValoraciónDeInventariosPorProductoToolStripMenuItem_Click(sender As Object, e As EventArgs)
  ValoracionInvPro.MdiParent = Me
  ValoracionInvPro.Show()
 End Sub


 Private Sub SMDescuentos_Click(sender As Object, e As EventArgs) Handles SMDescuentos.Click
  Descuentos.MdiParent = Me
  Descuentos.Show()
 End Sub

 Private Sub EnvíoDeFacturasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMEnvioFacturas.Click
  'Process.Start("\\Servidorsap\b1_shr\TPD\Facturas\Facturas\Facturas\bin\Debug\Facturas.exe")
 End Sub


 Private Sub SMAutoAlta_Click(sender As Object, e As EventArgs) Handles SMAutoAlta.Click
  cochesalta.MdiParent = Me
  cochesalta.Show()
 End Sub

 Private Sub SMAutosCargar_Click(sender As Object, e As EventArgs) Handles SMAutosCargar.Click
  CargaCombustible.MdiParent = Me
  CargaCombustible.Show()
  'If band_carga_combustible = False Then
  '    CargaCombustible.Close()
  'Else
  '    band_carga_combustible = False
  'End If

 End Sub

 Private Sub SMAutosBitacora_Click(sender As Object, e As EventArgs) Handles SMAutosBitacora.Click
  Bitacora.MdiParent = Me
  Bitacora.Show()
 End Sub

 Private Sub SMAutosActPre_Click(sender As Object, e As EventArgs) Handles SMAutosActPre.Click
  PrecioGaso.MdiParent = Me
  PrecioGaso.Show()
 End Sub

 Private Sub SMControlEnvios_Click(sender As Object, e As EventArgs) Handles SMControlEnvios.Click
  ControlEnvios.MdiParent = Me
  ControlEnvios.Show()
 End Sub

 Private Sub GarantíasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMGarantias.Click
  Garantias.MdiParent = Me
  DetGarantias.MdiParent = Me
  DetGarantiasSeg.MdiParent = Me

  DetGarantias2.MdiParent = Me
  DetGarantias2.Show()

  'Garantias.Show()
 End Sub

 Private Sub SMBoletin2_Click(sender As Object, e As EventArgs) Handles SMBoletin2.Click
  ArticulosBoletin.MdiParent = Me
  ArticulosBoletin.Show()
 End Sub

 Private Sub AgentesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMAgentesConta.Click
  AgentesConta.MdiParent = Me
  AgentesConta.Show()
 End Sub

 Private Sub VentasClienteMensualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMVtaCliMen.Click
  VentasCliMen.MdiParent = Me
  VentasCliMen.Show()
 End Sub


 Private Sub SMFacturaArticulos_Click(sender As Object, e As EventArgs) Handles SMFacturaArticulos.Click
  FacturasArtCli.MdiParent = Me
  FacturasArtCli.Show()
 End Sub


 Private Sub SMSalidas_Click(sender As Object, e As EventArgs) Handles SMSalidas.Click
  ValeSalida.MdiParent = Me
  ValeSalida.Show()
 End Sub

 Private Sub SolicitudVacToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMSolicitudVac.Click
  FormatoVacaciones.MdiParent = Me
  FormatoVacaciones.Show()
 End Sub

 Private Sub AltaEmpleadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMAltaEmp.Click
  EmpleadoAlta.MdiParent = Me
  EmpleadoAlta.Show()
 End Sub

 Private Sub ConsultaVacToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMConsultaVac.Click
  VacacionesConsulta.MdiParent = Me
  VacacionesConsulta.Show()
 End Sub

 Private Sub SMCotizacion_Click(sender As Object, e As EventArgs) Handles SMCotizacion.Click
  Dim SQL3 As New Comandos_SQL()
  SQL3.conectarTPM()

  Dim estatus As Boolean = SQL3.CampoEspecifico("SELECT bloqueado FROM Bloqueo_Ventas", "bloqueado")
  If estatus = True And UsrTPM <> "MANAGER" Then
   Advertencia.Show()
  Else
   Cotizacion.MdiParent = Me
   Cotizacion.Show()
  End If
 End Sub

 Private Sub FacturasCanceladas_Click(sender As Object, e As EventArgs) Handles FacturasCanceladas.Click
  Facturas_Canceladas.MdiParent = Me
  Facturas_Canceladas.Show()
 End Sub

 Private Sub PackinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackinToolStripMenuItem.Click
  Packin.MdiParent = Me
  Packin.Show()
 End Sub

 Private Sub SMCheckIn_Click(sender As Object, e As EventArgs)
  ControlCheckIn.MdiParent = Me
  ControlCheckIn.Show()
 End Sub

 Private Sub SMPartidasAbiertas_Click(sender As Object, e As EventArgs) Handles SMPartidasAbiertas.Click
  PartidasAbiertasCompras.MdiParent = Me
  PartidasAbiertasCompras.Show()
 End Sub

 Private Sub ReporteTrackingDiarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMReporteTracking.Click
  Tracking.MdiParent = Me
  Tracking.Show()
 End Sub

 Private Sub SMAnalisisAlmacen_Click(sender As Object, e As EventArgs)
  'AnalisisAlmacen.MdiParent = Me
  'AnalisisAlmacen.Show()
 End Sub

 Private Sub RegistroAlmacenistaEmpacadorToolStripMenuItem_Click(sender As Object, e As EventArgs)
  AnalisisAlmacen.MdiParent = Me
  AnalisisAlmacen.Show()
 End Sub

 Private Sub VerOrdenesDeVentaToolStripMenuItem_Click(sender As Object, e As EventArgs)
  MostrarOrdenes.MdiParent = Me
  MostrarOrdenes.Show()
 End Sub

 Private Sub AnalisisAlmacenistasToolStripMenuItem_Click(sender As Object, e As EventArgs)

 End Sub

 Private Sub SMEstatusOrdVenta_Click(sender As Object, e As EventArgs) Handles SMEstatusOrdVenta.Click
  EstatusOrdVta.MdiParent = Me
  EstatusOrdVta.Show()
 End Sub

 Private Sub SurtirOrdenesDeVentaToolStripMenuItem_Click(sender As Object, e As EventArgs)
  EmpacarOrdenes.MdiParent = Me
  EmpacarOrdenes.Show()
 End Sub

 Private Sub EnvioMasivoDeFacturasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnvioMasivoDeFacturasToolStripMenuItem.Click
  Process.Start("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Facturas\Facturas\Facturas\bin\Debug\Facturas.exe")
 End Sub

 Private Sub ReenvioDeFacturasFaltantesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReenvioDeFacturasFaltantesToolStripMenuItem.Click
  Process.Start("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\FacturasFaltantes\FacturasFaltantes\Facturas\bin\Debug\Facturas.exe")

 End Sub

 Private Sub EstatusOrdVtaToolStripMenuItem_Click(sender As Object, e As EventArgs)
  EstatusOrdVta.MdiParent = Me
  EstatusOrdVta.Show()
 End Sub

 Private Sub AlmacenistaRecibo_Click(sender As Object, e As EventArgs) Handles AlmacenistaRecibo.Click
  Process.Start("PerfilesPuesto\" & "Almacenista de Recibo" & ".pdf")
 End Sub


 Private Sub MatrizToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MatrizToolStripMenuItem.Click
  Organigrama.MdiParent = Me
  Organigrama.Show()
 End Sub

 Private Sub SucursalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SucursalToolStripMenuItem.Click
  OrganigramaSucursal.MdiParent = Me
  OrganigramaSucursal.Show()
 End Sub

 Private Sub Almacenista_Click(sender As Object, e As EventArgs) Handles Almacenista.Click
  Process.Start("PerfilesPuesto\" & "Almacenista" & ".pdf")
 End Sub

 Private Sub AnalistaLogistica_Click(sender As Object, e As EventArgs) Handles AnalistaLogistica.Click
  Process.Start("PerfilesPuesto\" & "Analista de Logistica" & ".pdf")
 End Sub

 Private Sub AuxiliarLogistica_Click(sender As Object, e As EventArgs) Handles AuxiliarLogistica.Click
  Process.Start("PerfilesPuesto\" & "Auxiliar de Logistica" & ".pdf")
 End Sub

 Private Sub EncargadoDevoluciones_Click(sender As Object, e As EventArgs) Handles EncargadoDevoluciones.Click
  Process.Start("PerfilesPuesto\" & "Encargado de Devoluciones" & ".pdf")
 End Sub

 Private Sub GerenteOperacion_Click(sender As Object, e As EventArgs) Handles GerenteOperacion.Click
  Process.Start("PerfilesPuesto\" & "Gerente de Operacion" & ".pdf")
 End Sub

 Private Sub AsesorComercial_Click(sender As Object, e As EventArgs) Handles AsesorComercial.Click
  Process.Start("PerfilesPuesto\" & "Asesor Comercial foraneo" & ".pdf")
 End Sub

 Private Sub EjecutivoComercial_Click(sender As Object, e As EventArgs) Handles EjecutivoComercial.Click
  Process.Start("PerfilesPuesto\" & "Ejecutivo Comercial" & ".pdf")
 End Sub

 Private Sub GerenteComercial_Click(sender As Object, e As EventArgs) Handles GerenteComercial.Click
  Process.Start("PerfilesPuesto\" & "Gerente Comercial" & ".pdf")
 End Sub

 Private Sub AnalistaDeCreditoYCobranzaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnalistaDeCreditoYCobranzaToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Analista de Credito y Cobranza" & ".pdf")
 End Sub

 Private Sub AuditorDeInventariosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuditorDeInventariosToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Auditor de Inventarios" & ".pdf")
 End Sub

 Private Sub AuxiliarContableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuxiliarContableToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Auxiliar contable" & ".pdf")
 End Sub

 Private Sub AuxiliarDeCreditoYCobranzaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuxiliarDeCreditoYCobranzaToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Auxiliar de Credito y Cobranza" & ".pdf")
 End Sub

 Private Sub AuxiliarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuxiliarToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Auxiliar de Logistica" & ".pdf")
 End Sub

 Private Sub CompradorInternacionalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompradorInternacionalToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Comprador Internacional" & ".pdf")
 End Sub

 Private Sub GerenteAdministrativoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerenteAdministrativoToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Gerente Administrativo" & ".pdf")
 End Sub

 Private Sub JefeDeComprasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JefeDeComprasToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Jefe de Compras" & ".pdf")
 End Sub

 Private Sub JefeDeCreditoYCobranzaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles JefeDeCreditoYCobranzaToolStripMenuItem1.Click
  Process.Start("PerfilesPuesto\" & "Jefe de Credito y Cobranza" & ".pdf")
 End Sub

 Private Sub AuxiliarDeSistemasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuxiliarDeSistemasToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Auxiliar de Sistemas" & ".pdf")
 End Sub

 Private Sub GerenteDeSistemasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerenteDeSistemasToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Gerente de Sistemas" & ".pdf")
 End Sub

 Private Sub RecursosHumanosToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RecursosHumanosToolStripMenuItem1.Click
  Process.Start("PerfilesPuesto\" & "Recursos Humanos" & ".pdf")
 End Sub

 Private Sub VigilanteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VigilanteToolStripMenuItem.Click
  Process.Start("PerfilesPuesto\" & "Vigilante" & ".pdf")
 End Sub

 Private Sub ArticulosRemateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArticulosRemateToolStripMenuItem.Click
  'Dim ArticulosRemate As New frmArticuloRemates()
  'ArticulosRemate.MdiParent = Me
  'ArticulosRemate.Show()
  Dim ArticulosRemate As New frmArticulosRemate()
  ArticulosRemate.MdiParent = Me
  ArticulosRemate.Show()
 End Sub

 Private Sub ValorDelInventarioPorLineaToolStripMenuItem_Click(sender As Object, e As EventArgs)
  Dim ValorInventarioLinea As New frmvalor_inventario
  ValorInventarioLinea.MdiParent = Me
  ValorInventarioLinea.Show()
 End Sub

 Private Sub SolicitudDeCancelaciones_Click(sender As Object, e As EventArgs) Handles SolicitudDeCancelaciones.Click
  Dim Cancelacion As New frmcancelaciones
  Cancelacion.MdiParent = Me
  Cancelacion.Show()
 End Sub

 Private Sub SMEnvioFacturasCli_Click(sender As Object, e As EventArgs)
  Dim EnvioFac As New frmEnvioFacturas()
  EnvioFac.MdiParent = Me
  EnvioFac.Show()
 End Sub

 Private Sub SolArticulosEspecialesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SolArticulosEspecialesToolStripMenuItem.Click
  Dim ArticulosEspeciales As New frmArticulosEspeciales
  ArticulosEspeciales.MdiParent = Me
  ArticulosEspeciales.Show()
 End Sub

 Private Sub ProcesoDeOrdenVtaToolStripMenuItem_Click(sender As Object, e As EventArgs)
  'Dim OrdendeVta As New frmOrdenesVta
  'OrdendeVta.MdiParent = Me
  'OrdendeVta.Show()
 End Sub

 Private Sub SurtimientoToolStripMenuItem_Click(sender As Object, e As EventArgs)
  Dim MostrarOrdenes As New frmMostrarOrdenes
  MostrarOrdenes.MdiParent = Me
  MostrarOrdenes.Show()
 End Sub

 Private Sub EToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EstatusOrdenToolStripMenuItem.Click
  Dim OrdendeVta As New frmOrdenesVta
  OrdendeVta.MdiParent = Me
  OrdendeVta.Show()
 End Sub


 Private Sub ErroresDeTimbradoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ErroresDeTimbradoToolStripMenuItem.Click
  Dim ErroresTimbrado As New frmErroresDeTimbrado
  ErroresTimbrado.MdiParent = Me
  ErroresTimbrado.Show()
 End Sub

 Private Sub OperacionToolStripMenuItem_Click(sender As Object, e As EventArgs)

 End Sub

 Private Sub SurtimientoToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles SurtimientoToolStripMenuItem.Click
  'Dim MostrarOrdenes As New frmMostrarOrdenes
  'MostrarOrdenes.MdiParent = Me
  'MostrarOrdenes.Show()
 End Sub

 Private Sub EmpaqueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmpaqueToolStripMenuItem.Click
  Dim Empaque As New frmEstatusEmpaque
  frmEstatusEmpaque.MdiParent = Me
  frmEstatusEmpaque.Show()
 End Sub

 Private Sub SurtirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SurtirToolStripMenuItem.Click
  Dim MostrarOrdenes As New frmMostrarOrdenes
  MostrarOrdenes.MdiParent = Me
  MostrarOrdenes.Show()
 End Sub

 Private Sub ConsultaModificaciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsultaModificaciónToolStripMenuItem.Click
  Dim ConsultaAlmacen As New FrmConsultaModificacion
  ConsultaAlmacen.MdiParent = Me
  ConsultaAlmacen.Show()
 End Sub

 Private Sub VentasTotalesXMesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMVentasTotales.Click
  Dim frmVentasNetas As New frmVentasNetasTotales
  frmVentasNetas.MdiParent = Me
  frmVentasNetas.Show()
 End Sub

 Private Sub RegistroDeVisitasDeAgentesToolStripMenuItem_Click(sender As Object, e As EventArgs)
  Dim frmVisita_Agente As New frmVisita_Agente
  frmVisita_Agente.MdiParent = Me
  frmVisita_Agente.Show()
 End Sub

 Private Sub LiberacionDeEntregaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiberacionDeEntregaToolStripMenuItem.Click
  Dim frmLiberacionMatLOG As New frmLiberacionMatLOG
  frmLiberacionMatLOG.MdiParent = Me
  frmLiberacionMatLOG.Show()
 End Sub

 Private Sub LiberacionDeGuiasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiberacionDeGuiasToolStripMenuItem.Click
  Dim frmLiberacionGuias As New frmLiberacionGuias
  frmLiberacionGuias.MdiParent = Me
  frmLiberacionGuias.Show()
 End Sub

 Private Sub SalidaDeMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalidaDeMaterialToolStripMenuItem.Click
  Dim frmSalidaMaterial As New Entrada_Salida_Material
  frmSalidaMaterial.MdiParent = Me
  frmSalidaMaterial.Show()
 End Sub

 Private Sub SeguimientoDeEntregaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SeguimientoDeEntregaToolStripMenuItem.Click
  Dim frmpantallasEntrega As New frmpantallasEntrega
  frmpantallasEntrega.MdiParent = Me
  frmpantallasEntrega.Show()
 End Sub

 Private Sub DefinicionDeLimitesDeCreditoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefinicionDeLimitesDeCreditoToolStripMenuItem.Click
  frmLimiteClientes.MdiParent = Me
  frmLimiteClientes.Show()
 End Sub

 Private Sub FNCPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FNCPToolStripMenuItem.Click
  frmFNCPSinCancelados.MdiParent = Me
  frmFNCPSinCancelados.Show()
 End Sub

 Private Sub ReporteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReporteToolStripMenuItem.Click
  frmReporteAlberto.MdiParent = Me
  frmReporteAlberto.Show()
 End Sub

 Private Sub ScoreCardClientesToolStripMenuItem_Click(sender As Object, e As EventArgs)

 End Sub

 Private Sub ClasificaciónPorVentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClasificaciónPorVentasToolStripMenuItem.Click
  ClasificacionProductosPorVentas.MdiParent = Me
  ClasificacionProductosPorVentas.Show()
 End Sub

 Private Sub BarCodeToolStripMenuItem_Click(sender As Object, e As EventArgs)
  frmBarCode.MdiParent = Me
  frmBarCode.Show()
 End Sub

 Private Sub VisitasPorAsesorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VisitasPorAsesorToolStripMenuItem.Click
  Consultar_visitasporasesor.MdiParent = Me
  Consultar_visitasporasesor.Show()
 End Sub

 Private Sub MCompras_Click(sender As Object, e As EventArgs) Handles MCompras.Click

 End Sub

 Private Sub ToolStripMenuItem133_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1332.Click

 End Sub

 Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
  Dim forma As Form = AcercaDe
  forma.Show()
 End Sub

 Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
  Dim forma As Form = frmUsuarios
  forma.Show()
 End Sub

 Private Sub DevolucionesToolStripMenuItem_Click(sender As Object, e As EventArgs)
  Devoluciones.MdiParent = Me
  Devoluciones.Show()
 End Sub

 Private Sub AdministrarCódigosDeBarraToolStripMenuItem_Click(sender As Object, e As EventArgs)
  If UsrTPM = "MANAGER" Or UsrTPM = "PCOMPRAS" Then
   frmAdminBarCode.MdiParent = Me
   frmAdminBarCode.Show()
  Else
   AdministrarCódigosDeBarraToolStripMenuItem.Visible = False
  End If
 End Sub

 Private Sub MnuBonoMensual_Click(sender As Object, e As EventArgs) Handles MnuBonoMensual.Click
  Dim forma As Form = frmBonoMensual
  forma.Show()
 End Sub

 Private Sub ListadoEspecialDePrecios_Click(sender As Object, e As EventArgs) Handles ListadoEspecialDePrecios.Click
  ListasPreciosEspecial.MdiParent = Me
  ListasPreciosEspecial.Show()
 End Sub

 Private Sub AgteListaPrecio_Click(sender As Object, e As EventArgs)

 End Sub

 Private Sub SMAgentes_Click(sender As Object, e As EventArgs) Handles SMAgentes.Click
  ConsultaProd.MdiParent = Me
  ConsultaProd.Show()
 End Sub

 Private Sub SMAgteClite_Click(sender As Object, e As EventArgs) Handles SMAgteClite.Click
  VtasClteAgte.MdiParent = Me
  VtasClteAgte.Show()
 End Sub

 Private Sub AgenteClienteRutasMdiParentMeToolStripMenuItem_Click(sender As Object, e As EventArgs)

 End Sub

 Private Sub SMAgteClteRutas_Click(sender As Object, e As EventArgs) Handles SMAgteClteRutas.Click
  AgenteClienteRutas.MdiParent = Me
  AgenteClienteRutas.Show()
 End Sub

 Private Sub SMAgteLinea_Click(sender As Object, e As EventArgs) Handles SMAgteLinea.Click
  If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" And UsrTPM <> "AVERACRUZ" Then
   Reporte_de_Ventas_DetallePA.MdiParent = Me
   Reporte_de_Ventas_DetallePA.Show()
  Else
   Reporte_de_Ventas_Detalle.MdiParent = Me
   Reporte_de_Ventas_Detalle.Show()
  End If
 End Sub

 Private Sub KPIsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem133.Click
  SCGeneral.MdiParent = Me
  SCGeneral.Show()
 End Sub

 Private Sub SMScoreCard_Click(sender As Object, e As EventArgs) Handles SMScoreCard.Click
  ScoreCardTPD.MdiParent = Me
  ScoreCardTPD.Show()
 End Sub

 Private Sub ScoreCardClientesToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ScoreCardClientesToolStripMenuItem.Click
  ScoreCardCliente.MdiParent = Me
  ScoreCardCliente.Show()
  If CerrarSCClientes = True Then
   ScoreCardCliente.Close()
   CerrarSCClientes = False
  End If
 End Sub

 Private Sub SMLíneasHalcon_Click(sender As Object, e As EventArgs) Handles SMLíneasHalcon.Click
  If UsrTPM = "MANAGER" Or UsrTPM = "DDORANTES" Or UsrTPM = "PRUEBAS" Then
   LineasHalcon.Enabled = True
  End If

  LineasHalcon.MdiParent = Me
  LineasHalcon.Show()
 End Sub

 Private Sub SMLíneasObjetivo_Click(sender As Object, e As EventArgs) Handles SMLíneasObjetivo.Click
  If UsrTPM = "MANAGER" Or UsrTPM = "DDORANTES" Or UsrTPM = "PRUEBAS" Then
   LineasObjetivo.Enabled = True
  End If
  LineasObjetivo.MdiParent = Me
  LineasObjetivo.Show()
 End Sub

 Private Sub SMEnvioFacturasCli_Click_1(sender As Object, e As EventArgs) Handles SMEnvioFacturasCli.Click
  Dim EnvioFac As New frmEnvioFacturas()
  EnvioFac.MdiParent = Me
  EnvioFac.Show()
 End Sub

 Private Sub BarCodeToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles BarCodeToolStripMenuItem.Click
  frmBarCode.MdiParent = Me
  frmBarCode.Show()
 End Sub

 Private Sub AdministrarCódigosDeBarraToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles AdministrarCódigosDeBarraToolStripMenuItem.Click
  If UsrTPM = "MANAGER" Or UsrTPM = "PCOMPRAS" Then
   frmAdminBarCode.MdiParent = Me
   frmAdminBarCode.Show()
  Else
   AdministrarCódigosDeBarraToolStripMenuItem.Visible = False
  End If
 End Sub

 Private Sub PorAlmacénToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMValoracionInv.Click
  ValoracionInv.MdiParent = Me
  ValoracionInv.Show()
 End Sub

 Private Sub PorProductoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SMValInvPRO.Click
  ValoracionInvPro.MdiParent = Me
  ValoracionInvPro.Show()
 End Sub

 Private Sub PorLíneaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValorDelInventarioPorLineaToolStripMenuItem.Click
  Dim ValorInventarioLinea As New frmvalor_inventario
  ValorInventarioLinea.MdiParent = Me
  ValorInventarioLinea.Show()
 End Sub

 Private Sub mnu_ActualizarSistema_Click(sender As Object, e As EventArgs) Handles mnu_ActualizarSistema.Click
  RealizaActualizacion()
 End Sub

 'Private Sub RealizaActualizacion()
 ' Dim info As UpdateCheckInfo = Nothing
 ' If (ApplicationDeployment.IsNetworkDeployed) Then
 '  MsgBox("Actualiazndo")
 '  TStripMsg.Text = "Revisando nueva actualización"
 '  Dim AD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment
 '  Try
 '   TStripMsg.Text = "Descargando actualización"
 '   info = AD.CheckForDetailedUpdate()
 '  Catch dde As DeploymentDownloadException
 '  Catch ioe As InvalidOperationException
 '  End Try
 '  If (info.UpdateAvailable) Then
 '   Try
 '    TStripMsg.IsLink = False
 '    TStripMsg.Text = "Instalando nueva versión, por favor espere a que el sistema sea reinicado"
 '    txtmsg.BackColor = Color.Black
 '    txtmsg.ForeColor = Color.Lime
 '    txtmsg.Text = "EL SISTEMA SERÁ ACTUALIZADO Y REINICIADO, POR FAVOR ESPERE..."
 '    txtmsg.Visible = True
 '    AD.Update()
 '    Application.Restart()
 '   Catch dde As DeploymentDownloadException
 '   End Try
 '  End If
 ' End If
 'End Sub

 ''Private Function CheckForUpdateAvailable() As Boolean
 '' Dim updateLocation As Uri = ApplicationDeployment.CurrentDeployment.UpdateLocation

 '' 'Used to use the Clickonce API but we've uncovered a pretty serious bug which results in a COMException and the loss of ability
 '' 'to check for updates. So until this Is fixed, we're resorting to a very lo-fi way of checking for an update.

 '' Dim webClient As WebClient = New WebClient()
 '' webClient.Encoding = Encoding.UTF8
 '' Dim manifestFile As String = webClient.DownloadString(updateLocation)

 '' 'We have some garbage info from the file header, presumably because the file Is a .application And Not .xml
 '' '  Just start from the start of the first tag
 '' Dim startOfXml As Integer = manifestFile.IndexOfAny("<")

 '' manifestFile = manifestFile.Substring(startOfXml)

 '' Dim Version As Version

 '' Dim doc As XmlDocument = New XmlDocument()

 '' 'build the xml from the manifest
 '' doc.LoadXml(manifestFile)

 '' Dim nodesList As XmlNodeList = doc.GetElementsByTagName("assemblyIdentity")
 '' If nodesList Is Nothing Or nodesList.Count <= 0 Then
 ''  MsgBox("Could not read the xml manifest file, which is required to check if an update is available.")
 ''  'Throw New XmlException("Could not read the xml manifest file, which is required to check if an update is available.")
 '' End If

 '' Dim theNode As XmlNode = nodesList(0)
 '' 'Version = New Version(theNode.Attributes["version"].Value)
 '' MsgBox(theNode.Attributes("version").Value)
 '' Version = New Version(theNode.Attributes("version").Value)

 '' If (Version > ApplicationDeployment.CurrentDeployment.CurrentVersion) Then
 ''  Return True
 '' Else
 ''  Return False
 '' End If
 ''End Function

 'Public Sub RevisaActualizacion()

 ' 'Dim info As UpdateCheckInfo = Nothing
 ' 'Try
 ' ' If (ApplicationDeployment.IsNetworkDeployed) Then

 ' '  Dim AD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment

 ' '  If (ApplicationDeployment.IsNetworkDeployed) Then
 ' '   Dim updateIsAvailable As Boolean
 ' '   Try
 ' '    updateIsAvailable = CheckForUpdateAvailable()
 ' '   Catch ex As Exception
 ' '    MsgBox("Error 1: " & ex.Message.ToString())
 ' '   End Try

 ' '   Try
 ' '    If (updateIsAvailable) Then
 ' '     AD = ApplicationDeployment.CurrentDeployment
 ' '     If (AD Is Nothing) Then
 ' '      MsgBox("Salir")
 ' '      Exit Sub
 ' '     End If

 ' '     MsgBox("Hace Update")

 ' '     If (AD.Update() = True) Then
 ' '      MsgBox("Update Ok")
 ' '     Else
 ' '      MsgBox("Update mal")
 ' '     End If
 ' '     Application.Restart()
 ' '    End If
 ' '   Catch ex2 As Exception
 ' '    MsgBox("Error 2: " & ex2.Message.ToString())
 ' '   End Try
 ' '  End If
 ' ' End If
 ' 'Catch ex As Exception
 ' ' If UsrTPM <> "SISTEMAS" Then
 ' '  TStripMsg.Text = TStripMsg.Text & " Error (" & ex.Message.ToString() & ")"
 ' ' Else
 ' '  TStripMsg.Text = TStripMsg.Text & " (Archivos no localizados)"
 ' ' End If
 ' 'End Try


 ' Dim info As UpdateCheckInfo = Nothing
 ' Try
 '  If (ApplicationDeployment.IsNetworkDeployed) Then
 '   Dim AD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment
 '   TStripMsg.Text = "No se localizó actualización"
 '   Try
 '    MsgBox("3")

 '    MsgBox("Version actual: " & AD.CurrentVersion.ToString())
 '    MsgBox("Directorio OnceClick: " & AD.DataDirectory().ToString)
 '    MsgBox("WEB Location Update: " & AD.UpdateLocation.ToString())

 '    Try
 '     Dim AppIdentity As New ApplicationIdentity(AD.UpdatedApplicationFullName)
 '     Dim UnrestrictedPerms As New System.Security.PermissionSet(Security.Permissions.PermissionState.Unrestricted)
 '     Dim AppTrust As New System.Security.Policy.ApplicationTrust(AppIdentity) With {
 '           .DefaultGrantSet = New Security.Policy.PolicyStatement(UnrestrictedPerms),
 '           .IsApplicationTrustedToRun = True,
 '           .Persist = True
 '           }
 '     Security.Policy.ApplicationSecurityManager.UserApplicationTrusts.Add(AppTrust)


 '     MsgBox("AppIdentity.FullName: " & AppIdentity.FullName.ToString())

 '    Catch ex As Exception
 '     TStripMsg.Text = "Error permisos: " & ex.Message.ToString()
 '     End
 '    End Try

 '    MsgBox("3.5")

 '    info = AD.CheckForDetailedUpdate()
 '    MsgBox("4")
 '    End
 '    TStripMsg.Text = "Se esta ejecutando la ultima versión del sistema " & String.Format(My.Application.Info.Version.ToString)
 '   Catch dde As DeploymentDownloadException

 '    MsgBox("DDE:" & dde.Message.ToString())
 '   Catch ioe As InvalidOperationException
 '    MsgBox("IOE:" & ioe.Message.ToString())
 '   End Try

 '   MsgBox("5")

 '   If (info.UpdateAvailable) Then
 '    Try
 '     TStripMsg.Text = "Presione aquí para actualizar su versión despues de haber guardado su información"
 '     TStripMsg.IsLink = True
 '     txtmsg.Visible = True
 '     mnu_ActualizarSistema.Visible = True
 '     txtmsg.BackColor = Color.Yellow
 '     txtmsg.ForeColor = Color.Red
 '     txtmsg.Text = "EL SISTEMA NECESITA SER ACTUALIZADO!! POR FAVOR ACTUALICELO DESDE EL MENÚ"
 '     txtmsg.Visible = True
 '     TimeUpdate.Enabled = False
 '     MsgBox("Actualización localizada, por favor guarde su información y actualice el sistema desde el menu en cuanto le sea posible", MsgBoxStyle.Information, "¡¡IMPORTANTE!!")
 '    Catch dde As DeploymentDownloadException
 '    End Try
 '   End If
 '  End If
 ' Catch ex As Exception
 '  If UsrTPM <> "SISTEMAS" Then
 '   TStripMsg.Text = TStripMsg.Text & " Error (" & ex.Message.ToString() & ")"
 '  Else
 '   TStripMsg.Text = TStripMsg.Text & " (Archivos no localizados)"
 '  End If
 ' End Try
 'End Sub

 Private Sub RealizaActualizacion()
  Dim info As UpdateCheckInfo = Nothing
  If (ApplicationDeployment.IsNetworkDeployed) Then
   TStripMsg.Text = "Revisando nueva actualización"
   Dim AD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment
   Try
    TStripMsg.Text = "Descargando actualización"
    info = AD.CheckForDetailedUpdate()
   Catch dde As DeploymentDownloadException
   Catch ioe As InvalidOperationException
   End Try
   If (info.UpdateAvailable) Then
    Try
     TStripMsg.IsLink = False
     TStripMsg.Text = "Instalando nueva versión, por favor espere a que el sistema sea reinicado"
     txtmsg.BackColor = Color.Black
     txtmsg.ForeColor = Color.Lime
     txtmsg.Text = "EL SISTEMA SERÁ ACTUALIZADO Y REINICIADO, POR FAVOR ESPERE..."
     txtmsg.Visible = True
     AD.Update()
     Application.Restart()
    Catch dde As DeploymentDownloadException
    End Try
   End If
  End If
 End Sub

 Public Sub RevisaActualizacion()
  Dim info As UpdateCheckInfo = Nothing
  Try
   If (ApplicationDeployment.IsNetworkDeployed) Then
    Dim AD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment
    'TStripMsg.Text = "Verifique su conexión a internet ya que no se puede buscar actualización"
    Try
     info = AD.CheckForDetailedUpdate()
     TStripMsg.Text = "Se esta ejecutando la ultima versión del sistema " & String.Format(My.Application.Info.Version.ToString)
    Catch dde As DeploymentDownloadException
    Catch ioe As InvalidOperationException
    End Try
    If (info.UpdateAvailable) Then
     Try
      TStripMsg.Text = "Presione aquí para actualizar su versión despues de haber guardado su información"
      TStripMsg.IsLink = True
      txtmsg.Visible = True
      mnu_ActualizarSistema.Visible = True
      txtmsg.BackColor = Color.Yellow
      txtmsg.ForeColor = Color.Red
      txtmsg.Text = "EL SISTEMA NECESITA SER ACTUALIZADO!! POR FAVOR ACTUALICELO DESDE EL MENÚ"
      txtmsg.Visible = True
      TimeUpdate.Enabled = False
      MsgBox("Actualización localizada, por favor guarde su información y actualice el sistema desde el menu en cuanto le sea posible", MsgBoxStyle.Information, "¡¡IMPORTANTE!!")
     Catch dde As DeploymentDownloadException
     End Try
    End If
   End If
  Catch ex As Exception
   TStripMsg.Text = TStripMsg.Text & " (Archivos no localizados)"
  End Try
 End Sub

 Private Sub TimeUpdate_Tick(sender As Object, e As EventArgs) Handles TimeUpdate.Tick
  RevisaActualizacion()
 End Sub

 Private Sub TStripMsg_Click(sender As Object, e As EventArgs) Handles TStripMsg.Click
  If TStripMsg.IsLink Then
   TStripMsg.IsLink = False
   RealizaActualizacion()
  End If
 End Sub

 Private Sub Devoluciones2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Devoluciones2ToolStripMenuItem.Click
  Dim Devs2 As New Devoluciones2()
  Devs2.MdiParent = Me
  Devs2.Show()
 End Sub

 Private Sub ModificaciónDeComisionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModificaciónDeComisionesToolStripMenuItem.Click
  CambioComisiones.MdiParent = Me
  CambioComisiones.Show()
 End Sub

 Private Sub BorradoresToolStripMenuItem_Click(sender As Object, e As EventArgs)

 End Sub

 Private Sub BorradoresToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles BorradoresToolStripMenuItem.Click
  BorradorVentas.MdiParent = Me
  BorradorVentas.Show()
 End Sub

 Private Sub EnvíoDeCorreosMultifuncionalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnvíoDeCorreosMultifuncionalToolStripMenuItem.Click
  Envio_de_correos_multifuncional.MdiParent = Me
  Envio_de_correos_multifuncional.Show()
 End Sub

 Private Sub PagosAProveedoresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PagosAProveedoresToolStripMenuItem.Click
  PagoProveedores.MdiParent = Me
  PagoProveedores.Show()
 End Sub

 Private Sub PagosRealizadosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PagosRealizadosToolStripMenuItem.Click
  PagosRealizados.MdiParent = Me
  PagosRealizados.Show()
 End Sub

 Private Sub Inicio_Resize(sender As Object, e As EventArgs) Handles Me.Resize
  Dim Alto As Integer = Me.Height
  Dim Largo As Integer = Me.Width
  Dim LoginAlto As Integer = Login.Height
  Dim Loginlargo As Integer = Login.Width

  Dim posX As Integer = (Largo - Loginlargo) / 2
  Dim posY As Integer = (Alto - LoginAlto) / 2

  Dim p As Point = Login.Location()
  Login.SetDesktopLocation(posX, posY)
 End Sub

 Private Sub BloquearModVentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BloquearModVentasToolStripMenuItem.Click
  Dim con As New SqlConnection
  Dim cmd As New SqlCommand
  Dim CadenaSQL As String = ""
  Dim msg As String = ""

  If (BloquearModVentasToolStripMenuItem.Checked.Equals(False)) Then
   'Si no esta bloqueado entonces cambio status y permito ventas y cotizaciones
   BloquearModVentasToolStripMenuItem.Checked = True
   BloquearModVentasToolStripMenuItem.Text = "Desbloquear cotizaciones y ordenes de venta"
   CadenaSQL = "UPDATE Bloqueo_Ventas set bloqueado = 1"
   msg = "Las cotizaciones y generación de ordenes de venta se han bloqueado, no podrá realizar operaciones en estos módulos"
  Else
   'Si no esta bloqueado entonces cambio status y NO permito ventas y cotizaciones
   BloquearModVentasToolStripMenuItem.Checked = False
   BloquearModVentasToolStripMenuItem.Text = "Bloquear cotizaciones y ordenes de venta"
   CadenaSQL = "UPDATE Bloqueo_Ventas set bloqueado = 0"
   msg = "Las cotizaciones y generación de ordenes de venta se han desbloqueado, ya podrá realizar operaciones en estos módulos"
  End If

  Try
   con.ConnectionString = StrTpm
   con.Open()
   cmd.Connection = con
   cmd.CommandText = CadenaSQL
   cmd.ExecuteNonQuery()

  Catch ex As Exception
   MessageBox.Show("Error al cambiar el status del bloqueo" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
  Finally
   con.Close()
  End Try

  MessageBox.Show(msg, "Cambio de status correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)

 End Sub

 Private Sub AdministraciónDeTabletasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdministraciónDeTabletasToolStripMenuItem.Click
  Admin_Tablets.MdiParent = Me
  Admin_Tablets.Show()
 End Sub

 Private Sub NuevasOrdenesDeVentaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevasOrdenesDeVentaToolStripMenuItem.Click
  ObtenerOrdenVenta.MdiParent = Me
  ObtenerOrdenVenta.Show()
 End Sub

 Private Sub AgenteHistòricoListaDePreciosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgenteHistoricoListaDePreciosToolStripMenuItem.Click
  Historico_ListaPrecios.MdiParent = Me
  Historico_ListaPrecios.Show()
 End Sub

 Private Sub AgenteListaDePrecioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgteListaPrecio.Click
  AgenteListaPrecio.MdiParent = Me
  AgenteListaPrecio.Show()
 End Sub

 Private Sub AgenteHistoricoPasadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgenteHistoricoPasadoToolStripMenuItem.Click
  frmHistoricoPasadoListasPrecio.MdiParent = Me
  frmHistoricoPasadoListasPrecio.Show()
 End Sub

 Private Sub txtmsg_TextChanged(sender As Object, e As EventArgs) Handles txtmsg.TextChanged

 End Sub

 Private Sub AnalisisDeVentaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnalisisDeVentaToolStripMenuItem.Click
  Analisis_de_ventas.MdiParent = Me
  Analisis_de_ventas.Show()
 End Sub

 Private Sub ClasificaciónPorVentasToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClasificaciónPorVentasToolStripMenuItem1.Click
  ClasificacionPorPorcentajes.MdiParent = Me
  ClasificacionPorPorcentajes.Show()
 End Sub

 Private Sub ImpresiónDeUbicacionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImpresiónDeUbicacionesToolStripMenuItem.Click
  frmPrintUbicaciones.MdiParent = Me
  frmPrintUbicaciones.Show()
 End Sub

 Private Sub ChecaAviso()
  Dim vComent As String
  Dim con As New SqlConnection
  Dim cmd As New SqlCommand
  Dim CadenaSQL As String = ""
  'vComent = QuitarCaracteres(txtComentarios.Text)
  Dim Registros As Integer = 0

  Try
   CadenaSQL = "SELECT COUNT(*) as total FROM Alerta_usuarios WHERE (idUsuarioResp = '" + UsrTPM + "' OR idUsuario = '" + UsrTPM + "') AND ConfirmacionLectura IS NULL"
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   conexion_universal.slq_s = New SqlCommand(CadenaSQL, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
   'RECORRE LA CONSULTA
   If conexion_universal.rd_s.Read Then
    Registros = Integer.Parse(rd_s.Item("total").ToString())
   End If
   con.Close()
   conexion_universal.cerrar_conectar()

   If Registros > 1 Then
    timer_aviso.Interval = 500
   Else
                timer_aviso.Interval = 60000
            End If

   CadenaSQL = "SELECT * FROM Alerta_usuarios WHERE (idUsuarioResp = '" + UsrTPM + "' OR idUsuario = '" + UsrTPM + "') AND ConfirmacionLectura IS NULL"
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   conexion_universal.slq_s = New SqlCommand(CadenaSQL, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
   'RECORRE LA CONSULTA
   If conexion_universal.rd_s.Read Then
    timer_aviso.Enabled = False
    lblAlertaUsr.Text = rd_s.Item("Mensaje")
    lbl_IdAlert.Text = rd_s.Item("id")
    panelAvisoAlerta.Visible = True
   End If
  Catch ex As Exception
   MessageBox.Show("Error al obtener el mensaje de la alerta" & ex.Message, "Mensaje de alerta", MessageBoxButtons.OK, MessageBoxIcon.Error)
  Finally
   'CIERRA LAS CONEXIONES DE USO
   con.Close()
   conexion_universal.cerrar_conectar()
  End Try
 End Sub

 Private Sub timer_aviso_Tick(sender As Object, e As EventArgs) Handles timer_aviso.Tick
  'Reviso si existe algun aviso o alerta para el usuario firmado
  If UsrTPM = "" Then Exit Sub
  ChecaAviso()
 End Sub

 Private Sub btn_enteradoAlerta_Click(sender As Object, e As EventArgs) Handles btn_enteradoAlerta.Click
  Dim con As New SqlConnection
  Dim cmd As New SqlCommand
  Dim CadenaSQL As String = ""
  'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
  CadenaSQL = "UPDATE Alerta_usuarios SET ConfirmacionLectura = GETDATE() WHERE id = " & lbl_IdAlert.Text
  'EJECUTA LA CONSULTA
  Try
   con.ConnectionString = StrTpm
   con.Open()
   cmd.Connection = con
   cmd.CommandText = CadenaSQL
   cmd.ExecuteNonQuery()
  Catch ex As Exception
   MessageBox.Show("Error al actualizar el Registro de la alerta" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
   Return
  Finally
   timer_aviso.Enabled = True
   panelAvisoAlerta.Visible = False
   con.Close()
  End Try
 End Sub


 Private Sub ADMINISTRARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ADMINISTRARToolStripMenuItem.Click
  Dim admin As New AdminUbicaciones()
  admin.MdiParent = Me
  admin.Show()

 End Sub
End Class