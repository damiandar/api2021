using System.Collections.Generic;
using System.Linq;
using Clase2DatabaseFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Clase2DatabaseFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController:ControllerBase
    {
        private readonly ComercioDbContext db;

        public ProductosController(ComercioDbContext dbContext){
            db=dbContext;
        }
        [HttpGet]
        public ActionResult <List <Producto>> GetAll(){
            return db.Productos 
            .ToList();
        } 

        [HttpGet("{id}")]
        public ActionResult <Producto> MostrarPorId(int id){
            var prodbuscado= db.Productos.Find(id);
            if(prodbuscado ==null)
            {
                return NotFound("El producto no fue encontrado con ese id: " +id );
            }
            return prodbuscado;
        }
             
        [HttpGet("busqueda/{nombre}")]
        public ActionResult <Producto> MostrarPorNombre(string nombre){
            var prodbuscado= db.Productos.Where(x=> x.Nombre==nombre).FirstOrDefault();
            if(prodbuscado ==null)
            {
                return NotFound("El producto no fue encontrado con ese nombre: " +nombre );
            }
            return prodbuscado;
        }

        [HttpGet("busquedaporvalor/{precio:double}")]
        public ActionResult <List<Producto>> MostrarPorNombre(double precio){
            var ListaPorPrecio= db.Productos.Where(x=> x.Precio>precio).ToList();
            if(ListaPorPrecio ==null || ListaPorPrecio.Count()==0)
            {
                return NotFound("No hay productos con precio mayor a " + precio );
            }
            return ListaPorPrecio;
        }

        [HttpGet("/api/Mensajes/{Numero:int:min(1000)}")]
        public ActionResult MostrarMensaje(int Numero){
            return Ok("Este es el numero: " + Numero );
        }

        [HttpGet("/api/Mensajes/{Mensaje:minlength(2)}")]
        public ActionResult MostrarMensaje(string Mensaje){
            return Ok("Este es el mensaje: " + Mensaje );
        }

        [HttpGet("/api/Avisos/{nro:int?}")]
        public ActionResult MostrarAviso(int nro=1033){
            return Ok("Este es el aviso: " + nro);
        }

         [HttpGet("/api/Llamadas/{nro:int=999}")]
        public ActionResult MostrarAviso2(int nro){
            return Ok("Este es el aviso: " + nro);
        }

        [HttpGet("busquedaporcategoria")]
        public ActionResult MostrarProductosPorCategoria(int categoriaId){
            return Ok("estos son los productos con este id de categoria:" + categoriaId);
        }


    
    }
}