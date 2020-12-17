using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace poligramvc.Models
{
    public class Vinculos
    {
        public int id { get; set; }
        public int idHistorico { get; set; }
        public string nombreReferido { get; set; }
        public string alias { get; set; }
        public string relacion { get; set; }
        public string genero { get; set; }
        public string coorporacion { get; set; }
        public string municipio { get; set; }
        public string referencia { get; set; }
        public DateTime fecha { get; set; }
        public string paternoReferido { get; set; }
        public string maternoReferido { get; set; }
        public int areaRefiere { get; set; }
        public string vinculador { get; set; }

        public static List<Vinculos> getListaVinculosPoligrafia(string p_vinculo)
        {
            List<Vinculos> listaVinculo = new List<Vinculos>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Vinculos>(cnn, "sp_socioeconomicos_get_vinculado_vinculador", new { @nombre = p_vinculo }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaVinculo = (List<Vinculos>)returnDapper;
            return listaVinculo;
        }
    }
}