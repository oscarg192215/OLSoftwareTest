using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OLSoftwareTest.Models.DTO
{
    public class TiposPruebas
    {
        [Key]
        [DisplayName("Id")]
        public int id_tipo_prueba { get; set; }
        [DisplayName("Tipo Prueba")]
        public string? nombre_tipo_prueba { get; set; }
        [DisplayName("Descripcion")]
        public string? descripcion_tipo_prueba { get; set; }
    }
}
