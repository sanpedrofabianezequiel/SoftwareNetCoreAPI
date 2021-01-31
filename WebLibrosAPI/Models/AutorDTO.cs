using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebLibrosAPI.Models
{
    public class AutorDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "El nombre del Autor es muy largo", MinimumLength = 2)]
        [Required]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Models.LibroDTO> Libros { get; set; }

    }
}
