

using Serilog;

namespace Lotfi.SeriLogSample
{
    public interface IService
    {
        void TestLog();
    }
    class ServiceTest : IService
    {
        private readonly ILogger logger;

        public ServiceTest(ILogger logger)
        {
            this.logger = logger;
        }

        public void TestLog()
        {
            logger.Information("logger.LogInformation 222222222222222");
        }


    }
}
