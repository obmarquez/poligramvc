using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligramvc.Models;

namespace poligramvc.Controllers
{
    public class TestController : Controller //Cat_Controlador: 28
    {
        // GET: Test
        [FiltroPermisos]
        public ActionResult IndexEvaluador() //Cat_controlador_Acciones: 63
        {
            if (Session["Id_userPol"] != null)
            {
                var losSup = Consultas.getSupervisores(4).ToList();
                var losSuper = new SelectList(losSup, "UsuarioSise", "Nombre");
                ViewData["super"] = losSuper;

                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult IndexSupervisor() //Cat_controlador_ACciones: 64
        {
            if (Session["Id_userPol"] != null)
            {
                var losInv = Consultas.get_investigador_area(4).ToList();
                var losInvest = new SelectList(losInv, "UsuarioSise", "Nombre");
                ViewData["investigador"] = losInvest;

                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult IndexDirector(string fecha01 = "", string fecha02 = "") //Cat_controlador_ACciones: 80
        {
            if (fecha01 == "")
            {
                List<Test> evaluacionPoligrafista = Test.getEvalPoligrafista("01/01/1900", "01/01/1900");
                ViewData["poligrafista"] = evaluacionPoligrafista;

                List<Test> evaluacionSupervisor = Test.getEvalSupervisor("01/01/1900", "01/01/1900");
                ViewData["supervisor"] = evaluacionSupervisor;

                return View();
            }
            else
            {
                List<Test> evaluacionPoligrafista = Test.getEvalPoligrafista(fecha01, fecha02);
                ViewData["poligrafista"] = evaluacionPoligrafista;

                List<Test> evaluacionSupervisor = Test.getEvalSupervisor(fecha01, fecha02);
                ViewData["supervisor"] = evaluacionSupervisor;

                return View();
            }
        }

        public JsonResult jsAgregaEvaluacionDesempeno(Test t)
        {
            string resultado = "Error";

            string result = Test.agregarDesempeno(Session["UsuarioSise"].ToString(), t.caltec01, t.caltec02, t.caltec03, t.caltec04, t.caltec05, t.caltec06, t.caltec07, t.observatec01, t.observatec02, t.observatec03, t.observatec04, t.observatec05, t.observatec06, t.observatec07, t.calcon01, t.calcon02, t.calcon03, t.observacon01, t.observacon02, t.observacon03, t.evaluado, t.accion);

            if (result == "Ok")
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