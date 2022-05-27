using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] float pickDestroyDelay = 1;
    [SerializeField] private ParticleSystem soulParticles;
    
    private bool _isActive = true;

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
}
