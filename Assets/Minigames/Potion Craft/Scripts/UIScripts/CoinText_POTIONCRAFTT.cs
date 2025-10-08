using TMPro;
using UnityEngine;

public class CoinText_POTIONCRAFT : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        text.text = SubmitPotion_POTIONCRAFT.totalCoins + "/" + SubmitPotion_POTIONCRAFT.coinsToWin;
    }
}
