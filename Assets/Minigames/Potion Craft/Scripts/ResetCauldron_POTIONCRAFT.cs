using UnityEngine;

public class ResetCauldron_POTIONCRAFT : MonoBehaviour
{
    [SerializeField] ColorMixing_POTIONCRAFT colorMixer_editor;
    public static ColorMixing_POTIONCRAFT colorMixer;
    [SerializeField] LeverAnimation_POTIONCRAFT animation_editor;
    static bool canInteract = false;
    static LeverAnimation_POTIONCRAFT animation;
    private static AudioSource audioSource;

    void Start()
    {
        colorMixer = colorMixer_editor;
        animation = animation_editor;
        audioSource = GetComponent<AudioSource>();
    }
    public static void ResetPotion()
    {
        if (animation.Finished())
        {
            colorMixer.resetColors();
            animation.PressLever();
            audioSource.Play();
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
