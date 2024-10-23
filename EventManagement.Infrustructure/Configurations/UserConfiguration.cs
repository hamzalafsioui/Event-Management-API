using EventManagement.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrustructure.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);
			builder.Property(u => u.Id)
				.ValueGeneratedOnAdd();

			builder.HasIndex(u => u.UserName)
				.IsUnique()
				.HasDatabaseName("IX_Users_UserName");

			builder.Property(u => u.UserName)
				.HasMaxLength(50)
				.IsRequired();


			builder.Property(u => u.FirstName)
				.HasMaxLength(50)
				.IsRequired();
			builder.Property(u => u.LastName)
				.HasMaxLength(50)
				.IsRequired();

			// store Enum as string in DB
			//builder.Property(u => u.Role)
			//	   .HasConversion(v => v.ToString(),
			//		v => (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), v)
			//	    );

			// explicitly
			builder.Property(u => u.Role)
				.HasColumnType("int");

			builder
				.Property(u => u.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder
				.Property(u => u.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.HasIndex(u => u.Email)
				.IsUnique()
				.HasDatabaseName("IX_Users_Email");


			builder.ToTable("Users");
		}
	}

}
