using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersAPI.Actions
{
    public static class UserActions
    {
        public const string UPDATE_USERS = "updateUsers";
        public const string GET_ONLINE_USERS = "getOnlineUsers";
        public const string LOGIN_USER = "loginUser";
        public const string REGISTER_USER = "registerUser";
        public const string USER_NOT_FOUND = "userNotFound";
        public const string USER_ALREADY_EXISTS = "userAlreadyExists";
        public const string LOGIN_SUCCESSFULL = "loginSuccessfull";
        public const string REGISTRATION_SUCCESSFULL = "registrationSuccessfull";
        public const string INCORRECT_PASSWORD = "incorrectPassword";
        public const string USER_ALREADY_LOGIN = "userAlreadyLogin";
    }
}
