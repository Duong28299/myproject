using MISA.HCT.ETax.Api.Interfaces;
using MISA.HCT.ETax.ApplicationCore.Entities;
using MISA.HCT.ETax.ApplicationCore.Properties;
using MISA.HCT.ETax.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.HCT.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.HCT.ETax.Api.Services
{
    public class ProviderGroupService: IProviderGroupService
    {
        private readonly IProviderGroupRepository _providerGroupRepository;
        public ProviderGroupService(IProviderGroupRepository providerGroupRepository)
        {
            _providerGroupRepository = providerGroupRepository;

        }
        public Task<ActionServiceResult> GetProviderGroup()
        {
            var result = new ActionServiceResult();
            var providerGroups = _providerGroupRepository.GetProviderGroup().Result;
            result.Data = providerGroups;
            return Task.FromResult(result);
        }
    }
}
