namespace EventManagement.Data.Abstracts
{
	public interface IHasExpiredAt
	{
		DateTime ExpiredAt { get; set; }
	}
}
