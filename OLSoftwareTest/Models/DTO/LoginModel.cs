using System.ComponentModel.DataAnnotations;

namespace OLSoftwareTest.Models.DTO
{
    public class LoginModel
    {        
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La Clave es obligatoria")]
        public string Password { get; set; }
    }
}
