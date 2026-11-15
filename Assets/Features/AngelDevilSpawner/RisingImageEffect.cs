using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RisingImageEffect : MonoBehaviour
{
    [SerializeField] private float despawnTimer = 5f;
    [SerializeField] private float riseAnimationSpeed = 500f;
    [SerializeField] private float swivelMagnitude = 2f;
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Rise animation
        _rectTransform.anchoredPosition += riseAnimationSpeed * Time.deltaTime * Vector2.up;

        // Swivel effect
        _rectTransform.anchoredPosition += swivelMagnitude * Random.Range(-1f, 1f) * Vector2.right;
        
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0f) Destroy(gameObject);
    }
}
