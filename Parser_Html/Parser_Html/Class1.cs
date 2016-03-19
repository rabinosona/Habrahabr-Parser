using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileHelpers;
using AngleSharp;
using AngleSharp.Html;
using AngleSharp.Parser.Html;
using AngleSharp.Extensions;
using System.Net.Mail;

namespace Parser_Html
{
        class Connect
        {
            string[] input_from_server = new string[60];
            string URL;
            byte[] translate = new byte[1024];
            string user;
            int m_pages;
              
            public void Download_Write(int pages)
            {
                File.Delete(@"E:\C#\Parser\Saving folder\info.csv"); // delete the last info
                Console.WriteLine("Enter the topic: ");
                user = Console.ReadLine();
                WebClient con = new WebClient();
                for (int i = 0; i < pages; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    URL = "https://habrahabr.ru/search/page" + i + 1 + "/?q=" + user + "&target_type=posts&order_by=relevance"; // get user's page count and key word
                    input_from_server[i] = con.DownloadString(URL);
                    translate = Encoding.Default.GetBytes(input_from_server[i]); // get page and encode to utf-8
                    input_from_server[i] = Encoding.UTF8.GetString(translate);
                }
                m_pages = pages;
                // System.IO.File.WriteAllLines(@"E:\C#\Parser\Saving folder\Pars.html", input_from_server); // write to file
            }

            public void Check()
            {
                // System.IO.File.AppendAllText(@"E:\C#\Parser\Saving folder\info.csv", "sep = ;" + Environment.NewLine);
                for (int i = 0; i < m_pages; i++)
                {
                var parser = new HtmlParser();
                var dom = parser.Parse(input_from_server[i]); // open file and parse it to DOM
                var elements = dom.QuerySelectorAll("a.post_title"); // find all a.post_title
                foreach (var item in elements)
                {
                    string n = item.GetAttribute("href"); // string for link
                    System.IO.File.AppendAllText(@"E:\C#\Parser\Saving folder\info.csv", "\"" + item.TextContent + "\"" +","+ "\"" + n + "\"" + Environment.NewLine); // "\"" construction to avoid bugs
                }
            }
            }

            public void Send(string from, string to)
            {
            string file = @"E:\C#\Parser\Saving folder\info.csv"; // file to send
            MailMessage Msg = new MailMessage();
            // Sender e-mail address.
            Msg.From = new MailAddress(from);
            Attachment data = new Attachment(file); // attachment to mail
            // Recipient e-mail address.
            Msg.To.Add(to);
            Msg.Subject = "Csv";
            Msg.Attachments.Add(data);
            Msg.Body = "Open this file with google viewer";
            // message info
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential(from, "SlojnoTa");
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            /* info for google smtp: Credentials, Host and enable ssl */
        }
        }
    }