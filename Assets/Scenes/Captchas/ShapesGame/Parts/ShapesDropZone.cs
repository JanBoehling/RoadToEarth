using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapesDropZone : MonoBehaviour, IDropHandler
{
    public event Action ShapeDropped;

    private RectTransform rectTrans;
    
    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
    }
    
    [SerializeField] private ShapesGameShape.ShapeType acceptedType;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log($"{eventData.pointerDrag.name} was dropped onto {acceptedType}!");
            if (!eventData.pointerDrag.TryGetComponent<ShapesGameShape>(out var shape)) return;
            if (!AcceptsShape(shape.shapeType)) return;
            
            Debug.Log("Accepted");
            if (!shape.TryGetComponent<DraggableImage>(out var draggable)) return;
            draggable.SnapToDropZone(rectTrans);
            eventData.pointerDrag.GetComponent<RectTransform>().position = transform.position;
        }
    }

    private bool AcceptsShape(ShapesGameShape.ShapeType shape)
    {
        if (acceptedType == ShapesGameShape.ShapeType.Square) return true; // Passt schon ins square hole lol
        return shape == acceptedType;
    }
}
