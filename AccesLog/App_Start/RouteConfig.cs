using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace AccesLog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Usuarios",
                "Usuarios/AdicionarUsuarios",
                new { controller = "Usuarios", action = "AdicionarUsuarios" }
            );

            routes.MapRoute(
                "CadUsuarios",
                "CadUsuarios",
                new { controller = "Usuarios", action = "Salvar" }
            );

            routes.MapRoute(
                "UsuariosEditar",
                "Usuarios/AlterarUsuarios/:id",
                new { controller = "Usuarios", action = "AlterarUsuarios", id = 0 }
            );

            routes.MapRoute(
                "LogAcessos",
                "Home/LogAcesso",
                new { controller = "Home", action = "LogAcesso" }
            );

            routes.MapRoute(
                "UsuariosExcluir",
                "Usuarios/UsuariosExcluir/:id",
                new { controller = "Usuarios", action = "UsuariosExcluir", id = 0 }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
