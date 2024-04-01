using SMovie.Domain.Models;

namespace SMovie.WebUI.Models
{
    public class CreateModelAccount
    {
        public RegisterUser RegisterUser { get; set; } = new RegisterUser();
        public IEnumerable<UserChosen> Users { get; set; } = new List<UserChosen>();
    }
}
