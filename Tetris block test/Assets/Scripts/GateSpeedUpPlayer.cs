using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class GateSpeedUpPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = new Vector3(0.1f, 1f, 0.1f);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ISpeedable player = other.GetComponentInParent<ISpeedable>();
        if (player == null) return;
        player.SpeedUp();

    }
}
