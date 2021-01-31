using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;// LIbreria a utilizar ILOGGER<>
namespace WebLibrosAPI.Helpers
{
    public class FiltroAccionPersonalizado : IActionFilter //using Microsoft.AspNetCore.Mvc.Filters;
    {
        private readonly ILogger<FiltroAccionPersonalizado> logger;
        public FiltroAccionPersonalizado(ILogger<FiltroAccionPersonalizado> logger )
        {
            this.logger = logger;//Inyeccion
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogError("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogError("OnActionExecuting");
        }
    }
}
