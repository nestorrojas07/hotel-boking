using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Ports.CaseUse;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Auth.Options;

namespace Services.Auth;

public class JtwTokenValidator : ITokenValidator
{
    private readonly JwtOptions _jwtSetting;

    public JtwTokenValidator(IOptions<JwtOptions> jwtSetting)
    {
        _jwtSetting = jwtSetting.Value;
    }

    public bool IsValidToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSetting.Secret));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtSetting.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtSetting.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = securityKey
        };

        try
        {
            SecurityToken validatedToken;
            tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
