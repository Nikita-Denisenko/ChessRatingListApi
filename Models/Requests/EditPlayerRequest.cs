using System.ComponentModel.DataAnnotations;

namespace ChessRatingListApi.Models.Requests
{
    public record EditPlayerRequest
    (
        [Required]
        string Name,
        [Required]
        string Surname,
        [Required]
        [Range(4, 100, ErrorMessage = "Возраст должен быть от 3 до 100!")]
        int Age,
        [Required]
        [Range(0, 3000, ErrorMessage = "Рейтинг должен быть в диапазоне от 0 до 3000")]
        int Rating,
        string Federation
     );
}
