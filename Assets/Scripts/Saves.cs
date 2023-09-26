using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Saves : MonoBehaviour
{
    [SerializeField] private GameObject _lines;
    [SerializeField] private GameObject _numbers;
    [SerializeField] private GameObject _labels;

    private string _savesPath;

    private void Awake()
    {
        _savesPath = Path.Combine(Application.persistentDataPath, "Saves");
    }

    public void CreateSave()
    {
        string filePath = Path.Combine(_savesPath, "save.txt");
        Save save = new(_lines, _numbers, _labels);
        string json = JsonUtility.ToJson(save);
        try
        {
            Directory.CreateDirectory(_savesPath);
            File.WriteAllText(filePath, json);
        }
        catch
        {
        }
    }

    public void Load()
    {
        if (!Directory.Exists(_savesPath))
            return;
        ClearScene();
        string filePath = Path.Combine(_savesPath, "save.txt");
        string json = "";
        Save save = null;
        try
        {
            json = File.ReadAllText(filePath);
            save = JsonUtility.FromJson<Save>(json);
        }
        catch
        {
        }
        LoadLines(save.LineInfos);
        LoadNumbers(save.NumberInfos);
        LoadLabels(save.LabelInfos);
    }

    private void LoadLines(LineInfo[] lineInfos)
    {
        GameObject linePrefab = Resources.Load<GameObject>("Prefabs/Line");
        foreach (LineInfo info in lineInfos)
        {
            GameObject line = Instantiate(linePrefab, _lines.transform);
            line.GetComponent<LineRenderer>()
                .SetPositions(new Vector3[]{ info.Start, info.End});
            line.GetComponent<LineCollider>().BakeMesh();
        }
    }

    private void LoadNumbers(NumberInfo[] numberInfos)
    {
        GameObject numberPrefab = Resources.Load<GameObject>("Prefabs/Number");
        foreach (NumberInfo info in numberInfos)
        {
            GameObject number = Instantiate(numberPrefab, _numbers.transform);
            Number numberComponent = number.GetComponent<Number>();
            numberComponent.Text = info.Text;
            numberComponent.Initialize();
            number.transform.position = info.Position;
        }
    }

    private void LoadLabels(LabelInfo[] labelInfos)
    {
        GameObject labelPrefab = Resources.Load<GameObject>("Prefabs/Label");
        foreach (LabelInfo info in labelInfos)
        {
            GameObject label = Instantiate(labelPrefab, _labels.transform);
            Label labelComponent = label.GetComponent<Label>();
            labelComponent.Text = info.Text;
            labelComponent.Initialize();
            label.transform.position = info.Position;
        }
    }

    private void ClearScene()
    {
        GameObject temp = new();
        for (int i = _lines.transform.childCount - 1; i >= 0; i--)
            _lines.transform.GetChild(i).parent = temp.transform;
        for (int i = _numbers.transform.childCount - 1; i >= 0; i--)
            _numbers.transform.GetChild(i).parent = temp.transform;
        for (int i = _labels.transform.childCount - 1; i >= 0; i--)
            _labels.transform.GetChild(i).parent = temp.transform;
        _numbers.GetComponent<NumbersSource>().Numbers.Clear();
        Destroy(temp);
    }

    [Serializable]
    private class Save
    {
        public LineInfo[] LineInfos;
        public NumberInfo[] NumberInfos;
        public LabelInfo[] LabelInfos;

        public Save()
        { }

        public Save(GameObject lines, GameObject numbers, GameObject labels)
        {
            List<LineInfo> lineInfos = new();
            foreach (Transform line in lines.transform)
            {
                lineInfos.Add(new(line.GetComponent<LineRenderer>()));
            }
            LineInfos = lineInfos.ToArray();

            List<NumberInfo> numberInfos = new();
            foreach (Transform number in numbers.transform)
            {
                numberInfos.Add(new(number.GetComponent<Number>()));
            }
            NumberInfos = numberInfos.ToArray();

            List<LabelInfo> labelInfos = new();
            foreach (Transform label in labels.transform)
            {
                labelInfos.Add(new(label.GetComponentInChildren<TextMeshPro>()));
            }
            LabelInfos = labelInfos.ToArray();
        }
    }

    [Serializable]
    private class LineInfo
    {
        public Vector2 Start;
        public Vector2 End;

        public LineInfo()
        { }

        public LineInfo(LineRenderer line)
        {
            Vector3[] nodes = new Vector3[2];
            line.GetPositions(nodes);
            Start = nodes[0];
            End = nodes[1];
        }
    }

    [Serializable]
    private class NumberInfo
    {
        public Vector2 Position;
        public string Text;

        public NumberInfo()
        { }

        public NumberInfo(Number number)
        {
            Position = number.transform.position;
            Text = number.Text;
        }
    }

    [Serializable]
    private class LabelInfo
    {
        public Vector2 Position;
        public string Text;

        public LabelInfo()
        { }

        public LabelInfo(TextMeshPro text)
        {
            Position = text.transform.position;
            Text = text.text;
        }
    }
}