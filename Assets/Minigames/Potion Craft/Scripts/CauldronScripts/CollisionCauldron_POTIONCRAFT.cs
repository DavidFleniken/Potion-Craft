using System;
using UnityEngine;

public class CollisionCauldron_POTIONCRAFT : MonoBehaviour
{
    void HandleItem(GameObject item)
    {
        bool itemDropped = item.GetComponent<PickupItem_POTIONCRAFT>().GetItemDropped();
        bool itemThrown = item.GetComponent<ThrowCurrentItem_POTIONCRAFT>().GetThrown();

        if (itemDropped || itemThrown)
        {
            GetComponent<ColorMixing_POTIONCRAFT>().addColor(item.GetComponent<ItemProperties_POTIONCRAFT>().GetColor());
            Destroy(item.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            HandleItem(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            HandleItem(other.gameObject);
        }
    }
}
