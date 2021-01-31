using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebLibrosAPI.Entidades
{

    public class Autor
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(100,ErrorMessage ="El nombre del Autor es muy largo",MinimumLength =2)]
        [Required]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Libro> Libros { get; set; }//Recuperamos todos los libros
        [CreditCard]
        public string TarjetaCredito { get; set; }
        [Url]
        public string Url { get; set; }

        //Servicio para evitar la Referencia Circular
    }
}
