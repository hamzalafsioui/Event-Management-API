namespace EventManagement.Data.Abstracts
{
	public interface IHasCreatedAt
	{
		DateTime CreatedAt { get; set; }
	}
	public interface IHasUpdatedAt
	{
		DateTime UpdatedAt { get; set; }
	}
}
