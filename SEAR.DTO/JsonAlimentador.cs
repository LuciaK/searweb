using System;
using System.Collections.Generic;
using System.Text;

namespace SEAR.DTO
{
    public class JsonAlimentador
    {
        public bool alerta { get; set; }
        public string hora1 { get; set; }
        public string hora2 { get; set; }
        public string hora3 { get; set; }
        public string hora4 { get; set; }
        public string nombreMascota { get; set; }
        public int porcion { get; set; }
    }
}
