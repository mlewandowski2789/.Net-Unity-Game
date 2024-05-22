using SharedLibrary;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

namespace Server.Secvices;

public class GameService
{
    public List<GameInfo> games = new();

    public GameInfo CreateGame()
    {
        var game = new GameInfo() { Id = games.Count};
        game.Players.Add(new PlayerInfo() { Id = 0 });
        games.Add(game);

        return game;
    }

    public PlayerInfo JoinGame(int id)
    {
        var game = games[id] ?? throw new Exception("No games available");
        game.Players.Add(new PlayerInfo() { Id = game.Players.Count });

        return game.Players.Last();
    }

    public void SetPlayer(int gameId, int playerId, PlayerInfo player)
    {
        var game = games[gameId] ?? throw new Exception("Game not found");
        game.Players[playerId] = new PlayerInfo()
        {
            Id = player.Id,
            Health = player.Health,
            Bullets = new List<BulletInfo>(player.Bullets),
            Position = new float[] { player.Position[0], player.Position[1], player.Position[2] },
            Velocity = new float[] { player.Velocity[0], player.Velocity[1], player.Velocity[2] },
            Rotation = new float[] { player.Rotation[0], player.Rotation[1], player.Rotation[2] }
        };

    }

    public GameInfo GetGame(int id)
    {
        return games[id] ?? throw new Exception("Game not found");
    }

}
