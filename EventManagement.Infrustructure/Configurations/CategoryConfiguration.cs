using EventManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrustructure.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasKey(c => c.CategoryId);
			builder.Property(c => c.CategoryId)
				.ValueGeneratedOnAdd();



			builder.ToTable("Categories");
		}
	}

}
