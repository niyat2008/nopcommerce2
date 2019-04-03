using Nop.Services.Z_Harag.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Nop.Web.Infrastructure.Harag
{
    public class EmailManager
    {
        public string server { get; set; }
        public string emailHost { get; set; }
        public string password { get; set; }


        public EmailManager(string emailServer, string emailHost, string password)
        {
            this.emailHost = emailHost;
            this.server = emailServer;
            this.password = password;
        }

        public bool SendMail(string from, string to, CustomerServiceModel mailModel)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = this.server;
            smtpClient.Port = 587;


            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailHost, password);
            smtpClient.UseDefaultCredentials = false;

            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = mailModel.FullName  + " | من عميل موقع المزارع";
            mailMessage.Body = String.Format(
                "تفاصيل رساله العميل الي اداره  موقع " + mailModel.ContactDepartment  + " \n" +
                "الاسم : {0} {1} \n" +
                "الهاتف: {2} \n" +
                "القسم: {3} \n" +
                "نوع الاستفسار: {4} \n" +
                "البريد الالكتروني: {5} \n" +
                "محتوي الرساله: {6} \n" ,
                "",
                 mailModel.FullName,
                mailModel.Phone,
                mailModel.ContactDepartment,
                mailModel.ContactType,
                mailModel.Email,
                mailModel.Message).ToString();
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                return false;
            }


            return true;
        }
 
    }

}
