using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El Apellido de usuario es requerida.")]
        public string Apellido { get; set; } = null!;
        [Required(ErrorMessage = "La edad del usuario es requerida.")]
        public int? Edad { get; set; }
        [Required(ErrorMessage = "El email del usuario es requerido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "EL Numero de telefono del usuario es requerido.")]
        [Display(Name = "Número de Teléfono")]
        public string? Telefono { get; set; }


        [Required(ErrorMessage = "La contraseña es requerida.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        public string Contraseña { get; set; } = null!;

        public string Rol { get; set; } = null!;
        public byte? Estatus { get; set; }


        [NotMapped] // Esta propiedad no será mapeada a la base de datos
        [Compare("Contraseña", ErrorMessage = "La confirmación de contraseña no coincide.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme su contraseña.")]
        public string ConfirmarPassword { get; set; }

        [NotMapped]
        public int Take { get; set; }
        public virtual ICollection<Catalogo> Catalogos { get; set; }
        public virtual ICollection<Reseña> Reseñas { get; set; }
    }
}
