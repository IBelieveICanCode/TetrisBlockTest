using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisRunnerSpace
{
    namespace PlayerSpace
    {
        public class PlayerSpeedControl : MonoBehaviour, ISpeedable
        {
            private float _timeToSpeedUp; // seconds to accelerate from zero to minimum speed
            private float _acceleration;
            private float _maxSpeed;
            private float _minSpeed;

            private float _speed;
            private bool _isSlowedDown;
            public bool IsSlowedDown => _isSlowedDown;

            public void Init(PlayerSpeedSettings settings)
            {
                _timeToSpeedUp = settings.TimeToSpeedUpFromZero;
                _acceleration = settings.Acceleration;
                _maxSpeed = settings.MaxSpeed;
                _minSpeed = settings.MinSpeed;
                _speed = _minSpeed;
            }
                
            private void Update()
            {
                MoveForward();
            }

            void MoveForward()
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.World);
            }

            public void SpeedUp() // if we hit blocks beforehand we can't accelerate
            {
                if (IsSlowedDown) //TODO StateMachine for player
                    _isSlowedDown = false;
                else
                {
                    _speed += _acceleration;
                    LevelProgress.Instance.Score += 1000;
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
                    LevelProgress.Instance.Score -= 1000;
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
