using Microsoft.AspNetCore.Mvc;
using SMovie.Application.IService;

namespace SMovie.Dashboard.Controllers.Dashboard
{
    public class ApiController : Controller
    {
        private readonly ICommonService _commonService;

        public ApiController(ICommonService commonService)
        {
            _commonService = commonService;
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

    }
}
