using Microsoft.AspNetCore.Mvc;
using MISA.HCT.ETax.Api.Interfaces;
using MISA.HCT.ETax.ApplicationCore.Entities;
using MISA.HCT.ETax.ApplicationCore.Entities.ETax;
using MISA.HCT.ETax.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.HCT.ETax.Api.Controllers
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
        /// <summary>
        /// Lấy danh sách 
        /// </summary>
        /// createdDate 4.12.2020
        /// created by tcduong
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionServiceResult> GetProviderGroups()
        {
            return await _providerGroupService.GetProviderGroup();
        }
    }
}
