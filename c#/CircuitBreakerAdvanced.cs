private readonly AsyncCircuitBreakerPolicy<string?> _circuitBreaker =
        Policy<string?>
            .Handle<RedisTimeoutException>()
            .CircuitBreakerAsync(1, TimeSpan.FromSeconds(30));