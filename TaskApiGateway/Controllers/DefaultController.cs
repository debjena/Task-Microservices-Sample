using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DefaultController : ControllerBase
    {
        private readonly iapiaggregator _agg;
        public DefaultController(iapiaggregator agg)
        {
            _agg = agg;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var o = await _agg.GetTasks();
                return Ok(o);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

    }
}