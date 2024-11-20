using EventManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrustructure.Configurations
{
	public class SpeakerEventConfiguration : IEntityTypeConfiguration<SpeakerEvent>
	{
		public void Configure(EntityTypeBuilder<SpeakerEvent> builder)
		{
			builder.HasKey(x => new
			{
				x.EventId,
				x.SpeakerId,
			});

			builder.HasOne(x => x.Event)
				.WithMany(x => x.SpeakerEvents)
				.HasForeignKey(x => x.EventId);

			builder.HasOne(x => x.Speaker)
				.WithMany(x => x.SpeakerEvents)
				.HasForeignKey(x => x.SpeakerId);

			builder.ToTable("SpeakerEvents");

		}
	}
}
