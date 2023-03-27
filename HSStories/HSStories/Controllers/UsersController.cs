using HSStories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<User> GetUsers()
        {
            var users = _context.Users.ToList();

            if (users == null || users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);

        }

        [HttpGet("GetUser/{id}")]
        public ActionResult GetUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromForm] User user)
        {
            try
            {
                if (user.Password != null)
                {
                    string hashedPassword = HashPasswords.HashPassword(user.Password);
                    user.Password = hashedPassword;
                }

                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        user.ProfilePhoto = ms.ToArray();
                    }
                }



                _context.Users.Add(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }



        [HttpPut("EditUser/{id}")]
        public async Task<ActionResult> EditUser(int id, [FromForm] User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return BadRequest();
            }

            // Actualizar los campos del usuario
            user.Name = updatedUser.Name;
            user.LastName = updatedUser.LastName;
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.Phone = updatedUser.Phone;
            user.Adress = updatedUser.Adress;
            user.Birthday = updatedUser.Birthday;

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
        }


        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
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
