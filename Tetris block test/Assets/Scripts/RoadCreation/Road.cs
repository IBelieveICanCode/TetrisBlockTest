using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;
using System;
using TetrisRunnerSpace.GateSpace;

namespace TetrisRunnerSpace
{
    namespace RoadSpace
    {
        public class Road : MonoBehaviour, IResettable, IDieable
        {
            public event EventHandler Death;

            Gate _gate;
            public void Reset()
            {
            }

            public void PoolInit()
            {
            }
            public void Die()
            {
                _gate?.Die();
                Death?.Invoke(this, null);
            }

            public void Init(Gate gate)
            {
                _gate = gate;
                _gate.gameObject.SetActive(true);
                _gate.transform.position = transform.position;
            }

            void OnBecameInvisible()
            {
                Die();
            }
        }
    }
}
