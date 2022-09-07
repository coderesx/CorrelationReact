using CorrelationReact.Models;

namespace CorrelationReact.Services
{
	public interface ICorrelationCalculator
	{
		public double Calculate( IList<double> xs, IList<double> ys );
	}
}
