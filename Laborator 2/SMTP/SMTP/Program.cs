using Limilabs.Client.IMAP;
using Limilabs.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Laborator2
{
     class Program
     {
          static void Main()
          {
               string Login, Receiver, Password, Subject, Message;
               bool isActive = true;

               Console.WriteLine("Enter your login: ");
               Login = Console.ReadLine();

               Console.WriteLine("Enter your password: ");
               Password = Console.ReadLine();

               while (isActive)
               {
                    Console.Clear();
                    Console.WriteLine("1. Send a message");
                    Console.WriteLine("2. Check my messages");
                    Console.WriteLine("3. Exit");
                    int k = int.Parse(Console.ReadLine());
                    switch (k)
                    {
                         case 1:
                              Console.WriteLine("Enter receiver e-mail: ");
                              Receiver = Console.ReadLine();

                              Console.WriteLine("Type the subject of message: ");
                              Subject = Console.ReadLine();

                              Console.WriteLine("Type the content of message: ");
                              Message = Console.ReadLine();

                              SendMessage(Login, Receiver, Password, Subject, Message);
                              break;
                         case 2:
                              ShowMessages(Login, Password);
                              break;
                         case 3: default:
                              return;

                    }
                    Console.ReadKey();
               }
               Console.ReadKey();
          }

          public static void SendMessage(string from, string receiver, string password, string subject, string message)
          {
               MailMessage mail = new MailMessage();
               
               mail.From = new MailAddress(from);
               mail.To.Add(receiver);
               mail.Subject = subject;
               mail.Body = "<h1>" + message + "</h1>";
               mail.IsBodyHtml = true;

               SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
                    
               smtp.Credentials = new NetworkCredential(from, password);
               smtp.EnableSsl = true;
               smtp.Send(mail);


               Console.WriteLine("Message was successfully sent to " + receiver);
               
          }

          public static void ShowMessages(string Email, string password)
          {
               Console.WriteLine("InboxMessages:");
               Imap imap = new Imap();
               imap.Connect("outlook.office365.com");
               imap.UseBestLogin(Email, password);
               imap.SelectInbox();
               List<long> uids = imap.Search(Flag.New);
               foreach (long uid in uids)
               {
                    IMail email = new MailBuilder().CreateFromEml(imap.GetMessageByUID(uid));
                         Console.WriteLine(email.Subject);
               }
               imap.Close();
               
          }

     }
}
