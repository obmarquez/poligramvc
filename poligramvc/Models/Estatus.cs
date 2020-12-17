using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace poligramvc.Models
{
    public class Estatus
    {
        public int idhistorico { get; set; }
        public int ide { get; set; }
        public int idhp { get; set; }
        public string evaluado { get; set; }
        public string idpol { get; set; }
        public string fecha_alta { get; set; }
        public int estatuspol { get; set; }
        public string fEntCus { get; set; }

        public static List<Estatus> getListaRegresados(int p_idH, int p_idE, int p_idHp)
        {
            List<Estatus> lista = new List<Estatus>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Estatus>(cnn, "sp_poligrafia_estatus_regresadis", new { @idH = p_idH, @idE = p_idE, @idHp = p_idHp }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Estatus>)returnDapper;
            return lista;
        }

        public static string updateEstatusPoligrafia(int p_idH, int p_idE, int p_idHp)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = p_idH });
                lparametros.Add(new parametros { parametro = "@ide", tipodato = "int", valorEntero = p_idE });
                lparametros.Add(new parametros { parametro = "@idhp", tipodato = "int", valorEntero = p_idHp });

                result = BDConn.EjecutarStoreProc_string("sp_poligrafia_actualiza_estatus_6", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }
    }
}