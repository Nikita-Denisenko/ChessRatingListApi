using ChessRatingListApi.Entities;
using System.Text.Json;

namespace ChessRatingListApi.DataHelpers
{
    public static class DataHelper
    {
        public static List<Player> LoadPlayers(string path)
        {
            try
            {
                var text = File.ReadAllText(path);
                var players = JsonSerializer.Deserialize<List<Player>>(text)
                    ?? new List<Player>();

                return players;
            }
            catch(FileNotFoundException ex) 
            {
                Console.WriteLine("Ошибка! Файл не найден.");
                return new List<Player>();
            }
        }

        public static void SavePlayers(string path, List<Player> players)
        {
            var text = JsonSerializer.Serialize(players);
            File.WriteAllText(path, text);
        }
    }
}
