using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace poligramvc.Models
{
    public class Poa
    {
        public string sexo { get; set; }
        public int totalXGenero { get; set; }
        public string cevaluacion { get; set; }
        public int totalEvaluacion { get; set; }
        public string desc_dependencia { get; set; }
        public int totalDependencia { get; set; }

        public static List<Poa> getPoa(string fecha01, string fecha02, int opcion)
        {
            List<Poa> listaPoaSexo = new List<Poa>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Poa>(cnn, "sp_poligrafia_obtener_poa_fechapoligrafia", new { @fecha01 = fecha01, @fecha02 = fecha02, @opcion = opcion }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaPoaSexo = (List<Poa>)returnDapper;

            return listaPoaSexo;
        }
    }
}