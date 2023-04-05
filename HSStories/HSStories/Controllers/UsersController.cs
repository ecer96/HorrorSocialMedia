using HSStories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
using System.Text;

namespace HSStories.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly HsstoriesContext _context;

        public UsersController(HsstoriesContext context)
        {
            _context = context;
        }

        [HttpGet("GetUsers")]
<<<<<<< HEAD
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null)
=======
        public ActionResult<User> GetUsers()
        {
            var users = _context.Users.ToList();

            if (users == null || users.Count == 0)
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
            {
                return NotFound();
            }

            return Ok(users);

        }

<<<<<<< HEAD
        [HttpGet("GetUser/{username}")]
        public async Task<ActionResult<User>> GetUser(int Id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == Id);
=======
        [HttpGet("GetUser/{id}")]
        public ActionResult GetUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPost("CreateUser")]
<<<<<<< HEAD
        public async Task<ActionResult> CreateUser(User user)
=======
        public async Task<ActionResult> CreateUser([FromForm] User user)
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
        {
            try
            {
                if (user.Password != null)
                {
                    string hashedPassword = HashPasswords.HashPassword(user.Password);
                    user.Password = hashedPassword;
                }

<<<<<<< HEAD
=======
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        user.ProfilePhoto = ms.ToArray();
                    }
                }



>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
                _context.Users.Add(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await _context.SaveChangesAsync();
<<<<<<< HEAD
           
            return Ok("Usuario Creado Correctamente");
=======
            return Ok();
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
        }



        [HttpPut("EditUser/{id}")]
<<<<<<< HEAD
        public async Task<ActionResult> EditUser(int id, User updatedUser)
=======
        public async Task<ActionResult> EditUser(int id, [FromForm] User updatedUser)
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
<<<<<<< HEAD
                return BadRequest("No Existe El Usuario Especificado");
=======
                return BadRequest();
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
            }

            // Actualizar los campos del usuario
            user.Name = updatedUser.Name;
            user.LastName = updatedUser.LastName;
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.Phone = updatedUser.Phone;
            user.Adress = updatedUser.Adress;
            user.Birthday = updatedUser.Birthday;

<<<<<<< HEAD

            await _context.SaveChangesAsync();
            return Ok("Se han Modificado tus datos Correctamente");
=======
            // Procesar la imagen si se proporcionó
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    user.ProfilePhoto = ms.ToArray();
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
        }


        [HttpDelete("DeleteUser/{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

=======
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(user);
        }


    }
}
