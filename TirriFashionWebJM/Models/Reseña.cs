using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TirriFashionWebJM.Models
{
    public partial class Reseña
    {
        public int Id { get; set; }

        [Display(Name = "Catalogo")]
        public int IdCatalogo { get; set; }
        [Required(ErrorMessage = "EL Usuario es requerido.")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }
        
        public string? Comentario { get; set; }
        [Required(ErrorMessage = "La calificacion es obligatoria.")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "La calificacion debe tener entre 1 y 2 caracteres.")]
        public string Calificacion { get; set; } = null!;

        [Display(Name = "Catalogo")]
        public virtual Catalogo IdCatalogoNavigation { get; set; } = null!;
        [Display(Name = "Usuario")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
