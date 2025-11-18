using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;
using MiTramite_Shared.Validators;

namespace MiTramite_Shared.DTOs.FuncionarioDTOs
{
    public class FuncionarioEditDTO
    {
        public long IdFuncionario { get; set; }
        [Required(ErrorMessage = "El nombre del funcionario es obligatorio.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre funcionario solo puede contener letras.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido paterno del funcionario es obligatorio.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ]+$", ErrorMessage = "El apellido paterno del funcionario solo puede contener letras.")]
        public string ApellidoPaterno { get; set; }

        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ]+$", ErrorMessage = "El apellido materno del funcionario solo puede contener letras.")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento del funcionario es obligatoria.")]
        [MayorDeEdad(18, ErrorMessage = "El funcionario debe ser mayor de edad.")]
        public DateTime? FechaNacimiento { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El correo del funcionario es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo del funcionario no es válido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono del funcionario es obligatorio.")]
        [RegularExpression(@"^[67]+[0-9]{7,}$", ErrorMessage = "El teléfono del funcionario solo puede contener números y debe comenzar con 6 o 7 con un máximo de 8 dígitos.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La contraseña del funcionario es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña del funcionario debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#@$!%*?&_\-\.])[A-Za-z\d#@$!%*?&_\-\.]{8,}$", ErrorMessage = "La contraseña del funcionario debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
        public string Password { get; set; } = null!;

        public FuncionarioEditDTO(Funcionario funcionario)
        {
            IdFuncionario = funcionario.IdFuncionario;
            Nombres = funcionario.Nombres;
            ApellidoPaterno = funcionario.ApellidoPaterno;
            ApellidoMaterno = funcionario.ApellidoMaterno;
            FechaNacimiento = funcionario.FechaNacimiento;
            Correo = funcionario.Correo;
            Telefono = funcionario.Telefono;
            Password = funcionario.PasswordHash;
        }

        public FuncionarioEditDTO()
        {

        }

    }
}