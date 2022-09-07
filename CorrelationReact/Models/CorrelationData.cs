namespace CorrelationReact.Models
{
	public class CorrelationData
	{
		public SerieData? UsdCad { get; set; }
		public SerieData? Corra { get; set; }
		public string? PearsonCoeff { get; set; }

		public string? Error { get; set; }
	}
}
