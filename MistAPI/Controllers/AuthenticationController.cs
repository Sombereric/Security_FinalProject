// FILE : AuthenticationController.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// Handles all authentication requests from the webpage to the server and database

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MistAPI.Data;
using MistAPI.Models.Entities;
using MistAPI.Models.Requests;
using MistAPI.Models.Responses;

namespace MistAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppDbContext AppDbContext;
        private readonly PasswordHasher<User> passwordHasher = new();
        public AuthenticationController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(UserRegisterRequest userRegisterRequest)
        {
            //checks if the email is already in use by another account.
            string emailToCheck = userRegisterRequest.UserEmail;
            User? existingUser = await AppDbContext.Users.FirstOrDefaultAsync(user => user.UserEmail == emailToCheck);
            //if the email is returned by the query then a account with that email already exists
            if (existingUser != null)
            {
                return BadRequest("Email already in use");
            }

            return Ok(userRegisterRequest);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserRegisterRequest userRegisterRequest)
        {
            return Ok(userRegisterRequest);
        }
    }
}