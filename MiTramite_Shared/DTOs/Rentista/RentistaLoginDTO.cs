using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MiTramite_Shared.DTOs.RentistaDTOs
{
    public class RentistaLoginDTO
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MaxLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres.")]
        public string? Password { get; set; }
        public RentistaLoginDTO()
        {

        }
    }
}