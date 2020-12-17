using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace poligramvc.Models
{
    public class FiltroPermisos : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            String Controlador = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            String Metodo = filterContext.ActionDescriptor.ActionName;

            List<Permisos> Permisos = new List<Permisos>();

            Permisos = (List<Permisos>)filterContext.HttpContext.Session["ListaPermisos"];

            int Privilegio = 0;

            try
            {
                if (Permisos == null)
                {
                    if (Metodo.Contains("js_") == true)
                    {
                        var result = new JsonResult();
                        result.Data = "SESION_EXPIRED";
                        filterContext.Result = result;
                    }
                    else { filterContext.Result = new RedirectResult("../Home/Index"); }
                }
                else
                {
                    foreach (var item in Permisos)
                    {
                        if (item.Desc_Controlador.ToString() == Controlador && item.Desc_Metodo.ToString() == Metodo) { Privilegio++; }
                    }

                    if (Privilegio == 0)
                    {
                        if (Metodo.Contains("js_") == true)
                        {
                            var result = new JsonResult();
                            result.Data = "PERMISO_DENEGADO"; //((ViewResult)filterContext.Result).Model;
                            filterContext.Result = result;
                        }
                        else
                        {
                            ViewResult result = new ViewResult();
                            result.ViewName = "nopermiso";
                            result.ViewBag.ErrorMessage = "No tiene permiso para realizar la acción deseada, contacte al administrador.";
                            filterContext.Result = result;
                        }
                    }
                }
            }
            catch { filterContext.Result = new RedirectResult("../Home/acceso"); }
        }
    }
}