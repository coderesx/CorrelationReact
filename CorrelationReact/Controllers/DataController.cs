using CorrelationReact.Services;
using Microsoft.AspNetCore.Mvc;

namespace CorrelationReact.Controllers
{
	[ApiController]
	[Route( "[controller]" )]
	public class DataController : ControllerBase
	{
		private readonly ILogger<DataController> _logger;
		private readonly ICorrelationService _correlationService;

		public DataController( ILogger<DataController> logger, ICorrelationService correlationService )
		{
			_logger = logger;
			_correlationService = correlationService;
		}

		[HttpGet]
		public async Task<IActionResult> Get( DateTime startDate, DateTime endDate )
		{
			var result = await _correlationService.GetDataAsync( startDate, endDate );
			if( result.Error == null )
			{
				return Ok( result );
			}
			else
			{
				return BadRequest( result );
			}
		}
	}
}