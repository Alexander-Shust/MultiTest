using System.Collections.Generic;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiAccess : MonoBehaviour
{
    [SerializeField] private TMP_Text _coins;

    [SerializeField] private Image _healthBar;

    [SerializeField] private GameObject _victoryPanel;

    [SerializeField] private TMP_Text _winner;

    [SerializeField] private TMP_Text _runnerUp;

    public static UiAccess Get;

    private List<GameResult> _results;

    private List<PlayerRef> _deadPlayers;

    private void Awake()
    {
        _victoryPanel.SetActive(false);
        _results = new();
        _deadPlayers = new();
        Get = this;
    }

    public void SetCoins(int amount)
    {
        _coins.text = amount.ToString();
    }

    public void SetHealth(float amount)
    {
        _healthBar.fillAmount = amount / 100.0f;
    }

    public void AddResult(PlayerRef playerRef, int coins)
    {
        _results.Add(new GameResult
        {
            Ref = playerRef,
            Coins = coins
        });
        _deadPlayers.Add(playerRef);
        CheckForVictory();
    }

    private void CheckForVictory()
    {
        var players = FindObjectsOfType<PlayerHandler>();
        var aliveCount = players.Count(p => !_deadPlayers.Contains(p.PlayerRef));
        if (aliveCount == 1)
        {
            var winner = players.First(p => !_deadPlayers.Contains(p.PlayerRef));
            var winnerCoins = players.First(p => p == winner).CollectedCoins;
            _victoryPanel.SetActive(true);
            _winner.text = $"The winner is: {winner} ({winnerCoins} coins)";
            var runnersUp = string.Empty;
            foreach (var dead in _deadPlayers)
            {
                var deadCoins = players.First(p => p.PlayerRef == dead).CollectedCoins;
                runnersUp += $"{dead} ({deadCoins})  ";
            }

            _runnerUp.text = "Dead: " + runnersUp;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public struct GameResult
    {
        public PlayerRef Ref;
        public int Coins;
    }
}