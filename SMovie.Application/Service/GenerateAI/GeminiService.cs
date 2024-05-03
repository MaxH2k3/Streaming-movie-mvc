using GenerativeAI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMovie.Application.MessageService;
using SMovie.Domain.Constants;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SMovie.Application.Service
{
    public class GeminiService : IGeminiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private GenerativeModel _client = null!;
        private readonly IConfiguration _config;
        private readonly MovieBot _movieBotOptions;

        public GeminiService(IUnitOfWork unitOfWork, IConfiguration configuration,
            IOptions<MovieBot> options)
        {
            _unitOfWork = unitOfWork;
            _config = configuration;
            _movieBotOptions = options.Value;
        }

        private async Task<string> GenerateScript(string content, string nation)
        {
            var features = (await _unitOfWork.FeatureRepository.GetAll()).ToJson();
            var categories = (await _unitOfWork.CategoryRepository.GetAll()).ToJson();
            var nations = (await _unitOfWork.NationRepository.GetAll()).ToJson();

            MovieGemeni movieGemini = new();
            var pattern = $"{movieGemini} \n\n Give me only one json with that format about film has named \"{content}\" ";
            pattern += (nation != null) ? "produced in " + nation : "";
            pattern += $" using example from FeatureFilm {features} and Category {categories} and Nation {nations}. You should search exactly name. ";
            pattern += "Note: give me just json not giving anythings.";

            return pattern;
        }

        public async Task<ResponseDTO> Chat(string content, string nation)
        {
            string? result = "";
            var key = _config.GetValue<string>("GeminiAI:APIKey");
            if (key == null)
            {
                return new ResponseDTO(HttpStatusCode.ServiceUnavailable, $"Gemini Key {MessageCommon.NotFound}");
            }

            _client = new GenerativeModel(key);

            try
            {
                string script = await GenerateScript(content, nation);
                result = await _client.GenerateContentAsync(script);
                result = CleanResult(result!);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (CheckNull(result!))
            {
                return new ResponseDTO
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = MessageSystem.ServerError
                };
            }

            if(!IsJson(result!))
            {
                return new ResponseDTO
                {
                    Status = HttpStatusCode.NotFound,
                    Message = $"{content} {MessageCommon.NotFound}"
                };
            }

            return new ResponseDTO
            {
                Status = HttpStatusCode.OK,
                Message = MessageCommon.Complete,
                Data = result.ToJson()
            };
        }

        public async Task<ResponseDTO> ChatBotMovie(string content, string nation)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add(CozeBotAPI.Authorization, $"Bearer {_movieBotOptions.Authorization}");
            client.DefaultRequestHeaders.Add(CozeBotAPI.Connection, _movieBotOptions.Connection);

            var data = new
            {
                bot_id = _movieBotOptions.BotId,
                stream = _movieBotOptions.Stream,
                user = _movieBotOptions.User,
                query = await GenerateScript(content, nation)
            };

            var response = await client.PostAsJsonAsync(CozeBotAPI.API, data);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JObject.Parse(responseContent);
                var result = responseData[CozeBotProperty.Messages]?.FirstOrDefault(x => x[CozeBotProperty.Type]!.ToString().Equals(CozeBotProperty.Answer))?[CozeBotProperty.Content];
                
                if (CheckNull(result!.ToString()) || IsJson(result!.ToString()))
                {
                    var movie = JsonConvert.DeserializeObject<MovieGemeni>(result!.ToString());
                    return new ResponseDTO
                    {
                        Status = HttpStatusCode.OK,
                        Message = MessageCommon.Complete,
                        Data = movie!
                    };
                }
            }

            return new ResponseDTO
            {
                Status = HttpStatusCode.InternalServerError,
                Message = MessageSystem.ServerError
            };
        }

        public static string CleanResult(string data)
        {
            data = data.Replace("`", "");
            data = data.Replace("json", "");
            return data;
        }

        public static bool IsJson(string str)
        {
            try
            {
                JsonDocument.Parse(str);
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public static bool CheckNull(string data)
        {
            Regex regex = new("null");
            MatchCollection matches = regex.Matches(data);
            if (matches.Count >= 2)
            {
                return true;
            }
            return false;
        }

    }
}
