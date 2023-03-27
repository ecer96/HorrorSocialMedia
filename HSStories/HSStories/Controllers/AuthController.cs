using HSStories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HSStories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HsstoriesContext _context;

        public AuthController(HsstoriesContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
       public ActionResult Login(string email , string password)
        {
            //Se busca el Usuario en nuestra base de datos , especificamente si existe un correo electronico igual al que pasamos.
            var user=_context.Users.SingleOrDefault(u => u.Email == email);

            //En caso de no existir se tira un Unauthorized
            if (user == null)
            {
                return Unauthorized("Credentials Incorrect , Please Verify youre Email...");
            }

            //Se  verifica la password por medio de nuestro metodo Verify Password de HashPassword 

            bool isPasswordValid = HashPasswords.VerifyPassword(password, user.Password );

            //Si no coincide tirara un bool como false y mandara unauthorized
            if(!isPasswordValid)
            {
                return Unauthorized("Password Incorrect , Please Verify youre Password....");
            }

            //Se crean las claims que almacenaremos en el jwt
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecret987654321"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "Hsstories",
                audience: "Hsstories",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials:credentials
                ); 

            return Ok(new {token=new JwtSecurityTokenHandler().WriteToken(token)});

        }
    }
}
