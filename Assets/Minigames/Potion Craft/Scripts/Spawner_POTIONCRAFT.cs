using UnityEngine;

public class Spawner_POTIONCRAFT : MonoBehaviour
{
    public PotionType potionType;
    public GameObject prefab;
    private ItemProperties_POTIONCRAFT properties;
    void Start()
    {
        properties = prefab.GetComponent<ItemProperties_POTIONCRAFT>();
        properties.initialPotionType = potionType;
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
        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
        
        obj.SetActive(true);
    }

}
