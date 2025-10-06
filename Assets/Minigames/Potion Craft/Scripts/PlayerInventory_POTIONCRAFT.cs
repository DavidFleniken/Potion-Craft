using UnityEngine;

public class PlayerInventory_POTIONCRAFT : MonoBehaviour
{
    GameObject currentPotion = null;

    public void SetPotion(GameObject potion)
    {
        currentPotion = potion;
    }

    public GameObject GetPotion()
    {
        return currentPotion;
    }
}
