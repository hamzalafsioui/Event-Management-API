namespace EventManagement.Data.Entities
{
	public class Category
	{

		public int CategoryId { get; set; }
		public required string Name { get; set; }
		public string Description { get; set; }

		public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
	}

}
