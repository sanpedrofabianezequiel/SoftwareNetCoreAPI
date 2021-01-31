using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLibrosAPI.Helpers
{
    public class FiltroExcepcion: ExceptionFilterAttribute//using Microsoft.AspNetCore.Mvc.Filters;
    {
        public override void OnException(ExceptionContext context)
        {
            //context.ModelState.IsValid;
            string msj= context.Exception.Message;
        }
    }
}
