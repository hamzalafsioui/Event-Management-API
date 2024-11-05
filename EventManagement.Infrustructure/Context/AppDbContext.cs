using EventManagement.Data.Abstracts;
using EventManagement.Data.Entities;
using EventManagement.Data.Entities.Identity;
using EventManagement.Infrustructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace EventManagement.Infrustructure.Context
{
	public class AppDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		public AppDbContext()
		{

		}
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{

		}


		//public DbSet<User> Users { get; set; } // is Included in IdentityDbContext via the IdentityUser
		public DbSet<Event> Events { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Attendee> Attendees { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }


		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entities = ChangeTracker.Entries();
			foreach (var entry in entities)
			{

				if (entry.State == EntityState.Modified)
				{
					if (entry.Entity is IHasUpdatedAt updatedEntity)
					{
						updatedEntity.UpdatedAt = DateTime.UtcNow;
					}

				}
				if (entry.State == EntityState.Added)
				{
					if (entry.Entity is IHasCreatedAt createdEntity)
					{
						createdEntity.CreatedAt = DateTime.UtcNow;
					}
				}

				// Handle DeletedAt for soft delete
				if (entry.State == EntityState.Deleted)
				{
					// If the entity implements IHasDeletedAt, perform soft delete instead of actual deletion
					if (entry.Entity is IHasDeletedAt deletedEntity)
					{
						// Convert the delete operation into an update
						entry.State = EntityState.Modified;
						deletedEntity.DeletedAt = DateTime.UtcNow;


						if (entry.Entity is User userEntity)
						{
							userEntity.IsDeleted = true;
						}
					}
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
			modelBuilder.ApplyConfiguration(new AttendeeConfiguration()); //explicitly applying
			modelBuilder.ApplyConfiguration(new CategoryConfiguration()); //explicitly applying
			modelBuilder.ApplyConfiguration(new EventConfiguration()); // explicitly applying
																	   //	modelBuilder.ApplyConfiguration(new UserConfiguration()); // explicitly applying
			modelBuilder.ApplyConfiguration(new CommentConfiguration()); // explicitly applying

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(Attendee).Assembly);

		}



	}
}
