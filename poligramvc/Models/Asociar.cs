using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace poligramvc.Models
{
    public class Asociar
    {
        public int idh { get; set; }
        public string idevaluador { get; set; }
        public int depa { get; set; }
        public int nCubiculo { get; set; }
        public bool reex { get; set; }

        public static string asociar_poligrafista(int idh, string idEvaluador, int depa, bool reex, int idevalpol, string p_idSupervisor, int p_nCubiculo)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idHistorico", tipodato = "int", valorEntero = idh });
                lparametros.Add(new parametros { parametro = "@idEvaluador", tipodato = "string", valorCadena = idEvaluador });
                lparametros.Add(new parametros { parametro = "@depa", tipodato = "int", valorEntero = depa });
                lparametros.Add(new parametros { parametro = "@reex", tipodato = "boleano", valorBoleano = reex });
                lparametros.Add(new parametros { parametro = "@idevalpol", tipodato = "int", valorEntero = idevalpol });
                lparametros.Add(new parametros { parametro = "@idSupervisor", tipodato = "string", valorCadena = p_idSupervisor });
                lparametros.Add(new parametros { parametro = "@nCubiculo", tipodato = "int", valorEntero = p_nCubiculo });

                result = BDConn.EjecutarStoreProc_string("sp_general_actualiza_asocia", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }
    }
}