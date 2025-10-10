using UnityEngine;

public class ResetCauldron_POTIONCRAFT : MonoBehaviour
{
    [SerializeField] ColorMixing_POTIONCRAFT colorMixer_editor;
    public static ColorMixing_POTIONCRAFT colorMixer;
    [SerializeField] LeverAnimation_POTIONCRAFT animation_editor;
    static bool canInteract = false;
    static LeverAnimation_POTIONCRAFT animation;

    void Start()
    {
        colorMixer = colorMixer_editor;
        animation = animation_editor;
    }
    public static void ResetPotion()
    {
        if (animation.Finished())
        {
            colorMixer.resetColors();
            animation.PressLever();
        }
       
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
