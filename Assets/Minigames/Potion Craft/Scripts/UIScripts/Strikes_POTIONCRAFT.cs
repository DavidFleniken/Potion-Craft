using UnityEngine;

public class Strikes_POTIONCRAFT : MonoBehaviour
{
    [SerializeField] GameObject strike1;
    [SerializeField] GameObject strike2;
    [SerializeField] GameObject strike3;

    static GameObject staticStrike1;
    static GameObject staticStrike2;
    static GameObject staticStrike3;

    static int strike = 0;

    private void Start()
    {
        strike = 0;
        staticStrike1 = strike1;
        staticStrike2 = strike2;
        staticStrike3 = strike3;
    }

    public static void addStrike()
    {
        strike++;

        switch (strike)
        {
            case 1: 
                staticStrike1.SetActive(true); 
                break;
            case 2:
                staticStrike2.SetActive(true);
                break;
            case 3:
                staticStrike3.SetActive(true);
                MinigameManager.SetStateToFailure();
                break;
            default:
                break;
        }
    }
}
