using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.CaseUse;

public interface ITokenValidator
{
    public bool IsValidToken(string token);
}
