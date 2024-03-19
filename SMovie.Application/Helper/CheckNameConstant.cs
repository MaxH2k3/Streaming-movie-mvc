using SMovie.Domain.Constants;

namespace SMovie.Application.Helper
{
    public class CheckNameConstant
    {
        public static string CheckRoleName(string roleName)
        {
            if (roleName.ToLower().Equals(Role.RoleActor.ToLower()))
            {
                return Role.RoleActor;
            } else if (roleName.ToLower().Equals(Role.RoleProducer.ToLower()))
            {
                return Role.RoleProducer;
            }

            return string.Empty;
        }
    }
}
