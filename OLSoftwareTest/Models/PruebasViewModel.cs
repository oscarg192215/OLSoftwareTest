using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OLSoftwareTest.Models.DTO;
using System.ComponentModel;

namespace OLSoftwareTest.Models
{
    public class PruebasViewModel
    {
        [Key]
        public int id_prueba { get; set; }
        [DisplayName("Nombre Prueba")]
        public string? nombre_prueba { get; set; }
        public int id_tipo_prueba { get; set; }
        public int id_lenguaje { get; set; }
        public int id_nivel { get; set; }
        public int id_estado_prueba_aspirante { get; set; }
        public int cantidad_preguntas { get; set; }
        [ForeignKey("id_tipo_prueba")]
        public virtual TiposPruebas TiposPruebas { get; set; }
        [ForeignKey("id_lenguaje")]
        public virtual LenguajesProgramacion LenguajesProgramacion { get; set; }
        [ForeignKey("id_nivel")]
        public virtual NivelesConocimiento NivelesConocimiento { get; set; }
        [ForeignKey("id_estado_prueba_aspirante")]
        public virtual EstadosPruebaAspirante EstadosPruebaAspirante { get; set; }
        public List<Preguntas> Preguntas { get; set; }
    }
}
