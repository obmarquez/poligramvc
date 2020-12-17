using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace poligramvc.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult PolNuevo()
        {
            return View();
        }

        public ActionResult PolEditar()
        {
                return View();
        }

        public ActionResult Desempeno()
        {
            return View();
        }

        public ActionResult Verificacion()
        {
            return View();
        }
    }
}