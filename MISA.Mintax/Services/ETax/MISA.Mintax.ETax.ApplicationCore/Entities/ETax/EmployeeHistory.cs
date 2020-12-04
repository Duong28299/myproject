using System;
using System.Collections.Generic;
using System.Text;
namespace MISA.Mintax.ETax.ApplicationCore.Entities
{
    public class EmployeeHistory : BaseEntity
    {
        public EmployeeHistory()
        {
        }

        #region Thông tin TemporyTaxDetail
        /// <summary>
        /// ID chi tiết bảng tính thuế
        /// </summary>
        public Guid? TemporaryTaxDetailID { get; set; }
        /// <summary>
        /// Id bảng tính thuế
        /// </summary>
        public Guid TemporaryTaxID { get; set; }
        /// <summary>
        /// Tổng thu nhập
        /// </summary>
        public decimal TotalIncome { get; set; }
        /// <summary>
        /// Thu nhập không chịu thuế
        /// </summary>
        public decimal IncomeNotTaxable { get; set; }
        /// <summary>
        /// Thu nhập chịu thuế
        /// </summary>
        public decimal IncomeTaxable { get; set; }

        public decimal TaxIncurred { get; set; }
        /// <summary>
        /// Số tiền đóng bảo hiểm
        /// </summary>
        public decimal InsurancePayment { get; set; }
        /// <summary>
        /// Số tiền giảm trừ khác
        /// </summary>
        public decimal OtherReduction { get; set; }
        /// <summary>
        /// Số người phụ thuộc
        /// </summary>
        public int DependentNumber { get; set; }
        /// <summary>
        /// Giảm trừ người phụ thuộc
        /// </summary>
        public decimal DependentReduction { get; set; }
        /// <summary>
        /// Giảm trừ bản thân
        /// </summary>
        public decimal SelfReduction { get; set; }
        /// <summary>
        /// Tổng giảm trừ gia cảnh
        /// </summary>
        public decimal TotalReduction { get; set; }
        /// <summary>
        /// Tổng thu nhập tính thuế
        /// </summary>
        public decimal TotalIncomeTaxable { get; set; }
        /// <summary>
        /// ID liên kết loại thuế áp dụng
        /// </summary>
        public int? TaxInfoID { get; set; }

        /// <summary>
        /// Biểu thuế áp dụng
        /// </summary>
        public string TariffsApply { get; set; }
        /// <summary>
        /// Có chịu thuế hay không: false-Không, true- Có
        /// </summary>
        public ulong? IsTaxable { get; set; }
        /// <summary>
        /// Tổng thuế TNCN khấu trừ
        /// </summary>
        public decimal TotalTaxWithHeld { get; set; }
        /// <summary>
        /// ID người lao động
        /// </summary>
        public Guid? EmployeeID { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Họ
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Họ và tên đầy đủ
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Đại diện cho ai? tổ chức nào
        /// </summary>
        public string RepresentFor { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxNo { get; set; }
        /// <summary>
        /// Email liên hệ
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Điện thoại liên hệ
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Tên vị trí/Chức vụ
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// ID vị trí/Chức vụ
        /// </summary>
        public Guid? PositionID { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public ulong? Gender { get; set; }
        /// <summary>
        /// ID Quốc gia
        /// </summary>
        public int? NationalityID { get; set; }
        /// <summary>
        /// Tên bộ phân phòng ban
        /// </summary>
        public string OrganizationUnitName { get; set; }
        /// <summary>
        /// ID bộ phân phòng ban
        /// </summary>
        public Guid? OrganizationUnitID { get; set; }
        /// <summary>
        /// Dạng giấy tờ 
        /// </summary>
        public Guid? IndentityType { get; set; }
        /// <summary>
        /// ID của dân tộc
        /// </summary>
        public int? EthnicID { get; set; }
        /// <summary>
        /// Số chứng minh thư nhân dân/hộ chiếu
        /// </summary>
        public string CitizenIdentityNo { get; set; }
        /// <summary>
        /// Nơi cấp chứng minh thư nhân dân
        /// </summary>
        public int? CitizenIdentityPlaceID { get; set; }
        /// <summary>
        /// Ngày cấp chứng minh thư nhân dân/hộ chiếu
        /// </summary>
        public DateTime? CitizenIdentityDate { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàngg
        /// </summary>
        public string BankAccountNo { get; set; }
        /// <summary>
        /// Tên chủ tài khoản ngân hàng
        /// </summary>
        public string BankAccountName { get; set; }
        /// <summary>
        /// 1- Hợp đông lao động trên 3 tháng,0: Hợp đồng lao động dưới 3 tháng
        /// </summary>
        public ulong ContractOffical { get; set; }

        /// <summary>
        /// Cá nhân cư trú hay không
        /// </summary>
        public ulong IndividualResident { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Có cần tính toán lại không
        /// </summary>
        public DateTime ReCalculator { get; set; }

        /// <summary>
        /// Xác nhận dùng khi thêm mới
        /// </summary>
        public int Confirm { get; set; }

        public Guid? OrganizationID { get; set; }

        #endregion


        #region Thông tin TemporyTax
        /// <summary>
        /// Năm bảng tạm tính
        /// </summary>
        public int PeriodYear { get; set; }

        /// <summary>
        /// Nếu TaxType = 2(quý)  thì nhận giá trj 1 - 4; nếu = 1 (tháng) thì nhận giá trị 1 - 12
        /// </summary>
        public int? PeriodMonthOrQuarter { get; set; }

        /// <summary>
        /// Loại tính thuế:2 - Tính thuế theo quý; = 1  tính thuế theo tháng 
        /// </summary>
        public int TemporaryTaxType { get; set; }

        #endregion

    }
}