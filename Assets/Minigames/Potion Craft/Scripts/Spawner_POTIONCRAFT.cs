using UnityEngine;

public class Spawner_POTIONCRAFT : MonoBehaviour
{
    public PotionType potionType;
    public GameObject prefab;

    void Start()
    {
        SpawnPotion();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            SpawnPotion();
        }
    }

    private void SpawnPotion()
    {
        GameObject instance = Instantiate(prefab, transform.position + Vector3.up * 0.2f, Quaternion.identity);
        var props = instance.GetComponent<ItemProperties_POTIONCRAFT>();
        props.initialPotionType = potionType;
    }
}