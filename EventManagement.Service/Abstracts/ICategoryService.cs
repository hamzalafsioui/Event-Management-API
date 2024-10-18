namespace EventManagement.Service.Abstracts
{
	public interface ICategoryService
	{
		public Task<bool> IsCategoryIdExist(int categoryId);
	}
}
