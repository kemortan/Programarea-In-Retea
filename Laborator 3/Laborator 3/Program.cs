using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;


namespace HttpClientApp
{
     class Program
     {
          static void Main(string[] args)
          {

               Console.WriteLine("Menu");
               Console.WriteLine("1. Search announcements (999)");
               Console.WriteLine("2. Profile page (999)");
               Console.WriteLine("4. Head (utm)");
               Console.WriteLine("5. Options (protv)");
               Console.WriteLine("6. Exit");

               int k;

               try
               {
                    k = Int32.Parse(Console.ReadLine());
               }
               catch
               {
                    Console.WriteLine("Wrong option!");
                    k = 6;
               }

               while (k != 6)
               {
                    switch (k)
                    {
                         case 1:
                              Console.WriteLine("Enter name of announcement:");
                              string searchQuery = Console.ReadLine();
                              try
                              {
                                   GetAnnouncement(searchQuery);
                              }
                              catch (Exception ex)
                              {
                                   Console.WriteLine(ex.Message);
                              }
                              break;

                         case 2:
                              try
                              {
                                   GetProfileWithCookies();
                              }
                              catch (Exception ex)
                              {
                                   Console.WriteLine(ex.Message);
                              }

                              break;

                         case 4:
                              try
                              {
                                   Head();
                              }
                              catch (Exception ex)
                              {
                                   Console.WriteLine(ex.Message);
                              }

                              break;

                         case 5:
                              try
                              {
                                   Options();
                              }
                              catch (Exception ex)
                              {
                                   Console.WriteLine(ex.Message);
                              }

                              break;
                    }

                    Console.WriteLine("\nMenu");
                    Console.WriteLine("1. Search announcements (999)");
                    Console.WriteLine("2. Profile page (999)");
                    Console.WriteLine("4. Head (utm)");
                    Console.WriteLine("5. Options (protv)");
                    Console.WriteLine("6. Exit");

                    try
                    {
                         k = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                         Console.WriteLine("Wrong option!");
                         k = 6;
                    }
               }
          }

          static void GetAnnouncement(string searchQuery)
          {
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://999.md/ru/search?query=" + searchQuery);
               WebProxy myproxy = new WebProxy("46.188.30.132", 8080);
               request.Proxy = myproxy;
               request.Method = "GET";
               HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               //Console.WriteLine(response.GetResponseStream());
               var encoding = ASCIIEncoding.ASCII;
               string responseText;
               using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
               {
                    responseText = reader.ReadToEnd();
               }
               string path = @"C:\Users\Vlad\source\repos\ConsoleApp4\WebSiteData.txt";

               if (!File.Exists(path))
               {
                    File.Create(path);
               }

               File.WriteAllText(path, responseText);

               Console.WriteLine("Forrmat the html code in WebSiteData.txt[y/n]");
               string formatted = Console.ReadLine();
               if (formatted.Equals("y"))
               {
                    Regex regexTitle = new Regex("ads-list-photo-item-title");
                    var lines = File.ReadLines(path);

                    Console.WriteLine("Announcements Title:");
                    foreach (string line in lines)
                    {
                         if (regexTitle.IsMatch(line))
                         {
                              int begin = line.IndexOf("target=\"_blank\">") + "target=\"_blank\">".Length;
                              int end = line.IndexOf("</a>");
                              string title = line.Substring(begin, end - begin);
                              Console.WriteLine(title);
                         }
                    }
               }
               else
               {
                    Console.WriteLine("Can't get announcements!");
               }


          }

