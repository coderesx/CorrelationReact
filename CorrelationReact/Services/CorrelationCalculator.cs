namespace CorrelationReact.Services
{
	public class CorrelationCalculator : ICorrelationCalculator
	{
        public double Calculate( IList<double> xs, IList<double> ys )
        {
            if( xs.Count != ys.Count )
            {
                throw new ArgumentException( "Values must be the same length" );
            }

            double sumX = 0d;
            double sumX2 = 0d;
            double sumY = 0d;
            double sumY2 = 0d;
            double sumXY = 0d;
            var n = xs.Count;

            for( var i = 0; i < n; ++i )
            {
                double x = xs[i];
                double y = ys[i];

                sumX += x;
                sumX2 += x * x;
                sumY += y;
                sumY2 += y * y;
                sumXY += x * y;
            }

            double stdX = Math.Sqrt( sumX2 / n - sumX * sumX / n / n );
            double stdY = Math.Sqrt( sumY2 / n - sumY * sumY / n / n );
            double covariance = ( sumXY / n - sumX * sumY / n / n );

            return covariance / stdX / stdY;
        }
    }
}
