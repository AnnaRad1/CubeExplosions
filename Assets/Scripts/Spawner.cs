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

        for (int i = 1; i <= objectsNumber; i++)
        {
            Cube newCube = Instantiate(_prefab);
            newCube.gameObject.transform.SetParent(_parent);
            newCube.transform.position = _positionCalculator.GetRandomPosition(position);
            newCube.transform.localScale = scale / splitCount;
            newCube.GetComponent<Renderer>().material.color = _colorRandomizer.GetRandomColor();
            newCube.SetSpawnChance(spawnChance / splitCount);
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
