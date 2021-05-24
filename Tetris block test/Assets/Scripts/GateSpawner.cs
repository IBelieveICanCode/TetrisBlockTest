using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace TetrisRunnerSpace
{
    namespace GateSpace
    {
        public class GateSpawner
        {
            int _numInColumn;
            GateBlock _gateBlock;
            GameObject _outerGate;
            float _gaps;
            IRotatable _player;

            Renderer _gateBottom;

            //Explaining for this abominations at bottom of the script
            private readonly List<Gate> _gates = new List<Gate>();
            private readonly Queue<Pool<Gate>> _gatePools = new Queue<Pool<Gate>>();
            public Queue<Pool<Gate>> GatePools => _gatePools;
            public GateSpawner(IRotatable player, GateSettings settings, float gap)
            {
                _numInColumn = settings.NumInColumn;
                _gateBlock = settings.GateBlock;
                _outerGate = settings.OuterGate;
                _gaps = gap;
                _player = player;
            }
            public void CreateGatePools()
            {
                for (int i = 0; i < 4; i++) // I hardcoded it cos we we will rotate player 4 times for 90 degrees. That covers exactly 360 degrees
                    CreateNewTypeOfGate();

                Gate gate = new MonoBehaviourFactory<Gate>("Gate").Create();
                gate.StartCoroutine(ExcludeBlocksFromGates()); //I needed any Monobehaviour to start coroutine
            }

            void CreateNewTypeOfGate()
            {
                Gate gate = new MonoBehaviourFactory<Gate>("Gate").Create();
                GameObject futureBottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
                futureBottom.name = "GateBottom";
                futureBottom.transform.localScale = new Vector3(_gaps, 0.01f, 1);
                futureBottom.transform.parent = gate.transform;
                _gateBottom = futureBottom.GetComponent<Renderer>();

                GameObject outerGate = GameObject.Instantiate(_outerGate);
                outerGate.name = "DecorGate";
                outerGate.transform.parent = gate.transform;

                GateSpeedUpPlayer speedUpcollider = new MonoBehaviourFactory<GateSpeedUpPlayer>("Speed up Col").Create();
                speedUpcollider.transform.position += Vector3.right * 0.2f; //We need to place it just behind the gate +-
                speedUpcollider.transform.parent = gate.transform;
                speedUpcollider.transform.eulerAngles = Vector3.zero;

                float yGaps = 0.1f;
                int numInRow = Mathf.RoundToInt((_gateBottom.bounds.size.z / _gaps)) - 1;// so we can skip last element. We don't need a cubes to be spawned in gate panels
                for (int i = 0; i < _numInColumn; i++)
                {
                    float xGaps = 0;
                    for (int k = 1; k < numInRow; k++) //We start from 1 element, for the same reason but for the first element
                    {
                        xGaps += _gaps;

                        GateBlock block = GameObject.Instantiate(_gateBlock);
                        block.transform.position = new Vector3(_gateBottom.bounds.max.x - 0.05f, yGaps, _gateBottom.bounds.max.z - 0.05f - xGaps);
                        block.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        block.transform.parent = _gateBottom.transform;

                        gate.Blocks.Add(block);
                    }
                    yGaps += _gaps;
                }
                _gates.Add(gate);
                gate.gameObject.SetActive(false);
            }

            //Every block has OnTriggerEnter method, which triggers on player, we wait for physics to work, disable blocks and rotate player 90 degrees. Repeat with all gate "prefabs" we have
            IEnumerator ExcludeBlocksFromGates()
            {
                for (int i = 0; i < _gates.Count; i++)
                {
                    _gates[i].gameObject.SetActive(true);
                    yield return new WaitForFixedUpdate();
                    _gates[i].BlocksArePrepared();
                    _gates[i].gameObject.SetActive(false);
                    _player.Rotate(-90);
                }
                CreateObjectPoolsForGates();
            }

            //We want to pool every 4 of our "prefab" gates we create, so we don't instantiate them every time we need a gate
            void CreateObjectPoolsForGates()
            {
                foreach (Gate gate in _gates)
                {
                    Pool<Gate> newPool = new Pool<Gate>(new PrefabFactory<Gate>(gate.gameObject));
                    _gatePools.Enqueue(newPool);
                }
            }
        }
    }
}

