using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataLayer.ModelConfiguration
{
    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("nvarchar(50)");
            builder.Property(e => e.Surname).HasColumnType("nvarchar(70)");
            builder.Property(e => e.MiddleName).HasColumnType("nvarchar(50)");
            builder.Property(c => c.MiddleName).HasColumnName("Middle name");
            builder.Property(c => c.GetJob).HasColumnName("Date of employment");
            builder.Property(c => c.GetJob).HasDefaultValue(DateTime.Now);
            builder.HasOne(c => c.Company).WithMany(c => c.Employees);
        }
    }
}
