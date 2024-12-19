using Livraria.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Livraria.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LivrariaDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AdicionarCors();
            services.AdicionarControllerComJsonConfig();
            services.AddEndpointsApiExplorer();
            services.AdicionarInjecaoDependecia();
            services.AdicionarSwagger();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livraria API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }
    }
}
