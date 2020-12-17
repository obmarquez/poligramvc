using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligramvc.Models;

namespace poligramvc.Controllers
{
    public class InformesController : Controller //Cat_Controlador: 29
    {
        // GET: Informes
        [FiltroPermisos]
        public ActionResult TotalDetalles(string fecha01 = "", string fecha02 = "") //Cat_Controlador_Acciones: 65
        {
            if (Session["Id_userPol"] != null)
            {
                if (fecha01 != "" && fecha02 != "")
                {
                    return View(Consultas.getTotalizadoDetalle(fecha01, fecha02, 1).ToList());
                }
                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult Totalizado(string opcionTotalizado = "", string fecha01 = "", string fecha02 = "") //Cat_Controlador_Acciones: 66
        {
            if (Session["Id_userPol"] != null)
            {
                List<SelectListItem> opcion = new List<SelectListItem>();
                opcion.Add(new SelectListItem { Text = "NUEVO INGRESO", Value = "NUEVO INGRESO" });
                opcion.Add(new SelectListItem { Text = "PERMANENCIA", Value = "PERMANENCIA" });
                ViewBag.lasOpciones = opcion;

                if (fecha01 != "" && fecha02 != "")
                {
                    return View(Consultas.getTotalizado(fecha01, fecha02, opcionTotalizado).ToList());
                }

                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult IndexGraResPol(string poligrafista = "", string fecha01 = "", string fecha02 = "") //Cat_Controlador_Acciones: 79
        {
            if (Session["Id_userPol"] != null)
            {
                var losInv = Consultas.get_investigador_area(4).ToList();
                var losInvest = new SelectList(losInv, "UsuarioSise", "Nombre");
                ViewData["investigador"] = losInvest;

                if (poligrafista != "")
                {
                    return View(Consultas.getGraResPol(poligrafista, fecha01, fecha02).ToList());
                }
                else
                {
                    return View(Consultas.getGraResPol("999", "01/01/1900", "01/01/1900"));
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
            
        }
    }
}