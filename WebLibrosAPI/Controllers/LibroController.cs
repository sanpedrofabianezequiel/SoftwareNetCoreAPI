using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLibrosAPI.Contexto;
using WebLibrosAPI.Entidades;

namespace WebLibrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        //Inyeccion
        public LibroController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            //Trae todos los libros que tienen autor
            return context.Libros.Include( x=> x.Autor).ToList();
        }
    }
}
