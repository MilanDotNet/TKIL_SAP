using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace TKILSAPRFC.Core.Helpers
{
    public class EmailAccount
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
        public bool UserDefaultCredentials { get; set; }
    }
    public static class EmailHelper
    {
        private static readonly EmailAccount _DefaultAccount;
        static EmailHelper()
        {
            int IsUAT = AppSettings.Current.IsUAT;

            string RegisterCompany = AppSettings.Current.CompanyDetails.RegisterCompany;

            if (IsUAT == 1)
            {
                _DefaultAccount = new EmailAccount
                {
                    DisplayName = RegisterCompany,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Email = "procuresens@safalsoftcom.com",
                    EnableSSL = true,
                    UserDefaultCredentials = false,
                    Username = "procuresens@safalsoftcom.com",
                    Password = "Safal@1.2.3.4",
                };
            }
            else
            {
                _DefaultAccount = new EmailAccount
                {
                    DisplayName = RegisterCompany,
                    Host = "smtp.office365.com",//"smtp-mail.outlook.com",
                    Port = 587,
                    Email = "welspun_procurement@welspun.com",
                    EnableSSL = true,
                    UserDefaultCredentials = false,
                    Username = "welspun_procurement@welspun.com",
                    Password = "mpckpqfzqvsfmkck",
                };
        }
    }

    public static int SendMail(string To, string CC, string BCC, string Subject, string Body)
    {
        try
        {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                int SendMail = AppSettings.Current.SendMail;
                if (SendMail == 1)
                {
                MailMessage EMsg = new MailMessage();
                EMsg.From = new MailAddress(_DefaultAccount.Username);

                string[] lstTo = To.Trim(',').Split(',');
                foreach (string t in lstTo)
                {
                    if (t.Trim() != "")
                    {
                        EMsg.To.Add(t);
                    }
                }

                string[] lstCC = CC.Trim(',').Split(',');
                foreach (string tc in lstCC)
                {
                    if (tc.Trim() != "")
                    {
                        EMsg.CC.Add(tc);
                    }
                }

                string[] lstBCC = BCC.Trim(',').Split(',');
                foreach (string tb in lstBCC)
                {
                    if (tb.Trim() != "")
                    {
                        EMsg.Bcc.Add(tb);
                    }
                }

                EMsg.Subject = Subject.Trim();
                EMsg.Body = Body.Trim();
                EMsg.IsBodyHtml = true;

                using (SmtpClient sm = new SmtpClient())
                {

                    sm.UseDefaultCredentials = _DefaultAccount.UserDefaultCredentials;
                    sm.Host = _DefaultAccount.Host;
                    sm.Port = _DefaultAccount.Port;
                    sm.EnableSsl = _DefaultAccount.EnableSSL;
                    sm.Credentials = new NetworkCredential(_DefaultAccount.Username, _DefaultAccount.Password);
                    sm.Send(EMsg);
                }
            }
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public static int SendMailWithAttachment(string To, string CC, string BCC, string Subject, string Body, string Attachment)
    {
        try
        {
            int SendMail = AppSettings.Current.SendMail;
            if (SendMail == 1)
            {
                MailMessage EMsg = new MailMessage();
                EMsg.From = new MailAddress(_DefaultAccount.Username);

                string[] lstTo = To.Trim(',').Split(',');
                foreach (string t in lstTo)
                {
                    if (t.Trim() != "")
                    {
                        EMsg.To.Add(t);
                    }
                }

                string[] lstCC = CC.Trim(',').Split(',');
                foreach (string tc in lstCC)
                {
                    if (tc.Trim() != "")
                    {
                        EMsg.CC.Add(tc);
                    }
                }

                string[] lstBCC = BCC.Trim(',').Split(',');
                foreach (string tb in lstBCC)
                {
                    if (tb.Trim() != "")
                    {
                        EMsg.Bcc.Add(tb);
                    }
                }

                string[] lstAttachment = Attachment.Trim(',').Split(',');
                foreach (string atm in lstAttachment)
                {
                    if (atm.Trim().ToString() != "")
                    {
                        System.Net.Mail.Attachment objAttc = new System.Net.Mail.Attachment(atm);
                        EMsg.Attachments.Add(objAttc);
                    }
                }

                EMsg.Subject = Subject.Trim();
                EMsg.Body = Body.Trim();
                EMsg.IsBodyHtml = true;

                using (SmtpClient sm = new SmtpClient())
                {

                    sm.UseDefaultCredentials = _DefaultAccount.UserDefaultCredentials;
                    sm.Host = _DefaultAccount.Host;
                    sm.Port = _DefaultAccount.Port;
                    sm.EnableSsl = _DefaultAccount.EnableSSL;
                    sm.Credentials = new NetworkCredential(_DefaultAccount.Username, _DefaultAccount.Password);
                    sm.Send(EMsg);
                }
            }
            return 1;
        }
        catch (Exception ex)
        {

            return 0;
        }
    }

}
}