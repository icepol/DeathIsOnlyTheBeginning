using pixelook;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Text soulsText;
    private Vector2 _position;

    private Animator _animator;
    
    void Start()
    {
        // move off the screen
        _position = transform.position;
        
        transform.position = new Vector3(10000, 10000, 0);

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        soulsText.text = GameState.SoulsSaved.ToString();
        transform.position = _position;
        _animator.enabled = true;
    }
}
