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
        //string name = string.Empty;
        //string message = string.Empty;
        //string emailprivate = string.Empty;

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Message { get; set; }
        public string PostedMessage { get; set; } = string.Empty;

        public void OnGet(int id)
        {
            if(Request.QueryString.HasValue && Request.QueryString.Value.Contains("pb=1"))
            {
                ViewData["PostedMessage"] = "Your message has been sent [viewdata]";
                PostedMessage = "Your message has been sent [property]";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //SendMail(Name, Email, Message);
            return Redirect("/contact?pb=1");
        }

        public void SendMail(string name, string email, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("moran mono", "moran@mono.com"));
            mimeMessage.To.Add(new MailboxAddress(name, email));
            mimeMessage.Subject = "what is up?";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }
	
    }
}