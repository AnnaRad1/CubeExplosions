using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public int SpawnChance { get; private set; } = 100;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(Vector3 position, Vector3 scale, Color color, int spawnChance)
    {
        transform.position = position;
        transform.localScale = scale;
        _renderer.material.color = color;
        SpawnChance = spawnChance;
    }

    public void AddExplosionForce(float explosionForce, Vector3 explosionCenter, float explosionRadius)
    {
        _rigidbody.AddExplosionForce(explosionForce, explosionCenter, explosionRadius);
    }
}
