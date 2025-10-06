using UnityEngine;

public enum PotionType
{
    None = 0,
    Red,
    Green,
    Blue
}
public class ItemProperties_POTIONCRAFT : MonoBehaviour, MinigameSubscriber
{
    Color curColor;
    public PotionType initialPotionType = PotionType.Red;
    public void OnMinigameStart()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (initialPotionType == PotionType.Red)
        {
            renderer.material.color = Color.red;
        }
        else if (initialPotionType == PotionType.Green)
        {
            renderer.material.color = Color.green;
        }
        else if (initialPotionType == PotionType.Blue)
        {
            renderer.material.color = Color.blue;
        }

        curColor = renderer.material.color;
    }

    public void OnTimerEnd()
    {
        
    }
    public Color GetColor()
    {
        return curColor;
    }
    void Start()
    {
        MinigameManager.Subscribe(this);
    }
}
