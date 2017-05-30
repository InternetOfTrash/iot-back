using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace iot_backend
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
            MailMessage m = new MailMessage();
            m.Body = mailBody + destAdress + "/1'>here!</a>";
            m.IsBodyHtml = true;
            m.Subject = mailSubject;
            m.From = new MailAddress(sendAdress);
            m.To.Add(new MailAddress(destAdress));
            client.Send(m);
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
            MailMessage m = new MailMessage();
            foreach (String dest in destAdresslist)
            {
                m.Body = mailBody + dest + "/1'>here!</a>";
                m.IsBodyHtml = true;
                m.Subject = mailSubject;
                m.From = new MailAddress(sendAdress);
                m.To.Add(new MailAddress(dest));
                client.Send(m);
            }
        }

        public bool isValidEmail(string email)
        {
            string validEmailPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            Regex regex = new Regex(validEmailPattern,RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}
