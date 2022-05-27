using System;
using System.IO;
using UnityEngine;

public class ScreenCapture : MonoBehaviour
{
    [SerializeField] private float activeArea = 0.5f;

    private float _activeAreaSize;

    private void Start()
    {
        _activeAreaSize = Screen.height * activeArea;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.y > _activeAreaSize 
            || Input.GetKeyDown(KeyCode.S))
            Capture();
    }

    void Capture()
    {
        String fileName = $"screenshot_{DateTime.UtcNow:yyyyMMdd_HH:mm:ss}_{Screen.width}x{Screen.height}.png"; 
        String filePath = Path.Combine(Application.persistentDataPath, fileName);
        
        UnityEngine.ScreenCapture.CaptureScreenshot(filePath);
        
        Debug.Log($"ScreenCapture: image saved to '{filePath}'");
    }
}
