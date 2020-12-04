using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Mintax.ETax.ApplicationCore.Entities
{
    public partial class Employee : BaseEntity
    {
        public Guid? EmployeeID  { get;set;}
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get;set;}
        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName   { get;set;}
        /// <summary>
        /// Họ
        /// </summary>
        public string LastName   { get;set;}
        /// <summary>
        /// Họ và tên đầy đủ
        /// </summary>
        public string FullName  { get;set;}

        /// <summary>
        /// Đại diện cho ai? tổ chức nào
        /// </summary>
        public string RepresentFor { get;set;}

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxNo   { get;set;}

        /// <summary>
        /// Email liên hệ
        /// </summary>
        public string Email   { get;set;}

        /// <summary>
        /// Điện thoại liên hệ
        /// </summary>
        public string Phone   { get;set;}
        /// <summary>
        /// ID vị trí/Chức vụ
        /// </summary>
        public Guid? PositionID  { get;set;}

        public string PositionName { get; set; }


        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime?  DateOfBirth { get;set;}

        /// <summary>
        /// Giới tính
        /// </summary>
        public ulong?  Gender  { get;set;}

        /// <summary>
        /// ID Quốc gia
        /// </summary>
        public int?  NationalityID { get;set;}

        /// <summary>
        /// Tên bộ phân phòng ban
        /// </summary>
        public string   OrganizationUnitName { get;set;}
        public Guid? OrganizationUnitID { get; set; }


        /// <summary>
        /// ID tổ chức
        /// </summary>
        public Guid?   OrganizationID { get;set;}
        
        /// <summary>
        /// ID của dân tộc
        /// </summary>
        public int?    EthnicID { get;set;}
        /// <summary>
        /// Dạng giấy tờ 
        /// </summary>
        public Guid? IndentityType { get; set; }

        /// <summary>
        /// Số chứng minh thư nhân dân/hộ chiếu
        /// </summary>
        public string CitizenIdentityNo { get;set;}

        /// <summary>
        /// Nơi cấp chứng minh thư nhân dân
        /// </summary>
        public int? CitizenIdentityPlaceID { get;set;}

        /// <summary>
        /// Ngày cấp chứng minh thư nhân dân/hộ chiếu
        /// </summary>
        public DateTime? CitizenIdentityDate  { get;set;}

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get;set;}

        /// <summary>
        /// ID ngân hàng
        /// </summary>
        public Guid? BankID { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàngg
        /// </summary>
        public string BankAccountNo    { get;set;}

        /// <summary>
        /// Tên chủ tài khoản ngân hàng
        /// </summary>
        public string BankAccountName  { get;set;}

        /// <summary>
        /// 1- Hợp đông lao động trên 3 tháng,0: Hợp đồng lao động dưới 3 tháng
        /// </summary>
        public ulong? ContractOffical { get;set;}

        public ulong? IndividualResident { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string   CreatedBy { get;set;}

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime?   CreatedDate { get;set;}
        /// <summary>
        /// người sửa cuối cùng
        /// </summary>
        public string   ModifiedBy { get;set;}
        /// <summary>
        /// Ngày sửa cuối cùng
        /// </summary>
        public DateTime?   ModifiedDate { get;set;}

        /// <summary>
        /// Quốc gia theo hộ khẩu
        /// </summary>
        /// <returns></returns>
        public int? NationalIDByHousehold { get; set; }
        /// <summary>
        /// Tỉnh/Thành phố theo hộ khẩu
        /// </summary>
        /// <returns></returns>
        public int? ProvinceIDByHousehold { get; set; }
        /// <summary>
        /// Quận/Huyện theo hộ khẩu
        /// </summary>
        /// <returns></returns>
        public int? DistrictIDByHousehold { get; set; }
        /// <summary>
        /// Xã/Phường theo hộ khẩu
        /// </summary>
        /// <returns></returns>
        public int? WardIDByHousehold { get; set; }
        /// <summary>
        /// Địa chỉ theo hộ khẩu
        /// </summary>
        /// <returns></returns>
        public string AddressByHousehold { get; set; }
        /// <summary>
        /// Quốc gia theo địa chỉ cư trú
        /// </summary>
        /// <returns></returns>
        public int? NationalIDByResident { get; set; }
        /// <summary>
        /// Tỉnh/Thành phố theo địa chỉ cư trú
        /// </summary>
        /// <returns></returns>
        public int?  ProvinceIDByResident { get; set; }
        /// <summary>
        /// Quận/Huyện theo địa chỉ cư trú
        /// </summary>
        /// <returns></returns>
        public int? DistrictIDByResident { get; set; }
        /// <summary>
        /// Xã/Phường theo địa chỉ cư trú
        /// </summary>
        /// <returns></returns>
        public int? WardIDByResident { get; set; }
        /// <summary>
        /// Địa chỉ theo địa chỉ cư trú
        /// </summary>
        /// <returns></returns>
        public string AddressByResident { get; set; }
        /// <summary>
        /// Ngày chính thức làm việc
        /// </summary>
        public DateTime? StartWorkDate { get; set; }

        /// <summary>
        /// Ngày chính thức nghỉ việc
        /// </summary>
        public DateTime? QuitJobDate { get; set; }

        /// <summary>
        /// trạng thái làm việc 0-nghỉ việc, 1-đang làm
        /// </summary>
        public ulong? EmployeeStatus { get; set; }

        /// <summary>
        /// Thu nhập không chịu thuế
        /// </summary>
        public decimal IncomeNotTaxable { get; set; }
        /// <summary>
        /// Thu nhập chịu thuế
        /// </summary>
        public decimal IncomeTaxable { get; set; }

        /// <summary>
        /// Bảo hiểm phải nộp
        /// </summary>
        public decimal InsurancePayment { get; set; }

        /// <summary>
        ///Mức đóng bảo hiểm
        /// </summary>
        public decimal InsurancePremiums { get; set; }

        /// <summary>
        /// Tỷ lệ trích bảo hiểm
        /// </summary>
        public string InsuranceDeductionRate { get; set; }

        /// <summary>
        /// Số nhà/Đường phố, thôn xóm theo hộ khẩu
        /// </summary>
        public string ApartmentNumberByHousehold { get; set; }
        /// <summary>
        /// Số nhà/Đường phố, thôn xóm theo địa chỉ cư trú
        /// </summary>
        public string ApartmentNumberByResident { get; set; }

        /// <summary>
        /// Danh sách người phụ thuộc
        /// </summary>

        

        #region
        public bool Confirm { get; set; }
        #endregion
    }

    /// <summary>
    /// Class nhiệm vụ check tồn tại người lao động hay không
    /// </summary>
    public partial class EmployeeCheck  :BaseEntity{

        [JsonProperty(DefaultValueHandling =DefaultValueHandling.Ignore)]
        public Guid? EmployeeID { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]

        public Guid? DeclarationDetailID { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]

        public Guid? TemporaryTaxDetailID { get; set; }
        public string FullName { get; set; }
        /// <summary>
        /// Lí do bị warning/từ chối : 1- Đã có trong hồ sơ kê khai/bảng tính , 2- Đã có trong hồ sơ lao động
        /// </summary>
        public int Reason { get; set; }
        /// <summary>
        /// CMT/Hộ chiếu
        /// </summary>
        public string CitizenIdentityNo { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxNo { get; set; }

        public string EmployeeCode { get; set; }




    }
}
