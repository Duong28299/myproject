using MISA.HCT.ETax.ApplicationCore.Properties;
using static MISA.HCT.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.HCT.ETax.ApplicationCore.Entities
{
	public class ActionServiceResult
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public MISACode MISACode { get; set; }
		public object Data { get; set; }

		/// <summary>
		/// Hàm khởi tạo mặc định
		/// </summary>
		public ActionServiceResult()
		{
			Success = true;
			Message = Resources.Success;
			MISACode = MISACode.Success;
			Data = null;
		}

		/// <summary>
		/// Hàm khởi tạo mặc định với các tham số truyền vào
		/// </summary>
		/// created by lttuan 23 10 2020
		public ActionServiceResult(bool _Success, string _Message, MISACode _MisaCode, object _Data=null)
		{
			Success = _Success;
			Message = _Message;
			MISACode = _MisaCode;
			Data = _Data;
		}
	}

	public class InsertDeclarationSettleResponse :ActionServiceResult
	{
		public bool DuplicationDeclaration { get; set; }

		public InsertDeclarationSettleResponse(bool success, string message, MISACode misaCode, object data, bool duplicate)
		{
			Success = success;
			MISACode = misaCode;
			Data = data;
			DuplicationDeclaration = duplicate;
			Message = message;
		}
	}
}
