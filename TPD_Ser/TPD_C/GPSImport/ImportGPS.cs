using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TPD_C.ControlVehiculo;

namespace TPD_C.GPSImport
{
    public class ImportGPS
    {

        SqlCommand cmd;
        //SqlConnection cn;

        public static void impInfGPD()
        {
            //CONECTA A LA BASE DE DATOS
            conexion.conectar(true);
            //ALMACEN LA CONSULTA EN UN CMD

            //EJECUTA LA CONSULTA
            conexion.dr = conexion.cmd.ExecuteReader();
        }

        public int ImportaInfGPS(string IdGPSAsesor, string NombreAsesor, DateTime FechaI, DateTime FechaF, ref ProgressBar PBStatus)
        {
            try
            {
                APIService aPIService = new APIService();
                return aPIService.GetRutesByVehicle(FechaI, FechaF, IdGPSAsesor);
            }
            catch (SystemException e)
            {
                return 0;
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public string Insertar(string Conductor, string idVehiculo, string id,
            string fechaSalida, string lugarSalida, string horaFinalSalida,
            string fechaLlegada, string lugarLlegada, string horaFinalLlegada,
            float latitud, float longitud)
        {
            string salida = "Si se insertó";
            try
            {
                conexion.conectar(true);
                SqlConnection cn = new SqlConnection(conexion.SQLCadena);
                conexion.cmd = new SqlCommand("SELECT Count(id) Existe FROM Registro_Viajes_Diarios WHERE id = " + id, conexion.con);
                conexion.dr = conexion.cmd.ExecuteReader();
                cn.Open();
                while (conexion.dr.Read())
                {

                    if (Convert.ToInt16(conexion.dr["Existe"]) == 0)
                    {
                        cmd = new SqlCommand("INSERT INTO Registro_Viajes_Diarios (Conductor,idVehiculo,id, fechaSalida, lugarSalida, horaFinalSalida, fechaLlegada, lugarLlegada, horaFinalLlegada,latitusp,longitudsp)" +
                          " values('" + Conductor + "','" + idVehiculo + "'," + id + ",'" + fechaSalida + "','" + lugarSalida + "','" + horaFinalSalida + "','" + fechaLlegada + "','" + lugarLlegada + "','" + horaFinalLlegada
                          + "'," + latitud + "," + longitud + ")", cn);
                        cmd.ExecuteNonQuery();
                    }
                }
                cn.Close();
                conexion.cerra_conectar();
            }
            catch (Exception ex)
            {
                salida = "No se conecto:" + ex.ToString();
            }
            return salida;
        }
    }
}
