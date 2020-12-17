using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace poligramvc.Models
{
    public class RepAnt
    {
        public int idhistorico { get; set; }
        //----------- Datos principales
        public string observacionRegistral { get; set; }
        public string inhabilitacionEstatal { get; set; }
        public string constanciaInhabilitacionEstatal { get; set; }
        public string fechaConstanciaEstatal { get; set; }
        public string antecedenteEstatal { get; set; }
        public string constanciaAntecedenteEstatal { get; set; }
        public string fechaAntecedenteEstatal { get; set; }
        public string antecedenteFederal { get; set; }
        public string constanciaAntecedenteFederal { get; set; }
        public string fechaAntecedenteFederal { get; set; }
        public string observacionAdministrativo { get; set; }
        public string contexto { get; set; }
        public string publicacion { get; set; }
        public string observacionGeneral { get; set; }
        public string personaCargo { get; set; }
        public string cantidadPersonal { get; set; }
        public string cuip { get; set; }
        public string inhabilitacionFederal { get; set; }
        public string constanciaInhabilitacionFederal { get; set; }
        public string fechaConstanciaFederal { get; set; }
        public string cSuic { get; set; }
        public string cRnpsp { get; set; }
        public string cTelscan { get; set; }
        public string cEvaluacionAnterior { get; set; }
        public string cAnalisis { get; set; }
        //----------- Antecedente Registral
        public string tipoRegistro { get; set; }
        public string ncp { get; set; }
        public string procedencia { get; set; }
        //----------- Antecedentes Penales
        public string constanciaPenal { get; set; }
        public string lugarFechaPenal { get; set; }
        public string obsPenal { get; set; }
        public string fechaEvalPenal { get; set; }
        //----------- Aplicativo 2
        public bool aplicativoActual { get; set; }
        public string observaAplicativo { get; set; }
        //----------- Vehiculos
        public string ofiEnvVeh { get; set; }
        public string ofiRecVeh { get; set; }
        public string fenviado { get; set; }
        public string frecibido { get; set; }
        public string tipo { get; set; }
        public string marca { get; set; }
        public string placa { get; set; }
        public string clase { get; set; }
        public string serie { get; set; }
        public string modelo { get; set; }
        public string faltaVehicular { get; set; }
        public bool vehiculoencontrado { get; set; }
        public string observaVehiculo { get; set; }

        public static List<RepAnt> getRepAntPrimero(int idhistorico)
        {
            List<RepAnt> repAntPri = new List<RepAnt>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<RepAnt>(cnn, "sp_socioeconomicos_get_repAnt_1de5", new { @idhistorico = idhistorico }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            repAntPri = (List<RepAnt>)returnDapper;
            return repAntPri;
        }

        public static List<RepAnt> getRepAntSegundo(int idhistorico)
        {
            List<RepAnt> repAntSeg = new List<RepAnt>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<RepAnt>(cnn, "sp_socioeconomicos_get_repAnt_2de5", new { @idhistorico = idhistorico }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            repAntSeg = (List<RepAnt>)returnDapper;
            return repAntSeg;
        }

        public static List<RepAnt> getRepAntTercero(int idhistorico)
        {
            List<RepAnt> repAntTer = new List<RepAnt>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<RepAnt>(cnn, "sp_socioeconomicos_get_repAnt_3de5", new { @idHistorico = idhistorico }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            repAntTer = (List<RepAnt>)returnDapper;
            return repAntTer;
        }

        public static List<RepAnt> getRepAntCuarto(int idhistorico)
        {
            List<RepAnt> repAntCua = new List<RepAnt>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<RepAnt>(cnn, "sp_socioeconomicos_get_repAnt_4de5", new { @idhistorico = idhistorico }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            repAntCua = (List<RepAnt>)returnDapper;
            return repAntCua;
        }

        public static List<RepAnt> getRepAntQuinto(int idhistorico)
        {
            List<RepAnt> repAntCin = new List<RepAnt>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<RepAnt>(cnn, "sp_socioeconomicos_get_repAnt_5de5", new { @idhistorico = idhistorico }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            repAntCin = (List<RepAnt>)returnDapper;
            return repAntCin;
        }
    }
}