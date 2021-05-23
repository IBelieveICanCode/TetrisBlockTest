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

            public void Reset()
            {
                gameObject.SetActive(false);
            }

            public void Die()
            {
                Death?.Invoke(this, null);
            }

            public void Init()
            {
            }

            void OnBecameInvisible()
            {
                Die();
            }

        }
    }
}
