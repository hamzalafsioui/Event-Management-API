using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Seeder
{
	public static class CategorySeeder
	{
		public static async Task SeedAsync(ICategoryRepository _categoryService)
		{
			var categories = new List<Category>()
			{
				new Category() { Name = "Conferences", Description = "Professional gatherings for networking, learning, and discussing industry topics." },
				new Category() { Name = "Workshops", Description = "Interactive sessions focused on skill development and hands-on learning." },
				new Category() { Name = "Seminars", Description = "Educational meetings for discussing specialized topics or subjects." },
				new Category() { Name = "Webinars", Description = "Online seminars conducted over the internet." },
				new Category() { Name = "Meetups", Description = "Informal gatherings for people with shared interests." },
				new Category() { Name = "Concerts", Description = "Live music performances." },
				new Category() { Name = "Festivals", Description = "Large public celebrations with various activities, often including music, food, and entertainment." },
				new Category() { Name = "Sports", Description = "Competitive athletic events and games." },
				new Category() { Name = "Networking", Description = "Events focused on building professional connections and relationships." },
				new Category() { Name = "Trade Shows", Description = "Exhibitions where companies showcase and demonstrate their products and services." },
				new Category() { Name = "Charity", Description = "Events organized to raise funds or awareness for charitable causes." },
				new Category() { Name = "Parties", Description = "Social gatherings for celebration and entertainment." },
				new Category() { Name = "Classes", Description = "Educational sessions or courses on various topics." },
				new Category() { Name = "Hackathons", Description = "Events where programmers and developers collaborate intensively on software projects." },
				new Category() { Name = "Exhibitions", Description = "Public displays of art, products, or information." },
				new Category() { Name = "Religious", Description = "Events related to religious practices and gatherings." },
				new Category() { Name = "Fundraisers", Description = "Events organized to raise money for specific causes or organizations." },
				new Category() { Name = "Community", Description = "Local events that engage and involve the community." },
				new Category() { Name = "Family", Description = "Events designed for family participation and enjoyment." },
				new Category() { Name = "Other", Description = "Events that do not fit into the above categories." }

			};
			var existingCategories = await  _categoryService.GetTableNoTracking().AnyAsync();
			if (!existingCategories)
			{
				await _categoryService.AddRangeAsync(categories);
			}
		}
	}
}
