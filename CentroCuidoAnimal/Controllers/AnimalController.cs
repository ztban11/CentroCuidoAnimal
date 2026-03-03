using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CentroCuidoAnimal.ViewModels;

namespace CentroCuidoAnimal.Controllers
{
    public class AnimalController : Controller
    {
        private bool EsAdministrador()
        {
            return Session["RolUsuario"] != null &&
                   Session["RolUsuario"].ToString() == "Administrador";
        }
        // GET: Animal
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Paso1()
        {
            if (!EsAdministrador())
                return RedirectToAction("Index", "Panel");

            var model = Session["Animal"] as RegistroAnimal_ViewModel;

            return View(model ?? new RegistroAnimal_ViewModel());
        }

        //POST Paso1
        [HttpPost]
        
        public ActionResult Paso1(RegistroAnimal_ViewModel RA_VM_Paso1)
        {
            if (!EsAdministrador())
                return RedirectToAction("Index", "Panel");

            Session["Animal"] = RA_VM_Paso1;

            return RedirectToAction("Paso2");
        }

        public ActionResult Paso2()
        {
            if (!EsAdministrador())
                return RedirectToAction("Index", "Panel");

            var model = Session["Animal"] as RegistroAnimal_ViewModel;

            if (model == null)
                return RedirectToAction("Paso1");

            return View(model);
        }

        //POST Paso2
        [HttpPost]
        
        public ActionResult Paso2(RegistroAnimal_ViewModel RA_VM_Paso2)
        {
            if (!EsAdministrador())
                return RedirectToAction("Index", "Panel");

            var model = Session["Animal"] as RegistroAnimal_ViewModel;

            if (model == null)
                return RedirectToAction("Paso1");

            // Paso 2 - Info de Salud
            model.bVacunado = RA_VM_Paso2.bVacunado;
            model.sNotasMedicas = RA_VM_Paso2.sNotasMedicas;
            model.dPeso = RA_VM_Paso2.dPeso;

            Session["Animal"] = model;

            return RedirectToAction("Paso3");
        }
        public ActionResult Paso3()
        {
            if (!EsAdministrador())
                return RedirectToAction("Index", "Panel");

            var model = Session["Animal"] as RegistroAnimal_ViewModel;

            if (model == null)
                return RedirectToAction("Paso2");

            return View(model);
        }

        //POST Paso3
        [HttpPost]
        
        public ActionResult Paso3(RegistroAnimal_ViewModel RA_VM_Paso3)
        {
            if (!EsAdministrador())
                return RedirectToAction("Index", "Panel");

            var model = Session["Animal"] as RegistroAnimal_ViewModel;

            if (model == null)
                return RedirectToAction("Paso2");

            // Paso 3 - Info de encargado
            model.dtFechaRecibo = RA_VM_Paso3.dtFechaRecibo;
            model.sNombrePersonaResponsable = RA_VM_Paso3.sNombrePersonaResponsable;
            model.sUbicacionIncidente = RA_VM_Paso3.sUbicacionIncidente;
            model.sNotasAdicionales = RA_VM_Paso3.sNotasAdicionales;

            Session["Animal"] = model;

            return RedirectToAction("Paso4");
        }
    }
}