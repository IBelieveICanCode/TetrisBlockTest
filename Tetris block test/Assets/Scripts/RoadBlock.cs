using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;
using System;

namespace TetrisGameSpace
{
    namespace RoadSpace
    {
        public class RoadBlock : MonoBehaviour, IResettable, IDieable
        {
            public event EventHandler Death;

            Gate _gate;
            public void Reset()
            {
                gameObject.SetActive(false);
            }

            public void Die()
            {
                Death?.Invoke(this, null);
            }

            public void Init(Gate gate)
            {
                _gate = gate;
                _gate.transform.position = transform.position;
            }

            void OnBecameInvisible()
            {
                Die();
            }

        }
    }
}
