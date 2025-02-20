using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;

namespace Domain.Ports.CaseUse;

public interface ITokenGenerator
{
    public string GenerateToken(string id, UserApp user);
}
