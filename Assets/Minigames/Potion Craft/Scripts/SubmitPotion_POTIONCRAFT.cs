using UnityEngine;

public class SubmitPotion_POTIONCRAFT : MonoBehaviour
{
    [SerializeField] ColorMixing_POTIONCRAFT cauldron_editor;
    [SerializeField] Costumer_POTIONCRAFT costumer_editor;

    static SubmitPotion_POTIONCRAFT singleton = null;

    static ColorMixing_POTIONCRAFT cauldron;
    static Costumer_POTIONCRAFT costumer;

    // how many coins per percentage. EX) if 20, for every 20% accurate, get a coin, so 100% = 5 coins, etc
    [SerializeField] static float conversion = 20f;

    static int totalCoins = 0;
    static bool canInteract = false;

    private void Start()
    {
        if (singleton != null)
        {
            Debug.LogError("Only one potion submit script allowed");
            return;
        }

        singleton = this;
        cauldron = cauldron_editor;
        costumer = costumer_editor;
    }

    public static bool CanInteract()
    {
        return canInteract;
    }

    public static void submitPotion()
    {
        Color cauldronCol = cauldron.getColor();
        Color goalColor = costumer.goalColor;

        float distance = (new Vector3(goalColor.r, goalColor.g, goalColor.b)).magnitude;

        // absolute value probably doesn't matter here but im too lazy to double check
        Vector3 accuracyVec =
            new Vector3(Mathf.Abs(goalColor.r - cauldronCol.r), Mathf.Abs(goalColor.g - cauldronCol.g), Mathf.Abs(goalColor.b - cauldronCol.b));

        float accuracy = accuracyVec.magnitude;

        accuracy = 100 - (Mathf.Clamp01((accuracy / distance)) * 100);

        int addedCoins = (int)(accuracy / conversion);

        Debug.Log("Vector: " + accuracyVec + "\nAccuracy: " + accuracy + "\nCoins: " + addedCoins);
        totalCoins += addedCoins;

        if (accuracy == 100)
        {
            //perfect bonus
            totalCoins += (int)(addedCoins * 0.5);
        }

        cauldron.resetColors();
        costumer.generateColor();
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
