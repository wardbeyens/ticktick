using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TickTick.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public TokenController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateToken(LoginModal loginModal)
        {
            IActionResult response = Unauthorized();

            UserModal userModal = Authenticate(loginModal);

            if (userModal != null)
            {
                string tokenString = BuildToken();
                return Ok(new { token = tokenString });
            }
            return response;
        }

        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:ServerSecret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["JWT:Issuer"],
                configuration["JWT:Issuer"],
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModal Authenticate(LoginModal loginModal)
        {
            // Enkel bij dit voorbeeld niet in prod gebruiken
            if (loginModal.Username == "Ward" && loginModal.Password == "qwerty")
            {
                return new UserModal
                {
                    Name = loginModal.Username,
                    Email = "wardbeyens99@gmail.com"
                };
            }

            return null;
        }
    }
}
