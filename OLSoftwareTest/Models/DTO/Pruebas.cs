using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace OLSoftwareTest.Models.DTO
{
    public class Pruebas
    {
        [Key]
        public int id_prueba { get; set; }
        [DisplayName("Nombre Prueba")]
        public string? nombre_prueba { get; set; }
        public int id_tipo_prueba { get; set; }
        public int id_lenguaje { get; set; }
        public int id_nivel { get; set; }
        public int id_estado_prueba_aspirante { get; set; }
        public int[]? id_pregunta { get; set; }
    }
}
