using SMovie.Domain.Constants;

namespace SMovie.Dashboard.Utilities
{
    public class Helper
    {
        public static string ConvertStatusToClass(string status)
        {
            if(status.Equals(MovieStatus.Upcoming))
            {
                return "badge bg-soft-warning text-warning";
            } else if(status.Equals(MovieStatus.Pending))
            {
                return "badge bg-soft-info text-info";
            } else if(status.Equals(MovieStatus.Released))
            {
                return "badge bg-soft-success text-success";
            }

            return "";
        }

        /*public static string ConvertCategoryToClass()
        {

        }*/

    }
}
