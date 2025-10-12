using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class SubmitPotion_POTIONCRAFT : MonoBehaviour
{
    [SerializeField] ColorMixing_POTIONCRAFT cauldron_editor;
    [SerializeField] Costumer_POTIONCRAFT costumer_editor;
    [SerializeField] LeverAnimation_POTIONCRAFT animation_editor;

    static SubmitPotion_POTIONCRAFT singleton = null;

    static ColorMixing_POTIONCRAFT cauldron;
    static Costumer_POTIONCRAFT costumer;
    static LeverAnimation_POTIONCRAFT animation;

    // how many coins per percentage. EX) if 20, for every 20% accurate, get a coin, so 100% = 5 coins, etc
    [SerializeField] static float conversion = 20f;
    static bool canInteract = false;
    public static int totalCoins = 0;
    public static int coinsToWin = 20;

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
        animation = animation_editor;
    }

    public static bool CanInteract()
    {
        return canInteract;
    }

    public static void submitPotion()
    {
        if (!animation.Finished())
        {
            return;
        }
        animation.PressLever();
        Color cauldronCol = cauldron.getColor();
        Color goalColor = costumer.goalColor;

        float distance = (new Vector3(goalColor.r, goalColor.g, goalColor.b)).magnitude;

        // absolute value probably doesn't matter here but im too lazy to double check
        Vector3 accuracyVec =
            new Vector3(Mathf.Abs(goalColor.r - cauldronCol.r), Mathf.Abs(goalColor.g - cauldronCol.g),
                Mathf.Abs(goalColor.b - cauldronCol.b));

        float accuracy = accuracyVec.magnitude;

        accuracy = 100 - (Mathf.Clamp01((accuracy / distance)) * 100);

        int addedCoins = (int)(accuracy / conversion);

        //Debug.Log("Vector: " + accuracyVec + "\nAccuracy: " + accuracy + "\nCoins: " + addedCoins);

        if (accuracy == 100)
        {
            //perfect bonus
            totalCoins += (int)(addedCoins * 0.5);
        }
        else if (accuracy <= 35)
        {
            addedCoins = 0;
            Strikes_POTIONCRAFT.addStrike();
        }

        totalCoins += addedCoins;


        cauldron.resetColors();
        costumer.generateColor();

        if (totalCoins >= coinsToWin)
        {
            MinigameManager.SetStateToSuccess();
        }
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
