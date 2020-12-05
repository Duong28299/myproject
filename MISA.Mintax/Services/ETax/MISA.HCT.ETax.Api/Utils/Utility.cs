using MISA.HCT.ETax.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.HCT.ETax.Api.Utils
{
	public class Utility
	{
		/// <summary>
		/// Convert base64 sang string
		/// </summary>
		/// <param name="base64"></param>
		/// <returns></returns>
		public static string DecodeStringFromBase64(string base64)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
		}
		/// <summary>
		/// Convert string sang base64
		/// </summary>
		/// <param name="plainText"></param>
		/// <returns></returns>
		public static string EncodeStringToBase64(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
		/// <summary>
		/// Trả về ngày tháng bắt đầu của quý
		/// </summary>
		/// <param name="year">Năm</param>
		/// <param name="quater">Qúy</param>
		/// <returns></returns>
		public static string GetFirst(int year, int quater)
		{
			return $"01/{(quater * 3 - 2)}/{year}";
		}
		/// <summary>
		/// Trả về ngày,tháng cuois cùng của quý
		/// </summary>
		/// <param name="year"></param>
		/// <param name="quater"></param>
		/// <returns></returns>
		public static string GetLast(int year, int quater)
		{
			DateTime dateTime = new DateTime(year, quater * 3 - 2, 1);
			var temp = dateTime.AddMonths(3).AddDays(-1);
			return $"{temp.Day}/{temp.Month}/{temp.Year}";
		}

		public static string DateTimeToString(DateTime dateTime)
		{
			return $"{dateTime.Day}/{dateTime.Month}/{dateTime.Year}";
		}
		public static DateTime? TryParseTime(string value, string format = "")
		{
			DateTime dateValue;
			if (format == "")
			{
				if (DateTime.TryParse(value, out dateValue))
					return dateValue;
			}
			else
			{
				if (DateTime.TryParseExact(value, format,
					CultureInfo.InvariantCulture,
					   DateTimeStyles.None, out dateValue))
					return dateValue;
			}
			return null;
		}		
		/// <summary>
		/// Chuyển từ DateTime sang String
		/// </summary>
		/// <param name="value">Giá trị datetime</param>
		/// <param name="format">Dạng format</param>
		/// <returns></returns>
		public static string TryParseTimeToString(DateTime? value, string format = "")
		{
			string dateValue;
			if (format == "")
			{
				dateValue = Convert.ToDateTime(value).ToString();
			}
			else
			{
				dateValue = Convert.ToDateTime(value).ToString(format);
			}
			return dateValue;
		}


		/// <summary>
		/// Check validate 
		/// CreatedBy NVANH 10.11.2020
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="listname"></param>
		/// <returns></returns>
		public static bool CheckValidateObject<T>(T obj, string[] listname) where T : BaseEntity
        {
			foreach(var name in listname)
            {
				var item = obj.GetType().GetProperty(name).GetValue(obj);

				if (item.GetType().Equals(typeof(int)))
                {
                    if ((int)item < 0)
                    {
						return false;
                    }
                }
				if(item.GetType().Equals(typeof(Guid)))
                {
                    if (Guid.Empty.Equals(item))
                    {
						return false;
                    }
                }					

				if(item.GetType() == null)
                {
					return false;
                }
					
            }
			return true;
        }
		
	}
}
