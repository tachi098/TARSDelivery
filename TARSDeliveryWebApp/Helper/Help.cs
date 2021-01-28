using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Helper
{
    public static class Help
    {
        //Send Email
        public static class SendEmail
        {
            public static async Task<bool> SendMail(string _from, string _to, string _subject, string _body, SmtpClient client)
            {
                MailMessage message = new MailMessage(
                    from: _from,
                    to: _to,
                    subject: _subject,
                    body: _body
                );
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.ReplyToList.Add(new MailAddress(_from));
                message.Sender = new MailAddress(_from);


                try
                {
                    await client.SendMailAsync(message);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            public static async Task<bool> EmployeeLogin(string _to, string password, string path)
            {
                string _subject = "Employee Account TarsDelivery";
                string _from = "tarsdeliverydemo@gmail.com";
                string _body = @$"<html>
                      <body>
                      <p>Dear employee,</p>
                      <p>I gave you email and password to signin TarsDelivery Management</p>
                      <p><b>Email: </b>{_to}</p>
                      <p><b>Password: </b>{password}</p>
                      <p><b>Link: </b>{path}</p>
                      <p>From,<br>-Admin</br></p>
                      </body>
                      </html>
                     ";
                string _gmailsend = "tarsdeliverydemo@gmail.com";
                string _gmailpassword = "aptech123456";
                MailMessage message = new MailMessage(
                    from: _from,
                    to: _to,
                    subject: _subject,
                    body: _body
                );
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.ReplyToList.Add(new MailAddress(_from));
                message.Sender = new MailAddress(_from);

                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential(_gmailsend, _gmailpassword);
                    client.EnableSsl = true;
                    return await SendMail(_from, _to, _subject, _body, client);
                }
            }
            public static async Task<bool> ResetPassword(string _to, string path)
            {
                string _subject = "Employee Account TarsDelivery";
                string _from = "tarsdeliverydemo@gmail.com";
                string _body = @$"<html>
                      <body>
                      <p>Dear !</p>
                      <p>Click to Link to Reset Password</p>
                      <a href='{path}'><b>Click here</b></a>
                      <p>From,<br>-Admin</br></p>
                      </body>
                      </html>
                     ";
                string _gmailsend = "tarsdeliverydemo@gmail.com";
                string _gmailpassword = "aptech123456";
                MailMessage message = new MailMessage(
                    from: _from,
                    to: _to,
                    subject: _subject,
                    body: _body
                );
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.ReplyToList.Add(new MailAddress(_from));
                message.Sender = new MailAddress(_from);

                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential(_gmailsend, _gmailpassword);
                    client.EnableSsl = true;
                    return await SendMail(_from, _to, _subject, _body, client);
                }
            }
        }
    }
}
