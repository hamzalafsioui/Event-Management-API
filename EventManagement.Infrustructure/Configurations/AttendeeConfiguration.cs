using EventManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrustructure.Configurations
{
	public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
	{
		public void Configure(EntityTypeBuilder<Attendee> builder)
		{
			throw new NotImplementedException();
		}
	}

}
