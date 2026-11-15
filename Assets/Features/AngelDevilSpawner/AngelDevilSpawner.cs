using System.Collections;
using UnityEngine;

public class AngelDevilSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform angelTemplate;
    [SerializeField] private RectTransform devilTemplate;

    [SerializeField] private RectTransform spawnLimitLeft;
    [SerializeField] private RectTransform spawnLimitRight;
    [SerializeField] private int spawnCount = 10;

    private Coroutine _spawnRoutine;
    
    public void SpawnAngels() => SpawnMany(angelTemplate);
    public void SpawnDevils() => SpawnMany(devilTemplate);

    public void SpawnMany(RectTransform template)
    {
        if (_spawnRoutine != null) StopCoroutine(_spawnRoutine);
        _spawnRoutine = StartCoroutine(SpawnRoutine(template));
        
    }

    private IEnumerator SpawnRoutine(RectTransform template)
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnOne(template);
            yield return new WaitForSeconds(Random.Range(0f, 0.4f));
        }

        _spawnRoutine = null;
    }

    private void SpawnOne(RectTransform template)
    {
        var instance = Instantiate(template, transform);
        instance.gameObject.SetActive(true);
        
        var lerpPos = Random.Range(0f, 1f);
        instance.position = Vector2.Lerp(spawnLimitLeft.position, spawnLimitRight.position, lerpPos);
    }
    
    
}
