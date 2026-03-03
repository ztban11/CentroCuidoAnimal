using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCuidoAnimal.Models
{
    public class Tratamiento
    {
        public DateTime FechaAplicacion { get; set; }
        public string TipoTratamiento { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string Observaciones { get; set; }
        public string UsuarioAplicador { get; set; }
    }
}