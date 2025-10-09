using UnityEngine;

public class ItemCollision_POTIONCRAFT : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<ThrowCurrentItem_POTIONCRAFT>().GetThrown() && !(other.CompareTag("Item") || other.CompareTag("Player")))
        {
            Debug.Log("Destroy 3");
            Destroy(gameObject);
        }
    }
}
