using Antlr.Runtime.Misc;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace poligramvc.Models
{
    public class Seguimiento
    {
        public int id { get; set; }
        public string fecha { get; set; }
        //fecha   smalldatetime
        public string apEvaluador { get; set; }
        public string apSupervisor { get; set; }
        public string periodo { get; set; }
        public string fortaleza { get; set; }
        public string oportunidad { get; set; }
        public string observaciones { get; set; }
        public string Evaluador { get; set; }

        public static List<Seguimiento> getSeguimientoId(int id)
        {
            List<Seguimiento> lista = new List<Seguimiento>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Seguimiento>(cnn, "sp_poligrafia_obtiene_seguimiento_contenido", new { @id = id }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Seguimiento>)returnDapper;
            return lista;
        }

    }
}