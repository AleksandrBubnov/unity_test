using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider[] colliders;
    private Rigidbody[] rigidbodies;

    private void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        RagdollOff();
    }

    public void RagdollOn()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        foreach (var collider in colliders)
            collider.enabled = true;
        foreach (var rigidbody in rigidbodies)
            rigidbody.isKinematic = false;
    }
    public void RagdollOff()
    {
        foreach (var collider in colliders)
            collider.enabled = false;
        foreach (var rigidbody in rigidbodies)
            rigidbody.isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }
}
