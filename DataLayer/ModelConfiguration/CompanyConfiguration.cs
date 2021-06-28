using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ModelConfiguration
{
    public class CompanyConfiguration: IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasIndex(c => c.Name).IsUnique();
            builder.HasMany(c => c.Employees).WithOne(c => c.Company).HasForeignKey(c => c.CompanyId);
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)");
            builder.Property(c => c.FormOfOrganization).HasColumnName("Form Of Organization");
        }
    }
}
