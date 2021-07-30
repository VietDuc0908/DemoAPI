using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS.Common
{
    public static class Constants
    {
        public static class Validation
        {
            public static class Message
            {
                public const string Required = "Required";
            }
        }

        public static class Role
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }

        public enum ResponseStatusCode
        {
            Success = 1,
            Fail = 0,
            Notfound = 404,
            TooManyRequest = 429,
            Unauthorized = 401
        }
    }

    public static class FilterInfo
    {
        public static List<string> EqualStrings = new List<string> { "eq" };

        public static List<string> IsNullStrings = new List<string> { "isnull" };

        public static List<string> NotNullStrings = new List<string> { "isnotnull" };

        public static List<string> GreaterOrEqualStrings = new List<string> { "gte" };

        public static List<string> LessOrEqualStrings = new List<string> { "lte" };

        public static List<string> ContainsStrings = new List<string> { "contains" };
    }
}
