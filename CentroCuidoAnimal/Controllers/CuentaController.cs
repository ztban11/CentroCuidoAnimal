using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using CentroCuidoAnimal.Models;
using Newtonsoft.Json;


namespace CentroCuidoAnimal.Controllers
{
    public class CuentaController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(string NombreUsuario, string Password)
        {
            // Garantizar no espacios blancos
            NombreUsuario = NombreUsuario?.Trim();
            Password = Password?.Trim();

            var ruta = Server.MapPath("~/App_Data/usuarios.json");
            var json = System.IO.File.ReadAllText(ruta);

            var listaUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);

            var usuario = listaUsuarios.FirstOrDefault(u => string.Equals(u.NombreUsuario?.Trim(), NombreUsuario, StringComparison.OrdinalIgnoreCase) && (u.Password?.Trim() == Password)
            );

            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, false);

                Session["RolUsuario"] = usuario.Rol;
                Session["NombreUsuario"] = usuario.NombreUsuario;

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}