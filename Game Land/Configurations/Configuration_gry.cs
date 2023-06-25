using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI.Model.Entities;

namespace TNAI.Model.Configurations
{
    internal class Configuration_gry:IEntityTypeConfiguration<Gry>
    {

        public void Configure(EntityTypeBuilder<Gry> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Key).HasMaxLength(500);
            builder.Property(x => x.Url_image).HasMaxLength(500);
        }
    }
}
