using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using UnityEngine;
using UnityEngine.Rendering;
using Font = iTextSharp.text.Font;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scaleModifier = -1.5F;

    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private Camera _scanCamera;
    [SerializeField] private GameObject _grid;
    [SerializeField] private NumbersSource _numbersSource;

    private Texture2D _destinationTexture;
    private Texture2D _cutTexture;

    private void Awake()
    {
        _destinationTexture = new(4096, 2048, TextureFormat.RGBA32, false);
        //_cut
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
        Rect rect = new(new(0F, 0F), new(4096F, 2048F));
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
        Document document = new(PageSize.A4, 31.7F, 31.7F, 31.7F, 31.7F);
        string fontPath = Path.Combine(Application.streamingAssetsPath, "arimo-font/Arimo-mO92.ttf");
        BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, true);
        Font font = new(baseFont, 12F);
        try
        {
            PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
            document.Open();
            Image image = Image.GetInstance(pngPath);
            //image.SetAbsolutePosition(0, 0);
            float scalePercent = document.PageSize.Width / image.Width * 100F + _scaleModifier;
            image.ScalePercent(scalePercent);
            image.Alignment = Element.ALIGN_CENTER;
            document.Add(image);
            document.Add(new Paragraph("Jebać disa", font));
            document.Add(new Paragraph("Jebać disa", font));
            document.Add(new Paragraph("Jebać disa", font));
            //document.Add(IElement);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "/n" + e.StackTrace);
        }
        finally
        {
            document.Close();
        }
    }
}