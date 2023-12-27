using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OLSoftwareTest.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace OLSoftwareTest.Models
{
    public class AspirantesViewModel
    {
        [Key]
        public int id_aspirante { get; set; }
        public string id_login { get; set; }
        public string nombre_aspirante { get; set; }
        public string apellido_aspirante { get; set; }
        public string documento_aspirante { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public string fecha_nacimiento_aspirante { get; set; }
        public string celular_aspirante { get; set; }
        [DataType(DataType.EmailAddress)]
        [MaxLength(10)]
        public string email_aspirante { get; set; }
        public string direccion_aspirante { get; set; }
        public string pais_aspirante { get; set; }
        public string ciudad_aspirante { get; set; }
        public int id_prueba { get; set; }
        public string nombre_prueba { get; set; }
        public int cantidad_preguntas { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public string fecha_inicio { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public string fecha_finalizacion { get; set; }
        
        public int id_prueba_aspirante { get; set; }
    }
}
