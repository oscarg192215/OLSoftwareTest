using System.ComponentModel.DataAnnotations;

namespace OLSoftwareTest.Models.DTO
{
    public class RegistrationModel
    {        
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La Clave es obligatoria")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$",ErrorMessage ="Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; }
        [Required(ErrorMessage = "La Confirmacion de Clave es obligatoria")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        public string? Role { get; set; }
        
    }
}
