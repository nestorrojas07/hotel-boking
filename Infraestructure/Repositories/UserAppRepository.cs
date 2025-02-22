using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;
using Domain.Ports.Repositories.Auth;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class UserAppRepository: IUserAppRepository
{
    private readonly BookingContext _authContext;

    public UserAppRepository(BookingContext authContext)
    {
        _authContext = authContext;
    }

    public async Task<UserApp?> FindByEmail(string email) => 
        await _authContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    
}
