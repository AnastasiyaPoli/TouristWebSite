using System;
using System.Net;
using System.Net.Mail;

namespace TouristWebSite.Helpers
{
    public class EmailSenderHelper
    {
        public static int numberOfRetrials = 3;

        public static void SendEmail(string to, string subject, string body, string filename = "")
        {
            using (var mailClient = new SmtpClient())
            {
                MailMessage msg = new MailMessage
                {
                    From = new MailAddress("anastasiyapolianska@gmail.com")
                };
                msg.To.Add(to);
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;
                if (filename != string.Empty)
                {
                    msg.Attachments.Add(new Attachment(filename));
                }

                mailClient.Host = "smtp.gmail.com";
                mailClient.Port = 587;
                mailClient.EnableSsl = true;
                mailClient.Credentials = new NetworkCredential("anastasiyapolianska@gmail.com", "papa_america13");

                int numberOfTrial = 0;

                while (true)
                {
                    try
                    {
                        mailClient.Send(msg);
                        break;
                    }
                    catch (Exception e)
                    {
                        numberOfTrial++;
                        if (numberOfTrial < numberOfRetrials)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}