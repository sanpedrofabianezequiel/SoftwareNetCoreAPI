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
        [HttpGet("{id}",Name ="ObtenerLibro")]//Le setiamos un nombre para poder reutilizar en el POST
        public ActionResult<Libro> Get(int id)
        {
            var result = context.Libros.FirstOrDefault(x => x.Id == id);
            if (result==null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            //En formato JSON en Postaman le envio el Objeto COMPLETO
            context.Libros.Add(libro);
            context.SaveChanges();
            //Redirijo a obtener un LIBRO x el id que acabo de generar
            //El primer id => representa el ID DEL PARAMETRO QUE NECESITA EL GET obtenerlibro
            return new CreatedAtRouteResult("ObtenerLibro", new { id=libro.Id },libro);
        }
    }
}
