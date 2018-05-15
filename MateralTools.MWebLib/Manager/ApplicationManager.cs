using MateralTools.MConvert;
using MateralTools.MVerify;
using MateralTools.MWebLib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MateralTools.MWebLib
{
    /// <summary>
    /// 应用程序管理器
    /// </summary>
    public class ApplicationManager
    {
        /// <summary>
        /// 登录用户信息Cookie名称
        /// </summary>
        public const string LoginUserInfoCooikeName = "LOGINUSERINFO";
        /// <summary>
        /// 登录用户参数名称
        /// </summary>
        private const string LoginUserIDParamName = "LoginUserID";
        /// <summary>
        /// Token参数名称
        /// </summary>
        private const string TokenParamName = "Token";
        /// <summary>
        /// 获取登录用户参数
        /// </summary>
        /// <param name="context">Action连接对象</param>
        /// <returns></returns>
        public static LoginUserModel GetLoginUserParams(ActionExecutingContext context)
        {
            LoginUserModel loginUserM = GetLoginUserParamsByHttpContext(context.HttpContext);
            if (loginUserM.UserID == Guid.Empty && loginUserM.Token.MIsNullOrEmpty())
            {
                KeyValuePair<string, object>[] actionParams = context.ActionArguments.ToArray();
                if (actionParams != null && actionParams.Length > 0)
                {
                    object obj = actionParams[0].Value;
                    Type objType = obj.GetType();
                    Type iVerificationLoginType = objType.GetInterface(nameof(IVerificationLoginModel));
                    if (iVerificationLoginType != null)
                    {
                        IVerificationLoginModel loginM = (IVerificationLoginModel)obj;
                        loginUserM.UserID = loginM.LoginUserID;
                        loginUserM.Token = loginM.Token;
                    }
                }
            }
            return loginUserM;
        }
        /// <summary>
        /// 从HTTPContext获取登录用户参数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static LoginUserModel GetLoginUserParamsByHttpContext(HttpContext context)
        {
            LoginUserModel loginUserM = new LoginUserModel();
            if (context != null)
            {
                HttpRequest request = context.Request;
                //先查找url参数
                string query = request.QueryString.Value;
                if (!query.MIsNullOrEmpty())
                {
                    string[] paras = query.TrimStart('?').Split('&');
                    string[] temp;
                    foreach (string para in paras)
                    {
                        temp = para.Split('=');
                        if (temp[0] == LoginUserIDParamName && !temp[1].MIsNullOrEmpty())
                        {
                            loginUserM.UserID = Guid.Parse(temp[1]);
                        }
                        else if (temp[0] == TokenParamName && !temp[1].MIsNullOrEmpty())
                        {
                            loginUserM.Token = temp[1];
                        }
                    }
                }
                if (loginUserM.UserID == Guid.Empty && loginUserM.Token.MIsNullOrEmpty())
                {
                    //然后查找Cookies
                    string loginUserInfo = request.Cookies[LoginUserInfoCooikeName];
                    if (!loginUserInfo.MIsNullOrEmpty())
                    {
                        loginUserM = loginUserInfo.MJsonToObject<LoginUserModel>();
                    }
                }
            }
            return loginUserM;
        }
    }
}
