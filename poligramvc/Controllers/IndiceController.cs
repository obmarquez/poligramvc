using poligramvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace poligramvc.Controllers
{
    public class IndiceController : Controller
    {
        // GET: Indice
        public ActionResult NuevoIndice(int sIdh, int sIdHP)
        {
            ViewBag.sIdh = sIdh;
            ViewBag.sIdHP = sIdHP;

            return View();
        }

        [HttpPost]
        public ActionResult NuevoIndice(Indice i)
        {
            string result = Indice.agregaActualizaIndice(i.idhistorico, i.idevalpol, Session["UsuarioSise"].ToString(), i.nreporte, i.datosgenerales, i.carta, i.personales, i.entrevista, i.preguntas, i.analisis, i.graficos, i.oficial, i.otros_cantidad, i.identificacion, i.otros_texto, 1);
            if (result == "Ok")
            {
                return RedirectToAction("IndexPoligrafista", "Evaluacion");
            }
            else
                return View();
        }

        public ActionResult EditarIndice(int sIdh, int sIdHP)
        {
            ViewBag.sIdh = sIdh;
            ViewBag.sIdHP = sIdHP;

            return View(Indice.obtenerIndice(sIdh, sIdHP).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditarIndice(Indice i)
        {
            string result = Indice.agregaActualizaIndice(i.idhistorico, i.idevalpol, Session["UsuarioSise"].ToString(), i.nreporte, i.datosgenerales, i.carta, i.personales, i.entrevista, i.preguntas, i.analisis, i.graficos, i.oficial, i.otros_cantidad, i.identificacion, i.otros_texto, 2);
            if (result == "Ok")
            {
                return RedirectToAction("IndexPoligrafista", "Evaluacion");
            }
            else
                return View();
        }
    }
}