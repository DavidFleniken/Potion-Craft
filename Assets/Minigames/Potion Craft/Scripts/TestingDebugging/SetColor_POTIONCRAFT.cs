using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class SetColor_POTIONCRAFT : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] bool reset_material = true;

    Material material;

    private void OnValidate()
    {
        if (reset_material)
        {
            GetComponent<Renderer>().sharedMaterial = new Material(material);
            material = null;
            color = Color.white;
            reset_material = false;
        }
        if (material == null)
        {
            material = GetComponent<Renderer>().sharedMaterial;
            reset_material = false;
        }

        material.color = color;
    }
}
