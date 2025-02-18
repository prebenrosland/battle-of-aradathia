using UnityEngine;
using System.IO;

public class PrefabToPNG : MonoBehaviour
{
    public Camera captureCamera;
    public string outputFileName = "PrefabImage.png";
    public int width = 1024;
    public int height = 1024;

    private void Start()
    {
        CapturePrefabToPNG();
    }

    private void CapturePrefabToPNG()
    {
        // Set the camera's clear flags and background color to transparent
        captureCamera.clearFlags = CameraClearFlags.SolidColor;
        captureCamera.backgroundColor = new Color(0, 0, 0, 0);

        // Create a render texture with 32-bit depth to support transparency
        RenderTexture rt = new RenderTexture(width, height, 32);
        captureCamera.targetTexture = rt;

        // Use the RGBA32 texture format to support transparency
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGBA32, false);
        captureCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        string filePath = Path.Combine(Application.dataPath, outputFileName);
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Prefab image with transparent background saved to: " + filePath);
    }
}
