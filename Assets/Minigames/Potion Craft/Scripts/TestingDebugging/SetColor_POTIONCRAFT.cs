using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class SetColor_POTIONCRAFT : MonoBehaviour
{
    [SerializeField] Color color;

    Material material;

    private void OnValidate()
    {
        if (material == null)
        {
            material = GetComponent<Renderer>().sharedMaterial;
        }

        material.color = color;
    }
}
