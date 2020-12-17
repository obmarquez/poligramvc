using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace poligramvc.Models
{
    public class Permisos
    {
        public int Id_Controladores { get; set; }
        public int Id_Controladores_Acciones { get; set; }
        public string Desc_Controlador { get; set; }
        public string Desc_Metodo { get; set; }


        public static List<Permisos> getLista(int pid_rol)
        {
            List<Permisos> lista = new List<Permisos>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Permisos>(cnn, "sp_admin_permisos_usuarios_get", new { Id_Rol = pid_rol }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Permisos>)returnDapper;
            return lista;
        }

        public static int getId_Accion(string pnom_accion, List<Permisos> plist_permisos)
        {
            int id = 0;
            foreach (var item in plist_permisos)
            {
                if (item.Desc_Metodo.ToString() == pnom_accion)
                {
                    id = item.Id_Controladores_Acciones;
                    break;
                }
            }
            return id;
        }
    }

    class Controladores
    {
        public int Id_Controlador { get; set; }
        public string Desc_Controlador { get; set; }
        public string Fecha_Registro_String { get; set; }
        public static List<Controladores> getLista()
        {
            List<Controladores> lista = new List<Controladores>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Controladores>(cnn, "sp_controladores_list", null, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Controladores>)returnDapper;
            return lista;
        }

    }

    class Controladores_Accciones
    {
        public int Id_Controladores_Acciones { get; set; }
        public string Desc_Metodo { get; set; }
        public string Descripcion { get; set; }
        public string Fecha_Registro_String { get; set; }
        public static List<Controladores_Accciones> getLista(int pid_controlador)
        {
            List<Controladores_Accciones> lista = new List<Controladores_Accciones>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Controladores_Accciones>(cnn, "sp_controladores_acciones_list", new { Id_Controlador = pid_controlador }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Controladores_Accciones>)returnDapper;
            return lista;
        }

    }
}