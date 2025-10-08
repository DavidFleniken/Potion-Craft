using UnityEngine;

public class ResetCauldron_POTIONCRAFT : MonoBehaviour
{
    [SerializeField] ColorMixing_POTIONCRAFT colorMixer_editor;
    public static ColorMixing_POTIONCRAFT colorMixer;
    static bool canInteract = false;

    void Start()
    {
        colorMixer = colorMixer_editor;
    }
    public static void ResetPotion()
    {
        colorMixer.resetColors();
    }
    public static bool CanInteract()
    {
        return canInteract;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
