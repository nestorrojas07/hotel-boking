using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models.Auth;
using Domain.Ports.CaseUse;
using Domain.Ports.Repositories.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;
using Services.Auth.Options;

namespace Services.Auth;

public class LoginService  
{
    private readonly  ITokenGenerator _tokenGenerator;
    private readonly ITokenValidator _tokenValidator;
    private readonly IUserAppRepository _userAppRepository;
    private readonly AuthOption _authOption;

    public LoginService(IUserAppRepository userAppRepository, ITokenGenerator tokenGenerator, ITokenValidator tokenValidator, IOptions<AuthOption> authOption)
    {
        _userAppRepository = userAppRepository;
        _tokenGenerator = tokenGenerator;
        _tokenValidator = tokenValidator;
        _authOption = authOption.Value;
    }

    public async Task<string> Login(string email, string password) {
        var user = await _userAppRepository.FindByEmail(email);
        if (user == null) throw new InvalidCredentialExeption();

        if(!BCrypt.Net.BCrypt.Verify(password, user.Password))
            throw new InvalidCredentialExeption();
        //todo: bcrypt validator 

        string sessionId = Guid.NewGuid().ToString();

        return _tokenGenerator.GenerateToken(sessionId, user);
    }
}
