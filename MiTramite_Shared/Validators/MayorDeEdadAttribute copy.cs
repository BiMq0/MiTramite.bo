using System.ComponentModel.DataAnnotations;

namespace MiTramite_Shared.Validators
{
    public class LimiteEdad : ValidationAttribute
    {
        private readonly int _edadMaxima;

        public LimiteEdad(int edadMaxima = 100)
        {
            _edadMaxima = edadMaxima;
            ErrorMessage = $"Debe ser mayor de {edadMaxima} años";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime fechaNacimiento)
                return ValidationResult.Success;

            var edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;

            return edad < _edadMaxima
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        }
    }
}