

PackNugget
-EntityFrameworkCore
-EntityFrameworkCore.SqlServer
-EntityFrameworkCore.Tools
-Microsoft.AspNetCore.Mvc.NewtonsoftJson version 3.0  EVITAMOS LA REFERENCIA CIRCULAR
	            services.AddControllers() 
			.AddNewtonsoftJson(x=> 
				{ x.SerializerSettings.ReferenceLoopHandling = 
					Newtonsoft.Json.ReferenceLoopHandling.Ignore; });



JWT = > Autentication
Microsoft.AspNetCore.Authentication.JwtBearer revisar la versio que se acople al proyecto


Importante en el startup
Agregar en el ConfigureService
services.AddScoped<Helpers.FiltroAccionPersonalizado>();//Lo agregamos

en el Configure lo llamamos//es dcir en el Metodo Configure Usarlo!!
en algunos casos como Filtro no se usa el en CONFIGURE ya que no para verlo nosotros solamente

REVISAR EL [Authorize] por que sin la Autorizacion no me levantaria la API


-When we need AutoMapper = > Then We Need Create One folder with name will be Models with ClassNameDTO
-Insert Into Console MappNugget
-Command => Install-Package Automapper.Extensions.Microsoft.DependencyInjection
-Luego se configura en el StartUp
-Agregamos ene l ConfigureService el Automapper
	=>    services.addAutoMapper(typeof(Startup));


-Sintaxis Final para el Mappep
	services.AddAutoMapper(configuration =>//Mappeo
              {
                  configuration.CreateMap<Autor, AutorDTO>();
              },
            typeof(Startup));
--En el controlador debemos agregar
a nivel injection de dependencia   
-Creamos una variable 
	.private readonly IMapper maper;
	.Hacemos una Injeccion de Depencias en  el construcctor agregandole un parametro para que la inicialice
	---Sintaxis Final
	 private readonly ApplicationDbContext context;
        private readonly IMapper mapper;//using AutoMapper;
        public AutorController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

--Luego Add-Migration Para hacer los cambios y Update-Database