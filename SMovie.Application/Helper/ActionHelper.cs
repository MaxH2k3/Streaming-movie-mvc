namespace SMovie.Application.Helper
{
    public class ActionHelper
    {
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

            if(tableUser.Any(t => t.Equals(action)))
            {
                return "User";
            } else if(tableMovie.Any(t => t.Equals(action)))
            {
                return "Movie";
            } else if(tablePerson.Any(t => t.Equals(action)))
            {
                return "Person";
            }

            return string.Empty;
        }
    }
}
