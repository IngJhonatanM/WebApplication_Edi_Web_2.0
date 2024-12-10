using System.Net.Mail;

namespace EDIBANK.Models.Users_EdiWeb
{
    public class EmailHelper
    {

        // methods Confirm Email
        /*   public bool SendEmail(string userEmail, string confirmationLink)
           {
               MailMessage mailMessage = new MailMessage();
               mailMessage.From = new MailAddress("relay@eniac.com");
               mailMessage.To.Add(new MailAddress(userEmail));

               mailMessage.Subject = "Confirm your email";
               mailMessage.IsBodyHtml = true;
               mailMessage.Body = confirmationLink;

               SmtpClient client = new SmtpClient();
               client.Credentials = new System.Net.NetworkCredential("relay@eniac.com", "Lol11473");
               client.Host = "outlook.office365.com";
               client.Port = 587;
               client.EnableSsl = true;

               try
               {
                   client.Send(mailMessage);
                   return true;
               }
               catch (Exception ex)
               {
                   // log exception
               }
               return false;
           }*/

        // methods for 2fa

        public bool SendEmailTwoFactorCode(string userEmail, string code)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("relay@eniac.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Two Factor Code";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = code;


            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("relay@eniac.com", "/TuPsy98\\1i");
            client.Host = "outlook.office365.com";
            client.Port = 587;
            client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

        // methods for ResetPassword

        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("relay@eniac.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("relay@eniac.com", "/TuPsy98\\1i");
            client.Host = "outlook.office365.com";
            client.Port = 587;
            client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}