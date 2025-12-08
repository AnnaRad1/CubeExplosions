using System;
using UnityEngine;

public class InputDetector : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;
    private const int OneFingerTouch = 0;

    public event Action<Vector2> InputReceived;

    private void Update()
    {
        if (TryGetInput(out Vector2 position))
            InputReceived?.Invoke(position);
    }

    private bool TryGetInput(out Vector2 inputPosition)
    {
        if (Input.GetMouseButtonDown(LeftMouseButton) || Input.GetMouseButtonDown(RightMouseButton))
        {
            inputPosition = Input.mousePosition;
            return true;
        }

        if (Input.touchCount > 0 && Input.GetTouch(OneFingerTouch).phase == TouchPhase.Began)
        {
            inputPosition = Input.GetTouch(OneFingerTouch).position;
            return true;
        }

        inputPosition = Vector2.zero;
        return false;
    }
}
