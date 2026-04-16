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
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<GameInApp>>> GetAllGamesByName()
        {
            List<GameInApp> gameInApp = await AppDbContext.GamesInApp.OrderBy(games => games.GameName).ToListAsync();

            return Ok();
        }
        [HttpGet("genre/{genre}")]
        public async Task<ActionResult<IEnumerable<GameInApp>>> GetAllGenreGames()
        {
            return Ok();
        }
        [HttpGet("publisher/{publisherId}")]
        public async Task<ActionResult<IEnumerable<GameInApp>>> GetAllPublisherGames()
        {
            return Ok();
        }
        [HttpGet("sort/price")]
        public async Task<ActionResult<IEnumerable<GameInApp>>> GetGamesBySortPrice()
        {
            return Ok();
        }
    }
}
