using Inventarios.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios.AccesoDatos.Configuracion
{
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NumeroSerie).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.Precio).IsRequired();
            builder.Property(x => x.Costo).IsRequired();
            builder.Property(x => x.CategoriaId).IsRequired();
            builder.Property(x => x.MarcaId).IsRequired();
            builder.Property(x => x.ImagenUrl).IsRequired(false);
            builder.Property(x => x.PadreId).IsRequired(false);

            /* Relaciones */

            builder.HasOne(x => x.Categoria).WithMany() // Uno a Muchos
                .HasForeignKey(x => x.CategoriaId)
                //.OnDelete(DeleteBehavior.Cascade)
                .OnDelete(DeleteBehavior.NoAction); // no borra en Cascada

            builder.HasOne(x => x.Marca).WithMany() // Uno a Muchos
               .HasForeignKey(x => x.MarcaId)
               .OnDelete(DeleteBehavior.NoAction); // no borra en Cascada

            builder.HasOne(x => x.Padre).WithMany() // Uno a Muchos
               .HasForeignKey(x => x.PadreId)
               .OnDelete(DeleteBehavior.NoAction); // no borra en Cascada

        }
    }
}
