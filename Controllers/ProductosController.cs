using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Clase2DatabaseFirst.Helpers;
using Clase2DatabaseFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clase2DatabaseFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController:ControllerBase
    {
        private readonly ComercioDbContext db;
        private readonly ILogger<Producto> logger;

        public ProductosController(ILogger<Producto> loggerC,ComercioDbContext dbContext){
            db=dbContext;
            logger=loggerC;

        }
        [HttpGet]
        public ActionResult <List <Producto>> GetAll(string Orden,int pagina,int tamanio){
           var ordenar=new Ordenador<Producto>();
          
            //return StatusCode(((int)System.Net.HttpStatusCode.BadGateway));
           /*var consulta= db.Productos.Select(x=> new Producto(){
                Id=x.Id,
                Nombre=x.Nombre,
                Marca=x.Marca,
                Precio=x.Precio,
                Categoria=x.Categoria
            });*/
            logger.LogInformation("hola");
            var consulta=db.Productos;
            var re= ordenar.Ordenar(consulta,Orden);
            
            var lista= ListaPaginada<Producto>.ToPagedList( re,pagina,tamanio);

            var metadata = new
            {
                lista.TotalCount,
                lista.PageSize,
                lista.CurrentPage,
                lista.TotalPages,
                lista.HasNext,
                lista.HasPrevious
            };
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));
            return Ok(lista);
        } 
        [HttpGet("search/{search}")]
        public async Task<ActionResult<List<Producto>>> Search(string name, int categoriaid)
        {
            try
            {
                var result =  db.Productos.Select(x=> new Producto(){
                Id=x.Id,
                Nombre=x.Nombre,
                Categoria=x.Categoria
                });


                if (result.Any())
                {
                    return Ok(result.ToListAsync());
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
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