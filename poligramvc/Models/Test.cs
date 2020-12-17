using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace poligramvc.Models
{
    public class Test
    {
		public string usuario { get; set; }	//quien evalua
		public string evaluado { get; set; } //a quien evalua
		public int caltec01 { get; set; }
		public int caltec02 { get; set; }
		public int caltec03 { get; set; }
		public int caltec04 { get; set; }
		public int caltec05 { get; set; }
		public int caltec06 { get; set; }
		public int caltec07 { get; set; }
		public string observatec01 { get; set; }
		public string observatec02 { get; set; }
		public string observatec03 { get; set; }
		public string observatec04 { get; set; }
		public string observatec05 { get; set; }
		public string observatec06 { get; set; }
		public string observatec07 { get; set; }
		public int calcon01 { get; set; }
		public int calcon02 { get; set; }
		public int calcon03 { get; set; }
		public string observacon01 { get; set; }
		public string observacon02 { get; set; }
		public string observacon03 { get; set; }
		public int accion { get; set; }
		public DateTime fecha { get; set; }
		public int id { get; set; }
		[NotMapped]
		public string fechaTest { get; set; }
		
		public static string agregarDesempeno(string p_usuario, int p_caltec01, int p_caltec02, int p_caltec03, int p_caltec04, int p_caltec05, int p_caltec06, int p_caltec07, string p_observatec01, string p_observatec02, string p_observatec03, string p_observatec04, string p_observatec05, string p_observatec06, string p_observatec07, int p_calcon01, int p_calcon02, int p_calcon03, string p_observacon01, string p_observacon02, string p_observacon03, string p_evaluado, int p_accion)
        {
			string result = "ERROR";
            try
            {
				List<parametros> lparametros = new List<parametros>();
				lparametros.Add(new parametros { parametro = "@usuario", tipodato = "string", valorCadena = p_usuario });
				lparametros.Add(new parametros { parametro = "@caltec01", tipodato = "int", valorEntero = p_caltec01 });
				lparametros.Add(new parametros { parametro = "@caltec02", tipodato = "int", valorEntero = p_caltec02 });
				lparametros.Add(new parametros { parametro = "@caltec03", tipodato = "int", valorEntero = p_caltec03 });
				lparametros.Add(new parametros { parametro = "@caltec04", tipodato = "int", valorEntero = p_caltec04 });
				lparametros.Add(new parametros { parametro = "@caltec05", tipodato = "int", valorEntero = p_caltec05 });
				lparametros.Add(new parametros { parametro = "@caltec06", tipodato = "int", valorEntero = p_caltec06 });
				lparametros.Add(new parametros { parametro = "@caltec07", tipodato = "int", valorEntero = p_caltec07 });
				lparametros.Add(new parametros { parametro = "@observatec01", tipodato = "string", valorCadena = p_observatec01 });
				lparametros.Add(new parametros { parametro = "@observatec02", tipodato = "string", valorCadena = p_observatec02 });
				lparametros.Add(new parametros { parametro = "@observatec03", tipodato = "string", valorCadena = p_observatec03 });
				lparametros.Add(new parametros { parametro = "@observatec04", tipodato = "string", valorCadena = p_observatec04 });
				lparametros.Add(new parametros { parametro = "@observatec05", tipodato = "string", valorCadena = p_observatec05 });
				lparametros.Add(new parametros { parametro = "@observatec06", tipodato = "string", valorCadena = p_observatec06 });
				lparametros.Add(new parametros { parametro = "@observatec07", tipodato = "string", valorCadena = p_observatec07 });
				lparametros.Add(new parametros { parametro = "@calcon01", tipodato = "int", valorEntero = p_calcon01 });
				lparametros.Add(new parametros { parametro = "@calcon02", tipodato = "int", valorEntero = p_calcon02 });
				lparametros.Add(new parametros { parametro = "@calcon03", tipodato = "int", valorEntero = p_calcon03 });
				lparametros.Add(new parametros { parametro = "@observacon01", tipodato = "string", valorCadena = p_observacon01 });
				lparametros.Add(new parametros { parametro = "@observacon02", tipodato = "string", valorCadena = p_observacon02});
				lparametros.Add(new parametros { parametro = "@observacon03", tipodato = "string", valorCadena = p_observacon03 });
				lparametros.Add(new parametros { parametro = "@evaluado", tipodato = "string", valorCadena = p_evaluado });
				lparametros.Add(new parametros { parametro = "@accion", tipodato = "int", valorEntero = p_accion });

				result = BDConn.EjecutarStoreProc_string("sp_poligrafia_agrega_evaluacion_desempeño", lparametros, true, "@Result");
			}
			catch
			{
				result = "ERROR";
			}
			return result;
		}

		public static List<Test> getEvalPoligrafista(string fecha01, string fecha02)
        {
			List<Test> evalPol = new List<Test>();

			IDbConnection cnn = BDConn.AbreConexion();
			var returnDapper = Dapper.SqlMapper.Query<Test>(cnn, "sp_poligrafia_get_test_poligrafista", new { @fecha1 = fecha01, @fecha2 = fecha02 }, commandType: CommandType.StoredProcedure);
			BDConn.CierraConexion(cnn);

			evalPol = (List<Test>)returnDapper;

			return evalPol;
        }

		public static List<Test> getEvalSupervisor(string fecha01, string fecha02)
        {
			List<Test> evalSup = new List<Test>();

			IDbConnection cnn = BDConn.AbreConexion();
			var returnDapper = Dapper.SqlMapper.Query<Test>(cnn, "sp_poligrafia_get_test_supervisor", new { @fecha1 = fecha01, @fecha2 = fecha02 }, commandType: CommandType.StoredProcedure);
			BDConn.CierraConexion(cnn);

			evalSup = (List<Test>)returnDapper;

			return evalSup;
		}

		public static List<Test> getEvaluacionDesempenoImprimir(int id, int opcion)
        {
			List<Test> evaluacionDesempeno = new List<Test>();
			IDbConnection cnn = BDConn.AbreConexion();
			var returnDapper = Dapper.SqlMapper.Query<Test>(cnn, "sp_poligrafia_evaluaciones_desempeno", new { @id = id, @opcion = opcion }, commandType: CommandType.StoredProcedure);
			BDConn.CierraConexion(cnn);

			evaluacionDesempeno = (List<Test>)returnDapper;

			return evaluacionDesempeno;
        }

	}
}