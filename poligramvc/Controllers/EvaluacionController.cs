using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligramvc.Models;

namespace poligramvc.Controllers
{
    public class EvaluacionController : Controller  //Cat_Controlador: 12
    {
        // GET: Evaluacion
        [FiltroPermisos]
        public ActionResult IndexSupervisor(string cadena = "") //Cat_controlador_Acciones: 32
        {
            if (Session["Id_userPol"] != null)
            {
                if (cadena.Trim() != "")
                    return View(Consultas.getListaEvaluadosSupervisor(cadena).ToList());
                else
                    return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }

        }

        [FiltroPermisos]
        public ActionResult IndexPoligrafista() //Cat_controlador_Acciones: 31
        {
            if (Session["Id_userPol"] != null)
            {
                //Obteniendo los municipios para el modal del red de vinculos
                var losMuni = Consultas.getMunicipiosEstado().ToList();
                var losMunic = new SelectList(losMuni, "cve_mun", "mun_desc");
                ViewData["municipios"] = losMunic;

                return View(Consultas.getListaEvaluadosPendiente(Session["UsuarioSise"].ToString()).ToList());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult NuevaEvaluacionPoligrafia(int pIdH, int pIdHP) //Cat_controlador_Acciones: 61
        {
            if (Session["Id_userPol"] != null)
            {
                var losSup = Consultas.getSupervisores(4).ToList();
                var losSuper = new SelectList(losSup, "UsuarioSise", "Nombre");
                ViewData["super"] = losSuper;

                //lista de las observaciones publicas del evaluado
                List<Consultas> observacionesEval = Consultas.getListaObservacionDiariaxEvaluado(pIdH, 4);
                ViewData["obser"] = observacionesEval;

                ViewBag.sIdH = pIdH;
                ViewBag.sIdHP = pIdHP;

                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult EditarEvaluacion(int pIdh, int pIdHP) //Cat_controlador_Acciones: 62
        {
            if (Session["Id_userPol"] != null)
            {
                var losSup = Consultas.getSupervisores(4).ToList();
                //var losSuper = new SelectList(losSup, "UsuarioSise", "UsuarioSise");
                var losSuper = new SelectList(losSup, "UsuarioSise", "Nombre");
                ViewData["super"] = losSuper;

                //lista de las observaciones publicas del evaluado
                List<Consultas> observacionesEval = Consultas.getListaObservacionDiariaxEvaluado(pIdh, 4);
                ViewData["obser"] = observacionesEval;

                ViewBag.sIdH = pIdh;
                ViewBag.sIdHP = pIdHP;

                return View(EvaluacionPol.obtenerEvaluacion(pIdh, pIdHP).FirstOrDefault());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult convertirImagen(int _IdH)
        {
            var imagenEvaluado = EvaluacionPol.getImgEva(_IdH).FirstOrDefault<EvaluacionPol>();
            if (imagenEvaluado != null)
                return File(imagenEvaluado.Picture, "image/jpeg");
            else
                return Json("Nada");
        }

        public JsonResult AddUpdInvestigacionPoligrafica(EvaluacionPol Eval)
        {
            string resultado = "Error";

            string result = EvaluacionPol.agregaActualizaEvaluacionPoligrafica(Eval.idhistorico, Eval.fEvalpol, Eval.cEscolaridad, Eval.cCarrera, Eval.cLaboral, Eval.cAdmisiones, Eval.cObservaciones, Eval.cNota, Eval.cSintesis, Eval.cAnalisis, Eval.cObservapoligrafia, Eval.cDiagnostico, Eval.cDxt, Eval.cCalidad, Eval.bNdiporinformacion, Eval.bDiilegales, Eval.bDiconfidencia, Eval.bDiilicitos, Eval.bDiorganizada, Eval.bDidelitos, Eval.bDialterar, Eval.bAdmilegales, Eval.bAdmconfidencial, Eval.bAdmilicitos, Eval.bAdmorganizada, Eval.bAdmdelitos, Eval.bAdmalterar, Eval.bCardiacos, Eval.bEpilepsia, Eval.bEmbarazo, Eval.bOtros, Eval.bEmpriricosimplificado, Eval.bCuartografico, Eval.bAnalisisglobal, Eval.bOss3, Eval.bMgqtv1, Eval.bTrespuntos, Eval.bSietepuntos, Eval.bDlst, Eval.idevalpol, Session["UsuarioSise"].ToString(), Session["UsuarioSise"].ToString(), Eval.accion);

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

        public ActionResult IndicePoligrafia(int sIdH, int sIdHP)
        {
            return RedirectToAction("NuevoIndice", "Indice", new { sIdH = sIdH, sIdHP = sIdHP });
        }

        public ActionResult IndiceEditarPoligrafia(int sIdH, int sIdHP)
        {
            return RedirectToAction("EditarIndice", "Indice", new { sIdH = sIdH, sIdHP = sIdHP });
        }

        public ActionResult js_red_vinculos(Int32 p_idHistoricoIndex, String p_nombreReferido_Modal, String p_paternoReferido_Modal, String p_maternoReferido_Modal, String p_genero_Modal, String p_relacion_Modal, String p_alias_Modal, String p_coorporacion_Modal, String p_municipio_Modal, String p_referencia_Modal)
        {
            //List<Permisos> list_permisos = new List<Permisos>();
            //list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
            //int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
            string result = Red.agregarRed(0, p_idHistoricoIndex, p_nombreReferido_Modal, p_alias_Modal, p_relacion_Modal, p_genero_Modal, p_coorporacion_Modal, p_municipio_Modal, p_referencia_Modal, Session["UsuarioSise"].ToString(), 1, p_paternoReferido_Modal, p_maternoReferido_Modal);
            return Json(result);

            //if (Session["Id_Usuario"] != null)
            //{
            //    //List<Permisos> list_permisos = new List<Permisos>();
            //    //list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
            //    //int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
            //    string result = Red.agregarRed(0, p_idHistoricoIndex, p_nombreReferido_Modal, p_alias_Modal, p_relacion_Modal, p_genero_Modal, p_coorporacion_Modal, p_municipio_Modal, p_referencia_Modal, Session["UsuarioSise"].ToString(), 1, p_paternoReferido_Modal, p_maternoReferido_Modal);
            //    return Json(result);
            //}
            //else { return Json("SESSION EXPIRED"); }
        }

        [FiltroPermisos] //Cat_Controlador_Acciones: 
        public ActionResult EntradaDiariaPol(string fecha = "")
        {
            if (Session["Id_userPol"] != null)
            {
                if (fecha == "")
                {
                    return View();
                }
                else
                {
                    return View(Consultas.getEntradaDiaria(fecha).ToList());
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }
    }
}