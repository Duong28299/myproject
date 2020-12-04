using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Mintax.ETax.ApplicationCore.Entities
{
    public class EmployeeList
    {
        public EmployeeList(int total, IEnumerable<EmployeeForList> declarations, bool isEmpty)
        {
            TotalEmployee = total;
            EmployeeForList = declarations;
            IsEmpty = isEmpty;
        }


        /// <summary>
        ///     
        /// </summary>
        public int TotalEmployee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EmployeeForList> EmployeeForList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty { get; set; }


    }
    public partial class EmployeeForList
{
        public Guid? EmployeeID { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ và tên đầy đủ
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxNo { get; set; }
        /// <summary>
        /// ID vị trí/Chức vụ
        /// </summary>
        public Guid? PositionID { get; set; }
        /// <summary>
        /// Tên bộ phân phòng ban
        /// </summary>
        public string OrganizationUnitName { get; set; }
        public Guid? OrganizationUnitID { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public Guid? OrganizationID { get; set; }

        /// <summary>
        /// Số chứng minh thư nhân dân/hộ chiếu
        /// </summary>
        public string CitizenIdentityNo { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// người sửa cuối cùng
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Ngày sửa cuối cùng
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

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
    }
}
