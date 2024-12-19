using Livraria.Application.Handlers;
using Livraria.Application.Queries;
using Livraria.Application.Repositories;
using Livraria.Application.Services;
using Livraria.Infra.Data.Repositories;
using Livraria.Infra.RelatorioService;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace Livraria.API
{
    public static class MiddlewareService
    {
        public static void AdicionarSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Livraria API", Version = "v1" });
            });
        }

        public static void AdicionarCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed(origin => true);
                });
            });
        }

        public static void AdicionarControllerComJsonConfig(this IServiceCollection services) =>
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        public static void AdicionarInjecaoDependecia(this IServiceCollection services)
        {
            services.AddScoped<CadastroCommandHandler, CadastroCommandHandler>();
            services.AddScoped<ICadastroRepository, CadastroRepository>();
            services.AddScoped<ICadastroQuery, CadastroQuery>();

            services.AddScoped<RelatorioCommandHandler, RelatorioCommandHandler>();

            services.AddTransient<IRelatorioService, RelatorioService>();
        }
    }
}
