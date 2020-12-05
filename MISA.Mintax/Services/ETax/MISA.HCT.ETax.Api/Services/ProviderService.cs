using MISA.HCT.ETax.Api.Interfaces;
using MISA.HCT.ETax.ApplicationCore.Entities;
using MISA.HCT.ETax.ApplicationCore.Entities.ETax;
using MISA.HCT.ETax.ApplicationCore.Properties;
using MISA.HCT.ETax.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.HCT.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.HCT.ETax.Api.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        public ProviderService(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        /// <summary>
        /// Lấy nhà cung cấp theo ID
        /// </summary>
        /// <param name="providerID">ID nhà cung cấp</param>
        /// <returns></returns>
        public async Task<ActionServiceResult> GetProviderByID(string providerID)
        {
            if (IsGuid(providerID))
            {
                var provider = _providerRepository.GetProviderByID(Guid.Parse(providerID)).Result;
                if (provider!=null)
                    return await Task.FromResult(new ActionServiceResult(true, Resources.Success, MISACode.Success, provider));

                return await Task.FromResult(new ActionServiceResult(false, Resources.NotFound, MISACode.NotFound, provider));
            }
            return await Task.FromResult(new ActionServiceResult(false, Resources.ValidateEntity, MISACode.ValidateEntity, providerID));
        }

        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        /// <param name="pageIndex">số trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi</param>
        /// <returns></returns>
        public Task<ActionServiceResult> GetProviders(int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 1;
            pageSize = pageSize ?? 15;
            var result = new ActionServiceResult();
            var providers = _providerRepository.GetProviders(pageIndex, pageSize).Result;
            var total = _providerRepository.GetToTalProvider().Result;
            result.Data = new {providers, total };
            return Task.FromResult(result);
        }


        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// created by tcduong 30.11.2019
        /// <returns></returns>
        public Task<ActionServiceResult> InsertProvider(Provider provider)
        {
            var check = CheckValidate(provider);
            if (!check)
                return Task.FromResult(new ActionServiceResult(false, Resources.ValidateEntity, MISACode.ValidateEntity, null));

            var checkProviderCode = CheckDuplicateProviderCode(provider,1).Result;
            if (!checkProviderCode)
                return Task.FromResult(new ActionServiceResult(false, Resources.Duplicate, MISACode.Duplicate, checkProviderCode));

            provider.ProviderID = Guid.NewGuid();
            var insertProvider = _providerRepository.InsertProvider(provider).Result;
            if (insertProvider==null)
                Task.FromResult(new ActionServiceResult(false, Resources.ErrorAddEntity, MISACode.ErrorAddEntity, null));

            return Task.FromResult(new ActionServiceResult(true, Resources.Success, MISACode.Success, insertProvider));
        }

        /// <summary>
        /// Sửa nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// <returns></returns>
        public Task<ActionServiceResult> UpdateProvider(Provider provider)
        {
            var check = CheckValidate(provider);
            if (!check)
                return Task.FromResult(new ActionServiceResult(false, Resources.ValidateEntity, MISACode.ValidateEntity, null));
            if (provider.ProviderID != null && IsGuid(provider.ProviderID.ToString()))
            {
                var checkProviderCode = CheckDuplicateProviderCode(provider, 0).Result;
                if (!checkProviderCode)
                    return Task.FromResult(new ActionServiceResult(false, Resources.Duplicate, MISACode.Duplicate, checkProviderCode));

                var update = _providerRepository.UpdateProvider(provider).Result;
                if (update<1)
                    return Task.FromResult(new ActionServiceResult(false, Resources.ErrorUpdateEntity, MISACode.ErrorUpdateEntity,null));

                return Task.FromResult(new ActionServiceResult(true, Resources.Success, MISACode.Success, update));
            }
            return Task.FromResult(new ActionServiceResult(false, Resources.ErrorUpdateEntity, MISACode.ErrorUpdateEntity,null));
        }

        /// <summary>
        /// Validate dữ liệu đầu vào
        /// </summary>
        /// <param name="provider"></param>
        /// createdDate 4.12.2020
        /// created by tcduong
        /// <returns></returns>
        public bool CheckValidate(Provider provider)
        {
            if (provider.ProviderCode==null || provider.ProviderName==null)
                return false;
            return true;
        }

        /// <summary>
        /// Check trùng mã
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// <param name="mode">mode sửa hoặc thêm 1-Thêm,0-Sửa</param>
        /// <returns></returns>
        public Task<bool> CheckDuplicateProviderCode(Provider provider,int mode)
        {
            var res = new ActionServiceResult();

            if (mode==(int)Mode.Add)
            {
                var checkProviderCode = _providerRepository.CheckProviderByCode(provider.ProviderCode).Result;
                if (checkProviderCode)
                    return Task.FromResult(false);             
            }else if (mode == (int)Mode.Edit)
            {
                var checkProviderCode = _providerRepository.CheckProviderByCodeEdit(provider.ProviderCode,(Guid)provider.ProviderID).Result;
                if (checkProviderCode)
                    return Task.FromResult(false); ;
            }
            
            return Task.FromResult(true);
        }


        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="providerIDs">Danh sách ID nhà cung cấp</param>
        /// <returns></returns>
        public Task<ActionServiceResult> DeleteProvider(List<string> providerIDs)
        {
            var stringID = "";
            for (int i = 0; i < providerIDs.Count; i++)
            {
                if (IsGuid(providerIDs[i]))
                {
                    if (i == (providerIDs.Count - 1))
                        stringID += "'" + providerIDs[i] + "'";
                    else
                        stringID += "'" + providerIDs[i] + "',";
                }               
            }
            var deleteProvider = _providerRepository.DeleteProvider(stringID).Result;
            if (deleteProvider >=1)
                return Task.FromResult(new ActionServiceResult(true, Resources.Success, MISACode.Success, deleteProvider));

            return Task.FromResult(new ActionServiceResult(false, Resources.ErrorDeleteEntity, MISACode.ErrorDeleteEntity, deleteProvider));

        }

        /// <summary>
        /// Check GUID
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }
    }
}
