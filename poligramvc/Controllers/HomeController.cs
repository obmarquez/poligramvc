using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligramvc.Models;

namespace poligramvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["Id_RolPol"]) == 20)
            {
                var nin = Consultas.getTotalEvaluadoTipoPOL(1, Session["UsuarioSise"].ToString()).FirstOrDefault();
                ViewBag.nuevoIngreso = nin.TotalE;

                var per = Consultas.getTotalEvaluadoTipoPOL(2, Session["UsuarioSise"].ToString()).FirstOrDefault();
                ViewBag.permanencia = per.TotalE;

                var pro = Consultas.getTotalEvaluadoTipoPOL(3, Session["UsuarioSise"].ToString()).FirstOrDefault();
                ViewBag.promocion = pro.TotalE;
            }

            return View(Consultas.ListStatus(Session["UsuarioSise"].ToString(), Convert.ToInt32(Session["Id_RolPol"])).ToList());
        }

        public ActionResult acceso()
        {
            return View();
        }

        [HttpPost]
        public ActionResult acceso(Logeo model)
        {
            if (ModelState.IsValid)
            {
                Usuarios Userr = Logeo.IsValid(model.UserName, model.Password);
                if (Userr.Activo == 1)
                {
                    Session["Id_userPol"] = Userr.Id_Usuario;
                    Session["Id_RolPol"] = Userr.Id_Rol;
                    Session["Nombre"] = Userr.NombreCompleto;
                    Session["Foto"] = Userr.Foto;
                    Session["UsuarioSise"] = Userr.UsuarioSise;
                    Session["UsuarioP"] = Userr;

                    ViewBag.rol = Session["Id_RolPol"];

                    List<Permisos> permisos = Permisos.getLista(Userr.Id_Rol);
                    Session["ListaPermisos"] = permisos;

                    return RedirectToAction("Index", "Home");
                }
                else if (Userr.Activo == 2)
                {
                    ModelState.AddModelError("", "El usuario o la contraseña son incorrectos.");
                }
            }
            else { ModelState.AddModelError("", "El usuario o la contraseña son incorrectos."); }
            return View(model);
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("acceso", "Home");
        }
    }
}