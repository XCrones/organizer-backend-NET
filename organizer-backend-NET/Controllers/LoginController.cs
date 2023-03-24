using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using organizer_backend_NET.Domain.Entity;
using organizer_backend_NET.Domain.Enums;
using organizer_backend_NET.Domain.Interfaces;
using organizer_backend_NET.Domain.Response;
using organizer_backend_NET.Domain.ViewModel;
using organizer_backend_NET.Implements.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace organizer_backend_NET.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly JWTSettings _options;

        public LoginController(IUserService userService, IOptions<JWTSettings> optAccess)
        {
            _userService = userService;
            _options = optAccess.Value;
        }

        private string GenerateToken(int UId)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UId", $"{UId}"));

            var signUpKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

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
        public async Task<ISignResponse<User>> SignUp(SignupViewModel model)
        {
            var key = _options.SecretKey;
            Console.WriteLine(key);

            var response = await _userService.SignUp(model);

            if (response.StatusCode != EStatusCode.OK)
            {
                Response.StatusCode = 400;
                return new SignInResponse<User>()
                {
                    Description = response.Description
                };
            }

            var token = GenerateToken(response.Data.UId);

            return new SignInResponse<User>() { 
                Token = token,
                UserData = response.Data
            };
        }

        [HttpPost("signin")]
        public async Task<ISignResponse<User>> SignIn(SigninViewModel model) {

            var key = _options.SecretKey;
            Console.WriteLine(key);

            var response = await _userService.SignIn(model);

            if (response.StatusCode != EStatusCode.OK)
            {
                Response.StatusCode = 400;
                return new SignInResponse<User>()
                {
                    Description = response.Description
                };
            }

            var token = GenerateToken(response.Data.UId);

            return new SignInResponse<User>()
            {
                Token = token,
                UserData = response.Data
            };
        }
    }
}
