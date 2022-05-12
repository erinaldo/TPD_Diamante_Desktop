  using System;
  using Newtonsoft.Json;
  using System.Data.SqlClient;
  using System.IO;
  using System.Net;

  using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace TPD_C.GPSImport
  {
    public class ImportGPS
    {

    SqlCommand cmd;
    SqlConnection cn;

    public void impInfGPD()
    {
      //CONECTA A LA BASE DE DATOS
      conexion.conectar(true);
      //ALMACEN LA CONSULTA EN UN CMD

      //conexion.cmd = new SqlCommand(SQLFactura, conexion.con);

      //EJECUTA LA CONSULTA
      conexion.dr = conexion.cmd.ExecuteReader();
      //RECORRE LA CONSULTA
      while (conexion.dr.Read()){
        //if (conexion.dr["Nombre"] > 0)
      }
    }

    public int ImportaInfGPS(String IdGPSAsesor, String NombreAsesor, DateTime FechaI, DateTime FechaF, ref ProgressBar PBStatus)
    {
      try
      {
        IdGPSAsesor = IdGPSAsesor.Replace("\n", "");
        IdGPSAsesor = IdGPSAsesor.Replace("\r", "");
        NombreAsesor = NombreAsesor.Replace("\n", "");
        NombreAsesor = NombreAsesor.Replace("\r", "");

        //20200919T000000
        //20200920T235959
        String fInicio = "";
        String fFin = "";

        fInicio = FechaI.Year.ToString() + (FechaI.Month < 10 ? "0" + FechaI.Month.ToString() : FechaI.Month.ToString()) + (FechaI.Day < 10 ? "0" + FechaI.Day.ToString() : FechaI.Day.ToString());
        fFin = FechaF.Year.ToString() + (FechaF.Month < 10 ? "0" + FechaF.Month.ToString() : FechaF.Month.ToString()) + (FechaF.Day < 10?"0" + FechaF.Day.ToString() : FechaF.Day.ToString());
        fInicio = fInicio + "T000000";
        fFin = fFin + "T235959";

        conexion cn = new conexion();
        string url = "";
        url = "https://live.mzoneweb.net/api/v2/vehicles/" + IdGPSAsesor.ToString() + @"/trips/" + fInicio + @"/" + fFin + @".json";
        string respuesta = GetHttp(url);
        Root oObject = JsonConvert.DeserializeObject<Root>(respuesta);

        PBStatus.Minimum = 0;
        PBStatus.Maximum = oObject.Items.Count;

        int cont = 0;
        foreach (Item c in oObject.Items)
        {
          PBStatus.Value = cont;
          cont ++;
          int id = c.Id; /*(Insertar)*/
                         //Lugar de salida,fecha de llegada y hora de llegada

          DateTime horaSalida = (c.StartLocalTimestamp);

          int f1 = horaSalida.Day;
          int f2 = horaSalida.Month;
          int anio = horaSalida.Year;
          string dia, mes;
          if (f1 < 10)
          {
            dia = ("0" + f1).ToString();
          }
          else
          {
            dia = f1.ToString();
          }
          if (f2 < 10)
          {
            mes = ("0" + f2).ToString();
          }
          else
          {
            mes = f2.ToString();
          }

          string fechaSalida = anio + "-" + mes + "-" + dia;/*(Insertar)*/
          string lugarSalida = c.StartLocation;/*(Insertar)*/

          string horaFinalSalida = horaSalida.ToString("HH:mm:ss", CultureInfo.CurrentCulture);/*(Insertar)*/

          //Lugar de llegada,fecha de llegada y hora de llegada

          DateTime horaLlegada = (c.EndLocalTimestamp);

          int ff1 = horaLlegada.Day;
          int ff2 = horaLlegada.Month;
          int anioo = horaLlegada.Year;
          string diaa, mees;
          if (ff1 < 10)
          {
            diaa = ("0" + ff1).ToString();
          }
          else
          {
            diaa = f1.ToString();
          }
          if (ff2 < 10)
          {
            mees = ("0" + ff2).ToString();
          }
          else
          {
            mees = ff2.ToString();

          }

          string fechaLlegada = anioo + "-" + mees + "-" + diaa;/*(Insertar)*/
          string lugarLlegada = c.EndLocation;/*(Insertar)*/
          string horaFinalLlegada = horaLlegada.ToString("HH:mm:ss", CultureInfo.CurrentCulture);/*(Insertar)*/
          double distancia = c.Distance;

          string Conductor = NombreAsesor;
          string idVehiculo = IdGPSAsesor;

          List<double> auxiliar = new List<double>();

          auxiliar = c.StartPosition;

          float latitud, longitud;
          latitud = Convert.ToSingle(auxiliar[1]);
          longitud = Convert.ToSingle(auxiliar[0]);
          Console.WriteLine(insertar(Conductor, idVehiculo, id, fechaSalida, lugarSalida, horaFinalSalida, fechaLlegada, lugarLlegada, horaFinalLlegada, latitud, longitud));
          Console.Read();
        }
        PBStatus.Value = PBStatus.Maximum;
        return oObject.Items.Count;

      }
      catch (SystemException e) {
        return 0;
        MessageBox.Show("Error:" + e.Message);
      }
    }

    public string insertar(string Conductor, string idVehiculo, int id, string fechaSalida, string lugarSalida, string horaFinalSalida, string fechaLlegada, string lugarLlegada, string horaFinalLlegada, float latitud, float longitud
            )
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

    //private Boolean YaExiste(Int32 id2Check){
    //  //CONECTA A LA BASE DE DATOS
    //  conexion.conectar(true);
    //  conexion.cmd = new SqlCommand("SELECT Count(id) Existe FROM Registro_Viajes_Diarios WHERE id = " + id2Check, conexion.con);
    //  //EJECUTA LA CONSULTA
    //  conexion.dr = conexion.cmd.ExecuteReader();
    //  //RECORRE LA CONSULTA
    //  while (conexion.dr.Read()){
    //    if (Convert.ToInt16(conexion.dr["Existe"]) > 0)
    //      return true;
    //    else
    //      return false;
    //  }
    //  return false;
    //}

    public static string GetHttp(string url)
    {
      WebRequest myWebRequest = WebRequest.Create(url);
      myWebRequest.Credentials = new NetworkCredential("API_DIAMANTE", "D14M4NT32020");
      WebResponse oResponse = myWebRequest.GetResponse();

      StreamReader sr = new StreamReader(oResponse.GetResponseStream());
      return sr.ReadToEnd().Trim();
    }

  }

  public class Item
  {
    public int Id { get; set; }
    public DateTime StartLocalTimestamp { get; set; }
    public List<double> StartPosition { get; set; }
    public DateTime EndLocalTimestamp { get; set; }
    public List<double> EndPosition { get; set; }
    public double Distance { get; set; }
    public string StartLocation { get; set; }
    public string EndLocation { get; set; }
    public int NumberOfExceptions { get; set; }
    public int NumberOfDriverBehaviourExceptions { get; set; }
    public string idVehiculo { get; set; }
    public string Conductor { get; set; }
  }

  public class Visita
  {
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalResults { get; set; }
    public List<Item> Items { get; set; }
    public bool HasMoreResults { get; set; }
  }

  public class Root
  {
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalResults { get; set; }
    public List<Item> Items { get; set; }
    public bool HasMoreResults { get; set; }
  }
  }
