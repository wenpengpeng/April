using System.Web.Http;
using WebActivatorEx;
using AprilWebApi;
using Swashbuckle.Application;
using System;
using System.IO;
using System.Reflection;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace AprilWebApi
{
    /// <summary>
    ///     ≈‰÷√swagger
    /// </summary>
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {                       
                        c.SingleApiVersion("v1", "AprilWebApi");
                        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,$"bin\\{typeof(SwaggerConfig).Assembly.GetName().Name}.XML");
                        c.IncludeXmlComments(filePath);
                    })
                .EnableSwaggerUi("docs/{*assetPath}",c =>
                {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(SwaggerConfig)),"AprilWebApi.swagger.js");
                });
        }
    }
}
