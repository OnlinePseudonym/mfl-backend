using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MFL.Services.Players.Models;
using MFL.Services.Players;
using MFL.Data.SeedWork;

namespace MFL.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayersService _playersService;

        public PlayersController(PlayersService playersService)
        {
            _playersService = playersService;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            var players = await _playersService.GetAll();

            return Ok(players);
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _playersService.Get(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            var status = await _playersService.Put(id, player);
            
            if (status == EntityStatus.UnmatchedId)
            {
                return BadRequest();
            }
            else if (status == EntityStatus.EntityDoesntExist)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            var result = await _playersService.Post(player);

            return CreatedAtAction(nameof(GetPlayer), new { id = result.Id }, result);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var status = await _playersService.Delete(id);

            if (status == EntityStatus.EntityDoesntExist)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
