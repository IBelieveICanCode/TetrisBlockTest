using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace TetrisRunnerSpace
{
    namespace GateSpace
    {
        public class Gate : MonoBehaviour, IResettable, IDieable
        {
            private List<GateBlock> _blocks = new List<GateBlock>();
            public List<GateBlock> Blocks => _blocks;
 
            public event EventHandler Death;
            GateSpeedUpPlayer _speedUp;

            public void Die()
            {
                Death?.Invoke(this, null);
            }

            public void BlocksArePrepared()
            {
                foreach (GateBlock block in _blocks)
                    block.IsPrepared = true;
            }

            public void Reset()
            {
                foreach (GateBlock block in _blocks)
                    block.Reset(); //restore positions of blocks
                gameObject.SetActive(false);
                _speedUp.gameObject.SetActive(true); //activate speed up collider. It was disabled after contacting player
            }

            public void PoolInit()
            {
                GateBlock[] blocks = GetComponentsInChildren<GateBlock>();
                _blocks = new List<GateBlock>(blocks);
                _speedUp = GetComponentInChildren<GateSpeedUpPlayer>();
                gameObject.SetActive(true);
            }
        }
    }
}
