using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace poligramvc.Models
{
    public class Admision
    {
        public int id { get; set; }
        public int idh { get; set; }
        public int ide { get; set; }
        public int idhp { get; set; }
        public string evaluado { get; set; }
        public string evaluacion { get; set; }
        public string fechaEvaluacion { get; set; }
        public int estatus { get; set; }
        public string dx { get; set; }
        public string dxt { get; set; }
        public string ap { get; set; }
        public string dependencia { get; set; }
        public string serial { get; set; }
        public string cAdmisiones { get; set; }
        public string horario { get; set; }
        public string verificacion { get; set; }
        public int accion { get; set; }
        public string poligrafista { get; set; }
        public string supervisor { get; set; }
        public string director { get; set; }
        public string fecha { get; set; }

        public static List<Admision> getListaAdmision(string valor)
        {
            List<Admision> lista = new List<Admision>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Admision>(cnn, "sp_poligrafia_obtener_admisiones", new { @valor = valor }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Admision>)returnDapper;

            return lista;
        }

        public static List<Admision> getConcentradoAdmisiones(string fecha01, string fecha02)
        {
            List<Admision> lista = new List<Admision>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Admision>(cnn, "sp_poligrafia_obtener_concentrado_admisiones", new { @fecha01 = fecha01, @fecha02 = fecha02 }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Admision>)returnDapper;

            return lista;
        }

        public static List<Admision> getlaAdmision(int idh, int idhp)
        {
            List<Admision> laAdmision = new List<Admision>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Admision>(cnn, "sp_poligrafia_obtener_laadmision", new { @idh = idh, @idhp = idhp }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            laAdmision = (List<Admision>)returnDapper;

            return laAdmision;
        }

        public static string agregaActualizaVerificacion(int p_id, int p_idHistorico, string p_verificacion, string p_usuario, string p_horarioVerificacion, int p_accion)
        {
            string result = "ERROR";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@id", tipodato = "int", valorEntero = p_id });
                lparametros.Add(new parametros { parametro = "@idHistorico", tipodato = "int", valorEntero = p_idHistorico });
                lparametros.Add(new parametros { parametro = "@verificacion", tipodato = "string", valorCadena = p_verificacion });
                lparametros.Add(new parametros { parametro = "@usuario", tipodato = "string", valorCadena = p_usuario });
                lparametros.Add(new parametros { parametro = "@accion", tipodato = "int", valorEntero = p_accion });
                lparametros.Add(new parametros { parametro = "@horarioVerificacion", tipodato = "string", valorCadena = p_horarioVerificacion });

                result = BDConn.EjecutarStoreProc_string("sp_poligrafia_agregar_actualizar_verificacion", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        public static List<Admision> getAdmisionImprimir(int idVerificacion)
        {
            List<Admision> printAdminsion = new List<Admision>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Admision>(cnn, "sp_poligrafia_print_admision", new { @idVerificacion = idVerificacion }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            printAdminsion = (List<Admision>)returnDapper;
            return printAdminsion;
        }
    }
}