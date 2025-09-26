using UnityEngine;

public class CollisionCauldron_POTIONCRAFT : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            bool itemDropped = other.GetComponent<PickupItem_POTIONCRAFT>().GetItemDropped();
            bool itemThrown = other.GetComponent<ThrowCurrentItem_POTIONCRAFT>().GetThrown();

            if (itemDropped || itemThrown)
            {
                GetComponent<ColorMixing_POTIONCRAFT>().addColor(other.GetComponent<ItemProperties_POTIONCRAFT>().GetColor());
                Destroy(other.gameObject);
            }
        }
    }
}
