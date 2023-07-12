using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _roomName;
    [SerializeField] private NetworkRunner _networkRunnerPrefab;

    private NetworkRunner _runnerInstance;
    
    public async void StartGame()
    {
        _runnerInstance = FindObjectOfType<NetworkRunner>();
        if (_runnerInstance == null)
        {
            _runnerInstance = Instantiate(_networkRunnerPrefab);
        }
        
        _runnerInstance.ProvideInput = true;

        var startGameArgs = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = _roomName.text
        };
        await _runnerInstance.StartGame(startGameArgs);
    }
}