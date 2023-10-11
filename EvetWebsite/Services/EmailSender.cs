using Microsoft.AspNetCore.Identity;
using NuGet.Common;
using System.Net.Mail;
using System.Net;

namespace EvetWebsite.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task<bool> SendAsync(string message, string recipient, string subject)
        {
            try
            {  
                 try
                {


                    ////create the mail message 
                    MailMessage mail = new MailMessage();


                    mail.Body = message;
                    //set the addresses 



                    mail.From = new MailAddress("admin@paramallam.com.ng", "Prof. Oluwafunmilayo J. Para-mallam mni @60"); //IMPORTANT: This must be same as your smtp authentication address.
                    mail.To.Add(recipient);

                    //set the content 
                    mail.Subject = subject.Replace("\r\n", "");

                    mail.IsBodyHtml = true;
                    //send the message 
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                    //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                    NetworkCredential Credentials = new NetworkCredential("admin@paramallam.com.ng", "lgynfcletdigyfag");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = Credentials; smtp.Timeout = 20000;
                    //alternative port number is 8889
                    smtp.EnableSsl = true; smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);


                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }
            catch (Exception exception)
            { 
                return false;
            }
        }
    }
}
