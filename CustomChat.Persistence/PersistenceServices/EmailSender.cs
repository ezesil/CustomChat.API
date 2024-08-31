using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChat.Persistence.PersistenceServices
{
    public class EmailSender : IEmailSender<IdentityUser>
    {
        public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
        {
            Console.WriteLine($"Enviando correo a {email}...");
            return Task.CompletedTask;
        }

        public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
        {
            Console.WriteLine($"Enviando correo a {email}...");
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
        {
            Console.WriteLine($"Enviando correo a {email}...");
            return Task.CompletedTask;
        }
    }
}
