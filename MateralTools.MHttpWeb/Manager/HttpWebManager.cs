using MateralTools.Base.MTable;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace MateralTools.MHttpWeb
{
    /// <summary>
    /// Http管理器
    /// </summary>
    public class HttpWebManager
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="URL">URL地址</param>
        /// <param name="Params">参数</param>
        /// <param name="methodType">发送类型</param>
        /// <param name="paramType">参数类型</param>
        /// <param name="dataEncode">编码类型</param>
        /// <returns>返回结果</returns>
        public static string SendRequest(string URL, string Params, MethodTypeEnum methodType, ParamTypeEnum paramType, Encoding dataEncode = null)
        {
            if (dataEncode == null)
            {
                dataEncode = Encoding.UTF8;
            }
            HttpWebRequest webReq = GetHttpWebRequest(URL, Params, methodType, paramType, dataEncode);
            using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 获得Get方式的WeRequest
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public static HttpWebRequest GetGetHttpWebRequest(string URL, string Params)
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(URL + Params));
            webReq.Method = "GET";
            webReq.ContentType = "text/html;charset=UTF-8";
            return webReq;
        }
        /// <summary>
        /// 获得Post方式的WeRequest
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Params"></param>
        /// <param name="paramType"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        public static HttpWebRequest GetPostHttpWebRequest(string URL, string Params, ParamTypeEnum paramType, Encoding dataEncode)
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(URL));
            webReq.Method = "POST";
            switch (paramType)
            {
                case ParamTypeEnum.Text:
                    webReq.ContentType = "text/html;charset=UTF-8";
                    break;
                case ParamTypeEnum.Form:
                    webReq.ContentType = "application/x-www-form-urlencoded";
                    break;
                case ParamTypeEnum.Json:
                    webReq.ContentType = "application/json;charset=UTF-8";
                    break;
            }
            byte[] byteArray = dataEncode.GetBytes(Params);
            using (Stream writer = webReq.GetRequestStream())
            {
                writer.Write(byteArray, 0, byteArray.Length);
            }
            return webReq;
        }
        public static HttpWebRequest GetHttpWebRequest(string URL, string Params, MethodTypeEnum methodType, ParamTypeEnum paramType, Encoding dataEncode)
        {
            if (methodType == MethodTypeEnum.POST)
            {
                return GetPostHttpWebRequest(URL, Params, paramType, dataEncode);
            }
            else
            {
                return GetGetHttpWebRequest(URL, Params);
            }
        }
        /// <summary>
        /// 从请求中获得对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>获取的对象</returns>
        public static T GetModelByRequest<T>(bool IsAttribut = false)
        {
            Type TType = typeof(T);
            object Model = null;
            ConstructorInfo[] cis = TType.GetConstructors();
            if (cis.Length > 0)
            {
                ConstructorInfo ci = cis[0];
                ParameterInfo[] pis = ci.GetParameters();
                if (pis.Length == 0)
                {
                    PropertyInfo[] props = TType.GetProperties();
                    Model = ci.Invoke(new object[0]);
                    foreach (PropertyInfo prop in props)
                    {
                        if (IsAttribut)
                        {
                            RequestToModelHasAttributes(Model, prop);
                        }
                        else
                        {
                            RequestToModelNotAttributes(Model, prop);
                        }
                    }
                }
            }
            return (T)Model;
        }
        /// <summary>
        ///  请求转换为对象(有特性)
        /// </summary>
        /// <param name="Model">实例对象</param>
        /// <param name="prop">属性</param>
        private static void RequestToModelHasAttributes(object Model, PropertyInfo prop)
        {
            ColumnModelAttribute dma = null;
            foreach (Attribute attr in Attribute.GetCustomAttributes(prop))
            {
                if (attr.GetType() == typeof(ColumnModelAttribute))
                {
                    dma = attr as ColumnModelAttribute;
                    object value = HttpContext.Current.Request[dma.DBColumnName];
                    SetRequestValue(Model, value, prop);
                }
            }
        }
        /// <summary>
        /// 请求转换为对象(没有特性)
        /// </summary>
        /// <param name="Model">实例对象</param>
        /// <param name="prop">属性</param>
        private static void RequestToModelNotAttributes(object Model, PropertyInfo prop)
        {
            object value = HttpContext.Current.Request[prop.Name];
            SetRequestValue(Model, value, prop);
        }
        /// <summary>
        /// 从请求中设置值到模型
        /// </summary>
        /// <param name="Model">要设置的对象</param>
        /// <param name="value">值</param>
        /// <param name="prop">成员信息</param>
        private static void SetRequestValue(object Model, object value, PropertyInfo prop)
        {
            string propTypeName = prop.PropertyType.Name;
            if (value != null && value.ToString() != "")
            {
                if (propTypeName == typeof(Int32).Name)
                {
                    prop.SetValue(Model, Convert.ToInt32(value), null);
                }
                else if (propTypeName == typeof(String).Name)
                {
                    prop.SetValue(Model, Convert.ToString(value), null);
                }
                else if (propTypeName == typeof(Boolean).Name)
                {
                    prop.SetValue(Model, Convert.ToBoolean(value), null);
                }
                else if (propTypeName == typeof(Int64).Name)
                {
                    prop.SetValue(Model, Convert.ToInt64(value), null);
                }
                else if (propTypeName == typeof(Byte).Name)
                {
                    prop.SetValue(Model, Convert.ToByte(value), null);
                }
                else if (propTypeName == typeof(Char).Name)
                {
                    prop.SetValue(Model, Convert.ToChar(value), null);
                }
                else if (propTypeName == typeof(DateTime).Name)
                {
                    prop.SetValue(Model, Convert.ToDateTime(value), null);
                }
                else if (propTypeName == typeof(Decimal).Name)
                {
                    prop.SetValue(Model, Convert.ToDecimal(value), null);
                }
                else if (propTypeName == typeof(Double).Name)
                {
                    prop.SetValue(Model, Convert.ToDouble(value), null);
                }
                else if (propTypeName == typeof(SByte).Name)
                {
                    prop.SetValue(Model, Convert.ToSByte(value), null);
                }
                else if (propTypeName == typeof(Single).Name)
                {
                    prop.SetValue(Model, Convert.ToSingle(value), null);
                }
                else if (propTypeName == typeof(UInt16).Name)
                {
                    prop.SetValue(Model, Convert.ToUInt16(value), null);
                }
                else if (propTypeName == typeof(UInt32).Name)
                {
                    prop.SetValue(Model, Convert.ToUInt32(value), null);
                }
                else if (propTypeName == typeof(UInt64).Name)
                {
                    prop.SetValue(Model, Convert.ToUInt64(value), null);
                }
            }
        }
    }
}
