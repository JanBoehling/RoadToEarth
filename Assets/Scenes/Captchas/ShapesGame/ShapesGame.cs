using System;
using UnityEngine;
using UnityEngine.UI;

public class ShapesGame : MonoBehaviour
{
    [Header("Children")]
    [SerializeField] private DraggableImage[] shapes;
    [SerializeField] private ShapesDropZone[] holes;
    [SerializeField] private ShapesDropZone squareHole;

    private readonly ShapesGameResult _result = new();
    
    private void Awake()
    {
        foreach (var hole in holes)
        {
            hole.ShapeDropped += OnShapeDropped;
        }
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
