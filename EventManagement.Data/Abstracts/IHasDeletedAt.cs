namespace EventManagement.Data.Abstracts
{
	public interface IHasDeletedAt
	{
		DateTime? DeletedAt { get; set; }
	}
}
