using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using organizer_backend_NET.Domain.Enums;
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
        public async Task<IActionResult> GetUserData()
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _userService.GetItem(UId);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpDelete("profile")]
        public async Task<IActionResult> DeleteUser()
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _userService.RemoveItem(UId);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost("profile-restore")]
        public async Task<IActionResult> RestoreUser()
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _userService.RestoreItem(UId);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPatch("profile")]
        public async Task<IActionResult> EditUser(SignupViewModel model)
        {
            int UId = GetUId();

            if (UId != -1)
            {
                var result = await _userService.EditItem(UId, model);

                if (result.StatusCode == EStatusCode.OK)
                {
                    return Created("", result.Data);
                }

                return BadRequest(result.Description);
            }

            return Unauthorized();
        }
    }
}
