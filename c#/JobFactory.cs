public class JobFactory : IJobFactory
{
    protected readonly IServiceScopeFactory serviceScopeFactory;

    public JobFactory(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        IServiceScope scope = this.serviceScopeFactory.CreateScope();
        IJob job;

        try
        {
            job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }
        catch
        {
            // Dispose the scope if job creation failed
            scope.Dispose();
            throw;
        }

        if (job == null)
        {
            // Dispose the scope if job creation failed
            scope.Dispose();
            throw new SchedulerException($"Failed to create job {bundle.JobDetail.JobType.FullName} as it does not implement {typeof(IJob)}.");
        }

        return new ScopedJob(job, scope);
    }

    public void ReturnJob(IJob job)
    {
        // Cast the job back to ScopedJob and dispose the scope
        if (job is ScopedJob scopedJob)
        {
            scopedJob.Scope.Dispose();
        }
    }
}
