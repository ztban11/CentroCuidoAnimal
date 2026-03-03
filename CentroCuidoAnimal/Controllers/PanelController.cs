using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCuidoAnimal.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        public ActionResult Index()
            {
            ViewBag.Role = Session["RolUsuario"];
            return View();
        }
       
    }
}