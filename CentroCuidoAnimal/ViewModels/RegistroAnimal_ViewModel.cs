using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCuidoAnimal.ViewModels
{
    public class RegistroAnimal_ViewModel
    {
        public RegistroAnimal_ViewModel()
        {
            Tratamientos = new List<Tratamiento_ViewModel>();
        }
    
        // Paso 1 - Info Básica
        public string sNumeroExpediente { get; set; }
        public string sNombre { get; set; }
        public string sEspecie { get; set; }
        public int? iEdad { get; set; }
        public string sGenero { get; set; }

        // Paso 2 - Info de Salud
        public bool bVacunado { get; set; }

        public decimal? dPeso { get; set; }
        public string sNotasMedicas { get; set; }

        public string sMotivoIngreso { get; set; }
        public string sEstadoSaludIngreso { get; set; }
        public string sTemperamento { get; set; }

        public string sNecesidadesInmediatas { get; set; }

        public string sHistorialVacunas { get; set; }

        // Paso 3 - Info de Recibo
        public DateTime? dtFechaRecibo { get; set; }

        public string sUbicacionIncidente { get; set; }
        public string sNombrePersonaResponsable { get; set; }
        public string sTelefonoContacto { get; set; }
        public string sEmailContacto { get; set; }
        public string sNotasAdicionales { get; set; }

        public List<Tratamiento_ViewModel> Tratamientos { get; set; }

    }
}