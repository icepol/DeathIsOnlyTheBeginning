using System.Collections;
using pixelook;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float restartLevelDelay = 5;
    [SerializeField] GameSetup gameSetup;
    
    private bool _isGameRunning;

    public static GameManager Instance { get; private set; }
    public GameSetup GameSetup => gameSetup;

    private void Awake()
    {
        Instance = this;

        GameState.OnApplicationStarted();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }
    
    private void Update()
    {
        if (_isGameRunning) return;

        if (!Input.anyKey) return;
        
        _isGameRunning = true;
        EventManager.TriggerEvent(Events.GAME_STARTED);
    }

    private void OnGameStarted()
    {
        GameState.OnGameStarted();
    }
    
    private void OnPlayerDied()
    {
        StartCoroutine(WaitAndRestart());
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(restartLevelDelay);
        
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
