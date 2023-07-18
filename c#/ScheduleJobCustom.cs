var jobDataAsString = JsonConvert.SerializeObject(jobData);

var job = JobBuilder.Create<PaymentsProcessingJob>()
    .WithIdentity(key)
    .UsingJobData("jobData", jobDataAsString)
    .Build();

var trigger = TriggerBuilder.Create()
    .ForJob(job)
    .StartNow()
    .Build();

await _scheduler.ScheduleJob(job, trigger);