using UnityEngine;
using Random = UnityEngine.Random;

namespace pixelook
{
    public class DelayAnimation : MonoBehaviour
    {
        private Animator _animator;
        
        void Start()
        {
            _animator = GetComponent<Animator>();

            _animator.Play(
                _animator.GetCurrentAnimatorStateInfo(0).shortNameHash,
                0,
                Random.Range(0f, 1f));
        }
    }
}