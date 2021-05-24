using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace TetrisRunnerSpace
{
    namespace PlayerSpace
    {
        public class PlayerCreator
        {
            private readonly float _gaps;
            private readonly int _numHeightBlocks;
            private readonly int _numWidthBlock;
            private int _numDoubleBranches;
            readonly GameObject _blockPrefab; 

            private readonly MonoBehaviourFactory<PlayerController> _playerFactory;

            private PlayerController _player;
            
            public PlayerCreator(PlayerSettings settings)
            {
                _numHeightBlocks = settings.NumHeightBlocks;
                _numWidthBlock = settings.NumWidthBlocks;
                _numDoubleBranches = settings.NumDoubleBranches;
                _blockPrefab = settings.BlockPrefab;
                _gaps = settings.Gap;

                _playerFactory = new MonoBehaviourFactory<PlayerController>("Player");
            }

            public PlayerController CreatePlayer()
            {
                _player = _playerFactory.Create();
                PlaceBlocks();
                return _player;
            }

            void PlaceBlocks()
            {
                //First place height blocks
                Vector3 startPosition = (Vector3.up * 0.1f) + (Vector3.forward * 0.01f); // we need to adjust our player to the middle of gate block
                List<Transform> heightBlocks = new List<Transform>();

                for (int i = 0; i < _numHeightBlocks; i++)
                {
                    Transform element = GameObject.Instantiate(_blockPrefab).transform;
                    element.parent = _player.transform;
                    element.position = startPosition;
                    element.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                    heightBlocks.Add(element);
                    startPosition.y += _gaps;
                }
                PlaceWidthBLocks(heightBlocks);
            }

            void PlaceWidthBLocks(List<Transform> heightBlocks)
            {
                int seed = Random.Range(0, int.MaxValue);
                Transform[] shuffledBlocks = Utility.ShuffleArray(heightBlocks.ToArray(), seed);

                Vector3[] directionsFromBlock = new Vector3[4] { Vector3.left, Vector3.forward, Vector3.right, Vector3.back };
                Queue<Vector3> shuffledDirectionsFromBlock = new Queue<Vector3>(Utility.ShuffleArray(directionsFromBlock, seed));

                for (int i = 0; i < _numWidthBlock; i++)
                {
                    Vector3 whereToPlaceBranch = shuffledDirectionsFromBlock.Dequeue();
                    shuffledDirectionsFromBlock.Enqueue(whereToPlaceBranch);

                    PlaceBranchToBlock(shuffledBlocks[i], whereToPlaceBranch, true);
                }
            }

            void PlaceBranchToBlock(Transform block, Vector3 whereTo, bool doublebranching)
            {
                Vector3 place = block.transform.position + (whereTo * _gaps);
                Transform branch = GameObject.Instantiate(_blockPrefab).transform;
                branch.transform.position = place;
                branch.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                branch.parent = block;

                if (_numDoubleBranches > 0 && doublebranching)
                {
                    _numDoubleBranches--;
                    PlaceBranchToBlock(branch, whereTo, false);                  
                }
            }
        }
    }
}
