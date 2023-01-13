using InfrastructureBase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using static FreeSql.Internal.GlobalFilter;

namespace Workflow.Api.Controllers
{
    /// <summary>
    /// mongodb数据库测试接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MongoTestController : ControllerBase
    {
        private readonly IMongoConnection mongoConnection;

        public MongoTestController(IMongoConnection _mongoConnection)
        {
            mongoConnection = _mongoConnection;
        }

        /// <summary>
        /// 获取mongo数据库
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("database")]
        public IActionResult GetDataBase()
        {
            IMongoDatabase mongoDatabase= mongoConnection.GetDataBase();
            return Ok(mongoDatabase);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("collection")]
        public IActionResult GetCollections()
        {
            IMongoCollection<object> mongoDatabase = mongoConnection.GetCollections<object>();
            return Ok(mongoDatabase);
        }

        [HttpPost, Route("")]
        public IActionResult AddCollections()
        {
            object item=new
            {
                userName="张三",
                age=12,
                sex=1,
            };
            mongoConnection.AddEntityToCollection(item,"logmanage1");
            return Ok();
        }

        [HttpDelete, Route("")]
        public IActionResult DeleteEntityToCollection()
        {
            //mongoConnection.DeleteEntityToCollection(, "logmanage1");
            return Ok();
        }
    }
}
