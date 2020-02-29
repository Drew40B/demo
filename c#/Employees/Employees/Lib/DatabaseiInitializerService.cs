using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Employees.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Employees.Lib
{
    public class DatabaseiInitializerService : IHostedService
    {

        private readonly IServiceProvider _provider;
       
        public DatabaseiInitializerService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var db = this._provider.GetService<IEmployeeDatabase>();
            await db.Init();
        }

        // noop
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
