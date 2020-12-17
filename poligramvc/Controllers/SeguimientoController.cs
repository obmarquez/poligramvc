using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligramvc.Models;

namespace poligramvc.Controllers
{
    public class SeguimientoController : Controller //Cat_Controlador: 35
    {
        // GET: Seguimiento
        [FiltroPermisos]
        public ActionResult Index(string poligrafista = "") //Cat_Controlador_Acciones: 81
        {
            List<SelectListItem> periodo = new List<SelectListItem>();
            periodo.Add(new SelectListItem { Text = "I", Value = "I" });
            periodo.Add(new SelectListItem { Text = "II", Value = "II" });
            periodo.Add(new SelectListItem { Text = "III", Value = "III" });
            periodo.Add(new SelectListItem { Text = "IV", Value = "IV" });
            ViewBag.losPeriodos = periodo;

            var losInv = Consultas.get_investigador_area(4).ToList();
            var losInvest = new SelectList(losInv, "UsuarioSise", "Nombre");
            ViewData["investigador"] = losInvest;

            if(poligrafista == "")
            {
                return View();
            }
            else
            {
                List<Consultas> losSeguimientos = Consultas.getSeguimientoCartaCompromiso(poligrafista, 2);
                ViewData["seguimientos"] = losSeguimientos;

                List<Consultas> lasCartas = Consultas.getSeguimientoCartaCompromiso(poligrafista, 3);
                ViewData["cartas"] = lasCartas;

                return View(Consultas.getSeguimientoCartaCompromiso(poligrafista, 1).FirstOrDefault());
            }
            
        }
    }
}