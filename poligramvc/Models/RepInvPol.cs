using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace poligramvc.Models
{
    public class RepInvPol
    {
		public int idhistorico { get; set; }
		public int idevalpol { get; set; }
		public string evaluado { get; set; }
		public string edad { get; set; }
		public string sexo { get; set; }
		public string obsest { get; set; } 
		public string rfc { get; set; }
		public string curp { get; set; }
		public string cevaluacion { get; set; }
		public string fechaAlta { get; set; }
		public string dependencia { get; set; }
		public string subDependencia { get; set; }
		public string puesto { get; set; }
		public string categoriaPuesto { get; set; }
		public string funcion { get; set; }
		public string funcionInstitucional { get; set; }
		public string cAdscripcion { get; set; }
		public string comision { get; set; }
		public string fEvalpol { get; set; }
		public string cLaboral { get; set; }
		public string cAdmisiones { get; set; }
		public string cObservaciones { get; set; }
		public string cNota { get; set; }
		public string cSintesis { get; set; }
		public string cAnalisis { get; set; }
		public string evaluo { get; set; }
		public string supervisor { get; set; }
		public string dx { get; set; }
		public byte[] foto { get; set; }
		public string codigoevaluado { get; set; }
		public string domicilio { get; set; }

		public static List<RepInvPol> getRepInvPol(int idh, int idhp)
        {
			List<RepInvPol> repPol = new List<RepInvPol>();
			IDbConnection cnn = BDConn.AbreConexion();
			var returnDapper = Dapper.SqlMapper.Query<RepInvPol>(cnn, "sp_poligrafia_get_rep_investigacion", new { @idh = idh, @idhp = idhp }, commandType: CommandType.StoredProcedure);
			BDConn.CierraConexion(cnn);
			repPol = (List<RepInvPol>)returnDapper;
			return repPol;
        }

	}
}