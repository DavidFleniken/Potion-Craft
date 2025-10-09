using UnityEngine;

public class Spawner_POTIONCRAFT : MonoBehaviour
{
    public PotionType potionType;
    public GameObject prefab;

    Quaternion spawnRot = new Quaternion(0.706984699f, 0, 0, -0.707228839f);

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
        GameObject instance = Instantiate(prefab, transform.position + Vector3.up * 0.2f, spawnRot);
        var props = instance.GetComponent<ItemProperties_POTIONCRAFT>();
        props.initialPotionType = potionType;
    }
}