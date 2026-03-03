using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCuidoAnimal.ViewModels
{
    public class Tratamiento_ViewModel
    {
        public DateTime dtFechaAdministracion { get; set; }

        public string sMedicamento { get; set; }

        public string sDosis { get; set; }

        public string sUsuario { get; set; }
    }
}