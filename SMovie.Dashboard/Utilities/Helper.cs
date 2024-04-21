using SMovie.Dashboard.Constants;
using SMovie.Domain.Constants;

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

    }
}
