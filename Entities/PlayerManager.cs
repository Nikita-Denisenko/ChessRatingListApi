using ChessRatingListApi.Models.Requests;

namespace ChessRatingListApi.Entities
{
    public class PlayerManager
    {
        public List<Player> Players { get; set; }

        public PlayerManager(List<Player> players)
        {
            Players = players;
        }

        public Player? GetPlayerById(int id)
        {
            var player = Players.FirstOrDefault(player => player.Id == id);
            return player;
        }

        public void AddPlayer(Player player) => Players.Add(player);

        public void DeletePlayerById(int id)
        {
            var player = GetPlayerById(id);
            if (player is null) return;
            Players.Remove(player);
        }

        public void EditPlayer(int id, EditPlayerRequest newInfoPlayer)
        {
            var player = GetPlayerById(id);
            if (player is null) return;

            player.Name = newInfoPlayer.Name;
            player.Surname = newInfoPlayer.Surname;
            player.Age = newInfoPlayer.Age;
            player.Rating = newInfoPlayer.Rating;
            player.Federation = newInfoPlayer.Federation;
        }
        public List<Player> GetOrderedByRating(bool byDescending)
        {
            var orderedPlayers = byDescending
                ? Players.OrderByDescending(player => player.Rating).ToList()
                : Players.OrderBy(player => player.Rating).ToList();

            return orderedPlayers;
        }
    }
}
