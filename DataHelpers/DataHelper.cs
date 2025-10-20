using ChessRatingListApi.Entities;
using System.Text.Json;

namespace ChessRatingListApi.DataHelpers
{
    public static class DataHelper
    {
        public static List<Player> LoadPlayers(string path)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,  
                WriteIndented = true
            };

            var text = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Player>>(text, options)
                   ?? new List<Player>();
        }

        public static void SavePlayers(string path, List<Player> players)
        {
            var text = JsonSerializer.Serialize(players);
            File.WriteAllText(path, text);
        }
    }
}
