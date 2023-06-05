using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Model
{
    public class EmailModel
    {
        /// <summary>
        /// 发送者邮箱
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 接收者邮箱列表
        /// </summary>
        public List<string> To { get; set; }

        /// <summary>
        /// 抄送者邮箱列表
        /// </summary>
        public string[] Cc { get; set; }

        /// <summary>
        /// 秘抄者邮箱列表
        /// </summary>
        public string[] Bcc { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 是否是HTML格式
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public string[] Attachments { get; set; }

        ///// <summary>
        ///// 邮箱服务类型(Pop3,SMTP,IMAP,MAIL等)，默认为SMTP
        ///// </summary>
        //public string ServiceType { get; set; }

        /// <summary>
        /// 邮箱服务器，如果没有定义邮箱服务器，则根据serviceType和Sender组成邮箱服务器
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 邮箱端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 邮箱账号(默认为发送者邮箱的账号)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱密码(默认为发送者邮箱的密码)，默认格式GB2312
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 邮箱优先级
        /// </summary>
        public MailPriority MailPriority { get; set; }

        /// <summary>
        ///  邮件正文编码格式
        /// </summary>
        public Encoding Encoding { get; set; }
    }
}
