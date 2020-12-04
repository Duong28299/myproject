using System;
using System.Text;

namespace MISA.Mintax.ETax.Api.Utils
{
	/// <summary>
	/// Chuyển tiếng việt có dấu sang không dấu
	/// </summary>
	public class ConvertUnsign
	{
		private static readonly string[] VietNamChar = new string[]
		{
			"aAeEoOuUiIdDyY",
			"áàạảãâấầậẩẫăắằặẳẵ",
			"ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
			"éèẹẻẽêếềệểễ",
			"ÉÈẸẺẼÊẾỀỆỂỄ",
			"óòọỏõôốồộổỗơớờợởỡ",
			"ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
			"úùụủũưứừựửữ",
			"ÚÙỤỦŨƯỨỪỰỬỮ",
			"íìịỉĩ",
			"ÍÌỊỈĨ",
			"đ",
			"Đ",
			"ýỳỵỷỹ",
			"ÝỲỴỶỸ"
		};
		/// <summary>
		/// Hàm chuyển sang không dấu
		/// </summary>
		/// <param name="str">chuỗi cần chueyern không dấu</param>
		/// <returns></returns>
		public static string converToUnsign(string str)
		{
			//Thay thế và lọc dấu từng char      
			for (int i = 1; i < VietNamChar.Length; i++)
			{
				for (int j = 0; j < VietNamChar[i].Length; j++)
					str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
			}
			return str.ToLower();
		}

		
	}
}
