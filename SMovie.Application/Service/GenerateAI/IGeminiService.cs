using SMovie.Domain.Models;

namespace SMovie.Application.Service
{
    public interface IGeminiService
    {
        Task<ResponseDTO> Chat(string content, string nation);
        Task<ResponseDTO> ChatBotMovie(string content, string nation);
    }
}
