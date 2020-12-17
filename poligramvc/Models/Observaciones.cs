using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace poligramvc.Models
{
    public class Observaciones
    {
        public Int32 id { get; set; }
        public String curp { get; set; }
        public String idusuario { get; set; }
        public DateTime fecha { get; set; }
        public String observacionpublica { get; set; }
        public String user_responde { get; set; }
        public String recomendacion { get; set; }
        [NotMapped]
        public Int32 tt { get; set; }
        [NotMapped]
        public Int32 sIdH { get; set; }

        public static string responderObservacion(Int32 id, String user_responde, String recomendacion, Int32 area)
        {
            string result = "ERROR";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@id", tipodato = "int", valorEntero = id });
                lparametros.Add(new parametros { parametro = "@user_responde", tipodato = "string", valorCadena = user_responde });
                lparametros.Add(new parametros { parametro = "@recomendacion", tipodato = "string", valorCadena = recomendacion });
                lparametros.Add(new parametros { parametro = "@area", tipodato = "int", valorEntero = area });

                result = BDConn.EjecutarStoreProc_string("sp_general_actualiza_respuesta_obsevacion", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        public static string agregarObservacion(String idusuario, Int32 idHistorico, String observacionpublica)
        {
            string result = "ERROR";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idusuario", tipodato = "string", valorCadena = idusuario });
                lparametros.Add(new parametros { parametro = "@idHistorico", tipodato = "int", valorEntero = idHistorico });
                lparametros.Add(new parametros { parametro = "@observacionpublica", tipodato = "string", valorCadena = observacionpublica });

                result = BDConn.EjecutarStoreProc_string("sp_general_inserta_observacion", lparametros, true, "@Result");  //Si lo hizo bien regresará un "Ok"
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }
    }
}