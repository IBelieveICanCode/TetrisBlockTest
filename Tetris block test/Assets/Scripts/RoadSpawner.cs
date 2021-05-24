using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace TetrisRunnerSpace
{
    namespace RoadSpace
    {
        public class RoadSpawner
        {
            private int _blocksOnLevel;
            GameObject _startRoadPref;
            Road _roadBlockPref;
            FinishBlock _finishBlockPref;

            private Vector3 _nextBlockPosition = Vector3.zero;
            private float _blockLength;

            private Pool<Road> _roadPool;
            Queue<Pool<GateSpace.Gate>> _gatePools;

            public RoadSpawner(RoadSettings settings)
            {
                _blocksOnLevel = settings.RoadBlocksOnLevel;
                _startRoadPref = settings.StartingRoad;
                _roadBlockPref = settings.RoadBlock;
                _finishBlockPref = settings.FinishBlock;

                _blockLength = _roadBlockPref.GetComponent<Renderer>().bounds.size.x;

                _roadPool = new Pool<Road>(new PrefabFactory<Road>(_roadBlockPref.gameObject));
            }

            public void CreateStartRoad(Queue<Pool<GateSpace.Gate>> gatePools)
            {               
                _gatePools = gatePools;
                InstantiateStartRoad();
            }

            public void StartGame()
            {
                for (int i = 0; i < 3; i++)
                    SpawnRoad();
            }

            void SpawnRoad()
            {
                if (ShouldSpawnAnotherRoad())
                {
                    InstantiateNewRoadElement();
                    _nextBlockPosition = CalculateNextBlockPosition();
                }
                else
                    InstantiateEndOfRoad();
            }

            void InstantiateStartRoad()
            {
                GameObject startRoad = GameObject.Instantiate(_startRoadPref);
                startRoad.transform.position = _nextBlockPosition;
                CalculateNextBlockPosition();
            }

            void InstantiateNewRoadElement()
            {
                Road road = _roadPool.Allocate();            
                void handler(object sender, EventArgs e)
                {
                    _roadPool.Release(road);
                    road.Death -= handler;
                    SpawnRoad();
                }
                road.Death += handler;
                road.transform.position = _nextBlockPosition;
                GateSpace.Gate gate = GetRandomGate();
                road.Init(gate);               
            }

            void InstantiateEndOfRoad()
            {
                FinishBlock finish = GameObject.Instantiate(_finishBlockPref);
                finish.transform.position = _nextBlockPosition;
            }

            Vector3 CalculateNextBlockPosition()
            {
                return _nextBlockPosition += Vector3.right * _blockLength;
            }

            bool ShouldSpawnAnotherRoad()
            {
                if (_blocksOnLevel < 0) return false;
                _blocksOnLevel--;
                return true;

            }

            GateSpace.Gate GetRandomGate()
            {
                Pool<GateSpace.Gate> pool = _gatePools.Dequeue();
                GateSpace.Gate myGate = pool.Allocate();
                void handler(object sender, EventArgs e)
                {
                    pool.Release(myGate);
                    myGate.Death -= handler;
                }
                myGate.Death += handler;
                _gatePools.Enqueue(pool);
                return myGate;
            }
        }
    } 
}

