using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebLibrosAPI.Contexto;
using WebLibrosAPI.Entidades;
using WebLibrosAPI.Models;

namespace WebLibrosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)// ACA SE AGREGAN LAS CONFIGURACIONES SERIA UNA INTERFAZ
        {

            services.AddCors(options =>
                    options.AddPolicy("PermitirApiRequest",
                    builder => builder.WithOrigins("http://www.apirequest.io").WithOrigins("GET","POST").AllowAnyHeader()
                    ));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //Con que vamos a autenticar y como JSONWEBTOCKEN => LIBREARIA(JWT)
            services.AddResponseCaching();//Servicio de Cache Activado(CONFIGURACION)la logica va en la APLICACION
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            //requiere Microsofot.AspNetCore.Authentication.JwtBearer;
            services.AddScoped<Helpers.FiltroAccionPersonalizado>();
            services.AddControllers(options => { options.Filters.Add(new Helpers.FiltroExcepcion()); }); //No necesita una dependencia del CONFIGURE

            services.AddControllers() .AddNewtonsoftJson(x=> { x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
            services.AddControllers();

            services.AddAutoMapper(configuration =>//Mappeo
              {
                  configuration.CreateMap<Autor, AutorDTO>();
                  configuration.CreateMap<Autor, AutorCreacionDTO>();
              },
            typeof(Startup));

            //Inyectamos los servicios que vamos a UTILIZAR
            //SQL EJ:
            services.AddDbContext<ApplicationDbContext>(opciones => 
                opciones.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
         

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//ACA SE USAN, SERIA LA IMPLEMENTACION
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseResponseCaching();//ACA LO VAMOS  BUSCAR (ESTO ES CONFIURACION)


            app.UseAuthentication();//jwt

            app.UseRouting();

            app.UseAuthorization();

            //Menejo de CORS-----
            //  app.UseCors(builder => builder.WithOrigins("http://www.apirequest.io"));
            // app.UseCors(x => x.AllowAnyOrigin());
            //app.UseCors(builder => builder.WithOrigins("http://www.apirequest.io").WithMethods("GET","POST").AllowAnyHeader());

            //Se puede hacer de esta forma y SETIANDOSELO AL ATRIBUTO/CONTROLADOR
            app.UseCors(); //Y EN EL configureService


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
