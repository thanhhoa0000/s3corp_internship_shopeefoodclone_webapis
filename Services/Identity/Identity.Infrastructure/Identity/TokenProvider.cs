namespace ShopeeFoodClone.WebApi.Identity.Infrastructure.Identity;

public class TokenProvider : ITokenProvider
{
    private readonly JwtProperties _jwtProperties;

    public TokenProvider(IOptions<JwtProperties> jwtOptions)
    {
        _jwtProperties = jwtOptions.Value;
    }

    public string CreateAccessToken(AppUser user, IEnumerable<Role> roles)
    {          
        var secretKey = _jwtProperties!.Key;
        var key = Encoding.ASCII.GetBytes(secretKey!);
        var securityKey = new SymmetricSecurityKey(key);

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };
        claims.AddRange(roles.Select(role
            => new Claim(ClaimTypes.Role, role.ToString())));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes((double)_jwtProperties.Expiration!),
            SigningCredentials = credentials,
            Issuer = _jwtProperties.Issuer,
            Audience = _jwtProperties.Audience
        };

        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
