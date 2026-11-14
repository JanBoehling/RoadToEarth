using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapesDropZone : MonoBehaviour, IDropHandler
{
    public event Action<ShapesDropZone> ShapeDropped;

    private RectTransform rectTrans;
    
    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
    }
    
    [SerializeField] private ShapesGameShape.ShapeType acceptedType;
    public ShapesGameShape.ShapeType AcceptedType => acceptedType;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        
        Debug.Log($"{eventData.pointerDrag.name} was dropped onto {acceptedType}!");
        if (!eventData.pointerDrag.TryGetComponent<ShapesGameShape>(out var shape)) return;
        if (!AcceptsShape(shape.shapeType)) return;
            
        Debug.Log("Accepted");
        if (!shape.TryGetComponent<DraggableImage>(out var draggable)) return;
        draggable.OnDroppedOnZone(rectTrans);
        eventData.pointerDrag.GetComponent<RectTransform>().position = transform.position;
        
        ShapeDropped?.Invoke(this);
    }

    private bool AcceptsShape(ShapesGameShape.ShapeType shape)
    {
        if (acceptedType == ShapesGameShape.ShapeType.Square) return true; // Passt schon ins square hole lol
        return shape == acceptedType;
    }
}
