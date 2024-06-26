using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;

namespace diplom.Services
{
    public class EmailSmtpService
    {
        public void SendMail(string email, string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    SmtpClient mySmtpClient = new SmtpClient("smtp.yandex.ru");

                    //set smtp-client with SSL auth
                    mySmtpClient.UseDefaultCredentials = false;
                    NetworkCredential basicAuthInfo = new NetworkCredential("riarlikeya@yandex.ru", "bzcsbzbbzgmftiza");
                    mySmtpClient.Credentials = basicAuthInfo;
                    mySmtpClient.EnableSsl = true;

                    //add from to mailaddresses
                    MailAddress from = new MailAddress("riarlikeya@yandex.ru", "Автоматическая система оповещения");
                    MailAddress to = new MailAddress(email, "o");
                    MailMessage myMail = new MailMessage(from, to);

                    //add reply to
                    MailAddress replyTo = new MailAddress("riarlikeya@yandex.ru");
                    myMail.ReplyToList.Add(replyTo);

                    //Тема письма
                    myMail.Subject = "Допуск к работам";
                    myMail.SubjectEncoding = Encoding.UTF8;

                    //Текст письма
                    myMail.Body = message;
                    myMail.BodyEncoding = Encoding.UTF8;

                    myMail.IsBodyHtml = true;
                    mySmtpClient.Send(myMail);
                }
                else MessageBox.Show("Передано пустое значение");
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException("SmtpException has occured: " + ex.Message);
            }
        }
    }
}
