using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laborator1
{
     class Program
     {
          static void Main(string[] args)
          {
               Connect("unite.md", 80);
               string path = @"D:\Universitate\Cursul III\Semestrul II\Programarea in retea\laborator 1\ConsoleApp1\WebSiteData.txt";
               string[] images = GetImagesPath(path);

               Parallel.For(0, images.Length, i => DownloadImage("unite.md", 80, images[i]));
          }

          private static void DownloadImage(string host, int port, string imageUrl)
          {
               IPAddress[] IPs = Dns.GetHostAddresses(host);

               Socket socket = new Socket(AddressFamily.InterNetwork,
                   SocketType.Stream,
                   ProtocolType.Tcp);

               socket.Connect(IPs, port);

               string resp = null;

               var GetReq = $"GET {imageUrl} HTTP/1.1\r\nHost: {host}\r\nConnection: " +
                              $"keep-alive\r\nAccept: text/html\r\nUser-Agent: CSharpTests\r\n\r\n";
               socket.Send(Encoding.UTF8.GetBytes(GetReq));

               var bytesStored = new byte[socket.ReceiveBufferSize];
               int responseSizeInBytes = socket.Receive(bytesStored);
               for (int i = 0; i < responseSizeInBytes; i++)
               {
                    resp += $"{Convert.ToChar(bytesStored[i]).ToString()}";
               }


               var index = resp.IndexOf("\r\n\r\n");

               resp = resp.Trim();

               int begin = imageUrl.LastIndexOf("/") + 1;
               string imageName = imageUrl.Substring(begin, imageUrl.Length - begin);
               var writer = new BinaryWriter(File.Open(@"D:\Universitate\Cursul III\Semestrul II\Programarea in retea\laborator 1\ConsoleApp1\Images\" + imageName, FileMode.OpenOrCreate));
               for (int i = 4 + index; i < resp.Length; i++)
               {
                    writer.Write((byte)resp[i]);
               }

               socket.Close();
          }

          public static void Connect(string host, int port)
          {
               IPAddress[] IPs = Dns.GetHostAddresses(host);

               Socket s = new Socket(AddressFamily.InterNetwork,
                   SocketType.Stream,
                   ProtocolType.Tcp);
               Console.WriteLine("Establishing Connection to {0}",
                   host);
               s.Connect(IPs, port);
               Console.WriteLine("Connection established");

               string request = "GET / HTTP/1.1\r\nHost: " + host + "\r\nAccept: *\r\n\r\n";
               int response = s.Send(Encoding.UTF8.GetBytes(request));

               FileStream file;
               string path = @"D:\Universitate\Cursul III\Semestrul II\Programarea in retea\laborator 1\ConsoleApp1\WebSiteData.txt";

               if (!File.Exists(path))
               {
                    file = File.Create(path);
                    file.Close();
               }

               File.WriteAllText(path, string.Empty);

               byte[] buffer = new byte[20000];

               while (true)
               {
                    var receive = s.Receive(buffer);

                    if (receive == 0)
                    {
                         return;
                    }

                    string responseString = Encoding.ASCII.GetString(buffer, 0, receive);
             
                    Console.WriteLine(responseString);
                    File.AppendAllText(path, responseString + Environment.NewLine);
               }

          }

          public static string[] GetImagesPath(string path)
          {
               var lines = File.ReadLines(path);

               string[] images = new string[0];

               Regex regexPNGImages = new Regex("images.*.png");
               Regex regexJPGImages = new Regex("images.*.jpg");

               int i = 0;
               foreach (var line in lines)
               {
                    if (regexPNGImages.IsMatch(line))
                    {
                         if (line.Contains("wide"))
                         {
                              int begin = line.IndexOf("wide=\"") + "wide=\"".Length;
                              int end = line.IndexOf("png\"") + "png\"".Length;
                              string imagePath = line.Substring(begin, end - begin - 1);
                              Array.Resize(ref images, images.Length + 1);
                              images[i] = imagePath;
                              i++;
                         }
                         else if (line.Contains("lazy"))
                         {
                              int begin = line.IndexOf("lazy=\"") + "lazy=\"".Length;
                              int end = line.IndexOf("png\"") + "png\"".Length;
                              string imagePath = line.Substring(begin, end - begin - 1);
                              Array.Resize(ref images, images.Length + 1);
                              images[i] = imagePath;
                              i++;
                         }
                         else
                         {
                              int begin = line.IndexOf("src=\"") + "src=\"".Length;
                              int end = line.IndexOf("png\"") + "png\"".Length;
                              string imagePath = line.Substring(begin, end - begin - 1);
                              Array.Resize(ref images, images.Length + 1);
                              images[i] = imagePath;
                              i++;
                         }

                         if (line.Contains("narrow"))
                         {
                              int begin = line.IndexOf("narrow=\"") + "narrow=\"".Length;
                              int end = line.LastIndexOf("png\"") + "png\"".Length;
                              string imagePath = line.Substring(begin, end - begin - 1);
                              Array.Resize(ref images, images.Length + 1);
                              images[i] = imagePath;
                              i++;
                         }

                    }
                    else if (regexJPGImages.IsMatch(line))
                    {
                         if (line.Contains("wide"))
                         {
                              int begin = line.IndexOf("wide=\"") + "wide=\"".Length;
                              int end = line.IndexOf("jpg\"") + "jpg\"".Length;
                              string imagePath = line.Substring(begin, end - begin - 1);
                              Array.Resize(ref images, images.Length + 1);
                              images[i] = imagePath;
                              i++;

                              if (line.Contains("narrow"))
                              {
                                   begin = line.IndexOf("narrow=\"") + "narrow=\"".Length;
                                   end = line.LastIndexOf("jpg\"") + "jpg\"".Length;
                                   imagePath = line.Substring(begin, end - begin - 1);
                                   Array.Resize(ref images, images.Length + 1);
                                   images[i] = imagePath;
                                   i++;
                              }
                         }
                         else if (line.Contains("lazy"))
                         {
                              int begin = line.IndexOf("lazy=\"") + "lazy=\"".Length;
                              int end = line.IndexOf("jpg\"") + "jpg\"".Length;
                              string imagePath = line.Substring(begin, end - begin - 1);
                              Array.Resize(ref images, images.Length + 1);
                              images[i] = imagePath;
                              i++;
                         }
                         else if (line.Contains("src"))
                         {
                              int begin = line.IndexOf("src=\"") + "src=\"".Length;
                              int end = line.IndexOf("jpg\"") + "jpg\"".Length;
                              Console.WriteLine(line);
                              string imagePath = line.Substring(begin, end - begin - 1);
                              Array.Resize(ref images, images.Length + 1);
                              images[i] = imagePath;
                              i++;
                         }
                         else
                         {
                              int begin = line.IndexOf("url('") + "url('".Length;
                              int end = line.IndexOf("jpg\'") + "jpg\'".Length;
                              string imagePath = line.Substring(begin, end - begin - 1);
                              Array.Resize(ref images, images.Length + 1);
                              images[i] = imagePath;
                              i++;
                         }
                    }

               }

               return images;
          }

     }
}