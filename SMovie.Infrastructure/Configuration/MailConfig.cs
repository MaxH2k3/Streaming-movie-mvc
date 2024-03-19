using Microsoft.Extensions.DependencyInjection;
using SMovie.Domain.Models;

namespace SMovie.Infrastructure.Configuration;

public static class MailConfig
{
    public static void AddFluentEmail(this IServiceCollection services)
    {
        var gmailSetting = new MailSetting();

        services.AddFluentEmail(gmailSetting.Mail)
        .AddSmtpSender(gmailSetting.SmtpServer, gmailSetting.Port,
                        gmailSetting.DisplayName, gmailSetting.Password)
        .AddRazorRenderer();
    }
}
