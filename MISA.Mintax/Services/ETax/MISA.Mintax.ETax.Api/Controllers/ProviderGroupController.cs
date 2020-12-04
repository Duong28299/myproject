using Microsoft.AspNetCore.Mvc;
using MISA.Mintax.ETax.Api.Interfaces;
using MISA.Mintax.ETax.ApplicationCore.Entities;
using MISA.Mintax.ETax.ApplicationCore.Entities.ETax;
using MISA.Mintax.ETax.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Mintax.ETax.Api.Controllers
{
    [Route("etax/api/v1/[controller]/")]
    [ApiController]
    public class ProviderGroupController
    {
        private readonly IProviderGroupService _providerGroupService;
        public ProviderGroupController(IProviderGroupService providerGroupService)
        {
            _providerGroupService = providerGroupService;
        }
        [HttpGet]
        public async Task<ActionServiceResult> GetProviderGroups()
        {
            return await _providerGroupService.GetProviderGroup();
        }
    }
}
