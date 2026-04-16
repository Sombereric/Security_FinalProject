// FILE : LibraryController.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// Handles the use of a users library, buying games and downloading games to their computer

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MistAPI.Data;
using MistAPI.Models.Entities;
using MistAPI.Models.Requests;

namespace MistAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly AppDbContext AppDbContext;
        public LibraryController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        /// <summary>
        /// fills the user owned games in the library page
        /// </summary>
        /// <param name="userId">the logged in user</param>
        /// <returns>returns a list of owned games</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserOwnedGame>>> GetUserOwnedGames(int userId)
        {
            //checks to see if the user exists
            User? user = await AppDbContext.Users
                         .FirstOrDefaultAsync(user => user.UserID == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            //queries all the games the user owns
            List<UserOwnedGame> ownedGames = await AppDbContext.UserOwnedGames
                                           .Where(userOwnedGames => userOwnedGames.UserID == userId)
                                           .Include(userOwnedGames => userOwnedGames.Game)
                                           .ThenInclude(game => game.Publisher)
                                           .ToListAsync();

            return Ok(ownedGames);
        }
        /// <summary>
        /// the api endpoint of purchasing a game
        /// NOTE: all payments are implied to be successful 
        /// within this prototype though the ground works for a payment system are implimented
        /// </summary>
        /// <param name="request">the request body of the game to purchase</param>
        /// <returns>the result of the game purchase</returns>
        [HttpPost("buy")]
        public async Task<IActionResult> BuyGame(GameItemRequest request)
        {
            //checks if the user exists
            User? user = await AppDbContext.Users
                         .FirstOrDefaultAsync(user => user.UserID == request.UserID);

            if (user == null)
            {
                return NotFound("User not found");
            }

            //checks if the game exists next
            GameInApp? game = await AppDbContext.GamesInApp
                              .FirstOrDefaultAsync(games => games.GameID == request.GameID);

            if (game == null)
            {
                return NotFound("Game not found.");
            }

            // checks if the user already owns the game
            UserOwnedGame? existingOwnedGame = await AppDbContext.UserOwnedGames
                .FirstOrDefaultAsync(userOwnedGames => userOwnedGames.UserID == request.UserID && userOwnedGames.GameID == request.GameID);

            if (existingOwnedGame != null)
            {
                return BadRequest("User already owns this game.");
            }
            
            // should all checks pass the user is then able to purchase a game
            // creates new owned game entry
            UserOwnedGame ownedGame = new UserOwnedGame(request.UserID, request.GameID, DateTime.Now);

            //updates the server
            AppDbContext.UserOwnedGames.Add(ownedGame);
            await AppDbContext.SaveChangesAsync();

            return Ok();
        }
        /// <summary>
        /// allowes users to download games to their computer
        /// </summary>
        /// <param name="request">the request body of the game to download</param>
        /// <returns>returns the result of that download</returns>
        [HttpPost("download")]
        public async Task<IActionResult> DownloadGame(GameItemRequest request)
        {
            UserOwnedGame? ownedGame = await AppDbContext.UserOwnedGames
                                      .Include(userOwnedGame => userOwnedGame.Game)
                                      .FirstOrDefaultAsync(userOwnedGame => userOwnedGame.UserID == request.UserID && userOwnedGame.GameID == request.GameID);

            if (ownedGame == null)
            {
                return BadRequest("User cannot download unowned game");
            }

            if (ownedGame.Game == null)
            {
                return NotFound("Game not found");
            }

            // creates a safe file name
            string gameName = ownedGame.Game.GameName;
            string safeFileName = string.Join("_", gameName.Split(Path.GetInvalidFileNameChars())) + ".txt";

            // creates the fake download file
            string fileContents = $"You downloaded {gameName} on {DateTime.Now}.";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), safeFileName);

            await System.IO.File.WriteAllTextAsync(filePath, fileContents);

            return Ok();
        }
    }
}
