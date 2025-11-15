using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiTramite_Domain.Entities;

namespace MiTramite_Shared.DTOs.FuncionarioDTOs
{
    public class FuncionarioAccesosDTO
    {
        public long IdFuncionario { get; set; }
        public string? CodigoFuncionario { get; set; }
        public string? Rol { get; set; }
        public List<string>? Permisos { get; set; }
        public List<string>? Opciones { get; set; }
        public FuncionarioAccesosDTO(Funcionario funcionario)
        {
            IdFuncionario = funcionario.IdFuncionario;
            CodigoFuncionario = funcionario.CodigoFuncionario;
            Rol = funcionario.Rol?.NombreRol;
            Permisos = funcionario.Rol?.RolPermisos?.Select(rp => rp.Permiso!.Nombre).ToList();
            Opciones = funcionario.Rol?.RolOpciones?.Select(ro => ro.Opcion!.LabelOpcion).ToList();
        }

        public FuncionarioAccesosDTO()
        {

        }
    }
}