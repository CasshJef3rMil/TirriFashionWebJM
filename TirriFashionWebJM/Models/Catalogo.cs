using System;
using System.Collections.Generic;

namespace TirriFashionWebJM.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            Reseñas = new HashSet<Reseña>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public byte[]? Imagen { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? AñoFabricacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }

        public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Reseña> Reseñas { get; set; }
    }
}
