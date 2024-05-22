using Microsoft.AspNetCore.Mvc;
using Server.Secvices;
using SharedLibrary;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("{id}")]
    public GameInfo Post(int id, [FromBody] PlayerInfo player)
    {
        _gameService.SetPlayer(id, player.Id, player);
        var game = new GameInfo(_gameService.GetGame(id));
        return game;
    }

    [HttpGet("create")]
    public ActionResult<GameInfo> CreateGame()
    {
        var game = _gameService.CreateGame();
        return Ok(game);
    }

    [HttpGet("{id}/join")]
    public ActionResult<PlayerInfo> JoinGame(int id)
    {
        var player = _gameService.JoinGame(id);
        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);
    }

    [HttpGet]
    public GameService GetGame()
    {
        return _gameService;
    }
}