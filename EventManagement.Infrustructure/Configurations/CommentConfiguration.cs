using EventManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrustructure.Configurations
{
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.HasKey(c => c.CommentId);

			builder.Property(c => c.CommentId)
				.ValueGeneratedOnAdd();

			builder.Property(c => c.Content)
				.HasMaxLength(300);

			builder.Property(c => c.Status)
				.HasConversion<int>();

			//builder
			//	.Property(u => u.CreatedAt)
			//	.ValueGeneratedOnAdd()
			//	.HasDefaultValueSql("GETUTCDATE()");

			//builder
			//	.Property(u => u.UpdatedAt)
			//	.ValueGeneratedOnUpdate()
			//	.HasDefaultValueSql("GETUTCDATE()");

			builder
				.HasOne(c => c.Event)
				.WithMany(e => e.Comments)
				.HasForeignKey(c => c.EventId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.User)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasQueryFilter(x => x.Status == Data.Helper.Enums.CommentStatusEnum.Active);
			builder.ToTable("Comments");
		}
	}

}
