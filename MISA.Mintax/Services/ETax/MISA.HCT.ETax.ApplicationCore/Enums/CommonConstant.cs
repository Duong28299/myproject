

using MISA.HCT.ETax.ApplicationCore.Entities.Common;
using System.Collections;
using System.Collections.Generic;
using static MISA.HCT.ETax.ApplicationCore.Enums.Enumeration;
using System.Linq;

public class CommonConstant
{
    public static readonly IEnumerable<CaculationMethod> CaculationMethodList = new List<CaculationMethod>
    {
            /// Các trường tính toán của mẫu 01/XSBHĐC
            new CaculationMethod(){ GroupType = GroupType.XSBHDC, PropertyName ="TotalIncome", Method = 2, AccumulatedStatus= true},
            new CaculationMethod(){ GroupType = GroupType.XSBHDC, PropertyName ="IncomeTaxable", Method = 2,AccumulatedStatus= true},
            new CaculationMethod(){ GroupType = GroupType.XSBHDC, PropertyName ="TaxIncurred", Method = 2,AccumulatedStatus= true},
            new CaculationMethod(){ GroupType = GroupType.XSBHDC, PropertyName ="TotalReduction", Method = 2 ,AccumulatedStatus= true},
            new CaculationMethod(){ GroupType = GroupType.XSBHDC, PropertyName ="TotalTaxWithHeld", Method = 2,AccumulatedStatus= true},

            /// Các trường tính toán của mẫu 01/TTS
            new CaculationMethod(){ GroupType = GroupType.TTS, PropertyName ="TotalTax", Method = 2},

            // Các trường mapping cho mẫu 05/KK_TNCN
            new CaculationMethod(){ GroupType = GroupType.TLTL, PropertyName ="TotalTaxWithHeld", Method = 2},

            /// Các trường tính toán của mẫu 06/ Kê khai cho đầu tư vốn, chuyển nhượng chứng khoán
            new CaculationMethod(){ GroupType = GroupType.DTVCNTT, PropertyName ="IncomeTaxable", Method = 2,AccumulatedStatus= true},
            new CaculationMethod(){ GroupType = GroupType.DTVCNTT, PropertyName ="TotalTaxWithHeld", Method = 2,AccumulatedStatus= true},

    };
    /// <summary>
    /// Lấy dạng tính thuế nhóm tờ khai
    /// </summary>
    /// <param name="groupType"></param>
    /// <returns></returns>
    public static IEnumerable<CaculationMethod> GetCaculatorMethod(int groupType)
    {
        foreach (var entity in CaculationMethodList)
        {
            if ((int)entity.GroupType == groupType)
            {
                yield return entity;
            }
        }
    }

    /// <summary>
    /// Lấy dạng tính thuế theo dạng kê khai thuế
    /// </summary>
    /// <param name="incomeType">dạng kê khai thuế</param>
    /// <returns></returns>
    public static IEnumerable<CaculationMethod> GetCaculatorMethodByIncomeType(int incomeType)
    {
        var groupType = GetGroupTypeByIncomeType(incomeType);
        var result = CaculationMethodList.Where(x => x.GroupType == groupType);
        return result;
    }
    /// <summary>
    /// Thông tin về cac tờ khai thuế
    /// </summary>
    public static readonly IList<DeclarationInfo> DeclarationInfoList = new List<DeclarationInfo>
    {
        new DeclarationInfo(){ GroupType = GroupType.XSBHDC, Name="Tờ khai thuế thu nhập cá nhân từ xổ số bảo hiểm đa cấp" ,AccumulatedStatus = true},
        new DeclarationInfo(){ GroupType = GroupType.TTS, Name="Tờ khai thuế thu nhập cá nhân từ xổ số bảo hiểm đa cấp" ,AccumulatedStatus = false },
        new DeclarationInfo(){ GroupType = GroupType.TLTL, Name="Tờ khai thuế thu nhập cá nhân từ xổ số bảo hiểm đa cấp" ,AccumulatedStatus = false},
        new DeclarationInfo(){ GroupType = GroupType.DTVCNTT, Name="Tờ khai thuế thu nhập cá nhân từ xổ số bảo hiểm đa cấp" ,AccumulatedStatus = false},

    };
    public static bool GetAccumulatedStatus(int groupType)
    {
        var result = DeclarationInfoList.Where(x => x.GroupType == (GroupType)groupType).FirstOrDefault().AccumulatedStatus;
        return result;
    }

