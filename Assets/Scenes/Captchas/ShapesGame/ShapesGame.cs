using UnityEngine;
using UnityEngine.UI;

public class ShapesGame : MonoBehaviour
{
    [Header("Children")]
    [SerializeField] private ShapesGameShape[] shapes;
    [SerializeField] private ShapesDropZone[] holes;
    [SerializeField] private ShapesDropZone squareHole;
}
