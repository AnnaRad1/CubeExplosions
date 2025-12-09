using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private PositionCalculator _positionCalculator;

    public void DoExplosionEffect(List<Cube> explodedObjects, Vector3 explosionPosition)
    {
        foreach (Cube cube in explodedObjects)
            cube.AddExplosionForce(_explosionForce, _positionCalculator.GetRandomPosition(explosionPosition), _explosionRadius);
    }
}
