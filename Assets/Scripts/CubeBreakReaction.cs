using UnityEngine;
using UnityEngine.UIElements;

public class CubeBreakReaction : MonoBehaviour
{
    [SerializeField] private Raycaster _raycast;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;

    private void OnEnable()
    {
        _raycast.CubeDetected += React;
    }

    private void OnDisable()
    {
        _raycast.CubeDetected -= React;
    }

    private void React(Cube chosenCube)
    {
        Vector3 position = chosenCube.transform.position;

        if (IsSpawned(chosenCube))
        {
            Vector3 scale = chosenCube.transform.localScale;
            int currentSpawnChance = chosenCube.SpawnChance;
            _explosion.DoExplosionEffectForNewCubes(_spawner.MakeNewObjects(scale, position, currentSpawnChance), position);
        }
        else
        {
            _explosion.ExplodeCubesAround(chosenCube, position);
        }

        _spawner.Destroy(chosenCube);
    }

    private bool IsSpawned(Cube chosenCube)
    {
        int maxPercent = 100;
        return Random.Range(1, maxPercent + 1) <= chosenCube.SpawnChance;
    }
}
