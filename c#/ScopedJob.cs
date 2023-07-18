using Quartz;

namespace Euroins.Payment.Internal.Infrastructure.Extensions
{
    public class ScopedJob : IJob
    {
        private readonly IJob _innerJob;

        public IServiceScope Scope { get; }

        public ScopedJob(IJob innerJob, IServiceScope scope)
        {
            _innerJob = innerJob;
            Scope = scope;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return _innerJob.Execute(context);
        }
    }

}
