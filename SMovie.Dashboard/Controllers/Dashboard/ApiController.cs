using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;
using SMovie.Dashboard.Constants;
using SMovie.Domain.Models;

namespace SMovie.Dashboard.Controllers.Dashboard
{
    public class ApiController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly INotificationService _notificationService;

        public ApiController(ICommonService commonService, 
                    INotificationService notificationService)
        {
            _commonService = commonService;
            _notificationService = notificationService;
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

    }
}
