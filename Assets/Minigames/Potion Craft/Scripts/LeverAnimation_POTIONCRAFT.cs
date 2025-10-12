using UnityEngine;

public class LeverAnimation_POTIONCRAFT : MonoBehaviour
{
    private float animationDuration = 1.0f;
    private float duration = 0.0f;
    private bool submitted = false;
    void Update()
    {
        if (submitted)
        {
            duration += Time.deltaTime;
        }
        if (duration <= animationDuration && submitted)
        {
            Transform child = transform.Find("lever_base");
            Transform arm = child.Find("lever_arm");
            Quaternion rotation = arm.rotation;
            rotation.x += Time.deltaTime;
            arm.rotation = rotation;
        }
        else if (duration > animationDuration && submitted)
        {
            Transform child = transform.Find("lever_base");
            Transform arm = child.Find("lever_arm");
            Quaternion rotation = arm.rotation;
            rotation.x -= Time.deltaTime;
            arm.rotation = rotation;
        }

        if (duration > animationDuration * 2.0f && submitted)
        {
            submitted = false;
            duration = 0.0f;
        }
    }

    public void PressLever()
    {
        submitted = true;
        duration = 0.0f;
    }

    public bool Finished()
    {
        return !submitted;
    }

}
