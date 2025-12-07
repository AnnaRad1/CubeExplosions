using System;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private RaycastHit _hit;

    public event Action<Cube> ObjectChosen;

    private void Start()
    {
        _hit = new RaycastHit();
    }

    private void Update()
    {
        bool isInputPressed = Input.GetMouseButtonDown(0);

        if (isInputPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, LayerMask.GetMask("Explosive")))
                ObjectChosen?.Invoke(_hit.transform.GetComponent<Cube>());
        }
    }
}
