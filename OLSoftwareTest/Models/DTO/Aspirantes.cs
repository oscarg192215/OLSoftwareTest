using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OLSoftwareTest.Models.DTO
{
    public class Aspirantes
    {
        [Key]
        public int id_aspirante { get; set; }
        [DisplayName("LogIn")]
        public string id_login { get; set; }
        [DisplayName("Nombre")]
        public string nombre_aspirante { get; set; }
        [DisplayName("Apellido")]
        public string apellido_aspirante { get; set; }
        [DisplayName("Documento")]
        public string documento_aspirante { get; set; }
        [DisplayName("Fecha Nacimiento")]
        [BindProperty, DataType(DataType.Date)]
        public string fecha_nacimiento_aspirante { get; set; }
        [DisplayName("Celular")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        public string celular_aspirante { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string email_aspirante { get; set; }
        [DisplayName("Direccion")]
        public string direccion_aspirante { get; set; }
        [DisplayName("Pais")]
        public string pais_aspirante { get; set; }
        [DisplayName("Ciudad")]
        public string ciudad_aspirante { get; set; }
        [BindProperty, DataType(DataType.Date)]
        [DisplayName("Fecha Inicio")]
        public string fecha_inicio { get; set; }
        [BindProperty, DataType(DataType.Date)]
        [DisplayName("Fecha Finalizacion")]
        public string fecha_finalizacion { get; set; }
        [DisplayName("Prueba")]
        public int id_prueba { get; set; }
    }
}
