  using System;
  //LIBRERIA PARA CONECTAR A LA BASE DE DATOS, PERMITE USAR TODAS LAS VARIABLES DE CONEXION PARA MANEJO DEL GESTOR SQL PARA C#
  using System.Data.SqlClient;
  //LIBERIA 
  using System.Windows.Forms;
using System.Configuration;
  ///CLASE QUE REALIZA LAS CONEXION A LAS BASES DE DATOS
  namespace TPD_C
  {
    class conexion
  {
    //Ruta para los reportes
    
    public static String RutaReportes;
    public static String cPassword;
    public static String cUserID;
    public static String cServerName;
    public static String cDatabaseNameSAP;
    public static String cDatabaseNameTMP;
    public static String cDatabaseNameZPRUE2018;
    public static String cDatabaseNameSBO_Diamante;
    public static String cDatabaseNameZPRUE2019;
    public static String cDatabaseNameTPM20FEB19;

    //public const String RutaReportes = "Servidordell";
    //public const String cPassword = "@dministrator1"; //Pr0c3s0.12
    //public const String cUserID = "sa";
    //public const String cServerName = "SERVIDORSAP"; // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS;
    //public const String cDatabaseNameSAP = "SBO_TPD";
    //public const String cDatabaseNameTMP = "TPM";
    //public const String cDatabaseNameZPRUE2018 = "ZPRUEBAS31OCT18";
    //public const String cDatabaseNameSBO_Diamante = "SBO-Diamante-productiva";
    //public const String cDatabaseNameZPRUE2019 = "ZPRUEBAS16ABR2019";
    //public const String cDatabaseNameTPM20FEB19 = "TPM09FEB2019";

    //DECLARACION DE VARIABLES DE CONEXION

    public conexion()
    {
      cUserID = Properties.Settings.Default.gUserID;
      cDatabaseNameZPRUE2018 = Properties.Settings.Default.gDatabaseNameZPRUE2018;
      cDatabaseNameSBO_Diamante = Properties.Settings.Default.gDatabaseNameSBO_Diamante;
      cDatabaseNameZPRUE2019 = Properties.Settings.Default.gDatabaseNameZPRUE2019;
      cDatabaseNameTPM20FEB19 = Properties.Settings.Default.gDatabaseNameTPM20FEB19;
      if (Properties.Settings.Default.AMBIENTE_PRODUCCION) { 
        RutaReportes = Properties.Settings.Default.gRutaReportesProduccion;
        cPassword = Properties.Settings.Default.gPasswordProduccion; //Pr0c3s0.12
        cServerName = Properties.Settings.Default.gServerNameProduccion; // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS;
        cDatabaseNameSAP = Properties.Settings.Default.gDatabaseNameSAPProduccion;
        cDatabaseNameTMP = Properties.Settings.Default.gDatabaseNameTMPProduccion;
      }
      else
      {
        RutaReportes = Properties.Settings.Default.gRutaReportesPruebas;
        cPassword = Properties.Settings.Default.gPasswordPruebas; //Pr0c3s0.12
        cServerName = Properties.Settings.Default.gServerNamePruebas; // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS;
        cDatabaseNameSAP = Properties.Settings.Default.gDatabaseNameSAPPruebas;
        cDatabaseNameTMP = Properties.Settings.Default.gDatabaseNameTMPPruebas;
      }
    }

    public static SqlConnection con; //PERMITE HACER CONEXION A LA BASE DE DATOS
        public static SqlCommand cmd; //PARA PÓDER MANIPULAR LA CONEXION
        public static SqlDataReader dr; //NOS PERMITE IR LEYENDO REGISTRO POR REGISTRO EN UNA CONSULTA HACIA LA BASE DE DATOS
        public static string SQLCadena = ""; //CADENA DE CONEXION A LA BASE DE DATOS

        ////VARIABLES PARA LLENADOS DE COMBOS
        //public static SqlDataAdapter VAdap;
        //public static DataTable VDataT;
        //public static DataSet VDataS;
        //public static DataRow VFila;

        //CONSTRUCTOR PUBLICO PARA LA CONEXION
        public static void conectar(Boolean BaseTMP = false)
        {
            //ALMACENA LA CONSULTA
        if (cServerName == null){
         new conexion();
        }

        if (BaseTMP == false)
          SQLCadena =  "Data Source = " + cServerName + "; Initial Catalog = " + cDatabaseNameSAP + "; Persist Security Info = True; User ID = " + cUserID + "; Password = " + cPassword + "";
        else
        SQLCadena = "Data Source = " + cServerName + "; Initial Catalog = " +   cDatabaseNameTMP + "; Max Pool Size=10024; Persist Security Info = True; User ID = " + cUserID + "; Password = " + cPassword + "";
      //APERTURA DE CONEXION *************************** 
      //CAPTURA EL ERROR SI ES QUE LO HAY EN LA CONEXION O CONSULTA
      try
            {
                //CREA LA CONEXION A LA BASE DE DATOS SAP
                con = new SqlConnection(SQLCadena);
                //ABRE LA CONEXION
                
                con.Open();
                //MANDA MENSAJE SI CONECTO DE MANERA CORRECTA //MENSAJE OPCIONAL
                //MessageBox.Show("Conectado");
            }
            catch (Exception ex){ 
                //MANDA MENSAJE DE ERROR SI NO CONECTO CORRECTAMENTE
                MessageBox.Show("Error de conexion: " + ex.ToString(), "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//FIN CAPTURA EL ERROR SI ES QUE LO HAY EN LA CONEXION O CONSULTA
        }//FIN CONSTRUCTOR PUBLICO PARA LA CONEXION

        //CONSTRUCTOR PUBLICO PARA LA CONEXION
        public static void cerra_conectar()
        {
            //CAPTURA EL ERROR SI ES QUE LO HAY EN LA CONEXION O CONSULTA
            try
            {
                con.Close();
            }
            catch (Exception ex)
            {
                //MANDA MENSAJE DE ERROR SI NO CONECTO CORRECTAMENTE
                MessageBox.Show("Error de conexion: " + ex.ToString(), "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//FIN CAPTURA EL ERROR SI ES QUE LO HAY EN LA CONEXION O CONSULTA
        }//FIN CONSTRUCTOR PUBLICO PARA LA CONEXION
    }
  }///FIN CLASE QUE REALIZA LAS CONEXION A LAS BASES DE DATOS
