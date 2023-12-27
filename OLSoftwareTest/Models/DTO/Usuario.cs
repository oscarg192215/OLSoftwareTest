using System.ComponentModel.DataAnnotations;

namespace OLSoftwareTest.Models.DTO
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string id_login { get; set; }
        public string name_login { get; set; }
    }
}
