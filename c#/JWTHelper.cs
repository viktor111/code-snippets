public static class JWTHelper
{
    public static string GenerateToken(EuroinsHealthUser user, string role, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("jwt-key"));
        
        var userId = CryptoHelper.Encrypt(user.Id, configuration.GetValue<string>("crypto-key"));

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role),
            }),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encryptedToken = tokenHandler.WriteToken(token);

        var cryptoJWTKEY = configuration.GetValue<string>("jwt-cr-key");
        string cryptToken = CryptoHelper.Encrypt(encryptedToken, cryptoJWTKEY);
        return cryptToken;
    }
}