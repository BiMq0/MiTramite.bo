using System.ComponentModel.DataAnnotations;
using MiTramite_Shared.Validators;
namespace MiTramite_Shared.DTOs.RentistaDTOs
{
    public class RentistaSignupDTO
    {
        [Required(ErrorMessage = "La cédula de identidad es obligatoria.")]
        [MinLength(5, ErrorMessage = "La cédula de identidad debe tener al menos 5 caracteres.")]
        [MaxLength(11, ErrorMessage = "La cédula de identidad no puede exceder los 11 caracteres.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "La cédula de identidad solo puede contener números.")]
        public string? CI { get; set; }


        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [MinLength(2, ErrorMessage = "Los nombres deben tener al menos 2 caracteres.")]
        [MaxLength(50, ErrorMessage = "Los nombres no pueden exceder los 50 caracteres.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        public string? Nombres { get; set; }


        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "El apellido paterno solo puede contener letras.")]
        public string? ApellidoPaterno { get; set; }

        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "El apellido materno solo puede contener letras.")]
        public string? ApellidoMaterno { get; set; }


        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [LimiteEdad(100, ErrorMessage = "La edad máxima permitida es de 100 años.")]
        [MayorDeEdad(18, ErrorMessage = "El rentista debe ser mayor de edad.")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El teléfono del funcionario es obligatorio.")]
        [RegularExpression(@"^[67]+[0-9]{7,}$", ErrorMessage = "El teléfono del funcionario solo puede contener números y debe comenzar con 6 o 7 con un máximo de 8 dígitos.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo electrónico no es válido.")]
        public string? Correo { get; set; }


        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#@$!%*?&_\-\.])[A-Za-z\d#@$!%*?&_\-\.]{8,}$", ErrorMessage = "La contraseña del funcionario debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
        public string? Password { get; set; }
        public RentistaSignupDTO()
        {

        }
    }
}