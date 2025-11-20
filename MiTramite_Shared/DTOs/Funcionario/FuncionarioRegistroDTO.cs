using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Shared.DTOs.FuncionarioDTOs
{
    public class FuncionarioRegistroDTO
    {
        public long IdFuncionario { get; set; }
        public string CodigoFuncionario { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public FuncionarioRegistroDTO(Funcionario funcionario)
        {

            IdFuncionario = funcionario.IdFuncionario;
            CodigoFuncionario = funcionario.CodigoFuncionario;
            Nombres = funcionario.Nombres;
            ApellidoPaterno = funcionario.ApellidoPaterno;
            FechaNacimiento = funcionario.FechaNacimiento;
            Telefono = funcionario.Telefono;
            Correo = funcionario.Correo;
        }

        public FuncionarioRegistroDTO()
        {

        }
    }
}