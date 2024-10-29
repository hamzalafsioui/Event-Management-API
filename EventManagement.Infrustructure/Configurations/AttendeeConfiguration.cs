using EventManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrustructure.Configurations
{
	public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
	{
		public void Configure(EntityTypeBuilder<Attendee> builder)
		{
			builder.HasKey(a => new { a.EventId, a.UserId });

			builder
				.HasOne(a => a.Event)
				.WithMany(a => a.Attendees)
				.HasForeignKey(a => a.EventId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(a => a.User)
				.WithMany(x => x.AttendingEvents)
				.HasForeignKey(a => a.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			//builder
			//	.Property(a => a.Status)
			//	.HasConversion(v => v.ToString(),  // how data will be store in DB
			//	v => (RSVPStatus)Enum.Parse(typeof(RSVPStatus), v))  // how data will be retrieve from DB to Enum
			//	.HasMaxLength(30);



			builder.HasIndex(a => new { a.EventId, a.UserId })
				   .HasDatabaseName("IX_Attendees_EventId_UserId");

			//builder.Property(a => a.CreatedAt)
			//	.ValueGeneratedOnAdd()
			//	.HasDefaultValueSql("GETUTCDATE()");
			//builder.Property(a => a.UpdatedAt)
			//	.ValueGeneratedOnUpdate()
			//	.HasDefaultValueSql("GETUTCDATE()");

			builder.ToTable("Attendees");
		}
	}

}
