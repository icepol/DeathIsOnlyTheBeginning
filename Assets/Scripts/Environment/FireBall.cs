using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var fenix = other.GetComponent<Fenix>();
        
        if (!fenix) return;
        
        fenix.TakeDamage();
    }
}
