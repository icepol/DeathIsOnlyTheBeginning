using System;
using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FireBall fireBallPrefab;
    private FirstPersonController _controller;

    private bool _hasSoulTaken;
    private bool _isActive;
    
    public bool IsInPortal { get; private set; }

    public bool HasSoulTaken
    {
        get => _hasSoulTaken;

        set
        {
            if (_hasSoulTaken == value) return;

            _hasSoulTaken = value;

            EventManager.TriggerEvent(_hasSoulTaken ? Events.SOUL_TAKEN : Events.SOUL_SAVED);

            if (!_hasSoulTaken)
                // soul was saved
                GameState.SoulsSaved++;
        }
    }

    void Awake()
    {
        _controller = GetComponent<FirstPersonController>();
        
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.PORTAL_ENTERED, OnPortalEntered);
        EventManager.AddListener(Events.PORTAL_LEAVED, OnPortalLeaved);
    }

    private void Start()
    {
        // player is in portal by default
        IsInPortal = true;
    }

    private void Update()
    {
        if (!_isActive) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            Fire();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.PORTAL_ENTERED, OnPortalEntered);
        EventManager.RemoveListener(Events.PORTAL_LEAVED, OnPortalLeaved);
    }

    private void OnGameStarted()
    {
        _isActive = true;
    }
    
    private void OnPlayerDied()
    {
        _isActive = false;
        _controller.enabled = false;
    }

    private void OnPortalEntered()
    {
        IsInPortal = true;
    }

    private void OnPortalLeaved()
    {
        IsInPortal = false;
    }

    private void Fire()
    {
        Instantiate(fireBallPrefab, transform.localPosition, transform.localRotation);
    }
}
