using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Mintax.ETax.Api.Interfaces;
using MISA.Mintax.ETax.ApplicationCore.Entities;
using MISA.Mintax.ETax.ApplicationCore.Properties;
using static MISA.Mintax.ETax.ApplicationCore.Enums.Enumeration;

namespace MISA.Mintax.ETax.Controllers
{
    /// <summary>
    /// Class Base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// CreateBy: NVMANH (20/04/2020)
    [Route("etax/api/v1/[controller]/")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase
    {
        /// <summary>
        /// Interface reference tới BaseEntityService
        /// </summary>
        protected readonly IBaseEntityService<T> _baseEntityService;

        /// <summary>
        /// Khởi tạo service chung
        /// </summary>
        /// <param name="baseEntityService"></param>
        /// CreateBy: NVMANH (20/04/2020)
        public BaseController(IBaseEntityService<T> baseEntityService)
        {
            _baseEntityService = baseEntityService;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách đối tượng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 08.07.2020
        [HttpGet]
        public virtual async Task<ActionServiceResult> GetEntities()
        {
            var response = new ActionServiceResult();
            var entities = await _baseEntityService.GetEntities();
            response.Data = entities;
            return response;
        }

        /// <summary>
        /// Lấy thông tin theo mã (khóa chính)
        /// </summary>
        /// <param name="id">giá trị khóa chính trong bảng CSDL</param>
        /// <returns></returns>
        /// created by vdthang 08.07.2020
        [HttpGet("{id}")]
        public virtual async Task<ActionServiceResult> GetEntityByID(string id)
        {
            var response = new ActionServiceResult();
            if (string.IsNullOrEmpty(id))
            {
                response.Success = false;
                response.MISACode = MISACode.ValidateEntity;
                response.Message = Resources.ValidateEntity;
            }
            else
            {
                var entity = await _baseEntityService.GetEntityById(id);
                if (entity == null)
                {
                    response.Success = false;
                    response.MISACode = MISACode.NotFound;
                    response.Message = Resources.NotFound;
                }
                else
                {
                    response.Data = entity;
                }
            }

            return response;
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Đối tượng thêm mới</param>
        /// <returns></returns>
        /// created by vdthang 08.07.2020
        [HttpPost]
        public virtual async Task<ActionServiceResult> Post([FromBody] T entity)
        {
            var response = new ActionServiceResult();
            // Validate dữ liệu theo các Attribure Property
            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.Message = Resources.ValidateEntity;
                response.MISACode = MISACode.ValidateEntity;
                response.Data = ModelState;
            }
            else
            {
                var result = await _baseEntityService.Insert(entity, true);
                if (result.Success == false)
                {
                    response.Success = false;
                    response.MISACode = MISACode.ErrorAddEntity;
                    response.Message = Resources.ErrorAddEntity;
                }
            }
            return response;

        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="entity">Đối tượng sửa</param>
        /// <returns></returns>
        /// created by vdthang 08.07.2020
        [HttpPut]
        public virtual async Task<ActionServiceResult> Put([FromBody] T entity)
        {
            var response = new ActionServiceResult();
            if (entity == null)
            {
                response.Success = false;
                response.MISACode = MISACode.NotFound;
                response.Message = Resources.NotFound;
            }
            else
            {
                response = await _baseEntityService.Update(entity);
            }
            return response;
        }

        /// <summary>
        /// Xóa theo ID
        /// </summary>
        /// <param name="listID">list id của đối tượng</param>
        /// <returns></returns>
        /// created by vdthang 08.07.2020
        [HttpDelete]
        public virtual async Task<ActionServiceResult> Delete([FromBody] List<string> listID)
        {
            var response = new ActionServiceResult();
            if (listID.Count == 0 || listID == null)
            {
                response.Success = false;
                response.MISACode = MISACode.ValidateEntity;
                response.Message = Resources.ValidateEntity;
            }
            else
            {
                List<int> resultArray = new List<int>();
                var success = false;
                foreach (var id in listID)
                { 
                    var result = 0;
                    if ((await _baseEntityService.GetEntityById(id)) == null)
                    {
                        result = 3;
                    }
                    else
                        result = await _baseEntityService.Delete(id);
                    if(result != 0)
                    {
                        success = true;
                    }
                    resultArray.Add(result);
                }
                response.Data = resultArray;
                if(success == false)
                {
                    response.Success = false;
                    response.MISACode = MISACode.ErrorDeleteEntity;
                    response.Message = Resources.ErrorDeleteEntity;
                }
                
            }
            return response;
        }

    }

}