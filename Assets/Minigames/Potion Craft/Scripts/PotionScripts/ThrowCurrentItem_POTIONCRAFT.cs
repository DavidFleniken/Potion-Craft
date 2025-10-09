using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ThrowCurrentItem_POTIONCRAFT : MonoBehaviour
{
    private Rigidbody rb;
    private bool thrown = false;

    private float durationThrown = 0.0f;
    public float aliveDuration = 2.0f;

    public float forwardForce = 8.0f;
    public float upwardForce = 3.0f;

    public bool GetThrown()
    {
        return thrown;
    }
    public void ThrowPotion(Transform playerTransform)
    {
        Debug.Log("Throw!");
        if (!rb) return;
        
        transform.position += Vector3.up * 0.5f;

        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;

        Vector3 forward = playerTransform.forward;
        forward.y = 0;
        forward.Normalize();

        rb.AddForce(forward * forwardForce + Vector3.up * upwardForce, ForceMode.Impulse);
        rb.angularVelocity = new Vector3(10f, 20f, 0f);
        thrown = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (thrown)
        {
            durationThrown += Time.deltaTime;
        }

        if (durationThrown >= aliveDuration)
        {
            Debug.Log("destroying");
            Destroy(gameObject);
        }
    }
}
