using genics.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Data
{
    public interface IUserRepository
    {
        Task<AuthResponse> RegisterNewUser(RegisterDto model);
        Task<AuthResponse> LoginUser(LoginDto model);

    }
}
