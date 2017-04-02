using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace w1Consultorio
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Perfil",
                "Perfil/{action}/{sistemaid}/{moduloid}/{id}",
                new { controller = "Perfil", action = "Index", id = UrlParameter.Optional, sistemaid = UrlParameter.Optional, moduloid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Transacao",
                "Transacao/{action}/{sistemaid}/{moduloid}/{id}",
                new { controller = "Transacao", action = "Index", id = UrlParameter.Optional, sistemaid = UrlParameter.Optional, moduloid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Pergunta",
                "Pergunta/{action}/{grupoid}/{id}",
                new { controller = "Pergunta", action = "Index", id = UrlParameter.Optional, grupoid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Modulo",
                "Modulo/{action}/{sistemaid}/{id}",
                new { controller = "Modulo", action = "Index", id = UrlParameter.Optional, grupoid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}