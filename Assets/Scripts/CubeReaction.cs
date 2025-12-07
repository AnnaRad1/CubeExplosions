using UnityEngine;

public class CubeReaction : MonoBehaviour
{
    [SerializeField] private Raycast _raycast;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private Transform[] _children;

    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _children = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _children[i] = transform.GetChild(i);
    }

    private void OnEnable()
    {
        _raycast.ObjectChosen += React;
    }

    private void OnDisable()
    {
        _raycast.ObjectChosen -= React;
    }

    private void React(Cube chosenCube)
    {
        if (IsSpawned(chosenCube))
        {
            Vector3 scale = chosenCube.transform.localScale;
            Vector3 position = chosenCube.transform.position;
            int currentGeneration = chosenCube.Generation;
            _spawner.Destroy(chosenCube);
            _explosion.DoExplosionEffect(_spawner.MakeNewObjects(scale, position, currentGeneration), position);
        }
        else
        {
            _spawner.Destroy(chosenCube);
        }
    }

    private bool IsSpawned(Cube chosenCube)
    {
        int maxPercent = 100;
        int spawnChance = maxPercent / chosenCube.Generation;

        if (Random.Range(1, maxPercent + 1) <= spawnChance)
            return true;
        else
            return false;
    }
}
