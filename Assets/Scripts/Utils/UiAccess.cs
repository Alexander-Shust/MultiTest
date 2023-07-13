using System.Collections.Generic;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiAccess : MonoBehaviour
{
    [SerializeField] private TMP_Text _coins;

    [SerializeField] private Image _healthBar;
    
    public static UiAccess Get;

    private List<GameResult> _results;

    private List<PlayerRef> _deadPlayers;

    private void Awake()
    {
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
            ShowVictory(winner.PlayerRef, winnerCoins);
        }
    }

    private void ShowVictory(PlayerRef winner, int winnerCoins)
    {
        Debug.LogWarning($"Winner is {winner} with {winnerCoins} coins!");
    }

    public struct GameResult
    {
        public PlayerRef Ref;
        public int Coins;
    }
}