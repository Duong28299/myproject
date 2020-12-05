using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.HCT.ETax.ApplicationCore.Helpers
{
    public class ValidateEnitty<T>
    {
        /// <summary>
        /// Validate dữ liệu đầu vào
        /// </summary>
        /// <param name="enitty"></param>
        /// <param name="propertyValidate"></param>
        /// created by nvbinh2 21.11.2020
        /// <returns></returns>
        public static bool ValidateData(T enitty, List<string> propertyValidate)
        {
            if(enitty == null)
            {
                return false;
            }

            foreach(var ele in propertyValidate)
            {
                var propertyEle = enitty.GetType().GetProperty(ele).GetValue(enitty);
                if(propertyEle == null)
                {
                    return false;
                }
            }
            return true; 
        }

        //public static bool ValidateDataOut(T entity)
    }
}
