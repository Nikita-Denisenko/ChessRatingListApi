using ChessRatingListApi.Entities;
using ChessRatingListApi.Models.Requests;
using ChessRatingListApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using static ChessRatingListApi.DataHelpers.DataHelper;

namespace ChessRatingListApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private static readonly PlayerManager _playerManager = new PlayerManager(LoadPlayers("Data/players.json"));

        [HttpGet]
        public IActionResult GetPlayers()
        {
            return Ok(_playerManager.Players);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPlayerById([FromRoute] int id)
        {
            var player = _playerManager.GetPlayerById(id);
            if (player is null) return NotFound("Ошибка, игрок с таким Id не найден!");
            return Ok
                (
                    new GetPlayerResponse
                    (
                        player.Id,
                        player.Name,
                        player.Surname,
                        player.Age,
                        player.Rating,
                        player.Federation
                    )
                );
        }

        [HttpPost]
        public IActionResult CreatePlayer([FromBody] CreatePlayerRequest createPlayerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Игрок не создан!");

            var player = new Player
            (
                createPlayerRequest.Id,
                createPlayerRequest.Name,
                createPlayerRequest.Surname,
                createPlayerRequest.Age,
                createPlayerRequest.Rating,
                createPlayerRequest.Federation
            );

            return CreatedAtAction(
                nameof(GetPlayerById),
                new { id = player.Id },
                new GetPlayerResponse
                (
                    player.Id,
                    player.Name,
                    player.Surname,
                    player.Age,
                    player.Rating,
                    player.Federation
                )
            );
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePlayer([FromRoute] int id)
        {
            if (_playerManager.GetPlayerById(id) is null)
                return NotFound("Ошибка, игрок с таким Id не найден!");
            _playerManager.DeletePlayerById(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult EditPlayerInfo
        (
            [FromRoute] int id,
            [FromBody] EditPlayerRequest editPlayerRequest
        )
        {
            if (_playerManager.GetPlayerById(id) is null)
                return NotFound("Ошибка, игрок с таким Id не найден!");
            _playerManager.EditPlayer(id, editPlayerRequest);
            return Ok(editPlayerRequest);
        }

        [HttpGet]
        public IActionResult GerOrderedByRatingPlayers([FromQuery] bool byDescending = false)
        {
            var orderedPlayers = _playerManager.GetOrderedByRating(byDescending);
            return Ok(orderedPlayers);
        }
    }
}
