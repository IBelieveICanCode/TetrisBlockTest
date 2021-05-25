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
            #region Settings param
            private int _numStartSpawn;
            private int _blocksOnLevel;
            GameObject _startRoadPref;
            Road _roadBlockPref;
            FinishRoad _finishBlockPref;
            #endregion

            private Vector3 _nextBlockPosition = Vector3.zero;
            private float _blockLength;

            private Pool<Road> _roadPool;
            Queue<Pool<GateSpace.Gate>> _gatePools; //road will pick up random gate from pool 

            public RoadSpawner(RoadSettings settings)
            {
                _blocksOnLevel = settings.RoadBlocksOnLevel;
                _startRoadPref = settings.StartingRoad;
                _roadBlockPref = settings.RoadBlock;
                _finishBlockPref = settings.FinishBlock;
                _numStartSpawn = settings.NumStartSpawn;

                _blockLength = _roadBlockPref.GetComponent<Renderer>().bounds.size.x;

                _roadPool = new Pool<Road>(new PrefabFactory<Road>(_roadBlockPref.gameObject));
            }

            public void Init(Queue<Pool<GateSpace.Gate>> gatePools)
            {               
                _gatePools = gatePools;
                CreateStartRoad();
            }

            void CreateStartRoad()
            {
                InstantiateStartRoad();
            }

            public void StartSpawnMainRoad()
            {
                for (int i = 0; i < _numStartSpawn; i++)
                    SpawnRoad();
            }

            void SpawnRoad()
            {
                if (ShouldSpawnAnotherRoad())
                {
                    InstantiateMainRoadElement();
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

            void InstantiateMainRoadElement()
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
                GateSpace.Gate gate = GetRandomGate(); //we allow road to pick its gate on it's own
                road.Init(gate);               
            }

            void InstantiateEndOfRoad()
            {
                FinishRoad finish = GameObject.Instantiate(_finishBlockPref);
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
                Pool<GateSpace.Gate> pool = Utility.GetRandomElementFromQueue<Pool<GateSpace.Gate>>(_gatePools); //shuffle queues so we wom't get same pattern of spawning
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

