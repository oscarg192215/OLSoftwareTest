using System.ComponentModel.DataAnnotations;

namespace OLSoftwareTest.Models.DTO
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "La Clave Actual es obligatoria")]
        public string ? CurrentPassword { get; set; }

        [Required(ErrorMessage = "Nueva Clave es obligatoria")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$",ErrorMessage ="Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string? NewPassword { get; set; }
        [Required(ErrorMessage = "Confirmacion de Nueva Clave es obligatoria")]
        [Compare("NewPassword")]
        public string ? PasswordConfirm { get; set; }
        
    }
}
