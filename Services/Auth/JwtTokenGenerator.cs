using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;
using Domain.Ports.CaseUse;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Auth.Options;

namespace Services.Auth;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _jwtSetting;

    public JwtTokenGenerator(IOptions<JwtOptions> jwtSetting)
    {
        _jwtSetting = jwtSetting.Value;
    }

    public string GenerateToken(string id, UserApp user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim("userId", $"{user.Id}"),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(_jwtSetting.Expire),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
