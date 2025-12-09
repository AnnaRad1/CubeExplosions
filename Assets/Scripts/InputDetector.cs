using System;
using UnityEngine;

public class InputDetector : MonoBehaviour
{
    private const int MainInteractionButton = 0;

    public event Action<Vector2> InputReceived;

    private void Update()
    {
        if (TryGetInput(out Vector2 position))
            InputReceived?.Invoke(position);
    }

    private bool TryGetInput(out Vector2 inputPosition)
    {
        if (Input.GetMouseButtonDown(MainInteractionButton))
        {
            inputPosition = Input.mousePosition;
            return true;
        }

        inputPosition = Vector2.zero;
        return false;
    }
}
