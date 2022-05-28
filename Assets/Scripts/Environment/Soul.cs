using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Soul : MonoBehaviour
{
    [SerializeField] float pickDestroyDelay = 1;
    [SerializeField] private ParticleSystem soulParticles;
    
    [SerializeField] private float minDelayBetweenVoice = 3f;
    [SerializeField] private float maxDelayBetweenVoice = 10f;
    
    [SerializeField] private AudioClip[] soulVoices;
    
    private AudioSource _audioSource;
    
    private bool _isActive = true;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(WaitAndPlayVoice());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;
            
        var player = other.GetComponent<Player>();
        
        if (!player) return;

        _isActive = false;
        
        player.HasSoulTaken = true;
        soulParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        
        Destroy(gameObject, pickDestroyDelay);
    }

    IEnumerator WaitAndPlayVoice()
    {
        while (true)
        {
            if (!_isActive)
                yield break;
            
            _audioSource.clip = soulVoices[Random.Range(0, soulVoices.Length)];
            _audioSource.Play();
            
            yield return new WaitForSeconds(Random.Range(minDelayBetweenVoice, maxDelayBetweenVoice));
        }
    }
}
