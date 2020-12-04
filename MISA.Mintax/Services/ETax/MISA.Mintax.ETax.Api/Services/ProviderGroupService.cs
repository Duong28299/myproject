using MISA.Mintax.ETax.Api.Interfaces;
using MISA.Mintax.ETax.ApplicationCore.Entities;
using MISA.Mintax.ETax.ApplicationCore.Properties;
using MISA.Mintax.ETax.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.Mintax.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.Mintax.ETax.Api.Services
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
