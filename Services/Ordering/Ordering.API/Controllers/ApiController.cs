﻿using Microsoft.AspNetCore.Mvc;

namespace Ordering.API.Controllers
{

    [ApiVersion("1")]
    [Route("api/v1/{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController:ControllerBase
    {
    }
}
