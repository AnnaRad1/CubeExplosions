using System;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private LayerMask _destructibleLayer;
    [SerializeField] private InputDetector _input;

    private RaycastHit _hit;

    public event Action<Cube> ObjectChosen;

    private void OnEnable()
    {
        _input.InputReceived += DetectCube;
    }

    private void OnDisable()
    {
        _input.InputReceived -= DetectCube;
    }

    private void Start()
    {
        _hit = new RaycastHit();
    }

    private void DetectCube(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _destructibleLayer))
            if (_hit.transform.TryGetComponent(out Cube cube))
                ObjectChosen?.Invoke(cube);
    }
}
