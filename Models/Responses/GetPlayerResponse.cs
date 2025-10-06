namespace ChessRatingListApi.Models.Responses
{
    public record GetPlayerResponse(int Id, string Name, string Surname, int Age, int Rating, string Federation);
}
