using Microsoft.AspNetCore.Mvc;
using MISA.Mintax.ETax.Api.Interfaces;
using MISA.Mintax.ETax.ApplicationCore.Entities;
using MISA.Mintax.ETax.ApplicationCore.Entities.ETax;
using MISA.Mintax.ETax.ApplicationCore.Properties;
using MISA.Mintax.ETax.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.Mintax.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.Mintax.ETax.Api.Controllers
{
    [Route("etax/api/v1/[controller]/")]
    [ApiController]
    public class ProviderController
    {

        private readonly IProviderService _providerService;
        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<ActionServiceResult> GetProviders(int? pageIndex, int? pageSize)
        {
            return await _providerService.GetProviders(pageIndex, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionServiceResult> GetProviderByID(string id)
        {
            if (id != null)
            {
                return await _providerService.GetProviderByID(id);
            }
            return await Task.FromResult(new ActionServiceResult(false, Resources.ValidateEntity, MISACode.ValidateEntity, id));
        }
        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// created by tcduong 30.11.2019
        /// <returns></returns>
        [HttpPost]
        public  async Task<ActionServiceResult> Post([FromBody] Provider provider)
        {
            if (provider != null)
            {
                return  await _providerService.InsertProvider(provider);
            }
            return await Task.FromResult(new ActionServiceResult(false, Resources.ErrorAddEntity, MISACode.ErrorAddEntity, provider));
        }

        /// <summary>
        /// Sửa nhà cung cấp
        /// </summary>
        /// <param name="provider">Thông tin nhà cung cấp</param>
        /// created by tcduong 30.11.2019
        /// <returns></returns>
        [HttpPut]
        public  async Task<ActionServiceResult> Put([FromBody] Provider provider)
        {
            if (provider != null)
            {
                return await _providerService.UpdateProvider(provider);
            }
            return await Task.FromResult(new ActionServiceResult(false, Resources.ErrorUpdateEntity, MISACode.ErrorUpdateEntity, provider));
        }


        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="providerIDs">danh sách ID nhà cung cấp</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionServiceResult> Delete([FromBody] List<string> providerIDs)
        {
            if (providerIDs != null && providerIDs.Count > 0)
            {
                return await _providerService.DeleteProvider(providerIDs);
            }
            return await Task.FromResult(new ActionServiceResult(false, Resources.ErrorDeleteEntity, MISACode.ErrorDeleteEntity, providerIDs));
        }

    }
}
