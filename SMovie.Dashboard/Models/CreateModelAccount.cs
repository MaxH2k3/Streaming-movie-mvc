using SMovie.Domain.Models;

namespace SMovie.Dashboard.Models
{
    public class CreateModelAccount
    {
        public RegisterUser RegisterUser { get; set; } = new RegisterUser();
        public IEnumerable<UserChosen> Users { get; set; } = new List<UserChosen>();
    }
}
