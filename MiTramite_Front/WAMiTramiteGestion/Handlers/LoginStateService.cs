using MiTramite_Shared.DTOs.FuncionarioDTOs;

namespace WAMiTramiteGestion.Handlers
{
    public class LoginStateService
    {
        public event Action<FuncionarioAccesosDTO>? OnLoginSuccess;


        public event Action? OnLogout;

        public FuncionarioAccesosDTO? UsuarioActual { get; private set; }

        public bool EstaAutenticado => UsuarioActual != null;


        public void NotifyLoginSuccess(FuncionarioAccesosDTO usuario)
        {
            UsuarioActual = usuario;
            if (usuario != null)
            {
                OnLoginSuccess?.Invoke(usuario);
            }
        }

        public void NotifyLogout()
        {
            UsuarioActual = null;
            OnLogout?.Invoke();
        }
    }
}
