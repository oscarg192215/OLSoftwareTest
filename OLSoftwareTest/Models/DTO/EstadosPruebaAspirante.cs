using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OLSoftwareTest.Models.DTO
{
    public class EstadosPruebaAspirante
    {
        [Key]
        public int id_estado_prueba_aspirante { get; set; }
        [DisplayName("Estado")]
        public string? nombre_estado { get; set; }
        [DisplayName("Descripcion")]
        public string? descripcion_estado { get; set; }
    }
}
