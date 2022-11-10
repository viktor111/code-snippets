if (_circuitBreaker.CircuitState is CircuitState.Open or CircuitState.Isolated)
        {
            var tokenResponse = await _getTokenService.TokenResponse();
            return tokenResponse.Result.Token;
        }
        var token = await _circuitBreaker.ExecuteAsync(() => _redisClient.GetDefaultDatabase().GetAsync<string>(Prefix));