    public static readonly IEnumerable<GroupTypeInfo> GroupTypeInfo = new List<GroupTypeInfo>
    {
        new GroupTypeInfo(){ GroupType = GroupType.TLTL, IncomeTypes = new List<IncomeType> { IncomeType.BHNTDNNG, IncomeType.TLTC}},
        new GroupTypeInfo(){ GroupType = GroupType.XSBHDC, IncomeTypes =new List<IncomeType> {IncomeType.XS, IncomeType.BH, IncomeType.TLBHNT, IncomeType.DC}}

    };

    /// <summary>
    /// Lấy kiểu tờ khai theo dạng kê khai thuế
    /// </summary>
    /// <param name="incomeType">Dạng kê khai thuế</param>
    /// <returns></returns>
    public static GroupType GetGroupTypeByIncomeType(int incomeType)
    {
        return GroupTypeInfo.Where(x => x.IncomeTypes.Any(y => y == (IncomeType)incomeType)).FirstOrDefault().GroupType;
    }


    /// <summary>
    /// Mode các kiểu danh sách tờ khai
    /// TemporaryMode - Nhóm sinh ra từ bảng tính - Có list ở màn hình danh sách tờ khai 
    /// TaxRegisterMode - Nhóm sinh ra từ đăng kí/thay đổi người nộp thuế - có danh sách ở màn hình đăng kí/thay đổi người nộp thuế
    /// DependentRegisterMode - Nhóm sinh ra từ tờ khai đăng kí người phụ thuộc  - có danh sách ở màn hình đăng kí người phụ thuộc
    /// </summary>
    /// created by nvbinh2 14.9.2020
    public static IList<DeclarationMode> DeclarationModes = new List<DeclarationMode>
    {

        new DeclarationMode(){GroupMode = GroupMode.TemporaryMode, ListMode = new List<GroupType>{ GroupType.DTVCNTT, GroupType.TLTL, GroupType.TTS, GroupType.XSBHDC}, EmployeeStatus =0},
        new DeclarationMode(){GroupMode = GroupMode.TaxRegisterMode, ListMode = new List<GroupType>{ GroupType.DKTTMST, GroupType.TDTTDKMST},
            EmployeeStatus = 1, PropertyMapper = MappingProperty.PropertyMapTaxRegister
        },
        new DeclarationMode(){ GroupMode = GroupMode.DependentRegisterMode,ListMode = new List<GroupType>{GroupType.DKNPT }, EmployeeStatus = 1, PropertyMapper = MappingProperty.PropertyMapDependentRegister},

    };

    public static DeclarationMode GetDeclarationMode(string groupMode)
    {
        return DeclarationModes.Where(x => x.GroupMode.Equals(groupMode, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
    }
    public static DeclarationMode GetDeclarationModeByGroupType(int groupType)
    {
        var result = DeclarationModes.Where(x => x.ListMode.Contains((GroupType)groupType)).FirstOrDefault();
        return result;
    }
    /// <summary>
    /// Danh sách các GroupMode
    /// </summary>
    public class GroupMode
    {
        public const string TemporaryMode = "TemporaryMode";
        public const string TaxRegisterMode = "TaxRegisterMode";
        public const string DependentRegisterMode = "DependentRegisterMode";
    }
    /// <summary>
    /// Các property cần mapp đối với declarationDetail
    /// </summary>
    public class MappingProperty
    {
        public static List<string> PropertyMapTaxRegister = new List<string> { "TaxNo","EmployeeCode", "FullName", "DateOfBirth", "Gender", "IndentityType",
            "CitizenIdentityNo", "CitizenIdentityDate", "NationalityID", "CitizenIdentityPlaceID",  "PositionID","Phone",
            "Email", "NationalIDByHousehold", "ProvinceIDByHousehold", "DistrictIDByHousehold",
            "WardIDByHousehold", "AddressByHousehold", "NationalIDByResident", "ProvinceIDByResident",
            "DistrictIDByResident", "WardIDByResident" , "AddressByResident","OrganizationUnitName","OrganizationUnitID" };
        public static List<string> PropertyMapDependentRegister = new List<string> { "EmployeeCode", "FullName", "OrganizationUnitName","OrganizationUnitID" ,"PositionID", "TaxNo" };
        public static List<string> PropertyMapTemporary = new List<string> { "" };
    }
}
