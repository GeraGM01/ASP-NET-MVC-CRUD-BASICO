using CrudNet8MVC.Datos;
using CrudNet8MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudNet8MVC.Controllers
{
    public class InicioController : Controller
    {
        //Para acceder a la base de datos creo un atributo privado de lectira, es para el contexto
        //Internamente lo que hago es llamar al dbcontext para poder hacer uso de los modelos que esten dentro del contexto
        private readonly ApplicationDbContext _contexto;

        //Hago la inyeccion de dependencias con mi metodo constructor
        public InicioController(ApplicationDbContext context)
        {
            _contexto = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Aqui ya podemos acceder al contexto y por ende a todas mis tablas de la base de datos
            return View(await _contexto.Contacto.ToListAsync()); //Aqui enviamos todos los registros de la tabla Contacto a una lista
        }


        //Metodo que se va a ejecutar cuando se haga click en Nuevo Usuario
        //No recibe parametros debido a que solo nos va a retornar a la otra pagina del formulario 
        // ---- Lo haremos de tipo GET ya que solo va a mostrar una vista  ----
        [HttpGet]
        public IActionResult Crear()
        {
            return View(); //Retornamos a la vista del formulario 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            //Validamos que el modelo este correcto, es decir que se cumpla las restricciones definidas en el modelo
            if (ModelState.IsValid)
            {
                //Asignamos fecha de creacion a nivel de codigo
                contacto.FechaCreacion = DateTime.Now;


                //Usamos el contexto para acceder al modelo contacto
                _contexto.Contacto.Add(contacto);
                //Guardamos los cambios en la base de datos
                await _contexto.SaveChangesAsync(); 
                //retornamos al Index
                return RedirectToAction(nameof(Index));

            }

            //Si no pasa las validaciones definidas en nuestro Modelo entonces retornamos a la misma vista, como quien dice nos quedamos en la pagina del formulario
            return View(); //Retornamos a la vista del formulario 
        }


        //Recibe el Id del registro a editar
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var contacto = _contexto.Contacto.Find(id); //Buscamos el id dentro del contexto 

            //Puede que no exista un registro con ese ID,entonces retornamos que no se encuentra
            if(contacto == null)
            {
                NotFound();
            }


            return View(contacto); //Si se encontro el id debemos retornar la vista y enviarle el modelo contacto 
        }


        //Recibe el Id del registro a editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contacto contacto)
        {
            if(ModelState.IsValid)
            {
                //Usamos el contexto para acceder al modelo contacto y actualizarlo
                _contexto.Contacto.Update(contacto);
                //Guardamos los cambios en la base de datos
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); //retornamos al Index
            }
            return View(); //En caso de que no sea valido el contacto, regresamos al index
        }


        //Recibe el Id del registro para ver sus detalles
        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = _contexto.Contacto.Find(id); //Buscamos el id dentro del contexto 

            //Puede que no exista un registro con ese ID,entonces retornamos que no se encuentra
            if (contacto == null)
            {
                NotFound();
            }


            return View(contacto); //Si se encontro el id debemos retornar la vista y enviarle el modelo contacto 
        }


        //Recibe el Id del registro para saber cual registro se va a borrar
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = _contexto.Contacto.Find(id); //Buscamos el id dentro del contexto 

            //Puede que no exista un registro con ese ID,entonces retornamos que no se encuentra
            if (contacto == null)
            {
                NotFound();
            }


            return View(contacto); //Si se encontro el id debemos retornar la vista y enviarle el modelo contacto 
        }

        //Recibe el Id del registro a eliminar
        [HttpPost, ActionName("Borrar")] //Cambiar el nombre del metodo pero que siga haciendo llamado al nombre original que le pasamos aqui
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarContacto(int? id)
        {
            //Buscamos ese contacto pot id
            var contacto = _contexto.Contacto.Find(id);

            if (contacto == null)
            {
                return View();
            }

            //Borrado
            _contexto.Contacto.Remove(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
