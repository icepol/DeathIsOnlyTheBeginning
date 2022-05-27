using UnityEngine;

public class DisableForWebGL : MonoBehaviour
{
#if UNITY_WEBGL
    void Awake()
    {
        gameObject.SetActive(false);
    }
#endif
}
