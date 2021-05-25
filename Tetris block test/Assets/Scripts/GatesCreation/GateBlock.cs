using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TetrisRunnerSpace
{
    namespace GateSpace
    {
        [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
        public class GateBlock : MonoBehaviour
        {
            private bool _isMoved;
            public bool IsPrepared = false; // variable which checks if this block prevents player from moving past gate

            private Vector3 _startPos;
            private Rigidbody _rb;

            public void Start()
            {
                _rb = GetComponent<Rigidbody>();
                _rb.isKinematic = true;
                BoxCollider col = GetComponent<BoxCollider>();
                col.isTrigger = true;
            }

            private void Disable()
            {
                gameObject.SetActive(false);
            }

            public void Reset()
            {
                _rb.isKinematic = true;
                if (_isMoved) transform.position = _startPos;
                _isMoved = false;
            }

            // we need to remove blocks which makes impossible for player to traverse through gate. 
            //We make them all prepared after first creation in Gate class
            private void OnTriggerEnter(Collider other)
            {
                if (!IsPrepared) 
                {
                    IRotatable player = other.GetComponentInParent<IRotatable>();
                    if (player == null) return;
                    IsPrepared = true;
                    Disable();
                }
                else 
                {
                    ISpeedable playerSpeed = other.GetComponentInParent<ISpeedable>();
                    if (playerSpeed == null) return;
                    _isMoved = true;
                    _startPos = transform.position;
                    playerSpeed.SlowDown();
                    _rb.isKinematic = false;
                    _rb.AddForce(Vector3.right, ForceMode.Impulse);
                }
            }
        }
    }
}
