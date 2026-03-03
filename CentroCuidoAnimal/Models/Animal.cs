using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCuidoAnimal.Models
{
    public class Animal
    {
        public string NumeroExpediente { get; set; }
        public string Especie { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int EdadAproximada { get; set; }
        public string Sexo { get; set; }
        public string Color { get; set; }
        public string MotivoIngreso { get; set; }
        public string EstadoSaludIngreso { get; set; }
        public string Temperamento { get; set; }
        public string NecesidadesMedicasInmediatas { get; set; }

        public string HistorialVacunas { get; set; }
        //Datos Responsable
        public string NombreResponsable { get; set; }
        public string TelefonoResponsable { get; set; }
        public string EmailResponsable { get; set; }

        public List<Tratamiento> Tratamientos { get; set; }
    }
}