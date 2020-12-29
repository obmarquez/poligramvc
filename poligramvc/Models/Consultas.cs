using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace poligramvc.Models
{
    public class Consultas
    {
        public int id { get; set; }
        public Int32 IdH { get; set; }
        public Int32 IdE { get; set; }
        public String evaluado { get; set; }
        public String evaluacion { get; set; }
        public String fecha { get; set; }
        public String fechapol { get; set; }
        public String dependencia { get; set; }
        public Int32 idhp { get; set; }
        public Int32 Hay { get; set; }
        public String UsuarioSise { get; set; }
        public String Nombre { get; set; }
        public int indice { get; set; }
        public String cve_mun { get; set; }
        public String mun_desc { get; set; }
        public string departamento { get; set; }

        public Int32 vinculo { get; set; }
        public string puesto { get; set; }
        public string comision { get; set; }
        public string cAdscripcion { get; set; }
        public Int32 idO { get; set; }
        public string observacionpublica { get; set; }
        public string recomendacion { get; set; }
        public int TotalE { get; set; }
        public string name { get; set; }
        public int Cantidad { get; set; }
        public string hayVinculo { get; set; }

        //Consulta detalle
        public string idpol { get; set; }
        public string est { get; set; }
        public string folio { get; set; }
        public string Dx { get; set; }
        public string cDxt { get; set; }
        //public string diAlt { get; set; }
        //public string diConf { get; set; }      
        //public string diDel { get; set; }
        //public string diIle { get; set; }
        //public string diIli { get; set; }
        //public string diOrg { get; set; }
        //public string admAlt { get; set; }
        //public string admConf { get; set; }
        //public string amdDel { get; set; }      
        //public string amdIle { get; set; }
        //public string admIli { get; set; }
        //public string amdOrg { get; set; }
        //public string anaGlo { get; set; }
        //------------------------------------------------- Variables Totalizado
        public string uno { get; set; }
        public string dos { get; set; }
        public string tres { get; set; }
        public string cuatro { get; set; }
        public string cinco { get; set; }
        public string seis { get; set; }
        public string siete { get; set; }
        public string ocho { get; set; }
        public string nueve { get; set; }
        public string diez { get; set; }
        public string once { get; set; }
        public string doce { get; set; }
        public string trece { get; set; }
        public string catorce { get; set; }
        public string quince { get; set; }
        public string dieciseis { get; set; }
        public string diecisiete { get; set; }
        public string dieciocho { get; set; }
        public string diecinueve { get; set; }
        public string veinte { get; set; }
        public string veintiuno { get; set; }
        public string veintidos { get; set; }
        public string veintitres { get; set; }
        public string veinticuatro { get; set; }
        public string veinticinco { get; set; }
        public string veintiseis { get; set; }
        public int Id_Usuario { get; set; }
        public string serial { get; set; }
        public int estatus { get; set; }
        public string fEntCus { get; set; }
        public string diferenciada { get; set; }
        //----------------------------------------------------------------------

        public static List<Consultas> getListaAsociarFecha(string fecha, int area)
        {
            List<Consultas> listaAsociar = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_listado_asociar_evalauados", new { @fecha = fecha, @area= area }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaAsociar = (List<Consultas>)returnDapper;

            return listaAsociar;
        }

        public static List<Consultas> get_investigador_area(int area)
        {
            List<Consultas> losevaluadores = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_evaluador_area_lista", new { @area = 6 }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            losevaluadores = (List<Consultas>)returnDapper;

            return losevaluadores;
        }

        public static List<Consultas> getListaEvaluadosPendiente(string poligrafista)
        {
            List<Consultas> lista = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_listado_pendientes", new { @idPoligrafista = poligrafista }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Consultas>)returnDapper;

            return lista;
        }

        public static List<Consultas> getListaEvaluadosSupervisor(string evaluado)
        {
            List<Consultas> lista = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_listado_supervisor", new { @nombre = evaluado }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Consultas>)returnDapper;

            return lista;
        }

        public static List<Consultas> getSupervisores(int rol)
        {
            List<Consultas> losSupervisores = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_supervisor_area_lista", new { @area = rol }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);

            losSupervisores = (List<Consultas>)returnDapper;

            return losSupervisores;
        }

        public static List<Consultas> getListaObservacionDiariaxEvaluado(int sIdH, int area)
        {
            List<Consultas> listaObservacionDiariaxEvaluado = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_lista_observacion_x_evaluado", new { @idH = sIdH, @area = area }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaObservacionDiariaxEvaluado = (List<Consultas>)returnDapper;

            return listaObservacionDiariaxEvaluado;
        }

        public static List<Consultas> getTotalEvaluadoTipoPOL(int p_Opcion, string p_Usuario)
        {
            List<Consultas> lista = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_total_evauluaciones", new { @opcion = p_Opcion, @usuario = p_Usuario }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Consultas>)returnDapper;
            return lista;
        }

        public static List<Consultas> ListStatus(string pEvaluado, int pidrol)
        {
            List<Consultas> listaEstatus = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_evaluadorStatusPendientes", new { @idpol = pEvaluado, @idrol = pidrol }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaEstatus = (List<Consultas>)returnDapper;
            return listaEstatus;
        }

        public static List<Consultas> getListaObservacionDiaria(int accion, string fecha, int idObservacion)
        {
            List<Consultas> listaObservacionDiaria = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_general_lista_observaciones", new { @accion = accion, @fecha = fecha, @idObservacion = idObservacion }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaObservacionDiaria = (List<Consultas>)returnDapper;

            return listaObservacionDiaria;
        }

        public static List<Consultas> getMunicipiosEstado()
        {
            List<Consultas> listaMunicipios = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDatpper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_Poligrafia_ObtieneMunicipios_Estado", commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaMunicipios = (List<Consultas>)returnDatpper;

            return listaMunicipios;
        }

        public static List<Consultas> getTotalizadoDetalle(string fecha01, string fecha02, int opcion)
        {
            List<Consultas> listaTotalDetalle = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_Consultas", new { @fecha01 = fecha01, @fecha02 = fecha02, opcion = opcion }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaTotalDetalle = (List<Consultas>)returnDapper;
            return listaTotalDetalle;
        }

        public static List<Consultas> getDetalleTotalizado(int opcion, string idpol, string fecha01, string fecha02, int estatus)
        {
            List<Consultas> listaDetallada = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_Consultas", new { @opcion = opcion, @idpol = idpol, @fecha01 = fecha01, @fecha02 = fecha02, @estatus = estatus }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaDetallada = (List<Consultas>)returnDapper;
            return listaDetallada;
        }

        public static List<Consultas> getTotalizado(string fecha01, string fecha02, string opcionTotalizado)
        {
            List<Consultas> listaTotal = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_conteoTotalizado", new { @f1 = fecha01, @f2 = fecha02, @evaluacion = opcionTotalizado }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaTotal = (List<Consultas>)returnDapper;
            return listaTotal;
        }

        public static List<Consultas> getGraResPol(string idpol, string fecha1, string fecha2)
        {
            List<Consultas> lista = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_evaluadorConcentradoResultados", new { @idpol = idpol, @fecha1 = fecha1, @fecha2 = fecha2 }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Consultas>)returnDapper;
            return lista;
        }

        public static List<Consultas> getSeguimientoCartaCompromiso(string UsuarioSise, int opcion)
        {
            List<Consultas> lista = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_poligrafia_obtener_seguimiento_carta", new { @UsuarioSise = UsuarioSise, @opcion = opcion }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Consultas>)returnDapper;
            return lista;
        }

        public static List<Consultas> getEntradaDiaria(string fecha)
        {
            List<Consultas> listaEntradaDiaria = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_entrada_diaria", new { @fecha = fecha }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaEntradaDiaria = (List<Consultas>)returnDapper;

            return listaEntradaDiaria;
        }

        public static List<Consultas> getAsociarNombre(string valor, int area)
        {
            List<Consultas> listaNombre = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_asociar_por_nombre", new { @area = area, @nombre = valor }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaNombre = (List<Consultas>)returnDapper;

            return listaNombre;
        }
    }
}