using System;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    private RaycastHit _hit;
    private float _explosionChancePercent = 100f;

    public event Action<GameObject> Exploded;

    private void Start()
    {
        _hit = new RaycastHit();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, LayerMask.GetMask("Explosive")))
                if (IsExploded())
                    Exploded?.Invoke(_hit.transform.gameObject);
                else
                    Destroy(_hit.transform.gameObject);
        }
    }

    private bool IsExploded()
    {
        float doubleDecreasing = 1.1f;
        int maxPercent = 100;
        bool isExploded = UnityEngine.Random.Range(1, maxPercent + 1) <= _explosionChancePercent;

        if (isExploded)
            _explosionChancePercent /= doubleDecreasing;

        return isExploded;
    }
}

