using MFL.Data.Models;
using MFL.Data.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFL.Services.Players
{
    public class PlayersService
    {
        private readonly IRepository<Player> _players;

        public PlayersService(IRepository<Player> players)
        {
            _players = players;
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            return await _players.GetAll();
        }

        public async Task<Player> Get(int id)
        {
            return await _players.Get(id);
        }

        public async Task<Player> Post(Player player)
        {
            return await _players.Post(player);
        }

        public async Task<EntityStatus> Put(int id, Player player)
        {
            return await _players.Put(id, player);
        }

        public async Task<EntityStatus> Delete(int id)
        {
            return await _players.Delete(id);
        }
    }
}
