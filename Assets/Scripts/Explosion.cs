using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionSpreadDistance = 2f;

    public void DoExplosionEffect(List<Cube> explodedObjects, Vector3 explosionPosition)
    {
        foreach (Cube cube in explodedObjects)
            cube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, GetRandomExplosionPosition(explosionPosition), _explosionRadius);
    }

    private Vector3 GetRandomExplosionPosition(Vector3 position)
    {
        return Cube.GetRandomPositionAround(position, _explosionSpreadDistance);
    }
}
