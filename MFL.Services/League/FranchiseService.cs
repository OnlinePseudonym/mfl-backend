using MFL.Common.Extensions;
using MFL.Services.Clients;
using MFL.Services.Clients.Models;
using MFL.Services.League.Models;
using MFL.Services.Players;
using MFL.Services.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFL.Services.League
{
    public class FranchiseService
    {
        private IMFLHttpClient _client;
        private PlayersService _playerService;
        public FranchiseService(IMFLHttpClient client, PlayersService playerService)
        {
            _client = client;
            _playerService = playerService;
        }

        public async Task<IEnumerable<Franchise>> GetAll(int leagueId)
        {
            IEnumerable<FranchiseDTO> franchiseDTOs = await GetFranchises(leagueId);
            var franchises = franchiseDTOs.Select(x => GetFranchiseFromDTO(x));

            return franchises;
        }

        public async Task<Franchise> GetById(int leagueId, int franchiseId)
        {
            FranchiseDTO franchiseDTO = await GetFranchiseById(leagueId, franchiseId);
            var franchise = GetFranchiseFromDTO(franchiseDTO);
            return franchise;
        }

        private Franchise GetFranchiseFromDTO(FranchiseDTO dto)
        {
            var franchise = DTOSerializer.FranchiseDTOtoModel(dto);
            franchise.Roster = _playerService.GetByIds(dto.player.Select(x => x.id.ToInt())).Result;

            return franchise;
        }

        private async Task<IEnumerable<FranchiseDTO>> GetFranchises(int leagueId)
        {
            MFLApiResponse result = await _client.GetFromJsonAsync($"/2020/export?TYPE=rosters&L={leagueId}&JSON=1");
            return result.rosters.franchise;
        }

        private async Task<FranchiseDTO> GetFranchiseById(int leagueId, int franchiseId)
        {
            MFLApiResponse result = await _client.GetFromJsonAsync($"/2020/export?TYPE=rosters&L={leagueId}&FRANCHISE={franchiseId:D4}&JSON=1");
            return result.rosters.franchise.FirstOrDefault();
        }
    }
}
