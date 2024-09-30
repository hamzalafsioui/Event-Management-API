using EventManagement.Data.Entities;

namespace EventManagement.Infrustructure.Data.Helper
{
	static public class DataBaseHelper
	{
		static public List<Category> loadCategories()
		{
			return new List<Category>
			{
				new Category { CategoryId = 1, Name = "Conferences", Description = "Professional gatherings for networking, learning, and discussing industry topics." },
				new Category { CategoryId = 2, Name = "Workshops", Description = "Interactive sessions focused on skill development and hands-on learning." },
				new Category { CategoryId = 3, Name = "Seminars", Description = "Educational meetings for discussing specialized topics or subjects." },
				new Category { CategoryId = 4, Name = "Webinars", Description = "Online seminars conducted over the internet." },
				new Category { CategoryId = 5, Name = "Meetups", Description = "Informal gatherings for people with shared interests." },
				new Category { CategoryId = 6, Name = "Concerts", Description = "Live music performances." },
				new Category { CategoryId = 7, Name = "Festivals", Description = "Large public celebrations with various activities, often including music, food, and entertainment." },
				new Category { CategoryId = 8, Name = "Sports", Description = "Competitive athletic events and games." },
				new Category { CategoryId = 9, Name = "Networking", Description = "Events focused on building professional connections and relationships." },
				new Category { CategoryId = 10, Name = "Trade Shows", Description = "Exhibitions where companies showcase and demonstrate their products and services." },
				new Category { CategoryId = 11, Name = "Charity", Description = "Events organized to raise funds or awareness for charitable causes." },
				new Category { CategoryId = 12, Name = "Parties", Description = "Social gatherings for celebration and entertainment." },
				new Category { CategoryId = 13, Name = "Classes", Description = "Educational sessions or courses on various topics." },
				new Category { CategoryId = 14, Name = "Hackathons", Description = "Events where programmers and developers collaborate intensively on software projects." },
				new Category { CategoryId = 15, Name = "Exhibitions", Description = "Public displays of art, products, or information." },
				new Category { CategoryId = 16, Name = "Religious", Description = "Events related to religious practices and gatherings." },
				new Category { CategoryId = 17, Name = "Fundraisers", Description = "Events organized to raise money for specific causes or organizations." },
				new Category { CategoryId = 18, Name = "Community", Description = "Local events that engage and involve the community." },
				new Category { CategoryId = 19, Name = "Family", Description = "Events designed for family participation and enjoyment." },
				new Category { CategoryId = 20, Name = "Other", Description = "Events that do not fit into the above categories." }
			};
		}
	}

}

