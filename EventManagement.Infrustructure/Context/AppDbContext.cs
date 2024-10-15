using EventManagement.Data.Abstracts;
using EventManagement.Data.Entities;
using EventManagement.Data.Helper;
using Microsoft.EntityFrameworkCore;


namespace EventManagement.Infrustructure.Context
{
	public class AppDbContext : DbContext
	{


		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{

		}


		public DbSet<User> Users { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Attendee> Attendees { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entities = ChangeTracker.Entries();
			foreach (var entry in entities)
			{
				
				if(entry.State == EntityState.Modified)
				{
					if(entry.Entity is IHasUpdatedAt updatedEntity)
					{
						updatedEntity.UpdatedAt = DateTime.UtcNow;
					}
					
				}
				if(entry.State == EntityState.Added)
				{
					if(entry.Entity is IHasCreatedAt createdEntity)
					{
						createdEntity.CreatedAt = DateTime.UtcNow;
					}
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(Attendee).Assembly);

		}



	}
}
