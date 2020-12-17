using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligramvc.Models;

namespace poligramvc.Controllers
{
    public class PoaPolController : Controller  //Cat_Controlador: 33
    {
        // GET: Poa
        [FiltroPermisos]
        public ActionResult IndexPoa(string fecha01 = "", string fecha02 = "")  //Cat_Controlador_Acciones: 76
        {
            if (Session["Id_userPol"] != null)
            {
                if (fecha01 != "" && fecha02 != "")
                {
                    List<Poa> poaEvalua = Poa.getPoa(fecha01, fecha02, 2);
                    ViewData["poaEval"] = poaEvalua;

                    List<Poa> poaDependencia = Poa.getPoa(fecha01, fecha02, 3);
                    ViewData["poaDep"] = poaDependencia;

                    return View(Poa.getPoa(fecha01, fecha02, 1).ToList());
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }
    }
}