using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudNet8MVC.Models
{
    //Este es un Modelo, Cada modelo corresponde a una tabla en la Base de Datos
    //Aqui vamos a ir agregando nuestros atributos de la tabla y sus respectivas restricciones.

    public class Contacto
    {
        [Key]  //Indicamos que sera una llave primaria, se puede omitir ya que c# por defecto detecta al iniciar por Id
        public int Id { get; set; } //LLave primaria de mi tabla contacto, sera autoincremental


        [Required(ErrorMessage ="El campo Nombre es obligatorio")] //Campo obligatorio, validacion del lado del servidor
        public string Nombre { get; set; }  //Atributo nombre de mi tabla contacto


        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        public string Telefono { get; set; }


        [Required(ErrorMessage = "El campo Celular es obligatorio")]
        public string Celular { get; set; }


        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string Email { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }  //A nivel interno para saber cuando se creo el registro

    }
}
