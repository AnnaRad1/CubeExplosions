using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private PositionCalculator _positionCalculator;
    [SerializeField] private ColorRandomizer _colorRandomizer;

    public List<Cube> MakeNewObjects(Vector3 scale, Vector3 position, int spawnChance)
    {
        int splitCount = 2;
        int objectsNumber = GetRandomObjectsNumberToSpawn();
        List<Cube> newObjects = new();
        Vector3 newScale = scale / splitCount;
        int newSpawnChance = spawnChance / splitCount;
        
        for (int i = 1; i <= objectsNumber; i++)
        {
            Cube newCube = Instantiate(_prefab, _parent);
            Vector3 newPosition = _positionCalculator.GetRandomPosition(position);
            Color newColor = _colorRandomizer.GetRandomColor();
            newCube.Initialize(newPosition, newScale, newColor, newSpawnChance);
            newObjects.Add(newCube);
        }

        return newObjects;
    }

    public void Destroy(Cube destroyingObject)
    {
        Destroy(destroyingObject.gameObject);
    }

    private int GetRandomObjectsNumberToSpawn()
    {
        int minObjectNumber = 2;
        int maxObjectNumber = 6;
        return Random.Range(minObjectNumber, maxObjectNumber + 1);
    }
}
