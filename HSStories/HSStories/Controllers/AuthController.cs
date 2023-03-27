using HSStories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            //Se busca el Usuario en nuestra base de datos , especificamente si existe un correo electronico igual al que pasamos.
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginModel.Email);

            //En caso de no existir se tira un Unauthorized
            if (user == null)
            {
                return Unauthorized("Credenciales Incorrectas , Favor de Verificar su Informacion");
            }

            //Se  verifica la password por medio de nuestro metodo Verify Password de HashPassword 
            bool isPasswordValid = HashPasswords.VerifyPassword(loginModel.Password, user.Password);

            //Si no coincide tirara un bool como false y mandara unauthorized
            if (!isPasswordValid)
            {
                return Unauthorized("Contraseña Incorrecta , Favor de Verficarla");
            }

            string token=createToken(user);

            return Ok(token);
        }

        private string createToken(User user)
        {
            //Creamos los claims que contienen los datos para el usuario autenticado
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
               
            };
            //Creamos nuestra secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretToSecret987654321"));

            //creamos nuestra firma del token
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:credentials

                );

            var jwt=new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}