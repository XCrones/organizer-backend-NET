using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Response;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Implements.Interfaces;
using organizer_backend_NET.Response;
using organizer_backend_NET.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace organizer_backend_NET.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly JWTSettings _options;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IUserService userService, IOptions<JWTSettings> optAccess, ILogger<LoginController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into LoginController");
            _userService = userService;
            _options = optAccess.Value;
        }

        private string GenerateToken(int UId)
        {
            var key = _options.SecretKey;

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UId", $"{UId}"));

            var signUpKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(signUpKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {
            var result = await _userService.SignUp(model);

            if (result.StatusCode != HttpStatusCode.Created)
            {
                return BadRequest(new ActionResponse<User>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                });
            }

            var token = GenerateToken(result.Data.UId);

            return Created("", new LoginResponse {
                Code = result.StatusCode,
                Message = result.Description,
                Data = new SignResponse()
                {
                    Email = result.Data.Email,
                    Name = result.Data.Name,
                    UrlAvatar = result.Data.UrlAvatar,
                },
                Token = token
            });
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SigninViewModel model) {

            var result = await _userService.SignIn(model);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest(new ActionResponse<SignResponse>
                {
                    Message = result.Description,
                    Code = result.StatusCode,
                });
            }

            var token = GenerateToken(result.Data.UId);

            return Ok(new LoginResponse
            {
                Code = result.StatusCode,
                Message = result.Description,
                Data = new SignResponse() {
                    Email = result.Data.Email,
                    Name = result.Data.Name,
                    UrlAvatar = result.Data.UrlAvatar,
                },
                Token = token
            });
        }
    }
}
