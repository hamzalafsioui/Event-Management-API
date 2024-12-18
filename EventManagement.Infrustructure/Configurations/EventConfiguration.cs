﻿using EventManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrustructure.Configurations
{
	public class EventConfiguration : IEntityTypeConfiguration<Event>
	{
		public void Configure(EntityTypeBuilder<Event> builder)
		{
			builder.HasKey(e => e.EventId);
			builder.Property(e => e.EventId)
				.ValueGeneratedOnAdd();

			builder.Property(e => e.Location)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(e => e.Title)
				.HasMaxLength(80)
				.IsRequired();

			//builder
			//	.Property(u => u.CreatedAt)
			//	.ValueGeneratedOnAdd()
			//	.HasDefaultValueSql("GETUTCDATE()");

			//builder
			//	.Property(u => u.UpdatedAt)
			//	.ValueGeneratedOnUpdate()
			//	.HasDefaultValueSql("GETUTCDATE()");

			builder.HasIndex(e => e.StartTime)
				.HasDatabaseName("IX_Events_StartTime");

			builder.HasIndex(e => e.EndTime)
				.HasDatabaseName("IX_Events_EndTime");

			builder.HasIndex(e => e.CategoryId)
				.HasDatabaseName("IX_Events_CategoryId");

			builder
				.HasOne(e => e.Category)
				.WithMany(c => c.Events)
				.HasForeignKey(e => e.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(e => e.Creator)
				.WithMany(u => u.CreatedEvents)
				.HasForeignKey(e => e.CreatorId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasQueryFilter(x => x.Status == EventStatus.Actived);
			builder.ToTable("Events");

		}
	}

}
