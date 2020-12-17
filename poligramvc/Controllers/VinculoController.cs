using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligramvc.Models;

namespace poligramvc.Controllers
{
    public class VinculoController : Controller //Cat_Controler = 
    {
        // GET: Vinculo
        public ActionResult IndexVinculoPoligrafia(string cadena = "") //Cat_controlador_Acciones: 47
        {
            if(cadena != "")
            {
                return View(Vinculos.getListaVinculosPoligrafia(cadena).ToList());
            }
            return View();
        }
    }
}