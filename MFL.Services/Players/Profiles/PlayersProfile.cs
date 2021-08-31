using AutoMapper;
using MFL.Services.Players.Models;

namespace MFL.Services.Players.Profiles
{
    public class PlayersProfile : Profile
	{
		public PlayersProfile()
		{
			CreateMap<Data.Players.Entities.Player, Player>();
			CreateMap<Player, Data.Players.Entities.Player>();
		}
	}
}
