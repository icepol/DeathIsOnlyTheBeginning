using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPoolPrewarmSetup", menuName = "Assets/Object Pool Prewarm Setup")]
public class ObjectPoolPrewarmSetup : ScriptableObject
{
    public GameObject gameObject;
    public int count;
}
