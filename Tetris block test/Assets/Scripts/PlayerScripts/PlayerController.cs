using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisRunnerSpace
{
    namespace PlayerSpace
    {
        public class PlayerController : MonoBehaviour, IRotatable, ICameraControllable
        {
            Vector3 _startTouchPos;
            Vector3 _endTouchPos;       
            public Transform Position => transform;

            void Update()
            {
                ControlPlayer();
                //Debug.Log(_speed);               
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

            public void Rotate(float angle)
            {
                transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.y + angle, Vector3.up);
                //transform.Rotate(0, transform.eulerAngles.y + angle, 0, Space.Self); //rotate
            }          
        }
    }
}
