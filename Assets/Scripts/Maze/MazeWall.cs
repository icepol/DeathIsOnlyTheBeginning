using UnityEngine;
using Random = UnityEngine.Random;

public class MazeWall : MonoBehaviour
{
    [SerializeField] private GameObject[] wallSegments;
    
    [SerializeField] private int minOpenWidth = 2;
    [SerializeField] private int maxOpenWidth = 6;

    public void Open()
    {
        var openWidth = Random.Range(minOpenWidth, maxOpenWidth);
        var openStartFrom = (int)(wallSegments.Length * 0.5f - openWidth * 0.5f);
        
        for (var i = openStartFrom; i < openStartFrom + openWidth; i++)
        {
            Destroy(wallSegments[i]);
        }
    }
}
