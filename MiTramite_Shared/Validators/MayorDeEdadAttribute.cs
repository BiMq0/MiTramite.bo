using System.ComponentModel.DataAnnotations;

namespace MiTramite_Shared.Validators
{
    public class MayorDeEdadAttribute : ValidationAttribute
    {
        private readonly int _edadMinima;

        public MayorDeEdadAttribute(int edadMinima = 18)
        {
            _edadMinima = edadMinima;
            ErrorMessage = $"Debe ser mayor de {edadMinima} años";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime fechaNacimiento)
                return ValidationResult.Success;

            var edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;

            return edad >= _edadMinima
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        }
    }
}