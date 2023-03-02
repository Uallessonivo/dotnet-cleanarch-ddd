using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Domain.MenuAggregate.Entities;
using BuberDinner.Domain.MenuAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureMenuSectionsTable(builder);
    }

    private static void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("menus");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => MenuId.Create(value));

        builder.Property(m => m.Name).HasMaxLength(100);
        builder.Property(m => m.Description).HasMaxLength(100);
        builder.OwnsOne(m => m.AverageRating);
        builder.Property(m => m.HostId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => HostId.Create(value));
    }

    private static void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.Sections, sb =>
        {
            sb.ToTable("MenuSections");
            sb.WithOwner().HasForeignKey("MenuId");
            sb.HasKey("Id", "MenuId");
            sb.Property(s => s.Id).HasColumnName("MenuSectionId")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => MenuSectionId.Create(value));
            sb.Property(s => s.Name).HasMaxLength(100);
            sb.Property(s => s.Description).HasMaxLength(100);

            sb.OwnsMany(s => s.Items, ib =>
            {
                ib.ToTable("MenuItems");
                ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");
                ib.Property(i => i.Id)
                    .HasColumnName("MenuItemId")
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value, 
                        value => MenuItemId.Create(value));
                
                ib.Property(s => s.Name).HasMaxLength(100);
                ib.Property(s => s.Description).HasMaxLength(100);
            });
        });
    }
}