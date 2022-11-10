services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(
            configuration.GetSection("RedisConfig").Get<RedisConfiguration>());