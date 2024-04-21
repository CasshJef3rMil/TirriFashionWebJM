using System;
using System.Collections.Generic;

namespace TirriFashionWebJM.Models
{
    public partial class Reseña
    {
        public int Id { get; set; }
        public int IdCatalogo { get; set; }
        public int IdUsuario { get; set; }
        public string? Comentario { get; set; }
        public string Calificacion { get; set; } = null!;

        public virtual Catalogo IdCatalogoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
