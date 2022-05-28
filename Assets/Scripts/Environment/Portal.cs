using pixelook;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private ParticleSystem portalOpen;

    [SerializeField] private AudioClip portalOpenSound;
    [SerializeField] private AudioClip soulSavedSound;

    private AudioSource _audioSource; 
    

    private void Awake()
    {
        EventManager.AddListener(Events.SOUL_TAKEN, OnSoulTaken);
        EventManager.AddListener(Events.SOUL_SAVED, OnSoulSaved);

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        portalOpen.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        
        if (!player) return;
        
        EventManager.TriggerEvent(Events.PORTAL_ENTERED);

        if (player.HasSoulTaken)
            player.HasSoulTaken = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            EventManager.TriggerEvent(Events.PORTAL_LEAVED);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.SOUL_TAKEN, OnSoulTaken);
        EventManager.RemoveListener(Events.SOUL_SAVED, OnSoulSaved);
    }

    private void OnSoulSaved()
    {
        portalOpen.Stop();
        
        _audioSource.clip = soulSavedSound;
        _audioSource.loop = false;
        _audioSource.Play();
    }

    private void OnSoulTaken()
    {
        portalOpen.Play();

        _audioSource.clip = portalOpenSound;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
