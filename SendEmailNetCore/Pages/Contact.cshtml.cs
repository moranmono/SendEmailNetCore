using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmailNetCore.Pages
{
    public class ContactModel : PageModel
    {
        public string PostedMessage { get; set; } = string.Empty;

        public void OnGet(int id)
        {
            ViewData["PostedMessage"] = "Your message has been sent [viewdata]";
            PostedMessage = "Your message has been sent [property]";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Redirect("/contact");
        }

        public void SendMail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("moran mono", "moran@mono.com"));
            message.To.Add(new MailboxAddress("boris moris", "moris@belhasan.com"));
            message.Subject = "what is up?";
            message.Body = new TextPart("plain")
            {
                Text = "Send email app dot net core 2.2"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("username", "password");
                client.Send(message);
                client.Disconnect(true);
            }
        }
	
    }
}