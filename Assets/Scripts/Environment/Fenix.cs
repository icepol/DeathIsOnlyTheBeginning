using System.Collections;
using pixelook;
using UnityEngine;
using UnityEngine.AI;

public class Fenix : MonoBehaviour
{
    [SerializeField] private float minDelayAfterSpawn = 0.5f;
    [SerializeField] private float maxDelayAfterSpawn = 1.5f;

    [SerializeField] private float minDelayBetweenVoice = 3f;
    [SerializeField] private float maxDelayBetweenVoice = 10f;
    
    [SerializeField] private float startSpeed = 1;
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private float increaseSpeedBy = 0.25f;

    [SerializeField] private ParticleSystem explosion;

    [SerializeField] private AudioClip fenixDied;
    [SerializeField] private AudioClip[] fenixVoices;

    private Player _player;
    private Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;
    private AudioSource _audioSource;

    private float _currentSpeed;
    private Vector3 _startingPosition;
    private Vector3 _targetPosition;

    private bool _isActive;
    private bool _isChasing;

    private void Awake()
    {
        EventManager.AddListener(Events.PORTAL_LEAVED, OnPortalLeaved);
        EventManager.AddListener(Events.PORTAL_ENTERED, OnPortalEntered);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);

        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _startingPosition = transform.position;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _playerTransform = _player.transform;

        StartCoroutine(WaitAndActivate());
    }
    
    void Update()
    {
        if (!_isActive) return;
        if (!_isChasing) return;

        var currentPosition = _playerTransform.position;
        currentPosition.y = transform.position.y;

        // increase speed
        _currentSpeed += increaseSpeedBy * Time.deltaTime;
        if (_currentSpeed > maxSpeed)
            _currentSpeed = maxSpeed;
        
        _navMeshAgent.speed = _currentSpeed;
        
        // update target position
        if (Vector3.Distance(currentPosition, _targetPosition) < 0.05f)
            return;

        _targetPosition = currentPosition;
        _navMeshAgent.SetDestination(_targetPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;
        
        if (other.CompareTag("Player"))
            EventManager.TriggerEvent(Events.PLAYER_DIED);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PORTAL_LEAVED, OnPortalLeaved);
        EventManager.RemoveListener(Events.PORTAL_ENTERED, OnPortalEntered);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        StopChasing();
    }

    private void OnPortalLeaved()
    {
        _isChasing = true;
    }

    private void OnPortalEntered()
    {
        StopChasing();
    }

    private void StopChasing()
    {
        _navMeshAgent.SetDestination(_startingPosition);
        _currentSpeed = startSpeed;
        _isChasing = false;
    }

    public void TakeDamage()
    {
        var position = transform.position;
        
        Instantiate(explosion, position, Quaternion.identity);
        
        EventManager.TriggerEvent(Events.FENIX_KILLED, position);

        AudioSource.PlayClipAtPoint(fenixDied, this.gameObject.transform.position);
        
        Destroy(gameObject);
    }

    IEnumerator WaitAndActivate()
    {
        yield return new WaitForSeconds(Random.Range(minDelayAfterSpawn, maxDelayAfterSpawn));

        _isActive = true;
        
        if (!_player.IsInPortal)
            _isChasing = true;

        yield return StartCoroutine(WaitAndPlayVoice());
    }

    IEnumerator WaitAndPlayVoice()
    {
        while (true)
        {
            if (!_isActive)
                yield break;
            
            _audioSource.clip = fenixVoices[Random.Range(0, fenixVoices.Length)];
            _audioSource.Play();
            
            yield return new WaitForSeconds(Random.Range(minDelayBetweenVoice, maxDelayBetweenVoice));
        }
    }
}
