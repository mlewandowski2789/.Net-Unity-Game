
using SharedLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly string _serverIp = "26.180.139.241:7098";

    public static GameManager instance;
    public GameObject cam;
    public Player playerPrefab;
    public Enemy enemyPrefab;
    public DisplayManager menu;
    public int playerCount = 4;

    private int _gameId;
    private int _playerId;
    private Player player;
    private readonly List<Enemy> _enemies = new();

    private bool isPaused = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    }

    public async void CreateGame()
    {
        menu.HideButtons();
        var result = await NetworkManager.Get<GameInfo>($"https://{_serverIp}/game/create");
        _gameId = result.Id;
        
        _playerId = 0;
        SpawnPlayer();
    }

    public async void JoinGame()
    {
        _gameId = menu.GetInput();
        var result = await NetworkManager.Get<PlayerInfo>($"https://{_serverIp}/game/{_gameId}/join");
        _playerId = result.Id;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam.SetActive(false);
        player = Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        player.Init(_playerId);
        menu.ShowGameId(_gameId);

        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if (player == null) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) Pause();
        }
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {
            UpdateGame();
            yield return new WaitForSeconds(.04f);
        }
    }

    private async void UpdateGame()
    {
        var playerInfo = player.GetInfo();
        GameInfo result;
        try
        {
            result = await NetworkManager.Post<GameInfo>($"https://{_serverIp}/game/{_gameId}", playerInfo);
        }

        catch (Exception)
        {
            return;
        }
        var players = result.Players;
        players.RemoveAt(_playerId);

        while (players.Count > _enemies.Count)
        {
            _enemies.Add(Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity));
        }
        while (players.Count < _enemies.Count)
        {
            
            Destroy(_enemies[^1].gameObject);
            _enemies.RemoveAt(_enemies.Count - 1); 
        }

        for (int i = 0; i < players.Count; i++)
        {
            _enemies[i].UpdateEnemy(players[i]);
        }
        await Task.Yield();
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        player.Pause(true);
        menu.Pause();

    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        player.Pause(false);
        menu.Resume();
    }
}
