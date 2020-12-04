using MISA.Mintax.ETax.Api.Interfaces;
using MISA.Mintax.ETax.ApplicationCore.Entities;
using MISA.Mintax.ETax.ApplicationCore.Entities.ETax;
using MISA.Mintax.ETax.ApplicationCore.Properties;
using MISA.Mintax.ETax.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.Mintax.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.Mintax.ETax.Api.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        public ProviderService(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<ActionServiceResult> GetProviderByID(string providerID)
        {
            if (IsGuid(providerID))
            {
                var provider = _providerRepository.GetProviderByID(Guid.Parse(providerID)).Result;
                if (provider!=null)
                {
                    return await Task.FromResult(new ActionServiceResult(true, Resources.Success, MISACode.Success, provider));
                }
                return await Task.FromResult(new ActionServiceResult(false, Resources.NotFound, MISACode.NotFound, provider));
            }
            return await Task.FromResult(new ActionServiceResult(false, Resources.ValidateEntity, MISACode.ValidateEntity, providerID));
        }

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
            {
                return Task.FromResult(new ActionServiceResult(false, Resources.ValidateEntity, MISACode.ValidateEntity, null));
            }
            var checkProviderCode = CheckDuplicateProviderCode(provider,1).Result;
            if (!checkProviderCode.Success)
            {
                return Task.FromResult(checkProviderCode);
            }
            provider.ProviderID = Guid.NewGuid();
            var insertProvider = _providerRepository.InsertProvider(provider).Result;
            if (insertProvider==null || insertProvider.ToString()=="")
            {
                Task.FromResult(new ActionServiceResult(false, Resources.ErrorAddEntity, MISACode.ErrorAddEntity, null));
            }
            return Task.FromResult(new ActionServiceResult(true, Resources.Success, MISACode.Success, insertProvider));
        }

        public Task<ActionServiceResult> UpdateProvider(Provider provider)
        {
            var check = CheckValidate(provider);
            if (!check)
                return Task.FromResult(new ActionServiceResult(false, Resources.ValidateEntity, MISACode.ValidateEntity, null));
            if (provider.ProviderID != null && IsGuid(provider.ProviderID.ToString()))
            {
                var checkProviderCode = CheckDuplicateProviderCode(provider, 0).Result;
                if (!checkProviderCode.Success)
                {
                    return Task.FromResult(checkProviderCode);
                }           
                var update = _providerRepository.UpdateProvider(provider).Result;
                if (update<1)
                {
                    return Task.FromResult(new ActionServiceResult(false, Resources.ErrorUpdateEntity, MISACode.ErrorUpdateEntity,null));
                }
                return Task.FromResult(new ActionServiceResult(true, Resources.Success, MISACode.Success, update));
            }
            return Task.FromResult(new ActionServiceResult(false, Resources.ErrorUpdateEntity, MISACode.ErrorUpdateEntity,null));
        }

        public bool CheckValidate(Provider provider)
        {
            if (provider.ProviderCode==null || provider.ProviderName==null)
            {
                return false;
            }
            return true;
        }

        public Task<ActionServiceResult> CheckDuplicateProviderCode(Provider provider,int mode)
        {
            var res = new ActionServiceResult();

            if (mode==1)
            {
                var checkProviderCode = _providerRepository.GetProviderByCode(provider.ProviderCode).Result;
                if (checkProviderCode != null)
                {
                    res.Success = false;
                    res.Message = Resources.Duplicate;
                    res.MISACode = MISACode.Duplicate;
                    res.Data = checkProviderCode;
                    return Task.FromResult(res);
                }
            }else if (mode == 0)
            {
                var checkProviderCode = _providerRepository.GetProviderByCodeEdit(provider.ProviderCode,(Guid)provider.ProviderID).Result;
                if (checkProviderCode != null)
                {
                    res.Success = false;
                    res.Message = Resources.Duplicate;
                    res.MISACode = MISACode.Duplicate;
                    res.Data = checkProviderCode;
                    return Task.FromResult(res);
                }
            }
            
            return Task.FromResult(res);
        }

        

        public Task<ActionServiceResult> DeleteProvider(List<string> providerIDs)
        {
            var listResult = new List<object>();
            foreach (var providerID in providerIDs)
            {
                if (IsGuid(providerID))
                {
                    var checkExist = _providerRepository.GetProviderByID(Guid.Parse(providerID));
                    if (checkExist==null)
                    {
                        listResult.Add(new { providerID, DeleteStatus.NotFound });
                    }
                    var deleteProvider = _providerRepository.DeleteProvider(Guid.Parse(providerID)).Result;
                    if (deleteProvider==1)
                    {
                        listResult.Add(new {providerID, DeleteStatus.Success });
                    }
                }
                else
                    listResult.Add(new { providerID, DeleteStatus.NotValidate });
            }
            return Task.FromResult(new ActionServiceResult(true, Resources.Success, MISACode.Success, listResult));
        }

        public static bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }
    }
}
