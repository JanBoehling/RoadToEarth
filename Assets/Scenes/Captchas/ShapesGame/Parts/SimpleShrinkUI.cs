using System;
using UnityEngine;

public class SimpleShrinkUI : MonoBehaviour
{
    public float shrinkSpeed = 3f;
    private bool _shrinking = false;
    
    public void Shrink()
    {
        _shrinking = true;
    }

    private void Update()
    {
        if (!_shrinking) return;
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);
        if (transform.localScale == Vector3.zero)
        {
            gameObject.SetActive(false);
        }
    }
}