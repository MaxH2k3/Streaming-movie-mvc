namespace SMovie.Application.MessageService
{
    public class MessageUser
    {
        // Account Message
        public const string UserNameOrEmailNotExisted = "UserName or Email not existed!";
        public const string WrongPassword = "Wrong password!";
        public const string UserBlocked = "Your account is blocked by admin!";
        public const string LoginSuccessfully = "Login successfully!";
        public const string UserNameExisted = "UserName have been existed!";
        public const string EmailExisted = "Email have been existed!";

        // User Message
        public const string UserNotFound = "User not found!";
        public const string NotFoundAccountToVerify = "Not found account to verify!";
        public const string FailToVerifyCode = "Fail to verify code!";
        public const string FailToVerifyToken = "Fail to verify token!";
        public const string CodeExpired = "Code expired!";
        public const string TokenExpired = "Token expired!";
        public const string VerifySuccessfully = "Verify successfully!";
        public const string FailToVerify = "Fail to verify!";
    }
}
