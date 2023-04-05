using HSStories.Models;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
=======
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
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
<<<<<<< HEAD
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            //Se busca el Usuario en nuestra base de datos , especificamente si existe un correo electronico igual al que pasamos.
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginModel.Email);
=======
       public ActionResult Login(string email , string password)
        {
            //Se busca el Usuario en nuestra base de datos , especificamente si existe un correo electronico igual al que pasamos.
            var user=_context.Users.SingleOrDefault(u => u.Email == email);
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e

            //En caso de no existir se tira un Unauthorized
            if (user == null)
            {
<<<<<<< HEAD
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
=======
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
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
