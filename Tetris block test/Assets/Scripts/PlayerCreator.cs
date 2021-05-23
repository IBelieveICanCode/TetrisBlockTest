using System.Collections.Generic;
using UnityEngine;

namespace TetrisGameSpace
{
    namespace PlayerSpace
    {
        public class PlayerCreator
        {
            private readonly float _gaps = 0.11f;
            private readonly int _numHeightBlocks;
            private readonly int _numWidthBlock;
            private int _numDoubleBranches;

            private readonly List<Transform> _heightBlocks = new List<Transform>();
            public PlayerCreator(int numHeightBlocks, int numWidthBlocks, int numDoubleBranches)
            {
                _numHeightBlocks = numHeightBlocks;
                _numWidthBlock = numWidthBlocks;
                _numDoubleBranches = numDoubleBranches;
            }

            public void CreatePlayer()
            {
                PlaceHeightBlocks();
                PlaceWidthBLocks();
            }

            void PlaceHeightBlocks()
            {
                Vector3 startPosition = Vector3.up * 0.1f;
                GameObject parent = new GameObject("Player");
                for (int i = 0; i < _numHeightBlocks; i++)
                {                
                    Transform element = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
                    element.parent = parent.transform;
                    element.position = startPosition;
                    element.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                    _heightBlocks.Add(element);
                    startPosition.y += _gaps;
                }
                parent.AddComponent<PlayerController>();        
            }

            void PlaceWidthBLocks()
            {
                int seed = Random.Range(0, int.MaxValue);
                Transform[] shuffledBlocks = Utility.ShuffleArray(_heightBlocks.ToArray(), seed);

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
                Transform branch = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
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
