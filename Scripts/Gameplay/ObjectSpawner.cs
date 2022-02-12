using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [Tooltip("Префаб для спауна")]
    [SerializeField] private Transform _spawnPrefab;
    [Tooltip("Место для спауна префаба")]
    [SerializeField] private Transform _spawnPoint;

    private ColliderChecker[] _colliderChecker;

    private void Start()
    {
        SpawnPrefab();
        _colliderChecker = FindObjectsOfType<ColliderChecker>();
        foreach (var item in _colliderChecker)
        {
            item.OnSpawnFood += SpawnPrefab;
        }
    }
  
    private void OnDestroy()
    {
        foreach (var item in _colliderChecker)
        {
            item.OnSpawnFood -= SpawnPrefab;
        }
    }

    private void SpawnPrefab()
    {
        var randomEulerX = Random.Range(-180, 180);
        var randomEulerY = Random.Range(-180, 180);
        var randomEulerZ = Random.Range(-180, 180);

        _spawnPoint.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), _spawnPoint.transform.position.y, _spawnPoint.transform.position.z);
        
           Instantiate(_spawnPrefab, _spawnPoint.transform.position,
                      Quaternion.Euler(randomEulerX, randomEulerY, randomEulerZ));        
    }


}

