using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS.Common
{
    public class Utilities
    {
        public static List<SelectListModel> GetPeriod()
        {
            var types = new List<SelectListModel>(){
                new SelectListModel { Value ="Admin", Text= "Admin" },
                new SelectListModel { Value ="User", Text= "User" },
            };
            return types;
        }

        public static string MD5Encrypt(string plainText)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = new UTF8Encoding().GetBytes(plainText);
            return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", "").ToLower();
        }

    }
}
