using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateralTools.MWebLib.Models
{
    /// <summary>
    /// 登录用户模型
    /// </summary>
    public class LoginUserModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// Token值
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
