using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace poligramvc.Models
{
    public class Usuarios
    {
        public Int32 Id_Usuario { get; set; }
        public Int32 Id_Rol { get; set; }
        public String Nombre { get; set; }
        public String Ap_Paterno { get; set; }
        public String Ap_Materno { get; set; }
        public Int32 Activo { get; set; }
        public String Foto { get; set; }
        public String UsuarioSise { get; set; }

        [NotMapped]
        public String NombreCompleto { get; set; }
        [NotMapped]
        public string Desc_Rol { get; set; }
        

    }
}