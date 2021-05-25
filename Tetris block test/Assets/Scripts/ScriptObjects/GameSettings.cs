using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TetrisRunnerSpace.PlayerSpace;
using TetrisRunnerSpace.RoadSpace;
using TetrisRunnerSpace.GateSpace;

namespace TetrisRunnerSpace
{ 
    [CreateAssetMenu(menuName = "GameSettings/Create Game Settings", fileName = "Settings")]
    public class GameSettings : ScriptableObject
    {
        public PlayerConstructSettings PlayerConstructSettings;
        public PlayerSpeedSettings PlayerSpeedSettings;
        public GateSettings GateSettings;
        public RoadSettings RoadSettings;
    }

    [System.Serializable]
    public struct PlayerConstructSettings
    {
        [SerializeField]
        private GameObject _blockPrefab;
        [SerializeField]
        private int _numHeightBlocks;
        [SerializeField]
        private int _numWidthBlocks;
        [SerializeField]
        private int _numDoubleBranches;

        public GameObject BlockPrefab => _blockPrefab;
        public int NumHeightBlocks => _numHeightBlocks;
        public int NumWidthBlocks => _numWidthBlocks;
        public int NumDoubleBranches => _numDoubleBranches;
    }

    [System.Serializable]
    public struct PlayerSpeedSettings
    {
        [SerializeField]
        private  float _timeToSpeedUpFromZero; // seconds to accelerate from zero to minimum speed
        [Range(0, 2f)]
        [SerializeField]
        private float _acceleration;
        [SerializeField]
        private float _maxSpeed;
        [SerializeField]
        private float _minSpeed;

        public float TimeToSpeedUpFromZero => _timeToSpeedUpFromZero;
        public float Acceleration => _acceleration;
        public float MaxSpeed => _maxSpeed;
        public float MinSpeed => _minSpeed;
    }


    [System.Serializable]
    public struct GateSettings
    {
        [SerializeField]
        GateBlock _gateBlock;
        [SerializeField]
        GameObject _outerGate;
        [SerializeField]
        int _numInColumn;

        public GateBlock GateBlock => _gateBlock;
        public GameObject OuterGate => _outerGate;
        public int NumInColumn => _numInColumn;
    }

    [System.Serializable]
    public struct RoadSettings
    {
        [SerializeField]
        private int _numStartSpawn;
        [SerializeField]
        private int _roadBlocksOnLevel;
        [SerializeField]
        GameObject _startRoad;
        [SerializeField]
        Road _roadBlock;
        [SerializeField]
        FinishRoad _finishBlockPref;

        public int NumStartSpawn => _numStartSpawn;
        public int RoadBlocksOnLevel => _roadBlocksOnLevel;
        public GameObject StartingRoad => _startRoad;
        public Road RoadBlock => _roadBlock;
        public FinishRoad FinishBlock => _finishBlockPref;

    }
}

