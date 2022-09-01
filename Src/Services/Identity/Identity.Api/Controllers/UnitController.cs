using Identity.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using InfrastructureBase;
using Identity.Application;

namespace Identity.Api.Controllers
{
    /// <summary>
    /// 部门功能模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService unitService;
        public UnitController(IUnitService _unitService)
        {
            this.unitService = _unitService;
        }

        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("list")]
        public Task<ResponseData> GetUnitListDataAsync()
        {
            return this.unitService.GetUnitListDataAsync();
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{Id}")]
        public Task<ResponseData> DeleteUnitByIdAsync(Guid Id)
        {
            return this.unitService.DeleteUnitByIdAsync(Id);
        }

        /// <summary>
        /// 部门详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet, Route("{Id}")]
        public Task<ResponseData> GetUnitInfoDataAsync(Guid Id)
        {
            return this.unitService.GetUnitInfoDataAsync(Id);
        }

        /// <summary>
        /// 添加人员进部门
        /// </summary>
        /// <param name="UnitAddUserInput"></param>
        /// <returns></returns>
        [HttpPost, Route("ry")]
        public Task<ResponseData> AddUserToUnitAsync([FromBody] UnitAddUserInput UnitAddUserInput)
        {
            return this.unitService.AddUserToUnitAsync(UnitAddUserInput);
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="UnitInput"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public Task<ResponseData> AddUnitDataAsync([FromBody] UnitInput UnitInput)
        {
            return this.unitService.AddUnitDataAsync(UnitInput);
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="UnitEditInput"></param>
        /// <returns></returns>
        [HttpPut, Route("")]
        public Task<ResponseData> EditUnitDataAsync([FromBody] UnitEditInput UnitEditInput)
        {
            return this.unitService.EditUnitDataAsync(UnitEditInput);
        }

    }
}
