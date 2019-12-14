using SimpleRedisQueue.Redis;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Http;

namespace SimpleRedisQueue
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ConfigRedisQueue();
        }

        public void ConfigRedisQueue()
        {
            var configurationOptions = new ConfigurationOptions
            {
                KeepAlive = 60,
            };

            configurationOptions.EndPoints.Add("localhost", 6379);

            RedisJobQueue.Connection = new Lazy<IConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions), true);
            var consumer = new RedisJobQueue(RedisJobQueue.Connection, "simple_queue");
            consumer.OnJobReceived += new EventHandler<string>((obj, jobStr) =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Debug.WriteLine(string.Format("Hello {0} the {1} time", jobStr, i + 1));
                    Thread.Sleep(1000);
                }
            });
            consumer.AsConsumer();
        }
    }
}
