using System;
using UnityEngine;

namespace pixelook
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private float levelStartOffset;
        [SerializeField] private float fadeOutDuration = 0.5f;

        private float _volume;
        private float _currentFadeOutDuration;
        private bool _isFadingOut;
            
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            _volume = audioSource.volume;
        }

        private void OnEnable()
        {
            EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
            EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
            EventManager.AddListener(Events.MUSIC_SETTINGS_CHANGED, OnMusicSettingsChanged);
        }

        private void Start()
        {
            if (!Settings.IsMusicEnabled)
                audioSource.Stop();
        }

        private void Update()
        {
            if (_isFadingOut) FadeOut();
        }

        private void FadeOut()
        {
            audioSource.volume = Mathf.Lerp(_volume, 0, _currentFadeOutDuration / fadeOutDuration);

            if (_currentFadeOutDuration >= fadeOutDuration)
                _isFadingOut = false;

            _currentFadeOutDuration += Time.deltaTime;
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
            EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
            EventManager.RemoveListener(Events.MUSIC_SETTINGS_CHANGED, OnMusicSettingsChanged);
        }

        private void OnPlayerDied()
        {
            _isFadingOut = true;
        }

        private void OnGameStarted()
        {
            audioSource.time = levelStartOffset;
        }

        private void OnMusicSettingsChanged()
        {
            if (Settings.IsMusicEnabled)
                audioSource.Play();
            else
                audioSource.Stop();
        }
    }
}