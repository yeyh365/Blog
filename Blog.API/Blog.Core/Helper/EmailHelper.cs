using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Model;

namespace Blog.Core.Helper
{
    public class EmailHelper
    {
        public int Send(EmailModel email)
        {
            var mailMessage = new MailMessage();

            //读取To  接收者邮箱列表
            try
            {
                if (email.To != null && email.To.Count > 0)
                {
                    foreach (string to in email.To)
                    {
                        if (string.IsNullOrEmpty(to)) continue;
                        mailMessage.To.Add(new MailAddress(to.Trim()));
                    }
                }
                //读取Cc  抄送者邮件地址
                if (email.Cc != null && email.Cc.Length > 0)
                {
                    foreach (var cc in email.Cc)
                    {
                        if (string.IsNullOrEmpty(cc)) continue;
                        mailMessage.CC.Add(new MailAddress(cc.Trim()));
                    }
                }
                //读取Attachments 邮件附件
                if (email.Attachments != null && email.Attachments.Length > 0)
                {
                    foreach (var attachment in email.Attachments)
                    {
                        if (string.IsNullOrEmpty(attachment)) continue;
                        mailMessage.Attachments.Add(new Attachment(attachment));
                    }
                }
                //读取Bcc 秘抄人地址
                if (email.Bcc != null && email.Bcc.Length > 0)
                {
                    foreach (var bcc in email.Bcc)
                    {
                        if (string.IsNullOrEmpty(bcc)) continue;
                        mailMessage.Bcc.Add(new MailAddress(bcc.Trim()));
                    }
                }
                //读取From 发送人地址
                mailMessage.From = new MailAddress(email.From);

                //邮件标题
                //Encoding encoding = Encoding.GetEncoding("GB2312");
                mailMessage.Subject = email.Subject;
                //邮件正文是否为HTML格式
                mailMessage.IsBodyHtml = email.IsBodyHtml;
                //邮件正文
                mailMessage.Body = email.Body;
                mailMessage.BodyEncoding = email.Encoding;
                //邮件优先级
                mailMessage.Priority = email.MailPriority;

                //发送邮件代码实现
                var smtpClient = new SmtpClient
                {
                    Host = email.Host,
                    Port = email.Port,
                    Credentials = new NetworkCredential(email.UserName, email.Password),
                    EnableSsl = false,
                };
                //加这段之前用公司邮箱发送报错：根据验证过程，远程证书无效
                //加上后解决问题
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
                //认证
                smtpClient.Send(mailMessage);
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
