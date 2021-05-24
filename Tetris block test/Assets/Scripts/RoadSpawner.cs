using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace TetrisGameSpace
{
    namespace RoadSpace
    {
        public class RoadSpawner : MonoBehaviour
        {
            [SerializeField]
            private int _blocksOnLevel = 10;

            [SerializeField]
            RoadBlock _roadBlockPref;
            [SerializeField]
            FinishBlock _finishBlockPref;

            private Pool<RoadBlock> _roadPool;
            [SerializeField]
            private int _poolSize = 5;

            private Vector3 _nextBlockPosition = Vector3.zero;
            private float _blockLength;

            private Queue<Gate> _gates;
            private Gate _pickedGate => Utility.GetRandomElementFromQueue(ref _gates);

            private void Awake()
            {
                _blockLength = _roadBlockPref.GetComponent<Renderer>().bounds.size.x;
                _roadPool = new Pool<RoadBlock>(new PrefabFactory<RoadBlock>(_roadBlockPref.gameObject), _poolSize);
            }

            public void Init(List<Gate> gates)
            {               
                _gates = new Queue<Gate>(gates.ToArray());
                for (int i = 0; i < 3; i++)
                    SpawnRoad();
            }

            private void SpawnRoad()
            {
                if (ShouldSpawnAnotherRoad())
                {
                    InstantiateNewRoad();
                    _nextBlockPosition = CalculateNextBlockPosition();
                }
                else
                    InstantiateEndOfLevel();
            }

            void InstantiateNewRoad()
            {
                RoadBlock road = _roadPool.Allocate();
                
                void handler(object sender, EventArgs e)
                {
                    _roadPool.Release(road);
                    road.Death -= handler;
                    SpawnRoad();
                }

                road.Death += handler;
                road.gameObject.SetActive(true);
                road.transform.position = _nextBlockPosition;
                road.Init(_pickedGate);               
            }

            void InstantiateEndOfLevel()
            {
                FinishBlock finish = Instantiate(_finishBlockPref);
                finish.transform.position = _nextBlockPosition;
            }

            private Vector3 CalculateNextBlockPosition()
            {
                return _nextBlockPosition += Vector3.right * _blockLength;
            }

            bool ShouldSpawnAnotherRoad()
            {
                if (_blocksOnLevel < 0) return false;
                _blocksOnLevel--;
                return true;

            }
        }
    } 
}