          static void GetProfileWithCookies()
          {
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://999.md/ru/profile/eugen-1");
               //WebProxy myproxy = new WebProxy("46.102.73.244", 53281);
               //WebProxy myproxy = new WebProxy("195.64.182.27", 56090);
               WebProxy myproxy = new WebProxy("213.136.89.190", 13006)
               request.Proxy = myproxy;
               request.Method = "GET";
               request.Headers["cookie"] = "_ym_uid=154590847681587041; search_exact_match=no; booster_tips=close; utid=\"2 | 1:0 | 10:1563635131 | 4:utid | 56:dUtBbVVUYnBUZ1dvTzZodmpraURqY2RxdlRYbmswckt1b01tUDA0UQ ==| 03e1558d3700e898038616839c204063c44b2d79a9b98fe3a9bc681d85503b89\"; __utma=117578817.1318796442.1545908476.1563032886.1563635137.59; _hjid=d97a959f-d605-4867-a0aa-8ca0753d8863; simpalsid.lang=ru; __gfp_64b=J8lsL8KhPyxy9PhvrF17pfgPyMupmXA2wDTrGa4p_iH.E7; _ym_d=1578157824; _fbp=fb.1.1583494989796.1563590219; _xsrf=2|ed6a3998|8c2f73499ca804f1c76588b899587d3b|1586287130; _gid=GA1.2.1501206201.1588460224; simpalsid.auth=f9b6929e4c20e045ef3e8660194c9cd83c55f077d5976d8b19e748df24a90546e1240d89f495f867088a062526388544c5d2f7c582b4272143f3ec462d8acb5a1c023b4025911be47427af1127313ef237c6fbde5bfb9bad25e2280d34363e26f6b57b42e8348dbb09b8532b1719a971298c56d7e694b7115f925d77097d4f24ceafd86f2fb6dee36c8ba9df054c026c04fee8b97ae2f5fbc5ffac4410ae2d882240e1e3888949fe22a7628ed0dc8b22d995d80f83ca0b874059bc53f7b386d5144c34dfe2dedb4bee2f4af20d2054ab; auth=\"2 | 1:0 | 10:1588526615 | 4:auth | 556:ZjliNjkyOWU0YzIwZTA0NWVmM2U4NjYwMTk0YzljZDgzYzU1ZjA3N2Q1OTc2ZDhiMTllNzQ4ZGYyNGE5MDU0NmUxMjQwZDg5ZjQ5NWY4NjcwODhhMDYyNTI2Mzg4NTQ0YzVkMmY3YzU4MmI0MjcyMTQzZjNlYzQ2MmQ4YWNiNWExYzAyM2I0MDI1OTExYmU0NzQyN2FmMTEyNzMxM2VmMjM3YzZmYmRlNWJmYjliYWQyNWUyMjgwZDM0MzYzZTI2ZjZiNTdiNDJlODM0OGRiYjA5Yjg1MzJiMTcxOWE5NzEyOThjNTZkN2U2OTRiNzExNWY5MjVkNzcwOTdkNGYyNGNlYWZkODZmMmZiNmRlZTM2YzhiYTlkZjA1NGMwMjZjMDRmZWU4Yjk3YWUyZjVmYmM1ZmZhYzQ0MTBhZTJkODgyMjQwZTFlMzg4ODk0OWZlMjJhNzYyOGVkMGRjOGIyMmQ5OTVkODBmODNjYTBiODc0MDU5YmM1M2Y3YjM4NmQ1MTQ0YzM0ZGZlMmRlZGI0YmVlMmY0YWYyMGQyMDU0YWI =| 3ef4d19e59c02f569712501f97cc1782de65cba25b03878a82e8540f43ad6ae4\"; view_type=photo; _ym_isad=1; trueDirect=False; _ga_BZSTQNNL68=GS1.1.1588538151.110.1.1588538163.0; _ga=GA1.2.1318796442.1545908476; _ym_visorc_23318743=b";
               HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               //Console.WriteLine(response.Headers);
               var encoding = ASCIIEncoding.ASCII;
               string responseText;
               using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
               {
                    responseText = reader.ReadToEnd();
               }

               string path = @"C:\Users\User\Desktop\HttpClientApp\ProfilePage.txt";

               if (!File.Exists(path))
               {
                    File.Create(path);
               }

               File.WriteAllText(path, responseText);

               Console.WriteLine("Forrmat the html code in WebSiteData.txt[y/n]");
               string formatted = Console.ReadLine();
               if (formatted.Equals("y"))
               {
                    Regex regexTitle = new Regex("verified-icon");
                    var lines = File.ReadLines(path);

                    Console.WriteLine("Profile name:");
                    foreach (string line in lines)
                    {
                         if (regexTitle.IsMatch(line))
                         {
                              int begin = line.IndexOf("</span>") + "</span>".Length;
                              int end = line.IndexOf("</div>");
                              string profileName = line.Substring(begin, end - begin);
                              Console.WriteLine(profileName);
                              break;
                         }
                    }
               }
               else
               {
                    Console.WriteLine("Can't get announcements!");
               }

          }

          static void Head()
          {
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://utm.md/");
               //WebProxy myproxy = new WebProxy("46.102.73.244", 53281);
               WebProxy myproxy = new WebProxy("5.59.141.152", 61981);
               request.Proxy = myproxy;
               request.Method = "HEAD";
               HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               Console.WriteLine("Response Header: " + response.Headers);
          }

          static void Options()
          {
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://protv.md/");
               //WebProxy myproxy = new WebProxy("46.102.73.244", 53281);
               WebProxy myproxy = new WebProxy("5.59.141.152", 61981);
               request.Proxy = myproxy;
               request.Method = "OPTIONS";
               HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               var encoding = ASCIIEncoding.ASCII;
               string responseText;
               using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
               {
                    responseText = reader.ReadToEnd();
               }
               Console.WriteLine(responseText);
          }
     }
}
