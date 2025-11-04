using System.ComponentModel.DataAnnotations;
using MiTramite_Shared.Validators;
namespace MiTramite_Shared.DTOs.RentistaDTOs
{
    public class RentistaSignupDTO
    {
        [Required(ErrorMessage = "La cédula de identidad es obligatoria.")]
        [MinLength(5, ErrorMessage = "La cédula de identidad debe tener al menos 5 caracteres.")]
        [MaxLength(11, ErrorMessage = "La cédula de identidad no puede exceder los 20 caracteres.")]
        public string? CI { get; set; }


        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [MinLength(2, ErrorMessage = "Los nombres deben tener al menos 2 caracteres.")]
        [MaxLength(50, ErrorMessage = "Los nombres no pueden exceder los 50 caracteres.")]
        public string? Nombres { get; set; }


        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }


        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [MayorDeEdad(18)]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [MaxLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string? Correo { get; set; }


        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_-])[A-Za-z\d@$!%*?&_-]{8,}$",
            ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
        public string? Password { get; set; }
        public RentistaSignupDTO()
        {

        }
    }
}