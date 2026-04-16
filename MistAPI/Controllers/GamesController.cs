// FILE : GamesController.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// The controller that handles all game actions, browsing sorting by genre, publisher or price.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MistAPI.Data;
using MistAPI.Models.Entities;

namespace MistAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext AppDbContext;
        public GamesController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        /// <summary>
        /// searches all games sorted by name aphebetically 
        /// </summary>
        /// <returns>returns the list of games found</returns>
        [HttpGet("sort/name")]
        public async Task<ActionResult<IEnumerable<GameInApp>>> GetAllGamesByName()
        {
            //our query builder using parameterized inputs
            List<GameInApp> gamesInApp = await AppDbContext.GamesInApp
                                         .Include(games => games.Publisher)
                                         .OrderBy(games => games.GameName)
                                         .ToListAsync();

            return Ok(gamesInApp);
        }
        /// <summary>
        /// searches all games by a genre
        /// </summary>
        /// <param name="genre">the genre to search</param>
        /// <returns>returns the list of games found</returns>
        [HttpGet("genre/{genre}")]
        public async Task<ActionResult<IEnumerable<GameInApp>>> GetAllGenreGames(string genre)
        {
            //our query builder using parameterized inputs
            List<GameInApp> gamesInApp = await AppDbContext.GamesInApp
                                         .Include(games => games.Publisher)
                                         .Where(games => games.GameGenre != null && games.GameGenre.Contains(genre))
                                         .ToListAsync();
            return Ok(gamesInApp);
        }
        /// <summary>
        /// searching all games by a publisher
        /// </summary>
        /// <param name="publisherName">the name of the publisher to search</param>
        /// <returns>returns the list of games found</returns>
        [HttpGet("publisher/{publisherName}")]
        public async Task<ActionResult<IEnumerable<GameInApp>>> GetAllPublisherGames(string publisherName)
        {
            //searches the db for a publisher name using a patramiterized query.
            Publisher? publisher = await AppDbContext.Publishers
                                  .FirstOrDefaultAsync(p => p.PublisherName.ToLower() == publisherName.ToLower());

            //returns should no publisher be found
            if (publisher == null)
            {
                return NotFound("Publisher not found.");
            }

            //searches for the game by publisher id
            List<GameInApp> gamesInApp = await AppDbContext.GamesInApp
                                         .Include(games => games.Publisher)
                                         .Where(g => g.PublisherID == publisher.PublisherID)
                                         .ToListAsync();

            return Ok(gamesInApp);
        }
        /// <summary>
        /// search games sorted by price
        /// </summary>
        /// <returns>returns the sorted list of games</returns>
        [HttpGet("sort/price")]
        public async Task<ActionResult<IEnumerable<GameInApp>>> GetGamesBySortPrice()
        {
            //searches for all games sorting by price
            List<GameInApp> gamesInApp = await AppDbContext.GamesInApp
                                         .Include(games => games.Publisher)
                                         .OrderBy(games => games.GamePrice)
                                         .ToListAsync();
            return Ok(gamesInApp);
        }
    }
}