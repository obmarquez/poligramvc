using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace poligramvc.Models
{
    public class Red
    {
        public Int32 id { get; set; }
        public Int32 idHistorico { get; set; }
        public String nombreReferido { get; set; }
        public String alias { get; set; }
        public String relacion { get; set; }
        public String genero { get; set; }
        public String coorporacion { get; set; }
        public String municipio { get; set; }
        public String referencia { get; set; }
        public String usuario { get; set; }
        public String paternoReferido { get; set; }
        public String maternoReferido { get; set; }
        public Int32 accion { get; set; }

        public static string agregarRed(Int32 id, Int32 idHistorico, String nombreReferido, String alias, String relacion, String genero, String coorporacion, String municipio, String referencia, String usuario, Int32 accion, String paternoReferido, String maternoReferido)
        {
            string result = "ERROR";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@id", tipodato = "int", valorEntero = id });
                lparametros.Add(new parametros { parametro = "@idHistorico", tipodato = "int", valorEntero = idHistorico });
                lparametros.Add(new parametros { parametro = "@nombreReferido", tipodato = "string", valorCadena = nombreReferido });
                lparametros.Add(new parametros { parametro = "@alias", tipodato = "string", valorCadena = alias });
                lparametros.Add(new parametros { parametro = "@relacion", tipodato = "string", valorCadena = relacion });
                lparametros.Add(new parametros { parametro = "@genero", tipodato = "string", valorCadena = genero });
                lparametros.Add(new parametros { parametro = "@coorporacion", tipodato = "string", valorCadena = coorporacion });
                lparametros.Add(new parametros { parametro = "@municipio", tipodato = "string", valorCadena = municipio });
                lparametros.Add(new parametros { parametro = "@referencia", tipodato = "string", valorCadena = referencia });
                lparametros.Add(new parametros { parametro = "@usuario", tipodato = "string", valorCadena = usuario });
                lparametros.Add(new parametros { parametro = "@accion", tipodato = "int", valorEntero = accion });
                lparametros.Add(new parametros { parametro = "@paternoReferido", tipodato = "string", valorCadena = paternoReferido });
                lparametros.Add(new parametros { parametro = "@maternoReferido", tipodato = "string", valorCadena = maternoReferido });

                result = BDConn.EjecutarStoreProc_string("sp_general_agrega_actualiza_red", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }
    }
}