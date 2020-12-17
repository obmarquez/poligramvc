using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligramvc.Models;

namespace poligramvc.Controllers
{
    public class EstatusController : Controller //ID CAT_CONTROLADOR : 30
    {
        [FiltroPermisos]
        // GET: Estatus
        public ActionResult IndexEstatus() //ID CAT_CONTROLADOR_ACCIONES : 67
        {
            if (Session["Id_userPol"] != null)
            {
                return View(Estatus.getListaRegresados(0, 0, 0).ToList());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [HttpPost]
        public JsonResult EditarEstatusPoligrafia(Estatus Est)
        {
            var resultado = new BaseResultado();
            string result = Estatus.updateEstatusPoligrafia(Est.idhistorico, Est.ide, Est.idhp);

            if(result == "Ok")
            {
                resultado.mensaje = "Estatus actualizado satisfactoriamente";
                resultado.Ok = true;
            }
            else
            {
                resultado.mensaje = "No se pudo realizar";
                resultado.Ok = false;
            }
            return Json(resultado);
        }
    }
}

public class BaseResultado
{
    public bool Ok { get; set; }
    public string mensaje { get; set; }
}