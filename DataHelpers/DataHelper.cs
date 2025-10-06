using ChessRatingListApi.Entities;
using System.Numerics;
using System.Text.Json;

namespace ChessRatingListApi.DataHelpers
{
    public static class DataHelper
    {
        public static List<Player> LoadPlayers(string path)
        {
            var text = File.ReadAllText(path);
            var players = JsonSerializer.Deserialize<List<Player>>(text)
                ?? new List<Player> { };

            return players;
        }

        public static void SavePlayers(string path, List<Player> players)
        {
            var text = JsonSerializer.Serialize(players);
            File.WriteAllText(path, text);
        }
    }
}
