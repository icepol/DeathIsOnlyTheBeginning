using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        [SerializeField] private AudioClip playerObstacleContact;
        [SerializeField] private AudioClip playerGatePassed;
        [SerializeField] private AudioClip playerGatePassedPhase2;
        [SerializeField] private AudioClip playerGatePassedPhase3;
        [SerializeField] private AudioClip playerGateDestroyed;

        private void OnEnable()
        {
            //EventManager.AddListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
        }

        private void OnDisable()
        {
            //EventManager.RemoveListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
        }
        
        private void OnPlayerGateDestroyed()
        {
            if (playerGateDestroyed && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerGateDestroyed, targetTransform.position);
        }
    }
}