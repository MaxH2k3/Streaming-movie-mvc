using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Application.Service;
using SMovie.Domain.Models;

namespace SMovie.Dashboard.Controllers.Dashboard
{
    public class ApiController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly INotificationService _notificationService;
        private readonly IGeminiService _geminiService;

        public ApiController(ICommonService commonService, 
                    INotificationService notificationService, 
                    IGeminiService geminiService)
        {
            _commonService = commonService;
            _notificationService = notificationService;
            _geminiService = geminiService;
        }

        public async Task<int> TotalCrews()
        {
            return await _commonService.TotalCrews();
        }

        public async Task<int> TotalAccount()
        {
            return await _commonService.TotalAccount();
        }

        public async Task<int> TotalMovies()
        {
            return await _commonService.TotalMovie();
        }

        public async Task<int> TotalCategory()
        {
            return await _commonService.TotalCategory();
        }

        public async Task<bool> Test(string id)
        {
            return await _commonService.TestDelete(id);
        }

        public async Task<IEnumerable<Notification>> GetNotifications(int page)
        {
            return await _notificationService.GetNotifications(page, 1);
        }

        public async Task<ResponseDTO> GenerateMovie(string content, string? nation = null)
        {
            var res = await _geminiService.ChatBotMovie("conan", "Japan");

            return res;
        }

    }
}
