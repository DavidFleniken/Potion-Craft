using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemCollision_POTIONCRAFT : MonoBehaviour
{
    private ThrowCurrentItem_POTIONCRAFT thrower;
    private PickupItem_POTIONCRAFT picker;
    private Collider collider;

    [SerializeField] private float surfaceOffset = 0.02f; // small lift to avoid clipping

    private void Awake()
    {
        thrower = GetComponent<ThrowCurrentItem_POTIONCRAFT>();
        picker = GetComponent<PickupItem_POTIONCRAFT>();
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (thrower.GetThrown())
        {
            if (!(other.CompareTag("Item") || other.CompareTag("Player")))
            {
                Destroy(gameObject);
                return;
            }
        }

        TrySnapToCounterTop(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TrySnapToCounterTop(other);
    }

    private void TrySnapToCounterTop(Collider other)
    {
        if (!thrower.GetThrown() && picker.GetItemDropped() && other.CompareTag("CounterTop"))
        {
            float topY = other.bounds.max.y;
            Vector3 pos = transform.position;
            float itemHalfHeight = collider.bounds.extents.y/2.0f;
            pos.y = topY + surfaceOffset + itemHalfHeight;
            transform.position = pos;
        }
    }
}