using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace iot_backend.Mail
{
    public class MailClient
    {
        public void sendMailSingle(string destAdress, string mailSubject, string mailBody)
        {
            sendGMail("internetoftrash@gmail.com", "raspberry12", destAdress, mailSubject, mailBody);
        }

        public void sendMailToList(List<String> destAdresslist, string mailSubject, string mailBody)
        {
            sendGMailtoList("internetoftrash@gmail.com", "raspberry12", destAdresslist, mailSubject, mailBody);
        }



        //private method exists for security reasons, please call it's public equivalent instead.
        private void sendGMail(string sendAdress, string sendPwd, string destAdress, string mailSubject, string mailBody)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(sendAdress, sendPwd),
                EnableSsl = true
            };
            client.Send(sendAdress, destAdress, mailSubject, mailBody);
            Console.WriteLine("Sent");
            Console.ReadLine();
           
        }

        //private method exists for security reasons, please call it's public equivalent instead.
        private void sendGMailtoList(string sendAdress, string sendPwd, List<String> destAdresslist, string mailSubject, string mailBody)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(sendAdress, sendPwd),
                EnableSsl = true
            };

            foreach (String dest in destAdresslist)
            {
                client.Send(sendAdress, dest, mailSubject, mailBody);
            }
        }
    }
}
