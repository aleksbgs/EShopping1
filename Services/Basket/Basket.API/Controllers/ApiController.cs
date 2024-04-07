using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{

    [ApiVersion("1")]
    [Route("api/v1/{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController:ControllerBase
    {

    }
}
