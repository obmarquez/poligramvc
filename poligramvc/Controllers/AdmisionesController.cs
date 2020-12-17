using poligramvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace poligramvc.Controllers
{
    public class AdmisionesController : Controller //Cat_Controlador: 34
    {
        // GET: Admisiones
        [FiltroPermisos]
        public ActionResult IndexAdmision(string valor = "") //Cat_controlador_Acciones: 77
        {
            if (Session["Id_userPol"] != null)
            {
                if (valor != "")
                    return View(Admision.getListaAdmision(valor).ToList());
                else
                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult AgregarVerificacion(int idh, int idhp)
        {
            if (Session["Id_userPol"] != null)
            {
                ViewBag.sIdH = idh;

                return View(Admision.getlaAdmision(idh, idhp).FirstOrDefault());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult ConcentradoVerificacion(string fecha01 = "", string fecha02 = "") //Cat_controlador_Acciones: 78
        {
            if(Session["Id_userPol"] != null)
            {
                if(fecha01 != "" && fecha02 != "")
                {
                    return View(Admision.getConcentradoAdmisiones(fecha01, fecha02).ToList());
                }
                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public JsonResult AgregarActualizarVerificacion(Admision Adm)
        {
            string resultado = "Error";

            string result = Admision.agregaActualizaVerificacion(Adm.id, Adm.idh, Adm.verificacion, Session["UsuarioSise"].ToString(), Adm.horario, Adm.accion);

            if(result == "Ok")
            {
                resultado = "Ok";
            }
            else
            {
                resultado = "ERROR";
            }
            return Json(resultado);
        }
    }
}