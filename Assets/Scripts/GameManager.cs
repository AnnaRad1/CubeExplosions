using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickController _clickController;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionSpreadDistance = 2f;

    private List<GameObject> _gameObjects;
    private Transform _parent;

    private void Awake()
    {
        _parent = GameObject.Find("ExplosiveObjects").transform;
        _gameObjects = _parent.Cast<Transform>().Select(child => child.gameObject).ToList();
    }

    private void OnEnable()
    {
        _clickController.Exploded += MakeObjectExploded;
    }

    private void OnDisable()
    {
        _clickController.Exploded -= MakeObjectExploded;
    }

    private void MakeObjectExploded(GameObject explodedObject)
    {
        Vector3 scale = explodedObject.transform.localScale;
        Vector3 position = explodedObject.transform.position;
        GameObject objectToDestroy = _gameObjects.Find(gameObject => gameObject == explodedObject);
        int splitCount = 2;

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
            _gameObjects.Remove(objectToDestroy);
        }

        DoExplosionEffect(MakeNewObjects(scale / splitCount, position), position);
    }

    private List<GameObject> MakeNewObjects(Vector3 scale, Vector3 position)
    {
        int minObjectNumber = 2;
        int maxObjectNumber = 6;
        int objectsNumber = Random.Range(minObjectNumber, maxObjectNumber + 1);
        List<GameObject> newObjects = new();

        for (int i = 1; i <= objectsNumber; i++)
        {
            GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newCube.transform.localScale = scale;
            newCube.transform.position = GetRandomPositionAround(position);
            newCube.transform.SetParent(_parent);
            newCube.AddComponent<Rigidbody>();
            newCube.GetComponent<Renderer>().material = GetRandomColorMaterial();
            newCube.layer = LayerMask.NameToLayer("Explosive");
            _gameObjects.Add(newCube);
            newObjects.Add(newCube);
        }

        return newObjects;
    }

    private Material GetRandomColorMaterial()
    {
        Material newMaterial = new Material(Shader.Find("Standard"));
        newMaterial.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0.7f, 0.9f), Random.Range(0.8f, 1f));
        return newMaterial;
    }

    private void DoExplosionEffect(List<GameObject> explodedObjects, Vector3 explosionPosition)
    {
        foreach (GameObject gameObject in explodedObjects)
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
    }

    private Vector3 GetRandomPositionAround(Vector3 position)
    {
        return position + new Vector3
            (
                Random.Range(-_explosionSpreadDistance, _explosionSpreadDistance),
                Random.Range(0f, _explosionSpreadDistance),
                Random.Range(-_explosionSpreadDistance, _explosionSpreadDistance)
            );
    }
}
