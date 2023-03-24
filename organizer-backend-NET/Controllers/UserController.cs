using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Implements.Interfaces;

namespace organizer_backend_NET.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private int GetUId()
        {
            try
            {
                var UId = User.Claims.Where(a => a.Type == "UId").FirstOrDefault().Value;

                if (UId == null || string.IsNullOrWhiteSpace(UId))
                {
                    return -1;
                }

                return Int32.Parse(UId);

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IBaseResponse<User>> GetUserData()
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _userService.GetItem(UId);
            }

            return (IBaseResponse<User>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpDelete("profile")]
        public async Task<IBaseResponse<bool>> DeleteUser()
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _userService.RemoveItem(UId);
            }

            return (IBaseResponse<bool>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpPost("profile-restore")]
        public async Task<IBaseResponse<User>> RestoreUser()
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _userService.RestoreItem(UId);
            }

            return (IBaseResponse<User>)BadRequest("Value must be passed in the request body.");
        }

        [Authorize]
        [HttpPatch("profile")]
        public async Task<IBaseResponse<User>> EditUser(SignupViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                return await _userService.EditItem(UId, model);
            }

            return (IBaseResponse<User>)BadRequest("Value must be passed in the request body.");
        }
    }
}
