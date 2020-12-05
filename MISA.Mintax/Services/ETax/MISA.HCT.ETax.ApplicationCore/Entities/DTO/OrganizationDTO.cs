using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.HCT.ETax.ApplicationCore.Entities.DTO
{
	public class OrganizationDTO : BaseEntity
	{
        /// <summary>
        /// ID
        /// </summary>
        public Guid OrganizationID { get; set; }

        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Mã số thuế đơn vị
        /// </summary>
        public string OrganizationTaxNo { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị
        /// </summary>
        public string TaxPlaceName { get; set; }

        /// <summary>
        /// ID của Tỉnh/Thành phố
        /// </summary>
        public int ProvinceID { get; set; }

        /// <summary>
        /// ID Quận/Huyện
        /// </summary>
        public int DistrictID { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string FaxNo { get; set; }
        /// <summary>
        /// Email liên hệ
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Số điện thoại liên hệ
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Ký bởi ai
        /// </summary>
        public string SignBy { get; set; }

        /// <summary>
        ///  Có sử dụng MTax hay không
        /// </summary>
        public ulong? IsUseMtax { get; set; }
        /// <summary>
        /// ID ngân hàng
        /// </summary>
        public Guid BankID { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string BankNo { get; set; }
        /// <summary>
        /// Cơ quan thuế cấp cục
        /// </summary>
        public string CAUnitName { get; set; }
        /// <summary>
        /// Cơ quan thuế quản lý
        /// </summary>
        public string CAUnitManage { get; set; }
        /// <summary>
        /// Check đơn vị chủ quản. True là đơn vị chủ quản
        /// </summary>
        public ulong IsParent { get; set; }
        public Guid OrganizationParentID { get; set; }
        /// <summary>
        /// ID amis của đơn vị con
        /// </summary>
        public Guid OrganizationAmisID { get; set; }
        //Cấp cơ cấu , tổ chức
        public string OrganizationUnit { get; set; }
        /// <summary>
        /// ID đơn vị bên AMIS
        /// </summary>
        public Guid TenantID { get; set; }

    }
}
