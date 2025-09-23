using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PotionType
{
    None = 0,
    Red,
    Green,
    Blue
}
public class PickupItem_POTIONCRAFT : MonoBehaviour, MinigameSubscriber
{
    public PotionType potionType = PotionType.Red;
    bool playerInRange = false;
    bool pickedUp = false;
    public void OnMinigameStart()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (potionType == PotionType.Red)
        {
            renderer.material.color = Color.red;
        }
        else if (potionType == PotionType.Green)
        {
            renderer.material.color = Color.green;
        }
        else if (potionType == PotionType.Blue)
        {
            renderer.material.color = Color.blue;
        }
    }

    public void OnTimerEnd()
    {
        
    }

    public void HandleInteractPressed(GameObject player)
    {
        if (playerInRange)
        {
            PlayerInventory_POTIONCRAFT playerInventory = player.GetComponent<PlayerInventory_POTIONCRAFT>();
            if (!pickedUp && playerInventory.GetPotion() == null)
            {
                Debug.Log("Picked Up: " + tag);
                transform.position = player.transform.position + player.transform.forward;
                transform.SetParent(player.transform);
                player.GetComponent<PlayerInventory_POTIONCRAFT>().SetPotion(gameObject);
                pickedUp = true;
            }
            else if(pickedUp && playerInventory.GetPotion() == gameObject)
            {
                Debug.Log("Dropped off: " + tag);
                transform.SetParent(null);
                player.GetComponent<PlayerInventory_POTIONCRAFT>().SetPotion(null);
                pickedUp = false;
            }
        }
    }

    private void HandleInteractReleased(GameObject player)
    {
        
    }
    void Start()
    {
        MinigameManager.Subscribe(this);
    }

    void Destroy()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
