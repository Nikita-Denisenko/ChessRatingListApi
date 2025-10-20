using ChessRatingListApi.Entities;
using ChessRatingListApi.Models.Requests;
using ChessRatingListApi.Models.Responses;
using ChessRatingListApi.Services;
using Microsoft.AspNetCore.Mvc;
using static ChessRatingListApi.DataHelpers.DataHelper;

namespace ChessRatingListApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public IActionResult GetPlayers([FromQuery] PlayerFilter filter)   
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_playerService.GetPlayers(filter));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPlayerById([FromRoute] int id)
        {
            var player = _playerService.GetPlayerById(id);
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

            _playerService.AddPlayer(player);
            SavePlayers("Data/players.json", _playerService.Players);

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
            if (_playerService.GetPlayerById(id) is null)
                return NotFound("Ошибка, игрок с таким Id не найден!");
            _playerService.DeletePlayerById(id);
            SavePlayers("Data/players.json", _playerService.Players);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult EditPlayerInfo
        (
            [FromRoute] int id,
            [FromBody] EditPlayerRequest editPlayerRequest
        )
        {
            if (_playerService.GetPlayerById(id) is null)
                return NotFound("Ошибка, игрок с таким Id не найден!");
            _playerService.EditPlayer(id, editPlayerRequest);
            SavePlayers("Data/players.json", _playerService.Players);
            return Ok(editPlayerRequest);
        }
    }
}
