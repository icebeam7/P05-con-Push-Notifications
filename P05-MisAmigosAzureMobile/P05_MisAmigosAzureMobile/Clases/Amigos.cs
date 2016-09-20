using System;
using System.Collections.Generic;
using System.Text;

namespace P05_MisAmigosAzureMobile.Clases
{
    public class Amigos
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public string Nombre { get; set; }
        public int Sexo { get; set; }
        public DateTime Cumple { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public bool EsMejorAmigo { get; set; }
    }
}
