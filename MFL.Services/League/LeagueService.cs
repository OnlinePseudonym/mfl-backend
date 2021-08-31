using MFL.Common.Extensions;
using MFL.Services.Clients;
using MFL.Services.Clients.Models;
using MFL.Services.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFL.Services.League
{
    public class LeagueService
    {
        private IMFLHttpClient _client;

        public LeagueService(IMFLHttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Models.League>> GetMyLeagues()
        {
            MFLApiResponse results = await _client.GetFromJsonAsync("/2020/export?TYPE=myleagues&FRANCHISE_NAMES=1&JSON=1");
            var leagues = new List<Models.League>();

            foreach (var leagueDto in results.leagues.league)
            {
                var league = await GetById(leagueDto.league_id.ToInt());

                var merged = league.Merge(DTOSerializer.LeagueInstanceDTOtoModel(leagueDto));
                leagues.Add(merged);
            }

            return leagues;
        }

        public async Task<Models.League> GetById(int id)
        {
            MFLApiResponse results = await _client.GetFromJsonAsync($"2020/export?TYPE=league&L={id}&JSON=1");
            var league = DTOSerializer.LeagueDTOtoModel(results.league);

            return league;
        }
    }
}
