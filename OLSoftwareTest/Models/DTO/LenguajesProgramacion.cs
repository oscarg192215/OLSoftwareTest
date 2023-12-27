using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OLSoftwareTest.Models.DTO
{
    public class LenguajesProgramacion
    {
        [Key]
        [DisplayName("Id")]
        public int id_lenguaje { get; set; }
        [DisplayName("Lenguaje")]
        public string? nombre_lenguaje { get; set; }
        [DisplayName("Descripcion")]
        public string? descripcion_lenguaje { get; set; }
    }
}
