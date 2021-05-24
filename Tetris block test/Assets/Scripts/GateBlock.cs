using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;
using System;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class GateBlock : MonoBehaviour, IResettable, IDieable
{

    public event EventHandler Death;
    public bool IsPrepared = false; // variable which checks if all required blocks are removed for player to pass
    public void Die()
    {
        Death?.Invoke(this, null);
    }

    public void Reset()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        ISpeedable player = other.GetComponentInParent<ISpeedable>();
        if (player == null) return;
        if (!IsPrepared)
        {
            IsPrepared = true;
            Die();
        }


    }


}
