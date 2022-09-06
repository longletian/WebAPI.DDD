using Dapper;
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
    public class UserService : IUserService
    {
        private readonly IFreeSql freeSql;
        private readonly IQueryRepository queryRepository;
        public UserService(IFreeSql _freeSql, IQueryRepository _queryRepository)
        {
            freeSql = _freeSql;
            queryRepository = _queryRepository;
        }

        /// <summary>
        /// 新增用户账号
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseData> AddUserAccountDataAsync(RegisterAccountInput registerAccountInput)
        {
            DateTime dateTime = DateTime.Now;
            Account account = registerAccountInput.Adapt<Account>();
            account.IsEnable = true;
            account.State = 0;
            account.GmtCreate = dateTime;

            User user = registerAccountInput.Adapt<User>();

            using (var uow = this.freeSql.CreateUnitOfWork())
            {
                try
                {
                    await uow.Orm.Insert<Account>(account).ExecuteAffrowsAsync();
                    user.AccountId = account.Id;
                    await uow.Orm.Insert<User>(user).ExecuteAffrowsAsync();
                    uow.Commit();
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                }
            }
            return new ResponseData { MsgCode = 0, Message = "请求成功" };
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseData> LoginUserDataAsync(AccountLoginInput accountLoginInput)
        {
            UserOutput userOutput =
               await this.freeSql.Select<Account, User>()
                 .LeftJoin((c, d) => c.Id == d.AccountId)
                 .WhereIf(!string.IsNullOrEmpty(accountLoginInput.AccountName.Trim()), accountLoginInput.AccountName)
                 .WhereIf(!string.IsNullOrWhiteSpace(accountLoginInput.Password.Trim()), accountLoginInput.Password)
                 .Where((c, d) => c.IsEnable == true && c.State == 0)
                 .ToOneAsync((c, d) => new UserOutput
                 {
                     State = c.State,
                     AccountId = d.AccountId,
                     AccountName = c.AccountName,
                     ImagePath = d.ImagePath,
                     Id = d.Id,
                     IsEnable = c.IsEnable,
                     Name = d.Name,
                     NickName = d.NickName
                 });
            if (userOutput == null)
                return new ResponseData { MsgCode = 1, Message = "账号密码不对" };
            return new ResponseData { MsgCode = 0, Message = "请求成功", Data = userOutput };
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData> GetUserInfoDataAsync(Guid userId)
        {
            throw new ArgumentException();
        }

        /// <summary>
        /// 获取人员用户列表
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData> GetPaingUserListDataAsync(UserQo userQo)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            string sql = @"";
            queryRepository.FindListAsync<UserListOutput>(sql, dynamicParameters);
            return Task.FromResult(new ResponseData { });
        }
    }
}
