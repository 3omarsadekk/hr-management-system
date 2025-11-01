using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Infrastructure.Configurations;

/// <summary>
/// Sample entity configuration - Replace with your actual entity configurations
/// </summary>
public class SampleEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        // Example configuration
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
