using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;

namespace Domain.Ports.Repositories.Auth;

public interface IUserAppRepository
{
    public Task<UserApp?> FindByEmail(string email);

}
