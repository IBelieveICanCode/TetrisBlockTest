using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace TetrisGameSpace
{
    namespace PlayerSpace
    {
        public class CameraController : MonoBehaviour
        {
            [SerializeField]
            private float _maxOrtographicSize;
            [SerializeField]
            private float _basicOrtographicSize = 6;
            private CinemachineVirtualCamera _vcam;
            private ICameraControllable _player;
            private readonly float koef = 0.0025f;

            public void Start()//ICameraControllable player)
            {
                 // !!!!!!!!!!!
                _player = FindObjectOfType<PlayerController>().GetComponent<ICameraControllable>();
                _vcam = GetComponent<CinemachineVirtualCamera>();
                _vcam.Follow = _player.Position;
            }

            private void Update()
            {
                //IncreaseOrtographicSize();
            }

            public void RemoveCameraFollow()
            {
                _vcam.LookAt = null;
                _vcam.Follow = null;
            }

            private void IncreaseOrtographicSize()
            {
                if (_vcam.m_Lens.OrthographicSize < Mathf.Lerp(_basicOrtographicSize, _maxOrtographicSize, _player.OrtographicCoef))
                {
                    _vcam.m_Lens.OrthographicSize += koef;
                }
                else if (_vcam.m_Lens.OrthographicSize > Mathf.Lerp(_basicOrtographicSize, _maxOrtographicSize, _player.OrtographicCoef))
                    _vcam.m_Lens.OrthographicSize -= koef;
            }
        }
    }
}
