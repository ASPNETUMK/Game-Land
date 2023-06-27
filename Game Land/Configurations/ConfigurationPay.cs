
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TNAI.Model.Entities;
using Game_Land.Entities;

namespace TNAI.Model.Configurations
{
    internal class ConfigurationPay:IEntityTypeConfiguration<pay>
    {

        public void Configure(EntityTypeBuilder<pay> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Number).HasMaxLength(500);
            builder.Property(x => x.time).HasMaxLength(500);
            builder.Property(x => x.id_User).HasMaxLength(500);
           
        }
    }
}
