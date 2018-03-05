using System;

namespace ProyectoWeb.Models
{
    public class Support
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string DetalleDelProblema { get; set; }
        public string QuienReporto {get;set;}
        public string EstadoActual { get; set; }
        public int ClienteID { get; set; }
        public virtual Clientes Cliente { get; set; }

    }
}