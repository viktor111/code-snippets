private async Task RescheduleJob(IJobExecutionContext context, int time, IntervalUnit intervalUnit)
{
    var triggerKey = context.Trigger.Key;

    var key = new JobKey(Guid.NewGuid().ToString());

    var job = JobBuilder.Create<PaymentsProcessingJob>()
        .WithIdentity(key)
        .UsingJobData("jobData", context.MergedJobDataMap.GetString("jobData"))
        .Build();

    var trigger = TriggerBuilder.Create()
        .ForJob(job)
        .StartAt(DateBuilder.FutureDate(time, intervalUnit))
        .Build();

    await _scheduler.UnscheduleJob(triggerKey);
    await _scheduler.ScheduleJob(job, trigger);
}