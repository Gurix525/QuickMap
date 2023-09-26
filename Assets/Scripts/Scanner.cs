using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Font = iTextSharp.text.Font;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scaleModifier = -1.5F;

    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private Camera _scanCamera;
    [SerializeField] private GameObject _grid;
    [SerializeField] private GameObject _pointer;
    [SerializeField] private NumbersSource _numbersSource;
    [SerializeField] private GameObject _lines;
    [SerializeField] private GameObject _numbers;
    [SerializeField] private GameObject _labels;

    private Texture2D _destinationTexture;
    private Rectangle _rectangle;

    private void Awake()
    {
        _destinationTexture = new(4096, 2048, TextureFormat.RGBA32, false);
    }

    private void OnScan()
    {
        _rectangle = GetFullRenderRectangle();
        if (_rectangle.Diagonal < 0.5F)
            return;
        _scanCamera.orthographicSize = _rectangle.Height / 2F + 1F;
        _scanCamera.transform.position = new(_rectangle.Center.x, _rectangle.Center.y, -10F);
        _grid.SetActive(false);
        _pointer.SetActive(false);
        _scanCamera.gameObject.SetActive(true);
        RenderPipelineManager.endCameraRendering += PrintToPDF;
    }

    private void PrintToPDF(ScriptableRenderContext context, Camera camera)
    {
        if (camera != _scanCamera)
            return;
        string pngPath = Path.Combine(Application.persistentDataPath, "Render.png");
        string pdfPath = Path.Combine(Application.persistentDataPath, "Map.pdf");
        try
        {
            RenderPNG(pngPath);
            CreatePDF(pngPath, pdfPath);
        }
        catch { }
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
        try
        {
            File.WriteAllBytes(pngPath, _destinationTexture.EncodeToPNG());
        }
        catch { }
    }

    private void CreatePDF(string pngPath, string pdfPath)
    {
        Document document = new(PageSize.A4, 31.7F, 31.7F, 31.7F, 31.7F);
        try
        {
            string regularFontPath = Path.Combine(Application.streamingAssetsPath, "arimo-font/Arimo-mO92.ttf");
            string boldFontPath = Path.Combine(Application.streamingAssetsPath, "arimo-font/ArimoBold-dVDx.ttf");
            BaseFont regularBase = BaseFont.CreateFont(regularFontPath, BaseFont.IDENTITY_H, true);
            BaseFont boldBase = BaseFont.CreateFont(boldFontPath, BaseFont.IDENTITY_H, true);
            Font regularFont = new(regularBase, 11F);
            Font boldFont = new(boldBase, 11F);

            PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
            document.Open();
            Image image = Image.GetInstance(pngPath);
            //image.SetAbsolutePosition(0, 0);
            float scalePercent = document.PageSize.Width / image.Width * 100F + _scaleModifier;
            image.ScalePercent(scalePercent);
            image.Alignment = Element.ALIGN_CENTER;
            document.Add(image);
            var legend = GetLegend(regularFont, boldFont);
            foreach (var item in legend)
            {
                document.Add(item);
            }
            File.Delete(pngPath);
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

    private Paragraph[] GetLegend(Font regularFont, Font boldFont)
    {
        Number[] numbers = _numbersSource.Numbers.ToArray();
        Paragraph[] legend = new Paragraph[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            Chunk start = new($"        {numbers[i].ID}. ", boldFont);
            Chunk end = new(numbers[i].Text, regularFont);
            legend[i] = new()
            {
                start,
                end
            };
            legend[i].Alignment = Element.ALIGN_JUSTIFIED;
        }
        return legend;
    }

    private Rectangle GetFullRenderRectangle()
    {
        Vector2 min = Vector2.positiveInfinity;
        Vector2 max = Vector2.negativeInfinity;
        foreach (Transform line in _lines.transform)
        {
            Bounds bounds = line.GetComponent<MeshCollider>().bounds;
            min = GetMin(min, bounds.min);
            max = GetMax(max, bounds.max);
        }
        foreach (Transform number in _numbers.transform)
        {
            Vector2 position = number.transform.position;
            min = GetMin(min, position);
            max = GetMax(max, position);
        }
        foreach (Transform label in _labels.transform)
        {
            Bounds bounds = label.GetComponentInChildren<TextMeshPro>().bounds;
            min = GetMin(min, bounds.min + label.position);
            max = GetMax(max, bounds.max + label.position);
        }
        return new(min, max);
    }

    private Vector2 GetMin(Vector2 l, Vector2 r)
    {
        return new(Mathf.Min(l.x, r.x), Mathf.Min(l.y, r.y));
    }

    private Vector2 GetMax(Vector2 l, Vector2 r)
    {
        return new(Mathf.Max(l.x, r.x), Mathf.Max(l.y, r.y));
    }

    private struct Rectangle
    {
        public Vector2 Min { get; }
        public Vector2 Max { get; }

        public float Width => Mathf.Abs(Max.x - Min.x);
        public float Height => Mathf.Abs(Max.y - Min.y);
        public float Diagonal => Vector2.Distance(Min, Max);
        public Vector2 Center => new((Max.x + Min.x) / 2F, (Max.y + Min.y) / 2F);

        public Rectangle(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }
    }
}