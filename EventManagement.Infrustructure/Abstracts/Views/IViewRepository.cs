using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure.Abstracts.IViewRepository
{
	public interface IViewRepository<T> : IGenericRepositoryAsync<T> where T : class
	{
	}
}
