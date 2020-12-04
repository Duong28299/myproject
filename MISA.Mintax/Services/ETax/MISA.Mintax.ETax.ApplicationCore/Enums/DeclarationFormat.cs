using System.Collections.Generic;
using static MISA.Mintax.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.Mintax.ETax.ApplicationCore.Entities
{
	public class DeclarationFormat
	{
		/// <summary>
		/// Tờ khai bổ sung
		/// </summary>
		public const string DeclarationAddition = "B";
		/// <summary>
		/// Tờ khai lần đầu
		/// </summary>
		public const string DeclarationFirst = "C";

		public const string KyThang = "M";

		public const string KyQuy = "Q";
		//Kỳ thanh toán theo năm
		public const string KyNam = "Y";

		public const string KyThanhToan = "K";
		/// <summary>
		/// Kì thanh toán tháng là 1. Merge dữ liệu số trong db với tên dữ liệu
		/// </summary>
		public const int TaxTypeMonth = 1;
		/// <summary>
		/// Kỳ thanh toán quý là 2.Merge dữ liệu số trong db với tên dữ liệu
		/// </summary>
		public const int TaxTypeQuater = 2;
		/// <summary>
		/// Kỳ thanh toán quý là 3.Merge dữ liệu số trong db với tên dữ liệu
		/// </summary>
		public const int TaxTypeKTT = 3;
		/// <summary>
		/// Kỳ thanh toán năm là 4.Merge dữ liệu số trong db với tên dữ liệu
		/// </summary>
		public const int TaxTypeYear = 0;

		public static Dictionary<int, string> TaxTypes = new Dictionary<int, string>
		{
			{1,"M" },
			{2,"Q" },
			{3, "Y"},
			{4,"K" }
		};
		#region
		//Dùng cho tờ khai 05/KK-TNCN 
		public static KeyValuePair<int, string> TaxType_Month = new KeyValuePair<int, string>(1, "M");
		//public const string maDvu = "AMIS MinTax";
		//public const string tenDVu = "AMIS Thuế TNCN";
		//public const string pbanDVu = "1.0";
		//public const string ttinNhaCCapDVu = "Công ty Cổ phần MISA";
		//public const string pbanTKhaiXML = "2.1.6";

		#endregion
		//public static Dictionary<int, KeyValuePair<string, string>> DeclarationIndentify = new Dictionary<int, KeyValuePair<string, string>>
		//{
		//	{ 5 , new KeyValuePair<string, string> (  "394" , "TK khấu trừ thuế thu nhập cá nhân Mẫu 05/KK-TNCN (TT92)" ) },
		//	{ 1 , new KeyValuePair<string, string> (  "389" , "Tờ khai khấu trừ thuế TNCN Mẫu 01/BHXSĐC (TT92)" ) },
		//	{ 20 , new KeyValuePair<string, string> (  "680" , "Đăng ký thuế TH cho CN có TN từ tiền lương, tiền công thông qua CQ chi trả TN" ) },
		//	{ 21 , new KeyValuePair<string, string> (  "680" , "Đăng ký thuế TH cho CN có TN từ tiền lương, tiền công thông qua CQ chi trả TN" ) },
		//	{ 22 , new KeyValuePair<string, string> (  "302" , "BẢNG TỔNG HỢP ĐĂNG KÝ NGƯỜI PHỤ THUỘC GIẢM TRỪ GIA CẢNH" ) }
		//};

		//public static Dictionary<int, KeyValuePair<string, string>> moTaBMau = new Dictionary<int, KeyValuePair<string, string>>
		//{
		//	{ 5 , new KeyValuePair<string, string> (  "394" , "(Ban hành kèm theo Thông tư số 92/2015/TT-BTC ngày 15/06/2015 của Bộ Tài chính)" ) },
		//	{ 1 , new KeyValuePair<string, string> (  "389" , "(Ban hành kèm theo Thông tư số 92/2015/TT-BTC ngày 15/06/2015 của Bộ Tài chính)" ) },
		//	{ 20 , new KeyValuePair<string, string> (  "680" , "Đăng ký thuế TH cho CN có TN từ tiền lương, tiền công thông qua CQ chi trả TN" ) },
		//	{ 21 , new KeyValuePair<string, string> (  "680" , "(Ban hành kèm theo Thông tư số 95/2016/TT-BTC ngày 28/6/2016 của Bộ Tài chính)" ) },
		//	{ 22 , new KeyValuePair<string, string> (  "302" , "(Ban hành kèm theo Thông tư số 156/2013/TT-BTC ngày 06/11/2013 của Bộ Tài chính)" ) }
		//};

		/// <summary>
		/// Chị Hoa confirm Mã GDTCode này không đổi nên đưa vào constant
		/// </summary>
		/// <GroupType,GDTCode>
		public static Dictionary<int, string> DeclarationConfig = new Dictionary<int, string>
		{
			{ 5 , "394"},
			{ 1 , "389" },
			{ 20 ,  "680" },
			{ 21 , "680" },
			{22,"302" }
		};
		/// <summary>
		/// Nhóm thuộc tờ khai 05
		/// </summary>
		public const int DeclarationGroup5 = 5;
		/// <summary>
		/// Nhóm thuộc tờ khai 01
		/// </summary>
		public const int DeclarationGroup1 = 1;
		/// <summary>
		/// Nhóm thuộc tờ khai chỉnh sửa MST
		/// </summary>
		public const int DeclarationGroup20 = 20;
		/// <summary>
		/// Nhóm thuộc tờ khai thêm mới MST
		/// </summary>
		public const int DeclarationGroup21 = 21;
		/// <summary>
		/// Nhóm thuộc tờ khai đăng kí/thay đổi NPT
		/// </summary>
		public const int DeclarationGroup22 = 22;
		/// <summary>
		/// Chưa gửi
		/// </summary>
		public const int NotSend = 1;
		/// <summary>
		/// Gửi MISA
		/// </summary>
		public const int SendMISASuccess = 2;
		/// <summary>
		/// Gửi MISA
		/// </summary>
		public const int SendMISAFailed = 3;
		/// <summary>
		/// Gửi TCT
		/// </summary>
		public const int SendGDT = 10;
		/// <summary>
		/// TCT chấp nhận
		/// </summary>
		public const int GDTSuccess = 12;
		/// <summary>
		/// TCT từ chối
		/// </summary>
		public const int GDTFailed = 11;
	}
	public class TaxRegistrationProfileConstant
	{
		//public const string maDvu = "AMIS MinTax";
		//public const string tenDVu = "AMIS Thuế TNCN";
		//public const string pbanDVu = "1.0";
		//public const string ttinNhaCCapDVu = "Công ty Cổ phần MISA";

		////public const string maTKhai = "680";
		//public const string tenTKhai = "Đăng ký thuế TH cho CN có TN từ tiền lương, tiền công thông qua CQ chi trả TN";
		////public const string moTaBMau = "(Ban hành kèm theo Thông tư số 95/2016/TT-BTC ngày 28/6/2016 của Bộ Tài chính)";
		//public const string pbanTKhaiXML = "1.0.0";
		public const int nhanKQQuaBuuDien = 0;
		public const int EditTaxRegistrationProfile = 0;
		public const int AddTaxRegistrationProfile = 1;
		public const int Female = 1;
		public const int Male = 2;
		public const string CCCD = "2080";

		public static Dictionary<int, string> Gender = new Dictionary<int, string>
		{
			{ 1, "Nữ"},
			{ 2, "Nam" }
		};
	}


	/// <summary>
	/// Const của hồ sơ người phụ thuộc
	/// </summary>
	public class DependentProfileConstant
	{
		//public const string maDvu = "AMIS MinTax";
		//public const string tenDVu = "AMIS Thuế TNCN";
		//public const string pbanDVu = "1.0";
		//public const string ttinNhaCCapDVu = "Công ty Cổ phần MISA";
		//public const string pbanTKhaiXML = "2.0.6";
		/// <summary>
		/// Báo tăng
		/// </summary>
		public const int Increase = 0;
		/// <summary>
		/// Báo giảm
		/// </summary>
		public const int Decrease = 1;
	}

	/// <summary>
	/// Const của hồ sơ quyết toán thuế
	/// </summary>
	public class DeclarationSettlementConstant
	{
		public const int GDTCode = 395;
		/// <summary>
		/// Lần đầu
		/// </summary>
		public const int Initial = 1;
		/// <summary>
		/// Bổ sung
		/// </summary>
		public const int Additional = 2;
		public const int ChangeData = 1;
		public const int NoChangeData = 0;
		/// <summary>
		/// Cá nhân người nước ngoài ủy quyền QTT
		/// </summary>
		public const int AuthorizeForeign = 1;

		/// <summary>
		/// Cá nhân người nước ngoài không ủy quyền QTT
		/// </summary>
		public const int NoAuthorizeForeign = 0;
		/// <summary>
		/// Tờ khai quyết toán thuế lần đâu
		/// </summary>
		public const int InitialDeclarationSettlemnet = 0;

		/// <summary>
		/// Tờ khai quyết toán thuế lần đâu
		/// </summary>
		public const int AdditionalDeclarationSettlemnet = 0;
		/// <summary>
		/// Cá nhân  ủy quyền QTT
		/// </summary>
		public const int Authorize = 1;

		/// <summary>
		/// Cá nhân  không ủy quyền QTT
		/// </summary>
		public const int NoAuthorize = 0;

		public const int IndividualResident = 1;

		public const int NoneIndividualResident = 0;


	}

	/// <summary>
	/// Response build tờ khai
	/// </summary>
	public class BindDataRes
	{
		public string DeclarationID { get; set; }
		public string Base64Data { get; set; }
		public string UserMessage { get; set; }
		public string DevMessage { get; set; }
		public MISACode MISACode { get; set; }


		public BindDataRes()
		{
			MISACode = MISACode.Success;
			Base64Data = null;
		}

		public BindDataRes(string declarationID, string base64Data, string userMessage, string devMessage, MISACode mISACode)
		{
			DeclarationID = declarationID;
			Base64Data = base64Data;
			UserMessage = userMessage;
			DevMessage = devMessage;
			MISACode = mISACode;
		}
	}
}