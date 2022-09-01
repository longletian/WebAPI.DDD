using Identity.Application.Dtos;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public interface IUnitService
    {
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        Task<ResponseData> GetUnitListDataAsync();

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ResponseData> DeleteUnitByIdAsync(Guid Id);

        /// <summary>
        /// 部门详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ResponseData> GetUnitInfoDataAsync(Guid Id);

        /// <summary>
        /// 添加人员进部门
        /// </summary>
        /// <param name="UnitAddUserInput"></param>
        /// <returns></returns>
        Task<ResponseData> AddUserToUnitAsync(UnitAddUserInput UnitAddUserInput);

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="UnitInput"></param>
        /// <returns></returns>
        Task<ResponseData> AddUnitDataAsync(UnitInput UnitInput);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="UnitEditInput"></param>
        /// <returns></returns>
        Task<ResponseData> EditUnitDataAsync(UnitEditInput UnitEditInput);

    }
}
