using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WAMiTramite.Handlers
{
    public class NavigationTriggers
    {
        private readonly SPANavManager SPANavManager;

        public NavigationTriggers(SPANavManager spaNavManager)
        {
            SPANavManager = spaNavManager;
        }

        public void NavigateToLanding() => SPANavManager.NavigateTo(SPANavItem.Landing);
        public void NavigateToLogin() => SPANavManager.NavigateTo(SPANavItem.Login);
        public void NavigateToSignup() => SPANavManager.NavigateTo(SPANavItem.Signup);
        public void NavigateToMenu() => SPANavManager.NavigateTo(SPANavItem.MainMenu);
    }
}