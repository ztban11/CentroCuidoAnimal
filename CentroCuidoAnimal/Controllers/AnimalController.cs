
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CentroCuidoAnimal.ViewModels;
using Newtonsoft.Json;

namespace CentroCuidoAnimal.Controllers
{
    public class AnimalController : Controller
    {
        private bool EsAdministrador()
        {
            return Session["RolUsuario"] != null &&
                   Session["RolUsuario"].ToString() == "Administrador";
        }

        // Obtener numero expediente
        private string GenerarNumeroExpediente()
        {
            string sRuta = Server.MapPath("~/App_Data/animales.json");

            if (!System.IO.File.Exists(sRuta))
                return $"{DateTime.Now.Year}-0001";

            string json = System.IO.File.ReadAllText(sRuta);
            var lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RegistroAnimal_ViewModel>>(json) ?? new List<RegistroAnimal_ViewModel>();

            if (!lista.Any())
                return $"{DateTime.Now.Year}-0001";

            int currentYear = DateTime.Now.Year;

            var registrosDelAnio = lista
                .Where(x => x.sNumeroExpediente.StartsWith(currentYear.ToString()))
                .ToList();

            if (!registrosDelAnio.Any())
                return $"{currentYear}-0001";

            int ultimoConsecutivo = registrosDelAnio
                .Select(x => int.Parse(x.sNumeroExpediente.Split('-')[1]))
                .Max();

            int nuevoConsecutivo = ultimoConsecutivo + 1;

            return $"{currentYear}-{nuevoConsecutivo.ToString("D4")}";
        }
       
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

            //Agregar número expediente
            model.sNumeroExpediente = GenerarNumeroExpediente();

            Session["Animal"] = model;

            return RedirectToAction("Confirmacion");
        }

        public ActionResult Confirmacion()
        {
            if (!EsAdministrador())
                return RedirectToAction("Index", "Panel");

            var model = Session["Animal"] as RegistroAnimal_ViewModel;

            if (model == null)
                return RedirectToAction("Paso1");

            return View(model);
        }

        [HttpPost]
        public ActionResult ConfirmacionGuardar()
        {
            if (!EsAdministrador())
                return RedirectToAction("Index", "Panel");

            var model = Session["Animal"] as RegistroAnimal_ViewModel;

            if (model == null)
                return RedirectToAction("Paso1");

            string path = Server.MapPath("~/App_Data/animales.json");

            List<RegistroAnimal_ViewModel> lista = new List<RegistroAnimal_ViewModel>();

            if (System.IO.File.Exists(path))
            {
                string jsonExistente = System.IO.File.ReadAllText(path);
                lista = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<List<RegistroAnimal_ViewModel>>(jsonExistente)
                        ?? new List<RegistroAnimal_ViewModel>();
            }

            lista.Add(model);

            string nuevoJson = Newtonsoft.Json.JsonConvert.SerializeObject(lista, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(path, nuevoJson);

            Session["Animal"] = null;

            TempData["Success"] = "Paciente registrado correctamente 🐾";

            return RedirectToAction("Index", "Panel");
        }

        public ActionResult Lista()
        {
            if (!EsAdministrador()) // later we can add role "Cuidador"
                return RedirectToAction("Index", "Panel");

            string path = Server.MapPath("~/App_Data/animales.json");

            List<RegistroAnimal_ViewModel> lista = new List<RegistroAnimal_ViewModel>();

            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                lista = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<List<RegistroAnimal_ViewModel>>(json)
                    ?? new List<RegistroAnimal_ViewModel>();
            }

            return View(lista);
        }

        public ActionResult Detalle(string id)
        {
            if (!EsAdministrador())
                return RedirectToAction("Index", "Panel");

            string path = Server.MapPath("~/App_Data/animales.json");

            if (!System.IO.File.Exists(path))
                return RedirectToAction("Lista");

            string json = System.IO.File.ReadAllText(path);

            var lista = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<RegistroAnimal_ViewModel>>(json);

            var animal = lista.FirstOrDefault(a => a.sNumeroExpediente == id);

            if (animal == null)
                return RedirectToAction("Lista");

            return View(animal);
        }
    }
}