using pixelook;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private ParticleSystem portalOpen;

    private void Awake()
    {
        EventManager.AddListener(Events.SOUL_TAKEN, OnSoulTaken);
        EventManager.AddListener(Events.SOUL_SAVED, OnSoulSaved);
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
    }

    private void OnSoulTaken()
    {
        portalOpen.Play();
    }
}
