using UnityEngine;

public class Cube : MonoBehaviour
{
    public int Generation { get; private set; } = 1;

    public Material GetRandomColorMaterial()
    {
        Material newMaterial = new Material(Shader.Find("Standard"));
        newMaterial.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0.7f, 0.9f), Random.Range(0.8f, 1f));
        return newMaterial;
    }

    public void SetGeneration(int generation)
    {
        Generation = generation;
    }

    public static Vector3 GetRandomPositionAround(Vector3 position, float spreadDistance = 3f)
    {
        return position + new Vector3(
            Random.Range(-spreadDistance, spreadDistance),
            Random.Range(0f, spreadDistance),
            Random.Range(-spreadDistance, spreadDistance)
        );
    }
}
