using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//LIBRERIA PARA CONECTAR A LA BASE DE DATOS, PERMITE USAR TODAS LAS VARIABLES DE CONEXION PARA MANEJO DEL GESTOR SQL
using System.Data.SqlClient;
//LIBERIA 
using System.Windows.Forms;

namespace TPD_C.Ventas
{
    //INICIO DEL FORMULARIO
    public partial class frmArticuloRemates : Form
    {
        public frmArticuloRemates()
        {
            InitializeComponent();
        }
        //METODO DE RELLENAR COMBOBOX DE ALMACENES
        public void llenarComboAlmacen()
        {
            //CAPTURA EL ERROR AL LLENADO
            try
            {
                //INICIALIZA LA CONEXION
                //conexion c = new conexion();
                new conexion();
                //ALMACENA LA CONSULTA A EJECUTAR
                conexion.cmd = new SqlCommand("SELECT WhsCode as Almacen, WhsName as Nombre FROM OWHS where WhsCode = 03 or WhsCode = 01 or WhsCode = 07", conexion.con);
                
                //EJECUTA LA CONSULTA ANTERIOR
                conexion.dr = conexion.cmd.ExecuteReader();
                //COLOCA EN PRIMER INSTANCIA "TODOS EN EL COMBOBOX"
                cbalmacen.Items.Add("TODOS");
                //RECORRE LOS REGISTROS DE LA CONSULTA ANTERIOR
                while(conexion.dr.Read()){
                    //COLOCA EL DATO OBTENIDO EN LA CONSULTA EN EL COMBO BOX
                    cbalmacen.Items.Add(conexion.dr["Nombre"].ToString());
                }//FIN RECORRE LOS REGISTROS DE LA CONSULTA ANTERIOR
                //CIERRA EL RECORRIDO DEL DATAREADER
                conexion.dr.Close();
                //CIERRA LA CONEXION
                conexion.con.Close();
                //MUESTRE EL PRIMER VALOR DEL COMBO
                cbalmacen.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //MANDA MENSAJE DE ERROR AL LLENAR EL COMBOBOX
                MessageBox.Show("Error en datos en lista de Almacén: "+ex.ToString(), "Error en Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//FIN CAPTURA EL ERROR AL LLENADO
        }//FIN METODO DE RELLENAR COMBOBOX DE ALMACENES
        //METODO DE RELLENAR COMBOBOX DE ALMACENES
        public void llenarComboLineas()
        {
            //CAPTURA EL ERROR AL LLENADO
            try
            {
                //INICIALIZA LA CONEXION
                //conexion c = new conexion();
                new conexion();
                //ALMACENA LA CONSULTA A EJECUTAR
                conexion.cmd = new SqlCommand("select ItmsGrpCod as Linea, ItmsGrpNam as Nombre from OITB", conexion.con);
                //EJECUTA LA CONSULTA ANTERIOR
                conexion.dr = conexion.cmd.ExecuteReader();
                //COLOCA EN PRIMER INSTANCIA "TODOS EN EL COMBOBOX"
                cblinea.Items.Add("TODOS");
                //RECORRE LOS REGISTROS DE LA CONSULTA ANTERIOR
                while (conexion.dr.Read())
                {
                    //COLOCA EL DATO OBTENIDO EN LA CONSULTA EN EL COMBO BOX
                    cblinea.Items.Add(conexion.dr["Nombre"].ToString());
                }//FIN RECORRE LOS REGISTROS DE LA CONSULTA ANTERIOR
                //CIERRA EL RECORRIDO DEL DATAREADER
                conexion.dr.Close();
                //CIERRA LA CONEXION
                conexion.con.Close();
                //MUESTRE EL PRIMER VALOR DEL COMBO
                cblinea.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //MANDA MENSAJE DE ERROR AL LLENAR EL COMBOBOX
                MessageBox.Show("Error en datos en lista de Linea: " + ex.ToString(), "Error en Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//FIN CAPTURA EL ERROR AL LLENADO
        }//^FIN METODO DE RELLENAR COMBOBOX DE ALMACENES

        private void cmdconsultar_Click(object sender, EventArgs e)
        {
            //VARIABLE ALMACENA CCONSULTA
            String consulta = "";
            //DECLRACION DE VARIABLES PARA ALMACEAMIENTO DE FECHAS
            String fi = "";
            String ff = "";
            //VARIABLE PARA LAMACENAR DATOS ELEGIDOS
            //String a1 = "";
            String almacen = cbalmacen.SelectedItem.ToString();
            String linea = cblinea.SelectedItem.ToString();
            //ALMACENAN LA FECHA OBTENIDA EN EL FORMULARIO          
            fi = dtpfecha_ini.Value.ToString("yyyy-MM-dd");
            ff = dtpfecha_fin.Value.ToString("yyyy-MM-dd");
            //VARIABLES DE TIPO FECHA
            DateTime fecha_ini, fecha_fin;
            //CONVIERTE A TIPO FECHA PARA PORDER VALIDAR RANGOS        
            fecha_ini = Convert.ToDateTime(fi).Date;
            fecha_fin = Convert.ToDateTime(ff).Date;
            //VALIDA QUE LAS FECHAS SEAN CORRECTAS
            if(fecha_ini <= fecha_fin){
                //CONSULTA PARA OBTENCION DE LOS DATOS CON LOS CRITERIOS SELECCIONADOS
                //INICIA LA CONEXION
                new conexion();
                //-----------ALMACENA LA CONSULTA A EJECUTAR
                //OBTIENE LOS ARTICULOS QUE SON DE REMATE
                consulta = " select t0.ItemCode as Articulo, t0.ItemName as Descripcion, t0.ItmsGrpCod as Linea into #ArticulosRemate from OITM t0 where ItemName like '%RMTE%' ";
                //HABILITAR SI SE QUIERE QUE NO SEAN DE REMATE
                //consulta = " select t0.ItemCode as Articulo, t0.ItemName as Descripcion, t0.ItmsGrpCod as Linea into #ArticulosRemate from OITM t0";
                //OBTIENE TODAS LAS LINEAS CORRESPONDIENTE LOS ARTICULOS DE REMATE
                consulta += " select t1.Articulo as Articulo, T1.Descripcion as Descripcion, ItmsGrpNam as Linea ";
                consulta += " into #LineasRemates ";
                consulta += " from OITB t0 inner join #ArticulosRemate T1 on t0.ItmsGrpCod = T1.Linea ";
                //VALIDA QUE LINEA ES
                if(linea != "TODOS"){
                    consulta += " and t0.ItmsGrpNam = '" + linea + "' ";
                }//FIN VALIDA QUE LINEA ES
                //OBTIENE ARTIUCLOS, LINEAS, Y STOCK POR ALMACENES
                consulta += " select t0.Articulo, t0.Descripcion, t0.Linea, ";
                //VALIDA QUE ALMACEN ES
                if (almacen == "PUEBLA"){
                    consulta += " (select OnHand from OITW where ItemCode = t0.Articulo and WhsCode = 01) as 'PUEBLA', ";
                    consulta += " 0 as 'MÉRIDA', ";
                    consulta += " 0 as 'TUXTLAGTZ' ";
                    //a1 = "01";
                }else if (almacen == "MÉRIDA"){
                    consulta += " 0 as 'PUEBLA', ";
                    consulta += " (select OnHand from OITW where ItemCode = t0.Articulo and WhsCode = 03) as 'MÉRIDA', ";
                    consulta += " 0 as 'TUXTLAGTZ' ";
                    //a1 = "03";
                }else if (almacen == "TUXTLA GTZ"){
                    consulta += " 0 as 'PUEBLA', ";
                    consulta += " 0 as 'MÉRIDA', ";
                    consulta += " (select OnHand from OITW where ItemCode = t0.Articulo and WhsCode = 07) as 'TUXTLAGTZ' ";
                    //a1 = "07";
                }else if (almacen == "TODOS") {
                    consulta += " (select OnHand from OITW where ItemCode = t0.Articulo and WhsCode = 01) as 'PUEBLA', ";
                    consulta += " (select OnHand from OITW where ItemCode = t0.Articulo and WhsCode = 03) as 'MÉRIDA', ";
                    consulta += " (select OnHand from OITW where ItemCode = t0.Articulo and WhsCode = 07) as 'TUXTLAGTZ' ";
                }//FIN VALIDA QUE ALMACEN ES
                consulta += " into #ArticulosRemateStock ";
                consulta += " from #LineasRemates t0 ";
                //OBTIENE EL STOCK TOTAL DE CADA ARTICULO DE REMATE EN CADA ALMACEN
                consulta += " select *, (PUEBLA + MÉRIDA + TUXTLAGTZ) as StockTotal ";
                consulta += " into #StockTotal ";
                consulta += " from #ArticulosRemateStock ";
                //OBTIENE LOS PRECIOS DE ARTICULOS EN LISTA 1 Y 9 CON TODOS LOS DATOS DE ARTICULOS Y ALMACENES
                consulta += " select *, ";
                consulta += " round((select Price from ITM1 where t0.Articulo = ItemCode and PriceList = 1),2) as 'Lista01', ";
                consulta += " round((select Price from ITM1 where t0.Articulo = ItemCode and PriceList = 9),2) as 'Lista09' ";
                consulta += " into #StockPrecio ";
                consulta += " from #StockTotal t0 ";
                //OBTIENE EL TOTAL MONETARIO DE LO QUE HAY EN STOCK * LOS PRECIOS 1 Y 9
                consulta += " select Articulo, Descripcion, Linea, PUEBLA, MÉRIDA, TUXTLAGTZ, StockTotal, Lista01, ";
                consulta += " case when round(Lista01 * StockTotal, 0) = 0 then 0 else round(Lista01 * StockTotal, 0) end as 'TotalL01', ";
                consulta += " Lista09, ";
                consulta += " case when round(Lista09 * StockTotal, 0) = 0 then 0 else round(Lista09 * StockTotal, 0) end as 'TotalL09' ";
                consulta += " into #StockTotalListas ";
                consulta += " from #StockPrecio ";





                /*SE OBTIENEN LA VENTAS BRUTAS DE CADA ARTICULO DE REMATE*/
                //OBTIENE TODAS LAS VENTAS DE CIERTO ARTICULO DE UN PERIODO ESTABLECIDO
                //consulta += " select distinct t0.DocEntry, t0.DocNum, t1.ItemCode, t1.LineTotal, t1.VatSum, t1.Quantity, t1.WhsCode ";
                //consulta += " into #TodasFacturasArticulos ";
                //consulta += " from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry ";
                //consulta += " inner join OITM t2 on t1.ItemCode = t2.ItemCode ";
                //consulta += " where t0.DocDate between '2018-04-01' and '2018-04-10'  ";
                //consulta += " and t0.DocType <> 'S' ";
                //consulta += " and t2.ItmsGrpCod <> 200 "; 
                //consulta += " and t1.Dscription like '%RMTE%' ";
                consulta += " select distinct t0.DocEntry, t0.DocNum, t1.ItemCode, t1.LineTotal, t1.VatSum, t1.Quantity, t1.WhsCode ";
                consulta += " into #TodasFacturasArticulos ";
                consulta += " from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry ";
                consulta += " inner join #ArticulosRemate t3 on t1.ItemCode = t3.Articulo ";
                consulta += " inner join OITM t2 on t3.Articulo = t2.ItemCode ";
                consulta += " where t0.DocDate between '" + fi + "' and '" + ff + "' ";
                consulta += " and t0.DocType <> 'S' ";
                consulta += " and t2.ItmsGrpCod <> 200 ";
                consulta += " and t1.ItemCode = t3.Articulo ";
               
                //OBTIENE TODAS LAS NC NO TIMBRADAS DE ARTICULOS
                //consulta += " select distinct t0.DocEntry, t0.DocNum, t1.ItemCode, t1.LineTotal, t1.VatSum, t1.Quantity, t1.WhsCode ";
                //consulta += " into #NC_Art_NoTimb ";
                //consulta += " from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry ";
                //consulta += " inner join OITM t2 on t1.ItemCode = t2.ItemCode ";
                //consulta += " left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 ";
                //consulta += " where t0.DocDate between '" + fi + "' and '" + ff + "' ";
                //consulta += " and t0.DocType <> 'S' ";
                //consulta += " and t2.ItmsGrpCod <> 200 ";
                //consulta += " and t3.ReportID is null ";
                //consulta += " and t1.Dscription like '%RMTE%' ";

                consulta += " select distinct t0.DocEntry, t0.DocNum, t1.ItemCode, t1.LineTotal, t1.VatSum, t1.Quantity, t1.WhsCode ";
                consulta += " into #NC_Art_NoTimb ";
                consulta += " from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry ";
                consulta += " inner join #ArticulosRemate t4 on t1.ItemCode = t4.Articulo ";
                consulta += " inner join OITM t2 on t1.ItemCode = t4.Articulo ";
                consulta += " left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 ";
                consulta += " where t0.DocDate between '" + fi + "' and '" + ff + "' ";
                consulta += " and t0.DocType <> 'S' ";
                consulta += " and t2.ItmsGrpCod <> 200 ";
                consulta += " and t3.ReportID is null ";
                consulta += " and t1.ItemCode = t4.Articulo ";

                //INSERTA EN #TEMP1 LS VENTAS SIN IVA DE LAS FACTURAS
                consulta += " SELECT t0.ItemCode as Articulo, t0.WhsCode, (t0.LineTotal) as VtaAntesIva ";
                consulta += " into #tmp1 ";
                consulta += " FROM #TodasFacturasArticulos t0 ";
                consulta += " union all ";
                consulta += " SELECT t0.ItemCode as Articulo, t0.WhsCode, (t0.LineTotal)*-1 as VtaAntesIva ";
                consulta += " FROM #NC_Art_NoTimb t0 ";
                consulta += " ORDER BY VtaAntesIva DESC "; 

                //DEVOLUCION******************
                //OBTIENE TODAS LAS NC TIMBRADAS DEL PERIODO ESTABLECIDO
                consulta += " select distinct t0.DocEntry, t0.DocNum, t1.ItemCode, t1.LineTotal, t0.VatSum, t1.Quantity, t1.WhsCode ";
                consulta += " into #NC_Art_Timb ";
                consulta += " from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry ";
                consulta += " inner join OITM t2 on t1.ItemCode = t2.ItemCode ";
                consulta += " left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 ";
                consulta += " where t0.DocDate between '" + fi + "' and '" + ff + "' ";
                consulta += " and t0.DocType <> 'S' ";
                consulta += " and t2.ItmsGrpCod <> 200 "; 
                consulta += " and t3.ReportID is not null ";
                consulta += " and t1.Dscription like '%RMTE%' ";

                //NC ANTES DEL IVA
                consulta += " select WhsCode, ItemCode as Articulo, SUM(LineTotal) as DevAntesIva ";
                consulta += " into #ArticuloDevTotal ";
                consulta += " from #NC_Art_Timb ";
                consulta += " group by WhsCode, ItemCode ";
                consulta += " order by DevAntesIva DESC ";
                //OBTIENE LAS CANTIDADES DEVUELTAS O CANCELADAS Y LAS RESTA A LAS VENTAS
                consulta += " select DocEntry, DocNum, ItemCode, LineTotal, VatSum, Quantity, WhsCode ";
                consulta += " into #n ";
                consulta += " from #TodasFacturasArticulos ";
                consulta += " union all ";
                consulta += " select DocEntry, DocNum, ItemCode, LineTotal, VatSum, (Quantity*-1) as Quantity, WhsCode from #NC_Art_NoTimb ";
                consulta += " union all ";
                consulta += " select DocEntry, DocNum, ItemCode, LineTotal, VatSum, (Quantity*-1) as Quantity, WhsCode from #NC_Art_Timb ";
                //consulta += " select ItemCode, sum(Quantity), WhsCode ";
                //consulta += " from #n ";
                //consulta += " group by ItemCode, WhsCode ";
                //REALIZA LA RESTA DE LAS VENTAS - DEVOLUCIONES DE UN ARTICULO EN ESPECIFICO
                //MODIFICACION 04-08-2018 POR AJUSTE EN DEVOLUCIONES
                consulta += "select distinct(t1.Articulo), t1.WhsCode, sum(t1.DevAntesIva) as DevAntesIva ";
                consulta += "into #TempTotal_Dev ";
                consulta += "from #ArticuloDevTotal t1 group by t1.Articulo, t1.WhsCode ";
                //consulta += " select distinct(t0.WhsCode), t0.Articulo, sum(t0.VtaAntesIva) - isnull((select t1.DevAntesIva from #ArticuloDevTotal t1 where t1.Articulo = t0.Articulo),0) as ArticuloTotal ";
                //consulta += " select distinct(t0.WhsCode), t0.Articulo, sum(t0.VtaAntesIva) - isnull((select sum(t1.DevAntesIva) as DevAntesIva from #ArticuloDevTotal t1 where t1.Articulo = t0.Articulo group by t1.Articulo),0) as ArticuloTotal ";
                consulta += " select distinct(t0.WhsCode), t0.Articulo, sum(t0.VtaAntesIva) - isnull((select t1.DevAntesIva as DevAntesIva from #TempTotal_Dev t1 where t1.Articulo = t0.Articulo and t0.WhsCode = t1.WhsCode),0) as ArticuloTotal ";
                consulta += " into #ImporteVtaTotal ";
                consulta += " from #tmp1 t0 ";
                consulta += " group by t0.WhsCode, t0.Articulo ";
                //OBTIENE EL MONTO TOTAL EN PIEZAS VENDIDAS POR CADA ALMACEN
                consulta += " select *, ";
                //VALIDA EL ALMACEN
                if (almacen == "PUEBLA")
                {
                    consulta += " ISNULL((select sum(Quantity) as Cantidad from #n where ItemCode = t0.Articulo and WhsCode = 01),0) as 'PzaVentaPuebla', ";
                    consulta += " ISNULL((select ArticuloTotal as Cantidad from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '01'),0) as 'ImpVtaPuebla', ";
                    consulta += " 0 as 'PzaVentaMerida', ";
                    consulta += " 0 as 'ImpVtaMerida', ";
                    consulta += " 0 as 'PzaTuxtla', ";
                    consulta += " 0 as 'ImpVtaTuxtla', ";
                    consulta += " ISNULL((select SUM(ArticuloTotal) from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '01' group by Articulo), 0) as 'ImporteTotal' ";
                }
                else if (almacen == "MÉRIDA")
                {
                    consulta += " 0 as 'PzaVentaPuebla', ";
                    consulta += " 0 as 'ImpVtaPuebla', ";
                    consulta += " ISNULL((select sum(Quantity) as Cantidad from #n where ItemCode = t0.Articulo and WhsCode = 03),0) as 'PzaVentaMerida', ";
                    consulta += " ISNULL((select ArticuloTotal as Cantidad from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '03'),0) as 'ImpVtaMerida', ";
                    consulta += " 0 as 'PzaTuxtla', ";
                    consulta += " 0 as 'ImpVtaTuxtla', ";
                    consulta += " ISNULL((select SUM(ArticuloTotal) from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '03' group by Articulo), 0) as 'ImporteTotal' ";
                }
                else if (almacen == "TUXTLA GTZ")
                {
                    //consulta += " 0 as 'PzaVentaPuebla', ";
                    //consulta += " 0 as 'ImpVtaPuebla', ";
                    //consulta += " 0 as 'PzaVentaMerida', ";
                    //consulta += " 0 as 'ImpVtaMerida', ";
                    //consulta += " ISNULL((select sum(Quantity) as Cantidad from INV1 where DocDate between '" + fi + "' and '" + ff + "' and ItemCode = t0.Articulo and WhsCode = '07'),0) as 'PzaTuxtla', ";
                    //consulta += " ISNULL((select ArticuloTotal as Cantidad from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '07'),0) as 'ImpVtaTuxtla', ";
                    //consulta += " ISNULL((select SUM(ArticuloTotal) from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '07' group by Articulo), 0) as 'ImporteTotal' ";
                    consulta += " 0 as 'PzaVentaPuebla', ";
                    consulta += " 0 as 'ImpVtaPuebla', ";
                    consulta += " 0 as 'PzaVentaMerida', ";
                    consulta += " 0 as 'ImpVtaMerida', ";
                    consulta += " ISNULL((select sum(Quantity) as Cantidad from #n where ItemCode = t0.Articulo and WhsCode = 07),0) as 'PzaTuxtla', ";
                    consulta += " ISNULL((select ArticuloTotal as Cantidad from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '07'),0) as 'ImpVtaTuxtla', ";
                    consulta += " ISNULL((select SUM(ArticuloTotal) from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '07' group by Articulo), 0) as 'ImporteTotal' ";
                }
                else if (almacen == "TODOS")
                {
                    //consulta += " ISNULL((select sum(Quantity) as Cantidad from INV1 where DocDate between '" + fi + "' and '" + ff + "' and ItemCode = t0.Articulo and WhsCode = '01'),0) as 'PzaVentaPuebla', ";
                    consulta += " ISNULL((select sum(Quantity) as Cantidad from #n where ItemCode = t0.Articulo and WhsCode = 01),0) as 'PzaVentaPuebla', ";
                    consulta += " ISNULL((select ArticuloTotal as Cantidad from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '01'),0) as 'ImpVtaPuebla', ";
                    consulta += " ISNULL((select sum(Quantity) as Cantidad from #n where ItemCode = t0.Articulo and WhsCode = 03),0) as 'PzaVentaMerida', ";
                    consulta += " ISNULL((select ArticuloTotal as Cantidad from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '03'),0) as 'ImpVtaMerida', ";
                    consulta += " ISNULL((select sum(Quantity) as Cantidad from #n where ItemCode = t0.Articulo and WhsCode = 07),0) as 'PzaTuxtla', ";
                    consulta += " ISNULL((select ArticuloTotal as Cantidad from #ImporteVtaTotal where Articulo = t0.Articulo and WhsCode = '07'),0) as 'ImpVtaTuxtla', ";
                    consulta += " ISNULL((select SUM(ArticuloTotal) from #ImporteVtaTotal where Articulo = t0.Articulo group by Articulo), 0) as 'ImporteTotal' ";
                }//FIN VALIDA EL ALMACEN
                consulta += " into #TotalAlmacenesVentas ";
                consulta += " from #StockTotalListas t0 ";
                //OBTIENE EL IMPORTE DE VENTAS DE TODOS LOS ARTICULOS DE REMATE, ASI COMO TODOS SUS DATOS
                consulta += " select Articulo, Descripcion, Linea, PUEBLA as 'Stock Puebla', MÉRIDA as 'Stock Merida', ";
                consulta += " TUXTLAGTZ as 'Stock Tuxtla', ";
                consulta += " StockTotal as 'Stock Total', Lista01 as 'Lista 01', TotalL01 as 'Total L01', Lista09 as 'Lista 09', TotalL09 as 'Total L09', ";
                consulta += " PzaVentaPuebla as 'Pza. Vendidas Puebla', ImpVtaPuebla as 'Imp. Vta. Puebla', PzaVentaMerida as 'Pza. Vendidas Merida', ImpVtaMerida as 'Imp. Vta. Merida', ";
                consulta += "  PzaTuxtla as 'Pza. Vendidas Tuxtla', ImpVtaTuxtla as 'Imp. Vta. Tuxtla', PzaVentaPuebla + PzaTuxtla + PzaVentaMerida as 'Total Pzas. Vdas' , ImporteTotal as 'Importe Vtas' ";
                consulta += " from #TotalAlmacenesVentas ";
                if(linea != "TODOS"){
                    //VALIDA SI SON ARTICULOS SOLO CON VENTA
                    if (cbarticulos_ventas.Checked == true){
                        consulta += " where Linea = '" + linea + "' and ImporteTotal <> 0";
                    }else{
                        consulta += " where Linea = '" + linea + "' ";
                    }
                }//FIN VALIDA SI SE QUIEREN TODAS LAS LINEAS O NO
                //VALIDA SI SON ARTICULOS SOLO CON VENTA
                if (cbarticulos_ventas.Checked == true && linea == "TODOS")
                {
                    consulta += " where ImporteTotal <> 0";
                }
                
                //UNE LOS MONTOS TOTALES JUNTO CON LOS ARTICULOS DE REMATES
                consulta += " UNION ALL ";
                consulta += " select 'MONTOS TOTALES', '', '', sum(PUEBLA), sum(MÉRIDA), ";
                consulta += " sum(TUXTLAGTZ), ";
                consulta += " sum(StockTotal), sum(Lista01), sum(TotalL01), sum(Lista09), sum(TotalL09), ";
                consulta += " sum(PzaVentaPuebla), sum(ImpVtaPuebla), sum(PzaVentaMerida), sum(ImpVtaMerida), ";
                consulta += " sum(PzaTuxtla), sum(ImpVtaTuxtla), ";
                consulta += " sum(PzaVentaPuebla) + sum(PzaTuxtla) + sum(PzaVentaMerida), ";
                consulta += " sum(ImporteTotal) ";
                consulta += " from #TotalAlmacenesVentas ";
                if (cbarticulos_ventas.Checked == true)
                {
                    consulta += " where ImporteTotal <> 0";
                }
                //ELIMINA LAS TABLAS TEMPORALES
                consulta += " drop table #ArticulosRemate ";
                consulta += " drop table #LineasRemates ";
                consulta += " drop table #ArticulosRemateStock ";
                consulta += " drop table #StockTotal ";
                consulta += " drop table #StockPrecio ";
                consulta += " drop table #StockTotalListas ";
                consulta += " drop table #TotalAlmacenesVentas ";
                //VENTAS TOTALES POR ARTICULO DE REMATE
                consulta += " drop table #TodasFacturasArticulos  ";
                consulta += " drop table #NC_Art_NoTimb  ";
                consulta += " drop table #tmp1 ";
                consulta += " drop table #NC_Art_Timb ";
                consulta += " drop table #ArticuloDevTotal ";
                consulta += " drop table #ImporteVtaTotal ";
                consulta += " drop table #tempTotal_Dev ";
                consulta += " drop table #n ";

           
                conexion.cmd = new SqlCommand(consulta, conexion.con);
                //CAPTURA EL ERROR DE LA CONSULTA O DEL DATAGRID
                try
                {
                    //BORRA EL DATAGRID DEL RESULTADO
                    //if (dgvarticulos.RowCount > 0 ) {
                    //    dgvarticulos.Rows.Clear();
                    //}
        
                    //VARIABLES PARA LLENADO DE DATAGRID CON LA CONSULTA DE SQL
                    SqlDataAdapter da = new SqlDataAdapter(conexion.cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    //LLENA DATAGRID EN MEMORIA PARA COLOCAR LOS DATOS DEL DETALLE DE ARTICULOS
                    dgvarticulos.DataSource = ds;
                    dgvarticulos.DataMember = ds.Tables[0].ToString();
                    //FORMATO EN MONEDA DE SOLO 2 DECIMALES Y NUMERO EN CANTIDADES
                    dgvarticulos.Columns["Stock Puebla"].DefaultCellStyle.Format = "N0";
                    dgvarticulos.Columns["Stock Merida"].DefaultCellStyle.Format = "N0";
                    dgvarticulos.Columns["Stock Tuxtla"].DefaultCellStyle.Format = "N0";
                    dgvarticulos.Columns["Stock Total"].DefaultCellStyle.Format = "N0";
                    dgvarticulos.Columns["Lista 01"].DefaultCellStyle.Format = "C2";
                    dgvarticulos.Columns["Total L01"].DefaultCellStyle.Format = "C2";
                    dgvarticulos.Columns["Lista 09"].DefaultCellStyle.Format = "C2";
                    dgvarticulos.Columns["Total L09"].DefaultCellStyle.Format = "C2";
                    dgvarticulos.Columns["Pza. Vendidas Puebla"].DefaultCellStyle.Format = "N0";
                    dgvarticulos.Columns["Imp. Vta. Puebla"].DefaultCellStyle.Format = "C2";
                    dgvarticulos.Columns["Pza. Vendidas Merida"].DefaultCellStyle.Format = "N0";
                    dgvarticulos.Columns["Imp. Vta. Merida"].DefaultCellStyle.Format = "C2";
                    dgvarticulos.Columns["Pza. Vendidas Tuxtla"].DefaultCellStyle.Format = "N0";
                    dgvarticulos.Columns["Imp. Vta. Tuxtla"].DefaultCellStyle.Format = "C2";
                    dgvarticulos.Columns["Total Pzas. Vdas"].DefaultCellStyle.Format = "N0";
                    dgvarticulos.Columns["Importe Vtas"].DefaultCellStyle.Format = "C2";
                    //FORMATO DE ALINEACION DE TEXTO SEGUN LO QUE SE REQUIERA
                    dgvarticulos.Columns["Stock Puebla"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvarticulos.Columns["Stock Merida"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvarticulos.Columns["Stock Tuxtla"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvarticulos.Columns["Stock Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvarticulos.Columns["Lista 01"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvarticulos.Columns["Total L01"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //COMENTAR ESTAS DOS LINEAS SI SE QUIERE QUE SE VEA EL PRECIO 9 Y EL TOTAL, DESCOMENTAR LAS 2 LINEAS DE ABAJO
                    dgvarticulos.Columns["Lista 09"].Visible = false;
                    dgvarticulos.Columns["Total L09"].Visible = false;
                    //dgvarticulos.Columns["Lista 09"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgvarticulos.Columns["Total L09"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvarticulos.Columns["Importe Vtas"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvarticulos.Columns["Pza. Vendidas Puebla"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvarticulos.Columns["Imp. Vta. Puebla"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvarticulos.Columns["Pza. Vendidas Merida"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvarticulos.Columns["Imp. Vta. Merida"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvarticulos.Columns["Pza. Vendidas Tuxtla"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvarticulos.Columns["Total Pzas. Vdas"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvarticulos.Columns["Imp. Vta. Tuxtla"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    
                    //COLOCA EN NEGRITAS LA ULTIMA FILA DE LOS TOTALES
                    dgvarticulos.Rows[dgvarticulos.Rows.Count - 2].DefaultCellStyle.Font = new Font(dgvarticulos.DefaultCellStyle.Font, FontStyle.Bold);
                    //COLOCA QUE ESTEN FIJAS ESTAS COLUMNAS
                    dgvarticulos.Columns["Articulo"].Frozen = true;
                    dgvarticulos.Columns["Descripcion"].Frozen = true;
                    dgvarticulos.Columns["Linea"].Frozen = true;
                    //COLOCA EN GRIS LA COLUMNA DE TOTAL DE PIEZAS Y VENTAS
                    dgvarticulos.Columns[17].DefaultCellStyle.BackColor = Color.Coral;
                    dgvarticulos.Columns[18].DefaultCellStyle.BackColor = Color.Goldenrod;
                    //DEFINE TAMAÑOS DE COLUMNAS
                    dgvarticulos.Columns[3].Width = 50;
                    dgvarticulos.Columns[4].Width = 50;
                    dgvarticulos.Columns[5].Width = 50;
                    dgvarticulos.Columns[6].Width = 50;
                    dgvarticulos.Columns[11].Width = 55;
                    dgvarticulos.Columns[13].Width = 55;
                    dgvarticulos.Columns[15].Width = 55;
                    dgvarticulos.Columns[17].Width = 55;
                    //TAMAÑO MAS GRANDE
                    dgvarticulos.Columns[7].Width = 80;
                    dgvarticulos.Columns[8].Width = 80;
                    dgvarticulos.Columns[12].Width = 80;
                    dgvarticulos.Columns[14].Width = 80;
                    dgvarticulos.Columns[16].Width = 80;
                    dgvarticulos.Columns[18].Width = 80;
                    //dgvarticulos.Columns[15].Width = 80;
                    
                }
                catch (Exception ex)
                {
                    //MANDA MENSAJE A PANTALLA CON ERROR CORRESPONDIENTE
                    MessageBox.Show("Error de llenado en el detalle: "+ex.ToString(), "Error de Llenado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }//FIN CAPTURA EL ERROR DE LA CONSULTA O DEL DATAGRID
            } else {//ELSE VALIDA QUE LAS FECHAS SEAN CORRECTAS
                //MANADA MENSAJE EN PANTALLA DE ERROR, Y NO EJECUTA NINGUN CODIGO
                MessageBox.Show("La fecha de Inicio NO puede ser mayor a la Final.", "Error en Perido de Fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MANDA EL FOCUS AL ELEMENTO QUE SE REQUIERA
                dtpfecha_ini.Focus();
            }//VALIDA QUE LAS FECHAS SEAN CORRECTAS
            //PAGINA PARA VALIDAR ALGUNA ACCION CON MENSAJES
            //https://msdn.microsoft.com/es-es/library/3tt9e94f(v=vs.110).aspx
        }

        //CODIGO PRINCIPAL DEL FORMULARIO
        private void frmArticuloRemates_Load(object sender, EventArgs e)
        {
            //MANDA A LLAMAR EL METODO DE LLENADO DEL COMBO BOX DE ALMACEN
            llenarComboAlmacen();
            //MANDA A LLAMAR AL METODO DE LLENADO DEL COMBO BOX DE ALMACEN
            llenarComboLineas();
        }//FIN CODIGO PRINCIPAL DEL FORMULARIO

        //INICIO DE BOTON DE EXPORTAR A EXCEL
        private void cmdexportar_Click(object sender, EventArgs e)
        {
            //VALIDA QUE EL DATAGRIDVIEW NO ESTE VACIO
            if (dgvarticulos.Rows.Count > 0){
                //CONTADOR DE LAS COLUMNAS DONDE TENDRA QUE EMPEZAR EL EMPATE CON EL EXCEL
                int con = 4, confila = 0;
                //DECLARACIÓN DE VARIABLES PARA LIBRO DE EXCEL CREANDO UN OBJETO DE EXCEL
                Microsoft.Office.Interop.Excel.Application Excel;
                Microsoft.Office.Interop.Excel.Workbook Libro;
                Microsoft.Office.Interop.Excel.Worksheet hoja;
                //CREA EL EXCEL
                Excel = new Microsoft.Office.Interop.Excel.Application();
                //CREA EL LIBRO DE EXCEL
                Libro = Excel.Workbooks.Add();
                //ENCABEZADOS
                Excel.Range["A1"].Value = "Reporte de Articulos de Remate";
                Excel.Range["E1"].Value = "Periodo: " + dtpfecha_ini.Value.ToString("yyyy-MM-dd") + " al " + dtpfecha_fin.Value.ToString("yyyy-MM-dd");
                Excel.Range["A3"].Value = "Articulo";
                Excel.Range["B3"].Value = "Descripción";
                Excel.Range["C3"].Value = "Linea";
                Excel.Range["D3"].Value = "Stock Puebla";
                Excel.Range["E3"].Value = "Stock Merida";
                Excel.Range["F3"].Value = "Stock Tuxtla";
                Excel.Range["G3"].Value = "Stock Total";
                Excel.Range["H3"].Value = "Lista 01";
                Excel.Range["I3"].Value = "Total L01";
                //Excel.Range["J3"].Value = "Lista 09";
                //Excel.Range["K3"].Value = "Total L09";
                Excel.Range["J3"].Value = "Pzas. Vendidas Pue.";
                Excel.Range["K3"].Value = "Imp. Vtas. Pue.";
                Excel.Range["L3"].Value = "Pzas. Vendidas Mer.";
                Excel.Range["M3"].Value = "Imp. Vtas. Mer.";
                Excel.Range["N3"].Value = "Pzas. Vendidas Tux.";
                Excel.Range["O3"].Value = "Imp. Vtas. Tux.";
                Excel.Range["P3"].Value = "Total Pzas. Vdas";
                Excel.Range["Q3"].Value = "Total";

                //CREA LA HOJA DE EXCEL
                hoja = (Microsoft.Office.Interop.Excel.Worksheet)Libro.Worksheets.get_Item(1);
                //RECORREMOS EL DATAGRIDVIEW RELLENANDO LA HOJA DE TRABAJO DE EXCEL
                for (int i = 0; i < dgvarticulos.Rows.Count - 1; i++)//RECORRE LAS COLUMNAS
                {
                    for (int j = 0; j < dgvarticulos.Columns.Count; j++)//RECORRE LAS FILAS
                    {
                        //hoja.Cells[i + 1, j + 1] = dgvarticulos.Rows[i].Cells[j].Value.ToString();
                        //VALIDA QUE NO COLOQUE LAS LISTA 9 Y SU TOTAL
                        if (j != 9 && j != 10) {
                            //AGREGA A LA HOJA DE EXCEL LOS DATOS OBTENIDOS DEL DATAGRIDVIE
                            //hoja.Cells[con, j + 1] = dgvarticulos.Rows[i].Cells[j].Value.ToString();
                            hoja.Cells[con, confila + 1] = dgvarticulos.Rows[i].Cells[j].Value.ToString();
                            confila++;
                        }//FIN VALIDA QUE NO COLOQUE LAS LISTA 9 Y SU TOTAL
                    } 
                    con++;
                    confila = 0;
                }//FIN RECORREMOS EL DATAGRIDVIEW RELLENANDO LA HOJA DE TRABAJO DE EXCEL
                //ESTO SOLO SI SE QUIERE GUARDAR EL ARCHIVO EN AUTOMATICO
                /*SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xls)|*.xls";
                if (fichero.ShowDialog() == DialogResult.OK)
                libro.SaveAs(fichero.FileName,
                Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                Libro.Close(true);
                Excel.Quit();*/
                //MUESTRA EL LIBRO DE EXCEL
                Excel.Visible = true;
                hoja = null;
                Libro = null;
                Excel = null;
            }else{
                MessageBox.Show("No hay datos que Exportar.", "Datos no Encontrados", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                dtpfecha_ini.Focus();
            }//FIN VALIDA QUE EL DATAGRIDVIEW NO TRAIGA 0
        }//FIN DE BOTON DE EXPORTAR A EXCEL
    }//FIN DEL FORMULARIO
    //EXPORTACIÓN PARA EL ARCHIVO
    //https://webprogramacion.com/334/csharp/exportar-datos-de-un-datagridview-a-un-fichero-excel-en-c.aspx
}
