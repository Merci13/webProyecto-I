using System;

namespace ProyectoWeb.Models
{
    public class Clientes
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int CedulaJuridica { get; set; }
        public string PaginaWeb { get; set; }
        public string DireccionFisica { get; set; }
        public int NúmerodeTelefono { get; set; }
        public string Sector { get; set; }
    }
}

