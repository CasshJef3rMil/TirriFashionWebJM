using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TirriFashionWebJM.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            Reseñas = new HashSet<Reseña>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "EL Usuario es requerido.")]
        public string Nombre { get; set; } = null!;
        public byte[]? Imagen { get; set; }
        [Required(ErrorMessage = "la descripcion es obligatoria.")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "EL Usuario es requerido.")]
        [Display(Name = "Año de fabricacion")]
        public DateTime? AñoFabricacion { get; set; }
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        [Display(Name = "Categoria")]
        public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
        [Display(Name = "Usuario")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Reseña> Reseñas { get; set; }
    }
}
