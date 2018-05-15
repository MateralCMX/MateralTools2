using MateralTools.MVerify;
using MateralTools.MWebLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Reflection;

namespace MateralTools.MWebLib
{
    /// <summary>
    /// 验证登录过滤器
    /// </summary>
    public class VerificationLoginAttribute : IActionFilter
    {
        /// <summary>
        /// Action执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Type ControllerType = context.Controller.GetType();
            if (ControllerType.GetCustomAttributes(typeof(NotVerificationLoginAttribute), false).Length == 0)
            {
                string ActionName = context.ActionDescriptor.RouteValues["Action"];
                MethodInfo mi = ControllerType.GetMethod(ActionName);
                if (mi != null && mi.GetCustomAttribute(typeof(NotVerificationLoginAttribute), false) == null)
                {
                    LoginUserModel loginUserM = ApplicationManager.GetLoginUserParams(context);
                    if (loginUserM.UserID != Guid.Empty && !loginUserM.Token.MIsNullOrEmpty())
                    {
                        //TokenBLL tokenBLL = new TokenBLL();
                        //bool resM = tokenBLL.VerificationToken(loginUserM.UserID, loginUserM.Token, TokenTypesEnum.Login);
                        //if (!resM)//Token过期或用户不存在
                        //{
                        //    if (mi.ReturnType == typeof(IActionResult))
                        //    {
                        //        context.Result = new RedirectResult("~/User/Login");
                        //    }
                        //    else
                        //    {
                        //        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        //    }
                        //}
                    }
                    else//未找到LoginUserID和Token
                    {
                        //if (mi.ReturnType == typeof(IActionResult))
                        //{
                        //    context.Result = new RedirectResult("~/User/Login");
                        //    //context.Result = new RedirectResult("~/Home/Error400");
                        //}
                        //else
                        //{
                        //    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        //}
                    }
                }
            }
        }
        /// <summary>
        /// Action结束后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
    /// <summary>
    /// 不进行登录验证
    /// </summary>
    public class NotVerificationLoginAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public NotVerificationLoginAttribute() { }
    }
}
