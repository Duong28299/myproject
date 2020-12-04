namespace MISA.Mintax.ETax.ApplicationCore.Enums
{
	/// <summary>
	/// Lưu trữ các store sử dụng chung trong hệ thống
	/// </summary>
	/// Created By NVBINH (27/07/2020)
	public class Enumeration
	{
		/// <summary>
		/// Tên kiểu store sẽ thực thi
		/// </summary>
		/// CreatedBy: NVBINH (27/07/2020)
		public enum ProcdureTypeName
		{
			/// <summary>
			///  Lấy dữ liệu
			/// </summary>
			Get,

			/// <summary>
			/// Lấy dữ liệu theo khóa chính
			/// </summary>
			GetById,

			/// <summary>
			/// Thêm mới
			/// </summary>
			Insert,

			/// <summary>
			/// Sửa/ cập nhật dữ liệu
			/// </summary>
			Update,

			/// <summary>
			/// Xóa dữ liệu
			/// </summary>
			Delete,

			/// <summary>
			/// Lấy dữ liệu có phân trang
			/// </summary>
			GetPaging
		}

		/// <summary>
		/// Trạng thái trùng người nộp thuế trong phụ lục 02-BK
		/// </summary>
		/// created by vdthang 13.11.2020
		public enum CheckDeclarationSettlementDetail
		{
			Citizen = 1,
			TaxNo = 2
		}

		/// <summary>
		/// Kiểu phương thức 
		/// </summary>
		public enum EntityState
		{
			/// <summary>
			/// Lấy dữ liệu
			/// </summary>
			GET,

			/// <summary>
			/// Thêm mới dữ liệu
			/// </summary>
			INSERT,

			/// <summary>
			/// Sửa dữ liệu
			/// </summary>
			UPDATE,

			/// <summary>
			/// Xóa dữ liệu
			/// </summary>
			DELETE
		}

		/// <summary>
		/// Các mã lỗi
		/// </summary>
		public enum MISACode
		{
			Success = 200,
			Created = 201,
			/// <summary>
			///  Cảnh báo thay đổi thông tin
			/// </summary>
			Warning = 300,
			WarningUpdateData = 310,

			/// <summary>
			/// Cảnh báo không update được tờ khai thuế
			/// </summary>
			ErrorUpdateDeclaration = 301,

			/// <summary>
			/// Cảnh báo không thêm mới được tờ khai thuế
			/// </summary>
			ErrorAddDeclaration = 302,

			/// <summary>
			/// Tờ khai thuế không tìm thấy
			/// </summary>
			DeclarationNotFound = 303,

			/// <summary>
			/// Lỗi validate dữ liệu chung
			/// </summary>
			Validate = 400,

			/// <summary>
			/// Lỗi validate dữ liệu không hợp lệ
			/// </summary>
			ValidateEntity = 401,

			/// <summary>
			/// Lỗi validate dữ liệu do không đúng nghiệp vụ
			/// </summary>
			ValidateBussiness = 402,

			/// <summary>
			/// Lỗi không tìm thấy
			/// </summary>
			NotFound = 404,

            /// <summary>
            /// Lỗi đã tồn tại 
            /// </summary>
            Exist = 405,

			/// <summary>
			/// Không tồn tại tổ chức
			/// </summary>
			ValidateOrganization = 450,

			/// <summary>
			/// Không khả dụng
			/// </summary>
			NotActive = 451,

			/// <summary>
			/// Lỗi Exception
			/// </summary>
			Exception = 500,

			/// <summary>
			/// Lỗi Trùng dữ liệu
			/// </summary>
			Duplicate = 501,

			/// <summary>
			/// Lỗi File không đúng định dạng
			/// </summary>
			FileFormat = 600,

			/// <summary>
			/// Lỗi File import không đúng định dạng
			/// </summary>
			ImportFileFormat = 601,

			/// <summary>
			/// Lỗi File Export không đúng định dạng
			/// </summary>
			ExportFileFormat = 602,

			/// <summary>
			/// Lỗi thêm mới entity
			/// </summary>
			ErrorAddEntity = 603,

			/// <summary>
			/// Lỗi xóa entity
			/// </summary>
			ErrorDeleteEntity = 604,

			/// <summary>
			/// Lỗi cập nhật entity
			/// </summary>
			ErrorUpdateEntity = 605,
			ErrorBinding = 999,
			ConfirmUpdate=789,
			NotEnough = 502
		}
		public enum AMISCode
		{
			/// <summary>
			/// Thành công
			/// </summary>
			SUCCESS = 0,
			/// <summary>
			/// Có lỗi xảy ra (lỗi đã được kiểm soát như các lỗi validate)
			/// </summary>
			ERROR = 99,
			/// <summary>
			/// Lỗi hệ thống (Exception)
			/// </summary>
			EXCEPTION = 999,

		}

		public enum GroupType
		{
			/// <summary>
			/// Nhóm thuộc tờ khai 01 Xổ số bảo hiểm đa cấp
			/// </summary>
			XSBHDC = 1,
			/// <summary>
			/// Nhóm thuộc tờ khai 01 Kê khai cho hoạt động cho thuê tài sản
			/// </summary>
			TTS = 2,
			/// <summary>
			/// Nhóm thuộc tờ khai 05 Kê Khai thuế từ tiền lương,tiền công
			/// </summary>
			TLTL = 5,

			/// <summary>
			/// Nhóm thuộc tờ khai 06 Kê khai cho đầu tư vốn, chuyển nhượng chứng khoán, từ bản quyền, chuyển nhượng thương mai
			/// </summary>
			DTVCNTT = 6,

			/// <summary>
			/// Nhóm tờ khai thay đổi thông tin mã số thuế
			/// </summary>
			TDTTDKMST = 20,

			/// <summary>
			/// Nhóm tờ khai đăng kí mã số thuế
			/// </summary>
			DKTTMST = 21,

			/// <summary>
			/// Nhóm tờ khai đăng kí người phụ thuộc
			/// </summary>
			DKNPT = 22,



		}

        public enum IncomeType
        {
            TLTC = 1,
            BHNTDNNG = 2,
            XS = 3,
            BH = 4,
            DC = 5,
            TLBHNT = 6,

		}
		/// <summary>
		/// Các enum ServerError
		/// </summary>
		public enum ServerEror
		{
			DUPLICATE = 1062,
		}

        /// <summary>
        /// Các trường kết quả
        /// </summary>
        /// Creatd by NVBINH 15/08/2020
        public enum ResultCode
        {
            /// <summary>
            /// Thành công
            /// </summary>
            SUCCESS = 1,
            /// <summary>
            /// Lỗi
            /// </summary>
            FALSE = 2,

			/// <summary>
			/// Không tồn tại
			/// </summary>
			NOT_EXIST = 3,

				

			ERROR_UPDATE = 4,

            ERROR_INSERT = 5,


			/// <summary>
			/// Lỗi ngoại lệ, 
			/// Nếu có vòng lặp thì thoát khỏi vòng lặp
			/// </summary>
			EXCEPTION = 6,

			/// <summary>
			/// Thông tin tờ khai thuế không tìm thấy
			/// </summary>
			DECLARATION_NOTFOUND = 10,

            /// <summary>
            /// Thông tin bảng tính thuế không tìm thấy
            /// </summary>
            TEMPORARYTAX_NOTFOUND = 20,
			NOCHANGE=99
		}


      
 
        public enum TaxInfoStatus
        {
            SELF_REDUCTION = 2,
            DEPENDENT_REDUCTION = 3,
            LEVEL_TAXPERCENT = 1,

        }

		public enum DeclarationSettlementDetailType
        {
			BK01 = 0,
			BK02 = 1
        }
            
		public enum DeleteStatus
        {
			Success = 1,
			NotFound = 0,
			Exist = 2,
			NotValidate = 3
        }
        
        /// <summary>
        /// Các trường giá trị mặc định của DeclrationSettlenment
        /// </summary>
        /// created by lttuan 23 10 2020 
        public enum Status
        {
            #region Giá trị của trạng thái tờ khai DeclarationStatus
            /// <summary>
            /// Tờ khai chưa gửi
            /// </summary>
            DeclarationSettlementNotSend = 1,
            #endregion

            #region Giá trị của ConfirmUpdate
            /// <summary>
            /// Giá trị mặc định của conffirmUpdate, nếu tờ khai bị trùng thì trả về thông báo
            /// </summary>
            DefaultConfirmUpdate = 0,

            /// <summary>
            /// Nếu tờ khai bị trùng, thì ghi đè
            /// </summary>
            UpdateSettlement = 1,

            /// <summary>
            /// Nếu tờ khai bị trùng, thì tạo tờ khai mới
            /// </summary>
            NewSettlement = 2,
            #endregion

            #region Giá trị của loại tờ khai DelarationType
            /// <summary>
            /// Tờ khai có loại là lần đầu
            /// </summary>
            FirstDeclarationSettlement = 1,

            /// <summary>
            /// Tờ khai có loại là bổ sung
            /// </summary>
            AdditionalDeclarationSettlement = 2,
            #endregion

            #region Giá trị của loại cá nhân cư trú hay k
            /// <summary>
            /// Cá nhân có cư trú
            /// </summary>
            Resident = 1,

            /// <summary>
            /// Cá nhân không cư trú
            /// </summary>
            NotResident = 0,
            #endregion

            #region Giá trị của loại phụ lục
            /// <summary>
            /// phụ lục 1 bk
            /// </summary>
            Type1BK = 0,

            /// <summary>
            /// phụ lục 2bk
            /// </summary>
            Type2BK = 1,
            #endregion

            #region Giá trị của ủy quyền quyết toán thay
            /// <summary>
            /// Không ủy quyền
            /// </summary>
            NotAuthorize = 0,

            /// <summary>
            /// Có Ủy quyền 
            /// </summary>
            Authorize =1,
            #endregion

            #region Giá trị hợp đồng trên 3 tháng?
            //Trên 3 tháng
            ContractOffical = 1,

            //dưới 3 tháng
            NoContractOffical = 0,

			/// <summary>
			/// Thu nhập vãng lai
			/// </summary>
			CurrentIncome = 1,

			/// <summary>
			/// Thu nhập từ tiền công tiền lương
			/// </summary>
			IncomeFromWgesAndSalaries=0
			#endregion

		}

	}
}
