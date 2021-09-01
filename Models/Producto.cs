using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Clase2DatabaseFirst.Models
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Marca {get;set;}
        public double Precio { get; set; } 
        public int? CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
