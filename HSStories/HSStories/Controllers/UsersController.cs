﻿using HSStories.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Authorize]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null)

            {
                return NotFound();
            }

            return Ok(users);

        }


        [HttpGet("GetUser/{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int Id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == Id);


            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //Controlador par ala creacion de  un usuario Nuevo
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(User user)

        {
            try
            {
                if (user.Password != null)
                {
                    //Implementamos nuestro metodo de Hash para la contraseña 
                    string hashedPassword = HashPasswords.HashPassword(user.Password);
                    user.Password = hashedPassword;
                }

                _context.Users.Add(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            await _context.SaveChangesAsync();

            return Ok("Usuario Creado Correctamente");

        }


        [HttpPut("EditUser/{id}")]
        [Authorize]

        public async Task<ActionResult> EditUser(int id, User updatedUser)

        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {

                return BadRequest("No Existe El Usuario Especificado");

            }

            // Actualizar los campos del usuario
            user.Name = updatedUser.Name;
            user.LastName = updatedUser.LastName;
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.Phone = updatedUser.Phone;
            user.Adress = updatedUser.Adress;
            user.Birthday = updatedUser.Birthday;



            await _context.SaveChangesAsync();
            return Ok("Se han Modificado tus datos Correctamente");

        }

        //Metodo para Borrar un Usuario por su Id
        [HttpDelete("DeleteUser/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

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
