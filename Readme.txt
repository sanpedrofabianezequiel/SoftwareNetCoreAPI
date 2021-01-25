

PackNugget
-EntityFrameworkCore
-EntityFrameworkCore.SqlServer
-EntityFrameworkCore.Tools
-Microsoft.AspNetCore.Mvc.NewtonsoftJson version 3.0  EVITAMOS LA REFERENCIA CIRCULAR
	            services.AddControllers() 
			.AddNewtonsoftJson(x=> 
				{ x.SerializerSettings.ReferenceLoopHandling = 
					Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
