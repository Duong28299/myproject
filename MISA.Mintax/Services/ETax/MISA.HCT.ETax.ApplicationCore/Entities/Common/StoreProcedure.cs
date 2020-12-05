namespace MISA.HCT.ETax.ApplicationCore.Common
{
    public class StoreProcedure
    {
       

        public partial class ProviderGroup
        {
            public const string Proc_GetProviderGroup = "Proc_GetProviderGroup";
        }

        public partial class Provider
        {
            public const string Proc_GetProviderPagping = "Proc_GetProviderPagping";
            public const string Proc_CheckProviderByCode = "Proc_CheckProviderByCode";
            public const string Proc_GetProviderByID = "Proc_GetProviderByID";
            public const string Proc_InsertProvider = "Proc_InsertProvider";
            public const string Proc_UpdateProvider = "Proc_UpdateProvider";
            public const string Proc_DeleteProvider = "Proc_DeleteProvider";
            public const string Proc_CheckProviderByCodeEdit = "Proc_CheckProviderByCodeEdit";
            public const string Proc_GetTotalProvider = "Proc_GetTotalProvider";
        }
    }
        
}
