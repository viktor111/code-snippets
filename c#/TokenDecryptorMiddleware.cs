public class TokenDecryptorMiddleware
    {
        const string AuthHeaderName = "Authorization";

        private readonly RequestDelegate _next;

        public TokenDecryptorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var token = GetToken(context);

                var originalToken = Crypter.Decrypt(token, "jwt-secret-key-very-secret-jwt-key-now12311");               

                context = SetAuthHeaderWithOriginalToken(context, originalToken);

                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                await _next.Invoke(context);
            }
        }

        private string GetToken(HttpContext context)
        {
            ValidateAuthHeader(context);

            var authHeader = context.Request.Headers[AuthHeaderName];
            var headerValues = authHeader.ToString().Split();

            var token = headerValues[1];

            return token;
        }

        private HttpContext SetAuthHeaderWithOriginalToken(HttpContext context, string token)
        {
            ValidateAuthHeader(context);

            context.Request.Headers[AuthHeaderName] = $"Bearer {token}";

            return context;
        } 

        private void ValidateAuthHeader(HttpContext context) 
        {
            if (string.IsNullOrEmpty(context.Request.Headers[AuthHeaderName]))
            {
                throw new InvalidOperationException("Auth header not present");
            }

            ValidateBearerExistInAuthHeader(context);
        }

        private void ValidateBearerExistInAuthHeader(HttpContext context)
        {
            var authHeader = context.Request.Headers[AuthHeaderName];
            var headerValues = authHeader.ToString().Split();

            if (headerValues.Count() < 1)
            {
                throw new InvalidOperationException("Bearer not present in auth header");
            }
        }
    }