using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] private float[] rotations;
    [SerializeField] private float rotationVariation = 0.1f;
        
    void Start()
    {
        transform.Rotate(
            Vector3.up * (
                rotations[Random.Range(0, rotations.Length)] + Random.Range(-rotationVariation, rotationVariation)
                ), Space.Self);
    }
}
