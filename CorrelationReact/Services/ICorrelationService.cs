using CorrelationReact.Models;

namespace CorrelationReact.Services
{
	public interface ICorrelationService
	{
		Task<CorrelationData> GetDataAsync( DateTime startDate, DateTime endDate );
	}
}
