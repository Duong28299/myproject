using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.HCT.ETax.ApplicationCore.Entities.ETax
{
    public class ProviderGroup:BaseEntity
    {
        /// <summary>
        /// ID nhóm nhà cung cấp
        /// </summary>
        public Guid ProviderGroupID { get; set; }

        /// <summary>
        /// Mã nhóm nhà cung cấp
        /// </summary>
        public string ProviderGroupCode { get; set; }
        /// <summary>
        /// Tên nhóm nhà cung cấp
        /// </summary>
        public string ProviderGroupName { get; set; }
    }
}
