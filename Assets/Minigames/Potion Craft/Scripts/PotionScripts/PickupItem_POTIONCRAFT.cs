using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(ThrowCurrentItem_POTIONCRAFT))]
public class PickupItem_POTIONCRAFT : MonoBehaviour
{
    ColorMixing_POTIONCRAFT cauldronScript;
    private bool itemDropped = true;
    public void HandleInteractPickup(Transform playerTransform)
    {
        Debug.Log("Picked Up: " + tag);
        transform.position = playerTransform.position + playerTransform.forward;
        transform.SetParent(playerTransform.transform);
        itemDropped = false;
    }
    public void HandleInteractDrop()
    {
        transform.SetParent(null);
        Vector3 position = transform.position;
        position.y = 0.5f;
        transform.position = position;
        Debug.Log("Dropped off: " + tag);
        itemDropped = true;
    }
    public bool GetItemDropped()
    {
        return itemDropped;
    }
}
