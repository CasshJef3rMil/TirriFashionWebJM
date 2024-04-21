using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TirriFashionWebJM.Models
{
    public partial class TirriFashionWebJMContext : DbContext
    {
        public TirriFashionWebJMContext()
        {
        }

        public TirriFashionWebJMContext(DbContextOptions<TirriFashionWebJMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catalogo> Catalogos { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Reseña> Reseñas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(" Data Source=.;Initial Catalog=TirriFashionWebJM;Integrated Security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalogo>(entity =>
            {
                entity.ToTable("Catalogo");

                entity.Property(e => e.AñoFabricacion).HasColumnType("date");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Catalogos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Catalogo__IdCate__2A4B4B5E");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Catalogos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Catalogo__IdUsua__29572725");
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reseña>(entity =>
            {
                entity.Property(e => e.Calificacion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Comentario)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCatalogoNavigation)
                    .WithMany(p => p.Reseñas)
                    .HasForeignKey(d => d.IdCatalogo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reseñas__IdCatal__2D27B809");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reseñas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reseñas__IdUsuar__2E1BDC42");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D1053426347A5A")
                    .IsUnique();

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rol).HasMaxLength(20);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
