using CorrelationReact.Models;
using System.Text.Json;

namespace CorrelationReact.Services
{
	public class CorrelationService : ICorrelationService
	{
		private readonly HttpClient _httpClient;
		private readonly ICorrelationCalculator _correlationCalculator;
		const string ApiAddress = "https://www.bankofcanada.ca/valet/";

		public CorrelationService( HttpClient httpClient, ICorrelationCalculator correlationCalculator )
		{
			_httpClient = httpClient;
			_correlationCalculator = correlationCalculator;
		}

		public async Task<CorrelationData> GetDataAsync( DateTime startDate, DateTime endDate )
		{
			try
			{
				var usdCadValues = await GetSeriesDataAsync( startDate, endDate, "FXUSDCAD" );
				var corraValues = await GetSeriesDataAsync( startDate, endDate, "AVG.INTWO" );

				var data = new CorrelationData {
					UsdCad = new SerieData { High = usdCadValues.Max(), Low = usdCadValues.Min(), MeanAvg = Math.Round( usdCadValues.Average(), 4 ) },
					Corra = new SerieData { High = corraValues.Max(), Low = corraValues.Min(), MeanAvg = Math.Round( corraValues.Average(), 4 ) },
					PearsonCoeff = _correlationCalculator.Calculate( usdCadValues, corraValues ).ToString( "N4" )
				};

				return data;
			}
			catch(Exception ex )
			{
				return new CorrelationData {
					Error = ex.Message
				};
			}
		}

		private async Task<List<double>> GetSeriesDataAsync( DateTime startDate, DateTime endDate, string series )
		{
			var requestUri = $"{ApiAddress}observations/{series}/json?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";
			var httpResponseMessage = await _httpClient.GetAsync( requestUri );
			httpResponseMessage.EnsureSuccessStatusCode();

			using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

			try
			{
				var values = new List<double>();
				var doc = await JsonDocument.ParseAsync( contentStream );

				foreach( var obs in doc.RootElement.GetProperty( "observations" ).EnumerateArray() )
				{
					//var date = obs.GetProperty( "d" ).GetDateTime();
					var value = double.Parse( obs.GetProperty( series ).GetProperty( "v" ).GetString() );
					values.Add( value );
				}

				return values;
			}
			catch
			{
				throw new Exception( "Error parsing series data" );
			}
		}
	}
}
