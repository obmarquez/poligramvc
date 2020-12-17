using poligramvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace poligramvc.Controllers
{
    public class AsociarPolController : Controller //Cat_Controlador: 11
    {
        // GET: Asociar
        [FiltroPermisos]
        public ActionResult IndexAsociarPol(String fecha = "") //Cat_Controlador_Acciones: 30
        {
            if(Session["Id_userPol"] != null)
            {
                if (fecha == "")
                    return View();
                else
                {
                    var losPoli = Consultas.get_investigador_area(6);
                    var losPoligrafos = new SelectList(losPoli, "UsuarioSise", "Nombre");
                    ViewData["poligrafistas"] = losPoligrafos;

                    //-------Cubiculos
                    List<SelectListItem> cubos = new List<SelectListItem>();
                    //cubos.Add(new SelectListItem { Text = "1", Value = "1" });
                    for(int i=1; i<=30; i++)
                    {
                        cubos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                    }
                    ViewBag.losCubos = cubos;                    
                    //-------Cubiculos

                    return View(Consultas.getListaAsociarFecha(fecha, 4).ToList());
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }            
        }

        public ActionResult js_asociar(int p_idHistorico, string p_idEvaluador, string p_idSupervisor, int p_nCubiculo)
        {
            if(Session["Id_userPol"] != null)
            {
                //List<Permisos> list_permisos = new List<Permisos>();
                //list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                //int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
                string result = Asociar.asociar_poligrafista(p_idHistorico, p_idEvaluador, 4, true, 1, p_idSupervisor, p_nCubiculo);
                return Json(result);
            }
            else { return Json("Sesion Expirada"); }
        }

        //public ActionResult Download(string fileName)
        public ActionResult Download(string parametro1)
        {
            //string path = Path.Combine(@"C:/wamp/www/ReportesExpedientes/GuardiaNacional/GNPersonales_84984.pdf");
            string path = Path.Combine(@"C:/wamp/www/ReportesExpedientes/GuardiaNacional/GNPersonales_" + parametro1+".pdf");

            return File(path, "application/pdf");

        }

        [FiltroPermisos]
        public ActionResult IndexAsociarNombre(string nombre = "") //Cat_Controlador_Acciones: 75
        {
            if (Session["Id_userPol"] != null)
            {
                if (nombre == "")
                {
                    return View();
                }
                else
                {
                    var losPoli = Consultas.get_investigador_area(6);
                    var losPoligrafos = new SelectList(losPoli, "UsuarioSise", "Nombre");
                    ViewData["poligrafistas"] = losPoligrafos;

                    //-------Cubiculos
                    List<SelectListItem> cubos = new List<SelectListItem>();
                    //cubos.Add(new SelectListItem { Text = "1", Value = "1" });
                    for (int i = 1; i <= 30; i++)
                    {
                        cubos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                    }
                    ViewBag.losCubos = cubos;
                    //-------Cubiculos

                    return View(Consultas.getAsociarNombre(nombre, 4).ToList());
                }                    
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }
    }
}