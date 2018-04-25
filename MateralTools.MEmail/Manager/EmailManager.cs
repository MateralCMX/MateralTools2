using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace MateralTools.MEmail
{
    public class EmailManager
    {
        /// <summary>
        /// 发送人邮件地址
        /// </summary>
        public string FormEmail { get; set; }
        /// <summary>
        /// 发送人名称
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 目标邮件地址
        /// </summary>
        public List<string> TargetEmail { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="formName">发送人名称</param>
        /// <param name="formEmail">发送邮件地址</param>
        /// <param name="targetEmail">目标地址</param>
        public EmailManager(string formName, string formEmail, string[] targetEmail)
        {
            FormEmail = formEmail;
            FormName = formName;
            TargetEmail = targetEmail.ToList();
        }
        /// <summary>
        /// 发送QQ邮件
        /// </summary>
        /// <param name="titles">邮件标题</param>
        /// <param name="contents">邮件内容</param>
        /// <param name="pwd">密码(授权码)</param>
        public void QQSend(string titles, string contents, string pwd)
        {
            SendSSL(titles, contents, pwd, "smtp.qq.com", 25);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="titles">邮件标题</param>
        /// <param name="contents">邮件内容</param>
        /// <param name="pwd">密码(授权码)</param>
        /// <param name="SMTPServer">SMTP地址</param>
        /// <param name="Port">端口号</param>
        public void Send(string titles, string contents, string pwd, string SMTPServer, int Port = 25)
        {
            SmtpClient client = new SmtpClient(SMTPServer)
            {
                EnableSsl = false,
                Port = Port,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(FormEmail, pwd)
            };
            MailAddress from = new MailAddress(FormEmail, FormName, Encoding.UTF8);
            MailMessage message = new MailMessage
            {
                From = from
            };
            foreach (string item in TargetEmail)
            {
                message.To.Add(new MailAddress(item, "", Encoding.UTF8));
            }
            message.Body = contents;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = titles;
            message.SubjectEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            client.Send(message);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="titles">邮件标题</param>
        /// <param name="contents">邮件内容</param>
        /// <param name="pwd">密码(授权码)</param>
        /// <param name="SMTPServer">SMTP地址</param>
        /// <param name="Port">端口号</param>
        public void SendSSL(string titles, string contents, string pwd, string SMTPServer, int Port = 25)
        {
            SmtpClient client = new SmtpClient(SMTPServer)
            {
                EnableSsl = true,
                Port = Port,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(FormEmail, pwd)
            };
            MailAddress from = new MailAddress(FormEmail, FormName, Encoding.UTF8);
            MailMessage message = new MailMessage
            {
                From = from
            };
            foreach (string item in TargetEmail)
            {
                message.To.Add(new MailAddress(item, "", Encoding.UTF8));
            }
            message.Body = contents;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = titles;
            message.SubjectEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            client.Send(message);
        }
    }
}
