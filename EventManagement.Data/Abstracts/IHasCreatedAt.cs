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
	public interface IHasDeletedAt
	{
		DateTime? DeletedAt { get; set; }
	}
	public interface IHasExpiredAt
	{
		DateTime ExpiredAt { get; set; }
	}
}
