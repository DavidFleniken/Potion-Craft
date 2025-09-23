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
public class PlayerController_POTIONCRAFT : MonoBehaviour, MinigameSubscriber
{
    //make events that other scripts can subscribe to, keeps code cleaner
    public static event System.Action<GameObject> OnInteractPressed;
    public static event System.Action<GameObject> OnInteractReleased;
    
    [SerializeField] float speed = 10f;
    [SerializeField] float interactRange = 3f;

    private Rigidbody rb;
    private Vector3 input;
    
    void Start()
    {
        // Subscribes this class to the minigame manager. This gives access to the
        // 'OnMinigameStart()' and 'OnTimerEnd()' functions. Otherwise, they won't be called.
        MinigameManager.Subscribe(this);
        rb = GetComponent<Rigidbody>();
    }

    void OnInteract(InputValue val)
    {
        if (!MinigameManager.IsReady())
            return;
            
        if (val.isPressed)
        {
            GameObject closestPotion = FindClosestPotion();
            if (closestPotion != null)
            {
                PickupItem_POTIONCRAFT potionScript = closestPotion.GetComponent<PickupItem_POTIONCRAFT>();
                if (potionScript != null)
                {
                    potionScript.HandleInteractPressed(gameObject);
                }
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

        Vector2 ValInput = val.Get<Vector2>() * speed; // Get the Vector2 that represents input
        input = new Vector3(ValInput.x, 0, ValInput.y); // map 2d vector to 3d vector
        if (ValInput.magnitude > 0.1f)
        {
            // Calculate angle in degrees (0° = forward, 90° = right, etc.)
            float angle = Mathf.Atan2(ValInput.x, ValInput.y) * Mathf.Rad2Deg;
        
            // Snap to 8 directions (optional, remove for smooth rotation)
            angle = Mathf.Round(angle / 45f) * 45f;
        
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        //rb.linearVelocity = input; // moving this to update so that linearVelocity is updated every frame instead of only on changes to movement input
    }

    private void Update()
    {
        // update movement
        input.y = rb.linearVelocity.y; // avoid messing with gravity
        rb.linearVelocity = input;
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
