using SMovie.Application.Extension;
using SMovie.Application.MessageService;
using SMovie.Dashboard.Constants;
using SMovie.Domain.Constants;
using SMovie.Domain.Enum;

namespace SMovie.Dashboard.Utilities
{
    public class Helper
    {
        public static string ConvertStatusToClass(string status)
        {
            if(status.Equals(MovieStatus.Upcoming))
            {
                return "badge bg-soft-warning text-upcoming";
            } else if(status.Equals(MovieStatus.Pending))
            {
                return "badge bg-soft-info text-pending";
            } else if(status.Equals(MovieStatus.Released))
            {
                return "badge bg-soft-success text-released";
            }

            return "";
        }

        public static string? GetIconCategory(int categoryId)
        {
            return IconDefault.IconCategories.ElementAtOrDefault(categoryId);
        }

        public static MethodType GetMethodType(string method)
        {
            if(method.ToLower().Equals("get"))
            {
                return MethodType.Read;
            } else if(method.ToLower().Equals("post"))
            {
                return MethodType.Create;
            } else if(method.ToLower().Equals("put"))
            {
                return MethodType.Update;
            } else if(method.ToLower().Equals("delete"))
            {
                return MethodType.Delete;
            }

            throw new NotFoundException(MessageException.TypeNotFound);
        }

        public static string GetTableUsingAction(string action)
        {
            /*IEnumerable<string> actions = new List<string>
            {
                "Index",
                "CreateMoviePage",
                "CreateAccountPage",
                "ListMovieDeleted",
                "MovieList",
                "IndexAsync",
                "IpAddress"
            };*/

            IEnumerable<string> tableUser = new List<string>
            {
                "CreateAccount",
                "DeleteAccount",
                "EditAccount",
                "ChangePassword",
                "RestoreAccount",
                "BlockAccount"
            };

            IEnumerable<string> tableMovie = new List<string>
            {
                "CreateMovie",
                "EditMovie",
                "DeleteMovie",
                "RestoreMovie",
                "UpdateStatusMovie"
            };

            IEnumerable<string> tablePerson = new List<string>
            {
                "CreatePerson",
                "EditPerson",
                "DeletePerson"
            };

            if (tableUser.Any(t => t.Equals(action)))
            {
                return "User";
            } else if (tableMovie.Any(t => t.Equals(action)))
            {
                return "Movie";
            } else if (tablePerson.Any(t => t.Equals(action)))
            {
                return "Person";
            }

            return string.Empty;
        }

    }
}
