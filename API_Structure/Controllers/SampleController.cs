using API_Structure.X_BAL.DomainModels.Models;
using API_Structure.X_BAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_Structure.Controllers
{
    [Route("api/Sample")]
    [ApiController]
    public class SampleController : Controller
    {
        public readonly IConfiguration _configuration;
        public readonly ISampleService _sampleService;
        public SampleController(IConfiguration configuration, ISampleService sampleService)
        {
            _configuration = configuration;
            _sampleService = sampleService;
        }

        [Route("Sample")]
        [HttpPost]
        public JsonResponse Sample(int ID)
        {
            return _sampleService.Sample(ID);
        }
    }
}
