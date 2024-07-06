using CrudNet8MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudNet8MVC.Datos
{
    //Encargado de manejar todos los modelos
    public class ApplicationDbContext : DbContext
    {
        //Constructor para cargar la inyeccion de dependencias
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Agregar los modelos aqui debajo. (Cada modelo corresponde a una tabla en la BD)
        public DbSet<Contacto> Contacto { get; set; }
    }
}
