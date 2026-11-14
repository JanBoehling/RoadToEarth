using UnityEngine;

public class ShapesGameShape : MonoBehaviour
{
    public enum  ShapeType
    {
        Circle,
        Square,
        Star,
        Triangle
    }
    
    [SerializeField] public ShapeType shapeType;
}
