using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS.Common.Modules
{
    public interface ILogModule
    {
        void LogInformation(string message, params object[] param);

        void LogWarning(string message, params object[] param);

        void LogCritical(string message, params object[] param);

        void LogError(string message, params object[] param);

        void LogError(Exception exception, string message, params object[] param);
    }

    public class LogModule : ILogModule, IDisposable
    {
        private readonly ILifetimeScope lifetimeScope;

        public LogModule(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public void LogInformation(string message, params object[] param)
        {
            using var localService = lifetimeScope.BeginLifetimeScope();
            var logger = localService.Resolve<ILogger<LogModule>>();
            logger.LogInformation(message, param);
        }

        public void LogWarning(string message, params object[] param)
        {
            using var localService = lifetimeScope.BeginLifetimeScope();
            var logger = localService.Resolve<ILogger<LogModule>>();
            logger.LogWarning(message, param);
        }

        public void LogCritical(string message, params object[] param)
        {
            using var localService = lifetimeScope.BeginLifetimeScope();
            var logger = localService.Resolve<ILogger<LogModule>>();
            logger.LogCritical(message, param);
        }

        public void LogError(string message, params object[] param)
        {
            using var localService = lifetimeScope.BeginLifetimeScope();
            var logger = localService.Resolve<ILogger<LogModule>>();
            logger.LogError(message, param);
        }

        public void LogError(Exception exception, string message, params object[] param)
        {
            using var localService = lifetimeScope.BeginLifetimeScope();
            var logger = localService.Resolve<ILogger<LogModule>>();
            logger.LogError(exception, message, param);
        }

        public void Dispose()
        {

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
