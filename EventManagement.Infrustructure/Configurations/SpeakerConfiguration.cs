using EventManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrustructure.Configurations
{
	public class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
	{
		public void Configure(EntityTypeBuilder<Speaker> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasOne(x => x.User)
				.WithMany()
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(x => x.UserId)
			   .IsUnique()
			   .HasDatabaseName("IX_Speaker_UserId");


			builder.ToTable("Speakers");
		}
	}
}
