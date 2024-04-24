using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TirriFashionWebJM.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Catalogos = new HashSet<Catalogo>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "EL Nombre es requerido.")]
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Catalogo> Catalogos { get; set; }
    }
}
