using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class GateSpeedUpPlayer : MonoBehaviour
{
    BoxCollider _col;
    Rigidbody _rb;
    // Start is called before the first frame update
    void Awake()
    {
        _col = GetComponent<BoxCollider>();
        _col.isTrigger = true;
        _col.size = new Vector3(0.1f, 1f, 0.1f);
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ISpeedable player = other.GetComponentInParent<ISpeedable>();
        if (player == null) return;
        player.SpeedUp();
        gameObject.SetActive(false);

    }


}
