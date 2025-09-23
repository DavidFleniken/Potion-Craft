using UnityEngine;

public class PlayerInventory_POTIONCRAFT : MonoBehaviour
{
    GameObject currentPotion = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPotion(GameObject potion)
    {
        currentPotion = potion;
    }

    public GameObject GetPotion()
    {
        return currentPotion;
    }
}
