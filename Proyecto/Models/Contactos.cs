using System;

namespace ProyectoWeb.Models
{
    public class Contactos
    {
        public int ID { get; set; }
        
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public int Telefono { get; set; }
        public string Puesto { get; set; }
        public int ClienteID { get; set; }
        public virtual Clientes Cliente { get; set; }
        




    }
}