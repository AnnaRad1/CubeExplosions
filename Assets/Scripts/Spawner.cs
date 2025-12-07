using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _parent;

    public List<Cube> MakeNewObjects(Vector3 scale, Vector3 position, int generation)
    {
        int splitCount = 2;
        int newGeneration = ++generation;
        int minObjectNumber = 2;
        int maxObjectNumber = 6;
        int objectsNumber = Random.Range(minObjectNumber, maxObjectNumber + 1);
        List<Cube> newObjects = new();

        for (int i = 1; i <= objectsNumber; i++)
        {
            Cube newCube = Instantiate(_prefab);
            newCube.gameObject.transform.SetParent(_parent);
            newCube.transform.localScale = scale / splitCount;
            newCube.transform.position = Cube.GetRandomPositionAround(position);
            newCube.GetComponent<Renderer>().material = _prefab.GetRandomColorMaterial();
            newCube.gameObject.layer = LayerMask.NameToLayer("Explosive");
            newCube.SetGeneration(newGeneration);
            newObjects.Add(newCube);
        }

        return newObjects;
    }

    public void Destroy(Cube destroyingObject)
    {
        Destroy(destroyingObject.gameObject);
    }
}
