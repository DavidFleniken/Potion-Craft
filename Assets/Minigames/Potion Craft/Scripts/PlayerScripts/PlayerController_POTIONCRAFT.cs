using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    This is an example script part of the debug minigame

    The purpose of it is to show you how to properly deal with input
    and use the provided MinigameManager.cs class
*/

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))] // This component must be attached to the GameObject for input to register
[RequireComponent(typeof(PlayerInventory_POTIONCRAFT))]
public class PlayerController_POTIONCRAFT : MonoBehaviour, MinigameSubscriber
{
    [SerializeField] float speed = 10f;
    [SerializeField] float interactRange = 3f;

    private Rigidbody rb;
    private Vector3 input;
    private Vector2 ValInput;
    private Vector2 lastInput;
    private int framesSinceChange;
    private PlayerInventory_POTIONCRAFT inventory;
    private float durationBetweenPresses = 0.0f;
    private bool waitingForSecondPress = false;
    
    void Start()
    {
        // Subscribes this class to the minigame manager. This gives access to the
        // 'OnMinigameStart()' and 'OnTimerEnd()' functions. Otherwise, they won't be called.
        MinigameManager.Subscribe(this);
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<PlayerInventory_POTIONCRAFT>();
    }

    private bool PickupPotion()
    {
        GameObject closestPotion = FindClosestPotion();
        if (closestPotion != null)
        {
            PickupItem_POTIONCRAFT potionScript = closestPotion.GetComponent<PickupItem_POTIONCRAFT>();
            potionScript.HandleInteractPickup(transform);
            inventory.SetPotion(closestPotion);
            return true;
        }

        return false;
    }

    private bool DropPotion()
    {
        GameObject currentPotion = inventory.GetPotion();
        PickupItem_POTIONCRAFT potionScript = currentPotion.GetComponent<PickupItem_POTIONCRAFT>();
        if (currentPotion != null)
        {
            potionScript.HandleInteractDrop();
            //only set potion to null in update when out of duration,
            //we handle the drop but we can still throw hence we need the reference
            return true;
        }
        return false;
    }

    private bool ThrowPotion()
    {
        GameObject currentPotion = inventory.GetPotion();
        if (currentPotion != null)
        {
            currentPotion.GetComponent<ThrowCurrentItem_POTIONCRAFT>().ThrowPotion(transform);
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private void HandlePress()
    {
        GameObject currentPotion = inventory.GetPotion();

        if (currentPotion == null && !waitingForSecondPress)
        {
            PickupPotion();
            return;
        }

        if (!waitingForSecondPress && currentPotion != null)
        {
            if (DropPotion())
            {
                durationBetweenPresses = 0f;
                waitingForSecondPress = true;
            }
        }
        else if (waitingForSecondPress)
        {
            ThrowPotion();
            durationBetweenPresses = 0f;
        }
    }

    void OnInteract(InputValue val)
    {
        if (!MinigameManager.IsReady())
            return;

        if (val.isPressed && val.Get<float>() > 0) 
        {
            if (SubmitPotion_POTIONCRAFT.CanInteract())
            {
                SubmitPotion_POTIONCRAFT.submitPotion();
            }
            else
            {
                HandlePress();
            }
        }
    }

    GameObject FindClosestPotion()
    {
        GameObject[] potions = GameObject.FindGameObjectsWithTag("Item");
        GameObject closestPotion = null;
        float closestDistance = Mathf.Infinity;
    
        foreach (GameObject potion in potions)
        {
            float distance = Vector3.Distance(transform.position, potion.transform.position);
            if (distance < closestDistance && distance <= interactRange)
            {
                closestDistance = distance;
                closestPotion = potion;
            }
        }
    
        return closestPotion;
    }

    void OnMove(InputValue val)
    {
        if (!MinigameManager.IsReady()) // IMPORTANT: Don't allow any input while the countdown is still occuring
            return;

        ValInput = val.Get<Vector2>() * speed; // Get the Vector2 that represents input
    }

    private void FixedUpdate()
    {
        if (waitingForSecondPress)
        {
            durationBetweenPresses += Time.fixedDeltaTime;

            if (durationBetweenPresses > 0.5f)
            {
                waitingForSecondPress = false;
                durationBetweenPresses = 0f;
                inventory.SetPotion(null);
            }
        }
        // Create buffer since otherwise ending on a diagonal is impossible
        if (ValInput != lastInput)
        {
            if (framesSinceChange == 1)
            {
                framesSinceChange = 0;
                lastInput = ValInput;
            }
            else
            {
                framesSinceChange++;
            }
        }
        else 
        {

            input = new Vector3(ValInput.x, 0, ValInput.y); // map 2d vector to 3d vector

            // update movement
            input.y = rb.linearVelocity.y; // avoid messing with gravity
            rb.linearVelocity = input;

            if (ValInput.magnitude > 0.1f)
            {
                // Calculate angle in degrees (0° = forward, 90° = right, etc.)
                float angle = Mathf.Atan2(ValInput.x, ValInput.y) * Mathf.Rad2Deg;

                // Snap to 8 directions (optional, remove for smooth rotation)
                angle = Mathf.Round(angle / 45f) * 45f;

                transform.rotation = Quaternion.Euler(0, angle, 0);
                //Debug.Log("Angle: " + angle + "\nInput: " + input);
            }
        }
    }

    public void OnMinigameStart()
    {
        Debug.Log("Minigame started!");
        // There isn't anything interesting that needs to happen in here for this example
    }

    public void OnTimerEnd()
    {
        // Timer has expired
        MinigameManager.SetStateToFailure();
        MinigameManager.EndGame();
    }
}
