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
    [SerializeField] float speed = 10f;

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
        if (!MinigameManager.IsReady()) // IMPORTANT: Don't allow any input while the countdown is still occuring
            return;

        MinigameManager.SetStateToSuccess(); // Change the minigame state to "Success"
        MinigameManager.EndGame(); // End the minigame. Without this, the minigame would end when the timer finishes instead (still with success).
    }

    void OnMove(InputValue val)
    {
        if (!MinigameManager.IsReady()) // IMPORTANT: Don't allow any input while the countdown is still occuring
            return;

        Vector2 ValInput = val.Get<Vector2>() * speed; // Get the Vector2 that represents input
        input = new Vector3(ValInput.x, 0, ValInput.y); // map 2d vector to 3d vector
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
