using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebLibrosAPI.Models
{
    public class LibroDTO
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }

        //Vinculacion con la clase Autor ForeKey 
        public int AutorId { get; set; }
    }
}
