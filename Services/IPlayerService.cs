using ChessRatingListApi.Entities;
using ChessRatingListApi.Models.Requests;

namespace ChessRatingListApi.Services
{
    public interface IPlayerService
    {
        public List<Player> Players { get; set; }
        public List<Player> GetPlayers(PlayerFilter filter);
        public Player? GetPlayerById(int id);
        public void AddPlayer(Player player) => Players.Add(player);
        public void DeletePlayerById(int id);
        public void EditPlayer(int id, EditPlayerRequest newInfoPlayer);
    }
}
