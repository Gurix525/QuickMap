using System;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class Scanner : MonoBehaviour
{
    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private Camera _scanCamera;
    [SerializeField] private GameObject _grid;

    private Texture2D _destinationTexture;

    private void Awake()
    {
        _destinationTexture = new(4096, 4096, TextureFormat.RGBA32, false);
    }

    private void OnScan()
    {
        _grid.SetActive(false);
        _scanCamera.gameObject.SetActive(true);
        RenderPipelineManager.endCameraRendering += PrintToPDF;
    }

    private void PrintToPDF(ScriptableRenderContext context, Camera camera)
    {
        if (camera != _scanCamera)
            return;
        string pngPath = Path.Combine(Application.persistentDataPath, "Render.png");
        string pdfPath = Path.Combine(Application.persistentDataPath, "Map.pdf");
        RenderPNG(pngPath);
        CreatePDF(pngPath, pdfPath);
        RenderPipelineManager.endCameraRendering -= PrintToPDF;
        _scanCamera.gameObject.SetActive(false);
        _grid.SetActive(true);
    }

    private void RenderPNG(string pngPath)
    {
        Rect rect = new(new(0F, 0F), new(4096F, 4096F));
        _destinationTexture.ReadPixels(rect, 0, 0);
        Color[] colors = _destinationTexture.GetPixels();
        for (int i = 0; i < colors.Length; i++)
        {
            Color color = colors[i];
            if (color.a == 0)
                continue;
            color = color.r > 0.9F ? Color.black : Color.white;
            colors[i] = color;
        }
        _destinationTexture.SetPixels(colors);
        _destinationTexture.Apply();
        File.WriteAllBytes(pngPath, _destinationTexture.EncodeToPNG());
    }

    private void CreatePDF(string pngPath, string pdfPath)
    {
    }
}