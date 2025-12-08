using UnityEngine;

public class PositionCalculator : MonoBehaviour
{
    [SerializeField] private float _spreadDistance = 2f;

    public Vector3 GetRandomPosition(Vector3 position)
    {
        return position + new Vector3
        (
            Random.Range(-_spreadDistance, _spreadDistance),
            Random.Range(0f, _spreadDistance),
            Random.Range(-_spreadDistance, _spreadDistance)
        );
    }
}
