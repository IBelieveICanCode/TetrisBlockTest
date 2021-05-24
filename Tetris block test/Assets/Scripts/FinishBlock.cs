﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace TetrisRunnerSpace
{
    namespace RoadSpace
    {
        [RequireComponent(typeof(Rigidbody))]
        public class FinishBlock : MonoBehaviour, IResettable
        {
            private void OnTriggerEnter(Collider other)
            {               
                ISpeedable player = other.GetComponentInParent<ISpeedable>();
                if (player != null)
                {
                    player.Stop();
                }             
            }
            public void Reset()
            {
                gameObject.SetActive(false);
            }
            public void PoolInit()
            {
                gameObject.SetActive(true);
            }
        }
    }
}
