using MFL.Services.Players.Models;
using MFL.Data.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using System;
using MFL.Services.Clients.Models;
using MFL.Services.Clients;
using MFL.Common.JsonConverters;
using MFL.Common.Extensions;
using MFL.Services.Serialization;

namespace MFL.Services.Players
{
    public class PlayersService
    {
        private readonly IMFLHttpClient _client;
        private readonly IRepository<Data.Players.Entities.Player> _players;
        private readonly IMapper _mapper;

        public PlayersService(IMFLHttpClient client, IRepository<Data.Players.Entities.Player> players, IMapper mapper)
        {
            _client = client;
            _players = players;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            var players = await _players.GetAll();
            var results = players.Select(x => _mapper.Map<Player>(x));
            return results;
        }

        public async Task<Player> Get(int id)
        {
            var player = await _players.Get(id);
            return _mapper.Map<Player>(player);
        }

        public async Task<IEnumerable<Player>> GetByIds(IEnumerable<int> ids)
        {
            var allPlayers = await _players.GetAll();
            var players = allPlayers.Where(x => ids.Contains(x.Id));

            return players.Select(x => _mapper.Map<Player>(x));
        }

        public async Task<Player> Post(Player player)
        {
            var entity = _mapper.Map<Data.Players.Entities.Player>(player);
            var res = await _players.Post(entity);
            return _mapper.Map<Player>(res);
        }

        public async Task<EntityStatus> Put(int id, Player player)
        {
            var entity = _mapper.Map<Data.Players.Entities.Player>(player);
            return await _players.Put(id, entity);
        }

        public async Task<EntityStatus> Delete(int id)
        {
            return await _players.Delete(id);
        }

        public async Task SyncPlayers(int year)
        {
            var lastModified = GetLastModifiedDate();

            if (lastModified.Date == DateTime.Today)
            {
                return;
            }

            var unixTime = ((DateTimeOffset)lastModified).ToUnixTimeSeconds();
            var endpoint = $"/{year}/export?TYPE=players&DETAILS=1&SINCE={unixTime}&JSON=1";
            MFLApiResponse results = await _client.GetFromJsonAsync(endpoint, new SingleOrManyConverter<PlayerDTO>());

            if (results.players != null)
            {
                var players = results.players.player;
                foreach(var playerDTO in players)
                {
                    var player = DTOSerializer.PlayerDTOtoEntity(playerDTO);
                    var res = await _players.Put(player.Id, player);

                    if (res == EntityStatus.EntityDoesntExist)
                    {
                        await _players.Post(player);
                    }
                }
            }
        }

        private DateTime GetLastModifiedDate()
        {
            var players = _players.GetAll().Result;

            var lastUpdated = players.OrderByDescending(x => x.UpdatedDate).FirstOrDefault();
            return lastUpdated?.UpdatedDate ?? DateTime.MinValue;
        }

        public async Task<IEnumerable<Player>> GetFreeAgents(string leagueId)
        {
            MFLApiResponse results = await _client.GetFromJsonAsync($"/2020/export?TYPE=freeAgents&L={leagueId}&JSON=1");
            var leagues = new List<Player>();

            var ids = results.freeAgents.leagueUnit.player.Select(x => x.id.ToInt());
            var players = await GetByIds(ids);

            return players;
        }
    }
}
