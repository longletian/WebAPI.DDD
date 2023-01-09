using DomainBase;
using Identity.Application.Dtos;
using Identity.Infrastructure.PersistenceObject;
using InfrastructureBase;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public class UnitService : IUnitService
    {
        private readonly IFreeSql freeSql;
        public UnitService(IFreeSql _freeSql)
        {
            freeSql = _freeSql;
        }

        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseData> GetUnitListDataAsync()
        {
            List<Unit> units = await this.freeSql.Select<Unit>()
                    .AsTreeCte()
                    .OrderByDescending((c) => c.SortNum)
                    .ToListAsync();
            return new ResponseData { MsgCode = 0, Message = "请求成功", Data = units };
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ResponseData> DeleteUnitByIdAsync(Guid Id)
        {
            this.freeSql.Delete<Unit>().Where((c) => c.Id == Id);
            return await Task.FromResult(new ResponseData { MsgCode = 0, Message = "请求成功" });
        }

        /// <summary>
        /// 部门详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ResponseData> GetUnitInfoDataAsync(Guid Id)
        {
            Unit unit = await this.freeSql.Select<Unit>().Where((c) => c.Id == Id).ToOneAsync();
            return await Task.FromResult(new ResponseData { MsgCode = 0, Message = "请求成功", Data = unit });

        }

        /// <summary>
        /// 添加人员进部门
        /// </summary>
        /// <param name="UnitAddUserInput"></param>
        /// <returns></returns>
        public async Task<ResponseData> AddUserToUnitAsync(UnitAddUserInput UnitAddUserInput)
        {
            Unit unit = await this.freeSql.Select<Unit>().Where((c) => c.Id == UnitAddUserInput.UnitId).ToOneAsync();
            if (unit == null)
                throw new ArgumentException("参数异常");

            List<UnitUser> unitUsers = new List<UnitUser>();
            this.freeSql.Delete<UnitUser>().Where((c) => c.UnitId == UnitAddUserInput.UnitId);
            if (UnitAddUserInput.UnitAddUserDtos != null && UnitAddUserInput.UnitAddUserDtos.Count() > 0)
            {
                UnitAddUserInput.UnitAddUserDtos?.ForEach((c) =>
                {
                    unitUsers.Add(new UnitUser
                    {
                        UnitId = UnitAddUserInput.UnitId,
                        UserId = c.UserId
                    });
                });
            }

            if (unitUsers != null && unitUsers.Count() > 0)
                this.freeSql.Insert<UnitUser>(unitUsers);
            return new ResponseData { MsgCode = 0, Message = "请求成功" };
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="UnitInput"></param>
        /// <returns></returns>
        public async Task<ResponseData> AddUnitDataAsync(UnitInput UnitInput)
        {
            Unit unit = UnitInput.Adapt<Unit>();
            unit.UnitPath = await CreateNewGuid(unit);
            await this.freeSql.Insert(unit).ExecuteAffrowsAsync();
            return await Task.FromResult(new ResponseData { MsgCode = 0, Message = "请求成功" });
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="UnitEditInput"></param>
        /// <returns></returns>
        public async Task<ResponseData> EditUnitDataAsync(UnitEditInput UnitEditInput)
        {
            Unit unit = await this.freeSql.Select<Unit>().Where((c) => c.Id == UnitEditInput.Id).ToOneAsync();
            if (unit == null)
                throw new ArgumentException("参数异常");

            unit = UnitEditInput.Adapt<Unit>();
            unit.UnitPath = await CreateNewGuid(unit);
            this.freeSql.Update<Unit>(unit);
            return await Task.FromResult(new ResponseData { MsgCode = 0, Message = "请求成功" });
        }

        /// <summary>
        /// 获取部门用户列表
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public async Task<ResponseData> GetPagingUnitUserListDataAsync(UnitUserQo unitUserQo)
        {
            var select = this.freeSql.Select<User, UnitUser, Unit>()
                .LeftJoin((a, b, c) => a.Id == b.UserId)
                .LeftJoin((a, b, c) => b.UnitId == c.Id)
                .WhereIf(unitUserQo.UnitId.HasValue,(a,b,c)=>c.Id== unitUserQo.UnitId.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(unitUserQo.KeyWord), (a, b, c) =>
                    a.Name.Contains(unitUserQo.KeyWord)
                );
            var total = await select.CountAsync();
            var list = await select.Page(unitUserQo.Page, unitUserQo.PageSize).ToListAsync();
            return new ResponseData
            {
                MsgCode = 0,
                Message = "请求成功",
                Data = new PageReturnDto<User>(list, total, unitUserQo.Page, unitUserQo.PageSize)
            };
        }


        /// <summary>
        /// 创建Pids格式
        /// 如果pid是0顶级节点，pids就是 [0];
        /// 如果pid不是顶级节点，pids就是 pid菜单的 pids + [pid] + ,
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        private async Task<string> CreateNewGuid(Unit unit)
        {
            if (unit.ParentId.HasValue == false)
            {
                return $"[{unit.Id}],";
            }
            else
            {
                var unitEntity = await this.freeSql.Select<Unit>().Where((c) => c.Id == unit.ParentId.Value).ToOneAsync();
                return unitEntity.UnitPath + $"[{unit.Id}],";
            }
        }
    }
}
