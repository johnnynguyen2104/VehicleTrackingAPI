using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleTracking.Security.Interfaces;
using VehicleTracking.Security.Models;

namespace VehicleTracking.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> Login([FromBody] LoginModel model)
        {
            return await _userService.Login(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> Registration([FromBody] RegisterModel model)
        {
            return await _userService.Register(model);
        }
    }
}