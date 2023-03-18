using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Interfaces.IResponse;
using organizer_backend_NET.Domain.Response.BaseResponse;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Domain.ViewModel.Login;

namespace organizer_backend_NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost("signin")]
        public IBaseResponse<SignInViewModel> SignIn(SignInViewModel model)
        {
            return new BaseResponse<SignInViewModel>
            {
                Data = model,
            };
        }

        [HttpPost("signup")]
        public IBaseResponse<SignUpViewModel> SignUp(SignUpViewModel model)
        {
            return new BaseResponse<SignUpViewModel>
            {
                Data = model,
            };
        }

        [HttpPost("profile"), Authorize]
        public IBaseResponse<string> GetUser()
        {
            return new BaseResponse<string>
            {
                Data = "GetUser",
            };
        }

        [HttpPatch, Authorize]
        public IBaseResponse<UserViewModel> UpdateUser(UserViewModel model)
        {
            return new BaseResponse<UserViewModel>
            {
                Data = model,
            };
        }

        [HttpPost("remove"), Authorize]
        public IBaseResponse<string> RemoveUser()
        {
            return new BaseResponse<string>
            {
                Data = "RemoveUser",
            };
        }
    }
}