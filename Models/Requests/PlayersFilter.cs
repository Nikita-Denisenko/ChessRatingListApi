using ChessRatingListApi.Constans;

namespace ChessRatingListApi.Models.Requests
{
    public class PlayerFilter
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
        public string? Federation { get; set; }
        public string SortBy { get; set; } = OrderParams.id;
        public bool Descending { get; set; } = false;
    }
}
