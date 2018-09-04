using System;
namespace MateralTools.CoreWebAPI.Models
{
    /// <summary>
    /// ¥ÌŒÛ ”Õºƒ£–Õ
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}