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
        public PlayerSettings PlayerSettings;
        public GateSettings GateSettings;
        public RoadSettings RoadSettings;
    }

    [System.Serializable]
    public struct PlayerSettings
    {
        [SerializeField]
        private GameObject _blockPrefab;
        [SerializeField]
        private int _numHeightBlocks;
        [SerializeField]
        private int _numWidthBlocks;
        [SerializeField]
        private int _numDoubleBranches;
        [Range(0, 1.5f)]
        [SerializeField]
        private float _gap;

        public GameObject BlockPrefab => _blockPrefab;
        public int NumHeightBlocks => _numHeightBlocks;
        public int NumWidthBlocks => _numWidthBlocks;
        public int NumDoubleBranches => _numDoubleBranches;
        public float Gap => _gap;
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
        private int _roadBlocksOnLevel;
        [SerializeField]
        GameObject _startRoad;
        [SerializeField]
        Road _roadBlock;
    [SerializeField]
        FinishBlock _finishBlockPref;

        public int RoadBlocksOnLevel => _roadBlocksOnLevel;
        public GameObject StartingRoad => _startRoad;
        public Road RoadBlock => _roadBlock;
        public FinishBlock FinishBlock => _finishBlockPref;

    }
}

