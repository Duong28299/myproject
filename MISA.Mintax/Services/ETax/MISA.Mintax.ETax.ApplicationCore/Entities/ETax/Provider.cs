using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Mintax.ETax.ApplicationCore.Entities.ETax
{
    public class Provider:BaseEntity
    {
        //Thông tin cơ bản
        /// <summary>
        /// ID Nhà cung cấp
        /// </summary>
        public Guid? ProviderID { get; set; }

        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public string ProviderCode { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Địa chỉ nhà cung cấp
        /// </summary>
        public string ProviderAddress { get; set; }

        /// <summary>
        /// Mã số thuế nhà cung cấp
        /// </summary>
        public string ProviderTaxNo { get; set; }

        /// <summary>
        /// SĐT nhà cung cấp
        /// </summary>
        public string ProviderPhone { get; set; }

        /// <summary>
        /// ID nhóm nhà cung cấp
        /// </summary>
        public Guid? ProviderGroupID { get; set; }

        /// <summary>
        /// Sô tiền nợ
        /// </summary>
        public decimal? Debt { get; set; }

        /// <summary>
        /// Hạn nợ
        /// </summary>
        public int? DebtTerm { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        public string BankBranchName { get; set; }
        public string AccountNumber { get; set; }

        public ulong? ProviderType { get; set; }

        public ulong? Follow { get; set; }

        ///Thông tin thêm khi nhà cung cấp là cá nhân
        ///
        /// <summary>
        /// Email nhà cung cấp cá nhân
        /// </summary>
        public string ProviderEmail { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string CitizenIdentityNo { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string CitizenIdentityPlace { get; set; }
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? CitizenIdentityDate { get; set; }

        //Thông tin người liên hệ
        
        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Email người liên hệ
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// SĐT người liên hệ
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Chức danh người liên hệ
        /// </summary>
        public string ContactTitle { get; set; }

        /// <summary>
        /// Địa chỉ người liên hệ
        /// </summary>
        public string ContactAddress { get; set; }


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

        #region Các trường cần bổ sung
        public string ProviderGroupName { get; set; }
        #endregion
    }
}
