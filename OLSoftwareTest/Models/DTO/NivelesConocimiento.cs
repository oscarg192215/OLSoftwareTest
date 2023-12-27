using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OLSoftwareTest.Models.DTO
{
    public class NivelesConocimiento
    {
        [Key]
        public int id_nivel { get; set; }
        [DisplayName("Nivel")]
        public string? nombre_nivel { get; set; }
        [DisplayName("Descripcion")]
        public string? descripcion_nivel { get; set; }
    }
}
