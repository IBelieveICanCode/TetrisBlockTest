using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisGameSpace
{
    namespace PlayerSpace
    {
        public class PlayerController : MonoBehaviour, IRotatable, ISpeedable, ICameraControllable
        {
            Vector3 _startTouchPos;
            Vector3 _endTouchPos;

            private readonly float _timeToSpeedUp = 2; // seconds to accelerate from zero to minimum speed

            private float _speed = 1;
            private float _maxSpeed;
            private float _minSpeed = 3;

            public float Speed
            {
                get => _speed;
                set
                {
                    if (_speed > MaxSpeed)
                        _speed = MaxSpeed;
                    else if (_speed < MinSpeed)
                        _speed = MinSpeed;
                    else
                        _speed = value;                   
                }
            }

            public float MinSpeed => _minSpeed;
            public float MaxSpeed => _maxSpeed;

            public Transform Position => transform;
            public float OrtographicCoef => (Speed - MinSpeed) / (MaxSpeed - MinSpeed);

            void Start()
            {
                _speed = _minSpeed;
            }
            void Update()
            {
                ControlPlayer();
                if (Input.GetKeyDown(KeyCode.Space))
                    SlowDown();
                //Debug.Log(_speed);
                MoveForward();
            }

            void ControlPlayer()
            {
                //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                if (Input.GetMouseButtonDown(0))
                {
                    _startTouchPos = Input.mousePosition;//Input.GetTouch(0).position;
                }
                //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                if (Input.GetMouseButtonUp(0))
                {
                    //_endTouchPos = Input.GetTouch(0).position;
                    _endTouchPos = Input.mousePosition;

                    Vector3 dirToRot = (Vector3.right * (_endTouchPos.x - _startTouchPos.x)).normalized;
                    float yAxisRotation = 90 * dirToRot.x;
                    Rotate(yAxisRotation);
                }
            }

            void MoveForward()
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.World);
            }

            public void Rotate(float angle)
            {
                transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.y + angle, Vector3.up);
                //transform.Rotate(0, transform.eulerAngles.y + angle, 0, Space.Self); //rotate
            }

            public void SpeedUp(float amount)
            {
                _speed += amount;
                if (_speed > _maxSpeed)
                    _speed = _maxSpeed;
            }

            public void SlowDown()
            {
                StartCoroutine(SlowDownCor());
            }

            IEnumerator SlowDownCor()
            {
                _speed = 0;
                float step = Time.fixedDeltaTime * _minSpeed/ _timeToSpeedUp;
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
