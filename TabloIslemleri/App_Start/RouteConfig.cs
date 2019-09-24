using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TabloIslemleri
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(//metodu URL'nizin şemasını ve bu URL şemasında hangi controller ve action'ın çalışacağı bilgisini tanımladığınız metotdur. 

                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Homes", action = "Index", id = UrlParameter.Optional }//Hangi Controller'ın çalışacağını işaret eder//İşaret edilen Controller ın hangi action metodunun çalışacağını işaret eder.
                //namespaces: new[] { "TabloIslemleri.Controllers" }
                );
           
        }

    }
}
