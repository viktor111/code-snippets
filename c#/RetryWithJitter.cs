using Polly;
using Polly.Contrib.WaitAndRetry;

services.AddHttpClient<IUsersHttpService, UsersHttpService>()
            .AddTransientHttpErrorPolicy(
                policyBuilder => policyBuilder
                .WaitAndRetryAsync(
                    Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3)));