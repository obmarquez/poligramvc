using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace poligramvc.Models
{
    public class EvaluacionPol
    {
        //idEvaluacion int campo autoincrmentalbe
        public int idhistorico { get; set; }
        //curp_evaluado   nvarchar(20) campo que se obtiene a partir del idhistorico
        public string cLaboral { get; set; }
        public string cAdmisiones { get; set; }
        public string cObservaciones { get; set; }
        public string cAnalisis { get; set; }
        //fecha_evaluacion smalldatetime fecha automatica de primer registro, fecha de sql server
        public string clave_investigador { get; set; }
        public string cDiagnostico { get; set; }    //Dx
        //fecha_modifica smalldatetime fecha automatica de cualquier cambio
        //public string clave_investigador_mod { get; set; }
        public string clave_revisor { get; set; }
        public string cNota { get; set; }
        public string cCalidad { get; set; }
        public string cEscolaridad { get; set; }
        public DateTime fEvalpol { get; set; }
        //cMotivo varchar(15) ya no se graba esta informacion
        //cPuesto varchar(150) ya no se graba esta informacion
        public string cCarrera { get; set; }
        public int idevalpol { get; set; }
        public string cSintesis { get; set; }
        public string cDxt { get; set; }
        //public bool bNdiilegales { get; set; }
        //public bool bNdiconfidencial { get; set; }
        //public bool bNdiilicitos { get; set; }
        //public bool bNdiorganizada { get; set; }
        //public bool bNdiReferencia { get; set; }
        //public bool bNdidelitos { get; set; }
        public bool bDiilegales { get; set; }
        public bool bDiconfidencia { get; set; }
        public bool bDiilicitos { get; set; }
        public bool bDiorganizada { get; set; }
        public bool bAdmilegales { get; set; }
        public bool bAdmconfidencial { get; set; }
        public bool bAdmilicitos { get; set; }
        public bool bAdmorganizada { get; set; }
        public bool bCardiacos { get; set; }
        public bool bEpilepsia { get; set; }
        public bool bEmbarazo { get; set; }
        public bool bOtros { get; set; }
        public string cOtros { get; set; }
        public bool bEmpriricosimplificado { get; set; }
        public bool bTrespuntos { get; set; }
        //public bool bSimplificado { get; set; }
        public bool bMgqtv1 { get; set; }
        //public bool bMgqtv2 { get; set; }
        public bool bDlst { get; set; }
        public bool bDidelitos { get; set; }
        public bool bAdmdelitos { get; set; }
        public bool bNdiporinformacion { get; set; }
        public bool bCuartografico { get; set; }
        public bool bAnalisisglobal { get; set; }
        public string cObservapoligrafia { get; set; }
        public bool bDialterar { get; set; }
        public bool bAdmalterar { get; set; }
        public bool bOss3 { get; set; }
        public bool bSietepuntos { get; set; }
        [NotMapped]
        public byte[] Picture { get; set; }
        [NotMapped]
        public int accion { get; set; }

        public static List<EvaluacionPol> getImgEva(int pIdH)
        {
            List<EvaluacionPol> img = new List<EvaluacionPol>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<EvaluacionPol>(cnn, "sp_general_obtieneImagenEvaluado", new { @idhistorico = pIdH }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            img = (List<EvaluacionPol>)returnDapper;
            return img;
        }

        public static string agregaActualizaEvaluacionPoligrafica(int p_idh, DateTime p_fEvalpol, string p_cEscolaridad, string p_cCarrera, string p_cLaboral, string p_cAdmisiones, string p_cObservaciones, string p_cNota, string p_cSintesis, string p_cAnalisis, string p_cObservapoligrafia, string p_cDiagnostico, string p_cDxt, string p_cCalidad, bool p_bNdiporinformacion, bool p_bDiilegales, bool p_bDiconfidencia, bool p_bDiilicitos, bool p_bDiorganizada, bool p_bDidelitos, bool p_bDiAlterar, bool p_bAdmilegales, bool p_bAdmconfidencial, bool p_bAdmilicitos, bool p_bAdmorganizada, bool p_bAdmdelitos, bool p_bAdmAlterar, bool p_bCardiacos, bool p_bEpilepsia, bool p_bEmbarazo, bool p_bOtros, bool p_bEmpriricosimplificado, bool p_bCuartografico, bool p_bAnalisisglobal, bool p_bOss3, bool p_bMgqtv1, bool p_bTrespuntos, bool p_bSietepuntos, bool p_bDlst, int p_idevalpol, string p_clave_investigador, string p_clave_revisor, int p_accion)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idh", tipodato = "int", valorEntero = p_idh });
                lparametros.Add(new parametros { parametro = "@fEvalpol", tipodato = "date", valorFecha = p_fEvalpol });
                lparametros.Add(new parametros { parametro = "@cEscolaridad", tipodato = "string", valorCadena = p_cEscolaridad });
                lparametros.Add(new parametros { parametro = "@cCarrera", tipodato = "string", valorCadena = p_cCarrera });
                lparametros.Add(new parametros { parametro = "@cLaboral", tipodato = "string", valorCadena = p_cLaboral });
                lparametros.Add(new parametros { parametro = "@cAdmisiones", tipodato = "string", valorCadena = p_cAdmisiones });
                lparametros.Add(new parametros { parametro = "@cObservaciones", tipodato = "string", valorCadena = p_cObservaciones });
                lparametros.Add(new parametros { parametro = "@cNota", tipodato = "string", valorCadena = p_cNota });
                lparametros.Add(new parametros { parametro = "@cSintesis", tipodato = "string", valorCadena = p_cSintesis });
                lparametros.Add(new parametros { parametro = "@cAnalisis", tipodato = "string", valorCadena = p_cAnalisis });
                lparametros.Add(new parametros { parametro = "@cObservapoligrafia", tipodato = "string", valorCadena = p_cObservapoligrafia });
                lparametros.Add(new parametros { parametro = "@cDiagnostico", tipodato = "string", valorCadena = p_cDiagnostico });
                lparametros.Add(new parametros { parametro = "@cDxt", tipodato = "string", valorCadena = p_cDxt });
                lparametros.Add(new parametros { parametro = "@cCalidad", tipodato = "string", valorCadena = p_cCalidad });
                lparametros.Add(new parametros { parametro = "@bNdiporinformacion", tipodato = "boleano", valorBoleano = p_bNdiporinformacion });
                lparametros.Add(new parametros { parametro = "@bDiilegales", tipodato = "boleano", valorBoleano = p_bDiilegales });
                lparametros.Add(new parametros { parametro = "@bDiconfidencia", tipodato = "boleano", valorBoleano = p_bDiconfidencia });
                lparametros.Add(new parametros { parametro = "@bDiilicitos", tipodato = "boleano", valorBoleano = p_bDiilicitos });
                lparametros.Add(new parametros { parametro = "@bDiorganizada", tipodato = "boleano", valorBoleano = p_bDiorganizada });
                lparametros.Add(new parametros { parametro = "@bDidelitos", tipodato = "boleano", valorBoleano = p_bDidelitos });
                lparametros.Add(new parametros { parametro = "@bDiAlterar", tipodato = "boleano", valorBoleano = p_bDiAlterar });
                lparametros.Add(new parametros { parametro = "@bAdmilegales", tipodato = "boleano", valorBoleano = p_bAdmilegales });
                lparametros.Add(new parametros { parametro = "@bAdmconfidencial", tipodato = "boleano", valorBoleano = p_bAdmconfidencial });
                lparametros.Add(new parametros { parametro = "@bAdmilicitos", tipodato = "boleano", valorBoleano = p_bAdmilicitos });
                lparametros.Add(new parametros { parametro = "@bAdmorganizada", tipodato = "boleano", valorBoleano = p_bAdmorganizada });
                lparametros.Add(new parametros { parametro = "@bAdmdelitos", tipodato = "boleano", valorBoleano = p_bAdmdelitos });
                lparametros.Add(new parametros { parametro = "@bAdmAlterar", tipodato = "boleano", valorBoleano = p_bAdmAlterar });
                lparametros.Add(new parametros { parametro = "@bCardiacos", tipodato = "boleano", valorBoleano = p_bCardiacos });
                lparametros.Add(new parametros { parametro = "@bEpilepsia", tipodato = "boleano", valorBoleano = p_bEpilepsia });
                lparametros.Add(new parametros { parametro = "@bEmbarazo", tipodato = "boleano", valorBoleano = p_bEmbarazo });
                lparametros.Add(new parametros { parametro = "@bOtros", tipodato = "boleano", valorBoleano = p_bOtros });
                lparametros.Add(new parametros { parametro = "@bEmpriricosimplificado", tipodato = "boleano", valorBoleano = p_bEmpriricosimplificado });
                lparametros.Add(new parametros { parametro = "@bCuartografico", tipodato = "boleano", valorBoleano = p_bCuartografico });
                lparametros.Add(new parametros { parametro = "@bAnalisisglobal", tipodato = "boleano", valorBoleano = p_bAnalisisglobal });
                lparametros.Add(new parametros { parametro = "@bOss3", tipodato = "boleano", valorBoleano = p_bOss3 });
                lparametros.Add(new parametros { parametro = "@bMgqtv1", tipodato = "boleano", valorBoleano = p_bMgqtv1 });
                lparametros.Add(new parametros { parametro = "@bTrespuntos", tipodato = "boleano", valorBoleano = p_bTrespuntos });
                lparametros.Add(new parametros { parametro = "@bSietepuntos", tipodato = "boleano", valorBoleano = p_bSietepuntos });
                lparametros.Add(new parametros { parametro = "@bDlst", tipodato = "boleano", valorBoleano = p_bDlst });
                lparametros.Add(new parametros { parametro = "@idevalpol", tipodato = "int", valorEntero = p_idevalpol });
                lparametros.Add(new parametros { parametro = "@clave_investigador", tipodato = "string", valorCadena = p_clave_investigador });
                lparametros.Add(new parametros { parametro = "@clave_revisor", tipodato = "string", valorCadena = p_clave_revisor });
                lparametros.Add(new parametros { parametro = "@accion", tipodato = "int", valorEntero = p_accion });

                result = BDConn.EjecutarStoreProc_string("sp_poligrafia_agrega_actualiza_investigacion", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }
            return result;
        }

        public static List<EvaluacionPol> obtenerEvaluacion(int pIdh, int pIdhp)
        {
            List<EvaluacionPol> laEvaluacion = new List<EvaluacionPol>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<EvaluacionPol>(cnn, "sp_poligrafia_obtener_expediente_poligrafico", new { @idh = pIdh, @idevalpol = pIdhp }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            laEvaluacion = (List<EvaluacionPol>)returnDapper;
            return laEvaluacion;
        }
    }
}