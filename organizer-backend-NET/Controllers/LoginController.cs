using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Implements.Interfaces;

namespace organizer_backend_NET.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IBaseResponse<User>> SignUp(SignupViewModel model)
        {
            return await _userService.SignUp(model);
        }
    }
}
