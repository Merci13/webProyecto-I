using System;
namespace ProyectoWeb.Models
{
    public class Reuniones
    {
        public int ID { get; set; }
        public string TituloDeLaReunion { get; set; }
        public DateTime Fecha { get; set; }
        public string UsuarioAsignado { get; set; }
        public bool Virtual { get; set; }
        public int UsuarioID { get; set; }
        public virtual Usuarios Usuario { get; set; }
        
    }
}