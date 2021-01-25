using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLibrosAPI.Entidades;

namespace WebLibrosAPI.Contexto
{
    public class ApplicationDbContext:DbContext
    {
        //Creamos el Constructor de la clase
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        //DBSet que representan tables en SQLSERVER

        //Generar una Migracion => Mapping con la Consola de PackNugget
        //Comandos=> Add-Migration "Nombre de esa migracion" para identifarlo
        //Add-Migration Name
        //Update-Database
        //Cada vez que agregamos una Tabla, ponemos Add-Migration "Nombre" =>Mapping
        //Para persistir los cambios en la DATABASE usamos Update-Databse
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
