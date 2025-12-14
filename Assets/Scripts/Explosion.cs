using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private PositionCalculator _positionCalculator;
    [SerializeField] private LayerMask _destructibleLayer;

    public void DoExplosionEffectForNewCubes(List<Cube> explodedObjects, Vector3 explosionCenter)
    {
        foreach (Cube cube in explodedObjects)
            cube.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius);
    }

    public void ExplodeCubesAround(Cube explodingCube, Vector3 explosionCenter)
    {
        float averageScale = (explodingCube.transform.localScale.x + explodingCube.transform.localScale.y + explodingCube.transform.localScale.z) / 3f;
        float onetimeExplosionForce = GetNumberDependingOn(_explosionForce, averageScale);
        float onetimeExplosionRadius = GetNumberDependingOn(_explosionRadius, averageScale);
        List<Cube> explodedObjects = GetExplosiveCubesByRadius(explodingCube, onetimeExplosionRadius);

        foreach (Cube cube in explodedObjects)
            cube.AddExplosionForce(onetimeExplosionForce, explosionCenter, onetimeExplosionRadius);
    }

    private List<Cube> GetExplosiveCubesByRadius(Cube explodingCube, float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(explodingCube.transform.position, explosionRadius, _destructibleLayer);
        List<Cube> explosionCubes = new List<Cube>();

        foreach (Collider hit in hits)
            if (hit.TryGetComponent(out Cube cube))
                explosionCubes.Add(cube);

        return explosionCubes;
    }

    private float GetNumberDependingOn(float baseNumber, float controlNumber)
    {
        float inverseCoefficientBase = 1f;
        float inverseFactor = inverseCoefficientBase / controlNumber;
        return inverseFactor * baseNumber;
    }
}
