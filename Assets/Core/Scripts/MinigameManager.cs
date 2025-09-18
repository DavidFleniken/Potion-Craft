using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
    static MinigameManager singleton; void Awake() { singleton = this; }
    private enum MinigameState
    {
        READY,
        PLAYING,
        SUCCESS,
        FAILURE
    }
    private MinigameState mstate = MinigameState.READY;

    private List<MinigameSubscriber> subscribers = new List<MinigameSubscriber>();

    /// <summary>
    /// Subscribes a class of 'MinigameSubscriber' to this manager.
    /// </summary>
    /// <param name="subscriber"></param>
    public static void Subscribe(MinigameSubscriber subscriber)
    {
        singleton.subscribers.Add(subscriber);
    }

    [Header("You're welcome to change these fields:")]
    [SerializeField] [Tooltip("The length of the minigame timer, in seconds")] private float minigameLength = 5f;

    void Start()
    {
        CoreUI.Timer.maxValue = minigameLength;
        CoreUI.Timer.value = minigameLength;

        // Ready countdown:
        mstate = MinigameState.READY;
        StartCoroutine(ReadyCountdown());
    }

    IEnumerator ReadyCountdown()
    {
        Time.timeScale = 0f;
        for (int i = 3; i > 0; i--)
        {
            CoreUI.CountdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        // Start game
        Time.timeScale = 1f;
        mstate = MinigameState.PLAYING;
        CoreUI.CountdownText.gameObject.SetActive(false);

        foreach (MinigameSubscriber s in subscribers)
            s.OnMinigameStart();
    }

    void Update()
    {
        CoreUI.Timer.value -= Time.deltaTime;
        if (CoreUI.Timer.value <= CoreUI.Timer.minValue)
        {
            foreach (MinigameSubscriber s in subscribers)
                s.OnTimerEnd();
        }
    }

    // Exposed functions
    public static bool IsReady()
    {
        return singleton.mstate != MinigameState.READY;
    }

    public static void SetStateToSuccess()
    {
        singleton.mstate = MinigameState.SUCCESS;
    }

    public static void SetStateToFailure()
    {
        singleton.mstate = MinigameState.FAILURE;
    }

    public static void EndGame()
    {
        // Validate state
        Debug.Log("Finished minigame with a state of: " + singleton.mstate.ToString());
        if ((int)singleton.mstate < 2)
            Debug.LogError("Finished minigame in ambiguous state. Minigames should finish in a clear 'SUCCESS' or 'FAILURE' state." +
                " Use MinigameManager.SetStateToSuccess() or MinigameManager.SetStateToFailure()");

#if UNITY_EDITOR
       EditorApplication.isPlaying = false;
#else
        // idk. I'll figure it out when I update this script later.
#endif
    }
}  
