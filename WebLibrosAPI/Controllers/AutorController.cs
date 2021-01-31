using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;    
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLibrosAPI.Contexto;
using WebLibrosAPI.Entidades;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace WebLibrosAPI.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;//using AutoMapper;
        public AutorController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        //[HttpGet("/listado")]
        //[HttpGet("listado")]
        //public ActionResult<IEnumerable<Autor>> Get()
        //{
        //    return context.Autores.Include(x=> x.Libros).ToList();
        //}

        //[HttpGet]//probarla en la URL/Api/AUTOR
        //[ResponseCache(Duration =15)]
        //[Authorize]//using Microsoft.AspNetCore.Authorization;//Necesita un TOKEN
        //[ServiceFilter(typeof(Helpers.FiltroAccionPersonalizado))]
        //public ActionResult<string> Get()
        //{

        //    return DateTime.Now.Second.ToString();//Ejemplo para el Cache

        //}


        // [HttpGet]//probarla en la URL/Api/AUTOR
        // [ResponseCache(Duration = 15)]
        //// [Authorize]//using Microsoft.AspNetCore.Authorization;//Necesita un TOKEN
        // [ServiceFilter(typeof(Helpers.FiltroAccionPersonalizado))]
        // public ActionResult<IEnumerable<Autor>> Get()
        // {
        //    // throw new NotImplementedException();
        //     return context.Autores.Include(x => x.Libros).ToList(); //Ejemplo para ServiceFilter
        // }
     

        [HttpGet("primero")]
        public ActionResult<Autor> GetPrimerAutor()
        {
            return context.Autores.Include(x => x.Libros).FirstOrDefault();
        }

        //[HttpGet("{id}", Name = "ObtenerAutor")]//SINCRONICO------------
        //public ActionResult<Autor> Get(int id)
        //{
        //    var resultado = context.Autores.Include(x => x.Libros).FirstOrDefault(x => x.Id == id);
        //    if (resultado == null)
        //    {
        //        return NotFound();
        //    }
        //    return resultado;
        //}
        ////[HttpGet("{id}/{param2}", Name = "ObtenerAutor")]// ASINCRONICA-----------
        ////public async Task<ActionResult<Autor>> Get(int id, string param2)
        ////{
        ////    var resultado = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);
        ////    if (resultado == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    return resultado;
        ////}

        //[HttpPost]
        //public ActionResult Post([FromBody] Autor autor)
        //{
        //    context.Autores.Add(autor);
        //    context.SaveChanges();
        //    return new CreatedAtRouteResult("ObtenerAutor", new { id= autor.Id},autor);
        //}
        //En el Put Necesitamos enviar el ID por parametro
        //y en el FromBody el Objeto completo, repitiendo el ID incluso
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] Autor value)
        {
            if (id != value.Id)
            {
                BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }
        
        //[HttpDelete("{id}")]
        //public ActionResult<Autor> Delete(int id)
        //{
        //    var resultado = context.Autores.FirstOrDefault(x => x.Id == id);
        //    if (resultado == null)
        //    {
        //        return NotFound();
        //    }
        //    context.Autores.Remove(resultado);
        //    context.SaveChanges();
        //    return resultado;
        //}


        //AutoMapper
        [HttpGet] 
        [ResponseCache(Duration = 15)]
        [ServiceFilter(typeof(Helpers.FiltroAccionPersonalizado))]
        public ActionResult<IEnumerable<Models.AutorDTO>> Get()
        {
            var autores= context.Autores.ToList();
            var autoresDTO = mapper.Map<List<Models.AutorDTO>>(autores);
            return autoresDTO;
        }
        [HttpGet("{id}/{param2}", Name = "ObtenerAutor")]// ASINCRONICA-----------
        public async Task<ActionResult<Models.AutorDTO>> Get(int id, string param2)
        {
            var resultado = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);
            if (resultado == null)
            {
                return NotFound();
            }
            return mapper.Map<Models.AutorDTO>(resultado);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.AutorCreacionDTO autorCreacion)
        {
            var autor = mapper.Map<Autor>(autorCreacion);
            context.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<Models.AutorDTO>(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autorDTO);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            //Con select recupero 1 ID
            //Solamente recupero 1 ID en donde se de la condicion x == id
            //No se usa X.ID  por que antes me traia todo el Objeto, ahora solo el ID
            var resultado = await context.Autores.Select(x=> x.Id).FirstOrDefaultAsync(x => x == id);
            if (resultado == default(int))
            {
                return NotFound();
            }
            context.Autores.Remove(new Autor { Id = resultado });//Si n es null, remueve donde este el ID
             await context.SaveChangesAsync();
            return NoContent();
        }
    }


  
}
