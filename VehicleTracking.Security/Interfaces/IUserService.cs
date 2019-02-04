using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Security.Models;

namespace VehicleTracking.Security.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(LoginModel model);

        Task<string> Register(RegisterModel model);

        Task CreateRolesAsync();
    }
}
