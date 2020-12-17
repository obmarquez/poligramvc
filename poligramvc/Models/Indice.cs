using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace poligramvc.Models
{
    public class Indice
    {
        public int idhistorico { get; set; }
        public string curp_evaluado { get; set; }
        public string clave_ap { get; set; }
        public int nreporte { get; set; }
        public int datosgenerales { get; set; }
        public int carta { get; set; }
        //public int temas { get; set; } // por default se manda 1
        public int personales { get; set; }
        public int entrevista { get; set; }
        public int preguntas { get; set; }
        public int analisis { get; set; }
        public int graficos { get; set; }
        public int comentarios { get; set; }
        public int oficial { get; set; }
        public int otros_cantidad { get; set; }
        public int idevalpol { get; set; }
        //public int manifestacion { get; set; } //por default se manda 1
        public string identificacion { get; set; }
        public string otros_texto { get; set; }
        [NotMapped]
        public string evaluado { get; set; }
        [NotMapped]
        public string fecha { get; set; }
        [NotMapped]
        public string rfc { get; set; }
        [NotMapped]
        public string serial { get; set; }

        public static string agregaActualizaIndice(int idhistorico, int idevalpol, string clave_ap, int nreporte, int datosgenerales, int carta, int personales, int entrevista, int preguntas, int analisis, int graficos, int oficial, int otros_cantidad, string identificacion, string otros_texto, int accion)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idh", tipodato = "int", valorEntero = idhistorico });
                lparametros.Add(new parametros { parametro = "@idevalpol", tipodato = "int", valorEntero = idevalpol });
                lparametros.Add(new parametros { parametro = "@clave_ape", tipodato = "string", valorCadena = clave_ap });
                lparametros.Add(new parametros { parametro = "@nreporte", tipodato = "int", valorEntero = nreporte });
                lparametros.Add(new parametros { parametro = "@datosgenerales", tipodato = "int", valorEntero = datosgenerales });
                lparametros.Add(new parametros { parametro = "@carta", tipodato = "int", valorEntero = carta });
                lparametros.Add(new parametros { parametro = "@personales", tipodato = "int", valorEntero = personales });
                lparametros.Add(new parametros { parametro = "@entrevista", tipodato = "int", valorEntero = entrevista });
                lparametros.Add(new parametros { parametro = "@preguntas", tipodato = "int", valorEntero = preguntas });
                lparametros.Add(new parametros { parametro = "@analisis", tipodato = "int", valorEntero = analisis });
                lparametros.Add(new parametros { parametro = "@graficos", tipodato = "int", valorEntero = graficos });
                lparametros.Add(new parametros { parametro = "@identificacion", tipodato = "string", valorCadena = identificacion });
                lparametros.Add(new parametros { parametro = "@oficial", tipodato = "int", valorEntero = oficial });
                lparametros.Add(new parametros { parametro = "@otros_texto", tipodato = "string", valorCadena = otros_texto });
                lparametros.Add(new parametros { parametro = "@otros_cantidad", tipodato = "int", valorEntero = otros_cantidad });
                lparametros.Add(new parametros { parametro = "@accion", tipodato = "int", valorEntero = accion });
                //lparametros.Add(new parametros { parametro = "@temas", tipodato = "int", valorEntero = temas });
                //lparametros.Add(new parametros { parametro = "@comentarios", tipodato = "int", valorEntero = comentarios });
                //lparametros.Add(new parametros { parametro = "@manifestacion", tipodato = "int", valorEntero = manifestacion });


                result = BDConn.EjecutarStoreProc_string("sp_poligrafia_agrega_actualiza_expediente", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        public static List<Indice> obtenerIndice(int idh, int idhp)
        {
            List<Indice> datosIndice = new List<Indice>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Indice>(cnn, "sp_poligrafia_obtener_indice", new { @idh = idh, @idhp = idhp }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            datosIndice = (List<Indice>)returnDapper;
            return datosIndice;
        }

        public static List<Indice> datosRepIndice(int idRep)
        {
            List<Indice> datoRepInd = new List<Indice>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Indice>(cnn, "sp_poligrafia_get_rep_indice", new { @idRep = idRep }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            datoRepInd = (List<Indice>)returnDapper;
            return datoRepInd;
        }

    }
}