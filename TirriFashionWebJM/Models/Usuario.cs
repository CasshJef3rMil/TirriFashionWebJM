using System;
using System.Collections.Generic;

namespace TirriFashionWebJM.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Catalogos = new HashSet<Catalogo>();
            Reseñas = new HashSet<Reseña>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int? Edad { get; set; }
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Contraseña { get; set; }

        public virtual ICollection<Catalogo> Catalogos { get; set; }
        public virtual ICollection<Reseña> Reseñas { get; set; }
    }
}
