// FILE : AuthenticationController.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// Handles all authentication requests from the webpage to the server and database

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
        private logger logToServer;
        public AuthenticationController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            logToServer = new logger(appDbContext);
        }
        /// <summary>
        /// this function allows users to registor new accounts
        /// </summary>
        /// <param name="userRegisterRequest">the user account request object</param>
        /// <returns>the outcome of the account creation request</returns>
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(UserRegisterRequest userRegisterRequest)
        {
            //checks if the email is already in use by another account.
            string emailToCheck = userRegisterRequest.UserEmail;
            User? userWithSameEmail = await AppDbContext.Users.FirstOrDefaultAsync(user => user.UserEmail == emailToCheck);
            //if the email is returned by the query then a account with that email already exists
            if (userWithSameEmail != null){
                return BadRequest("Email already in use");
            }

            //creates a new user object
            User user = new User(userRegisterRequest.UserName, emailToCheck);

            //hashses the password
            user.UserPasswordHash = passwordHasher.HashPassword(user, userRegisterRequest.Password);

            //sends the user data to the server
            AppDbContext.Users.Add(user);
            await AppDbContext.SaveChangesAsync();

            Log log = new Log(user.UserID, DateTime.Now, "User Created", 
                              "A new user has been created within the system using: " + user.UserEmail);

            await logToServer.LogToDb(log);

            //builds the response to the browser.
            AuthenticationResponse authenticationResponse =
                new AuthenticationResponse(user.UserID, user.UserName, user.UserEmail, "User registered successfully.");

            return Ok(authenticationResponse);
        }
        /// <summary>
        /// handles user logins
        /// </summary>
        /// <param name="userRegisterRequest">stores user login data</param>
        /// <returns>returns a login result to the webpage</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(LoginRequest userLoginRequest)
        {
            //checks if the user email and account exists on login attempt
            User? user = await AppDbContext.Users.FirstOrDefaultAsync(user => user.UserEmail == userLoginRequest.UserEmail);

            //if no user is returned then the email or password is wrong or no account exists
            if (user == null){
                return BadRequest("Email already in use");
            }

            //checks to see if the password is the same
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(
              user, user.UserPasswordHash, userLoginRequest.Password);

            //handles failed password matches
            if (result == PasswordVerificationResult.Failed){
                return Unauthorized("Invalid email or password");
            }

            Log log = new Log(user.UserID, DateTime.Now, "User Login", "A user using this email has logged-in: " + user.UserEmail);

            await logToServer.LogToDb(log);

            //builds response for the webpage to use should login be successful
            AuthenticationResponse authenticationResponse =
               new AuthenticationResponse(user.UserID, user.UserName, user.UserEmail, "Login successful.");

            return Ok(authenticationResponse);
        }
    }
}