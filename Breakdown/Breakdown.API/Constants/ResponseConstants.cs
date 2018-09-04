using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.Constants
{
    public static class ResponseConstants
    {
        public static string InvalidCredentials = "Invalid email or password.";

        public static string UserNotFound = "Cannot find a user matching to information provided.";

        public static string PasswordResetCodeSentSuccess = "Password Reset Code Sent.";

        public static string PasswordResetSuccess = "Password Reset Successfully.";

        public static string NotFound = "Not Found.";

        public static string AlreadyExists = "Already Exists.";

        public static string ValidationFailure = "Validation Failure.";

        public static string InternalServerError = "Internal Server Error.";

        public static string RequestContentNull = "Request Content Null.";

        public static string NoContent = "No Content.";

        public static string UserNull = "User Null.";

        public static string InvalidData = "Contains Invalid Data.";
    }
}
