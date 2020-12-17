using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace poligramvc.Models
{
    public class Logeo
    {
        [Required(ErrorMessage = "Ingrese su usuario")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public static Usuarios IsValid(string _username, string _password)
        {
            Usuarios usuario = new Usuarios();
            try
            {
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("username", _username);
                parametros.Add("contrasena", GenerateSHA1Hash(_password));

                DataTable dt = BDConn.ConsultarBD("sp_admin_logeo_validar", parametros);
                Boolean bandera = false;

                foreach (DataRow dr in dt.Rows)
                {
                    bandera = true;
                    usuario.Activo = 1;
                    usuario.Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]);
                    usuario.Id_Rol = Convert.ToInt32(dr["Id_Rol"]);
                    usuario.NombreCompleto = dr["NombreCompleto"].ToString();
                    usuario.Foto = dr["Foto"].ToString();
                    usuario.UsuarioSise = dr["UsuarioSise"].ToString();

                }
                if (bandera == false)
                {
                    usuario.Activo = 2; //Los datos son incorrectos;
                }
            }
            catch (Exception e)
            {
                usuario.Activo = 3; //Hubo un error
                usuario.Nombre = e.Message;
            }

            return usuario;
        }

        public static string GenerateSHA1Hash(string SourceText)
        {
            // Create an encoding object to ensure the encoding standard for the source text
            UnicodeEncoding Ue = new UnicodeEncoding();
            // Retrieve a byte array based on the source text
            byte[] ByteSourceText = Ue.GetBytes(SourceText);
            // Instantiate an MD5 Provider object
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            // Compute the hash value from the source
            byte[] ByteHash = SHA1.ComputeHash(ByteSourceText);
            // And convert it to String format for return
            return Convert.ToBase64String(ByteHash);
        }
    }
}