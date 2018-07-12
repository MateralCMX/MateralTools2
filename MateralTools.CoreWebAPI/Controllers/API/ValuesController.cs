using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MateralTools.MResult;

namespace MateralTools.CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public DataResult<string> GetValue()
        {
            return DataResult<string>.Success("Materal", "获取成功");
        }
    }
}
