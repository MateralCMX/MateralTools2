using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MateralTools.MResult;

namespace Materal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public MResultModel<string> GetValue()
        {
            return MResultModel<string>.GetSuccessResultM("Materal", "获取成功");
        }
    }
}
