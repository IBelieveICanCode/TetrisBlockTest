using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisRunnerSpace
{
    namespace PlayerSpace
    {
        public class PlayerSpeedControl : MonoBehaviour, ISpeedable
        {
            private readonly float _timeToSpeedUp = 2; // seconds to accelerate from zero to minimum speed
            private float _speed = 1;
            private float _acceleration = 0.2f;
            private float _maxSpeed = 7;
            private float _minSpeed = 5;

            private bool _isSlowedDown;
            public bool IsSlowedDown => _isSlowedDown;
                
            private void Update()
            {
                MoveForward();
            }

            void MoveForward()
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.World);
            }

            public void SpeedUp()
            {
                if (IsSlowedDown)
                {
                    _isSlowedDown = false;
                }
                else
                {
                    Debug.Log("accelerate");
                    _speed += _acceleration;
                    if (_speed > _maxSpeed)
                        _speed = _maxSpeed;
                }
            }

            public void SlowDown()
            {
                if (!IsSlowedDown)
                {
                    _isSlowedDown = true;
                    StartCoroutine(SlowDownCor());
                }
            }
            IEnumerator SlowDownCor()
            {
                _speed = 0;
                float step = Time.fixedDeltaTime * _minSpeed / _timeToSpeedUp;
                while (_speed < _minSpeed)
                {
                    _speed += step;
                    yield return new WaitForFixedUpdate();
                }
                _speed = _minSpeed;
            }
            public void Stop()
            {
                _speed = 0;
            }
        }
    }
}
