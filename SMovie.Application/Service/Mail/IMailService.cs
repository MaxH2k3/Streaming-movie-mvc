using MimeKit;
using SMovie.Domain.Models;

namespace SMovie.Application.IService;

public interface IMailService
{
    Task<bool> SendUsingTemplateFromFile(string template, string title, UserMail userMail);
}
