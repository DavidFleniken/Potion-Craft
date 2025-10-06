using UnityEngine;

public class SubmitPotion_POTIONCRAFT : MonoBehaviour
{
    [SerializeField] static ColorMixing_POTIONCRAFT cauldron;
    [SerializeField] static Costumer_POTIONCRAFT costumer;

    [SerializeField] static float threshold = 0.9f;


    public static void submitPotion()
    {
        Color cauldronCol = cauldron.getColor();
        Color goalColor = costumer.goalColor;


    }
}
