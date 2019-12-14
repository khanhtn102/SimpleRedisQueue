using SimpleRedisQueue.Redis;
using System.Diagnostics;
using System.Threading;
using System.Web.Http;

namespace SimpleRedisQueue.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            var redisJobQueue = new RedisJobQueue(RedisJobQueue.Connection, "simple_queue");

            redisJobQueue.AddJob("Thanh");

            Thread.Sleep(2000);
            Debug.WriteLine("End Api");

            return Ok();
        }
    }
}
