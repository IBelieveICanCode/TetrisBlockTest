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

            private Pool<RoadBlock> _blockPool;
            [SerializeField]
            private int _poolSize = 5;

            private Vector3 _nextBlockPosition = Vector3.zero;
            private float _blockLength;


            private void Awake()
            {
                _blockLength = _roadBlockPref.GetComponent<Renderer>().bounds.size.x;
                _blockPool = new Pool<RoadBlock>(new PrefabFactory<RoadBlock>(_roadBlockPref.gameObject), _poolSize);
            }
            private void Start()
            {                              
                for (int i = 0; i < 3; i++)
                    SpawnBlock();
            }

            private void SpawnBlock()
            {
                Debug.Log("Spawn");
                if (ShouldSpawnAnotherBlock())
                {
                    InstantiateNewBlock();
                    _nextBlockPosition = CalculateNextBlockPosition();
                }
                else
                    InstantiateEndOfLevel();
            }

            void InstantiateNewBlock()
            {
                RoadBlock block = _blockPool.Allocate();
                
                void handler(object sender, EventArgs e)
                {
                    _blockPool.Release(block);
                    block.Death -= handler;
                    SpawnBlock();
                }

                block.Death += handler;
                block.gameObject?.SetActive(true);
                block.transform.position = _nextBlockPosition;
                block.Init();               
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

            bool ShouldSpawnAnotherBlock()
            {
                if (_blocksOnLevel < 0) return false;
                _blocksOnLevel--;
                return true;

            }
        }
    } 
}

