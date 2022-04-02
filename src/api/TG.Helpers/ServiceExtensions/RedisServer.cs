using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace TG.Helpers.ServiceExtensions
{
    public class RedisServer
    {
        private ConnectionMultiplexer connectionMultiplexer;
        private IDatabase database;
        private string configuration;
        private int currentDatabaseId;

        public RedisServer(IConfiguration Configuration)
        {

        }

        public IDatabase Database => database;

        public void FlushDatabase()
        {
            connectionMultiplexer.GetServer(configuration).FlushDatabase(currentDatabaseId);
        }

        private void CreateRedisConfigurationString(IConfiguration Configuration)
        {
            string host = Configuration.GetSection("Redis:Host").Value;
            string port = Configuration.GetSection("Redis:Port").Value;

            configuration = $"{host}:{port}";
        }
    }
}
