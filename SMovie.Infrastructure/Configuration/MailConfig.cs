using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SMovie.Domain.Models;

namespace SMovie.Infrastructure.Configuration;

public static class MailConfig
{
    public static void AddFluentEmail(this IServiceCollection services, IConfiguration configuration)
    {
        var mailSetting = configuration.GetSection("GmailSetting").Get<MailSetting>();

        services.AddFluentEmail(mailSetting.Mail)
        .AddSmtpSender(mailSetting.SmtpServer, mailSetting.Port,
                        mailSetting.DisplayName, mailSetting.Password)
        .AddRazorRenderer();
    }
}
