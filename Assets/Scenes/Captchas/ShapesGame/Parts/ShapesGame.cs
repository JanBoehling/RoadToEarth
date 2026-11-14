using System;
using UnityEngine;
using UnityEngine.UI;

public class ShapesGame : MonoBehaviour
{
    public event Action ResetPressed;
    public event Action ConfirmPressed;
    
    [Header("Children")]
    [SerializeField] private DraggableImage[] shapes;
    [SerializeField] private ShapesDropZone[] holes;
    [SerializeField] private ShapesDropZone squareHole;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button confirmButton;
    
    private readonly ShapesGameResult _result = new();
    
    public bool IsDone()
    {
        foreach (var shape in shapes)
        {
            if (!shape.IsDropped) return false;
        }
        return true;
    }

    public bool IsCorrect()
    {
        return _result.Correct >= 4;
    }
    
    private void Awake()
    {
        foreach (var hole in holes)
        {
            hole.ShapeDropped += OnShapeDropped;
        }
        
        resetButton.onClick.AddListener(() => ResetPressed?.Invoke());
        confirmButton.onClick.AddListener(() => ConfirmPressed?.Invoke());
    }

    private void OnShapeDropped(ShapesDropZone zone)
    {
        Debug.Log($"Detected drop: {zone}");
        AddScore(zone);
        CheckDone();
    }

    private void AddScore(ShapesDropZone zone)
    {
        if (zone.AcceptedType == ShapesGameShape.ShapeType.Square)
        {
            _result.Correct += 1;
            return;
        }
        
        _result.Incorrect += 1;
    }

    private void CheckDone()
    {
        foreach (var shape in shapes)
        {
            if (!shape.IsDropped) return;
        }
        
        // All shapes dropped
        Debug.Log($"DONE {_result.Correct}, {_result.Incorrect}");
    }

    private class ShapesGameResult
    {
        public int Correct;
        public int Incorrect;
    }
}
