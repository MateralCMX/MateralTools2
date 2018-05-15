using Microsoft.AspNetCore.Mvc;

namespace MateralTools.MWebLib.Controllers
{
    /// <summary>
    /// 主控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
