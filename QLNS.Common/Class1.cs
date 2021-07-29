using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS.Common
{
    class Class1
    {
        //convert date time type
        public static DateTime ToDateTime(string s);

        //convert string to int

        //convert datatable to list 
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        //convert number fomat

        //convert 


        //
    }
}
