using ChessRatingListApi.Constans;
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

        public List<Player> GetPlayers(PlayerFilter filter)
        {
            var query = Players.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Surname))
                query = query.Where(p => p.Surname.Contains(filter.Surname));

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (filter.MinRating != null)
                query = query.Where(p => p.Rating >= filter.MinRating);

            if (filter.MaxRating != null)
                query = query.Where(p => p.Rating <= filter.MaxRating);

            if (filter.Federation != null)
                query = query.Where(p => p.Federation.Contains(filter.Federation));

            query = filter.SortBy switch
            {
                OrderParams.id => filter.Descending
                    ? query.OrderByDescending(p => p.Id)
                    : query.OrderBy(p => p.Id),

                OrderParams.surname => filter.Descending
                    ? query.OrderByDescending(p => p.Surname)
                    : query.OrderBy(p => p.Surname),

                OrderParams.name => filter.Descending
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name),

                OrderParams.age => filter.Descending
                    ? query.OrderByDescending(p => p.Age)
                    : query.OrderBy(p => p.Age),

                OrderParams.rating => filter.Descending
                   ? query.OrderByDescending(p => p.Rating)
                   : query.OrderBy(p => p.Rating),

                _ => filter.Descending
                    ? query.OrderByDescending(p => p.Id)
                    : query.OrderBy(p => p.Id),
            };

            query = query
                .Skip((filter.Page - 1) * filter.Size)
                .Take(filter.Size);

            return query.ToList();
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
    }
}
