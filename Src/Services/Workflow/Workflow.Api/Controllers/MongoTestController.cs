using InfrastructureBase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Workflow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoTestController : ControllerBase
    {
        private readonly IMongoConnection mongoConnection;

        public MongoTestController(IMongoConnection _mongoConnection)
        {
            mongoConnection = _mongoConnection;
        }

        [HttpGet,Route("database")]
        public IActionResult GetDataBase()
        {
            IMongoDatabase mongoDatabase= mongoConnection.GetDataBase();
            return Ok(mongoDatabase);
        }

        [HttpGet, Route("collection")]
        public IActionResult GetCollections()
        {
            IMongoCollection<object> mongoDatabase = mongoConnection.GetCollections<object>();
            return Ok(mongoDatabase);
        }
    }
}
