using System;
using System.Collections.Generic;
using System.Linq;
using WAMiTramite.Components.Pages.UserComponents;
using WAMiTramite.Components.Pages.Forms;
using WAMiTramite.Components.Pages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace WAMiTramite.Handlers
{

    public class SPANavManager
    {
        public string CurrentNavItem { get; set; } = "Landing";
        public RenderFragment CurrentRenderFragment { get; set; } = builder =>
        {
            builder.OpenComponent(0, typeof(Landing));
            builder.CloseComponent();
        };
        private readonly Dictionary<SPANavItem, Type> ComponentMap = new()
        {
            [SPANavItem.Landing] = typeof(Landing),
            [SPANavItem.Login] = typeof(Login),
            [SPANavItem.Signup] = typeof(Register),
            [SPANavItem.MainMenu] = typeof(UserMainPage)
        };

        public void SetNavItem(SPANavItem item)
        {
            CurrentNavItem = item.ToString();
            CurrentRenderFragment = builder =>
                {
                    if (ComponentMap.TryGetValue(item, out var componentType))
                    {
                        builder.OpenComponent(0, componentType);
                        builder.CloseComponent();
                    }
                    else
                    {
                        builder.AddContent(0, "Componente no encontrado");
                    }
                };

        }

        public void NavigateTo(SPANavItem item)
        {
            SetNavItem(item);
            OnNavItemChanged?.Invoke();
        }

        public Action OnNavItemChanged { get; set; }

    }
}