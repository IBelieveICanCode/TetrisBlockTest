using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace TetrisGameSpace
{

    public class GateSpawner : MonoBehaviour
    {
        [SerializeField]
        GateBlock _gateBlock;
        [SerializeField]
        GameObject _outerGate;
        Renderer _gateBottom;
        [SerializeField]
        int _height = 8;
        readonly float _yGaps = 0.11f;

        private MonoBehaviourFactory<Gate> _gateFactory;
        private Pool<GateBlock> _blockPool;

        private readonly List<Gate> _gates;
        public List<Gate> Gates => _gates;
        

        private void Start()
        {
            _gateFactory = new MonoBehaviourFactory<Gate>("Gate");
            IRotatable _player = FindObjectOfType<PlayerSpace.PlayerController>().GetComponent<IRotatable>();
            for (int i = 0; i < 4; i++) // 4 times we will rotate player. That covers exactly 360 degrees
            {
                CreateNewGate();
            }
            StartCoroutine(ExcludeBlocksFromGates(_player));

        }


        void CreateNewGate()
        {
            Gate gate = _gateFactory.Create();

            GameObject futureBottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
            futureBottom.name = "GateBottom";
            futureBottom.transform.localScale = new Vector3(0.1f, 0.01f, 1);
            futureBottom.transform.parent = gate.transform;
            _gateBottom = futureBottom.GetComponent<Renderer>();

            GameObject outerGate =  Instantiate(_outerGate);
            outerGate.name = "DecorGate";
            outerGate.transform.parent = gate.transform;

            _blockPool = new Pool<GateBlock>(new PrefabFactory<GateBlock>(_gateBlock.gameObject), 10);
            float gaps = 0.1f;
            int row = Mathf.RoundToInt((_gateBottom.bounds.size.z / 0.11f)) - 1;// so we can skip last element. We don't need a cubes to be spawned in gate panels

            for (int i = 0; i < _height; i++)
            {
                float threshold = 0;
                for (int k = 1; k < row; k++) //Same but for first element
                {
                    threshold += _yGaps;

                    GateBlock block = _blockPool.Allocate();
                    void handler(object sender, EventArgs e)
                    {
                        _blockPool.Release(block);
                        block.Death -= handler;
                    }
                    block.Death += handler;

                    block.gameObject.SetActive(true);
                    block.transform.position = new Vector3(_gateBottom.bounds.max.x - 0.05f, gaps, _gateBottom.bounds.max.z - 0.05f - threshold);
                    block.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    block.transform.parent = _gateBottom.transform;

                    gate.Blocks.Add(block);
                }
                gaps += _yGaps;
            }
            _gates.Add(gate);
            gate.gameObject.SetActive(false);
        }

        //Every block has OnTriggerEnter method, which triggers on player blocks after one frame. 
        //So we wait one frame, disable blocks and rotate player 90 degrees. Repeat with all gate "prefabs" we have
        IEnumerator ExcludeBlocksFromGates(IRotatable player)   
        {
            for (int i = 0; i < _gates.Count; i++)
            {
                _gates[i].gameObject.SetActive(true);
                yield return new WaitForEndOfFrame();
                _gates[i].DisableBlocks();
                _gates[i].gameObject.SetActive(false);
                player.Rotate(-90);
            }
        }
    }
}

