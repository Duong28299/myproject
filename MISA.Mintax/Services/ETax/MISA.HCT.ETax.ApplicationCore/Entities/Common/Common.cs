using System;
using System.Collections.Generic;
using System.Text;
using static MISA.HCT.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.HCT.ETax.ApplicationCore.Entities.Common
{
    public partial class CaculationMethod
    {
        public GroupType GroupType { get; set; }
        public string PropertyName { get; set; }
        public int Method { get; set; }
        public bool AccumulatedStatus { get; set; }

    }
    public partial class DeclarationInfo
    {
        public string Name { get; set; }
        public GroupType GroupType { get; set; }
        public List<int> IncomType { get; set; }
        public bool AccumulatedStatus { get; set; }
    }

    public partial class GroupTypeInfo
    {
        public GroupType GroupType { get; set; }
        public List<IncomeType> IncomeTypes { get; set; }

    }

    public partial class DeclarationMode
    {
        /// <summary>
        /// Nhóm
        /// </summary>
        public string GroupMode { set; get; }

        /// <summary>
        /// Danh sách các nhóm tờ khai
        /// </summary>
        public List<GroupType> ListMode { set; get; }
        /// <summary>
        /// Có check dữ liệu từ hồ sơ lao động lên không: 0- Không , 1- Chỉ waring, 2- Thông báo lỗi
        /// </summary>
        public int EmployeeStatus { get; set; }

        public List<string> PropertyMapper { get; set; }
    }

}
