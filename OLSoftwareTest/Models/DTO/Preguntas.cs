using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLSoftwareTest.Models.DTO
{
    public class Preguntas
    {
        [Key]
        public int id_pregunta { get; set; }
        [DisplayName("Pregunta")]
        public string contenido_pregunta { get; set; }

        public int id_nivel { get; set; }
        public int id_lenguaje { get; set; }
    }
}
